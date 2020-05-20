using AnyStore.BLL;
using AnyStore.DAL;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace AnyStore.UI
{
    public partial class frmProducts : Form
    {
        public frmProducts()
        {
            InitializeComponent();
        }


        categoriesDAL cdal = new categoriesDAL();
        // Here we will create Objects reference to our BLL and DAL
        productsBLL p = new productsBLL();
        productsDAL pdal = new productsDAL();
        // also create an object for user DAL
        userDAL udal = new userDAL();

        private void pictureBoxClose_Click(object sender, EventArgs e)
        {
            // add code to hide this form
            MessageBox.Show("You are Closing the Product Form");
            this.Hide();
        }

       
        private void frmProducts_Load(object sender, EventArgs e)
        {
            // creating Data Table to Hold the Categories from Data base

            DataTable categoriesDT = cdal.Select();

            //Specify Data source for Category ComboBox

            cmbCategory.DataSource = categoriesDT;

            // specify Display Member and Value Member for Combobox

            cmbCategory.DisplayMember = "title";
            cmbCategory.ValueMember = "title";

            // Load all the products in data Grid View

            DataTable dt = pdal.Select();
            dgvProducts.DataSource = dt;
        }

        private void btnAdd_Click(object sender, EventArgs e)
        {
            // Step 1: Get all values from Product form
            // we have already created two objects above and we will use them to get values form the frms.

            p.name = txtName.Text;
            p.category = cmbCategory.Text;
            p.qty = decimal.Parse(txtQty.Text); 
            p.description = txtDescription.Text;
            p.rate = decimal.Parse(txtRate.Text);
            p.added_date = DateTime.Now;

            // we will get user name who is logged in

            string loggedUsr = frmLogin.loggedIn;

            userBLL usr = udal.GetIDFromUsername(loggedUsr);

            p.added_by = usr.id;

            // create Boolean variable to check if the product is added successfully or Not

            bool success = pdal.Insert(p);

            // if the product is added successfully then the value of success will be true or will be false.

            if(success==true)
            {

                // product inserted successfully
                MessageBox.Show("Product Inserted Successfully");

                // we need to clear Text Boxes. we will write a method for this.
                // Calling the Clear Method
                Clear();

                // Refresh the Data Grid View as well.For that we wil lcreate a data Table.
                DataTable dt = pdal.Select();

                dgvProducts.DataSource = dt;


            }
            else
            {
                // Falied to Insert
                MessageBox.Show("Product NOT Inserted Successfully");
            }

        }

        public void Clear()
        {
            txtID.Text = "";
            txtName.Text = "";
            cmbCategory.Text = "";
            txtQty.Text = "";
            txtDescription.Text = "";
            txtRate.Text = "";
            txtSearch.Text = "";
        }

        private void dgvProducts_RowHeaderMouseClick(object sender, DataGridViewCellMouseEventArgs e)
        {
            // Create Integer variable to know which product was clicked

            int rowIndex = e.RowIndex;

            // Display value on respective text Boxes

            txtID.Text = dgvProducts.Rows[rowIndex].Cells[0].Value.ToString();
            txtName.Text = dgvProducts.Rows[rowIndex].Cells[1].Value.ToString();
            cmbCategory.Text= dgvProducts.Rows[rowIndex].Cells[2].Value.ToString();
            txtQty.Text = dgvProducts.Rows[rowIndex].Cells[5].Value.ToString();
            txtDescription.Text = dgvProducts.Rows[rowIndex].Cells[3].Value.ToString();
            txtRate.Text = dgvProducts.Rows[rowIndex].Cells[4].Value.ToString();
        }

        private void btnUpdate_Click(object sender, EventArgs e)
        {
            // Get the values from UI of Product form

            p.id = int.Parse(txtID.Text);
            p.name = txtName.Text;
            p.category = cmbCategory.Text;
            //p.qty = decimal.Parse(txtQty.Text);
            p.description = txtDescription.Text;
            p.rate = decimal.Parse(txtRate.Text);
            p.added_date = DateTime.Now;

            // Getting User name of Logged in User
            // we will get user name who is logged in

            string loggedUsr = frmLogin.loggedIn;

            userBLL usr = udal.GetIDFromUsername(loggedUsr);

            p.added_by = usr.id;

            // createa boolean variable to check if the product is updated or not

            bool success = pdal.Update(p);

            // If the product is updated successfully then the value of success will be true

            if(success==true)
            {
                //Product added successfully
                MessageBox.Show("Product Updated Successfully");
               
                // we need to clear Text Boxes. we will write a method for this.
                // Calling the Clear Method
                Clear();

                // Refresh the Data Grid View as well.For that we wil lcreate a data Table.
                DataTable dt = pdal.Select();

                dgvProducts.DataSource = dt;
            }
            else
            {
                MessageBox.Show("Product Not Updated Successfully");
            }


        }

        private void btnDelete_Click(object sender, EventArgs e)
        {
            // Get the ID of the Product to be deleted

            p.id = int.Parse(txtID.Text);

            // Create a bool variable to check if the data is deleted

            bool success = pdal.Delete(p);

            // if product is deleted successfully then the value of success will be true else it will be flase

            if (success == true)
            {
                //Product Deleted successfully
                MessageBox.Show("Product Deleted Successfully");

                // we need to clear Text Boxes. we will write a method for this.
                // Calling the Clear Method
                Clear();

                // Refresh the Data Grid View as well.For that we wil lcreate a data Table.
                DataTable dt = pdal.Select();

                dgvProducts.DataSource = dt;
            }
            else
            {
                MessageBox.Show("Product Not Deleted Successfully");
            }


        }

        private void txtSearch_TextChanged(object sender, EventArgs e)
        {

            // Get Keyword from form

            string keywords = txtSearch.Text;
            if(keywords!=null)
            {
                // then we will search the products. We Need Data Table for that
                DataTable dt = pdal.Search(keywords);
                dgvProducts.DataSource = dt;

            }
            else
            {
                // otherwise we will display products
                DataTable dt = pdal.Select();
                dgvProducts.DataSource = dt;

            }

        }
    }
}
