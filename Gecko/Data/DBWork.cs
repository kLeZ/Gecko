using System;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Text;

namespace Gecko.Data
{
	/// <summary>
	/// enumeratore che imposta il tipo di parametro
	/// </summary>
	public enum TypePar
	{
		VarChar = SqlDbType.VarChar,
		NVarChar = SqlDbType.NVarChar,
		Int = SqlDbType.Int,
		Float = SqlDbType.Float,
		Decimal = SqlDbType.Decimal,
		DateTime = SqlDbType.DateTime,
		Bit = SqlDbType.Bit,
		Xml = SqlDbType.Xml
	}

	/// <summary>
	/// enumeratore che imposta
	/// la direzione del paramentro
	/// </summary>
	public enum DirectionPar
	{
		Input = ParameterDirection.Input,
		InputOutput = ParameterDirection.InputOutput,
		Output = ParameterDirection.Output,
		ReturnValue = ParameterDirection.ReturnValue
	}

	/// <summary>
	/// classe la quale fornisce tutto ciò
	/// che riguarda i contenitori dati
	/// e il collegamento con il DB
	/// </summary>
	public class DBWork
	{
		public SqlConnection _Connection;
		public SqlTransaction _Transaction;
		private string _ConnectionString;

		/// </summary>
		/// <param name="ConnectionString">stringa di connessione del DB o chiave del file di configurazione dove è presente la stringa di connessione</param>
		/// <param name="Key">Indica se il valore passato in ConnectionString è una striga di connessione o il nome della chiave del file di configurazione dove è contenuta la stringa di connessione</param>
		public DBWork(string ConnectionString, bool Key)
		{
			if (!Key)
			{
				_ConnectionString = ConnectionString;
			}
			else
			{
				_ConnectionString = ConfigurationManager.ConnectionStrings[ConnectionString].ConnectionString;
			}

			_Connection = new SqlConnection(_ConnectionString);
		}


		/// <summary>
		/// Inizia una nuova transazione nel DB.
		/// </summary>
		/// <seealso cref="CommitTransaction"/>
		/// <seealso cref="RollbackTransaction"/>
		/// <returns>Un object rappresentante una nuova transazione.</returns>
		public SqlTransaction BeginTransaction()
		{
			CheckTransactionState(false);
			_Transaction = this._Connection.BeginTransaction();
			return _Transaction;
		}

		/// <summary>
		/// Inizia una nuova transazione nel DB specificando 
		/// il transaction isolation level.
		/// <seealso cref="CommitTransaction"/>
		/// <seealso cref="RollbackTransaction"/>
		/// </summary>
		/// <param name="isolationLevel">Il transaction isolation level.</param>
		/// <returns>Un  oggetto rappresentante una nuova transazione.</returns>
		public SqlTransaction BeginTransaction(IsolationLevel isolationLevel)
		{
			CheckTransactionState(false);
			_Transaction = _Connection.BeginTransaction(isolationLevel);
			return _Transaction;
		}

		/// <summary>
		/// Commit della transazione corrente nel database.
		/// <seealso cref="BeginTransaction"/>
		/// <seealso cref="RollbackTransaction"/>
		/// </summary>
		public void CommitTransaction()
		{
			CheckTransactionState(true);
			_Transaction.Commit();
			_Transaction = null;
		}


		/// <summary>
		/// Rolls back della transazione corrente nel database.
		/// <seealso cref="BeginTransaction"/>
		/// <seealso cref="CommitTransaction"/>
		/// </summary>
		public void RollbackTransaction()
		{
			CheckTransactionState(true);
			_Transaction.Rollback();
			_Transaction = null;
		}



		/// <summary>
		/// Verifica lo stato della transazione corrente
		/// </summary>
		/// <param name="mustBeOpen">variabile booleana
		/// 1- TRUE Transazione aperta
		/// 2 - FALSE Transazione chiusa</param>
		private void CheckTransactionState(bool mustBeOpen)
		{
			if (mustBeOpen)
			{
				if (null == _Transaction)
					throw new InvalidOperationException("Transazione non aperta.");
			}
			else
			{
				if (null != _Transaction)
					throw new InvalidOperationException("Transazione già aperta.");
			}
		}


