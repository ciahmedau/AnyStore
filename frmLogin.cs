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
    public partial class frmLogin : Form
    {
        public frmLogin()
        {
            InitializeComponent();
        }
        // we will refer and create two references to our login BLL and login DLL

        loginBLL l = new loginBLL();
        loginDAL dal = new loginDAL();

        // Here we will create a static method to Display Logged In User.
        public static string loggedIn;


        private void pboxClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void btnLogin_Click(object sender, EventArgs e)
        {
            l.username = txtUserName.Text.Trim();
            l.password = txtPassword.Text.Trim();
            l.user_type = cmbUserType.Text.Trim();

            // checking the login credentials

            bool success = dal.loginCheck(l);

            if(success==true)
            {
                //loging Successful
                MessageBox.Show("Login Successful");

                // here we will refer that logged in User. we need to display this in admin dashboard also.
                loggedIn = l.username;


                // Need to open respective form
                switch(l.user_type)
                {
                    case "Admin":
                        // this Admin should be Same spelling as Admin in Our Design of Combo Box
                        {
                            // Display Admin Dashboard
                            frmAdminDashboard admin = new frmAdminDashboard();
                            admin.Show();
                            this.Hide();

                        }
                        break;
                    case "User":
                        {
                            // Display User Dashboard
                            frmUserDashboard user = new frmUserDashboard();
                            user.Show();
                            this.Hide();
                        }
                        break;

                    default:
                        {
                            //Display an error message
                            MessageBox.Show("Invalid User Type. Neither Admin Nor User");

                        }
                        break;
                }
               

               
            }
            else
            {
                //login Failed
                MessageBox.Show("Login Failed and Not-Successful. Please Try Again");
            }
        }
    }
}
