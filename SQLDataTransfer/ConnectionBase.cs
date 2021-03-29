using System;
using System.Data;
using System.Data.SqlClient;

namespace SQLDataTransfer
{
    public class ConnectionBase
    {
        #region [ Conexao ]
        protected static ConnectionHelper ConnectionHelper { get; private set; }

        protected ConnectionHelper Connect(string pConnectionString)
        {
            SqlConnection connection;

            connection = new SqlConnection(pConnectionString);
            connection.Open();

            ConnectionHelper = new ConnectionHelper
            {
                Connection = connection
            };
            return ConnectionHelper;
        }

        #endregion

        /// <summary>
        /// Método que trata o valor que retornou do banco de dados;
        /// </summary>
        /// <typeparam name="T">Tipo do campo</typeparam>
        /// <param name="reader">Ponteiro do registro</param>
        /// <param name="fieldName">Nome do campo</param>
        /// <returns>Valor do campo</returns>
        public static T GetValue<T>(IDataReader reader, string fieldName)
        {
            if (reader.IsDBNull(reader.GetOrdinal(fieldName)).Equals(true))
                return default;
            else
                return (T)Convert.ChangeType(reader.GetValue(reader.GetOrdinal(fieldName)), typeof(T));
        }
    }
}