		/// <summary>
		/// metodo generic usato per effettuare un ExecuteScalar
		/// </summary>
		/// <typeparam name="T"></typeparam>
		/// <param name="cmd">tipo di dato del valore di output</param>
		/// <param name="WithTransaction">variabile boolena
		/// 1 - TRUE Il metodo ha bisogno di un oggetto <see cref="System.Data.SqlClient.SqlCommand"/></param>
		///     con una connessione ed una transazione aperta 
		/// 2 - FALSE Il metodo ha bisogno solo di un oggetto <see cref="System.Data.SqlClient.SqlCommand"/>
		/// <returns>incrocio dei pali</returns>
		public T CreateCommandScalar<T>(SqlCommand cmd, bool WithTransaction)
		{
			T iNREC = default(T);
			try
			{
				if (!WithTransaction)
					_Connection.Open();

				iNREC = (T)cmd.ExecuteScalar();
			}
			catch (Exception ex)
			{
				throw (ex);
			}
			finally
			{
				if (!WithTransaction)
					Close();
			}
			return iNREC;
		}

		/// <summary>
		/// metodo che esegue un ExecuteNonQuery
		/// </summary>
		/// <param name="cmd">tipo di dato del valore di output</param>
		/// <param name="WithTransaction">variabile boolena
		/// 1 - Il metodo ha bisogno di un oggetto <see cref=" System.Data.SqlClient.SqlCommand"/></param>
		///     con una connessione ed una transazione aperta
		/// 2 - FALSE Il metodo ha bisogno solo di un oggetto <see cref="System.Data.SqlClient.SqlCommand"/>
		/// <returns>numero dei record coinvolti</returns>
		public int CreateCommandNonQuery(SqlCommand cmd, bool WithTransaction)
		{
			int iNREC = 0;
			try
			{
				if (!WithTransaction)
					_Connection.Open();

				iNREC = cmd.ExecuteNonQuery();
			}
			catch (Exception ex)
			{
				throw (ex);
			}
			finally
			{
				if (!WithTransaction)
					Close();
			}
			return iNREC;
		}


		/// <summary>
		/// metodo che crea un oggetto <see cref="System.Data.SqlClient.SqlCommand"/>
		/// </summary>
		/// <param name="sqlText">striga contenente l'SQL del Command</param>
		/// <returns><see cref="System.Data.SqlClient.SqlCommand"/></returns>
		public SqlCommand CreateCommand(string sqlText)
		{
			return CreateCommand(sqlText, false, 0);
		}

		/// <summary>
		/// (overload) metodo che crea un oggetto <see cref="System.Data.SqlClient.SqlCommand"/>
		/// </summary>
		/// <param name="sqlText">striga contenente l'SQL del Command</param>
		/// <param name="TimeOut">valore del timeout della query</param>
		/// <returns><see cref="System.Data.SqlClient.SqlCommand"/></returns>
		public SqlCommand CreateCommand(string sqlText, int TimeOut)
		{
			return CreateCommand(sqlText, false, TimeOut);
		}


		/// <summary>
		/// Crea e ritorna un nuovo <see cref="System.Data.SqlClient.SqlCommand"/>
		/// </summary>
		/// <param name="sqlText">Testo della query.</param>
		/// <param name="procedure">Specifica se il testo della query 
		/// è il nome di una stored procedure.</param>
		/// <param name="TimeOut">variabile int setta la proprietà CommandTimeout</param>
		/// <returns><see cref="System.Data.SqlClient.SqlCommand"/></returns>
		public SqlCommand CreateCommand(string sqlText, bool procedure, int TimeOut)
		{
			SqlCommand cmd = _Connection.CreateCommand();
			cmd.CommandText = sqlText;
			cmd.Transaction = _Transaction;
			cmd.CommandTimeout = TimeOut;
			if (procedure)
				cmd.CommandType = CommandType.StoredProcedure;
			return cmd;
		}


		/// <summary>
		/// Overload Crea e ritorna un nuovo <see cref="System.Data.SqlClient.SqlCommand"/>
		/// </summary>
		/// <param name="sqlText">Testo della query.</param>
		/// <param name="procedure">Specifica se il testo della query 
		/// è il nome di una stored procedure.</param>
		/// e gli passo null non setta la proprietà CommandTimeout</param>
		/// <returns><see cref="System.Data.SqlClient.SqlCommand"/></returns>
		public SqlCommand CreateCommand(string sqlText, bool procedure)
		{
			SqlCommand cmd = _Connection.CreateCommand();
			cmd.CommandText = sqlText;
			cmd.Transaction = _Transaction;
			if (procedure)
				cmd.CommandType = CommandType.StoredProcedure;
			return cmd;
		}


