using System;
using System.Collections;

namespace Gecko.Collections
{
	public class Sorters
	{
		public IList BubbleSort(IList arrayToSort)
		{
			int n = arrayToSort.Count - 1;
			for (int i = 0; i < n; i++)
			{

				for (int j = n; j > i; j--)
				{
					if (((IComparable)arrayToSort[j - 1]).CompareTo(arrayToSort[j]) > 0)
					{
						object temp = arrayToSort[j - 1];
						arrayToSort[j - 1] = arrayToSort[j];
						arrayToSort[j] = temp;
					}
				}
			}
			return arrayToSort;
		}

		public IList BiDerectionalBubbleSort(IList arrayToSort)
		{

			int limit = arrayToSort.Count;
			int st = -1;
			bool swapped = false;
			do
			{
				swapped = false;
				st++;
				limit--;

				for (int j = st; j < limit; j++)
				{
					if (((IComparable)arrayToSort[j]).CompareTo(arrayToSort[j + 1]) > 0)
					{
						object temp = arrayToSort[j];
						arrayToSort[j] = arrayToSort[j + 1];
						arrayToSort[j + 1] = temp;
						swapped = true;
					}
				}
				for (int j = limit - 1; j >= st; j--)
				{
					if (((IComparable)arrayToSort[j]).CompareTo(arrayToSort[j + 1]) > 0)
					{
						object temp = arrayToSort[j];
						arrayToSort[j] = arrayToSort[j + 1];
						arrayToSort[j + 1] = temp;
						swapped = true;
					}
				}

			} while (st < limit && swapped);

			return arrayToSort;
		}

		public IList CombSort(IList arrayToSort)
		{
			int gap = arrayToSort.Count;
			int swaps = 0;

			do
			{
				gap = (int)(gap / 1.247330950103979);
				if (gap < 1)
				{
					gap = 1;
				}
				int i = 0;
				swaps = 0;

				do
				{
					if (((IComparable)arrayToSort[i]).CompareTo(arrayToSort[i + gap]) > 0)
					{
						object temp = arrayToSort[i];
						arrayToSort[i] = arrayToSort[i + gap];
						arrayToSort[i + gap] = temp;
						swaps = 1;
					}
					i++;
				} while (!(i + gap >= arrayToSort.Count));

			} while (!(gap == 1 && swaps == 0));

			return arrayToSort;
		}

		public IList InsertionSort(IList arrayToSort)
		{
			for (int i = 1; i < arrayToSort.Count; i++)
			{
				object val = arrayToSort[i];
				int j = i - 1;
				bool done = false;
				do
				{
					if (((IComparable)arrayToSort[j]).CompareTo(val) > 0)
					{
						arrayToSort[j + 1] = arrayToSort[j];
						j--;
						if (j < 0)
						{
							done = true;
						}
					}
					else
					{
						done = true;
					}
				} while (!done);
				arrayToSort[j + 1] = val;
			}
			return arrayToSort;
		}

		public IList SelectionSort(IList arrayToSort)
		{
			int min;
			for (int i = 0; i < arrayToSort.Count; i++)
			{
				min = i;
				for (int j = i + 1; j < arrayToSort.Count; j++)
				{
					if (((IComparable)arrayToSort[j]).CompareTo(arrayToSort[min]) < 0)
					{
						min = j;
					}
				}
				object temp = arrayToSort[i];
				arrayToSort[i] = arrayToSort[min];
				arrayToSort[min] = temp;
			}

			return arrayToSort;
		}

		public IList CountingSort(IList arrayToSort)
		{
			object min;
			object max;

			min = max = arrayToSort[0];

			for (int i = 0; i < arrayToSort.Count; i++)
			{
				if (((IComparable)arrayToSort[i]).CompareTo(min) < 0)
				{
					min = arrayToSort[i];
				}
				else if (((IComparable)arrayToSort[i]).CompareTo(max) > 0)
				{
					max = arrayToSort[i];
				}
			}

			int range = (int)max - (int)min + 1;

			int[] count = new int[range * sizeof(int)];

			for (int i = 0; i < range; i++)
			{
				count[i] = 0;
			}

			for (int i = 0; i < arrayToSort.Count; i++)
			{
				count[(int)arrayToSort[i] - (int)min]++;
			}
			int z = 0;
			for (int i = (int)min; i < arrayToSort.Count; i++)
			{
				for (int j = 0; j < count[i - (int)min]; j++)
				{
					arrayToSort[z++] = i;
				}
			}

			return arrayToSort;
		}

