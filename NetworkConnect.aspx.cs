using System;
using System.Configuration;
using System.IO;
using System.Linq;

namespace NetworkDrive
{
    public partial class WebForm1 : System.Web.UI.Page
    {
        string networkDrive = ConfigurationManager.AppSettings["templateLocation"];
        protected void Page_Load(object sender, EventArgs e)
        {

        }

        private string GetTemplateHtml(string path)
        {
            string templateHtml = "<html><body>Default Text...</body></html>";
            if (File.Exists(path))
            {
                templateHtml = File.ReadAllText(path);
            }
            return templateHtml;
        } 

        protected void findTemplate_Click(object sender, EventArgs e)
        {
            try
            {
                lblTemplatePath.Text = FindMatchingTemplate(audience.Items[audience.SelectedIndex].Value);
                message.Text = GetTemplateHtml(FindMatchingTemplate(audience.Items[audience.SelectedIndex].Value));
            }
            catch (Exception ex)
            {
                message.Text = ex.Message;
            }
        }

        private string FindMatchingTemplate(string audience)
        {
            // Create array of search words for pattern matching.
            string[] searchWords = audience.Split(' ');
            string fileWithMaxMatches = string.Empty;

            if (Directory.Exists(networkDrive))
            {
                // Get the list of files from the target directory.
                string[] files = Directory.GetFiles(networkDrive);


                // Filter files that contain all the search words.
                var matchingFiles = files.Where(file =>
                    searchWords.Any(word => Path.GetFileNameWithoutExtension(file).Contains(word))
                );

                // Find the file with the most word matches.
                int maxMatchCount = 0;

                foreach (var file in matchingFiles)
                {
                    int matchCount = searchWords.Count(word =>
                        Path.GetFileNameWithoutExtension(file).Contains(word)
                    );

                    if (matchCount > maxMatchCount)
                    {
                        maxMatchCount = matchCount;
                        fileWithMaxMatches = file;
                    }
                }
            }
            else
            {
                fileWithMaxMatches = "Network drive not available";
            }
            return fileWithMaxMatches;
        }
    }
}