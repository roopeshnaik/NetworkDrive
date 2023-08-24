using System.Collections;
using System.IO;
using System.Xml;
using System.Data;
using System.Text;
using System.Web;
using System;
using System.Linq;
using System.Collections.Generic;
using System.Reflection;
using System.Configuration;
using System.Web.Configuration;
using System.Text.RegularExpressions;

public class Util
{

    //return application setting as set in web.config
    public static string GetConfigSetting(string strSettingKey)
    {
        return ConfigurationManager.AppSettings[strSettingKey];
    }

    // Gets html from the matched template for audience.
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
    private static string FindMatchingTemplate(Audience audience)
    {
        string filteredTemplates = string.Empty;
        string audienceType = string.Empty;
        string networkDrive = GetConfigSetting("templateLocation");

        try
        {
            // Get the list of files from the target directory.
            string[] templateFiles = Directory.GetFiles(networkDrive);
            audienceType = audience.IsMBAAdmit ? "MBA Admit" : audience.IsMBA ? "MBA MSx" : audience.IsMSxAdmit ? "MSx Admit" : audience.IsMSx ? "MBA MSx" : audience.IsPhDAdmit ? "PhD Admit" : audience.IsPhD ? "PhD" : String.Empty;
            filteredTemplates = FilterTemplates(templateFiles, audience.Name, audienceType);
        }
        catch (Exception ex)
        {
            // logevent for no network drive available.
            string parameters = "Parameter:" + Constants.LINE_BREAK + Constants.LINE_BREAK +
                "Audience: " + audience.Name + Constants.LINE_BREAK + Constants.LINE_BREAK +
                "Network Drive: " + networkDrive + Constants.LINE_BREAK + Constants.LINE_BREAK +
                "Filtered Templates" + filteredTemplates + Constants.LINE_BREAK + Constants.LINE_BREAK;
            //Messaging.SendErrorMail(new Exception(parameters, ex), HttpContext.Current);
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

        if (string.IsNullOrEmpty(template))
        {
            // Log event for no template found.
            logEvent("No template found");
            return "No template found";
        }
        return template;
    }

    public static String SetConfigSetting(string strSettingKey, string value)
    {
        String errorInfo = null;

        try
        {
            Configuration objConfig = WebConfigurationManager.OpenWebConfiguration("~");
            AppSettingsSection objAppsettings = (AppSettingsSection)objConfig.GetSection("appSettings");

            if (objAppsettings != null)
            {
                objAppsettings.Settings[strSettingKey].Value = value;
                objConfig.Save(ConfigurationSaveMode.Modified);
            }
        }
        catch (Exception ex)
        {
            errorInfo = ex.ToString();
        }

        return errorInfo;
    }

    public static void getBlackoutDates(out Boolean enabled, out DateTime dateStart, out DateTime dateEnd)
    {
        String[] blackout = GetConfigSetting("Blackout").Split(' ');
        DateTime now = DateTime.Now;
        Int32 dayNow = (Int32)now.DayOfWeek;
        Int32 dayStart = (Int32)Enum.Parse(typeof(DayOfWeek), blackout[1]);
        Int32 dayEnd = (Int32)Enum.Parse(typeof(DayOfWeek), blackout[4]);
        Int32 hourStart = Convert.ToInt32(blackout[2]);
        Int32 hourEnd = Convert.ToInt32(blackout[5]);

        enabled = blackout[0] == "Enabled";

        //End date
        if (dayEnd > dayNow)
            dateEnd = now.AddDays(dayEnd - dayNow);
        else if (dayEnd == dayNow && hourEnd > now.Hour)
            dateEnd = now;
        else
            dateEnd = now.AddDays(7 - (dayNow - dayEnd));

        dateEnd = new DateTime(dateEnd.Year, dateEnd.Month, dateEnd.Day, hourEnd, 0, 0);

        // Start date
        if (dayEnd > dayStart)
            dateStart = dateEnd.AddDays(dayStart - dayEnd);
        else if (dayEnd == dayStart && hourEnd > hourStart)
            dateStart = dateEnd;
        else
            dateStart = dateEnd.AddDays(dayStart - dayEnd - 7);

        dateStart = new DateTime(dateStart.Year, dateStart.Month, dateStart.Day, hourStart, 0, 0);
    }

    public static string ConvertArrayToString(IList arrayObject, string separator)
    {
        StringBuilder resultString = new StringBuilder();
        if (arrayObject != null)
        {
            foreach (object objectElement in arrayObject)
            {
                if (resultString.Length > 0) resultString.Append(separator);
                resultString.Append(objectElement.ToString());
            }
        }

        return resultString.ToString();
    }

    public static string NullsToEmpty(object val)
    {
        if (val == System.DBNull.Value)
        {
            val = "";
        }
        return (string)val;
    }


    public static bool HasVal(object val)
    {
        if (string.IsNullOrEmpty(NullsToEmpty(val)))
        {
            return false;
        }
        else
        {
            return true;
        }
    }

    public static bool NoVal(object val)
    {
        if (string.IsNullOrEmpty(NullsToEmpty(val)))
        {
            return true;
        }
        else
        {
            return false;
        }
    }


    //******************************************
    //File, path, and filename functions
    //******************************************



    public static string AddBackSlash(string sThePath)
    {
        // Add a backslash to a path if needed
        // sThePath contains the path
        // Can be used as a function or a subroutine

        if (Util.Right(sThePath, 1) != "\\")
        {
            sThePath = sThePath + "\\";
        }
        return sThePath;
    }

    public static string AddForwardSlash(string sThePath)
    {
        // Add a backslash to a path if needed
        // sThePath contains the path
        // Can be used as a function or a subroutine

        if (Util.Right(sThePath, 1) != "/")
        {
            sThePath = sThePath + "/";
        }
        return sThePath;
    }





    public static void NoCache()
    {

        HttpContext.Current.Response.Cache.SetCacheability(HttpCacheability.NoCache);
        HttpContext.Current.Response.Cache.SetAllowResponseInBrowserHistory(false);
    }

    public static void w(string str)
    {
        HttpContext.Current.Response.Write(str);
    }

    public static void wl(string str)
    {
        HttpContext.Current.Response.Write(str + "<br>");
    }

    public static void wl(int val)
    {
        HttpContext.Current.Response.Write(val.ToString() + "<br>");
    }



    public static void we(string str)
    {
        HttpContext.Current.Response.End();
    }

    public static void RedirectWithMsg(string strURL, string strMsg)
    {
        HttpContext.Current.Session["msg"] = strMsg;
        Redirect(strURL);
    }
    public static void Redirect(string strURL)
    {
        HttpContext.Current.Response.Redirect(strURL);
    }

    public static void ShowMsg()
    {
        if (Util.HasVal(HttpContext.Current.Session["msg"]))
        {
            w("<div class=alert align=center>" + HttpContext.Current.Session["msg"] + "</div>");
            HttpContext.Current.Session["msg"] = "";
        }
    }

    public static string GetMsg()
    {
        string s = "";
        if (Util.HasVal(HttpContext.Current.Session["msg"]))
        {
            s = ("<div align=center><div class=alert>" + HttpContext.Current.Session["msg"] + "</div></div>");
            HttpContext.Current.Session["msg"] = "";
        }
        return s;
    }





    ////convert line breaks to <BR> tags
    //public static string ConvertLineBreaksBR(string str)
    //{
    //    return Strings.Replace(str, System.Object.Environement, "<BR>");
    //}

    ////convert <BR> tags to line breaks
    //public static string ConvertBRLineBreaks(string str)
    //{
    //    return Strings.Replace(str, "<BR>", Constants.vbCrLf);
    //}



    //public static string PadNumber(int num)
    //{
    //    if (num < 10)
    //    {
    //        return "0" + (string) num;
    //    }
    //    else
    //    {
    //        return (string)num;
    //    }
    //}

    //public static string CleanXmlData(string str)
    //{
    //    str = Strings.Replace(str, "\"", "&quot;");
    //    return str;
    //}

    /*
    public static void SetDropDown(ref DropDownList list, string value)
    {
        
        try
        {
            list.SelectedValue = value;
        }
        catch (Exception exc)
        {
            Err.Clear();
        }
    }
    */




    //public static string CleanStr(object str)
    //{
    //    if (str == System.DBNull.Value)
    //    {
    //        str = "";
    //    }
    //    str = (string)str;
    //    return str;
    //}



    public static int UrlID()
    {
        if (String.IsNullOrEmpty(HttpContext.Current.Request.QueryString["id"]))
        {
            return 0;
        }
        else
        {
            string val = HttpContext.Current.Request.QueryString["id"];
            int returnVal = 0;
            int.TryParse(val, out returnVal);
            return returnVal;
        }
    }



    /*
    public static string ControlToString(ref Control control)
    {

        StringBuilder SB = new StringBuilder();
        StringWriter SW = new StringWriter(SB);
        HtmlTextWriter htmlTW = new HtmlTextWriter(SW);
        control.RenderControl(htmlTW);

        return SB.ToString();
    }
*/

    public static DateTime? HandleNullDateTime(object val)
    {
        if (val == DBNull.Value)
        {
            //TODO: return null or equivalent
            return null;
        }
        else
        {
            return (DateTime)val;
        }
    }


    public static int BoolToInt(bool val)
    {
        if (val)
        {
            return 1;
        }
        else
        {
            return 0;
        }
    }

    //replace "~" with the AppUrlRoot
    public static object ReplaceUrlRoot(string url)
    {
        url = url.Replace("~", HttpContext.Current.Request.ApplicationPath);
        url = url.Replace("//", "/");
        return url;
    }

    public static XmlDocument DataTableToXML(DataTable dt)
    {
        System.IO.StringWriter sw = new System.IO.StringWriter();
        dt.WriteXml(sw);

        sw.Close();
        XmlDocument xd = new XmlDocument();
        xd.LoadXml(sw.ToString());
        return xd;
    }

    public static XmlDocument DataSetToXML(DataSet ds)
    {
        System.IO.StringWriter sw = new System.IO.StringWriter();
        ds.WriteXml(sw);

        sw.Close();
        XmlDocument xd = new XmlDocument();

        xd.LoadXml(sw.ToString());
        return xd;
    }

    public static string Left(string param, int length)
    {
        //we start at 0 since we want to get the characters starting from the
        //left and with the specified lenght and assign it to a variable
        string result = param.Substring(0, length);
        //return the result of the operation
        return result;
    }
    public static string Right(string param, int length)
    {
        //start at the index based on the lenght of the sting minus
        //the specified lenght and assign it a variable
        string result = param.Substring(param.Length - length, length);
        //return the result of the operation
        return result;
    }

    public static string Mid(string param, int startIndex, int length)
    {
        //start at the specified index in the string ang get N number of
        //characters depending on the lenght and assign it to a variable
        string result = param.Substring(startIndex, length);
        //return the result of the operation
        return result;
    }

    public static string Mid(string param, int startIndex)
    {
        //start at the specified index and return all characters after it
        //and assign it to a variable
        string result = param.Substring(startIndex);
        //return the result of the operation
        return result;
    }

    public static string Request(string param)
    {
        return HttpContext.Current.Request[param];
    }

    public static string Session(string param)
    {
        if (HttpContext.Current.Session[param] == null)
        {
            return "";
        }
        else
        {
            return Util.NullsToEmpty(HttpContext.Current.Session[param].ToString());
        }

    }

    public static void AppendWithComma(ref string str, string appendStr)
    {
        if (str.Length > 0)
            str += ", ";

        str += appendStr;
    }

    public static void AppendRepubReasonWithComma(ref string str, string appendStr)
    {
        if (str.Length > 0)
        {
            str += ", ";
        }
        else if (appendStr.Length > 0)
        {
            str += "This message is republished due to ";
        }


        str += appendStr;
    }

    /// <summary>
    /// Exports the datatable sent as input parameter to excel in binary format.
    /// </summary>
    /// <returns>Void</returns>
    public static void ExportToExcel(DataTable ds, HttpResponse response, string filename)
    {
        response.Clear();
        response.Charset = "";
        response.ContentType = Constants.EXCEL_RESPONSE_TYPE;
        response.AddHeader("Content-Disposition", "filename=" + filename + ".xls;");
        StringWriter strWriter = new StringWriter();
        System.Web.UI.HtmlTextWriter htmlWriter = new System.Web.UI.HtmlTextWriter(strWriter);
        System.Web.UI.WebControls.DataGrid dg = new System.Web.UI.WebControls.DataGrid();
        dg.DataSource = ds;
        dg.DataBind();
        dg.HeaderStyle.Font.Bold = true;
        dg.RenderControl(htmlWriter);
        response.Write(strWriter.ToString());
        response.End();
    }

    /// <summary>
    /// Exports the dataset sent as input parameter to excel in open xml format.Datatables in the dataset are exported to different sheets in the same excel file.
    /// </summary>
    /// <returns>Void</returns>
    public static void ExportToExcelInOpenXmlFormat(IEnumerable tables, HttpResponse Response, string fileName, IDictionary<string, int> dictOutlook, string strtEndDate)
    {
        Response.ClearContent();
        Response.ClearHeaders();
        Response.Buffer = true;
        Response.Charset = "";
        Response.ContentType = Constants.OFFICEXML_EXCEL_RESPONSE_TYPE;
        Response.AddHeader("content-disposition",
                 "attachment; filename=" + fileName + ".xls");



        using (XmlTextWriter x = new XmlTextWriter(Response.OutputStream, Encoding.UTF8))
        {
            int sheetNumber = 0;
            x.WriteRaw("<?xml version=\"1.0\"?><?mso-application progid=\"Excel.Sheet\"?>");
            x.WriteRaw("<Workbook xmlns=\"urn:schemas-microsoft-com:office:spreadsheet\" ");
            x.WriteRaw("xmlns:o=\"urn:schemas-microsoft-com:office:office\" ");
            x.WriteRaw("xmlns:x=\"urn:schemas-microsoft-com:office:excel\">");
            x.WriteRaw("<Styles><Style ss:ID='sText'>" +
                       "<NumberFormat ss:Format='@'/></Style>");
            x.WriteRaw("<Style ss:ID='sDate'><NumberFormat" +
                       " ss:Format='[$-409]m/d/yy\\ h:mm\\ AM/PM;@'/></Style>");
            x.WriteRaw("<Style ss:ID='sBold'>" +
                       "<Font ss:Bold='1'/>");
            x.WriteRaw("</Style></Styles>");

            foreach (DataTable dt in tables)
            {
                sheetNumber++;
                string sheetName = !string.IsNullOrEmpty(dt.TableName) ?
                       dt.TableName : "Sheet" + sheetNumber.ToString();
                x.WriteRaw("<Worksheet ss:Name='" + sheetName + "'>");
                x.WriteRaw("<Table>");
                string[] columnTypes = new string[dt.Columns.Count];

                for (int i = 0; i < dt.Columns.Count; i++)
                {
                    string colType = dt.Columns[i].DataType.ToString().ToLower();

                    if (colType.Contains("datetime"))
                    {
                        columnTypes[i] = "DateTime";
                        x.WriteRaw("<Column ss:StyleID='sDate'/>");

                    }
                    else if (colType.Contains("string"))
                    {
                        columnTypes[i] = "String";
                        x.WriteRaw("<Column ss:StyleID='sText'/>");

                    }
                    else
                    {
                        x.WriteRaw("<Column />");

                        if (colType.Contains("boolean"))
                        {
                            columnTypes[i] = "Boolean";
                        }
                        else
                        {
                            //default is some kind of number.
                            columnTypes[i] = "Number";
                        }

                    }
                }

                //$ rows with summarized info FOR ADD TO Calendar REPORT ONLY
                if (dictOutlook != null)
                {
                    x.WriteRaw("<Row>");
                    x.WriteRaw("</Row>");
                    x.WriteRaw("<Row>");
                    x.WriteRaw("<Cell ss:StyleID='sText'><Data ss:Type='String'>");
                    x.WriteRaw(Constants.ADD_TO_CALENDAR_REPORT_LABEL.ToString() + " " + strtEndDate);
                    x.WriteRaw("</Data></Cell>");
                    x.WriteRaw("</Row>");
                    x.WriteRaw("<Row>");
                    x.WriteRaw("</Row>");
                    foreach (KeyValuePair<string, int> keyValue in dictOutlook)
                    {
                        x.WriteRaw("<Row>");
                        x.WriteRaw("<Cell ss:StyleID='sBold'><Data ss:Type='String'>");

                        switch (keyValue.Key.ToString())
                        {
                            case "event":
                                x.WriteRaw(Constants.ADD_TO_CALENDAR_EVENTS.ToString());
                                break;
                            case "deadline":
                                x.WriteRaw(Constants.ADD_TO_CALENDAR_DEADLINES.ToString());
                                break;
                            case "academic calendar":
                                x.WriteRaw(Constants.ADD_TO_CALENDAR_ACAD_CALENDAR.ToString());
                                break;
                            case "enrolled section":
                                x.WriteRaw(Constants.ADD_TO_CALENDAR_ENROL_SECTION.ToString());
                                break;
                            case "Total Added":
                                x.WriteRaw(Constants.ADD_TO_CALENDAR_TOTAL_ADDED.ToString());
                                break;
                            default:
                                break;
                        }

                        x.WriteRaw("</Data></Cell>");
                        x.WriteRaw("<Cell ss:StyleID='sText'><Data ss:Type='String'>");
                        x.WriteRaw(dictOutlook[keyValue.Key].ToString());
                        x.WriteRaw("</Data></Cell>");
                        x.WriteRaw("</Row>");
                    }
                    x.WriteRaw("<Row>");
                    x.WriteRaw("</Row>");
                }
                //column headers
                x.WriteRaw("<Row>");
                foreach (DataColumn col in dt.Columns)
                {
                    x.WriteRaw("<Cell ss:StyleID='sBold'><Data ss:Type='String'>");
                    x.WriteRaw(col.ColumnName);
                    x.WriteRaw("</Data></Cell>");
                }
                x.WriteRaw("</Row>");
                //data
                bool missedNullColumn = false;
                foreach (DataRow row in dt.Rows)
                {
                    x.WriteRaw("<Row>");
                    for (int i = 0; i < dt.Columns.Count; i++)
                    {
                        if (!row.IsNull(i))
                        {
                            if (missedNullColumn)
                            {
                                int displayIndex = i + 1;
                                x.WriteRaw("<Cell ss:Index='" + displayIndex.ToString() +
                                           "'><Data ss:Type='" +
                                           columnTypes[i] + "'>");
                                missedNullColumn = false;
                            }
                            else
                            {
                                x.WriteRaw("<Cell><Data ss:Type='" +
                                           columnTypes[i] + "'>");
                            }

                            switch (columnTypes[i])
                            {
                                case "DateTime":
                                    x.WriteRaw(((DateTime)row[i]).ToString("s"));
                                    break;
                                case "Boolean":
                                    x.WriteRaw(((bool)row[i]) ? "1" : "0");
                                    break;
                                case "String":
                                    x.WriteString(row[i].ToString());
                                    break;
                                default:
                                    x.WriteString(row[i].ToString());
                                    break;
                            }

                            x.WriteRaw("</Data></Cell>");
                        }
                        else
                        {
                            missedNullColumn = true;
                        }
                    }
                    x.WriteRaw("</Row>");
                }
                x.WriteRaw("</Table></Worksheet>");
            }
            x.WriteRaw("</Workbook>");
        }
        Response.End();
    }

    public static string ExtractNumbers(string expr)
    {
        return string.Join(null, System.Text.RegularExpressions.Regex.Split(expr, "[^\\d]"));
    }

    #region Logging

    public static void logEvent(string logstring)
    {
        try
        {
            throw new Exception(logstring);
        }
        catch (Exception ex)
        {
            ErrorSignal.FromCurrentContext().Raise(ex);
        }
    }

    #endregion

    #region Email Digest & Outlook
    /// <summary>
    /// Format email author link
    /// </summary>
    /// <param name="message">Message object</param>
    /// <param name="linkText">Text for the link</param>
    /// <param name="lineSeparator">Line separator string before Email Author</param>
    /// <param name="cssClass">CSS Class name</param>
    /// <param name="useHTML">Indicate if to format in HTML or not</param>
    /// <returns>Email author html</returns>
    public static string FormatEmailAuthor(Message message, string linkText, string lineSeparator, string cssClass, bool useHTML)
    {
        string emailAuthor = string.Empty;

        if (message.AuthorEmail.Trim().Length > 0)
        {
            if (useHTML)
            {
                emailAuthor += lineSeparator + "<a href=\"mailto:" + message.AuthorEmail + "\">" +
                    "<span" + (cssClass.Trim().Length.Equals(0) ? ">" : " class=\"" + cssClass + "\">") +
                    (linkText.Trim().Length.Equals(0) ? message.AuthorEmail : linkText) + "</span></a>";
            }
            else emailAuthor += lineSeparator + linkText + "mailto:" + message.AuthorEmail;
        }

        return emailAuthor;
    }

    /// <summary>
    /// Format message categories
    /// </summary>
    /// <param name="message">Message object</param>
    /// <returns></returns>
    public static string FormatMessageCategories(Message message)
    {
        string messageCategories = string.Empty;
        foreach (Category messageCategory in message.Categories())
        {
            if (messageCategories.Trim().Length > 0) messageCategories += ", ";
            messageCategories += messageCategory.Name;
        }

        return messageCategories;
    }
    #endregion

    #region User Control

    /// <summary>
    /// Check if the selected audience type is the person's audience type
    /// </summary>
    /// <param name="person">Person object</param>
    /// <param name="selectedAudienceType">Selected audience type</param>
    /// <returns>True/False</returns>
    public static bool IsPersonAudience(Person person, string selectedAudienceType)
    {
        bool isPersonAudience = false;

        if (selectedAudienceType.Trim().Equals(string.Empty)) isPersonAudience = true;
        else
        {
            StringBuilder personAudience = new StringBuilder();
            foreach (Audience audience in person.Audiences)
            {
                if (personAudience.Length > 0) personAudience.Append(",");
                personAudience.Append(audience.Type.Replace(" ", string.Empty).ToLower());
            }

            isPersonAudience = selectedAudienceType.Trim().ToLower().Replace(" ", string.Empty).Equals(personAudience.ToString());
        }

        return isPersonAudience;
    }

    /// <summary>
    /// Persists selected values from navigation control
    /// </summary>
    /// <param name="controlID">Control id</param>
    /// <param name="selectedIndex">Selected index</param>
    /// <param name="selectedValue">Selected value</param>
    /// <param name="selectedLabel">Selected label</param>
    public static void SaveSelectedValues(string controlID, int selectedIndex, string selectedValue, string selectedLabel)
    {
        // Save selected values to session object
        string selectedValues = selectedIndex.ToString() + Constants.SEPARATOR_PIPE + selectedValue +
            Constants.SEPARATOR_PIPE + selectedLabel;
        HttpContext.Current.Session[controlID + Constants.CACHE_KEY_SEPARATOR + "SelectedValues"] = selectedValues;

        // and cookie, if specified (set the cookie to expire in 7 days)
        HttpContext.Current.Response.Cookies[controlID].Value = selectedValues;
        HttpContext.Current.Response.Cookies[controlID].Expires = DateTime.Now.AddDays(7);
    }

    /// <summary>
    /// Retrieve selected values for navigation control
    /// </summary>
    /// <param name="controlID">Control id</param>
    /// <param name="selectedIndex">Selected index</param>
    /// <param name="selectedValue">Selected value</param>
    /// <param name="selectedLabel">Selected label</param>
    /// <returns>Boolean indicate if the saved values were found</returns>
    public static bool TryGetSelectedValues(string controlID, out int selectedIndex, out string selectedValue, out string selectedLabel)
    {
        bool hasSelectedValues = false;
        selectedIndex = 0;
        selectedValue = string.Empty;
        selectedLabel = string.Empty;
        string[] arSelectedValues = null;

        // Retrieve selected values from cookie, if available
        string selectedValues = null;
        if (HttpContext.Current.Response.Cookies[controlID] != null) selectedValues = HttpContext.Current.Response.Cookies[controlID].Value;

        // Otherwise, try session object
        if (selectedValues == null || selectedValues.Trim().Equals(string.Empty))
        {
            if (HttpContext.Current.Session[controlID + Constants.CACHE_KEY_SEPARATOR + "SelectedValues"] != null)
            {
                arSelectedValues = HttpContext.Current.Session[controlID + Constants.CACHE_KEY_SEPARATOR + "SelectedValues"].ToString().Split(new string[] { Constants.SEPARATOR_PIPE },
                    StringSplitOptions.None);
            }
        }
        else arSelectedValues = selectedValues.Split(new string[] { Constants.SEPARATOR_PIPE }, StringSplitOptions.None);

        // Retrieve values from the array
        if (arSelectedValues != null && arSelectedValues.Length > 0)
        {
            selectedIndex = int.Parse(arSelectedValues[0]);
            selectedValue = arSelectedValues[1];
            selectedLabel = arSelectedValues[2];
            hasSelectedValues = true;
        }

        return hasSelectedValues;
    }
    #endregion

    public static bool IsValidEmail(string email)
    {
        string pattern = @"\w+([-+.']\w+)*@\w+([-.]\w+)*\.\w+([-.]\w+)*";

        Regex check = new Regex(pattern, RegexOptions.IgnorePatternWhitespace);

        bool valid = false;

        if (string.IsNullOrEmpty(email))
        {
            valid = false;
        }
        else
        {
            valid = check.IsMatch(email);
        }

        return valid;
    }

    public static bool IsAdmit(Person person)
    {
        bool isAdmit = false;

        if (person != null && person.Audiences != null && person.Audiences.Count > 0 && !person.IsStaff && !person.IsFaculty)
        {
            isAdmit = true;

            foreach (Audience audience in person.Audiences)
            {
                if (audience.IsMBA || audience.IsMSx || audience.IsPhD)
                {
                    isAdmit = false;
                    break;
                }
            }
        }

        return isAdmit;
    }

    #region Authoring tool

    /// <summary>
    /// converts linq resultset to datatable
    /// </summary>    
    /// <param name="message">LINQ resultset</param>
    /// <returns> datatable</returns>
    public static DataTable ConvertLinqResultsetToDataTable<T>(IEnumerable<T> varlist)
    {
        DataTable dtReturn = new DataTable();

        // column names 
        PropertyInfo[] oProps = null;

        if (varlist == null) return dtReturn;

        foreach (T rec in varlist)
        {
            // Use reflection to get property names, to create table, Only first time, others will follow 
            if (oProps == null)
            {
                oProps = ((Type)rec.GetType()).GetProperties();
                foreach (PropertyInfo pi in oProps)
                {
                    Type colType = pi.PropertyType;

                    if ((colType.IsGenericType) && (colType.GetGenericTypeDefinition()
                    == typeof(Nullable<>)))
                    {
                        colType = colType.GetGenericArguments()[0];
                    }

                    dtReturn.Columns.Add(new DataColumn(pi.Name, colType));
                }
            }

            DataRow dr = dtReturn.NewRow();

            foreach (PropertyInfo pi in oProps)
            {
                dr[pi.Name] = pi.GetValue(rec, null) == null ? DBNull.Value : pi.GetValue
                (rec, null);
            }

            dtReturn.Rows.Add(dr);
        }
        return dtReturn;
    }
    #endregion

}
