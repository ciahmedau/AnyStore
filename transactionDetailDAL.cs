using AnyStore.BLL;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace AnyStore.DAL
{
    class transactionDetailDAL
    {

        // static string mathod for database connection string
        // Step 1: First Establish connection. 
        static string myconnstring = ConfigurationManager.ConnectionStrings["connstring"].ConnectionString;

        #region Method to Insert Transactions Details

        // Here we will create and declare a bool variable.

        public bool InsertTranscationDetail(transactionDetailBLL td)

        {
            // Create a boolean Variable , create a boolean value and set its default value to false

            bool isSuccess = false;

           // creating sql connection to database. This will be in between bool variable isSuccess and we have returned it as well.

            SqlConnection conn = new SqlConnection(myconnstring);

            try
            {

                // writing SQL Query to get all data from database

                string sql = "INSERT INTO tbl_transaction_detail (product_id,rate,qty,total,dea_cust_id,added_date,added_by) VALUES (@product_id,@rate,@qty,@total,@dea_cust_id,@added_date,@added_by)";

                // create sql command to pass values
                SqlCommand cmd = new SqlCommand(sql, conn);

                //passing values through parameters using cmd

                cmd.Parameters.AddWithValue("@product_id", td.product_id);
                cmd.Parameters.AddWithValue("@rate", td.rate);
                cmd.Parameters.AddWithValue("@qty", td.qty);
                cmd.Parameters.AddWithValue("@total", td.total);
                cmd.Parameters.AddWithValue("@dea_cust_id", td.dea_cust_id);
                cmd.Parameters.AddWithValue("@added_date", td.added_date);
                cmd.Parameters.AddWithValue("@added_by", td.added_by);

                // Open Database connection

                conn.Open();

                //creating int variable to execute the query. Here we don't need first column or first row so we will use this old method
                int rows = cmd.ExecuteNonQuery();

                //object o = cmd.ExecuteScalar();

                //if the query is executed successfully then its value will be greater than zero else less than zero

                if (rows>0)
                {
                    
                    isSuccess = true;
                }
                else
                {
                    // Query Not Done successfully
                    isSuccess = false;
                }


            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            finally
            {
                conn.Close();

            }

            return isSuccess;
        }



        #endregion



    }
}
