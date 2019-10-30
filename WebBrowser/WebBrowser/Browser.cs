using System;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;
using WebBrowser.Properties;
using WebBrowser.UserSettings;
using WebBrowser.Views;

namespace WebBrowser
{
    public partial class Browser : Form
    {
        private readonly ConnectionHandler _handler = new ConnectionHandler();
        private readonly History _history = new History();
        private readonly FavouritesList _favourites = new FavouritesList();
        private string _url;
        private Home _home = new Home("http://www.macs.hw.ac.uk/~hwloidl/Courses/F21SC/");
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
                    url_box.Text = tempUrl.Text;
                    navigate_button_Click_1(s, e);
                };
            }

        }
        /// <summary>
        /// Updates favourites drop down menu based on favourites list.
        /// </summary>
        public void UpdateUserFavourites()
        { 
            var favouritesList = _favourites.GetFavourites();
                for (var i = favouritesToolStripMenuItem.DropDownItems.Count - 1; i >= 0; i--)
                {
                    if (favouritesToolStripMenuItem.DropDownItems[i] is ToolStripMenuItem)
                    {
                        favouritesToolStripMenuItem.DropDownItems.RemoveAt(i); 
                    }
                }
            
            foreach (var tempFav in favouritesList)
            {
                if (favouritesToolStripMenuItem.DropDownItems.Cast<ToolStripMenuItem>().Any(x => x.Text == tempFav.Name)) continue;
                var fav = favouritesToolStripMenuItem.DropDownItems.Add(tempFav.Name);
           
               fav.Click += (s, e) =>
               {
                   url_box.Text = tempFav.URL;
                   navigate_button_Click_1(s, e);
               };
            }
        }

        /// <summary>
        /// On browser load, navigate to home page.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void browser_Load(object sender, EventArgs e)
        {
            url_box.Text = _home.GetHome();
            navigate_button_Click_1(sender, e);
        }

        /// <summary>
        /// On browser close, write favourites to file by calling FavouritesList.WriteFavourites
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void browser_FormClosed(object sender, EventArgs e)
        {
            FavouritesList.WriteFavourites();
        }

        /// <summary>
        /// When enter is pressed on url box, navigate to page
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void url_box_keyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter) navigate_button_Click_1(sender, e);
        }

        /// <summary>
        /// When "Go" button is pressed, navigate to page. Uses ConnectionHandler class. Displays code 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="eArgs"></param>
        private void navigate_button_Click_1(object sender, EventArgs eArgs)
        {
            _url = url_box.Text;
            try
            {
                html_box.Text = _handler.Handle(_url);
                title_label.Text = _handler.GetTitle(html_box.Text);
                code_label.Text = _handler.GetCode();
                if (code_label.Text.Equals("200 | OK"))
                {
                    code_label.ForeColor = Color.Green;
                }
                var tempUrl = historyToolStripMenuItem.DropDownItems.Add(_url);
               tempUrl.Click += (s, e) =>
               {
                   url_box.Text = tempUrl.Text;
                   navigate_button_Click_1(s, e);
               };
            }
            catch (ConnectionHandler.HttpErrorCodeException err)
            {
                MessageBox.Show("HTPP Error!" + System.Environment.NewLine + err.Message);
                html_box.Text = err.Message;
                title_label.Text = "";
                code_label.Text = err.GetCode().ToString() + " | " + err.Message;
                code_label.ForeColor = Color.Red;
            }
            favourite_button.BackgroundImage = !FavouritesList.IsFavourite(url_box.Text) ? Resources.star_black : Resources.star_yellow;
        }

        /// <summary>
        /// On forward button. move to next page in local history stack. Uses History class
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void forward_button_Click(object sender, EventArgs e)
        {
            try
            {
                url_box.Text = _history.GoForward();
            }
            catch (History.SessionHistoryException err)
            {    
                MessageBox.Show("Error!" + System.Environment.NewLine + err.Message);
                return;
            }

            navigate_button_Click_1(sender, e);
        
    }
        /// <summary>
        ///  On back button. move to previous page in local history stack. Uses History class
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void back_button_Click(object sender, EventArgs e)
        {
            try
            {
                url_box.Text = _history.GoBack();
            }
            catch (History.SessionHistoryException err)
            {
                MessageBox.Show("Error!" + System.Environment.NewLine + err.Message);
                return;
            }

            navigate_button_Click_1(sender, e);
        }

        /// <summary>
        /// On refresh button click, navigate once again to current page. uses ConnectionHandler class
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void refresh_button_Click(object sender, EventArgs e)
        {
            html_box.Text = _handler.Handle(url_box.Text);
        }

        /// <summary>
        /// On menu button click, open context menu
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="eArgs"></param>
        private void menu_button_Click(object sender, EventArgs eArgs)
        {
            contextMenuStrip1.Show(menu_button, new Point(0, menu_button.Height));
           
        }

        /// <summary>
        /// On favourite button click, open FavouriteEdit form and update user favourites.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void favourite_button_Click_1(object sender, EventArgs e)
        {

            var addFavouriteForm = new FavouriteEditForm(url_box.Text, title_label.Text);
            var tempFavName = _favourites.GetFavouriteName(url_box.Text);
            if(tempFavName != null)
            {
                addFavouriteForm = new FavouriteEditForm(url_box.Text, tempFavName);
            }
            
            addFavouriteForm.ShowDialog(this);
            UpdateUserFavourites();
            
        }

        /// <summary>
        /// On clear favourites click, use FavouritesList class to clear favourites, and update the GUI accordingly. 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void clearFavouritesToolStripMenuItem_Click(object sender, EventArgs e)
        {
            FavouritesList.ClearFavourites();
            UpdateUserFavourites();
        }

        /// <summary>
        ///  On clear history click, use History class to clear history, and update the GUI accordingly. 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void clearHistoryToolStripMenuItem_Click(object sender, EventArgs e)
        {
            _history.ClearHistory();
            LoadUserHistory();
        }

        /// <summary>
        /// When clicking on the Set Current Page as Home button, 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void setCurrentPageAsHomeToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if(url_box.Text.Trim() == "" || _home.GetHome() == null || !_handler.Format(url_box.Text.Trim()))
            {
                MessageBox.Show("Your homepage must be a valid URL!");
            }else if (url_box.Text.Trim() == _home.GetHome())
            {
                MessageBox.Show("This is already your homepage!");
            }
            _home.EditHome(url_box.Text.Trim());
        }

        /// <summary>
        /// On home button click, navigate to Home. Uses Home and ConnectionHandler class.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void home_button_Click(object sender, EventArgs e)
        {
            if (_home.GetHome() == "" || _home.GetHome() == null) return; //sanity check, shouldn't happen 
            html_box.Text = "CLICK!";
            url_box.Text = _home.GetHome();
            this.navigate_button_Click_1(sender, e);
        }
    }
}