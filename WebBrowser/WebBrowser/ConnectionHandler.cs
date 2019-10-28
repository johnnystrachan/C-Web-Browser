using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using System.Text.RegularExpressions;

namespace WebBrowser
{
    class ConnectionHandler
    {
        History history = new History();
        public ConnectionHandler(){}

        public string handle(string s)
        {
            // Create a request for the URL. 
            if (this.format(s))
            {
                WebRequest request = null;
                try
                {
                    request = WebRequest.Create(s);
                }
                catch(Exception e)
                {
                    return "An exception occurred, please read the error message below. This is most likely due to a malformed request." + Environment.NewLine + e.Message;
                }
                
                // If required by the server, set the credentials.
                request.Credentials = CredentialCache.DefaultCredentials;
                // Get the response.
                HttpWebResponse response;
                try
                {
                 response = (HttpWebResponse)request.GetResponse();
                }catch(WebException e)
                {
                    var errorResponse = e.Response as HttpWebResponse;
                    return this.handleNotOK(errorResponse.StatusCode, s);
                }
                // Get response as stream
                Stream dataStream = response.GetResponseStream();
                // Open stream and read 
                StreamReader reader = new StreamReader(dataStream);     
                string responseFromServer = reader.ReadToEnd();
                
                //important to clean up!
                reader.Close();
                dataStream.Close();
                response.Close();
                this.history.AddToHistory(s);
                return response.StatusCode+System.Environment.NewLine+responseFromServer;
            }
            return "Invalid URL. Try formatting it as 'http://www.website.suffix'";
        }

        public string GetTitle(string s)
        {
            string regex = @"(?<=<title.*>)([\s\S]*)(?=</title>)";
            var match = Regex.Match(s, regex, RegexOptions.IgnoreCase);
            if (match.Success)
            {
                return match.Value;
            }else
            {
                return "Untitled Page";
            }
           
        }

        public bool format(string s)
       {
            //regex adapted from https://regex101.com/library/zB1sS9
            var regex = @"(((http|https):\/\/)|(\/)|(..\/))(\w+:{0,1}\w*@)?(\S+)(:[0-9]+)?(\/|\/([\w#!:.?+=&%@!\-\/]))?";
            var match = Regex.Match(s, regex, RegexOptions.IgnoreCase);
            if (!match.Success)
            {
                return false;
            }else
            {
                return true;
            }

        }

        public string handleNotOK(System.Net.HttpStatusCode code, string url)
        {
            switch (code)
            {
                case HttpStatusCode.BadRequest:
                    return "400 - Bad Request. " + System.Environment.NewLine + "Please check you haven't sent a malformed request and try again.";
                case HttpStatusCode.Forbidden:
                    return "403 - Forbidden." + System.Environment.NewLine + "Please check your credentials - it appears you don't have access to this link";
                case HttpStatusCode.NotFound:
                    return "404 - Not Found." + System.Environment.NewLine + "Please check that you entered a valid URL - the URL you entered could not be accessed";
                default:
                    return code.ToString() + System.Environment.NewLine+ "An error occured. Please check https://developer.mozilla.org/en-US/docs/Web/HTTP/Status for more information on your error"; 
            }
        }


    }
}
