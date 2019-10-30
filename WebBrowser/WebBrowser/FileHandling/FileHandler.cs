using System;
using System.Collections.Generic;
using System.IO;
using Newtonsoft.Json;
using static WebBrowser.FavouritesList;

namespace WebBrowser.FileHandling
{
    class FileHandler
    {

        public static readonly string FavouritePath = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData), "favourites.json");
        public static readonly string HistoryPath = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData), "history.txt");
        public static readonly string HomePath = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData), "home.txt");

        public FileHandler()
        {
            //check if this is the first time the application has been run
            //set files and default home.
            if (!(bool) Properties.Settings.Default["FirstRun"]) return;
            Properties.Settings.Default["FirstRun"] = false;
            Properties.Settings.Default.Save();
            var ff = File.Create(FavouritePath);
            var fh = File.Create(HistoryPath);
            File.Create(HomePath);
            File.WriteAllText(HomePath, "http://www.macs.hw.ac.uk/~hwloidl/Courses/F21SC/");
        }
     
        /// <summary>
        /// Go to hardcoded file and parse the JSON data to retrieve saved favourites
        /// </summary>
        /// <returns>A linked list of user stored favourites</returns>
        public List<Favourite> GetFavouritesFromFile()
        {
            if (!File.Exists(FavouritePath)) return new List<Favourite>();
            return new FileInfo(FavouritePath).Length != 0 ? JsonConvert.DeserializeObject<List<Favourite>>(File.ReadAllText(FavouritePath)) : new List<Favourite>();
        }

        /// <summary>
        /// Simple method that deletes a file at the specified path
        /// </summary>
        /// <param name="path"></param>
        public void Delete(string path)
        {
            File.Delete(path);
        }

        /// <summary>
        /// Overwrite previously saved favourites with new list of favourites
        /// </summary>
        /// <param name="favourites"></param>
        public void WriteFavourites(List<Favourite> favourites)
        {
                  File.WriteAllText(
                  FavouritePath,
                  JsonConvert.SerializeObject(favourites)
                  );           
        }

        /// <summary>
        /// Append a link to browser history 
        /// </summary>
        /// <param name="url"></param>
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