		public IList ShellSort(IList arrayToSort)
		{
			int i, j, increment;
			object temp;

			increment = arrayToSort.Count / 2;

			while (increment > 0)
			{
				for (i = 0; i < arrayToSort.Count; i++)
				{
					j = i;
					temp = arrayToSort[i];
					while ((j >= increment) && (((IComparable)arrayToSort[j - increment]).CompareTo(temp) > 0))
					{
						arrayToSort[j] = arrayToSort[j - increment];
						j = j - increment;
					}
					arrayToSort[j] = temp;
				}
				if (increment == 2)
					increment = 1;
				else
					increment = increment * 5 / 11;
			}

			return arrayToSort;
		}

		public IList HeapSort(IList list)
		{
			for (int i = (list.Count - 1) / 2; i >= 0; i--)
			{
				Adjust(list, i, list.Count - 1);
			}

			for (int i = list.Count - 1; i >= 1; i--)
			{
				object Temp = list[0];
				list[0] = list[i];
				list[i] = Temp;
				Adjust(list, 0, i - 1);
			}

			return list;
		}

		public void Adjust(IList list, int i, int m)
		{
			object Temp = list[i];
			int j = i * 2 + 1;
			while (j <= m)
			{
				if (j < m)
					if (((IComparable)list[j]).CompareTo(list[j + 1]) < 0)
						j = j + 1;

				if (((IComparable)Temp).CompareTo(list[j]) < 0)
				{
					list[i] = list[j];
					i = j;
					j = 2 * i + 1;
				}
				else
				{
					j = m + 1;
				}
			}
			list[i] = Temp;
		}

		public IList MergeSort(IList a, int low, int height)
		{
			int l = low;
			int h = height;

			if (l >= h)
			{
				return a;
			}

			int mid = (l + h) / 2;
			MergeSort(a, l, mid);
			MergeSort(a, mid + 1, h);

			int end_lo = mid;
			int start_hi = mid + 1;
			while ((l <= end_lo) && (start_hi <= h))
			{
				if (((IComparable)a[l]).CompareTo(a[start_hi]) < 0)
				{
					l++;
				}
				else
				{
					object temp = a[start_hi];
					for (int k = start_hi - 1; k >= l; k--)
					{
						a[k + 1] = a[k];
					}
					a[l] = temp;
					l++;
					end_lo++;
					start_hi++;
				}
			}
			return a;
		}

		public IList QuickSort(IList a, int left, int right)
		{
			int i = left;
			int j = right;
			double pivotValue = ((left + right) / 2);
			int x = (int)a[int.Parse(pivotValue.ToString())];

			while (i <= j)
			{
				while (((IComparable)a[i]).CompareTo(x) < 0)
				{
					i++;
				}
				while (((IComparable)x).CompareTo(a[j]) < 0)
				{
					j--;
				}
				if (i <= j)
				{
					object temp = a[i];
					a[i] = a[j];
					i++;
					a[j] = temp;
					j--;
				}
			}
			if (left < j)
			{
				QuickSort(a, left, j);
			}
			if (i < right)
			{
				QuickSort(a, i, right);
			}
			return a;
		}

		public IList GnomeSort(IList arrayToSort)
		{
			int pos = 1;
			while (pos < arrayToSort.Count)
			{
				if (((IComparable)arrayToSort[pos]).CompareTo(arrayToSort[pos - 1]) >= 0)
				{
					pos++;
				}
				else
				{
					object temp = arrayToSort[pos];
					arrayToSort[pos] = arrayToSort[pos - 1];

					arrayToSort[pos - 1] = temp;
					if (pos > 1)
					{
						pos--;
					}
				}
			}
			return arrayToSort;
		}

		public IList BubbleSort(IList arrayToSort, int left, int right)
		{
			for (int i = left; i < right; i++)
			{
				for (int j = right; j > i; j--)
				{
					if (((IComparable)arrayToSort[j - 1]).CompareTo(arrayToSort[j]) > 0)
					{
						object temp = arrayToSort[j - 1];
						arrayToSort[j - 1] = arrayToSort[j];
						arrayToSort[j] = temp;
					}
				}
			}
			return arrayToSort;
		}

