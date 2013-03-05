using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;
using System.IO;
using System.Net; 

namespace GitHub_CustomerIO_DotNET_Interface
{
    class Program
    {
        static void Main(string[] args)
        {
            try
            {

                // Console application output  
                Console.WriteLine("!!! *** Start Job *** !!!");
                Console.WriteLine(" ");
                Console.WriteLine("Attempting connection to Customer.IO servers for Data upload");
                Console.WriteLine(" ");

                //User or Customer ID must be unique per user as required by Customer.IO REST API
                // string User_ID = User_ID + Convert.ToString(j);
                string User_ID = "production_40";

                //Customer.io API Endpoint
                Uri address = new Uri("https://app.customer.io/api/v1/customers/" + User_ID);

                // Create the web request  
                HttpWebRequest myReq = HttpWebRequest.Create(address) as HttpWebRequest;

                string Site_ID = "c79xxxxxxxxxxxxxxx62";       //Your Site ID Here - provided by Customer.IO
                string Api_Key = "2fxxxxxxxxxxxxxxxx64";       //Your Secret API key here - provided by Customer.IO

                string usernamePassword = Site_ID + ":" + Api_Key;

                CredentialCache mycache = new CredentialCache();
                mycache.Add(address, "Basic", new NetworkCredential(Site_ID, Api_Key));
                myReq.Credentials = mycache;

                myReq.Headers.Add("Authorization", "Basic " + Convert.ToBase64String(new ASCIIEncoding().GetBytes(usernamePassword)));

                myReq.Method = "PUT";
                myReq.ContentType = "application/x-www-form-urlencoded";

                // Create the data we want to send, here you can customise various attributes   
                string email = "user@mail.com";
                string login = "Dummy_Login";
                string firstname = "Dummy_Name";
                string locked = "Yes";
                string confirmed = "Yes";
                string balance = "14.00";

                
                StringBuilder postData = new StringBuilder();

                postData.Append("&EMAIL=" + HttpUtility.UrlEncode(email));

                postData.Append("&LOGIN=" + HttpUtility.UrlEncode(login));

                postData.Append("&FIRSTNAME=" + HttpUtility.UrlEncode(firstname));

                postData.Append("&LOCKED=" + HttpUtility.UrlEncode(locked));

                postData.Append("&CONFIRMED=" + HttpUtility.UrlEncode(confirmed));

                postData.Append("&BALANCE=" + HttpUtility.UrlEncode(balance));


                byte[] byteData = Encoding.UTF8.GetBytes(postData.ToString());

                myReq.ContentLength = byteData.Length;

                // Write data  
                using (Stream postStream = myReq.GetRequestStream())
                {
                    postStream.Write(byteData, 0, byteData.Length);
                }

                WebResponse wr = myReq.GetResponse();
                Stream receiveStream = wr.GetResponseStream();

                StreamReader reader = new StreamReader(receiveStream, Encoding.UTF8);

                string content = reader.ReadToEnd();
                Console.WriteLine(content);

                Console.WriteLine("Data Successfully Uploaded");
                Console.WriteLine(" ");
                Console.WriteLine("!!! *** End Job *** !!!");
                Console.ReadLine();

            }
            catch (Exception e)
            {
                Console.WriteLine(" ");
                Console.WriteLine("!!! *** Exception *** !!!");
                Console.WriteLine(e.Message);
                Console.WriteLine("!!! *** Exception *** !!!");
                Console.ReadLine();
            }
        }
    }
}