		/// <summary>
		/// metodo che imposta gli oggetti <see cref="System.Data.SqlClient.SqlParameter"/>
		/// </summary>
		/// <typeparam name="T">Tipo di dato per il valore del paramentro (cast runtime)</typeparam>
		/// <param name="cmd"><see cref="System.Data.SqlClient.SqlCommand"/></param>
		/// <param name="paramName">Nome del parametro</param>
		/// <param name="dbType">enumeratore con il tipo di parametro <see cref="DirectionPar"/></param>
		/// <param name="ParDir">enumeratore con la direzione del parametro <see cref="TypePar"/></param>
		/// <param name="value">parametro generico contenente il valore contenuto nel paramentro</param>
		/// <returns><see cref="System.Data.SqlClient.SqlParameter"/></returns>
		public SqlParameter SetParameter<T>(SqlCommand cmd, string paramName, TypePar dbType, DirectionPar ParDir, T value)
		{
			SqlParameter parameter = cmd.CreateParameter();
			parameter.SqlDbType = (SqlDbType)dbType;
			// richiamo il metodo CreateSqlParameterName solo se uso SQL SERVER DB
			// altrimenti è sufficiente il parametro paramName
			parameter.ParameterName = this.CreateSqlParameterName(paramName);
			parameter.Direction = (ParameterDirection)ParDir;
			if (value == null)
				parameter.Value = DBNull.Value;
			else
				parameter.Value = value;
			cmd.Parameters.Add(parameter);
			return parameter;
		}



		/// <summary>
		/// metodo che ristituisce un DataSet <see cref="System.Data.DataSet"/>
		/// </summary>
		/// <param name="cmd"><see cref="System.Data.SqlClient.SqlCommand"/></param>
		/// <returns><see cref="System.Data.DataSet"/></returns>
		public DataSet GetDataSet(SqlCommand cmd)
		{
			DataSet dataSet = new DataSet();
			try
			{
				_Connection.Open();
				cmd.Connection = _Connection;
				SqlDataAdapter oleDA = new SqlDataAdapter(cmd);
				oleDA.Fill(dataSet);
			}
			catch (Exception ex)
			{
				throw (ex);
			}
			finally
			{
				_Connection.Close();
			}
			return dataSet;
		}

		/// <summary>
		/// metodo che ristituisce un DataTable <see cref="System.Data.DataTable"/>
		/// </summary>
		/// <param name="cmd"><see cref="System.Data.SqlClient.SqlCommand"/></param>
		/// <returns><see cref="System.Data.DataTable"/></returns>
		public DataTable GetDataTable(SqlCommand cmd)
		{
			DataTable dt = new DataTable();
			try
			{
				_Connection.Open();
				cmd.Connection = _Connection;
				SqlDataAdapter oleDA = new SqlDataAdapter(cmd);
				oleDA.Fill(dt);
			}
			catch (Exception ex)
			{
				throw (ex);
			}
			finally
			{
				_Connection.Close();
			}
			return dt;
		}



		/// <summary>
		/// Chiude la connessione al DB connection.
		/// </summary>
		public void Close()
		{
			if (null != _Connection)
				_Connection.Close();
		}

		/// <summary>
		/// Chiude la connessione al DB connection
		/// e ritorna indietro con le transazioni pendenti
		/// </summary>
		public void Dispose()
		{
			Close();
			if (null != _Connection)
				_Connection.Dispose();
		}



		/// <summary>
		/// metodo che crea il nome del paramentro
		/// Es. @ID
		/// </summary>
		/// <param name="paramName">Nome del parametro Es. ID</param>
		/// <returns>stringa con il nome del paramentro @ID</returns>
		public string CreateSqlParameterName(string paramName)
		{
			StringBuilder sbParamenterName = new StringBuilder();
			sbParamenterName.Append("@");
			sbParamenterName.Append(paramName);
			return sbParamenterName.ToString();
		}
	}
}
