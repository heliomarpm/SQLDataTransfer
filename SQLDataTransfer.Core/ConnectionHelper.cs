using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;

namespace SQLDataTransfer.Core
{
    public class ConnectionHelper
    {
        public SqlConnection Connection { get; internal set; }

        public string ConnectionString => Connection == null ? String.Empty : Connection.ConnectionString;

        public bool Disconnect()
        {
            if (Connection != null && Connection.State == ConnectionState.Open)
            {
                Connection.Close();
                return true;
            }
            else
                return false;

        }

        #region [ ExecuteDataTable ]
        /// <summary>
        /// Método que executa um processo e retorna um DataTable;
        /// </summary>
        /// <param name="commandText">Comando</param>
        /// <returns>Resultado</returns>
        public DataTable ExecuteDataTable(string commandText)
        {
            return ExecuteDataTable(commandText, CommandType.Text);
        }

        public DataTable ExecuteDataTable(string commandText, int commandTimeout)
        {
            return ExecuteDataTable(commandText, CommandType.Text, commandTimeout);
        }

        /// <summary>
        /// Método que executa um processo e retorna um DataTable;
        /// </summary>
        /// <param name="commandText">Comando</param>
        /// <param name="commandType">Tipo</param>       
        /// <returns>Resultado</returns>
        public DataTable ExecuteDataTable(string commandText, CommandType commandType)
        {
            return ExecuteDataTable(commandText, commandType, null, -1);
        }

        public DataTable ExecuteDataTable(string commandText, CommandType commandType, int commandTimeout)
        {
            return ExecuteDataTable(commandText, commandType, null, commandTimeout);
        }

        /// <summary>
        /// Método que executa um processo e retorna um DataTable;
        /// </summary>
        /// <param name="commandText">Comando</param>
        /// <param name="commandType">Tipo</param>       
        /// <param name="parameters">Parâmetros</param>
        /// <returns>Resultado</returns>
        public DataTable ExecuteDataTable(string commandText, CommandType commandType, List<IDataParameter> parameters)
        {
            return ExecuteDataTable(commandText, commandType, parameters, -1);
        }

        public DataTable ExecuteDataTable(string commandText, CommandType commandType, List<IDataParameter> parameters, int commandTimeout)
        {
            DataTable dt = new DataTable();
            using (var cmd = Connection.CreateCommand())
            {
                if (commandTimeout > -1)
                    cmd.CommandTimeout = commandTimeout;
                else
                    cmd.CommandTimeout = Connection.ConnectionTimeout;

                cmd.CommandText = commandText;
                cmd.CommandType = commandType;

                if (parameters != null && parameters.Count > 0)
                    cmd.Parameters.AddRange(parameters.ToArray());

                using (var da = new SqlDataAdapter(cmd))
                {
                    da.Fill(dt);
                }
            }
            return dt;
        }

        #endregion

        #region [ ExecuteDataset ]
        /// <summary>
        /// Método que executa um processo e retorna um DataSet;
        /// </summary>
        /// <param name="commandText">Comando</param>
        /// <returns>Resultado</returns>
        public DataSet ExecuteDataset(string commandText)
        {
            return ExecuteDataset(commandText, CommandType.Text);
        }

        public DataSet ExecuteDataset(string commandText, int commandTimeout)
        {
            return ExecuteDataset(commandText, CommandType.Text, commandTimeout);
        }

        /// <summary>
        /// Método que executa um processo e retorna um DataSet;
        /// </summary>
        /// <param name="commandText">Comando</param>
        /// <param name="commandType">Tipo</param>       
        /// <returns>Resultado</returns>
        public DataSet ExecuteDataset(string commandText, CommandType commandType)
        {
            return ExecuteDataset(commandText, commandType, null, -1);
        }

        public DataSet ExecuteDataset(string commandText, CommandType commandType, int commandTimeout)
        {
            return ExecuteDataset(commandText, commandType, null, commandTimeout);
        }

