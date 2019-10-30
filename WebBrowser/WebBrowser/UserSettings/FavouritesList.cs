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

        private static List<Favourite> _favourites;
        private static FileHandling.FileHandler IOHandler = new FileHandling.FileHandler();
        private static readonly string FavouritePath = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData), "favourites.json");

        public FavouritesList() {
            _favourites = IOHandler.getFavouritesFromFile();
        }

        /// <summary>
        /// Adds a favourite to the favourites json file 
        /// </summary>
        /// <param name="name"></param>
        /// <param name="url"></param>
        public static void AddFavourite(string name, string url)
        {
            var newFavourite = new Favourite(url, name);
            if (!_favourites.Any(fav => fav.URL.Equals(url)))
            {
                _favourites.Add(newFavourite);              
                WriteFavourites();
            }else
            {
                //TODO: Throw error if already in list, handle wherever it needs to be handled
            }

            //TODO: Home page functionality
            //TODO: fix duplicate favourites being added         

        }
        /// <summary>
        /// Simple getter
        /// </summary>
        /// <returns>Returns a list of Favourite structs</returns>
        public List<Favourite> GetFavourites()
        {
            return _favourites;
       
        }

        /// <summary>
        /// Check if a given url is already present in the favourites list
        /// </summary>
        /// <param name="url"></param>
        /// <returns>Returns a boolean</returns>
        public static bool IsFavourite(string url)
        {
            return _favourites != null && _favourites.Any(fav => fav.URL.Equals(url));
        }

        /// <summary>
        /// remove a favourite from favourites list, given a url 
        /// </summary>
        /// <param name="url"></param>
        public static void RemoveFavourite(string url)
        {
            foreach (var fav in _favourites.Where(fav => fav.URL.Equals(url.Trim())))
            {
                _favourites.Remove(fav);
            }
        }

        public static void WriteFavourites()
        {
            IOHandler.WriteFavourites(_favourites);
            Console.WriteLine("writing favourites ");
            _favourites.ForEach(x => Console.WriteLine(x.URL));
        }

        /// <summary>
        /// clear all favourites
        /// </summary>
        public static void ClearFavourites()
        {
            _favourites = new List<Favourite>();
            IOHandler.Delete(FavouritePath);
        }

    }
}
