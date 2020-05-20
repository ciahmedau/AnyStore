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
using System.Transactions;
using System.Windows.Forms;

namespace AnyStore.UI
{
    public partial class frmPurchaseAndSales : Form
    {
        public frmPurchaseAndSales()
        {
            InitializeComponent();
        }

       

        private void pictureBoxClose_Click(object sender, EventArgs e)
        {
            this.Hide();
        }

        // create a reference method 

        DeaCustDAL dcDAL = new DeaCustDAL();

        productsDAL pDAL = new productsDAL();

        userDAL uDAL = new userDAL();

        transactionDAL tDAL = new transactionDAL();

        transactionDetailDAL tdDAL = new transactionDetailDAL();

        // Create Data Table Here. This way we will be able to see all products in our Data Grid view
        DataTable transactionDT = new DataTable();

        private void frmPurchaseAndSales_Load(object sender, EventArgs e)
        {
            // Get the Transaction type value from user dashboard

            string type = frmUserDashboard.transactionType;

            // set the value on lblTop

            lblTop.Text = type;

            // we need to specify the columns for Transaction data table

            transactionDT.Columns.Add("productName");
            transactionDT.Columns.Add("Rate");
            transactionDT.Columns.Add("Quantity");
            transactionDT.Columns.Add("Total");
        }

        private void txtSearch_TextChanged(object sender, EventArgs e)
        {
            // Get the keyword from Text box

            string keyword = txtSearch.Text;

            if (keyword == "")
            {
                txtName.Text = "";
                txtEmail.Text = "";
                txtContact.Text = "";
                txtAddress.Text = "";
                return;

            }
            // write the code to get details and set the value on text boxes

            DeaCustBLL dc = dcDAL.SearchDealerCustomerTransaction(keyword);

            // Transfer the values from DeaCustBLL to our txtBoxes.

            txtName.Text = dc.name;
            txtEmail.Text = dc.email;
            txtContact.Text = dc.contact;
            txtAddress.Text = dc.address;

        }

        private void btnAdd_Click(object sender, EventArgs e)
        {
            //Get product Name , Rate and Qty Customer want to buy

            string productName = txtProductName.Text;
            decimal Rate = decimal.Parse(txtRate.Text);
            decimal Qty = decimal.Parse(txtQty.Text);
            
            // Total Qty Calculation

            decimal Total = Rate * Qty;

            // Show in the Values from Text Box

            decimal subTotal = decimal.Parse(txtSubTotal.Text);
            subTotal = subTotal + Total;

            // we need to add these in Data Grid View

            // Check whether the product is selected or Not

            if (productName == "")
            {
                // Display error Message
                MessageBox.Show("Select the Product First");

            }
            else
            {
                // add product in the DGV. We need to add Data Table at the Top
                transactionDT.Rows.Add(productName, Rate, Qty, Total);

                //show in Data Grid View
                dgvAddedProducts.DataSource = transactionDT;

                // Display the sub total in Text Box
                txtSubTotal.Text = subTotal.ToString();

                // Clear the text boxes

                txtSearchProduct.Text = "";
                txtProductName.Text = "";
                txtInventory.Text = "0.00";
                txtRate.Text = "0.00";
                txtQty.Text = "0.00";


            }

        }

        private void txtSearchProduct_TextChanged(object sender, EventArgs e)
        {
            // First Step is to get keyword from product search text box

            string keyword = txtSearchProduct.Text;

            // check if we have value of txt search product or not

            if (keyword == "")
            {
                txtProductName.Text = "";
                txtInventory.Text = "";
                txtRate.Text = "";
                txtQty.Text = "";
                return;
            }

            //search the product and display on respective text boxes

            productsBLL p = pDAL.GetProductsForTransaction(keyword);

            // set the values on text boxes based on the object p

            txtProductName.Text = p.name;
            txtInventory.Text = p.qty.ToString();
            txtRate.Text = p.rate.ToString();
            txtInventory.Text = p.qty.ToString();

        }

        private void txtDiscount_TextChanged(object sender, EventArgs e)
        {
            // Calculate Discount. Get the Values from discount text box

            string vlaue = txtDiscount.Text;

            if (vlaue == "")
            {
                MessageBox.Show("Please add discount first");
            }
            else
            {
                // Get the dicount in decimal value

                decimal subTotal = decimal.Parse(txtSubTotal.Text);
                decimal discount = decimal.Parse(txtDiscount.Text);


                // calculate the grand total based on the discount

                decimal grandTotal = ((100 - discount) / 100) * subTotal;

                //Disolay Grand Total in Text Box

                txtGrandTotal.Text = grandTotal.ToString();


            }
        }

        private void txtVat_TextChanged(object sender, EventArgs e)
        {
            // to check whether grand total have some value or not.If it is has no value then calculate discount first

            string check = txtGrandTotal.Text;

            if (check == "")
            {
                // Display the error message to calculate discount
                MessageBox.Show("Calculate the discount and set grand total first");

            }
            else
            {
                //calculate VAT. Getting the VAT % first. Get the grand total first

                decimal previousGT = decimal.Parse(txtGrandTotal.Text);
                decimal vat = decimal.Parse(txtVat.Text);
                decimal grandTotalwithVAT = ((100 + vat) / 100) * previousGT;

                // we will display the new grand total with vat

                txtGrandTotal.Text = grandTotalwithVAT.ToString();

            }


        }

