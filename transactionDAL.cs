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
    class transactionDAL
    {

        // static string mathod for database connection string
        // Step 1: First Establish connection. 
        static string myconnstring = ConfigurationManager.ConnectionStrings["connstring"].ConnectionString;

        #region Method to Insert Transactions

        // Here we will create and declare a bool variable.

        public bool Insert_Transcation(transactionsBLL t, out int transactionID)
        {

            //create a boolean value and set its default value to false

            bool isSuccess = false;

            // we need to assign the value to transaction iD. Set the out Transaction ID Value to minus one

            transactionID = -1;

            // creating sql connection to database. This will be in between bool variable isSuccess and we have returned it as well.

            SqlConnection conn = new SqlConnection(myconnstring);

            
            // here we will use our try catch block

            try
            {

                // writing SQL Query to get all data from database

                string sql = "INSERT INTO tbl_transactions (type,dea_cust_id,grandTotal,transaction_date,tax,discount,added_by) VALUES (@type,@dea_cust_id,@grandTotal,@transaction_date,@tax,@discount,@added_by); SELECT @@IDENTITY;";

                // create sql command to pass values
                SqlCommand cmd = new SqlCommand(sql, conn);

                //passing values through parameters using cmd

                cmd.Parameters.AddWithValue("@type", t.type);
                cmd.Parameters.AddWithValue("@dea_cust_id", t.dea_cust_id);
                cmd.Parameters.AddWithValue("@grandTotal", t.grandTotal);
                cmd.Parameters.AddWithValue("@transaction_date", t.transaction_date);
                cmd.Parameters.AddWithValue("@tax", t.tax);
                cmd.Parameters.AddWithValue("@discount", t.discount);
                cmd.Parameters.AddWithValue("@added_by", t.added_by);

                // Open Database connection

                conn.Open();

                //creating int variable to execute the query

                // int rows = cmd.ExecuteNonQuery();
                object o = cmd.ExecuteScalar();

                //if the query is executed successfully then its value will be greater than zero else less than zero

                if (o!= null)
                {
                    // Query executed Successfully
                    // 

                    transactionID = int.Parse(o.ToString());

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

        #region Method to Display all Transactions

        public DataTable DisplayAllTransactions()
        {

            // creating sql connection to database. This will be in between bool variable isSuccess and we have returned it as well.

            SqlConnection conn = new SqlConnection(myconnstring);

            // Create a Data Table to hold Data Temporarily

            DataTable dt = new DataTable();

            try
            {
                // write SQL Query to display all transactions

                string sql = "SELECT * FROM tbl_transactions";


                // create sql command to pass values
                SqlCommand cmd = new SqlCommand(sql, conn);

                // Sql data adapter to store data from database

                SqlDataAdapter adapter = new SqlDataAdapter(cmd);

                // Open Database connection

                conn.Open();

                // Need to pass data to adapter

                adapter.Fill(dt);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            finally
            {
                conn.Close();

            }
            return dt;


        }




        #endregion

        #region Methos to Display Transaction based on Transaction Type

        public DataTable DisplayTransactioByType(string type)
        {


            // creating sql connection to database. This will be in between bool variable isSuccess and we have returned it as well.

            SqlConnection conn = new SqlConnection(myconnstring);

            // Create a Data Table to hold Data Temporarily

            DataTable dt = new DataTable();

            try
            {
                // write SQL Query to display all transactions

                string sql = "SELECT * FROM tbl_transactions WHERE type ='"+type+"'";

                // create sql command to pass values

                SqlCommand cmd = new SqlCommand(sql, conn);

                // Sql data adapter to store data from database

                SqlDataAdapter adapter = new SqlDataAdapter(cmd);

                // Open Database connection

                conn.Open();

                // Need to pass data to adapter

                adapter.Fill(dt);


            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            finally
            {
                conn.Close();

            }


            return dt;


        }



        #endregion
    }
}
