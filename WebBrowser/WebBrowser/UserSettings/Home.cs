using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WebBrowser.UserSettings
{
    class Home
    {
        private string url;
        private static readonly string HomePath = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData), "home.txt");
        public Home(string url)
        {
            this.url = url;
        }

        public void EditHome(string url)
        {
            this.url = url;
            File.WriteAllText(
                 HomePath,
                 url
                 );
        }

        public string GetHome()
        {
            return this.url;
        }
    }
}
