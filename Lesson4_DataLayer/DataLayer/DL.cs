using Lesson4_DataLayer.Models;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lesson4_DataLayer.DataLayer
{
    internal class DL
    {
        public static string ConnectionString { get; private set; } = ConfigurationManager.ConnectionStrings["Company_db"].ConnectionString;
        static SqlConnection conn;

        public static class Customer
        {
            public static CustomerModel ByID(int customerId)
            {
                using (SqlConnection conn = new SqlConnection(ConnectionString))
                {
                    conn.Open();
                    SqlCommand getCustomer = new SqlCommand("stp_CustomerByID", conn);
                    getCustomer.Parameters.AddWithValue("@customerID", customerId);
                    getCustomer.CommandType = CommandType.StoredProcedure;
                    SqlDataReader reader = getCustomer.ExecuteReader();
                    CustomerModel customer = null;
                    while (reader.Read())
                    {
                        int ID = (int)reader[0];
                        string FirstName = reader[1].ToString();
                        string Lastname = reader[2].ToString();
                        DateTime birthDate = DateTime.Parse(reader[3].ToString());
                        customer = new CustomerModel(ID, FirstName, Lastname, birthDate);
                    }
                    reader.Close();
                    return customer;
                }
            }

            public static List<CustomerModel> All()
            {
                List<CustomerModel> customers = new List<CustomerModel>();

                using (SqlConnection conn = new SqlConnection(ConnectionString))
                {
                    conn.Open();
                    string custAll = "[dbo].[stp_CustomerAll]";
                    SqlCommand cmd = new SqlCommand(custAll, conn);
                    cmd.CommandType = CommandType.StoredProcedure;

                    using (SqlDataReader dr = cmd.ExecuteReader())
                    {
                        while (dr.Read())
                        {
                            int id = (int)dr[0];
                            string firstName = dr[1].ToString();
                            string lastName = dr[2].ToString();
                            DateTime birthDay = DateTime.Parse(dr[3].ToString());

                            customers.Add(new CustomerModel(id, firstName, lastName, birthDay));
                        }
                    }
                }

                return customers;
            }

            public static int Insert(CustomerModel tmp)
            {
                using (SqlConnection conn = new SqlConnection(ConnectionString))
                {
                    conn.Open();
                    string cust_add = "dbo.stp_CustomerAdd";
                    SqlCommand cmd = new SqlCommand(cust_add, conn);
                    cmd.CommandType = CommandType.StoredProcedure;
                    SqlCommandBuilder.DeriveParameters(cmd);

                    cmd.Parameters[4].Value = DBNull.Value;
                    cmd.Parameters[1].Value = tmp.FirstName;
                    cmd.Parameters[2].Value = tmp.LastName;
                    cmd.Parameters[3].Value = tmp.BirthDate;

                    cmd.ExecuteNonQuery();

                    int new_id = (int)cmd.Parameters[4].Value;
                    return new_id;
                }
            }

            public static int Delete(int customerId) // Был вариант сделать процедуру удаления типа BOOL, но я решил возвращать количество удаленных записей
            {
                using (SqlConnection conn = new SqlConnection(ConnectionString))
                {
                    conn.Open();
                    string custDelete = "[dbo].[stp_CustomerDelete]";
                    SqlCommand cmd = new SqlCommand(custDelete, conn);
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@customerID", customerId);

                    int rowsAffected = cmd.ExecuteNonQuery(); // Здесь я указываю количество удаленнх строк
                    return rowsAffected;
                }
            }

            public static int Update(CustomerModel customer)
            {
                using (SqlConnection conn = new SqlConnection(ConnectionString))
                {
                    conn.Open();
                    string custUpdate = "dbo.stp_CustomerUpdate";
                    SqlCommand cmd = new SqlCommand(custUpdate, conn);
                    cmd.CommandType = CommandType.StoredProcedure;

                    cmd.Parameters.AddWithValue("@id", customer.ID);
                    cmd.Parameters.AddWithValue("@FirstName", customer.FirstName);
                    cmd.Parameters.AddWithValue("@LastName", customer.LastName);
                    cmd.Parameters.AddWithValue("@DateOfBirth", customer.BirthDate);

                    int rowsAffected = cmd.ExecuteNonQuery();
                    return rowsAffected;
                }
            }
        }
    }
}
