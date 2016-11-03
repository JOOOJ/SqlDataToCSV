using System.Collections.Generic;
using System.Data;
using System.IO;

namespace SqlDataToCSV
{
    public class DataTableToCSV
    {
        public void Write(DataTable table,string csvFilePath)
        {
            using (StreamWriter writer = new StreamWriter(csvFilePath))
            {
                List<string> sb = new List<string>();
                foreach (DataColumn col in table.Columns)
                {
                    string value = PrepareValue(col.ColumnName);
                    sb.Add(value);
                }
                string line = string.Join(",", sb);
                writer.WriteLine(line);
            }

            using (StreamWriter writer = new StreamWriter(csvFilePath, true))
            {
                List<string> sb = new List<string>(table.Columns.Count);
                foreach (DataRow row in table.Rows)
                {
                    sb.Clear();
                    foreach (DataColumn col in table.Columns)
                    {
                        string value = PrepareValue(row[col].ToString());
                        sb.Add(value);
                    }
                    string line = string.Join(",", sb);
                    writer.WriteLine(line);
                }

            }
        }

        private string PrepareValue(string value)
        {
            string result = value.Replace("\"", "\"\"");
            result = "\"" + result + "\"";
            return result;

        }
    }
}
