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
    public partial class frmCategories : Form
    {
        public frmCategories()
        {
            InitializeComponent();
        }

        private void pictureBoxClose_Click(object sender, EventArgs e)
        {

            MessageBox.Show("You are closing Categories Form");
            this.Hide();
        }
        
        // here we wil lcreate two objects for our files. we will use them in our add method.
        
        categoriesBLL c = new categoriesBLL();
        categoriesDAL dal = new categoriesDAL();
        
        userDAL udal = new userDAL();

        private void btnADD_Click(object sender, EventArgs e)
        {
            // get values from category form

            c.title = txtTitle.Text;
            c.description = txtDescription.Text;
            c.added_date = DateTime.Now;

            // Getting ID in Added by Field. This will give name of laogged in use
            string loggedUser = frmLogin.loggedIn;

            // we need to convert this into user id

            userBLL usr = udal.GetIDFromUsername(loggedUser);
            // passing the id of logged in user in added by field
            c.added_by = usr.id;

            // sreate boolean method to insert data in to database.

            bool success = dal.Insert(c);

            //if the category is inserted successfully then the value of the success will be true else flase

            if (success == true)
            {
                // new category inserted successfully

                MessageBox.Show("Inserted successfully");
                Clear();
                

                // display all added categories in Data Grid View. We need a Data Table for this
                 DataTable dt = dal.Select();
                // here we have used select method to show data in data grid view
                 dgvCategories.DataSource = dt;
            }
            else
            {
                // Not success

                MessageBox.Show("Inserted NOT-successfully");
            }

        }

        // write a method to clear the content

        public void Clear()
        {
            txtCatgoryID.Text = "";
            txtDescription.Text = "";
            txtTitle.Text = "";
            txtSearch.Text = "";

        }

        private void frmCategories_Load(object sender, EventArgs e)
        {
            // display all added categories in Data Grid View. We need a Data Table for this

            DataTable dt = dal.Select();
            // here we have used select method to show data in data grid view

            dgvCategories.DataSource = dt;


        }

        private void dgvCategories_RowHeaderMouseClick(object sender, DataGridViewCellMouseEventArgs e)
        {
            // finding the row index of the row clicked on data grid view
            // we will create an integer and will 

            int RowIndex = e.RowIndex;

            txtCatgoryID.Text = dgvCategories.Rows[RowIndex].Cells[0].Value.ToString();
            txtTitle.Text = dgvCategories.Rows[RowIndex].Cells[1].Value.ToString();
            txtDescription.Text = dgvCategories.Rows[RowIndex].Cells[2].Value.ToString();
        }

        private void btnUpdate_Click(object sender, EventArgs e)
        {
            // Get the Values from the Category form

            c.id = int.Parse(txtCatgoryID.Text);
            c.title = txtTitle.Text;
            c.description = txtDescription.Text;
            c.added_date = DateTime.Now;

            // we will copy the code from ADD and Paste here

            // Getting ID in Added by Field. This will give name of laogged in use
            string loggedUser = frmLogin.loggedIn;

            // we need to convert this into user id

            userBLL usr = udal.GetIDFromUsername(loggedUser);
            // passing the id of logged in user in added by field
            c.added_by = usr.id;

            // Creating Boolean Variable to update Categories and Check
            bool success = dal.Update(c);

            // if the category is updated successfully, then the value of success will be true else it will be flase

            if(success==true)
            {
                // category updated successfully
                MessageBox.Show("Updated Successfully");
                Clear();
                // refresh data grid view
                DataTable dt = dal.Select();
                dgvCategories.DataSource = dt;


            }
            else
            {
                // not updated
                MessageBox.Show("Not Updated. try");
            }
        }

        private void btnDelete_Click(object sender, EventArgs e)
        {
            // Get the ID of the category which we want to delete

            c.id = int.Parse(txtCatgoryID.Text);

            // Creating Boolean Variable to Delete the Category
            bool success = dal.Delete(c);

            // If the category is deleted successfully then value will be true

            if(success==true)
            {
                // category deleted successfully
                MessageBox.Show("Deleted Successfully");
                Clear();
                // refresh data grid view
                DataTable dt = dal.Select();
                dgvCategories.DataSource = dt;

            }
            else
            {
                // category not deleted
                MessageBox.Show("Not Deleted. try again");
            }
        }

        private void txtSearch_TabIndexChanged(object sender, EventArgs e)
        {
            // Get the keywords first
            string keywords = txtSearch.Text;

            // Based on this keyword, we will display in our data grid view

            if(keywords!=null)
            {
                // use search method to display categories
                DataTable dt = dal.Search(keywords);
                dgvCategories.DataSource = dt;
            }
            else
            {
                // use select method to display all categories
                DataTable dt = dal.Select();
                dgvCategories.DataSource = dt;
            }


        }

        private void txtSearch_TextChanged(object sender, EventArgs e)
        {
            // Get the keywords first
            string keywords = txtSearch.Text;

            // Based on this keyword, we will display in our data grid view

            if (keywords != null)
            {
                // use search method to display categories
                DataTable dt = dal.Search(keywords);
                dgvCategories.DataSource = dt;
            }
            else
            {
                // use select method to display all categories
                DataTable dt = dal.Select();
                dgvCategories.DataSource = dt;
            }
        }
    }
}
