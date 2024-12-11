using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ClientDesktop
{
    

    public partial class DesktopMainForm : Form
    {
        private ApiClient _apiClient;

        public DesktopMainForm()
        {
            InitializeComponent();
            _apiClient = new ApiClient("https://localhost:7134/api/products"); // Use your API's base URL
        }


        private void DesktopMainPage_Load(object sender, EventArgs e)
        {

        }

        private void buttonRemoveProduct_Click(object sender, EventArgs e)
        {

        }

        private async void buttonFetchInventory_Click(object sender, EventArgs e)
        {
            try
            {
                var products = await _apiClient.GetAllProductsAsync(); // Await the API call
                if (products != null && products.Count > 0)
                {
                    // Transform the data to only include the desired fields
                    var filteredProducts = products.Select(product => new
                    {
                        product.OEM,
                        Description = product.ItemDescription, // Adjust property names based on your model
                        product.Price,
                        Availability = product.ItemAvailable ? "Available" : "Out of Stock"
                    }).ToList();

                    // Bind the filtered data to the DataGridView
                    dataGridViewProductSearchResult.DataSource = filteredProducts;
                }
                else
                {
                    MessageBox.Show("No products found.");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error loading products: {ex.Message}");
            }
        }


        private void listBoxSelectedParts_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private async void buttonSearch_Click(object sender, EventArgs e)
        {
            string oem = textBoxSearchForParts.Text;
            try
            {
                var product = await _apiClient.GetProductByOEMAsync(oem); // Await the API call
                if (product != null)
                {
                    // Bind only the desired fields to the DataGridView
                    dataGridViewProductSearchResult.DataSource = new List<object>
            {
                new
                {
                    product.OEM,
                    product.ItemDescription,
                    product.Price,
                    Availability = product.ItemAvailable ? "Available" : "Out of Stock"
                }
            };
                }
                else
                {
                    MessageBox.Show($"No product found with OEM: {oem}");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error fetching product: {ex.Message}");
            }
        }




        private void textBoxSearchForParts_TextChanged(object sender, EventArgs e)
        {

        }

        private void dataGridViewProductSearchResult_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }
    }
}
