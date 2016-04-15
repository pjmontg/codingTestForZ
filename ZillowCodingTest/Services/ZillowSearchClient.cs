using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Net;
using System.IO;
using System.Xml.Serialization;
using ZillowCodingTest.Models;
using ZillowCodingTest.Utilities;

namespace ZillowCodingTest.Services
{
    public class ZillowSearchClient
    {
        public searchresults GetPropertyDetails(string Address, string CityStateOrZip)
        {
            searchresults results = null;

            string requestUrl = String.Format("{0}?zws-id={1}&address={2}&citystatezip={3}", Constants.API_URL, Constants.API_KEY, HttpUtility.UrlEncode(Address), HttpUtility.UrlEncode(CityStateOrZip));
            try
            {
                HttpWebRequest request = (HttpWebRequest)WebRequest.Create(requestUrl);
                request.Method = "GET";
                WebResponse response = request.GetResponse();
                using (StreamReader reader = new StreamReader(response.GetResponseStream()))
                {
                    string resultXml = reader.ReadToEnd();

                    XmlSerializer serializer = new XmlSerializer(typeof(searchresults));
                    results = (searchresults)serializer.Deserialize(new StringReader(resultXml));
                    response.Close();
                }
            }

            catch (Exception ex)
            {
                //In a production app, I would normally log the stack trace, and then present thown an exception that the view could handle in a friendly way
                //to prompt the user to take some kind of action (contact administrator, etc.)
                throw new Exception(ex.Message);
            }

            return results;
        }
    }
}