        private void txtPaidAmount_TextChanged(object sender, EventArgs e)
        {
            // Get the paid amount and grand total

            decimal grandTotal = decimal.Parse(txtGrandTotal.Text);
            decimal paidAmount = decimal.Parse(txtPaidAmount.Text);

            decimal returnAmount = paidAmount - grandTotal;
            // Display the return amount as well

            txtReturnAmount.Text = returnAmount.ToString();
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            //Get the values from form purchase Sales form First
            // we will create an objetct of Transactions Class

            transactionsBLL transaction = new transactionsBLL();

            transaction.type = lblTop.Text;

            // Get the ID of Dealer or Customer Here
            // Lets get name of the dealer or customer first

            string deaCustName = txtName.Text;
            
            // here we have first written method in DeaCustDAL and now forwarding as 

            DeaCustBLL dc = dcDAL.GetDeaCustIDFromName(deaCustName);

            //Now we will refer the transaction

            transaction.dea_cust_id = dc.id;

            transaction.grandTotal = Math.Round(decimal.Parse(txtGrandTotal.Text), 2);
            transaction.transaction_date = DateTime.Now;
            transaction.tax = decimal.Parse(txtVat.Text);
            transaction.discount = decimal.Parse(txtDiscount.Text);

            // Get the user name,for added by, get the user name 

            string username = frmLogin.loggedIn;
            
            userBLL u = uDAL.GetIDFromUsername(username);
            
            // we need to passthistransaction
            transaction.added_by = u.id;
            
            transaction.transactionDetails = transactionDT;
            
            //Lets create a Boolean variable and set its value to False

            bool success = false;

            //Actual Code to Insert Transaction and Transaction Details

            using (TransactionScope scope = new TransactionScope())
            {
                int transactionID = 1;

                //Create a Boolean value and Insert Transaction
                bool w = tDAL.Insert_Transcation(transaction, out transactionID);

                //Use for loop to insert transaction details

                for (int i = 0; i < transactionDT.Rows.Count; i++)
                {
                    // create all details of products

                    transactionDetailBLL transactionDetail = new transactionDetailBLL();

                    // Get the product name and convert it to ID

                    string productName = transactionDT.Rows[i][0].ToString();
                    
                    productsBLL p = pDAL.GetproductIDFromName(ProductName);

                    transactionDetail.product_id = p.id;
                    transactionDetail.rate = decimal.Parse(transactionDT.Rows[i][1].ToString());
                    transactionDetail.qty = decimal.Parse(transactionDT.Rows[i][2].ToString());
                    transactionDetail.total = Math.Round(decimal.Parse(transactionDT.Rows[i][3].ToString()), 2);
                    transactionDetail.dea_cust_id = dc.id;
                    transactionDetail.added_date = DateTime.Now;
                    transactionDetail.added_by = u.id;

                    // Here we will increase or decrease the quantity based on purchase or sales
                    // we have created Methods to Increase and decrease Qty.
                    // Here Increase or Decrease Product Qty based on Purchase or Sales
                    // we wil lspecify it is Sales or Purchase. we wil lcreate a string variable

                    string transactionType = lblTop.Text;

                    //Lets Check we are on Purchase or Sales

                    bool x = false;

                    if (transactionType == "Purchase")
                    {
                        //Increase the product
                         x = pDAL.IncreaseProduct(transactionDetail.product_id, transactionDetail.qty);

                    }
                    else if (transactionType == "Sales")
                    {
                        //Decrease the Product Qty
                        x = pDAL.DecreaseProduct(transactionDetail.product_id, transactionDetail.qty);

                    }

                                        
                    //Insert Transaction Details Inside the Database
                    bool y = tdDAL.InsertTranscationDetail(transactionDetail);
                    success = w && y && x;
                                
                    
                }
                if (success == true)
                {
                    // transaction complete
                    scope.Complete();

                    MessageBox.Show("Transaction completed Successfully");
                    dgvAddedProducts.DataSource = null;
                    dgvAddedProducts.Rows.Clear();

                    txtSearch.Text = "";
                    txtName.Text = "";
                    txtEmail.Text = "";
                    txtContact.Text = "";
                    txtAddress.Text = "";

                    txtSearchProduct.Text = "";
                    txtProductName.Text = "";
                    txtInventory.Text = "0";
                    txtRate.Text = "0";
                    txtQty.Text = "0";

                    txtSubTotal.Text = "0";
                    txtDiscount.Text = "0";
                    txtVat.Text = "0";
                    txtGrandTotal.Text = "0";
                    txtPaidAmount.Text = "0";
                    txtReturnAmount.Text = "0";


                }
                else
                {
                    MessageBox.Show("This Transaction Has Not Been Completed and Failed");


                }

            }

        }

    

        
    }
}
