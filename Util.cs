using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text.RegularExpressions;
using System.Web;

namespace NetworkDrive
{
    using System;
    using System.Configuration;
    using System.IO;
    using System.Text.RegularExpressions;

    class Util
    {
        public static string GetConfigSetting(string strSettingKey)
        {
            return ConfigurationManager.AppSettings[strSettingKey];
        }

        public static string GetTemplateHtml(Audience audience)
        {
            string templateHtml = string.Empty;
            string templatePath = FindMatchingTemplate(audience);
            if (File.Exists(templatePath))
            {
                templateHtml = File.ReadAllText(templatePath);
            }
            return templateHtml;
        }

        // Finds matching template for audience from network drive.
        public static string FindMatchingTemplate(Audience audience)
        {
            string filteredTemplates = string.Empty;
            string audienceType = string.Empty;
            string networkDrive = GetConfigSetting("templateLocation");

            if (Directory.Exists(networkDrive))
            {
                // Get the list of files from the target directory.
                string[] templateFiles = Directory.GetFiles(networkDrive);

                if (audience.IsMBA) audienceType = "MBA";
                if (audience.IsMBAAdmit) audienceType = "MBA Admit";
                if (audience.IsMSx) audienceType = "MSx";
                if (audience.IsMSxAdmit) audienceType = "MSx Admit";
                if (audience.IsPhD) audienceType = "PhD";
                if (audience.IsPhDAdmit) audienceType = "PhD Admit";
                filteredTemplates = FilterTemplates(templateFiles, audience.AudienceName, audienceType);
            }
            else
            {
                // logevent for no network drive available.
            }

            return filteredTemplates.ToString();
        }

        // Filter templates based on audience search.
        private static string FilterTemplates(string[] templateFiles, string searchString, string filterCriteria)
        {
            var template = string.Empty;

            // Filter files based on the filter criteria string.
            var matchedFiles = templateFiles
                .Where(fileName => fileName.Contains(filterCriteria))
                .ToList();

            if (matchedFiles.Count > 1)
            {
                string year = Regex.Match(searchString, @"\d+").Value;
                foreach (var file in matchedFiles)
                {
                    if (file.Any(char.IsDigit))
                    {
                        if (file.Contains(year))
                        {
                            template = file;
                            break;
                        }
                    }
                    else { template = file; }
                }
            }
            else
            {
                template = matchedFiles.FirstOrDefault();
            }

            if (!string.IsNullOrEmpty(template))
            {
                // Log event for no template found.
                logEvent("No template found");
            }
            return template;
        }

        public static void Test(string directoryPath, string searchTerm)
        {
            string[] files = Directory.GetFiles(directoryPath);
            string closestMatch = null;
            int closestDistance = int.MaxValue;

            foreach (string file in files)
            {
                string fileName = Path.GetFileName(file);
                int distance = LevenshteinDistance(fileName, searchTerm);

                if (distance < closestDistance)
                {
                    closestMatch = file;
                    closestDistance = distance;
                }
            }

            if (closestMatch != null)
            {
                Console.WriteLine("Closest matching file: " + closestMatch);
            }
            else
            {
                Console.WriteLine("No matching file found.");
            }
        }

        static int LevenshteinDistance(string s, string t)
        {
            int m = s.Length;
            int n = t.Length;
            int[,] dp = new int[m + 1, n + 1];

            for (int i = 0; i <= m; i++)
            {
                dp[i, 0] = i;
            }

            for (int j = 0; j <= n; j++)
            {
                dp[0, j] = j;
            }

            for (int i = 1; i <= m; i++)
            {
                for (int j = 1; j <= n; j++)
                {
                    int cost = (s[i - 1] == t[j - 1]) ? 0 : 1;
                    dp[i, j] = Math.Min(Math.Min(dp[i - 1, j] + 1, dp[i, j - 1] + 1), dp[i - 1, j - 1] + cost);
                }
            }

            return dp[m, n];
        }
        public static int CountMatchingWords(string text, string[] words)
        {
            // Convert the text to lowercase for case-insensitive search
            text = text.ToLower();

            // Count the occurrences of each word
            int count = words.Sum(word => CountOccurrences(text, word.ToLower()));

            return count;
        }

        static int CountOccurrences(string text, string word)
        {
            int count = 0;
            int index = text.IndexOf(word, StringComparison.Ordinal);
            while (index != -1)
            {
                count++;
                index = text.IndexOf(word, index + 1, StringComparison.Ordinal);
            }
            return count;
        }
        public static void logEvent(string logstring)
        {
            try
            {
                throw new Exception(logstring);
            }
            catch (Exception ex)
            {

            }
        }

    }
}