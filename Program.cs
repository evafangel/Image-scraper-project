using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace ImageScraper
{
    class Program
    {
        static void Main(string[] args)
        {
            // Set the URL of the website to scrape
            string url = "https://www.example.com/images/";

            // Download the HTML of the website
            string html = GetHtml(url);

            // Extract the URLs of the images from the HTML
            List<string> imageUrls = ExtractImageUrls(html);

            // Download the images and save them to a local folder
            DownloadImages(imageUrls);
        }

        static string GetHtml(string url)
        {
            // Create a WebClient to download the HTML
            using (WebClient client = new WebClient())
            {
                // Download the HTML
                return client.DownloadString(url);
            }
        }

        static List<string> ExtractImageUrls(string html)
        {
            // Create a regular expression to match the src attribute of img tags
            Regex regex = new Regex("src=[\'\"](.+?)[\'\"]");

            // Find all the matches in the HTML
            MatchCollection matches = regex.Matches(html);

            // Create a list to store the image URLs
            List<string> imageUrls = new List<string>();

            // Loop through the matches and add the URL to the list
            foreach (Match match in matches)
            {
                imageUrls.Add(match.Groups[1].Value);
            }

            // Return the list of image URLs
            return imageUrls;
        }

        static void DownloadImages(List<string> imageUrls)
        {
            // Create a WebClient to download the images
            using (WebClient client = new WebClient())
            {
                // Loop through the image URLs
                for (int i = 0; i < imageUrls.Count; i++)
                {
                    // Get the file name from the URL
                    string fileName = Path.GetFileName(imageUrls[i]);

                    // Download the image and save it to a local folder
                    client.DownloadFile(imageUrls[i], "images\\" + fileName);
                }
            }
        }
    }
}
