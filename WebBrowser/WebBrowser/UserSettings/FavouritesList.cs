using System;
using System.Collections.Generic;
using System.Linq;



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
        

        public FavouritesList() {
            _favourites = IOHandler.GetFavouritesFromFile();
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
              //  WriteFavourites();
            }else
            {

                var index = _favourites.FindIndex(f => f.URL.Equals(url));
                Favourite[] favArr = _favourites.ToArray();
                favArr[index].Name = name;
                _favourites = favArr.ToList();
               // WriteFavourites();
            }

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
        /// Loop through favourites to find name of favourite in question
        /// </summary>
        /// <param name="url"></param>
        /// <returns>Returns name of favourite as a string</returns>
        public string GetFavouriteName(string url)
        {
            return (from fav in _favourites where fav.URL.Equals(url) select fav.Name).FirstOrDefault();
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
            for (var i = _favourites.Count - 1; i >= 0; i--)
            {
                if (_favourites[i].URL.Equals(url)) _favourites.RemoveAt(i);
            }
        }
        
        /// <summary>
        /// Method that writes the currnet favourites list to file
        /// </summary>
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
            IOHandler.Delete(FileHandling.FileHandler.FavouritePath);
        }

    }
}
