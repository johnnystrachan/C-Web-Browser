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
            LoadUserData();  
        }

        private void LoadUserData()
        {
            List<String> history = this.history.GetHistory();

            foreach (var url in history)
            {
                var tempURL = historyToolStripMenuItem.DropDownItems.Add(url);
                tempURL.Click += (s, e) =>
                {
                    this.url_box.Text = url;
                    navigate_button_Click_1(s,e);
                };
            }

        }

        private void back_button_Click(object sender, EventArgs e)
        {


            this.url_box.Text = this.history.GoBack();
            navigate_button_Click_1(sender, e);

        }

        private void url_box_TextChanged(object sender, EventArgs e)
        {
           
           
        }

        private void url_box_keyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
             navigate_button_Click_1(sender, e);
            }
        }

        private void html_box_TextChanged(object sender, EventArgs e)
        {
           
       
        }

        private void navigate_button_Click_1(object sender, EventArgs e)
        {
            this.url = this.url_box.Text;
            this.html_box.Text = this.handler.handle(this.url);
            this.title_label.Text = this.handler.GetTitle(this.html_box.Text);
            LoadUserData();
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
                this.html_box.Text = this.url;
            }
            else
            {
                favourite_button.BackgroundImage = Properties.Resources.star_yellow;
                notFavourite = false;
                this.html_box.Text = this.url;
            }
        }

        private void favouritesToolStripMenuItem_Click(object sender, EventArgs e)
        {
           
        }


        private void clearFavouritesToolStripMenuItem_Click(object sender, EventArgs e)
        {

        }

        private void clearHistoryToolStripMenuItem_Click(object sender, EventArgs e)
        {
            this.history.DeleteHistory();
            //this.history.GetHistory();
            LoadUserData();
        }


        //generate fake URLs using https://webhook.site


    }
}
