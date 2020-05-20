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
    class productsDAL
    {

        // static string mathod for database connection string
                // Step 1: First Establish connection. 
        static string myconnstring = ConfigurationManager.ConnectionStrings["connstring"].ConnectionString;

        #region Select Method for Product Module

        public DataTable Select()

        {
            // creating Database Connection

            SqlConnection conn = new SqlConnection(myconnstring);

            // create a data table to hold data from Database

            DataTable dt = new DataTable();

            // here we will write code. Try Catch
            try
            {
                // Writing SQL Query to get all data from database

                string sql = "SELECT * FROM tbl_products";

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

        #region Method to Insert Product in database

        public bool Insert(productsBLL p)
        {

            // creating a boolean variable and setting its default value to false

            bool isSuccess = false;

            // creating sql connection to database. This will be in between bool variable isSuccess and we have returned it as well.

            SqlConnection conn = new SqlConnection(myconnstring);

            // start block

            try
            {
                // writing SQL Query to get all data from database

                string sql = "INSERT INTO tbl_products (name,category,qty,description,rate,added_date,added_by) VALUES (@name,@category,@qty,@description,@rate,@added_date,@added_by)";

                // create sql command to pass values
                SqlCommand cmd = new SqlCommand(sql, conn);

                //passing values through parameters
                cmd.Parameters.AddWithValue("@name", p.name);
                cmd.Parameters.AddWithValue("@category", p.category);
                cmd.Parameters.AddWithValue("@qty", p.qty);
                cmd.Parameters.AddWithValue("@description", p.description);
                cmd.Parameters.AddWithValue("@rate", p.rate);
                cmd.Parameters.AddWithValue("@added_date", p.added_date);
                cmd.Parameters.AddWithValue("@added_by", p.added_by);

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

        #region Method to Update Product in Database

        public bool Update(productsBLL p)
        {
            // creating boolean variable and set its value to false

            bool isSuccess = false;

            // write query to update. Write SQL Connection

            SqlConnection conn = new SqlConnection(myconnstring);


            try
            {
                // writing SQL Query to get all data from database. We wil lalways add id into Update

                string sql = "UPDATE tbl_products SET name=@name,category=@category,qty=@qty,description=@description,rate=@rate,added_date=@added_date,added_by=@added_by WHERE id=@id";

                // create sql command to pass values to query

                SqlCommand cmd = new SqlCommand(sql, conn);

                //passing values through parameters

                cmd.Parameters.AddWithValue("@name", p.name);
                cmd.Parameters.AddWithValue("@category", p.category);
                cmd.Parameters.AddWithValue("@qty", p.qty);
                cmd.Parameters.AddWithValue("@description", p.description);
                cmd.Parameters.AddWithValue("@rate", p.rate);
                cmd.Parameters.AddWithValue("@added_date", p.added_date);
                cmd.Parameters.AddWithValue("@added_by", p.added_by);
                cmd.Parameters.AddWithValue("@id", p.id);

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

        #region Delete from Products Method

        public bool Delete(productsBLL p)

        {
            // create a boolean variable and set its value to false

            bool isSuccess = false;

            // write query to update

            SqlConnection conn = new SqlConnection(myconnstring);

            try
            {
                // write query to delete from database

                string sql = "DELETE FROM tbl_products WHERE id=@id";

                SqlCommand cmd = new SqlCommand(sql, conn);

                //passing the values using cmd

                cmd.Parameters.AddWithValue("@id", p.id);

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

        #region Search Method for Product Module

        public DataTable Search(string keywords)
        {

            // Create SQL Connection for Database
            SqlConnection conn = new SqlConnection(myconnstring);
            
            // Creating Data Table to Hold Value from database

            DataTable dt = new DataTable();

            try
            {
                // write SQL Query to search the product
                string sql = "SELECT * FROM tbl_products WHERE id LIKE '%"+keywords+ "%' OR name LIKE '%" + keywords + "%' OR category LIKE '%" + keywords + "%'";

                // Create SQL Command to execute query
                SqlCommand cmd = new SqlCommand(sql, conn);

                //SQL Data Adapter to hold data from database
                SqlDataAdapter adapter = new SqlDataAdapter(cmd);

                //Open Database Connection

                conn.Open();

                // Need t opass the value from Adapter to DT
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

        #region Method to Search Product in Transaction Module
        public productsBLL GetProductsForTransaction(string keyword)
        {
            // Create an abject of productBLL and return it

            productsBLL p = new productsBLL();

            // SQL Connection first

            SqlConnection conn = new SqlConnection(myconnstring);

            // create data table to store data temporarily

            DataTable dt = new DataTable();

            try
            {

                // here we will write Query to get the details

                string sql = "SELECT name,qty,rate FROM tbl_products WHERE id LIKE '%" + keyword+"%' OR name LIKE '%"+keyword+"%'";

                // Execute Query

                SqlDataAdapter adapter = new SqlDataAdapter(sql, conn);

                // Open Database Connection

                conn.Open();

                // Pass values from adapter to dt

                adapter.Fill(dt);

                // If we have any values on dt then set the values to products

                if(dt.Rows.Count>0)
                {
                    p.name = dt.Rows[0]["name"].ToString();
                   p.qty = decimal.Parse(dt.Rows[0]["qty"].ToString());
                    p.rate = decimal.Parse(dt.Rows[0]["rate"].ToString());

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

            return p;


        }




        #endregion

        #region Method to Get Product ID Based on Product Name


        public productsBLL GetproductIDFromName(string ProductName)
        {
            // First Create an Object of DeaCust BLL and Return it

            productsBLL p = new productsBLL();

            //SQL Connection
            SqlConnection conn = new SqlConnection(myconnstring);

            // Data Table to hold data

            DataTable dt = new DataTable();

            try
            {

                //SQL Query to get id based on Name

                string sql = "SELECT id FROM tbl_products WHERE name = '" + ProductName + "'";

                // create sql data adapter
                SqlDataAdapter adapter = new SqlDataAdapter(sql, conn);

                //open connection

                conn.Open();

                // Passing the values from adapter to datatable
                adapter.Fill(dt);

                if (dt.Rows.Count > 0)
                {
                    //pass the value from dt to DeaCustBLL dt:
                    p.id = int.Parse(dt.Rows[0]["id"].ToString());

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


            return p;


        }


        #endregion

        #region Method to Get Current Qty from Database Based on Product ID

        public decimal GetProductQty(int productID)
        {
            // when the error will show, we need to create decimal variable and return it. we will do it below.

            //SQL Connection first

            SqlConnection conn = new SqlConnection(myconnstring);

            decimal qty = 0;

            // create a data table to save data from database temporarily

            DataTable dt = new DataTable();

            try
            {
                // write SQL Query to get Qty from Database

                string sql = "SELECT qty FROM tbl_products WHERE id =  " + productID;

                // create sql command 
                SqlCommand cmd = new SqlCommand(sql, conn);

                // create SQL Data Adapter to Execute the Query
                SqlDataAdapter adapter = new SqlDataAdapter(cmd);

                //Open Database Connection
                conn.Open();

                // Pass the value from Data Adapter to Data Table
                adapter.Fill(dt);

                // Check if the data table has values or not

                if (dt.Rows.Count > 0)
                {
                    qty = decimal.Parse(dt.Rows[0]["qty"].ToString());
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
            return qty;


        }


        #endregion

        #region Method to Update Qty 

        public bool UpdateQuantity(int ProductID,  decimal Qty)
        {
            // Create a Boolean Variable and set its value to false

            bool success = false;

            //SQL Connection first

            SqlConnection conn = new SqlConnection(myconnstring);

            try
            {
                //Write the Query to update Qty

                string sql = "UPDATE tbl_products SET qty=@qty WHERE id=@id";

                // Create SQL Command to pass the value into Query

                SqlCommand cmd = new SqlCommand(sql, conn);

                //Passing values through parameters

                cmd.Parameters.AddWithValue("@qty", Qty);

                cmd.Parameters.AddWithValue("@id", ProductID);

                //Open Database connection
                conn.Open();

                // Create Int Variable and check whether the Query is executed Successfully or Not.

                int rows = cmd.ExecuteNonQuery();

                //Lets check out if the Query is Successfully Executed or Not

                if(rows>0)
                {
                    // query executed successfully
                    success = true;

                }
                else
                {
                    success = false;

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


            return success;
        }

        #endregion

        #region Method to Increase Product

        public bool IncreaseProduct(int ProductID , decimal IncreaseQty)
        {
            // Create a boolean variable and set its value to false.

            bool success = false;

            //SQL Connection first to connect database

            SqlConnection conn = new SqlConnection(myconnstring);

            // start try catch

            try
            {

                // Get the Current Qty from Database based on id

                decimal currentQty = GetProductQty(ProductID);

                // Increase the current Qty by the Qty purchased from Dealer

                decimal NewQty = currentQty + IncreaseQty;

                // Now we need to update Qty. we will use Update method.

                success = UpdateQuantity(ProductID, NewQty);

            }
            catch(Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            finally
            {
                conn.Close();
            }


            return success;
        }


        #endregion

        #region Method to Derease product

        public bool DecreaseProduct(int ProductID , decimal Qty)
        {
            // Create Boolean and set its value to false

            bool success = false;

            //SQL Connection first to connect database

            SqlConnection conn = new SqlConnection(myconnstring);


            try
            {

                // Get the Current Product Qty from Database based on id

                decimal currentQty = GetProductQty(ProductID);

                // Decrease the Product Qty by the Qty Based on Product Sales

                decimal NewQty = currentQty - Qty;

                // Now we need to update Qty. we will use Update method.

                success = UpdateQuantity(ProductID, NewQty);



            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            finally
            {
                conn.Close();
            }

            return success;

        }



        #endregion

        #region Method to Display Products based on Category

        public DataTable DisplayProductsByCategory(string category)
        {
            //SQL Connection first to connect database

            SqlConnection conn = new SqlConnection(myconnstring);

            DataTable dt = new DataTable();

            try
            {
                // SQL Query to display products based on category

                string sql = "SELECT * FROM tbl_products WHERE category ='"+category+"'";

                // create sql command

                SqlCommand cmd = new SqlCommand(sql, conn);

                SqlDataAdapter adapter = new SqlDataAdapter(cmd);

                // Open database connection

                conn.Open();

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
