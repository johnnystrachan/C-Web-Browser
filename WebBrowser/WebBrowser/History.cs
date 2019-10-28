using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace WebBrowser
{

    class History
    {

        private string path = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData), "history.txt");

        private LinkedList<string> localHistory;
        public History(){}
       
        public void AddToHistory(string url)
        {
            if ((bool) Properties.Settings.Default["FirstRun"] == true)
            {
                Properties.Settings.Default["FirstRun"] = false;
                Properties.Settings.Default.Save();
                FileStream fs = File.Create(path);
            }
           if (File.Exists(path)){
               File.AppendAllText(path, url + Environment.NewLine);
            }
            else
            {
                StreamWriter sw = new StreamWriter(path);
                sw.Write(url+Environment.NewLine);
                sw.Close();
            }

           if(localHistory.First == null)
            {
                localHistory.AddFirst(url);
            }else
            {
                localHistory.AddAfter(localHistory.Last, url);
            }
         
        }

        public List<String> GetHistory()
        {
            List<String> history = new List<String>();
            StreamReader sr = null;
            try
            {
               sr = new StreamReader(path);
            }catch(Exception e)
            {
                Console.Error.WriteLine(e.Message);
            }
         
        
            string url;
            if(sr != null)
            {
                while ((url = sr.ReadLine()) != null)
                {
                    history.Add(url);
                }
                sr.Close();
            }
          

           // history.Reverse();
           
            return history;
        }

        public void DeleteHistory()
        {
            if (File.Exists(path))
            {
                File.Delete(path);
            }
            return;
        }

        //need to implement GoBack method
    }
}
