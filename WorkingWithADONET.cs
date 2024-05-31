using Azure.Core;
using Microsoft.Data.SqlClient;
using System;
using System.Data;
using System.Text;
using System.Transactions;

namespace ConsoleApp1
{
    internal class WorkingWithADONET
    {
        internal static void Test()
        {
            // define the connection string
            var connStr = @"Server=tcp:vishnudbserver.database.windows.net,1433;Initial Catalog=vishnushedole;Persist Security Info=False;User ID=vishnudbadmin;Password=Vishnu#123;MultipleActiveResultSets=False;Encrypt=True;TrustServerCertificate=False;Connection Timeout=30;";
            var sqlText = "SELECT id, first_name, last_name, email from employee";

            SqlConnection connection = new SqlConnection();
            connection.ConnectionString = connStr;
            connection.Open();

            var command = new SqlCommand();
            command.CommandText = sqlText;
            command.CommandType = CommandType.Text;
            command.Connection = connection;

            var reader = command.ExecuteReader();
            while (reader.Read())
            {
                Console.WriteLine("ID: {0}, first_name: {1}\n\tlast_name: {2}\n\temail: {3}", reader.GetInt32(0), reader["first_name"].ToString(),
                    reader[2].ToString(),reader.GetString(3));

            }
            reader.Close();
            connection.Close();
        }
        internal static void Test2()
        {

            // define the connection string
            var connStr = @"Server=(local);database=northwind;integrated security=sspi;trustservercertificate=true";
            var sqlText = "SELECT CategoryId,CategoryName,Description from Categories;"
                + "SELECT ProductId,ProductName,UnitPrice,UnitsInStock,Discontinued from Products;";

            SqlConnection connection = new SqlConnection();
            connection.ConnectionString = connStr;
            connection.Open();

            var command = new SqlCommand();
            command.CommandText = sqlText;
            command.CommandType = CommandType.Text;
            command.Connection = connection;

            var reader = command.ExecuteReader();
            while (reader.Read())
            {
                Console.WriteLine("Id: {0}, Name:{1}\n\tDescription: {2}",
                    reader[0].ToString(), reader[1].ToString(), reader[2].ToString());
            }
            reader.NextResult();
            while(reader.Read())
            {
                Console.WriteLine("Id: {0}, Name: {1}\n\tPrice: {2}, Stock: {3}, In Stock? : {4}",
                    reader.GetInt32(0), reader["ProductName"].ToString(), reader.GetDecimal(2), reader.GetInt16(3), reader.GetBoolean(4));
            }
            reader.Close();
            connection.Close();

        }
        internal static void Test3()
        {

            // define the connection string
            var connStr = @"Server=(local);database=northwind;integrated security=sspi;trustservercertificate=true;
            multipleactiveresultsets=true";
            var sqlText1 = "SELECT CategoryId,CategoryName,Description from Categories;";
           

            SqlConnection connection = new SqlConnection();
            connection.ConnectionString = connStr;
            connection.Open();

            var command1 = new SqlCommand();
            command1.CommandText = sqlText1;
            command1.CommandType = CommandType.Text;
            command1.Connection = connection;

            var reader1 = command1.ExecuteReader();

            StringBuilder sb = new StringBuilder();
            sb.AppendLine("".PadLeft(50, '-'))
                .AppendFormat("{0,-5}{1,40}{2}\n", "Id", "Name", "Description")
                .AppendLine("".PadLeft(50, '-'));
            while (reader1.Read())
            {
                sb.AppendFormat("{0,-5}{1,40}{2}\n", reader1.GetInt32(0), reader1.GetString(1), reader1.GetString(2));
            var sqlText2 = "SELECT ProductId,ProductName,UnitPrice,UnitsInStock,Discontinued from Products " +
               "WHERE CategoryId =" + reader1["CategoryId"].ToString()+";";

            var command2 = new SqlCommand();
            command2.CommandText = sqlText2;
            command2.CommandType = CommandType.Text;
            command2.Connection = connection;


            var reader2 = command2.ExecuteReader();
                sb.AppendFormat("{0,-5}{1,-40}{2,-10}{3,-10},{4}\n", "Id", "Name", "Price", "Stock", "Discontinued")
                    .AppendLine("".PadLeft(50, '-'));
                while (reader2.Read())
                {
                    sb.AppendFormat("{0,-5}", reader2.GetInt32(0))
                        .AppendFormat("{0,-40}", reader2.GetString(1))
                        .AppendFormat("{0,-10}", reader2.GetDecimal(2))
                        .AppendFormat("{0,-10}", reader2.GetInt16(3))
                        .AppendFormat("{0}", reader2.GetBoolean(4))
                        .Append("\n");
                }
                if (!reader2.IsClosed)
                    reader2.Close();
                sb.AppendLine("\n".PadLeft(80, '='));
            }
            Console.WriteLine(sb.ToString());
            //while (reader.Read())
            //{
            //    Console.WriteLine("Id: {0}, Name:{1}\n\tDescription: {2}",
            //        reader[0].ToString(), reader[1].ToString(), reader[2].ToString());
            //}
            //reader.NextResult();
            //while (reader.Read())
            //{
            //    Console.WriteLine("Id: {0}, Name: {1}\n\tPrice: {2}, Stock: {3}, In Stock? : {4}",
            //        reader.GetInt32(0), reader["ProductName"].ToString(), reader.GetDecimal(2), reader.GetInt16(3), reader.GetBoolean(4));
            //}
            if(!reader1.IsClosed)
            reader1.Close();

            if(connection != null)
            connection.Close();

        }
        internal static void Test4()
        {
            // define the connection string
            var connStr = @"Server=(local);database=northwind;integrated security=sspi;trustservercertificate=true;
            multipleactiveresultsets=true";
            Console.Clear();
            Console.WriteLine("Enter Category Id :");
            string id = Console.ReadLine();


            var sqlText1 = "SELECT CategoryId,CategoryName,Description from Categories Where CategoryId = @categoryId;";


            SqlConnection connection = new SqlConnection();
            connection.ConnectionString = connStr;
            connection.Open();

            var command1 = new SqlCommand();
            command1.CommandText = sqlText1;
            command1.CommandType = CommandType.Text;
            command1.Connection = connection;

            SqlParameter p1 = new SqlParameter();
            p1.ParameterName = "@categoryId";
            p1.SqlDbType = SqlDbType.Int;
            p1.Size = 4;
            p1.Direction = ParameterDirection.Input;
            p1.Value = id;
            command1.Parameters.Add(p1);

            var reader1 = command1.ExecuteReader();

            StringBuilder sb = new StringBuilder();
            sb.AppendLine("".PadLeft(80, '-'))
                .AppendFormat("{0,-5}{1,-40}{2}\n", "Id", "Name", "Description")
                .AppendLine("".PadLeft(80, '-'));
            while(reader1.Read())
            {
                sb.AppendFormat("{0,-5}{1,-40}{2}\n", reader1.GetInt32(0), reader1.GetString(1), reader1.GetString(2));
            }    
            Console.WriteLine(sb.ToString());

            if(!reader1.IsClosed)
                reader1.Close();

            if( connection != null ) connection.Close();

        }
        static string connStr = @"Server=(local);database=northwind;integrated security=sspi;trustservercertificate=true;
            multipleactiveresultsets=true";
        internal static void Test5()
        {
            var sqlText1 = "Insert into Customers (CustomerId,CompanyName,ContactName,City,Country)" +
                " Values (@custId,@company,@contact,@city,@country)";
            string id, company, contact, city, country;
            Console.WriteLine("Enter ID:");
            id = Console.ReadLine();
            Console.WriteLine("Enter Company:");
            company = Console.ReadLine();
            Console.WriteLine("Enter Contact");
            contact = Console.ReadLine();
            Console.WriteLine("Enter City:");
            city = Console.ReadLine();
            Console.WriteLine("Enter Country:");
            country = Console.ReadLine();

            SqlConnection connection = new SqlConnection();
            connection.ConnectionString = connStr;
            connection.Open();

            var command1 = new SqlCommand();
            command1.CommandText = sqlText1;
            command1.CommandType = CommandType.Text;
            command1.Connection = connection;

            SqlParameter p1 = new SqlParameter();
            p1.ParameterName = "@custId";
            p1.Size = 5;
            p1.SqlDbType = SqlDbType.VarChar;
            p1.Value = id;
            command1.Parameters.Add(p1);

            SqlParameter p2 = new SqlParameter("@company",SqlDbType.VarChar,100);
            p2.Value = company;
            command1.Parameters.Add(p2);

            SqlParameter p3 = new SqlParameter("@contact", SqlDbType.VarChar, 50);
            p3.Value = contact;
            command1.Parameters.Add(p3);

            SqlParameter p4 = new SqlParameter("@city", SqlDbType.VarChar, 50);
            p4.Value = company;
            command1.Parameters.Add(p4);

            SqlParameter p5 = new SqlParameter("@country", SqlDbType.VarChar, 500);
            p5.Value = country;
            command1.Parameters.Add(p5);

            try
            {
                command1.ExecuteNonQuery();
                Console.WriteLine("Row inserted into table");

            }
            catch(SqlException sqlex)
            {
                foreach (SqlError error in sqlex.Errors)
                { Console.WriteLine(error.Message); }

            }
            catch(Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
            finally
            {
                connection.Close();
            }
        }

        internal static void Test6()
        {
            var sqlText1 = "sp_Insert_Customer";
            string id, company, contact, city, country;
            Console.WriteLine("Enter ID:");
            id = Console.ReadLine();
            Console.WriteLine("Enter Company:");
            company = Console.ReadLine();
            Console.WriteLine("Enter Contact");
            contact = Console.ReadLine();
            Console.WriteLine("Enter City:");
            city = Console.ReadLine();
            Console.WriteLine("Enter Country:");
            country = Console.ReadLine();

            SqlConnection connection = new SqlConnection();
            connection.ConnectionString = connStr;
            connection.Open();

            var command1 = new SqlCommand();
            command1.CommandText = sqlText1;
            command1.CommandType = CommandType.StoredProcedure; // CHANGE
            command1.Connection = connection;

            command1.Parameters.AddWithValue("@customerId", id);
            command1.Parameters.AddWithValue("@company", company);
            command1.Parameters.AddWithValue("@contact", contact);
            command1.Parameters.AddWithValue("@city", city);
            command1.Parameters.AddWithValue("@country", country);


            try
            {
                command1.ExecuteNonQuery();
                Console.WriteLine("Row inserted into table");

            }
            catch (SqlException sqlex)
            {
                foreach (SqlError error in sqlex.Errors)
                { Console.WriteLine(error.Message); }

            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
            finally
            {
                connection.Close();
            }
        }
        internal static void Test7()
        {
            var sqlText1 = "sp_GetCustomerById";
            string id;
            Console.WriteLine("Enter ID:");
            id = Console.ReadLine();
            
            SqlConnection connection = new SqlConnection();
            connection.ConnectionString = connStr;
            connection.Open();

            var command1 = new SqlCommand();
            command1.CommandText = sqlText1;
            command1.CommandType = CommandType.StoredProcedure; // CHANGE
            command1.Connection = connection;

            command1.Parameters.AddWithValue("@customerId", id);

            try
            {
                var Reader1 = command1.ExecuteReader();
                Console.WriteLine("Query executed.");

            }
            catch (SqlException sqlex)
            {
                foreach (SqlError error in sqlex.Errors)
                { Console.WriteLine(error.Message); }

            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
            finally
            {
                connection.Close();
            }
        }
        //internal static void Test8()
        //{
        //    SqlConnection connection = new SqlConnection(connStr);
        //    connection.Open();
        //    var trans = connection.BeginTransaction(IsolationLevel.Serializable);
        //    var sql1 = "INSERT INTO Customers(CustomerId,CompanyName,ContactName,City,Country) " +
        //        " VALUES ('VWXYZ1','transacted company','transact contact','transact','transact');";
        //    var sql2 = "DELETE FROM Customers WHERE CustomerId = '1';";

        //    var cmd1 = new SqlCommand(sql1,connection,trans);
        //    var cmd2 = new SqlCommand(sql2,connection,trans);

        //    try
        //    {
        //        cmd1.ExecuteNonQuery();
        //        cmd2.ExecuteNonQuery();
        //        trans.Commit();
        //    }catch(Exception ex)
        //    {
        //        trans.Rollback();
        //        Console.WriteLine("Transaction failed rolling back");
        //    }
        //    finally
        //    {
        //        connection.Close();
        //    }
        //}
        internal static void Test9()
        {
            // For Distributed transactions, use the System.Transactions namespace

            //Scope - Required - if there is an existing transaction, take part in it, if not create a new transaction
            //      - RequiresNew - always create a new transaction
            //      - Suppressed - do not take part in any transaction, even if it exists

            TransactionScopeOption options = TransactionScopeOption.RequiresNew;
            TransactionOptions transactionOptions = new TransactionOptions();
            transactionOptions.IsolationLevel = System.Transactions.IsolationLevel.Serializable;
            TransactionManager.ImplicitDistributedTransactions = true;

            // This scope objects enlists the connections into the DTC - MSDTC
            using(System.Transactions.TransactionScope scope 
                = new System.Transactions.TransactionScope(options,transactionOptions))
            {
                SqlConnection conn1 = new SqlConnection(connStr);
                SqlConnection conn2 = new SqlConnection(connStr);

                var sql1 = "INSERT INTO Customers(CustomerId,CompanyName,ContactName,City,Country)" +
                    " VALUES('54321','transacted company','transact contact','transact','transact');";
                var sql2 = "DELETE FROM Customers WHERE CustomerId='ALFKI';"; // Foreign Key constraint

                var cmd1 = new SqlCommand(sql1, conn1);
                var cmd2 = new SqlCommand (sql2, conn2);
                conn1.Open();
                conn2.Open();

                cmd1.ExecuteNonQuery();
                cmd2.ExecuteNonQuery();

                scope.Complete(); // Either it gets committed or rolled back
                conn1.Close();
                conn2.Close();
            }
        }
    }
}
