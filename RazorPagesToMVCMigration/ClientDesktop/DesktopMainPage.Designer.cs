namespace ClientDesktop
{
    partial class DesktopMainPage
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
            this.tabPageShoppingCart = new System.Windows.Forms.TabPage();
            this.tabControlMainPage = new System.Windows.Forms.TabControl();
            this.tabPageOrderHistory = new System.Windows.Forms.TabPage();
            this.comboBoxBrand = new System.Windows.Forms.ComboBox();
            this.comboBoxModel = new System.Windows.Forms.ComboBox();
            this.comboBoxVersion = new System.Windows.Forms.ComboBox();
            this.comboBoxPartSelect = new System.Windows.Forms.ComboBox();
            this.listBoxSelectedParts = new System.Windows.Forms.ListBox();
            this.buttonRemoveFromCart = new System.Windows.Forms.Button();
            this.labelTotalPrice = new System.Windows.Forms.Label();
            this.buttonDetailsForProduct = new System.Windows.Forms.Button();
            this.buttonAddToCart = new System.Windows.Forms.Button();
            this.textBoxSearchForParts = new System.Windows.Forms.TextBox();
            this.buttonSearch = new System.Windows.Forms.Button();
            this.buttonOrder = new System.Windows.Forms.Button();
            this.tabControlMainPage.SuspendLayout();
            this.SuspendLayout();
            // 
            // tabPageShoppingCart
            // 
            this.tabPageShoppingCart.Location = new System.Drawing.Point(4, 25);
            this.tabPageShoppingCart.Name = "tabPageShoppingCart";
            this.tabPageShoppingCart.Padding = new System.Windows.Forms.Padding(3);
            this.tabPageShoppingCart.Size = new System.Drawing.Size(326, 357);
            this.tabPageShoppingCart.TabIndex = 1;
            this.tabPageShoppingCart.Text = "Indkøbskurv";
            this.tabPageShoppingCart.UseVisualStyleBackColor = true;
            // 
            // tabControlMainPage
            // 
            this.tabControlMainPage.Controls.Add(this.tabPageShoppingCart);
            this.tabControlMainPage.Controls.Add(this.tabPageOrderHistory);
            this.tabControlMainPage.Location = new System.Drawing.Point(454, 12);
            this.tabControlMainPage.Name = "tabControlMainPage";
            this.tabControlMainPage.SelectedIndex = 0;
            this.tabControlMainPage.Size = new System.Drawing.Size(334, 386);
            this.tabControlMainPage.TabIndex = 0;
            // 
            // tabPageOrderHistory
            // 
            this.tabPageOrderHistory.Location = new System.Drawing.Point(4, 25);
            this.tabPageOrderHistory.Name = "tabPageOrderHistory";
            this.tabPageOrderHistory.Padding = new System.Windows.Forms.Padding(3);
            this.tabPageOrderHistory.Size = new System.Drawing.Size(326, 357);
            this.tabPageOrderHistory.TabIndex = 2;
            this.tabPageOrderHistory.Text = "Ordre historik";
            this.tabPageOrderHistory.UseVisualStyleBackColor = true;
            // 
            // comboBoxBrand
            // 
            this.comboBoxBrand.FormattingEnabled = true;
            this.comboBoxBrand.Location = new System.Drawing.Point(12, 12);
            this.comboBoxBrand.Name = "comboBoxBrand";
            this.comboBoxBrand.Size = new System.Drawing.Size(102, 24);
            this.comboBoxBrand.TabIndex = 1;
            this.comboBoxBrand.Text = "Mærke";
            // 
            // comboBoxModel
            // 
            this.comboBoxModel.FormattingEnabled = true;
            this.comboBoxModel.Location = new System.Drawing.Point(120, 12);
            this.comboBoxModel.Name = "comboBoxModel";
            this.comboBoxModel.Size = new System.Drawing.Size(102, 24);
            this.comboBoxModel.TabIndex = 2;
            this.comboBoxModel.Text = "Model";
            // 
            // comboBoxVersion
            // 
            this.comboBoxVersion.FormattingEnabled = true;
            this.comboBoxVersion.Location = new System.Drawing.Point(228, 12);
            this.comboBoxVersion.Name = "comboBoxVersion";
            this.comboBoxVersion.Size = new System.Drawing.Size(102, 24);
            this.comboBoxVersion.TabIndex = 3;
            this.comboBoxVersion.Text = "Version";
            // 
            // comboBoxPartSelect
            // 
            this.comboBoxPartSelect.FormattingEnabled = true;
            this.comboBoxPartSelect.Location = new System.Drawing.Point(336, 12);
            this.comboBoxPartSelect.Name = "comboBoxPartSelect";
            this.comboBoxPartSelect.Size = new System.Drawing.Size(101, 24);
            this.comboBoxPartSelect.TabIndex = 4;
            this.comboBoxPartSelect.Text = "Reservedel";
            // 
            // listBoxSelectedParts
            // 
            this.listBoxSelectedParts.FormattingEnabled = true;
            this.listBoxSelectedParts.ItemHeight = 16;
            this.listBoxSelectedParts.Location = new System.Drawing.Point(12, 42);
            this.listBoxSelectedParts.Name = "listBoxSelectedParts";
            this.listBoxSelectedParts.Size = new System.Drawing.Size(425, 356);
            this.listBoxSelectedParts.TabIndex = 5;
            // 
            // buttonRemoveFromCart
            // 
            this.buttonRemoveFromCart.Location = new System.Drawing.Point(454, 411);
            this.buttonRemoveFromCart.Name = "buttonRemoveFromCart";
            this.buttonRemoveFromCart.Size = new System.Drawing.Size(76, 23);
            this.buttonRemoveFromCart.TabIndex = 7;
            this.buttonRemoveFromCart.Text = "Remove";
            this.buttonRemoveFromCart.UseVisualStyleBackColor = true;
            // 
            // labelTotalPrice
            // 
            this.labelTotalPrice.AutoSize = true;
            this.labelTotalPrice.Location = new System.Drawing.Point(617, 414);
            this.labelTotalPrice.Name = "labelTotalPrice";
            this.labelTotalPrice.Size = new System.Drawing.Size(120, 16);
            this.labelTotalPrice.TabIndex = 10;
            this.labelTotalPrice.Text = "Total: \"\"inkl. moms.";
            this.labelTotalPrice.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // buttonDetailsForProduct
            // 
            this.buttonDetailsForProduct.Location = new System.Drawing.Point(261, 410);
            this.buttonDetailsForProduct.Name = "buttonDetailsForProduct";
            this.buttonDetailsForProduct.Size = new System.Drawing.Size(85, 23);
            this.buttonDetailsForProduct.TabIndex = 11;
            this.buttonDetailsForProduct.Text = "Detaljer";
            this.buttonDetailsForProduct.UseVisualStyleBackColor = true;
            // 
            // buttonAddToCart
            // 
            this.buttonAddToCart.Location = new System.Drawing.Point(352, 411);
            this.buttonAddToCart.Name = "buttonAddToCart";
            this.buttonAddToCart.Size = new System.Drawing.Size(85, 23);
            this.buttonAddToCart.TabIndex = 12;
            this.buttonAddToCart.Text = "Tilføj til kurv";
            this.buttonAddToCart.UseVisualStyleBackColor = true;
            // 
            // textBoxSearchForParts
            // 
            this.textBoxSearchForParts.Location = new System.Drawing.Point(12, 411);
            this.textBoxSearchForParts.Name = "textBoxSearchForParts";
            this.textBoxSearchForParts.Size = new System.Drawing.Size(162, 22);
            this.textBoxSearchForParts.TabIndex = 13;
            this.textBoxSearchForParts.Text = "Søg";
            // 
            // buttonSearch
            // 
            this.buttonSearch.Location = new System.Drawing.Point(180, 410);
            this.buttonSearch.Name = "buttonSearch";
            this.buttonSearch.Size = new System.Drawing.Size(75, 23);
            this.buttonSearch.TabIndex = 14;
            this.buttonSearch.Text = "Søg";
            this.buttonSearch.UseVisualStyleBackColor = true;
            // 
            // buttonOrder
            // 
            this.buttonOrder.Location = new System.Drawing.Point(536, 411);
            this.buttonOrder.Name = "buttonOrder";
            this.buttonOrder.Size = new System.Drawing.Size(75, 23);
            this.buttonOrder.TabIndex = 15;
            this.buttonOrder.Text = "Bestil";
            this.buttonOrder.UseVisualStyleBackColor = true;
            // 
            // DesktopMainPage
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(800, 450);
            this.Controls.Add(this.buttonOrder);
            this.Controls.Add(this.buttonSearch);
            this.Controls.Add(this.textBoxSearchForParts);
            this.Controls.Add(this.buttonAddToCart);
            this.Controls.Add(this.buttonDetailsForProduct);
            this.Controls.Add(this.labelTotalPrice);
            this.Controls.Add(this.buttonRemoveFromCart);
            this.Controls.Add(this.listBoxSelectedParts);
            this.Controls.Add(this.comboBoxPartSelect);
            this.Controls.Add(this.comboBoxVersion);
            this.Controls.Add(this.comboBoxModel);
            this.Controls.Add(this.comboBoxBrand);
            this.Controls.Add(this.tabControlMainPage);
            this.Name = "DesktopMainPage";
            this.Text = "HuggerRiget Home";
            this.Load += new System.EventHandler(this.DesktopMainPage_Load);
            this.tabControlMainPage.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TabPage tabPageShoppingCart;
        private System.Windows.Forms.TabControl tabControlMainPage;
        private System.Windows.Forms.TabPage tabPageOrderHistory;
        private System.Windows.Forms.ComboBox comboBoxBrand;
        private System.Windows.Forms.ComboBox comboBoxModel;
        private System.Windows.Forms.ComboBox comboBoxVersion;
        private System.Windows.Forms.ComboBox comboBoxPartSelect;
        private System.Windows.Forms.ListBox listBoxSelectedParts;
        
        private System.Windows.Forms.Button buttonRemoveFromCart;
        
        private System.Windows.Forms.Label labelTotalPrice;
        private System.Windows.Forms.Button buttonDetailsForProduct;
        private System.Windows.Forms.Button buttonAddToCart;
        private System.Windows.Forms.TextBox textBoxSearchForParts;
        private System.Windows.Forms.Button buttonSearch;
        private System.Windows.Forms.Button buttonOrder;
    }
}