		public IList QuickSortWithBubbleSort(IList a, int left, int right)
		{
			int i = left;
			int j = right;

			if (right - left <= 6)
			{
				BubbleSort(a, left, right);
				return a;
			}

			double pivotValue = ((left + right) / 2);
			int x = (int)a[int.Parse(pivotValue.ToString())];

			a[(left + right) / 2] = a[right];
			a[right] = x;

			while (i <= j)
			{
				while (((IComparable)a[i]).CompareTo(x) < 0)
				{
					i++;
				}
				while (((IComparable)x).CompareTo(a[j]) < 0)
				{
					j--;
				}

				if (i <= j)
				{
					object temp = a[i];
					a[i++] = a[j];
					a[j--] = temp;
				}
			}
			if (left < j)
			{
				QuickSortWithBubbleSort(a, left, j);
			}
			if (i < right)
			{
				QuickSortWithBubbleSort(a, i, right);
			}

			return a;
		}

		public IList BucketSort(IList arrayToSort)
		{
			if (arrayToSort == null || arrayToSort.Count == 0) return arrayToSort;

			object max = arrayToSort[0];
			object min = arrayToSort[0];

			for (int i = 0; i < arrayToSort.Count; i++)
			{
				if (((IComparable)arrayToSort[i]).CompareTo(max) > 0)
				{
					max = arrayToSort[i];
				}

				if (((IComparable)arrayToSort[i]).CompareTo(min) < 0)
				{
					min = arrayToSort[i];
				}
			}
			ArrayList[] holder = new ArrayList[(int)max - (int)min + 1];

			for (int i = 0; i < holder.Length; i++)
			{
				holder[i] = new ArrayList();
			}

			for (int i = 0; i < arrayToSort.Count; i++)
			{
				holder[(int)arrayToSort[i] - (int)min].Add(arrayToSort[i]);
			}

			int k = 0;

			for (int i = 0; i < holder.Length; i++)
			{
				if (holder[i].Count > 0)
				{
					for (int j = 0; j < holder[i].Count; j++)
					{
						arrayToSort[k] = holder[i][j];
						k++;
					}
				}
			}

			return arrayToSort;
		}

		public IList CycleSort(IList arrayToSort)
		{
			try
			{
				int writes = 0;
				for (int cycleStart = 0; cycleStart < arrayToSort.Count; cycleStart++)
				{
					object item = arrayToSort[cycleStart];
					int pos = cycleStart;

					do
					{
						int to = 0;
						for (int i = 0; i < arrayToSort.Count; i++)
						{
							if (i != cycleStart && ((IComparable)arrayToSort[i]).CompareTo(item) < 0)
							{
								to++;
							}

						}
						if (pos != to)
						{
							while (pos != to && ((IComparable)item).CompareTo(arrayToSort[to]) == 0)
							{
								to++;
							}

							object temp = arrayToSort[to];
							lock (this)
							{
								arrayToSort[to] = item;
							}
							item = temp;

							writes++;
							pos = to;
						}
					} while (cycleStart != pos);
				}
			}
			catch (Exception err)
			{
				string errr = err.Message;
			}
			return arrayToSort;
		}

		public IList OddEvenSort(IList arrayToSort)
		{
			bool sorted = false;
			while (!sorted)
			{
				sorted = true;
				for (var i = 1; i < arrayToSort.Count - 1; i += 2)
				{
					if (((IComparable)arrayToSort[i]).CompareTo(arrayToSort[i + 1]) > 0)
					{
						object temp = arrayToSort[i];
						arrayToSort[i] = arrayToSort[i + 1];
						arrayToSort[i + 1] = temp;
						sorted = false;
					}
				}

				for (var i = 0; i < arrayToSort.Count - 1; i += 2)
				{
					if (((IComparable)arrayToSort[i]).CompareTo(arrayToSort[i + 1]) > 0)
					{
						object temp = arrayToSort[i];
						arrayToSort[i] = arrayToSort[i + 1];
						arrayToSort[i + 1] = temp;
						sorted = false;
					}
				}
			}
			return arrayToSort;
		}

		public IList PigeonHoleSort(IList list)
		{
			object min = list[0], max = list[0];
			foreach (object x in list)
			{
				if (((IComparable)min).CompareTo(x) > 0)
				{
					min = x;
				}
				if (((IComparable)max).CompareTo(x) < 0)
				{
					max = x;
				}
			}

			int size = (int)max - (int)min + 1;

			int[] holes = new int[size];

			foreach (int x in list)
				holes[x - (int)min]++;

			int i = 0;
			for (int count = 0; count < size; count++)
				while (holes[count]-- > 0)
				{

					list[i] = count + (int)min;
					i++;
				}
			return list;
		}
	}
}
