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
    class userDAL
    {
        // Step 1: First Establish connection. 
        string myconnstring = ConfigurationManager.ConnectionStrings["connstring"].ConnectionString;

     #region Select Data from Database

        public DataTable Select()
        {
            // Step 2: Static Method to connect Database

            SqlConnection conn = new SqlConnection(myconnstring);

            // to Hold Data from Database
            DataTable dt = new DataTable();

            try
            {
                // SQL Querry to Get Data from Database
                string sql = "SELECT * FROM tbl_users";
                
                // Step 3 : For Executing Command
                SqlCommand cmd = new SqlCommand(sql, conn);
                
                //  Step 4: Getting Data from Database
                SqlDataAdapter adapter = new SqlDataAdapter(cmd);
                
                // Step 5: Database Connection Open
                conn.Open();
                // Fill Data in our Data Table
                adapter.Fill(dt);
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
            // Return the value in Data Table
            return dt;
        }

        #endregion

        #region Insert data in Database

        public bool Inset(userBLL u)
        {

            bool isSuccess = false;
            SqlConnection conn = new SqlConnection(myconnstring);

            try
            {
                // write a querry to enter data in Database
                String sql = "INSERT INTO tbl_users (first_name,last_name,email,username,password,contact,address,gender,user_type,added_date,added_by) VALUES(@first_name,@last_name,@email,@username,@password,@contact,@address,@gender,@user_type,@added_date,@added_by)";
                
                // create sql command
                SqlCommand cmd = new SqlCommand(sql, conn);

                // create parameters to pass values
                cmd.Parameters.AddWithValue("@first_name", u.first_name);
                cmd.Parameters.AddWithValue("@last_name", u.last_name);
                cmd.Parameters.AddWithValue("@email", u.email);
                cmd.Parameters.AddWithValue("@username", u.username);
                cmd.Parameters.AddWithValue("@password", u.password);
                cmd.Parameters.AddWithValue("@contact", u.contact);
                cmd.Parameters.AddWithValue("@address", u.address);
                cmd.Parameters.AddWithValue("@gender", u.gender);
                cmd.Parameters.AddWithValue("@user_type", u.user_type);
                cmd.Parameters.AddWithValue("@added_date", u.added_date);
                cmd.Parameters.AddWithValue("@added_by", u.added_by);

                // open connection to database

                conn.Open();

                int rows = cmd.ExecuteNonQuery();
                // if the query is executed successfully then the value to rows will be greater than 0 else it will be less than 0

                if(rows>0)
                {
                    // querry successful
                    isSuccess = true;
                }
                else
                {
                    //query failed
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

        #region Update Data in Database

        public bool Update(userBLL u)
        {
            bool isSuccess = false;
            SqlConnection conn = new SqlConnection(myconnstring);

            try
            {
               // we will write a query to update data in database
                string sql = "UPDATE tbl_users SET first_name=@first_name,last_name=@last_name,email=@email,username=@username,password=@password,contact=@contact,address=@address,gender=@gender,user_type=@user_type,added_date=@added_date,added_by=@added_by WHERE id=@id";

                // sql command
                SqlCommand cmd = new SqlCommand(sql, conn);

                cmd.Parameters.AddWithValue("@first_name", u.first_name);
                cmd.Parameters.AddWithValue("@last_name", u.last_name);
                cmd.Parameters.AddWithValue("@email", u.email);
                cmd.Parameters.AddWithValue("@username", u.username);
                cmd.Parameters.AddWithValue("@password", u.password);
                cmd.Parameters.AddWithValue("@contact", u.contact);
                cmd.Parameters.AddWithValue("@address", u.address);
                cmd.Parameters.AddWithValue("@gender", u.gender);
                cmd.Parameters.AddWithValue("@user_type", u.user_type);
                cmd.Parameters.AddWithValue("@added_date", u.added_date);
                cmd.Parameters.AddWithValue("@added_by", u.added_by);
                cmd.Parameters.AddWithValue("@id", u.id);

                // create database connection
                conn.Open();

                int rows = cmd.ExecuteNonQuery();
                if(rows>0)
                {
                    // query is successful
                    isSuccess = true;
                }
                else
                {
                    // query failed
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

        #region Delete Data from Database

        public bool Delete(userBLL u)
        {
            bool isSuccess = false;

            // create sql connection

            SqlConnection conn = new SqlConnection(myconnstring);
            try
            {
                // we will write a querry to delete data from database

                string sql = "DELETE FROM tbl_users WHERE id=@id";

                // create sql command

                SqlCommand cmd = new SqlCommand(sql, conn);

                // create a parameter to pass values
                cmd.Parameters.AddWithValue("@id", u.id);
                
                // Open connection

                conn.Open();

                int rows = cmd.ExecuteNonQuery();
                if(rows>0)
                {
                    // query successful
                    isSuccess = true;

                }
                else
                {
                    // querry unsuccessful
                    isSuccess = false;
                }
            }
            catch( Exception ex)
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

        #region Search User on Database using Keyword

        public DataTable Search(string keyword)
        {
            // Step 2: Static Method to connect Database

            SqlConnection conn = new SqlConnection(myconnstring);

            // to Hold Data from Database
            DataTable dt = new DataTable();

            try
            {
                // SQL Querry to Get Data from Database
                string sql = "SELECT * FROM tbl_users WHERE id LIKE '%"+keyword+"%' OR first_name LIKE '%"+keyword+ "%' OR  last_name LIKE '%" + keyword + "%' OR username LIKE '%" + keyword+"%'";

                // Step 3 : For Executing Command
                SqlCommand cmd = new SqlCommand(sql, conn);

                //  Step 4: Getting Data from Database
                SqlDataAdapter adapter = new SqlDataAdapter(cmd);

                // Step 5: Database Connection Open
                conn.Open();
                // Fill Data in our Data Table
                adapter.Fill(dt);
            }
            catch (Exception ex)
            {
                // Throw Message if any error occurs
                MessageBox.Show(ex.Message);

            }
            finally
            {
                // Close connection
                conn.Close();
            }
            // Return the value in Data Table
            return dt;
        }




        #endregion

        #region Getting User ID from User name

        public userBLL GetIDFromUsername(string username)
        {
            userBLL u = new userBLL();

            SqlConnection conn = new SqlConnection(myconnstring);

            DataTable dt = new DataTable();

            try
            {
                // write query for this

                string sql = "SELECT id FROM tbl_users WHERE username='" + username + "'";

                SqlDataAdapter adapter = new SqlDataAdapter(sql, conn);

                conn.Open();

                adapter.Fill(dt);

                if(dt.Rows.Count>0)
                {
                    u.id = int.Parse(dt.Rows[0]["id"].ToString());
                }
            }
            catch(Exception ex)

            {
                // Throw Message if any error occurs
                MessageBox.Show(ex.Message);

            }
            finally
            {
                conn.Close();

            }
            return u;


        }


        #endregion
    }
}
