using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace DesktopClient.Views
{
    public partial class LoginForm : Form
    {
        private readonly CustomerControl _customerControl;

        public LoginForm()
        {
            InitializeComponent();
            _customerControl = new CustomerControl();
        }

        private void btnLogin_Click(object sender, EventArgs e)
        {
            if (_customerControl.Authenticate(txtUsername.Text, txtPassword.Text))
            {
                MessageBox.Show("Login successful!");
                var shoppingCartForm = new ShoppingCartForm();
                shoppingCartForm.Show();
                this.Hide();
            }
            else
            {
                lblError.Text = "Invalid username or password!";
            }
        }
    }
}
