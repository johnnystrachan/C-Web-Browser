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
         
        }

        public List<String> GetHistory()
        {
            List<String> history = new List<String>();
            StreamReader sr = new StreamReader(path);
            string url;
            while ((url = sr.ReadLine()) != null)
            {
                history.Add(url);   
            }
            sr.Close();
            return history;
        }
       
    }
}
