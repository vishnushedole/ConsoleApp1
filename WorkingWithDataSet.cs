using Microsoft.Data.SqlClient;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Security.Cryptography;
using System.Security.Principal;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApp1
{
    internal class WorkingWithDataSet
    {
        static string connectionString = @"Server=(local);database=northwind;integrated security=sspi;trustservercertificate=true;
            multipleactiveresultsets=true";
        internal static void Test1()
        {
            DataSet ds = new DataSet();
            DataTable dtEmp = new DataTable(tableName:"Employees");
            ds.Tables.Add(dtEmp);

            DataColumn dc1 = new DataColumn();
            dc1.ColumnName = "EmpId";
            dc1.DataType = typeof(int);
            dtEmp.Columns.Add(dc1);
            // Primary Key
            dtEmp.PrimaryKey = new DataColumn[] { dtEmp.Columns["EmpId"] };

            dc1 = new DataColumn("EmpName", typeof(string));
            dtEmp.Columns.Add(dc1);

            dtEmp.Columns.Add(columnName: "Salary", type: typeof(double));

            DataRow newRow = dtEmp.NewRow(); // Empty Row object is created
            // fill in the data into the row
            newRow["EmpId"] = 1;
            newRow["Salary"] = 12345.67;
            //add the row to the table
            dtEmp.Rows.Add(newRow);

            //alternate way
            dtEmp.Rows.Add(2, "Sample 2", 9876.54);
            dtEmp.Rows.Add(3, "Sample 3", 9876.54);

            foreach(DataRow row in dtEmp.Rows)
            {
                Console.WriteLine("{0}, {1}, {2}", row[0], row[1], row["Salary"]);
            }
            //dtEmp.Rows.Add(3, "Sample 2", 9876.54);

            var row1 = dtEmp.Rows.Find(2);
            Console.WriteLine("{0}, {1}, {2}", row1[0], row1[1], row1["Salary"]);

            var rows = dtEmp.Select("EmpName LIKE '%2'");
            foreach(DataRow row in rows)
            {
                Console.WriteLine("{0}, {1}, {2}", row[0], row[1], row["Salary"]);
            }
        }
        internal static void Test2()
        {
            DataSet ds = new DataSet();
            var sqlText1 = "SELECT CategoryId, CategoryName, Description FROM Categories;";

            SqlConnection con = new SqlConnection(connectionString);
            con.StateChange += (sender, args) =>
            {
                Console.WriteLine($"State Changed to: {args.CurrentState} from {args.OriginalState}");
            };
            SqlDataAdapter adapter = new SqlDataAdapter(
                 selectCommandText: sqlText1,
                 selectConnection: con);

            if (File.Exists("../../../NorthwindData.xml"))
            {
                ds.ReadXmlSchema("../../../NorthwindDataSet.xsd");
                ds.ReadXml("../../../NorthwindData.xml", XmlReadMode.IgnoreSchema);
                /* XmlReadMode 
                 * - InferSchema    -> checks whether a schema exists, else creates the schema based on the col values 
                 * - IgnoreSchema   -> ignores the schema section, even if it exists 
                 * - ReadSchema     -> reads the schema section, throws error if it does not exist
                 * - Diffgram       -> reads the difference between original and proposed versions 
                 * - Fragment       -> reads a part of the XML file,
                 * - Auto           -> switches between ReadSchema | InferSchema | Diffgram | Fragment 
                */
            }
            else
            {
                adapter.Fill(ds, "Categories");
            }
            var table = ds.Tables["Categories"];
            Console.Write("{0,-17}", table.Columns[0].ColumnName);
            Console.Write("{0, -35}", table.Columns[1].ColumnName);
            Console.Write("{0}", table.Columns[2].ColumnName);
            Console.WriteLine("\n------------------------------------------------------------------------");

            foreach (DataRow row in ds.Tables["Categories"].Rows)
            {
                Console.WriteLine("{0,-15}  {1,-35}  {2}", row[0], row[1], row["Description"]);
            }

            var row1 = ds.Tables["Categories"].Select("CategoryId=1")[0];
            row1.BeginEdit();
            row1[2] = "New value updated to description";
            row1.EndEdit();

            //foreach (DataRow row in ds.Tables["Categories"].Rows)
            //{
            //    Console.WriteLine("{0,-15}  {1,-35}  {2}", row[0], row[1], row["Description"]);
            //}


            //to update the modifications to the DB, we need to invoke the UpdateCommand/InsertCommand/DeleteCommand
            //the relevant command object are not created, we can create those object dynamically 
            // based on the SelectCommand 
            SqlCommandBuilder builder = new SqlCommandBuilder(adapter);
            var cmdText = builder.GetInsertCommand().CommandText;
            Console.WriteLine($"INSERT COMMAND:\n{cmdText}");
            cmdText = builder.GetUpdateCommand().CommandText;
            Console.WriteLine($"UPDATE COMMAND:\n{cmdText}");

            adapter.Update(ds, "Categories");
            //to persist the dataset, we use the WriteXML/ReadXML methods 
            ds.WriteXmlSchema("../../../NorthwindDataSet.xsd");
            ds.WriteXml("../../../NorthwindData.xml", XmlWriteMode.IgnoreSchema);
            //XmlWriteMode - WriteSchema - schema and data are written together 
            //              - IgnoreSchema - only the data is written, the structure is not written 
            //              - Diffgram - writes the difference between the original and the changed versions 

        }
    }
}
