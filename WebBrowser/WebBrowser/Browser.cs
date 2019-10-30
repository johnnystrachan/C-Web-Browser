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
            LoadUserHistory();
            UpdateUserFavourites();
        }

        /// <summary>
        /// Loads new history, used when user navigates to a new page, to add the latest website visited into the GUI.
        /// </summary>
        private void LoadUserHistory()
        {
            var historyList = _history.GetHistory();

            historyToolStripMenuItem.DropDownItems.Clear();
            foreach (var tempUrl in historyList.Select(currUrl => historyToolStripMenuItem.DropDownItems.Add(currUrl)))
            {
                tempUrl.Click += (s, e) =>
                {
                    url_box.Text = _url;
                    navigate_button_Click_1(s, e);
                };
            }

        }

        public void UpdateUserFavourites()
        { 
            var favouritesList = _favourites.GetFavourites();
            if(favouritesList.Count <= 0)
            {
                for (int i = favouritesToolStripMenuItem.DropDownItems.Count - 1; i >= 0; i--)
                {
                    if (favouritesToolStripMenuItem.DropDownItems[i] is ToolStripMenuItem)
                    {
                        favouritesToolStripMenuItem.DropDownItems.RemoveAt(i); 
                    }
                }
            }
            foreach (var tempFav in favouritesList)
            {
               if (!favouritesToolStripMenuItem.DropDownItems
                   .Cast<ToolStripMenuItem>()
                     .Any(x => x.Text == tempFav.Name))
               {
                var fav = favouritesToolStripMenuItem.DropDownItems.Add(tempFav.Name);
           
                   fav.Click += (s, e) =>
                   {
                       url_box.Text = tempFav.URL;
                       navigate_button_Click_1(s, e);
                   };
               }
           }
        }

        private void browser_FormClosed(object sender, EventArgs e)
        {
            FavouritesList.WriteFavourites();
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
            try
            {
                html_box.Text = _handler.Handle(_url);
                title_label.Text = _handler.GetTitle(html_box.Text);
                var tempURL = historyToolStripMenuItem.DropDownItems.Add(_url);
                tempURL.Click += (s, err) =>
                {
                    url_box.Text = _url;
                    navigate_button_Click_1(s, e);
                };
            }
            catch (ConnectionHandler.HttpErrorCodeException err)
            {
                html_box.Text = err.Message;
                title_label.Text = "";
            }
            favourite_button.BackgroundImage = !FavouritesList.IsFavourite(url_box.Text) ? Resources.star_black : Resources.star_yellow;
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

        private void menu_button_Click(object sender, EventArgs eArgs)
        {
            contextMenuStrip1.Show(menu_button, new Point(0, menu_button.Height));
           
        }

        private void favourite_button_Click_1(object sender, EventArgs e)
        {
            var add_favourite_form = new FavouriteEditForm();
            add_favourite_form.ShowDialog(this);
            UpdateUserFavourites();
            
        }

   


        private void clearFavouritesToolStripMenuItem_Click(object sender, EventArgs e)
        {
            FavouritesList.ClearFavourites();
            UpdateUserFavourites();
        }

        private void clearHistoryToolStripMenuItem_Click(object sender, EventArgs e)
        {
            _history.ClearHistory();
            LoadUserHistory();
        }

        private void contextMenuStrip1_Opening(object sender, System.ComponentModel.CancelEventArgs e)
        {
        //    var favouritesList = _favourites.GetFavourites();
        //
        //    var favItems = favouritesToolStripMenuItem.DropDownItems.Cast<ToolStripDropDownItem>().ToArray();
        //    foreach (ToolStripDropDownItem item in favItems)
        //    {
        //        // item.Click -= item_Click;
        //        item.Dispose();
        //    }
        }







        //generate fake URLs using https://webhook.site
    }
}