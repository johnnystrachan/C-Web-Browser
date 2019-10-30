using System.IO;

namespace WebBrowser.UserSettings
{
    class Home
    {
        private string _url;
        public Home(string url)
        {
            this._url = url;
        }

        /// <summary>
        /// Method that allows to edit the home url by updating the object's url property, as well as writing the new url to the home.txt file
        /// </summary>
        /// <param name="url"></param>
        public void EditHome(string url)
        {
            this._url = url;
            File.WriteAllText(
                 FileHandling.FileHandler.HomePath,
                 url
                 );
        }

        //return home url
        public string GetHome()
        {
            return this._url;
        }
    }
}
