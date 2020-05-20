using AnyStore.UI;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace AnyStore
{
    public partial class frmUserDashboard : Form
    {
        public frmUserDashboard()
        {
            InitializeComponent();
        }

        // Here we will write a method to show label of purchase and Sale (lblTop)
        // set the public static method to specify whether the form is purchase or sale
        public static string transactionType;


        private void frmUserDashboard_FormClosed(object sender, FormClosedEventArgs e)
        {
            frmLogin login = new frmLogin();
            login.Show();
            this.Hide();
        }

        private void frmUserDashboard_Load(object sender, EventArgs e)
        {
            lblLoggedInUser.Text = frmLogin.loggedIn;
        }

        private void dealerAndCustomerToolStripMenuItem_Click(object sender, EventArgs e)
        {
            frmDeaCust DeaCust = new frmDeaCust();
            DeaCust.Show();
        
    }

        private void purchasesToolStripMenuItem_Click(object sender, EventArgs e)
        {
            // here we will set the value of transactionType here
            transactionType = "Purchase";


            frmPurchaseAndSales purchase = new frmPurchaseAndSales();
            purchase.Show();

            

            //transactionType = "Purchase";
        }

        private void salesFormsToolStripMenuItem_Click(object sender, EventArgs e)
        {

            // here we will set the value of transactionType here

            transactionType = "Sales";

            frmPurchaseAndSales sales = new frmPurchaseAndSales();
            sales.Show();

            


        }

        private void inventoryToolStripMenuItem_Click(object sender, EventArgs e)
        {
            frmInventory inventory = new frmInventory();
            inventory.Show();
        }
    }
}
