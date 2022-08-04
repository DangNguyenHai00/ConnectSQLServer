using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SqlClient;

namespace ConnectSQLServer
{
    class Program
    {
        static void Main(string[] args)
        {
            string connString = @"Server =LAPTOP-9Q643LVC; Database = Employment; Trusted_Connection = True;";
            //variables to store the query results
            string employId,FullName,PhoneNB,Email;
            DateTime Birth;
            int Employee_type;

            try
            {
                //sql connection object
                using (SqlConnection conn = new SqlConnection(connString))
                {

                    //retrieve the SQL Server instance version
                    string query = @"EXEC Search_employee_type 2";
                    //define the SqlCommand object
                    SqlCommand cmd = new SqlCommand(query, conn);

                    //open connection
                    conn.Open();

                    //execute the SQLCommand
                    SqlDataReader dr = cmd.ExecuteReader();

                    Console.WriteLine(Environment.NewLine + "Retrieving data from database..." + Environment.NewLine);
                    Console.WriteLine("Retrieved records:");

                    //check if there are records
                    if (dr.HasRows)
                    {
                        while (dr.Read())
                        {
                            employId = dr.GetString(0);
                            FullName = dr.GetString(1);
                            Birth = dr.GetDateTime(2);
                            PhoneNB = dr.GetString(3);
                            Email = dr.GetString(4);
                            Employee_type = dr.GetInt32(5);

                            //display retrieved record
                            Console.WriteLine("{0},{1},{2},{3},{4},{5}",employId, FullName, Birth.ToString(), PhoneNB, Email, Employee_type.ToString());
                        }
                    }
                    else
                    {
                        Console.WriteLine("No data found.");
                    }

                    //close data reader
                    dr.Close();

                    //close connection
                    conn.Close();
                }
            }
            catch (Exception ex)
            {
                //display error message
                Console.WriteLine("Exception: " + ex.Message);
            }
        }
    }
}
