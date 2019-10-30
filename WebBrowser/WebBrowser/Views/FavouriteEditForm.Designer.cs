namespace WebBrowser.Views
{
    partial class FavouriteEditForm
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
            this.add_favourite_label = new System.Windows.Forms.Label();
            this.done_button = new System.Windows.Forms.Button();
            this.remove_button = new System.Windows.Forms.Button();
            this.cancel_button = new System.Windows.Forms.Button();
            this.name_label = new System.Windows.Forms.Label();
            this.url_label = new System.Windows.Forms.Label();
            this.name_box = new System.Windows.Forms.TextBox();
            this.url_box = new System.Windows.Forms.TextBox();
            this.SuspendLayout();
            // 
            // add_favourite_label
            // 
            this.add_favourite_label.AutoSize = true;
            this.add_favourite_label.Location = new System.Drawing.Point(121, 9);
            this.add_favourite_label.Name = "add_favourite_label";
            this.add_favourite_label.Size = new System.Drawing.Size(96, 13);
            this.add_favourite_label.TabIndex = 0;
            this.add_favourite_label.Text = "Add/Edit Favourite";
            // 
            // done_button
            // 
            this.done_button.Location = new System.Drawing.Point(133, 155);
            this.done_button.Name = "done_button";
            this.done_button.Size = new System.Drawing.Size(75, 23);
            this.done_button.TabIndex = 1;
            this.done_button.Text = "Done";
            this.done_button.UseVisualStyleBackColor = true;
            this.done_button.Click += new System.EventHandler(this.done_button_Click);
            // 
            // remove_button
            // 
            this.remove_button.Location = new System.Drawing.Point(247, 155);
            this.remove_button.Name = "remove_button";
            this.remove_button.Size = new System.Drawing.Size(75, 23);
            this.remove_button.TabIndex = 2;
            this.remove_button.Text = "Remove";
            this.remove_button.UseVisualStyleBackColor = true;
            this.remove_button.Click += new System.EventHandler(this.remove_button_Click);
            // 
            // cancel_button
            // 
            this.cancel_button.Location = new System.Drawing.Point(29, 155);
            this.cancel_button.Name = "cancel_button";
            this.cancel_button.Size = new System.Drawing.Size(75, 23);
            this.cancel_button.TabIndex = 3;
            this.cancel_button.Text = "Cancel";
            this.cancel_button.UseVisualStyleBackColor = true;
            this.cancel_button.Click += new System.EventHandler(this.cancel_button_Click);
            // 
            // name_label
            // 
            this.name_label.AutoSize = true;
            this.name_label.Location = new System.Drawing.Point(26, 55);
            this.name_label.Name = "name_label";
            this.name_label.Size = new System.Drawing.Size(35, 13);
            this.name_label.TabIndex = 4;
            this.name_label.Text = "Name";
            // 
            // url_label
            // 
            this.url_label.AutoSize = true;
            this.url_label.Location = new System.Drawing.Point(26, 93);
            this.url_label.Name = "url_label";
            this.url_label.Size = new System.Drawing.Size(29, 13);
            this.url_label.TabIndex = 5;
            this.url_label.Text = "URL";
            // 
            // name_box
            // 
            this.name_box.Location = new System.Drawing.Point(67, 55);
            this.name_box.Name = "name_box";
            this.name_box.Size = new System.Drawing.Size(241, 20);
            this.name_box.TabIndex = 6;
            // 
            // url_box
            // 
            this.url_box.Location = new System.Drawing.Point(67, 90);
            this.url_box.Name = "url_box";
            this.url_box.Size = new System.Drawing.Size(241, 20);
            this.url_box.TabIndex = 7;
            // 
            // FavouriteEditForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(334, 190);
            this.Controls.Add(this.url_box);
            this.Controls.Add(this.name_box);
            this.Controls.Add(this.url_label);
            this.Controls.Add(this.name_label);
            this.Controls.Add(this.cancel_button);
            this.Controls.Add(this.remove_button);
            this.Controls.Add(this.done_button);
            this.Controls.Add(this.add_favourite_label);
            this.Name = "FavouriteEditForm";
            this.Text = "Edit Favourite Page";
            this.Load += new System.EventHandler(this.FavouriteEditForm_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label add_favourite_label;
        private System.Windows.Forms.Button done_button;
        private System.Windows.Forms.Button remove_button;
        private System.Windows.Forms.Button cancel_button;
        private System.Windows.Forms.Label name_label;
        private System.Windows.Forms.Label url_label;
        private System.Windows.Forms.TextBox name_box;
        private System.Windows.Forms.TextBox url_box;
    }
}