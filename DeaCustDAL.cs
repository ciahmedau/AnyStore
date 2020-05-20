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
    class DeaCustDAL
    {

        // Step 1: First Establish connection. 
        static string myconnstring = ConfigurationManager.ConnectionStrings["connstring"].ConnectionString;

        #region Select Method for Dealer and Customer

        public DataTable Select()

        {
            // creating SQL Database Connection

            SqlConnection conn = new SqlConnection(myconnstring);

            // create a data table to hold data from Database

            DataTable dt = new DataTable();

            // here we will write code. Try Catch

            try
            {
                // Writing SQL Query to get all data from database

                string sql = "SELECT * FROM tbl_dea_cust";

                // To execute this query we will use SQL Command

                SqlCommand cmd = new SqlCommand(sql, conn);

                // now data adapter to hold the values from database

                SqlDataAdapter adapter = new SqlDataAdapter(cmd);

                // Open connection now

                conn.Open();

                // adding the values from adapter to Data table dt

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

        #region Insert Method for Dealer and Customer

        public bool Insert(DeaCustBLL dc)
        {

            // creating a boolean variable and setting its default value to false

            bool isSuccess = false;

            // creating sql connection to database. This will be in between bool variable isSuccess and we have returned it as well.

            SqlConnection conn = new SqlConnection(myconnstring);

            // start block

            try
            {
                // writing SQL Query to get all data from database

                string sql = "INSERT INTO tbl_dea_cust (type,name,email,contact,address,added_date,added_by) VALUES (@type,@name,@email,@contact,@address,@added_date,@added_by)";

                // create sql command to pass values
                SqlCommand cmd = new SqlCommand(sql, conn);

                //passing values through parameters

                cmd.Parameters.AddWithValue("@type", dc.type);
                cmd.Parameters.AddWithValue("@name", dc.name);
                cmd.Parameters.AddWithValue("@email", dc.email);
                cmd.Parameters.AddWithValue("@contact", dc.contact);
                cmd.Parameters.AddWithValue("@address", dc.address);
                cmd.Parameters.AddWithValue("@added_date", dc.added_date);
                cmd.Parameters.AddWithValue("@added_by", dc.added_by);

                // Open connection
                conn.Open();

                //creating int variable to execute query
                int rows = cmd.ExecuteNonQuery();

                //if the query is executed successfully then its value will be greater than zero else less than zero

                if (rows > 0)
                {
                    // Query executed Successfully
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

        #region Update method for Dealer and Customer
        public bool Update(DeaCustBLL dc)
        {

            // creating boolean variable and set its value to false

            bool isSuccess = false;

            // write query to update. Write SQL Connection

            SqlConnection conn = new SqlConnection(myconnstring);


            try
            {
                // writing SQL Query to get all data from database. We wil lalways add id into Update

                string sql = "UPDATE tbl_dea_cust SET type=@type,name=@name,email=@email,contact=@contact,address=@address,added_date=@added_date,added_by=@added_by WHERE id=@id";

                // create sql command to pass values to query

                SqlCommand cmd = new SqlCommand(sql, conn);

                //passing values through parameters

                cmd.Parameters.AddWithValue("@type", dc.type);
                cmd.Parameters.AddWithValue("@name", dc.name);
                cmd.Parameters.AddWithValue("@email", dc.email);
                cmd.Parameters.AddWithValue("@contact", dc.contact);
                cmd.Parameters.AddWithValue("@address", dc.address);
                cmd.Parameters.AddWithValue("@added_date", dc.added_date);
                cmd.Parameters.AddWithValue("@added_by", dc.added_by);
                cmd.Parameters.AddWithValue("@id", dc.id);

                // Open connection
                conn.Open();

                //creating int variable to execute query
                int rows = cmd.ExecuteNonQuery();

                //if the query is executed successfully then its value will be greater than zero else less than zero

                if (rows > 0)
                {
                    // Query executed Successfully
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

        #region Delete Method for Dealer and Customer
        
        public bool Delete(DeaCustBLL dc)
        {

            // create a boolean variable and set its value to false

            bool isSuccess = false;

            // write query to update

            SqlConnection conn = new SqlConnection(myconnstring);

            try
            {
                // write query to delete from database

                string sql = "DELETE FROM tbl_dea_cust WHERE id=@id";

                SqlCommand cmd = new SqlCommand(sql, conn);

                //passing the values using cmd

                cmd.Parameters.AddWithValue("@id", dc.id);

                // open connection
                conn.Open();

                // we need to create integer variable to execute querry

                //creating int variable to execute query
                int rows = cmd.ExecuteNonQuery();

                // if query is successfully executed then the value will be greater than zero
                if (rows > 0)
                {
                    isSuccess = true;
                }
                else
                {
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

        #region Search Method for Dealer and Customer

        public DataTable Search(string keywords)
        {

            // Create SQL Connection for Database
            SqlConnection conn = new SqlConnection(myconnstring);

            // Creating Data Table to Hold Value from database

            DataTable dt = new DataTable();

            try
            {
                // write SQL Query to search the product
                string sql = "SELECT * FROM tbl_dea_cust WHERE id LIKE '%" + keywords + "%' OR name LIKE '%" + keywords + "%' OR type LIKE '%" + keywords + "%'";

                // Create SQL Command to execute query
                SqlCommand cmd = new SqlCommand(sql, conn);

                //SQL Data Adapter to hold data from database
                SqlDataAdapter adapter = new SqlDataAdapter(cmd);

                //Open Database Connection

                conn.Open();

                // Need t opass the value from Adapter to DT
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

        #region Method to Search Dealer or Customer For Transaction Module

        public DeaCustBLL SearchDealerCustomerTransaction(string keyword)
        {
            // create an object for DeaCust

            DeaCustBLL dc = new DeaCustBLL();

            // create a database connection

            SqlConnection conn = new SqlConnection(myconnstring);

            // create a data table to hold the value temporarily

            DataTable dt = new DataTable();

            try
            {

                // writing SQL Query to get all data from database. We wil lalways add id into Update

                string sql = "SELECT name,email,contact,address FROM tbl_dea_cust WHERE id LIKE '%"+keyword+ "%' OR name LIKE '%" + keyword + "%'";

                // create SQL data adapter to execute the query

                SqlDataAdapter adapter = new SqlDataAdapter(sql, conn);

                // open the database connection

                conn.Open();

                // transfer data from sql data adapter to data table

                adapter.Fill(dt);

                // if we have values in dt we need to save it in dealercustomer BLL

                if(dt.Rows.Count>0)
                {
                    dc.name = dt.Rows[0]["name"].ToString();
                    dc.email = dt.Rows[0]["email"].ToString();
                    dc.contact = dt.Rows[0]["contact"].ToString();
                    dc.address = dt.Rows[0]["address"].ToString();


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
            return dc;

        }

        #endregion

        #region Method to Get the ID of Dealer or Customer Based on Name

        public DeaCustBLL GetDeaCustIDFromName(string Name)
        {
            // First Create an Object of DeaCust BLL and Return it

            DeaCustBLL dc = new DeaCustBLL();

            //SQL Connection

            SqlConnection conn = new SqlConnection(myconnstring);

            // Data Table to hold data

            DataTable dt = new DataTable();

            try
            {

                //SQL Query to get id based on Name

                string sql = "SELECT id FROM tbl_dea_cust WHERE name = '"+Name+"'";

                // create sql data adapter
                SqlDataAdapter adapter = new SqlDataAdapter(sql, conn);

                //open connection

                conn.Open();

                // Passing the values from adapter to datatable
                adapter.Fill(dt);

                if (dt.Rows.Count > 0)
                {
                    //pass the value from dt to DeaCustBLL dt:
                    dc.id = int.Parse(dt.Rows[0]["id"].ToString());

                }

            }
            catch(Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            finally
            {
                conn.Close();
            }


            return dc;


        }


        #endregion

    }

}   