        /// <summary>
        /// Método que executa um processo e retorna um DataSet;
        /// </summary>
        /// <param name="commandText">Comando</param>
        /// <param name="commandType">Tipo</param>       
        /// <param name="parameters">Parâmetros</param>
        /// <returns>Resultado</returns>
        public DataSet ExecuteDataset(string commandText, CommandType commandType, List<IDataParameter> parameters)
        {
            return ExecuteDataset(commandText, commandType, parameters, -1);
        }
        public DataSet ExecuteDataset(string commandText, CommandType commandType, List<IDataParameter> parameters, int commandTimeout)
        {
            DataSet ds = new DataSet();
            using (var cmd = Connection.CreateCommand())
            {
                if (commandTimeout > -1)
                    cmd.CommandTimeout = commandTimeout;
                else
                    cmd.CommandTimeout = Connection.ConnectionTimeout;

                cmd.CommandText = commandText;
                cmd.CommandType = commandType;

                if (parameters != null && parameters.Count > 0)
                    cmd.Parameters.AddRange(parameters.ToArray());

                using (var da = new SqlDataAdapter(cmd))
                {
                    da.Fill(ds);
                }
            }
            return ds;
        }

        #endregion

        #region [ ExecuteReader ]
        /// <summary>
        /// Método que executa um processo e retorna um DataReader;
        /// </summary>
        /// <param name="commandText">Comando</param>       
        /// <returns>Resultado</returns>
        public IDataReader ExecuteReader(string commandText)
        {
            return ExecuteReader(commandText, CommandType.Text);
        }

        public IDataReader ExecuteReader(string commandText, int commandTimeout)
        {
            return ExecuteReader(commandText, CommandType.Text, commandTimeout);
        }

        /// <summary>
        /// Método que executa um processo e retorna um DataReader;
        /// </summary>
        /// <param name="commandText">Comando</param>       
        /// <param name="commandType">Tipo</param>
        /// <returns>Resultado</returns>
        public IDataReader ExecuteReader(string commandText, CommandType commandType)
        {
            return ExecuteReader(commandText, commandType, null, -1);
        }

        public IDataReader ExecuteReader(string commandText, CommandType commandType, int commandTimeout)
        {
            return ExecuteReader(commandText, commandType, null, commandTimeout);
        }

        /// <summary>
        /// Método que executa um processo e retorna um DataReader;
        /// </summary>
        /// <param name="commandText">Comando</param>
        /// <param name="commandType">Tipo</param>
        /// <param name="parameters">Parâmetros</param>
        /// <returns>Resultado</returns>
        public IDataReader ExecuteReader(string commandText, CommandType commandType, List<IDataParameter> parameters)
        {
            return ExecuteReader(commandText, commandType, parameters, -1);
        }

        public IDataReader ExecuteReader(string commandText, CommandType commandType, List<IDataParameter> parameters, int commandTimeout)
        {
            using (var cmd = Connection.CreateCommand())
            {
                if (commandTimeout > -1)
                    cmd.CommandTimeout = commandTimeout;
                else
                    cmd.CommandTimeout = Connection.ConnectionTimeout;

                cmd.CommandText = commandText;
                cmd.CommandType = commandType;

                if (parameters != null && parameters.Count > 0)
                    cmd.Parameters.AddRange(parameters.ToArray());

                return cmd.ExecuteReader();
            }

        }

        #endregion

        #region [ ExecuteDataRow ]
        /// <summary>
        /// Método que executa um processo e retorna um DataRow;
        /// </summary>
        /// <param name="commandText">Comando</param>
        /// <returns>Resultado</returns>
        public DataRow ExecuteDataRow(string commandText)
        {
            return ExecuteDataRow(commandText, CommandType.Text);
        }

        public DataRow ExecuteDataRow(string commandText, int commandTimeout)
        {
            return ExecuteDataRow(commandText, CommandType.Text, commandTimeout);
        }

        /// <summary>
        /// Método que executa um processo e retorna um DataRow;
        /// </summary>
        /// <param name="commandText">Comando</param>
        /// <param name="commandType">Tipo</param>       
        /// <returns>Resultado</returns>
        public DataRow ExecuteDataRow(string commandText, CommandType commandType)
        {
            return ExecuteDataRow(commandText, commandType, null, -1);
        }

        public DataRow ExecuteDataRow(string commandText, CommandType commandType, int commandTimeout)
        {
            return ExecuteDataRow(commandText, commandType, null, commandTimeout);
        }

        /// <summary>
        /// Método que executa um processo e retorna um DataRow;
        /// </summary>
        /// <param name="commandText">Comando</param>
        /// <param name="commandType">Tipo</param>       
        /// <param name="parameters">Parâmetros</param>
        /// <returns>Resultado</returns>
        public DataRow ExecuteDataRow(string commandText, CommandType commandType, List<IDataParameter> parameters)
        {
            return ExecuteDataRow(commandText, commandType, parameters, -1);
        }

