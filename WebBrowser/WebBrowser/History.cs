using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace WebBrowser
{
    internal class History
    {
        /// <summary>
        /// Custom Exception to handle errors when navigating through session history with next and back buttons 
        /// </summary>
        public class SessionHistoryException : Exception
        {
            public SessionHistoryException()
            {
            }

            public SessionHistoryException(string message)
                : base(message)
            {
            }

            public SessionHistoryException(string message, Exception inner)
                : base(message, inner)
            {
            }
        }
        

        private static readonly string Path = System.IO.Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData), "history.txt");

        private static LinkedList<string> _localHistory = new LinkedList<string>();
        static string _currentUrl;

        public History(){
            _currentUrl = "default";
        }
       /// <summary>
       /// Adds a url to session & persistent history 
       /// </summary>
       /// <param name="url"></param>
        public static void AddToHistory(string url)
        {
            if ((bool) Properties.Settings.Default["FirstRun"])
            {
                Properties.Settings.Default["FirstRun"] = false;
                Properties.Settings.Default.Save();
                Directory.CreateDirectory("ApexBrowser");
                var fs = File.Create(Path);
                
            }
            if (File.Exists(Path)){
                File.AppendAllText(Path, url + Environment.NewLine);
            }
            else
            {
                var sw = new StreamWriter(Path);
                sw.Write(url+Environment.NewLine);
                sw.Close();
            }

            if(_localHistory.First == null)
            {
                _localHistory.AddFirst(url);
                _currentUrl = url;
            }else
            {
                _localHistory.AddAfter(_localHistory.Last, url);
                _currentUrl = url;
            }
         
        }

       /// <summary>
       /// Return a list of urls visited by the user
       /// </summary>
       /// <returns>List<string> of urls in history ></returns>
        public List<string> GetHistory()
        {
            var history = new List<string>();
            StreamReader sr = null;
            try
            {
               sr = new StreamReader(Path);
            }catch(Exception e)
            {
                Console.Error.WriteLine(e.Message);
            }


            if (sr == null) return history;
            string url;
            while ((url = sr.ReadLine()) != null)
            {
                history.Add(url);
            }

            sr.Close();
            return history;
        }

        /// <summary>
        /// Steps through localHistory linked list, to get the URL previous to the one currently being visited. _currentURL then is updated to new current URL
        /// </summary>
        /// <returns>string currentUrl</returns>
        public string GoBack()
        {

            if (_currentUrl == "default" || _currentUrl == null)//sanity check
            {
                throw new SessionHistoryException("You don't have a local history yet. Please navigate to a website.");
            }
            var curr = _localHistory.Find(_currentUrl);
            if(curr?.Previous == null)
            {
                throw new SessionHistoryException("No previous URL in your session history");
            }
            var back = curr.Previous;
            _currentUrl = back.Value;
            return _currentUrl;
        }

        /// <summary>
        ///  Steps through localHistory linked list, to get the URL after to the one currently being visited. _currentURL then is updated to new current URL
        /// </summary>
        /// <returns>string currentUrl</returns>
        public string GoForward()
        {
            var curr = _localHistory.Find(_currentUrl);
            if(curr?.Next == null)
            {
                throw new SessionHistoryException("No next URL in your session history");
            }
            var forward = curr.Next;
            _currentUrl = forward.Value;
            return _currentUrl;
        }

        public void ClearHistory()
        {
          File.Delete(Path);
          _localHistory = new LinkedList<string>();
        }
    }
}
