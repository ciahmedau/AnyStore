using AnyStore.BLL;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace AnyStore.DAL
{
    class loginDAL
    {
        // Create Static String Mothod to Connect to Database
        // Step 1: First Establish connection. 
        static string myconnstring = ConfigurationManager.ConnectionStrings["connstring"].ConnectionString;

        // we will write a method to check our login
        public bool loginCheck(loginBLL l)
        {
            // Step 2: create a boolean variable and set its value to false and return it
            bool isSuccess = false;

            // we need to connect to database
            // Step 3: Static Method to connect Database

            SqlConnection conn = new SqlConnection(myconnstring);

            // Step 4: we will add try catch block to check login

            try
            {
                // Step 5:here we will add query to check login

                string sql = "SELECT * FROM tbl_users WHERE username=@username AND password=@password AND user_type=@user_type";

                // Step 6 : For Executing Command , we will create SQL Command to pass values
                SqlCommand cmd = new SqlCommand(sql, conn);

                // Step 7: set the parameters , same a step5 entries
                cmd.Parameters.AddWithValue("@username", l.username);
                cmd.Parameters.AddWithValue("@password", l.password);
                cmd.Parameters.AddWithValue("@user_type", l.user_type);

                //  Step 8: Getting Data from Database
                SqlDataAdapter adapter = new SqlDataAdapter(cmd);

                // Step 9: we will create a data table to hold temporary data
                DataTable dt = new DataTable();

                // Step 10: Fill Data in our Data Table
                adapter.Fill(dt);

                //Step 11: checking the rows in Data Table

                if(dt.Rows.Count>0)
                {
                    // login Successful
                    isSuccess = true;
                }
                else
                {
                    // not Successful
                    isSuccess = false;

                }
            }

            catch(Exception ex)
            {
                // Throw Message if any error occurs
                MessageBox.Show(ex.Message);

            }

            finally
            {

                // Close connection
                conn.Close();
            }
            return isSuccess;

        }

    }




}
