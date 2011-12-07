using System;
using System.Collections.Generic;
using System.Linq;
using System.Xml.Linq;

namespace Gecko.Xml
{
	public class NodeInfo
	{
		public NodeInfo()
		{
			ValuesForTypeGuess = new List<string>();
		}

		public XElement Schema { get; set; }

		public bool Repeats { get; set; }
		public string TypeGuess { get; set; }

		public IList<string> ValuesForTypeGuess { get; set; }
	}

	public class XsdFromXml
	{
		private Dictionary<string, NodeInfo> xpaths = new Dictionary<string, NodeInfo>();
		private Dictionary<string, bool> elements = new Dictionary<string, bool>();
		private List<string> recurseElements = new List<string>();

		private XNamespace target;
		private readonly XNamespace xs = XNamespace.Get("http://www.w3.org/2001/XMLSchema");
		private XDocument content = null;

		public XsdFromXml(string filename)
		{
			this.content = XDocument.Load(filename);
		}

		public XsdFromXml(XDocument content)
		{
			this.content = content;
		}

		public XDocument Generate(string targetNamespace)
		{
			xpaths.Clear();
			elements.Clear();
			recurseElements.Clear();

			RecurseAllXPaths(string.Empty, content.Elements().First());

			target = XNamespace.Get(targetNamespace);

			var compTypes = xpaths.Select(k => k.Key)
				.OrderBy(o => o)
				.Select(k => ComplexTypeElementFromXPath(k)).Where(q => null != q).ToList();

			var baseElementType = compTypes.First().Attribute("name").Value;
			var baseElementName = baseElementType.Substring(0, baseElementType.Length - 4); // trim off the "Info"

			compTypes.Add(new XElement(xs + "element",
				new XAttribute("name", baseElementName),
				new XAttribute("type", baseElementType)));

			// The first one is our root element... it needs to be extracted and massage
			// compTypes[0] = compTypes.First().Element(xs + "sequence").Element(xs + "element");

			return XDocument.Parse(
				// Warning: Namespaces are tricky/hinted here, be careful
				new XDocument(new XElement(target + "schema",
				// Why 'qualified'?
				// All "qualified" elements and attributes are in the targetNamespace of the
				// schema and all "unqualified" elements and attributes are in no namespace.
				//  All global elements and attributes are qualified.
				new XAttribute("elementFormDefault", "qualified"),

				// Specify the target namespace, you will want this for schema validation
				new XAttribute("targetNamespace", targetNamespace),

				// hint to xDocument that we want the xml schema namespace to be called 'xs'
				new XAttribute(XNamespace.Xmlns + "xs", "http://www.w3.org/2001/XMLSchema"),
				compTypes)).ToString().Replace("schema", "xs:schema"));
		}

		private void RecurseAllXPaths(string xpath, XElement elem)
		{
			var lclName = elem.Name.LocalName;
			xpath = string.Format("{0}/{1}", xpath, lclName);
			var missingXpath = !xpaths.ContainsKey(xpath);

			var hasLcl = elements.ContainsKey(lclName);


			// Check for recursion in the element name (same name different level)
			if (hasLcl && missingXpath)
				recurseElements.Add(lclName);
			else if (!hasLcl)
				elements.Add(lclName, true);

			// if it's not in the xpath, then add it.
			if (missingXpath)
				xpaths.Add(xpath, new NodeInfo());
			else
				xpaths[xpath].Repeats = true;

			if (!elem.HasElements && !string.IsNullOrEmpty(elem.Value))
				xpaths[xpath].ValuesForTypeGuess.Add(elem.Value);

			elem.Attributes().Where(q => q.Name.LocalName != "xmlns").ToList().ForEach(attr =>
			{
				var xpathAttr = string.Format("{0}/@{1}", xpath, attr.Name);
				// [ToDo] - Add a data type guess here
				if (!xpaths.ContainsKey(xpathAttr))
					xpaths.Add(xpathAttr, new NodeInfo());

				if (!string.IsNullOrEmpty(attr.Value))
					xpaths[xpathAttr].ValuesForTypeGuess.Add(attr.Value);
			});

			elem.Elements().ToList().ForEach(fe => RecurseAllXPaths(xpath, fe));
		}


		private XElement ComplexTypeElementFromXPath(string xp)
		{
			var parts = xp.Split('/');
			var nodeName = parts.Last();
			var isAttr = nodeName.StartsWith("@");
			var parent = ParentElementByXPath(parts);

			var node = xpaths[xp];

			if (node.ValuesForTypeGuess.Count() > 0)
				node.TypeGuess = TypeGuessFromValues(node.ValuesForTypeGuess);

			return (isAttr) ? BuildAttributeSchema(nodeName, node, parent) :
				BuildElementSchema(nodeName, node, parent);
		}

		private XElement BuildElementSchema(string elemName, NodeInfo nodeInfo, NodeInfo parentInfo)
		{
			XElement parent = (null == parentInfo) ? null : parentInfo.Schema;
			XElement seqElem = null;
			if (null != parent)
			{
				seqElem = parent.Element(xs + "sequence");

				if (null == seqElem && null != parent)
					parent.AddFirst(seqElem = new XElement(xs + "sequence"));
			}
			else
			{
				seqElem = new XElement(xs + "sequence");
			}

			var elemNameInfo = elemName + "Info";

			var hasKids = null == nodeInfo || string.IsNullOrEmpty(nodeInfo.TypeGuess);

			var elem0 = new XElement(xs + "element",
						new XAttribute("name", elemName),
						new XAttribute("type", hasKids ? elemNameInfo : nodeInfo.TypeGuess));

			if (null != nodeInfo && nodeInfo.Repeats)
				elem0.Add(new XAttribute("maxOccurs", "unbounded"));

			seqElem.Add(elem0); // add the ref to the existing sequence

			nodeInfo.Schema = new XElement(xs + "complexType",
					new XAttribute("name", elemNameInfo));

			return hasKids ? nodeInfo.Schema : null;
		}

		private XElement BuildAttributeSchema(string attrName, NodeInfo nodeInfo, NodeInfo parentInfo)
		{
			XElement parent = parentInfo.Schema;
			var elem0 = new XElement(xs + "attribute",
				new XAttribute("name", attrName.TrimStart('@')),
				new XAttribute("type", nodeInfo.TypeGuess ?? "xs:string"));

			if (null != parent)
				parent.Add(elem0);

			nodeInfo.Schema = elem0;

			return null;
		}

		private NodeInfo ParentElementByXPath(IEnumerable<string> parts)
		{
			var parentElemXPath =
				string.Join("/", parts.Take(parts.Count() - 1).ToArray());

			NodeInfo parentNode;
			if (xpaths.TryGetValue(parentElemXPath, out parentNode))
				return parentNode;
			else
				return null;
		}

		/// <summary>
		///  given a list of string values (of an attribute or element value), make a guess at the xs:type
		/// </summary>
		/// <param name="values"></param>
		/// <returns></returns>
		private string TypeGuessFromValues(IEnumerable<string> values)
		{
			var firstTen = values.Take(10);  // In case this is a real world sample, just take 10 values

			int iVal;
			if (firstTen.All(fn => int.TryParse(fn, out iVal)))
				return "xs:int";
			decimal dVal;
			if (firstTen.All(fn => decimal.TryParse(fn, out dVal)))
				return "xs:decimal";
			DateTime dtVal;
			if (firstTen.All(fn => DateTime.TryParse(fn, out dtVal)))
				return "xs:date";
			bool bVal;
			if (firstTen.All(fn => bool.TryParse(fn, out bVal)))
				return "xs:boolean";

			return "xs:string";
		}
	}
}
