using System.Text;
using System.Data.SqlClient;
using System;
using UnityEngine;
using DefectDataa;
using Newtonsoft.Json;

// read, update, delete
public class mysqlPractice
{
    // TODO check that you're not pushing your username and password to github
    static readonly string user = "<user>";
    static readonly string pwd = "<pass>!";
    static readonly string connStr = String.Format("Server=tcp:supervisor1.database.windows.net,1433;Initial Catalog=bw-supervisor;Persist Security Info=False;User ID={0};Password={1};MultipleActiveResultSets=False;Encrypt=True;TrustServerCertificate=False;Connection Timeout=30;", user, pwd);

    /// <summary>
    /// Only extracts data from the sql table [data].[Defect_Data]
    /// returns the string to be serialized into a json
    /// todo use try catch for connecting and reading from the sql table
    /// </summary>
    /// <param name="connectionString"></param>
    static void ReadQueryData(string connectionString)
    {
        // provide the sql query command
        string queryString =
            "SELECT * FROM [data].[Defect_Data] WHERE supervisor_id=1 AND value_stream_id=2 AND boat_model_id = 3;";

        if (queryString.Contains("*")) { print("you have selected all columns"); }
        else
        {
            FindNumColumns(queryString);
        }

        // provide a connection
        using (SqlConnection connection = new SqlConnection(
                connectionString))
        {
            // execute the command
            SqlCommand command = new SqlCommand(
                queryString, connection);
            connection.Open();

            // read thru the sql table
            using (SqlDataReader reader = command.ExecuteReader())
            {
                StringBuilder sb = new StringBuilder();
                string queriedRow = String.Empty;
                while (reader.Read())
                {
                    print("num columns in the table: " + reader.VisibleFieldCount);
                    // print($"{reader[0]}, {reader[1]}, {reader[2]}, {reader[3]}, {reader[4]}, {reader[5]}, {reader[6]}, {reader[7]},");
                    for (int i = 0; i < reader.VisibleFieldCount; ++i)
                    {
                        // added space for printing purposes
                        sb.Append(reader[i] + " ");
                    }

                    // string is complete (get rid of leading and trailing whitespaces with trim)
                    queriedRow = sb.ToString().Trim();

                    // clear the string builder for the next iteration
                    sb.Clear();

                    // for printing purposes
                    string[] ar = queriedRow.Split(' ');
                    foreach (var str in ar)
                    {
                        print(str);
                    }

                    // print(String.Join(" ", ar));
                }

                // print("last row: " + queriedRow);
            }

        }
    }

    // make sure we return an int
    private static void FindNumColumns(string queryString)
    {
        // todo counts the number of columns explicitly stated
        int selectInd = queryString.IndexOf("select", StringComparison.OrdinalIgnoreCase);

        int fromInd = queryString.IndexOf("from", StringComparison.OrdinalIgnoreCase);

        // split the substring of the query string from selectInd to fromInd
        // by spaces and the length of that string array will be the number of colums
        // to return

    }

    [RuntimeInitializeOnLoadMethod]
    static void MainMethod()
    {
        // for future, the size will be the number of rows
        DefectData[] d =
        {
            new DefectData(),
            new DefectData()
        };
        // ReadQueryData(connStr);

        // TODO store query data into object and serialize it into a json string
        string jsonData = JsonConvert.SerializeObject(d, Formatting.Indented);
        print("json: " + jsonData);
    }

    static void print(object msg)
    {
        UnityEngine.Debug.Log(msg);
    }
}
