namespace WebBrowser
{
    partial class Browser
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
            this.components = new System.ComponentModel.Container();
            this.back_button = new System.Windows.Forms.Button();
            this.forward_button = new System.Windows.Forms.Button();
            this.refresh_button = new System.Windows.Forms.Button();
            this.home_button = new System.Windows.Forms.Button();
            this.html_box = new System.Windows.Forms.TextBox();
            this.url_box = new System.Windows.Forms.TextBox();
            this.navigate_button = new System.Windows.Forms.Button();
            this.menu_button = new System.Windows.Forms.Button();
            this.favourite_button = new System.Windows.Forms.Button();
            this.contextMenuStrip1 = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.favouritesToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.historyToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.contextMenuStrip1.SuspendLayout();
            this.SuspendLayout();
            // 
            // back_button
            // 
            this.back_button.Location = new System.Drawing.Point(31, 12);
            this.back_button.Name = "back_button";
            this.back_button.Size = new System.Drawing.Size(52, 21);
            this.back_button.TabIndex = 2;
            this.back_button.Text = "Back";
            this.back_button.UseVisualStyleBackColor = true;
            this.back_button.Click += new System.EventHandler(this.button1_Click);
            // 
            // forward_button
            // 
            this.forward_button.Location = new System.Drawing.Point(89, 12);
            this.forward_button.Name = "forward_button";
            this.forward_button.Size = new System.Drawing.Size(59, 21);
            this.forward_button.TabIndex = 3;
            this.forward_button.Text = "Forward";
            this.forward_button.UseVisualStyleBackColor = true;
            this.forward_button.Click += new System.EventHandler(this.forward_button_Click);
            // 
            // refresh_button
            // 
            this.refresh_button.Location = new System.Drawing.Point(154, 12);
            this.refresh_button.Name = "refresh_button";
            this.refresh_button.Size = new System.Drawing.Size(52, 21);
            this.refresh_button.TabIndex = 4;
            this.refresh_button.Text = "Refresh";
            this.refresh_button.UseVisualStyleBackColor = true;
            this.refresh_button.Click += new System.EventHandler(this.refresh_button_Click);
            // 
            // home_button
            // 
            this.home_button.Location = new System.Drawing.Point(789, 13);
            this.home_button.Name = "home_button";
            this.home_button.Size = new System.Drawing.Size(52, 21);
            this.home_button.TabIndex = 6;
            this.home_button.Text = "Home";
            this.home_button.UseVisualStyleBackColor = true;
            // 
            // html_box
            // 
            this.html_box.Location = new System.Drawing.Point(-1, 39);
            this.html_box.Multiline = true;
            this.html_box.Name = "html_box";
            this.html_box.ReadOnly = true;
            this.html_box.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.html_box.Size = new System.Drawing.Size(1000, 534);
            this.html_box.TabIndex = 7;
            this.html_box.TextChanged += new System.EventHandler(this.html_box_TextChanged);
            // 
            // url_box
            // 
            this.url_box.Location = new System.Drawing.Point(212, 13);
            this.url_box.Name = "url_box";
            this.url_box.Size = new System.Drawing.Size(513, 20);
            this.url_box.TabIndex = 8;
            this.url_box.TextChanged += new System.EventHandler(this.url_box_TextChanged);
            this.url_box.KeyDown += new System.Windows.Forms.KeyEventHandler(this.url_box_keyDown);
            // 
            // navigate_button
            // 
            this.navigate_button.Location = new System.Drawing.Point(731, 13);
            this.navigate_button.Name = "navigate_button";
            this.navigate_button.Size = new System.Drawing.Size(52, 21);
            this.navigate_button.TabIndex = 9;
            this.navigate_button.Text = "Go";
            this.navigate_button.UseVisualStyleBackColor = true;
            this.navigate_button.Click += new System.EventHandler(this.navigate_button_Click_1);
            // 
            // menu_button
            // 
            this.menu_button.Location = new System.Drawing.Point(914, 13);
            this.menu_button.Name = "menu_button";
            this.menu_button.Size = new System.Drawing.Size(52, 21);
            this.menu_button.TabIndex = 10;
            this.menu_button.Text = "Menu";
            this.menu_button.UseVisualStyleBackColor = true;
            this.menu_button.Click += new System.EventHandler(this.menu_button_Click);
            // 
            // favourite_button
            // 
            this.favourite_button.BackgroundImage = global::WebBrowser.Properties.Resources.star_black;
            this.favourite_button.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Center;
            this.favourite_button.Location = new System.Drawing.Point(867, 13);
            this.favourite_button.Name = "favourite_button";
            this.favourite_button.Size = new System.Drawing.Size(41, 21);
            this.favourite_button.TabIndex = 5;
            this.favourite_button.TextImageRelation = System.Windows.Forms.TextImageRelation.TextBeforeImage;
            this.favourite_button.UseVisualStyleBackColor = true;
            this.favourite_button.Click += new System.EventHandler(this.favourite_button_Click_1);
            // 
            // contextMenuStrip1
            // 
            this.contextMenuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.favouritesToolStripMenuItem,
            this.historyToolStripMenuItem});
            this.contextMenuStrip1.Name = "contextMenuStrip1";
            this.contextMenuStrip1.Size = new System.Drawing.Size(153, 70);
            // 
            // favouritesToolStripMenuItem
            // 
            this.favouritesToolStripMenuItem.Name = "favouritesToolStripMenuItem";
            this.favouritesToolStripMenuItem.Size = new System.Drawing.Size(152, 22);
            this.favouritesToolStripMenuItem.Text = "Favourites";
            this.favouritesToolStripMenuItem.Click += new System.EventHandler(this.favouritesToolStripMenuItem_Click);
            // 
            // historyToolStripMenuItem
            // 
            this.historyToolStripMenuItem.Name = "historyToolStripMenuItem";
            this.historyToolStripMenuItem.Size = new System.Drawing.Size(152, 22);
            this.historyToolStripMenuItem.Text = "History";
            this.historyToolStripMenuItem.Click += new System.EventHandler(this.historyToolStripMenuItem_Click);
            // 
            // Browser
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1000, 573);
            this.Controls.Add(this.menu_button);
            this.Controls.Add(this.navigate_button);
            this.Controls.Add(this.url_box);
            this.Controls.Add(this.html_box);
            this.Controls.Add(this.home_button);
            this.Controls.Add(this.favourite_button);
            this.Controls.Add(this.refresh_button);
            this.Controls.Add(this.forward_button);
            this.Controls.Add(this.back_button);
            this.Name = "Browser";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Apex Browser";
            this.contextMenuStrip1.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private System.Windows.Forms.Button back_button;
        private System.Windows.Forms.Button forward_button;
        private System.Windows.Forms.Button refresh_button;
        private System.Windows.Forms.Button favourite_button;
        private System.Windows.Forms.Button home_button;
        private System.Windows.Forms.TextBox html_box;
        private System.Windows.Forms.TextBox url_box;
        private System.Windows.Forms.Button navigate_button;
        private System.Windows.Forms.Button menu_button;
        private System.Windows.Forms.ContextMenuStrip contextMenuStrip1;
        private System.Windows.Forms.ToolStripMenuItem favouritesToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem historyToolStripMenuItem;
    }
}

