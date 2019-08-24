using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Diagnostics;
using System.Net;
using System.IO;
using System.Collections.Specialized;

namespace SatSim.Methods.TLE_Scrap
{
    public class TLE_Scrap
    {
		public class WebClientEx : WebClient
		{
			// Create the container to hold all Cookie objects
			private CookieContainer _cookieContainer = new CookieContainer();

			// Override the WebRequest method so we can store the cookie
			// container as an attribute of the Web Request object
			protected override WebRequest GetWebRequest(Uri address)
			{
				WebRequest request = base.GetWebRequest(address);

				if (request is HttpWebRequest)
					(request as HttpWebRequest).CookieContainer = _cookieContainer;

				return request;
			}
		}   // END WebClient Class

		// Get the TLEs based of an array of NORAD CAT IDs, start date, and end date
		public string GetSpaceTrack(string[] norad, DateTime dtstart, DateTime dtend)
		{
			string uriBase = "https://www.space-track.org";
			string requestController = "/basicspacedata";
			string requestAction = "/query";
			// URL to retrieve all the latest tle's for the provided NORAD CAT
			// IDs for the provided Dates
			//string predicateValues   = "/class/tle_latest/ORDINAL/1/NORAD_CAT_ID/" + string.Join(",", norad) + "/orderby/NORAD_CAT_ID%20ASC/format/tle";
			// URL to retrieve all the latest 3le's for the provided NORAD CAT
			// IDs for the provided Dates
			string predicateValues = "/class/tle/EPOCH/" + dtstart.ToString("yyyy-MM-dd--") + dtend.ToString("yyyy-MM-dd") + "/NORAD_CAT_ID/" + string.Join(",", norad) + "/orderby/NORAD_CAT_ID%20ASC/format/3le";
			string request = uriBase + requestController + requestAction + predicateValues;

			// Create new WebClient object to communicate with the service
			using (var client = new WebClientEx())
			{
				// Store the user authentication information
				var data = new NameValueCollection
				{
					{ "identity", "myUserName" },
					{ "password", "myPassword" },
				};

				// Generate the URL for the API Query and return the response
				var response2 = client.UploadValues(uriBase + "/ajaxauth/login", data);
				var response4 = client.DownloadData(request);

				return (System.Text.Encoding.Default.GetString(response4));
			}
		}   // END GetSpaceTrack()

		public byte[] StartScrap()
        {
            try
            {
                System.Net.WebClient wc = new WebClient();

				wc.Encoding = Encoding.UTF8;
				wc.UseDefaultCredentials = true;
				wc.Credentials = CredentialCache.DefaultCredentials;
				wc.Credentials = new NetworkCredential(@"buro_4@hotmail.com", "buroso89startrack");
				wc.Headers[HttpRequestHeader.UserAgent] = @"Mozilla/5.0 (Windows NT 6.1; WOW64) AppleWebKit/535.2 (KHTML, like Gecko) Chrome/15.0.874.121 Safari/535.2";

				string uriBase = "https://www.space-track.org";
				string request = @"https://www.space-track.org/basicspacedata/query/class/tle_latest/ORDINAL/1/EPOCH/%3Enow-30/orderby/NORAD_CAT_ID/format/3le";

				// Create new WebClient object to communicate with the service
				using (var client = new WebClientEx())
				{
					// Store the user authentication information
					var data = new NameValueCollection
				{
					{ "identity", @"buro_4@hotmail.com" },
					{ "password", "buroso89startrack" },
				};

					// Generate the URL for the API Query and return the response
					var response2 = client.UploadValues(uriBase + "/ajaxauth/login", data);
					var response4 = client.DownloadData(request);

					//return (System.Text.Encoding.Default.GetString(response4));
					return response4;
				}

				//string WebData = wc.DownloadString(@"https://www.space-track.org/basicspacedata/query/class/tle_latest/ORDINAL/1/EPOCH/%3Enow-30/orderby/NORAD_CAT_ID/format/3le");
			}
            catch (Exception ex)
            {
                Debug.WriteLine(ex.ToString());
				return null;
            }
        }

        public byte[] StartHistoricScrap(string sat_ID, uint dataset_lim)
        {
            try
            {
                System.Net.WebClient wc = new WebClient();

                wc.Encoding = Encoding.UTF8;
                wc.UseDefaultCredentials = true;
                wc.Credentials = CredentialCache.DefaultCredentials;
                wc.Credentials = new NetworkCredential(@"buro_4@hotmail.com", "buroso89startrack");
                wc.Headers[HttpRequestHeader.UserAgent] = @"Mozilla/5.0 (Windows NT 6.1; WOW64) AppleWebKit/535.2 (KHTML, like Gecko) Chrome/15.0.874.121 Safari/535.2";

                string uriBase = "https://www.space-track.org";
                string request = string.Format("https://www.space-track.org/basicspacedata/query/class/tle/NORAD_CAT_ID/{0}/orderby/EPOCH desc/limit/{1}/format/tle", sat_ID, dataset_lim);

                // Create new WebClient object to communicate with the service
                using (var client = new WebClientEx())
                {
                    // Store the user authentication information
                    var data = new NameValueCollection
                {
                    { "identity", @"buro_4@hotmail.com" },
                    { "password", "buroso89startrack" },
                };

                    // Generate the URL for the API Query and return the response
                    var response2 = client.UploadValues(uriBase + "/ajaxauth/login", data);
                    var response4 = client.DownloadData(request);

                    wc.Dispose();

                    return response4;
                }
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex.ToString());
                return null;
            }
        }

        public string ParseIntDesignator(uint launchYear, uint launchNumber, string launchPiece)
		{
			string result = "";

			result = launchYear.ToString() + @"-" + launchNumber.ToString("D3") + launchPiece;

			return result;
		}

		public string GetAdditionalSatInfo(uint launchYear, uint launchNumber, string launchPiece)
		{
			try
			{
				string strReq = ParseIntDesignator(launchYear, launchNumber, launchPiece);

				string html = string.Empty;
				string url = @"https://nssdc.gsfc.nasa.gov/nmc/spacecraft/display.action?id=" + strReq;

				HttpWebRequest request = (HttpWebRequest)WebRequest.Create(url);
				request.AutomaticDecompression = DecompressionMethods.GZip;

				using (HttpWebResponse response = (HttpWebResponse)request.GetResponse())
				using (Stream stream = response.GetResponseStream())
				using (StreamReader reader = new StreamReader(stream))
				{
					html = reader.ReadToEnd();
				}

				var htmlDoc = new HtmlAgilityPack.HtmlDocument();
				htmlDoc.LoadHtml(html);

				var htmlBody = htmlDoc.DocumentNode.SelectSingleNode("//body/div/div/div/div/div/p/p").FirstChild;

				return htmlBody.OuterHtml;
			}
			catch (Exception ex)
			{
				Debug.WriteLine(ex.ToString());
				return "NO SATELLITE INFORMATION WAS FOUND";
			}
		}
    }
}

