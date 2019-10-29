using System;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;
using WebBrowser.Properties;
using WebBrowser.Views;

namespace WebBrowser
{
    public partial class Browser : Form
    {
        private readonly ConnectionHandler _handler = new ConnectionHandler();
        private readonly History _history = new History();
        private readonly FavouritesList _favourites = new FavouritesList();
        private string _url;

        public Browser()
        {
            InitializeComponent();
            LoadUserData();
        }

        /// <summary>
        /// Loads new history, used when user navigates to a new page, to add the latest website visited into the GUI.
        /// </summary>
        private void LoadUserData()
        {
            var historyList = _history.GetHistory();

            foreach (var tempUrl in historyList.Select(currUrl => historyToolStripMenuItem.DropDownItems.Add(currUrl)))
            {
                tempUrl.Click += (s, e) =>
                {
                    url_box.Text = _url;
                    navigate_button_Click_1(s, e);
                };
            }


            var favouritesList = _favourites.GetFavourites();
            foreach (var tempFav in favouritesList)
            {

                    var fav = favouritesToolStripMenuItem.DropDownItems.Add(tempFav.Name);

                    fav.Click += (s, e) =>
                    {
                        url_box.Text = tempFav.URL;
                        navigate_button_Click_1(s, e);
                    };

            }
            

        }

     

        private void url_box_TextChanged(object sender, EventArgs e)
        {
        }

        private void url_box_keyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter) navigate_button_Click_1(sender, e);
        }

        private void html_box_TextChanged(object sender, EventArgs e)
        {
        }

        private void navigate_button_Click_1(object sender, EventArgs e)
        {
            _url = url_box.Text;
            html_box.Text = _handler.Handle(_url);
            title_label.Text = _handler.GetTitle(html_box.Text);
            LoadUserData();
        }

        private void forward_button_Click(object sender, EventArgs e)
        {
            try
            {
                url_box.Text = _history.GoForward();
            }
            catch (History.SessionHistoryException err)
            {
                html_box.Text = err.Message;
                return;
            }

            navigate_button_Click_1(sender, e);
        
    }

        private void back_button_Click(object sender, EventArgs e)
        {
            try
            {
                url_box.Text = _history.GoBack();
            }
            catch (History.SessionHistoryException err)
            {
                html_box.Text = err.Message;
                return;
            }

            navigate_button_Click_1(sender, e);
        }

        private void refresh_button_Click(object sender, EventArgs e)
        {
            html_box.Text = _handler.Handle(url_box.Text);
        }

        private void menu_button_Click(object sender, EventArgs e)
        {
            contextMenuStrip1.Show(menu_button, new Point(0, menu_button.Height));
        }

        private void favourite_button_Click_1(object sender, EventArgs e)
        {
            favourite_button.BackgroundImage = !_favourites.IsFavourite(url_box.Text) ? Resources.star_black : Resources.star_yellow;
            var add_favourite_form = new FavouriteEditForm();
            add_favourite_form.Show();

        }

        private void favouritesToolStripMenuItem_Click(object sender, EventArgs e)
        {
        }

        private void clearFavouritesToolStripMenuItem_Click(object sender, EventArgs e)
        {
            _favourites.ClearFavourites();
            LoadUserData();
        }

        private void clearHistoryToolStripMenuItem_Click(object sender, EventArgs e)
        {
            _history.ClearHistory();
            LoadUserData();
        }






        //generate fake URLs using https://webhook.site
    }
}