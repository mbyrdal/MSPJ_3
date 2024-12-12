namespace ClientDesktop
{
    partial class DesktopMainForm
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.buttonRemoveProduct = new System.Windows.Forms.Button();
            this.buttonAddProduct = new System.Windows.Forms.Button();
            this.textBoxSearchForParts = new System.Windows.Forms.TextBox();
            this.buttonSearch = new System.Windows.Forms.Button();
            this.buttonEdit = new System.Windows.Forms.Button();
            this.buttonFetchInventory = new System.Windows.Forms.Button();
            this.dataGridViewProductSearchResult = new System.Windows.Forms.DataGridView();
            this.Navn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.OEM = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Description = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Price = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Lager = new System.Windows.Forms.DataGridViewTextBoxColumn();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridViewProductSearchResult)).BeginInit();
            this.SuspendLayout();
            // 
            // buttonRemoveProduct
            // 
            this.buttonRemoveProduct.Location = new System.Drawing.Point(107, 341);
            this.buttonRemoveProduct.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.buttonRemoveProduct.Name = "buttonRemoveProduct";
            this.buttonRemoveProduct.Size = new System.Drawing.Size(76, 19);
            this.buttonRemoveProduct.TabIndex = 7;
            this.buttonRemoveProduct.Text = "Fjern";
            this.buttonRemoveProduct.UseVisualStyleBackColor = true;
            this.buttonRemoveProduct.Click += new System.EventHandler(this.buttonRemoveProduct_Click);
            // 
            // buttonAddProduct
            // 
            this.buttonAddProduct.Location = new System.Drawing.Point(11, 341);
            this.buttonAddProduct.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.buttonAddProduct.Name = "buttonAddProduct";
            this.buttonAddProduct.Size = new System.Drawing.Size(76, 19);
            this.buttonAddProduct.TabIndex = 12;
            this.buttonAddProduct.Text = "Tilføj";
            this.buttonAddProduct.UseVisualStyleBackColor = true;
            // 
            // textBoxSearchForParts
            // 
            this.textBoxSearchForParts.Location = new System.Drawing.Point(9, 10);
            this.textBoxSearchForParts.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.textBoxSearchForParts.Name = "textBoxSearchForParts";
            this.textBoxSearchForParts.Size = new System.Drawing.Size(122, 20);
            this.textBoxSearchForParts.TabIndex = 13;
            this.textBoxSearchForParts.Text = "Søg";
            this.textBoxSearchForParts.TextChanged += new System.EventHandler(this.textBoxSearchForParts_TextChanged);
            // 
            // buttonSearch
            // 
            this.buttonSearch.Location = new System.Drawing.Point(135, 10);
            this.buttonSearch.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.buttonSearch.Name = "buttonSearch";
            this.buttonSearch.Size = new System.Drawing.Size(56, 19);
            this.buttonSearch.TabIndex = 14;
            this.buttonSearch.Text = "Søg";
            this.buttonSearch.UseVisualStyleBackColor = true;
            this.buttonSearch.Click += new System.EventHandler(this.buttonSearch_Click);
            // 
            // buttonEdit
            // 
            this.buttonEdit.Location = new System.Drawing.Point(203, 341);
            this.buttonEdit.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.buttonEdit.Name = "buttonEdit";
            this.buttonEdit.Size = new System.Drawing.Size(76, 19);
            this.buttonEdit.TabIndex = 15;
            this.buttonEdit.Text = "Ændre";
            this.buttonEdit.UseVisualStyleBackColor = true;
            this.buttonEdit.Click += new System.EventHandler(this.buttonEdit_Click);
            // 
            // buttonFetchInventory
            // 
            this.buttonFetchInventory.Location = new System.Drawing.Point(252, 9);
            this.buttonFetchInventory.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.buttonFetchInventory.Name = "buttonFetchInventory";
            this.buttonFetchInventory.Size = new System.Drawing.Size(104, 19);
            this.buttonFetchInventory.TabIndex = 16;
            this.buttonFetchInventory.Text = "Hent Lager";
            this.buttonFetchInventory.UseVisualStyleBackColor = true;
            this.buttonFetchInventory.Click += new System.EventHandler(this.buttonFetchInventory_Click);
            // 
            // dataGridViewProductSearchResult
            // 
            this.dataGridViewProductSearchResult.AllowUserToOrderColumns = true;
            this.dataGridViewProductSearchResult.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridViewProductSearchResult.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.Navn,
            this.OEM,
            this.Description,
            this.Price,
            this.Lager});
            this.dataGridViewProductSearchResult.Cursor = System.Windows.Forms.Cursors.Default;
            this.dataGridViewProductSearchResult.Location = new System.Drawing.Point(11, 34);
            this.dataGridViewProductSearchResult.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.dataGridViewProductSearchResult.Name = "dataGridViewProductSearchResult";
            this.dataGridViewProductSearchResult.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.dataGridViewProductSearchResult.RowHeadersVisible = false;
            this.dataGridViewProductSearchResult.RowHeadersWidth = 51;
            this.dataGridViewProductSearchResult.RowTemplate.Height = 24;
            this.dataGridViewProductSearchResult.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dataGridViewProductSearchResult.Size = new System.Drawing.Size(508, 292);
            this.dataGridViewProductSearchResult.TabIndex = 17;
            this.dataGridViewProductSearchResult.CellContentClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dataGridViewProductSearchResult_CellContentClick);
            // 
            // Navn
            // 
            this.Navn.DataPropertyName = "Name";
            this.Navn.HeaderText = "Navn";
            this.Navn.MinimumWidth = 6;
            this.Navn.Name = "Navn";
            this.Navn.Width = 125;
            // 
            // OEM
            // 
            this.OEM.DataPropertyName = "OEM";
            this.OEM.HeaderText = "OEM";
            this.OEM.MinimumWidth = 6;
            this.OEM.Name = "OEM";
            this.OEM.Width = 93;
            // 
            // Description
            // 
            this.Description.DataPropertyName = "Description";
            this.Description.HeaderText = "Beskrivelse";
            this.Description.MinimumWidth = 6;
            this.Description.Name = "Description";
            this.Description.Width = 93;
            // 
            // Price
            // 
            this.Price.DataPropertyName = "Price";
            this.Price.HeaderText = "Pris";
            this.Price.MinimumWidth = 6;
            this.Price.Name = "Price";
            this.Price.Width = 92;
            // 
            // Lager
            // 
            this.Lager.DataPropertyName = "Availability";
            this.Lager.HeaderText = "Lager";
            this.Lager.MinimumWidth = 6;
            this.Lager.Name = "Lager";
            this.Lager.Width = 93;
            // 
            // DesktopMainForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.AutoSize = true;
            this.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.ClientSize = new System.Drawing.Size(530, 395);
            this.Controls.Add(this.dataGridViewProductSearchResult);
            this.Controls.Add(this.buttonFetchInventory);
            this.Controls.Add(this.buttonEdit);
            this.Controls.Add(this.buttonSearch);
            this.Controls.Add(this.textBoxSearchForParts);
            this.Controls.Add(this.buttonAddProduct);
            this.Controls.Add(this.buttonRemoveProduct);
            this.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.Name = "DesktopMainForm";
            this.Text = "HuggerRiget Home";
            this.Load += new System.EventHandler(this.DesktopMainPage_Load);
            ((System.ComponentModel.ISupportInitialize)(this.dataGridViewProductSearchResult)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        
        private System.Windows.Forms.Button buttonRemoveProduct;
        private System.Windows.Forms.Button buttonAddProduct;
        private System.Windows.Forms.TextBox textBoxSearchForParts;
        private System.Windows.Forms.Button buttonSearch;
        private System.Windows.Forms.Button buttonEdit;
        private System.Windows.Forms.Button buttonFetchInventory;
        private System.Windows.Forms.DataGridView dataGridViewProductSearchResult;
        private System.Windows.Forms.DataGridViewTextBoxColumn Navn;
        private System.Windows.Forms.DataGridViewTextBoxColumn OEM;
        private System.Windows.Forms.DataGridViewTextBoxColumn Description;
        private System.Windows.Forms.DataGridViewTextBoxColumn Price;
        private System.Windows.Forms.DataGridViewTextBoxColumn Lager;
    }
}