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
    internal class ConnectionHandler
    {
        public class HttpErrorCodeException : Exception
        {
            public HttpErrorCodeException()
            {
            }

            public HttpErrorCodeException(string message)
                : base(message)
            {
            }

            public HttpErrorCodeException(string message, Exception inner)
                : base(message, inner)
            {
            }
        }
        public ConnectionHandler(){}
        /// <summary>
        /// Method that handles a connection attempt to a given URL, and returns HTML
        /// </summary>
        /// <param name="s"></param>
        /// <returns> returns HTML</returns>
        public string Handle(string s)
        {
            // Create a request for the URL. 
            if (!this.Format(s)) return "Invalid URL. Try formatting it as 'http://www.website.suffix'";
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
                if (errorResponse == null)
                {
                    throw new HttpErrorCodeException(e.Message);
                }
                    
                return HandleNotOk(errorResponse.StatusCode, s);
            }

            // Get response as stream
            var dataStream = response.GetResponseStream();
            // Open stream and read 
            var reader = new StreamReader(dataStream);     
            var responseFromServer = reader.ReadToEnd();
                
            //important to clean up!
            reader.Close();
            dataStream.Close();
            response.Close();
            History.AddToHistory(s);
            return response.StatusCode+System.Environment.NewLine+responseFromServer;
        }

        /// <summary>
        /// Method to get title from URL, using regex
        /// </summary>
        /// <param name="s"></param>
        /// <returns>Returns true if param matches regex, else false</returns>
        public string GetTitle(string s)
        {
            
            const string regex = @"(?<=<title.*>)([\s\S]*)(?=</title>)";
            var match = Regex.Match(s, regex, RegexOptions.IgnoreCase);
            return match.Success ? match.Value : "Untitled Page";
           
        }

        /// <summary>
        /// Checks if URL is formatted correctly, as per regex
        /// </summary>
        /// <param name="s"></param>
        /// <returns>Return true if param matches regex, else false</returns>
        public bool Format(string s)
       {
            //regex adapted from https://regex101.com/library/zB1sS9
            const string regex = @"(((http|https):\/\/)|(\/)|(..\/))(\w+:{0,1}\w*@)?(\S+)(:[0-9]+)?(\/|\/([\w#!:.?+=&%@!\-\/]))?";
            var match = Regex.Match(s, regex, RegexOptions.IgnoreCase);
            return match.Success;

        }
        /// <summary>
        /// A switch to handle different error codes as per the specification document. In this case, 400, 403, 404 are handled.
        /// </summary>
        /// <param name="code"></param>
        /// <param name="url"></param>
        /// <returns> A string to print to the GUI to let the user know there was an issue.</returns>
        public string HandleNotOk(System.Net.HttpStatusCode code, string url)
        {
            switch (code)
            {
                case HttpStatusCode.BadRequest:
                    throw new HttpErrorCodeException( "400 - Bad Request. " + System.Environment.NewLine + "Please check you haven't sent a malformed request and try again.");
                case HttpStatusCode.Forbidden:
                    throw new HttpErrorCodeException("403 - Forbidden." + System.Environment.NewLine + "Please check your credentials - it appears you don't have access to this link");
                case HttpStatusCode.NotFound:
                    throw new HttpErrorCodeException("404 - Not Found." + System.Environment.NewLine + "Please check that you entered a valid URL - the URL you entered could not be accessed");
                default:
                    throw new HttpErrorCodeException(code.ToString() + System.Environment.NewLine+ "An error occured. Please check https://developer.mozilla.org/en-US/docs/Web/HTTP/Status for more information on your error"); 
            }
        }


    }
}
