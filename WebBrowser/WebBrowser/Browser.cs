using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Net;
using System.IO;

namespace WebBrowser
{
    public partial class Browser : Form
    {
        string url;
        ConnectionHandler handler = new ConnectionHandler();
        History history = new History();
        public Browser()
        {
            InitializeComponent();
        }


        private void maskedTextBox1_MaskInputRejected(object sender, MaskInputRejectedEventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {

        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {

        }

        private void url_box_TextChanged(object sender, EventArgs e)
        {
           
           
        }

        private void url_box_keyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                this.handler = new ConnectionHandler();
                this.html_box.Text = handler.handle(this.url_box.Text);
                this.url = this.url_box.Text;
            }
        }

        private void html_box_TextChanged(object sender, EventArgs e)
        {
           
       
        }

        private void navigate_button_Click_1(object sender, EventArgs e)
        {
           
            this.html_box.Text = this.handler.handle(this.url_box.Text);
            this.url = this.url_box.Text;
        }

        private void forward_button_Click(object sender, EventArgs e)
        {

        }

        private void refresh_button_Click(object sender, EventArgs e)
        {
            this.html_box.Text = this.handler.handle(this.url_box.Text);
        }

        private void menu_button_Click(object sender, EventArgs e)
        {
            contextMenuStrip1.Show(menu_button, new Point(0, menu_button.Height));
        }

        private bool notFavourite = true;

        private void favourite_button_Click_1(object sender, EventArgs e)
        {
            if (!notFavourite)
            {
                favourite_button.BackgroundImage = Properties.Resources.star_black;
                notFavourite = true;
            }
            else
            {
                favourite_button.BackgroundImage = Properties.Resources.star_yellow;
                notFavourite = false;
            }
        }

        private void historyToolStripMenuItem_Click(object sender, EventArgs e)
        {
            List<String> history = this.history.GetHistory();
            html_box.Text = String.Join(Environment.NewLine, history);
        }

        private void favouritesToolStripMenuItem_Click(object sender, EventArgs e)
        {
           
        }
        //generate fake URLs using https://webhook.site


    }
}
