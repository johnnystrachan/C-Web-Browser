using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace WebBrowser.Views
{
    public partial class FavouriteEditForm : Form
    {
        ConnectionHandler cHandler = new ConnectionHandler();

        public FavouriteEditForm()
        {
            InitializeComponent();
        }

        private void done_button_Click(object sender, EventArgs e)
        {
            if (name_box.Text.Trim() == string.Empty)
            {
                MessageBox.Show("Your favourite must have a name!");
                return;
            }if (url_box.Text.Trim() == string.Empty || !cHandler.Format(url_box.Text.Trim()))
            {
                MessageBox.Show("You must favourite a valid URL!");
                return;
            }

            FavouritesList.AddFavourite(name_box.Text.Trim(), url_box.Text.Trim());
            this.Hide();
        }

        private void cancel_button_Click(object sender, EventArgs e)
        {
            this.Hide();
        }

        private void remove_button_Click(object sender, EventArgs e)
        {
            FavouritesList.RemoveFavourite(url_box.Text.Trim());
        }
    }
}
