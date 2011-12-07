using System;
using System.Configuration;
using System.Data.SqlClient;
using Microsoft.SqlServer.Management.Common;
using Microsoft.SqlServer.Management.Smo;
using Microsoft.SqlServer.Management.Smo.Agent;

namespace Gecko.Data
{
	public class SqlJobsController
	{
		public string ConnectionString { get; set; }

		public SqlJobsController(string ConnectionString, bool key)
		{
			if (key)
				this.ConnectionString = ConfigurationManager.ConnectionStrings[ConnectionString].ConnectionString;
			else this.ConnectionString = ConnectionString;
		}

		public Job GetJob(string JobName, out int retCode)
		{
			Job ret = null;
			retCode = 0;
			using (SqlConnection oConnection = new SqlConnection(ConnectionString))
			{
				retCode++;
				ServerConnection serverConnection = new ServerConnection(oConnection);
				retCode++;
				Server oSqlServer = new Server(serverConnection);
				retCode++;
				JobServer oAgent = oSqlServer.JobServer;
				retCode++;
				Job oJob = oAgent.Jobs[JobName];
				retCode++;
			}
			return ret;
		}

		public int StartJob(string JobName)
		{
			int ret = 0;
			try
			{
				Job oJob = GetJob(JobName, out ret);
				ret++;
				if (oJob.IsEnabled && oJob.CurrentRunStatus == JobExecutionStatus.Idle)
				{
					ret++;
					oJob.Start();
					ret++;
				}
			}
			catch
			{
				ret--;
			}
			return ret;
		}

		public int StopJob(string JobName)
		{
			int ret = 0;
			try
			{
				Job oJob = GetJob(JobName, out ret);
				ret++;
				if (oJob.IsEnabled && oJob.CurrentRunStatus == JobExecutionStatus.Idle)
				{
					ret++;
					oJob.Stop();
					ret++;
				}
			}
			catch
			{
				ret--;
			}
			return ret;
		}

		public bool IsStarted(string JobName)
		{
			bool ret = false;
			using (SqlConnection oConnection = new SqlConnection(ConnectionString))
			{
				ServerConnection serverConnection = new ServerConnection(oConnection);
				Server oSqlServer = new Server(serverConnection);
				JobServer oAgent = oSqlServer.JobServer;
				Job oJob = oAgent.Jobs[JobName];
				ret = (oJob.CurrentRunStatus != JobExecutionStatus.Idle);
			}
			return ret;
		}

		public DateTime GetLastRunDate(string JobName)
		{
			DateTime ret = DateTime.Now;
			using (SqlConnection oConnection = new SqlConnection(ConnectionString))
			{
				ServerConnection serverConnection = new ServerConnection(oConnection);
				Server oSqlServer = new Server(serverConnection);
				JobServer oAgent = oSqlServer.JobServer;
				Job oJob = oAgent.Jobs[JobName];
				ret = oJob.LastRunDate;
			}
			return ret;
		}
	}
}
