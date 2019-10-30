using System;
using System.Windows.Forms;

namespace WebBrowser.Views
{
    public partial class FavouriteEditForm : Form
    {
        readonly ConnectionHandler _cHandler = new ConnectionHandler();
        private readonly string _url;
        private readonly string _title;
        public FavouriteEditForm(string url, string title)
        {
            InitializeComponent();
            this._url = url;
            this._title = title;
        }
        /// <summary>
        /// Method that, on form loading, will populate name and url box with current page's default.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void FavouriteEditForm_Load(object sender, EventArgs e)
        {
            name_box.Text = this._title;
            url_box.Text = this._url;
        }

        /// <summary>
        /// Method that validates input from user, and then adds/edits a favourite by calling the FavouritesList.AddFavourite method with user input as arguments 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void done_button_Click(object sender, EventArgs e)
        {
            if (name_box.Text.Trim() == string.Empty)
            {
                MessageBox.Show("Your favourite must have a name!");
                return;
            }if (url_box.Text.Trim() == string.Empty || !_cHandler.Format(url_box.Text.Trim()))
            {
                MessageBox.Show("You must favourite a valid URL!");
                return;
            }

            FavouritesList.AddFavourite(name_box.Text.Trim(), url_box.Text.Trim());
            this.Hide();
        }

        /// <summary>
        /// Hide the modal
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void cancel_button_Click(object sender, EventArgs e)
        {
            this.Hide();
        }
        
        /// <summary>
        /// A method that calls the FavouritesList.Remove method with the url in the textbox 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void remove_button_Click(object sender, EventArgs e)
        {
            FavouritesList.RemoveFavourite(url_box.Text.Trim());
            MessageBox.Show("Favourite removed!");
            this.Hide();
        }

 
    }
}
