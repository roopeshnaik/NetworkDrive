using System;
using System.Configuration;
using System.IO;

namespace NetworkDrive
{
    public partial class WebForm1 : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }

        public string GetTemplateFile(string audience, string studentYear)
        {
            string networkDrive = ConfigurationManager.AppSettings["templateLocation"];
            string searchString = audience + studentYear;
            string templatePath = "Template file not found";
            if (Directory.Exists(networkDrive))
            {
                string[] files = Directory.GetFiles(networkDrive);
                foreach (string filePath in files)
                {
                    string fileName = Path.GetFileName(filePath);
                    if (fileName.Contains(searchString))
                    {
                        // File name matches the search string.
                        templatePath = fileName;
                    }
                }
            }
            else
            {
                templatePath = "Network drive not available";
            }

            return templatePath;
        }

        protected void findTemplate_Click(object sender, EventArgs e)
        {
            try
            {
                message.Text = GetTemplateFile(txtAudience.Text, txtYear.Text);
            }
            catch (Exception ex)
            {
                message.Text = ex.Message;
            }
        }
    }
}