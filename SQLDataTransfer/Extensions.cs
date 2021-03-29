using System.Collections.Generic;
using System.Data;
using System.Text;

namespace SQLDataTransfer
{
    static class Extensions
    {
        public static List<string> ToList(this IDataReader dataReader, bool includeHeader, string separator = ";")
        {
            List<string> result = new();
            StringBuilder sb;

            if (includeHeader)
            {
                sb = new StringBuilder();
                for (int i = 0; i < dataReader.FieldCount; i++)
                {
                    sb.Append(dataReader.GetName(i).Trim());

                    if (i < dataReader.FieldCount - 1)
                        sb.Append(separator);
                }
                result.Add(sb.ToString());
            }

            while (dataReader.Read())
            {
                sb = new StringBuilder();
                for (int i = 0; i < dataReader.FieldCount; i++)
                {
                    if (!dataReader.IsDBNull(i))
                        sb.Append(dataReader.GetValue(i).ToString().Trim());
                    else
                        sb.Append("");

                    if (i < dataReader.FieldCount - 1)
                        sb.Append(separator);
                }
                result.Add(sb.ToString());
            }

            return result;
        }
    }
}
