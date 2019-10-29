using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.Remoting.Messaging;


namespace WebBrowser
{
    class FavouritesList
    {
        internal struct Favourite
        {
            public string URL;
            public string Name;

            public Favourite(string url, string name)
            {
                this.Name = name;
                this.URL = url;
            }
        }

        private List<Favourite> favourites = new List<Favourite>();
        private static readonly string FavouritePath = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData), "favourites.json");
        public FavouritesList() {}

        public void AddFavourite(string name, string url)
        {
            var newFavourite = new Favourite(url, name);
            favourites.Add(newFavourite);

            var json = JsonConvert.SerializeObject(favourites, Formatting.Indented);
            File.WriteAllText(FavouritePath, json);

        }
        /// <summary>
        /// Method that reads from favourites.json file and gets favourites
        /// </summary>
        /// <returns>Returns a list of Favourite structs</returns>
        public List<Favourite> GetFavourites()
        {
            if (File.Exists(FavouritePath))
            {
                if (new FileInfo(FavouritePath).Length != 0)
                {
                    return JsonConvert.DeserializeObject<List<Favourite>>(File.ReadAllText(FavouritePath));
                }
               
            }

            try
            {
                if (!Directory.Exists(Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData),
                    "\\ApexBrowser"))) ;
                {
                    Directory.CreateDirectory(Path.Combine(
                        Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData),
                        "\\ApexBrowser"));
                }
            }
            catch (Exception e)
            {    Console.Error.WriteLine(e.Message);
            }
           

            using(File.Create(FavouritePath));
            File.WriteAllText(
                FavouritePath,
                JsonConvert.SerializeObject(new List<Favourite>())
                );
            return new List<Favourite>();

        }

        /// <summary>
        /// Check if a given url is already present in the favourites list
        /// </summary>
        /// <param name="url"></param>
        /// <returns>Returns a boolean</returns>
        public bool IsFavourite(string url)
        {
            return favourites != null && favourites.Any(fav => fav.URL.Equals(url));
        }

        public void RemoveFavourite(string url)
        {
            foreach (var fav in favourites.Where(fav => fav.URL.Equals(url.Trim())))
            {
                favourites.Remove(fav);
            }
        }

        public void ClearFavourites()
        {
            File.Delete(FavouritePath);
        }

    }
}