        public DataRow ExecuteDataRow(string commandText, CommandType commandType, List<IDataParameter> parameters, int commandTimeout)
        {
            DataRow dr = null;

            using (DataSet ds = ExecuteDataset(commandText, commandType, parameters, commandTimeout))
            {
                if (ds.Tables[0].Rows.Count > 0)
                    dr = ds.Tables[0].Rows[0];
            }
            return dr;
        }

        #endregion

        #region [ ExecuteNonQuery ]
        /// <summary>
        /// Método que executa um processo;
        /// </summary>
        /// <param name="commandText">Comando</param>
        /// <returns>Resultado</returns>
        public int ExecuteNonQuery(string commandText)
        {
            return ExecuteNonQuery(commandText, CommandType.Text);
        }
        public int ExecuteNonQuery(string commandText, int commandTimeout)
        {
            return ExecuteNonQuery(commandText, CommandType.Text, commandTimeout);
        }

        /// <summary>
        /// Método que executa um processo;
        /// </summary>
        /// <param name="commandText">Comando</param>
        /// <param name="commandType">Tipo</param>
        /// <returns>Resultado</returns>
        public int ExecuteNonQuery(string commandText, CommandType commandType)
        {
            return ExecuteNonQuery(commandText, commandType, null, -1);
        }
        public int ExecuteNonQuery(string commandText, CommandType commandType, int commandTimeout)
        {
            return ExecuteNonQuery(commandText, commandType, null, commandTimeout);
        }

        /// <summary>
        /// Método que executa um processo;
        /// </summary>
        /// <param name="commandText">Comando</param>
        /// <param name="commandType">Tipo</param>
        /// <param name="parameters">Parâmetros</param>
        /// <returns>Resultado</returns>
        public int ExecuteNonQuery(string commandText, CommandType commandType, List<IDataParameter> parameters)
        {
            return ExecuteNonQuery(commandText, commandType, parameters, -1);
        }

        public int ExecuteNonQuery(string commandText, CommandType commandType, List<IDataParameter> parameters, int commandTimeout)
        {
            using (var cmd = Connection.CreateCommand())
            {
                if (commandTimeout > -1)
                    cmd.CommandTimeout = commandTimeout;
                else
                    cmd.CommandTimeout = Connection.ConnectionTimeout;

                cmd.CommandText = commandText;
                cmd.CommandType = commandType;

                if (parameters != null && parameters.Count > 0)
                    cmd.Parameters.AddRange(parameters.ToArray());

                return cmd.ExecuteNonQuery();
            }

        }

        #endregion

        #region [ ExecuteScalar ]
        /// <summary>
        /// Método que executa um processo;
        /// </summary>
        /// <param name="commandType">Tipo</param>
        /// <returns>Resultado</returns>       
        public object ExecuteScalar(string commandText)
        {
            return ExecuteScalar(commandText, CommandType.Text);
        }

        public object ExecuteScalar(string commandText, int commandTimeout)
        {
            return ExecuteScalar(commandText, CommandType.Text, commandTimeout);
        }

        /// <summary>
        /// Método que executa um processo;
        /// </summary>
        /// <param name="commandType">Tipo</param>
        /// <param name="commandText">Comando</param>
        /// <returns>Resultado</returns>       
        public object ExecuteScalar(string commandText, CommandType commandType)
        {
            return ExecuteScalar(commandText, commandType, null);
        }

        public object ExecuteScalar(string commandText, CommandType commandType, int commandTimeout)
        {
            return ExecuteScalar(commandText, commandType, null, commandTimeout);
        }

        /// <summary>
        /// Método que executa um processo;
        /// </summary>
        /// <param name="commandType">Tipo</param>
        /// <param name="commandText">Comando</param>
        /// <param name="parameters">Parâmetros</param>
        /// <returns>Resultado</returns>       
        public object ExecuteScalar(string commandText, CommandType commandType, List<IDataParameter> parameters)
        {
            return ExecuteScalar(commandText, commandType, parameters, -1);
        }
        public object ExecuteScalar(string commandText, CommandType commandType, List<IDataParameter> parameters, int commandTimeout)
        {
            using (var cmd = Connection.CreateCommand())
            {
                if (commandTimeout > -1)
                    cmd.CommandTimeout = commandTimeout;
                else
                    cmd.CommandTimeout = Connection.ConnectionTimeout;

                cmd.CommandText = commandText;
                cmd.CommandType = commandType;

                if (parameters != null && parameters.Count > 0)
                    cmd.Parameters.AddRange(parameters.ToArray());

                return cmd.ExecuteScalar();
            }

        }
        #endregion
    }
}