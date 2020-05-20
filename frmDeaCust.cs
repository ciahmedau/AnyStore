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
    public partial class frmDeaCust : Form
    {
        public frmDeaCust()
        {
            InitializeComponent();
        }

        private void pictureBoxClose_Click(object sender, EventArgs e)
        {
            this.Hide();
        }

        // Here we will create Objects reference to our BLL and DAL like in our products frm load event
       // productsBLL p = new productsBLL();
        //productsDAL pdal = new productsDAL();
        // also create an object for user DAL
       // userDAL udal = new userDAL();

        DeaCustBLL dc = new DeaCustBLL();
        DeaCustDAL dcDal = new DeaCustDAL();
        userDAL uDal = new userDAL();
        private void btnAdd_Click(object sender, EventArgs e)
        {
            // Get the values from all Text boxes and store in BLL
            dc.type = cmbDeaCust.Text;
            dc.name = txtName.Text;
            dc.email = txtEmail.Text;
            dc.contact = txtContact.Text;
            dc.address = txtAddress.Text;
            dc.added_date = DateTime.Now;

            // Getting ID of Logged In User and Passing its value in Dealer or Customer Module
            // we will get user name who is logged in

            string loggedUsr = frmLogin.loggedIn;

            userBLL usr = uDal.GetIDFromUsername(loggedUsr);
            // we will pass this value
            dc.added_by = usr.id;

            // create Boolean variable to check if the Dealer is added successfully or Not

            bool success = dcDal.Insert(dc);

            // if the product is added successfully then the value of success will be true or will be false.

            if (success == true)
            {

                // product inserted successfully
                MessageBox.Show("Dealer or Customer Inserted Successfully");

                // we need to clear Text Boxes. we will write a method for this.
                // Calling the Clear Method
                Clear();

                // Refresh the Data Grid View as well.For that we wil lcreate a data Table.
                DataTable dt = dcDal.Select();

                dgvDeaCust.DataSource = dt;

            }
            else
            {
                // Falied to Insert
                MessageBox.Show("Product NOT Inserted Successfully");
            }

        }

        public void Clear()
        {
            txtDeaCustID.Text = "";
            txtName.Text = "";
            txtEmail.Text = "";
            txtContact.Text = "";
            txtAddress.Text = "";
            txtSearch.Text = "";



        }

        private void frmDeaCust_Load(object sender, EventArgs e)
        {
            // Refresh the Data Grid View as well.For that we wil lcreate a data Table.
            DataTable dt = dcDal.Select();
            dgvDeaCust.DataSource = dt;
        }

        private void dgvDeaCust_RowHeaderMouseClick(object sender, DataGridViewCellMouseEventArgs e)
        {
            // Create Integer variable to know which product was clicked

            int rowIndex = e.RowIndex;

            // Display value on respective text Boxes

            txtDeaCustID.Text = dgvDeaCust.Rows[rowIndex].Cells[0].Value.ToString();
            cmbDeaCust.Text = dgvDeaCust.Rows[rowIndex].Cells[1].Value.ToString();
            txtName.Text = dgvDeaCust.Rows[rowIndex].Cells[2].Value.ToString();
            txtEmail.Text = dgvDeaCust.Rows[rowIndex].Cells[3].Value.ToString();
            txtContact.Text = dgvDeaCust.Rows[rowIndex].Cells[4].Value.ToString();
            txtAddress.Text = dgvDeaCust.Rows[rowIndex].Cells[5].Value.ToString();
        }

        private void btnUpdate_Click(object sender, EventArgs e)
        {
            // Get the values from UI of Product form

            dc.id = int.Parse(txtDeaCustID.Text);
            dc.type =cmbDeaCust.Text;
            dc.name = txtName.Text;
            dc.email = txtEmail.Text;
            dc.contact = txtContact.Text;
            dc.address = txtAddress.Text;
            dc.added_date = DateTime.Now;
             
            // Getting User name of Logged in User
            // we will get user name who is logged in

            string loggedUsr = frmLogin.loggedIn;

            userBLL usr = uDal.GetIDFromUsername(loggedUsr);

            dc.added_by = usr.id;

            // createa boolean variable to check if the product is updated or not

            bool success = dcDal.Update(dc);

            // If the product is updated successfully then the value of success will be true

            if (success == true)
            {
                //Product added successfully
                MessageBox.Show("Dealer or Customer Updated Successfully");

                // we need to clear Text Boxes. we will write a method for this.
                // Calling the Clear Method
                Clear();

                // Refresh the Data Grid View as well.For that we wil lcreate a data Table.
                DataTable dt = dcDal.Select();

                dgvDeaCust.DataSource = dt;
            }
            else
            {
                MessageBox.Show("Dealer or Customer Not Updated Successfully");
            }

        }

        private void btnDelete_Click(object sender, EventArgs e)
        {
            // Get the ID of the Product to be deleted

            dc.id = int.Parse(txtDeaCustID.Text);

            // Create a bool variable to check if the data is deleted

            bool success = dcDal.Delete(dc);

            // if product is deleted successfully then the value of success will be true else it will be flase

            if (success == true)
            {
                //Product Deleted successfully
                MessageBox.Show("Product Deleted Successfully");

                // we need to clear Text Boxes. we will write a method for this.
                // Calling the Clear Method
                Clear();

                // Refresh the Data Grid View as well.For that we wil lcreate a data Table.
                DataTable dt = dcDal.Select();

                dgvDeaCust.DataSource = dt;
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
            if (keywords != null)
            {
                // then we will search the products. We Need Data Table for that
                DataTable dt = dcDal.Search(keywords);
                dgvDeaCust.DataSource = dt;

            }
            else
            {
                // otherwise we will display products
                DataTable dt = dcDal.Select();
                dgvDeaCust.DataSource = dt;

            }
        }
    }
    
}
