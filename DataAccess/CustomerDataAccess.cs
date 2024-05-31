using Microsoft.Data.SqlClient;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApp1.DataAccess
{
    public class CustomerDataAccess:BaseDataAccess
    {
        public  List<Customer> GetAllCustomers()
        {
                List<Customer> customers = new List<Customer>();
                string sql = "sp_GetAllCustomers";
                try
                {
                    var reader = ExecuteSql(
                        sqltext: sql,
                        commandType: CommandType.StoredProcedure,
                        parameters: new SqlParameter("@filter", ""));
                    while (reader.Read())
                    {
                        var custObj = new Customer
                        {
                            CustomerId = reader.GetString(0),
                            CompanyName = reader.GetString(1),
                            ContactName = reader.GetString(2),
                            City = reader.GetString(3),
                            Country = reader.GetString(4)
                        };
                        customers.Add(custObj);
                    }
                    if (!reader.IsClosed) reader.Close();
                }
                catch (SqlException sqle)
                {
                    throw;
                }
                catch (Exception e)
                {
                    throw;
                }
                finally
                {
                    CloseConnection();
                }
                return customers;
            }
        public Customer GetCustomerById(int id)
        {
            string sql = "sp_GetCustomerById11";
            var customer = new Customer();
            try
            {
                var reader = ExecuteSql(
                    sqltext: sql,
                    commandType: CommandType.StoredProcedure,
                    parameters: new SqlParameter("@customerId", id));
                while (reader.Read())
                {
                    customer = new Customer
                    {
                        CustomerId = reader.GetString(0),
                        CompanyName = reader.GetString(1),
                        ContactName = reader.GetString(2),
                        City = reader.GetString(3),
                        Country = reader.GetString(4)
                    };
               
                }
                if (!reader.IsClosed) reader.Close();
            }
            catch (SqlException sqle)
            {
                throw;
            }
            catch (Exception e)
            {
                throw;
            }
            finally
            {
                CloseConnection();
            }
            return customer;
        }
        public void Insert(Customer customer)
        {
            string sql = "sp_Insert_Customer";
            
            try
            {
                ExecuteNonQuery(
                   sqltext: sql,
                   commandType: CommandType.StoredProcedure,
                    new SqlParameter("@customerId", customer.CustomerId),
                    new SqlParameter("@company", customer.CompanyName),
                   new SqlParameter("@contact", customer.ContactName),
                    new SqlParameter("@city", customer.City),
                   new SqlParameter("@country", customer.Country));
                
            }
            catch (SqlException sqle)
            {
                throw;
            }
            catch (Exception e)
            {
                throw;
            }
            finally
            {
                CloseConnection();
            }
        }
        public void UpdateCustomer(Customer model)
        {
            string sql = "sp_UpdateCustomer";
            try
            {
                ExecuteNonQuery(
                    sqltext: sql,
                    commandType: CommandType.StoredProcedure,
                        new SqlParameter("@customerId", model.CustomerId),
                        new SqlParameter("@company", model.CompanyName),
                        new SqlParameter("@contact", model.ContactName),
                        new SqlParameter("@city", model.City),
                        new SqlParameter("@country", model.Country));
            }

            catch (SqlException sqle)
            {
                throw;
            }

            catch (Exception e)
            {
                throw;
            }

            finally
            {
                CloseConnection();
            }
        }
    }
}
