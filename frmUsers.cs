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
    public partial class frmUsers : Form
    {
        public frmUsers()
        {
            InitializeComponent();
        }

        // here we will add few references with out BLL class

         userBLL u = new userBLL();
         userDAL dal = new userDAL();

        private void pictureBoxClose_Click(object sender, EventArgs e)
        {
            MessageBox.Show(" Do U Want to Close User Form");
            this.Close();
        }

        private void btnAdd_Click(object sender, EventArgs e)
        {
           
            
            // Getting Data from UI
            u.first_name = txtFirstName.Text;
            u.last_name = txtLastName.Text;
            u.email = txtEmail.Text;
            u.username = txtUserName.Text;
            u.password = txtPassword.Text;
            u.contact = txtContact.Text;
            u.address = txtAddress.Text;
            u.gender = cmbBoxGender.Text;
            u.user_type =cmbBoxUserType.Text;
            u.added_date = DateTime.Now;

            // Getting Username of the logged in user
            string loggedUser = frmLogin.loggedIn;
            userBLL usr = dal.GetIDFromUsername(loggedUser);
            u.added_by = usr.id;

            // Inserting Data into Database. we will declare one variable success which will take data to DAL
            bool success = dal.Inset(u);

            // now we wil ltest our condition that if data is successfully inserted, then good , otherwise it will be false

            if(success==true)
            {
                // Data Successfully inserted then the value will be true else will be false
                MessageBox.Show("User successfully Created");
                
                // Here we will call clear method                 
                clear();
            }
            else
            {
                // falied
                MessageBox.Show("Falied to add new user");
            }

            // Refreshing Data Grid View.we will create a temporary storage data table to store data.
              DataTable dt = dal.Select();
            // this will refer the data grid view
            dgvUsers.DataSource = dt;


        }

        private void frmUsers_Load(object sender, EventArgs e)
        {
            // Refreshing Data Grid View.we will create a temporary storage data table to store data.
             DataTable dt = dal.Select();
            // this will refer the data grid view
            dgvUsers.DataSource = dt;
        }
        // we will write a method to clear the form

        private void clear()
        {
            txtUserID.Text = "";
            txtFirstName.Text = "";
            txtLastName.Text = "";
            txtEmail.Text = "";
            txtUserName.Text = "";
            txtPassword.Text = "";
            txtContact.Text = "";
            txtAddress.Text = "";
            cmbBoxGender.Text = "";
            cmbBoxUserType.Text = "";
            txtAddedBy.Text = "";


        }

        private void dgvUsers_RowHeaderMouseClick(object sender, DataGridViewCellMouseEventArgs e)
        {
            // Get the index of Particular Row

            int rowIndex = e.RowIndex;
            txtUserID.Text = dgvUsers.Rows[rowIndex].Cells[0].Value.ToString();
            txtFirstName.Text = dgvUsers.Rows[rowIndex].Cells[1].Value.ToString();
            txtLastName.Text = dgvUsers.Rows[rowIndex].Cells[2].Value.ToString();
            txtEmail.Text = dgvUsers.Rows[rowIndex].Cells[3].Value.ToString();
            txtUserName.Text = dgvUsers.Rows[rowIndex].Cells[4].Value.ToString();
            txtPassword.Text = dgvUsers.Rows[rowIndex].Cells[5].Value.ToString();
            txtContact.Text = dgvUsers.Rows[rowIndex].Cells[6].Value.ToString();
            txtAddress.Text = dgvUsers.Rows[rowIndex].Cells[7].Value.ToString();
            cmbBoxGender.Text = dgvUsers.Rows[rowIndex].Cells[8].Value.ToString();
            cmbBoxUserType.Text = dgvUsers.Rows[rowIndex].Cells[9].Value.ToString();
        }

        private void btnUpdate_Click(object sender, EventArgs e)
        {
            

            // Get the Values from User UI
            u.id = Convert.ToInt32(txtUserID.Text);
            u.first_name = txtFirstName.Text;
            u.last_name = txtLastName.Text;
            u.email = txtEmail.Text;
            u.username = txtUserName.Text;
            u.password = txtPassword.Text;
            u.contact = txtContact.Text;
            u.address = txtAddress.Text;
            u.gender = cmbBoxGender.Text;
            u.user_type = cmbBoxUserType.Text;
            u.added_date = DateTime.Now;
            
            // Get name of logged in user first
            string loggedUser = frmLogin.loggedIn;
            
            // we need to get id of user. we will create another Method in UserDAL.
            userBLL usr = dal.GetIDFromUsername(loggedUser);
            u.added_by = usr.id;

            // updating Data into Database
            bool success = dal.Update(u);

            // If data is successfully added, then success message will come
            if(success==true)
            {
                MessageBox.Show("Data Updated Successfully");
                clear();
            }
            else
            {
                MessageBox.Show("Not Updated ");
            }
            //Refresh Data Grid
            // Refreshing Data Grid View.we will create a temporary storage data table to store data.
            DataTable dt = dal.Select();
            // this will refer the data grid view
            dgvUsers.DataSource = dt;

        }

        private void btnDelete_Click(object sender, EventArgs e)
        {
            u.id = Convert.ToInt32(txtUserID.Text);
            bool success = dal.Delete(u);

            // If data is deleted then the success

            if(success==true)
            {
                MessageBox.Show("deleted Successfully");
                clear();
            }
            else
            {
                MessageBox.Show("Not Deleted");

            }
            //Refresh Data Grid
            // Refreshing Data Grid View.we will create a temporary storage data table to store data.
            DataTable dt = dal.Select();
            // this will refer the data grid view
            dgvUsers.DataSource = dt;

        }

        private void txtSearch_TextChanged(object sender, EventArgs e)
        {
            // Get keyword from Text Box
            string Keywords = txtSearch.Text;

            //check if keywords have value or not

            if(Keywords !=null)
            {
                // show user based on keywords
                DataTable dt = dal.Search(Keywords);
                dgvUsers.DataSource = dt;
            }
            else
            {
                // show all users from database
                DataTable dt = dal.Select();
                dgvUsers.DataSource = dt;
            }
        }
    }
}
