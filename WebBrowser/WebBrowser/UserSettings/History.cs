using System;
using System.Collections.Generic;
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
        
        public struct HistoryLink
        {
            string _url;
            int _position;
        }

      
        private static FileHandling.FileHandler _ioHandler = new FileHandling.FileHandler();
        private static LinkedList<string> _localHistory;
        static string _currentUrl;
        private static Stack<string> _backStack, _frontStack;
        public History(){
            _currentUrl = "http://www.macs.hw.ac.uk/~hwloidl/Courses/F21SC/";
            _localHistory = new LinkedList<string>();
            _backStack = new Stack<string>();
            _frontStack = new Stack<string>();
        }
       /// <summary>
       /// Adds a url to session & persistent history 
       /// </summary>
       /// <param name="url"></param>
        public static void AddToHistory(string url)
        {

            _ioHandler.AddToHistory(url);
            _backStack.Push(_currentUrl);
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
               sr = new StreamReader(FileHandling.FileHandler.HistoryPath);
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

            var backUrl = "";
            if (_currentUrl == "default" || _currentUrl == null)//sanity check
            {
                throw new SessionHistoryException("You don't have a local history yet. Please navigate to a website.");
            }
            try
            {
                backUrl = _backStack.Pop();
            }catch(Exception e)
            {
                throw new SessionHistoryException("No previous URL in your session history");
            }
            _frontStack.Push(_currentUrl);
            _currentUrl = backUrl;
            return _currentUrl;
        }

        /// <summary>
        ///  Steps through localHistory linked list, to get the URL after to the one currently being visited. _currentURL then is updated to new current URL
        /// </summary>
        /// <returns>string currentUrl</returns>
        public string GoForward()
        {
           var frontUrl = "";
            if (_currentUrl == "default" || _currentUrl == null)//sanity check
            {
                throw new SessionHistoryException("You don't have a local history yet. Please navigate to a website.");
            }
            try
            {
                frontUrl = _frontStack.Pop();
            }
            catch (Exception e)
            {
                throw new SessionHistoryException("No next URL in your session history");
            }
            _backStack.Push(_currentUrl);
            _currentUrl = frontUrl;
            return _currentUrl;
        }

        /// <summary>
        /// A method that clears the history by setting to an empty list, and deleting the history file. 
        /// </summary>
        public void ClearHistory()
        {
          File.Delete(FileHandling.FileHandler.HistoryPath);
          _localHistory = new LinkedList<string>();
        }
    }
}
