using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using Newtonsoft.Json;
using static WebBrowser.FavouritesList;

namespace WebBrowser.FileHandling
{
    class FileHandler
    {

        private static readonly string FavouritePath = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData), "favourites.json");
        private static readonly string HistoryPath = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData), "history.txt");
        public FileHandler()
        {
            if ((bool)Properties.Settings.Default["FirstRun"])
            {
                Properties.Settings.Default["FirstRun"] = false;
                Properties.Settings.Default.Save();
                var ff = File.Create(FavouritePath);
                var fh = File.Create(HistoryPath);

            }
        }
     
        public List<Favourite> getFavouritesFromFile()
        {
            if (File.Exists(FavouritePath))
            {
                if (new FileInfo(FavouritePath).Length != 0)
                {
                    return JsonConvert.DeserializeObject<List<Favourite>>(File.ReadAllText(FavouritePath));
                }

            }
            return new List<Favourite>();
        }

        public void Delete(string path)
        {
            File.Delete(path);
        }

        public void WriteFavourites(List<Favourite> favourites)
        {       
            using (File.Create(FavouritePath))
            File.WriteAllText(
                FavouritePath,
                JsonConvert.SerializeObject(favourites)
                );
        }

        public void AddToHistory(string url)
        {
            if (File.Exists(HistoryPath))
            {
                File.AppendAllText(HistoryPath, url + Environment.NewLine);
            }
            else
            {
                var sw = new StreamWriter(HistoryPath);
                sw.Write(url + Environment.NewLine);
                sw.Close();
            }
        }
    }
}
