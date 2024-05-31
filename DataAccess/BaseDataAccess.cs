﻿using Microsoft.Data.SqlClient;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApp1.DataAccess
{
    public class BaseDataAccess
    {
        protected SqlConnection connection;
        private string connectionString = @"Server=(local);database=northwind;integrated security=sspi;trustservercertificate=true;
            multipleactiveresultsets=true";
        protected void OpenConnection()
        {
            if(connection is null)
            {
                connection = new SqlConnection(connectionString);
            }
            if(connection.State==ConnectionState.Closed)
                connection.Open();

        }
        protected void CloseConnection()
        {
            if(connection is not null)
               if(connection.State != System.Data.ConnectionState.Closed)
                    connection.Close();
        }
        public SqlDataReader ExecuteSql(string sqltext,CommandType commandType,params SqlParameter[] parameters)
        {
            OpenConnection();
            var command = connection.CreateCommand();
            command.CommandType = commandType;
            command.CommandText = sqltext;
            if(parameters.Length > 0)
            {
                command.Parameters.AddRange(parameters);
            }
            return command.ExecuteReader();
        }
        public void ExecuteNonQuery(string sqltext,CommandType commandType,params SqlParameter[] parameters)
        {
            OpenConnection();
            var command = connection.CreateCommand();
            command.CommandType = commandType;
            command.CommandText = sqltext;
            if(parameters.Length > 0 )
            {
                command.Parameters.AddRange(parameters);
            }
            command.ExecuteNonQuery();
            return;
        }
    }
}
