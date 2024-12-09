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
            this.comboBoxBrand = new System.Windows.Forms.ComboBox();
            this.comboBoxModel = new System.Windows.Forms.ComboBox();
            this.comboBoxVersion = new System.Windows.Forms.ComboBox();
            this.comboBoxPartSelect = new System.Windows.Forms.ComboBox();
            this.listBoxSelectedParts = new System.Windows.Forms.ListBox();
            this.buttonRemoveProduct = new System.Windows.Forms.Button();
            this.buttonDetailsForProduct = new System.Windows.Forms.Button();
            this.buttonAddProduct = new System.Windows.Forms.Button();
            this.textBoxSearchForParts = new System.Windows.Forms.TextBox();
            this.buttonSearch = new System.Windows.Forms.Button();
            this.buttonEdit = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // comboBoxBrand
            // 
            this.comboBoxBrand.FormattingEnabled = true;
            this.comboBoxBrand.Location = new System.Drawing.Point(12, 63);
            this.comboBoxBrand.Name = "comboBoxBrand";
            this.comboBoxBrand.Size = new System.Drawing.Size(102, 24);
            this.comboBoxBrand.TabIndex = 1;
            this.comboBoxBrand.Text = "Mærke";
            // 
            // comboBoxModel
            // 
            this.comboBoxModel.FormattingEnabled = true;
            this.comboBoxModel.Location = new System.Drawing.Point(120, 63);
            this.comboBoxModel.Name = "comboBoxModel";
            this.comboBoxModel.Size = new System.Drawing.Size(102, 24);
            this.comboBoxModel.TabIndex = 2;
            this.comboBoxModel.Text = "Model";
            // 
            // comboBoxVersion
            // 
            this.comboBoxVersion.FormattingEnabled = true;
            this.comboBoxVersion.Location = new System.Drawing.Point(228, 63);
            this.comboBoxVersion.Name = "comboBoxVersion";
            this.comboBoxVersion.Size = new System.Drawing.Size(102, 24);
            this.comboBoxVersion.TabIndex = 3;
            this.comboBoxVersion.Text = "Version";
            // 
            // comboBoxPartSelect
            // 
            this.comboBoxPartSelect.FormattingEnabled = true;
            this.comboBoxPartSelect.Location = new System.Drawing.Point(336, 63);
            this.comboBoxPartSelect.Name = "comboBoxPartSelect";
            this.comboBoxPartSelect.Size = new System.Drawing.Size(101, 24);
            this.comboBoxPartSelect.TabIndex = 4;
            this.comboBoxPartSelect.Text = "Reservedel";
            // 
            // listBoxSelectedParts
            // 
            this.listBoxSelectedParts.FormattingEnabled = true;
            this.listBoxSelectedParts.ItemHeight = 16;
            this.listBoxSelectedParts.Location = new System.Drawing.Point(12, 93);
            this.listBoxSelectedParts.Name = "listBoxSelectedParts";
            this.listBoxSelectedParts.Size = new System.Drawing.Size(425, 356);
            this.listBoxSelectedParts.TabIndex = 5;
            // 
            // buttonRemoveProduct
            // 
            this.buttonRemoveProduct.Location = new System.Drawing.Point(120, 460);
            this.buttonRemoveProduct.Name = "buttonRemoveProduct";
            this.buttonRemoveProduct.Size = new System.Drawing.Size(102, 23);
            this.buttonRemoveProduct.TabIndex = 7;
            this.buttonRemoveProduct.Text = "Fjern";
            this.buttonRemoveProduct.UseVisualStyleBackColor = true;
            this.buttonRemoveProduct.Click += new System.EventHandler(this.buttonRemoveProduct_Click);
            // 
            // buttonDetailsForProduct
            // 
            this.buttonDetailsForProduct.Location = new System.Drawing.Point(336, 460);
            this.buttonDetailsForProduct.Name = "buttonDetailsForProduct";
            this.buttonDetailsForProduct.Size = new System.Drawing.Size(101, 23);
            this.buttonDetailsForProduct.TabIndex = 11;
            this.buttonDetailsForProduct.Text = "Detaljer";
            this.buttonDetailsForProduct.UseVisualStyleBackColor = true;
            // 
            // buttonAddProduct
            // 
            this.buttonAddProduct.Location = new System.Drawing.Point(12, 460);
            this.buttonAddProduct.Name = "buttonAddProduct";
            this.buttonAddProduct.Size = new System.Drawing.Size(102, 23);
            this.buttonAddProduct.TabIndex = 12;
            this.buttonAddProduct.Text = "Tilføj";
            this.buttonAddProduct.UseVisualStyleBackColor = true;
            // 
            // textBoxSearchForParts
            // 
            this.textBoxSearchForParts.Location = new System.Drawing.Point(12, 12);
            this.textBoxSearchForParts.Name = "textBoxSearchForParts";
            this.textBoxSearchForParts.Size = new System.Drawing.Size(162, 22);
            this.textBoxSearchForParts.TabIndex = 13;
            this.textBoxSearchForParts.Text = "Søg";
            // 
            // buttonSearch
            // 
            this.buttonSearch.Location = new System.Drawing.Point(180, 12);
            this.buttonSearch.Name = "buttonSearch";
            this.buttonSearch.Size = new System.Drawing.Size(75, 23);
            this.buttonSearch.TabIndex = 14;
            this.buttonSearch.Text = "Søg";
            this.buttonSearch.UseVisualStyleBackColor = true;
            // 
            // buttonEdit
            // 
            this.buttonEdit.Location = new System.Drawing.Point(228, 460);
            this.buttonEdit.Name = "buttonEdit";
            this.buttonEdit.Size = new System.Drawing.Size(102, 23);
            this.buttonEdit.TabIndex = 15;
            this.buttonEdit.Text = "Rediger";
            this.buttonEdit.UseVisualStyleBackColor = true;
            // 
            // DesktopMainPage
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(452, 495);
            this.Controls.Add(this.buttonEdit);
            this.Controls.Add(this.buttonSearch);
            this.Controls.Add(this.textBoxSearchForParts);
            this.Controls.Add(this.buttonAddProduct);
            this.Controls.Add(this.buttonDetailsForProduct);
            this.Controls.Add(this.buttonRemoveProduct);
            this.Controls.Add(this.listBoxSelectedParts);
            this.Controls.Add(this.comboBoxPartSelect);
            this.Controls.Add(this.comboBoxVersion);
            this.Controls.Add(this.comboBoxModel);
            this.Controls.Add(this.comboBoxBrand);
            this.Name = "DesktopMainPage";
            this.Text = "HuggerRiget Home";
            this.Load += new System.EventHandler(this.DesktopMainPage_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private System.Windows.Forms.ComboBox comboBoxBrand;
        private System.Windows.Forms.ComboBox comboBoxModel;
        private System.Windows.Forms.ComboBox comboBoxVersion;
        private System.Windows.Forms.ComboBox comboBoxPartSelect;
        private System.Windows.Forms.ListBox listBoxSelectedParts;
        
        private System.Windows.Forms.Button buttonRemoveProduct;
        private System.Windows.Forms.Button buttonDetailsForProduct;
        private System.Windows.Forms.Button buttonAddProduct;
        private System.Windows.Forms.TextBox textBoxSearchForParts;
        private System.Windows.Forms.Button buttonSearch;
        private System.Windows.Forms.Button buttonEdit;
    }
}