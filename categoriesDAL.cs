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
    class categoriesDAL
    {
        // static string mathod for database connection string

        // Step 1: First Establish connection. 
       static string myconnstring = ConfigurationManager.ConnectionStrings["connstring"].ConnectionString;

        #region Select method

        public DataTable Select()
        {
            // creating Database Connection

            SqlConnection conn = new SqlConnection(myconnstring);

            // create a data table

            DataTable dt = new DataTable();

            // here we will write code. Try Catch
            try
            {
                // writing SQL Query to get all data from database

                string sql = "SELECT * FROM tbl_categories";

                SqlCommand cmd = new SqlCommand(sql,conn);

                // now data adapter

                SqlDataAdapter adapter = new SqlDataAdapter(cmd);

                // Open connection
                conn.Open();

                // adding the values from adapter to Data table dt

                adapter.Fill(dt);

            }
            catch(Exception ex)
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

        #region Insert New category

        public bool Insert(categoriesBLL c)

        {
            // creating a boolean variable and setting its default value to false

            bool isSuccess = false;

            // creating sql connection to database

            SqlConnection conn = new SqlConnection(myconnstring);

            // start block

            try
            {
                // writing SQL Query to get all data from database

                string sql = "INSERT INTO tbl_categories (title,description,added_date,added_by) VALUES (@title,@description,@added_date,@added_by)";

                // create sql command to pass values
                SqlCommand cmd = new SqlCommand(sql, conn);

                //passing values through parameters
                cmd.Parameters.AddWithValue("@title", c.title);
                cmd.Parameters.AddWithValue("@description", c.description);
                cmd.Parameters.AddWithValue("@added_date", c.added_date);
                cmd.Parameters.AddWithValue("@added_by", c.added_by);

                // Open connection
                conn.Open();

                //creating int variable to execute query
                int rows = cmd.ExecuteNonQuery();

                //if the query is executed successfully then its value will be greater than zero else less than zero

                if(rows>0)
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

        #region update Method

        public bool Update(categoriesBLL c)
        {
            // creating boolean variable and set its value to false

            bool isSuccess = false;

            // write query to update

            SqlConnection conn = new SqlConnection(myconnstring);

            try
            {
                // Query to update category

                string sql = "UPDATE tbl_categories SET title=@title, description=@description,added_date=@added_date, added_by=@added_by WHERE id=@id";

                // create sql command to pass values on sql query

                SqlCommand cmd = new SqlCommand(sql, conn);

                //passing values using cmd
                //passing values through parameters
                cmd.Parameters.AddWithValue("@title", c.title);
                cmd.Parameters.AddWithValue("@description", c.description);
                cmd.Parameters.AddWithValue("@added_date", c.added_date);
                cmd.Parameters.AddWithValue("@added_by", c.added_by);
                cmd.Parameters.AddWithValue("@id", c.id);

                //open database connection
                // Open connection
                conn.Open();

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
            catch(Exception ex)
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

        #region Delete category Method

        public bool Delete(categoriesBLL c)

        {
            // create a boolean variable and set its value to false

            bool isSuccess = false;

            // write query to update

            SqlConnection conn = new SqlConnection(myconnstring);

            try
            {
                // write query to delete from database

                string sql = "DELETE FROM tbl_categories WHERE id=@id";

                SqlCommand  cmd = new SqlCommand(sql, conn);

                //passing the values using cmd

                cmd.Parameters.AddWithValue("@id",c.id);

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
            catch(Exception ex)
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

        #region Search Functionality Method

        public DataTable Search(string keywords)
        {
            // SQL Connection for database

            SqlConnection conn = new SqlConnection(myconnstring);

            // need to create data table to hold data from database temporarily

            DataTable dt = new DataTable();

            // here we will insert our real code in Try catch

            try
            {
                // first we will write querries to search categories database. 

                string sql = "SELECT * FROM tbl_categories WHERE id LIKE '%" + keywords + "%' OR title LIKE '%" + keywords + "%' OR description LIKE '%" + keywords + "%'";

                // we need to create SQL Command to execute the Querry

                SqlCommand cmd = new SqlCommand(sql,conn);

                // we need to add SQL Data Adapter to hold Data

                SqlDataAdapter adapter = new SqlDataAdapter(cmd);

                // open connection
                conn.Open();

                // we need to pass this value from Adapter to data Table

               adapter.Fill(dt);


            }
            catch(Exception ex)
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
