#region Namespaces
using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Text;
using System.Web;
#endregion

/// <summary>
/// Summary description for EmailDigestServiceUtils
/// </summary>
public class EmailDigestServiceUtils
{
    #region Global Data Members
    private const string QUERY_PARAMS_ADDTOOUTLOOK = "publicid={0}&messageid={1}";
    private const string QUERY_PARAMS_EDITDIGESTFREQUENCY = "publicid={0}&digestFrequency={1}";

    private const string TM_EMAIL_DIGEST_SUBJECT = "ed_subject";

    private const string NBSP = "&nbsp;";

    private const string VAR_DATE = "${date}";
    private const string VAR_AUDIENCE = "${audience}";
    private const string VAR_SECTION_NAME = "${section_name}";
    private const string VAR_TITLE = "${title}";
    private const string VAR_SPONSORS = "${sponsors}";
    private const string VAR_DESCRIPTION = "${description}";
    private const string VAR_EMAIL = "${email}";
    private const string VAR_SUBJECT = "${subject}";
    private const string VAR_DAY = "${day}";
    private const string VAR_MONTH = "${month}";
    private const string VAR_DAY_OF_WEEK = "${day_of_week}";
    private const string VAR_TIME_START = "${time_start}";
    private const string VAR_TIME_END = "${time_end}";
    private const string VAR_DAY_START = "${day_start}";
    private const string VAR_MONTH_START = "${month_start}";
    private const string VAR_DAY_OF_WEEK_START = "${day_of_week_start}";
    private const string VAR_DAY_END = "${day_end}";
    private const string VAR_MONTH_END = "${month_end}";
    private const string VAR_DAY_OF_WEEK_END = "${day_of_week_end}";
    private const string VAR_ADD_TO_CALENDAR_PARAMS = "${add_to_calendar_params}";
    private const string VAR_LOCATION = "${location}";
    private const string VAR_FREQUENCY = "${frequency}";
    private const string VAR_FREQUENCY_PARAMS = "${frequency_params}";
    private const string VAR_NEW = "${new}";
    private const string VAR_CANCELLED = "${cancelled}";
    private const string VAR_UPDATED = "${updated}";

    private const int avgNumDeadline = 3;
    private const int avgNumEvent = 5;
    private const int avgNumAnnounce = 2;

    private const int avgSizeDeadline = 5120;
    private const int avgSizeAnnouce = 2048;
    private const int avgSizeEvent = 8192;

    private StringBuilder mustKnowDeadline = null;
    private StringBuilder mustKnowAnnounce = null;
    private StringBuilder mustKnowEvent = null;

    private StringBuilder wantToKnowDeadline = null;
    private StringBuilder wantToKnowAnnounce = null;
    private StringBuilder wantToKnowEvent = null;

    private StringBuilder alsoHappeningDeadline = null;
    private StringBuilder alsoHappeningAnnounce = null;
    private StringBuilder alsoHappeningEvent = null;

    private StringBuilder htmlBody = null;

    //Most internal section types must go fisrt in the enun
    private enum SectionType
    {
        [Description("${new_green_")]
        new_green_message,
        [Description("${new_yellow_")]
        new_yellow_message,
        [Description("${cancelled_")]
        cancelled_message,
        [Description("${updated_")]
        updated_message,
        [Description("${header_")]
        header,
        [Description("${footer_")]
        footer,
        [Description("${empty_")]
        empty,
        [Description("${must_know_header_")]
        must_know_header,
        [Description("${must_know_title_")]
        must_know_title,
        [Description("${must_know_footer_")]
        must_know_footer,
        [Description("${want_to_know_header_")]
        want_to_know_header,
        [Description("${want_to_know_title_")]
        want_to_know_title,
        [Description("${want_to_know_footer_")]
        want_to_know_footer,
        [Description("${also_happening_header_")]
        also_happening_header,
        [Description("${also_happening_title_")]
        also_happening_title,
        [Description("${also_happening_footer_")]
        also_happening_footer,
        [Description("${deadline_title_")]
        deadline_title,
        [Description("${deadline_detail_")]
        deadline_detail,
        [Description("${announce_header_")]
        announce_header,
        [Description("${announce_footer_")]
        announce_footer,
        [Description("${announce_title_")]
        announce_title,
        [Description("${announce_detail_")]
        announce_detail,
        [Description("${event_header_")]
        event_header,
        [Description("${event_footer_")]
        event_footer,
        [Description("${event_title_")]
        event_title,
        [Description("${event_detail_multi_")]
        event_detail_multi,
        [Description("${event_detail_reg_")]
        event_detail_reg,
        [Description("${event_detail_all_")]
        event_detail_all
    }

    //private TextManager _textManager;
    private string _digestFrequency;
    private DateTime _digestDate;

    List<Template> templates;
    #endregion

    private class Template
    {
        public string name;
        public string html;
        public Dictionary<SectionType, string> sections;
    }

    private void initialize()
    {
        templates = new List<Template>();

        mustKnowDeadline = new StringBuilder(avgSizeDeadline * avgNumDeadline);
        mustKnowAnnounce = new StringBuilder(avgSizeAnnouce * avgNumAnnounce);
        mustKnowEvent = new StringBuilder(avgSizeEvent * avgNumEvent);

        wantToKnowDeadline = new StringBuilder(avgSizeDeadline * avgNumDeadline);
        wantToKnowAnnounce = new StringBuilder(avgSizeAnnouce * avgNumAnnounce);
        wantToKnowEvent = new StringBuilder(avgSizeEvent * avgNumEvent);

        alsoHappeningDeadline = new StringBuilder(avgSizeDeadline * avgNumDeadline);
        alsoHappeningAnnounce = new StringBuilder(avgSizeAnnouce * avgNumAnnounce);
        alsoHappeningEvent = new StringBuilder(avgSizeEvent * avgNumEvent);

        htmlBody = new StringBuilder(3 * (avgSizeDeadline * avgNumDeadline + avgSizeAnnouce * avgNumAnnounce + avgSizeEvent * avgNumEvent) + 8192);
    }

    #region Constructor
    public EmailDigestServiceUtils()
    {
        //_textManager = new TextManager();
        initialize();
    }
    #endregion

    #region Public Methods
    /// <summary>
    /// Send email digest
    /// </summary>
    /// <param name="emailDigestFrequency">Email digest frequency</param>
    /// <param name="showEmphasis">Indicate if to show emphasis</param>
    /// <param name="sendToPublicFolders">Indicate if to send digest to public folders</param>
    /// <param name="recipientSelectionDate">This date is used to exclude recipients who have already gotten the digest</param>
    /// <param name="digestDate">Digest date</param>
    public void SendEmailDigest(string emailDigestFrequency, bool showEmphasis, bool sendToPublicFolders, DateTime recipientSelectionDate,
        DateTime digestDate)
    {
        string emailBody;
        Person person = null;
        Audience audience = null;
        DateTime today = DateTime.Today;

        // Format and send the rest of the email
        emailBody = FormatEmail(person, audience, emailDigestFrequency, today);

    }

    #endregion

    #region Private Methods
    /// <summary>
    /// Encrypt query parameters
    /// </summary>
    /// <param name="person">Person object</param>
    /// <param name="message">Message object</param>
    /// <returns>Encrypted query parameter</returns>
    private string EncryptParams(Person person, Message message)
    {
        string queryParams = string.Format(QUERY_PARAMS_ADDTOOUTLOOK, person.PublicID, message.Id.ToString());
        string encryptedParams = Encryption.Encrypt(queryParams, Settings.EncryptionKey());

        // encode html parameters
        return System.Web.HttpUtility.HtmlEncode(encryptedParams);
    }

    /// <summary>
    /// Encrypt query parameters
    /// </summary>
    /// <param name="person">Person object</param>    
    /// <returns>Encrypted query parameter</returns>
    private string EncryptParams(Person person, string frequency)
    {
        string queryParams = string.Format(QUERY_PARAMS_EDITDIGESTFREQUENCY, person.PublicID, frequency);
        string encryptedParams = Encryption.Encrypt(queryParams, Settings.EncryptionKey());

        // encode html parameters
        return System.Web.HttpUtility.HtmlEncode(encryptedParams);
    }

    /// <summary>
    /// Extract the section and adds it to the template
    /// </summary>
    private void extractSection(SectionType sectionType, Template template)
    {
        string block = sectionType.GetEnumDescription();
        string html = template.html;

        //Find sections from the template, extract them into variables and remove them from the template:
        if (html.Contains(block))
        {
            string startTag = block + "start}";
            string endTag = block + "end}";

            int startIndex = html.IndexOf(startTag);
            int endIndex = html.IndexOf(endTag, startIndex + startTag.Length);
            string section = html.Substring(startIndex + startTag.Length, endIndex - startIndex - startTag.Length);
            html = html.Remove(startIndex, endIndex - startIndex + endTag.Length);

            template.sections.Add(sectionType, section);
            template.html = html;
        }
    }

    private Template getTemplate(Audience audience)
    {
        Template template = templates.Find(t => t.name == audience.Name);

        if (template == null)
        {
            string html = Util.GetTemplateHtml(audience);

            template = new Template();
            template.name = audience.Name;
            template.html = html;
            template.sections = new Dictionary<SectionType, string>();

            foreach (SectionType sectionType in Enum.GetValues(typeof(SectionType)))
                extractSection(sectionType, template);

            templates.Add(template);
        }

        return template;
    }

    private void addMessage(Message message, String msgTempalte, StringBuilder sbDeadline, StringBuilder sbAnounce, StringBuilder sbEvent)
    {
        if (message.MessageType == MessageType.Deadline)
            sbDeadline.Append(msgTempalte);
        else if (message.MessageType == MessageType.Announcement)
            sbAnounce.Append(msgTempalte);
        else
            sbEvent.Append(msgTempalte);
    }

    private void addSection(Template template, String sectionName, StringBuilder sbDeadline, StringBuilder sbAnnounce, StringBuilder sbEvent)
    {
        if (sbDeadline.Length == 0 && sbAnnounce.Length == 0 && sbEvent.Length == 0)
        {
            if (template.sections.ContainsKey(SectionType.empty))
                htmlBody.Append(template.sections[SectionType.empty].Replace(VAR_SECTION_NAME, sectionName));
        }
        else
        {
            if (sbDeadline.Length != 0 && template.sections.ContainsKey(SectionType.deadline_title))
                htmlBody.Append(template.sections[SectionType.deadline_title].Replace(VAR_SECTION_NAME, sectionName)).Append(sbDeadline.ToString());

            if (sbAnnounce.Length != 0 && template.sections.ContainsKey(SectionType.announce_title))
            {
                if (template.sections.ContainsKey(SectionType.announce_header))
                    htmlBody.Append(template.sections[SectionType.announce_header]);

                htmlBody.Append(template.sections[SectionType.announce_title].Replace(VAR_SECTION_NAME, sectionName)).Append(sbAnnounce.ToString());

                if (template.sections.ContainsKey(SectionType.announce_footer))
                    htmlBody.Append(template.sections[SectionType.announce_footer]);
            }

            if (sbEvent.Length != 0 && template.sections.ContainsKey(SectionType.event_title))
            {
                if (template.sections.ContainsKey(SectionType.event_header))
                    htmlBody.Append(template.sections[SectionType.event_header]);

                htmlBody.Append(template.sections[SectionType.event_title].Replace(VAR_SECTION_NAME, sectionName)).Append(sbEvent.ToString());

                if (template.sections.ContainsKey(SectionType.event_footer))
                    htmlBody.Append(template.sections[SectionType.event_footer]);
            }
        }
    }

    /// <summary>
    /// Format email message.
    /// <param name="person">Person object</param>
    /// <param name="audience">Audience object</param>
    /// <param name="emailDigestFrequency">Email digest frequency</param>
    /// <param name="messageSelectDate">Date from which the messages are to be selected</param>
    /// <returns>Html email string</returns>
    private string FormatEmail(Person person, Audience audience, string emailDigestFrequency, DateTime messageSelectDate)
    {
        string emailBody = string.Empty;
        string msgTemplate = String.Empty;
        DateTime msgDateTime;
        bool isNew;

        Template template = getTemplate(audience);

        if (template.html == String.Empty)
            return String.Empty;

        // Get messages
        ArrayList messages;
        if (messageSelectDate == null || messageSelectDate.Equals(DateTime.MinValue) || messageSelectDate.Equals(DateTime.MaxValue) ||
            audience == null)
        {
            messages = (ArrayList)person.EmailDigestMessages();
        }
        else if (audience != null && audience.Type != null && audience.Type.Trim().Length > 0)
        {
            messages = (ArrayList)person.EmailDigestMessages(new string[] { audience.Type }, messageSelectDate);
        }
        else messages = (ArrayList)person.EmailDigestMessages(messageSelectDate);

        // Process messages
        int messageProcessed = 0;
        int requiredMessageProcessed = 0;

        if (messages.Count > 0)
        {
            mustKnowDeadline.Clear();
            mustKnowAnnounce.Clear();
            mustKnowEvent.Clear();

            wantToKnowDeadline.Clear();
            wantToKnowAnnounce.Clear();
            wantToKnowEvent.Clear();

            alsoHappeningDeadline.Clear();
            alsoHappeningAnnounce.Clear();
            alsoHappeningEvent.Clear();

            htmlBody.Clear();

            if (template.sections.ContainsKey(SectionType.header))
                htmlBody.Append(template.sections[SectionType.header].Replace(VAR_DATE, String.Format("{0:MMM d, yyyy}", _digestDate)).Replace(VAR_AUDIENCE, audience.Name));

            if (template.sections.ContainsKey(SectionType.must_know_header))
                htmlBody.Append(template.sections[SectionType.must_know_header]);

            if (template.sections.ContainsKey(SectionType.must_know_title))
                htmlBody.Append(template.sections[SectionType.must_know_title]);

            // Format each message
            foreach (Message message in messages)
            {
                if (message.IsPublished || (message.IsCanceled && Settings.EmailDigestIncludeCanceled(audience.Type)))
                {
                    messageProcessed++;
                    msgTemplate = null;
                    msgDateTime = new DateTime(_digestDate.Year, _digestDate.Month, _digestDate.Day, 20, 0, 0);
                    isNew = _digestFrequency == EmailDigestFrequency.Daily ? message.PublishDate >= msgDateTime.AddHours(-24) : message.PublishDate >= msgDateTime.AddDays(-7);

                    if (message.MessageType == MessageType.Deadline)
                    {
                        if (template.sections.ContainsKey(SectionType.deadline_detail))
                        {
                            msgTemplate = template.sections[SectionType.deadline_detail];

                            msgTemplate = msgTemplate.Replace(VAR_DAY, message.StartDateTime.Value.Day.ToString());
                            msgTemplate = msgTemplate.Replace(VAR_MONTH, String.Format("{0:MMM}", message.StartDateTime).ToUpper());
                            msgTemplate = msgTemplate.Replace(VAR_DAY_OF_WEEK, String.Format("{0:ddd}", message.StartDateTime));
                            msgTemplate = msgTemplate.Replace(VAR_TIME_START, String.Format("{0:h:mm tt}", message.StartDateTime));
                        }
                    }
                    else if (message.MessageType == MessageType.Announcement)
                    {
                        if (template.sections.ContainsKey(SectionType.announce_detail))
                        {
                            msgTemplate = template.sections[SectionType.announce_detail];
                        }
                    }
                    else
                    {
                        if (message.IsMultiDay)
                        {
                            if (template.sections.ContainsKey(SectionType.event_detail_multi))
                            {
                                msgTemplate = template.sections[SectionType.event_detail_multi];

                                msgTemplate = msgTemplate.Replace(VAR_DAY_END, message.EndDateTime.Value.Day.ToString());
                                msgTemplate = msgTemplate.Replace(VAR_MONTH_END, String.Format("{0:MMM}", message.EndDateTime).ToUpper());
                                msgTemplate = msgTemplate.Replace(VAR_DAY_OF_WEEK_END, String.Format("{0:ddd}", message.EndDateTime));
                            }
                        }
                        else if (message.IsAllDay)
                        {
                            if (template.sections.ContainsKey(SectionType.event_detail_all))
                                msgTemplate = template.sections[SectionType.event_detail_all];
                        }
                        else
                        {
                            if (template.sections.ContainsKey(SectionType.event_detail_reg))
                            {
                                msgTemplate = template.sections[SectionType.event_detail_reg];

                                msgTemplate = msgTemplate.Replace(VAR_TIME_START, String.Format("{0:h:mm tt}", message.StartDateTime));
                                msgTemplate = msgTemplate.Replace(VAR_TIME_END, String.Format("{0:h:mm tt}", message.EndDateTime));
                            }
                        }

                        if (msgTemplate != null)
                        {
                            msgTemplate = msgTemplate.Replace(VAR_DAY_START, message.StartDateTime.Value.Day.ToString());
                            msgTemplate = msgTemplate.Replace(VAR_MONTH_START, String.Format("{0:MMM}", message.StartDateTime).ToUpper());
                            msgTemplate = msgTemplate.Replace(VAR_DAY_OF_WEEK_START, String.Format("{0:ddd}", message.StartDateTime));

                            msgTemplate = msgTemplate.Replace(VAR_LOCATION, message.Location);
                        }
                    }

                    if (msgTemplate != null)
                    {
                        if (message.MessageType == MessageType.Deadline || message.MessageType == MessageType.Event)
                            msgTemplate = msgTemplate.Replace(VAR_ADD_TO_CALENDAR_PARAMS, EncryptParams(person, message));

                        msgTemplate = msgTemplate.Replace(VAR_TITLE, message.Title);

                        msgTemplate = msgTemplate.Replace(VAR_SPONSORS, Util.FormatMessageCategories(message));
                        msgTemplate = msgTemplate.Replace(VAR_DESCRIPTION, message.Description);
                        msgTemplate = msgTemplate.Replace(VAR_EMAIL, message.AuthorEmail);
                        msgTemplate = msgTemplate.Replace(VAR_SUBJECT, "re: " + message.Title);

                        // New message.
                        if (message.Mandatory && template.sections.ContainsKey(SectionType.new_green_message))
                            msgTemplate = msgTemplate.Replace(VAR_NEW, isNew ? template.sections[SectionType.new_green_message] : "");
                        else if (template.sections.ContainsKey(SectionType.new_yellow_message))
                            msgTemplate = msgTemplate.Replace(VAR_NEW, isNew ? template.sections[SectionType.new_yellow_message] : "");

                        // Canceled/Updated
                        if (template.sections.ContainsKey(SectionType.cancelled_message))
                            msgTemplate = msgTemplate.Replace(VAR_CANCELLED, message.IsCanceled ? template.sections[SectionType.cancelled_message] : "");
                        if (template.sections.ContainsKey(SectionType.updated_message))
                            msgTemplate = msgTemplate.Replace(VAR_UPDATED, (message.RepublishDate != null) ? template.sections[SectionType.updated_message] : "");

                        if (audience.IsAdmit)
                            addMessage(message, msgTemplate, mustKnowDeadline, mustKnowAnnounce, mustKnowEvent);
                        else
                        {
                            if (message.Mandatory)
                            {
                                requiredMessageProcessed++;
                                addMessage(message, msgTemplate, mustKnowDeadline, mustKnowAnnounce, mustKnowEvent);
                            }
                            else if (String.IsNullOrEmpty(message.FomoStatus))
                                addMessage(message, msgTemplate, alsoHappeningDeadline, alsoHappeningAnnounce, alsoHappeningEvent);
                            else if (message.FomoStatus == "Subscribe")
                                addMessage(message, msgTemplate, wantToKnowDeadline, wantToKnowAnnounce, wantToKnowEvent);
                        }
                    }
                }
            }

            if (audience.IsAdmit)
            {
                if (mustKnowDeadline.Length != 0 && template.sections.ContainsKey(SectionType.deadline_title))
                    htmlBody.Append(template.sections[SectionType.deadline_title]).Append(mustKnowDeadline.ToString());

                if (mustKnowAnnounce.Length != 0 && template.sections.ContainsKey(SectionType.announce_title))
                    htmlBody.Append(template.sections[SectionType.announce_title]).Append(mustKnowAnnounce.ToString());

                if (mustKnowEvent.Length != 0 && template.sections.ContainsKey(SectionType.event_title))
                    htmlBody.Append(template.sections[SectionType.event_title]).Append(mustKnowEvent.ToString());
            }
            else
            {
                addSection(template, "Must know", mustKnowDeadline, mustKnowAnnounce, mustKnowEvent);

                if (template.sections.ContainsKey(SectionType.must_know_footer))
                    htmlBody.Append(template.sections[SectionType.must_know_footer]);

                if (template.sections.ContainsKey(SectionType.want_to_know_header))
                {
                    htmlBody.Append(template.sections[SectionType.want_to_know_header]);

                    if (template.sections.ContainsKey(SectionType.want_to_know_title))
                        htmlBody.Append(template.sections[SectionType.want_to_know_title]);

                    addSection(template, "Want to know", wantToKnowDeadline, wantToKnowAnnounce, wantToKnowEvent);

                    if (template.sections.ContainsKey(SectionType.want_to_know_footer))
                        htmlBody.Append(template.sections[SectionType.want_to_know_footer]);
                }

                if (template.sections.ContainsKey(SectionType.also_happening_header))
                {
                    htmlBody.Append(template.sections[SectionType.also_happening_header]);

                    if (template.sections.ContainsKey(SectionType.also_happening_title))
                        htmlBody.Append(template.sections[SectionType.also_happening_title]);

                    addSection(template, "Also happening", alsoHappeningDeadline, alsoHappeningAnnounce, alsoHappeningEvent);

                    if (template.sections.ContainsKey(SectionType.also_happening_footer))
                        htmlBody.Append(template.sections[SectionType.also_happening_footer]);
                }
            }

            string strFrequency = person.DigestFrequency.ToUpper() == "WEEKLY" ? "daily" : "weekly";
            string queryParams = EncryptParams(person, strFrequency);

            if (template.sections.ContainsKey(SectionType.footer))
                htmlBody.Append(template.sections[SectionType.footer].Replace(VAR_FREQUENCY, strFrequency).Replace(VAR_FREQUENCY_PARAMS, queryParams));

            emailBody = htmlBody.ToString();
        }

        return emailBody;
    }

    /// <summary>
    /// Format recipient emails
    /// </summary>
    /// <param name="externalRecipientEmails">External recipient emails</param>
    /// <param name="audience">Audience object</param>
    /// <param name="person">Person object</param>
    /// <param name="recipientEmails">Recipient emails</param>
    /// <param name="recipientNames">Recipient names</param>
    private void FormatRecipientEmails(string externalRecipientEmails, Audience audience, Person person, out string[] recipientEmails,
        out string[] recipientNames)
    {
        recipientEmails = null;
        recipientNames = null;

        if (externalRecipientEmails != null && externalRecipientEmails.Trim().Length > 0)
        {
            recipientEmails = externalRecipientEmails.Split(Constants.EMAIL_ADDRESS_SEPARATOR.ToCharArray());
        }
        else if (person.Univid != null)
        {
            // For ADMIT, we need to send to the secondary email address as well if it is different from the primary email address
            if (audience.IsAdmit && !person.EmailPrimary.Trim().ToLower().Equals(person.EmailSecondary.Trim().ToLower()))
            {
                recipientEmails = new string[] { person.EmailPrimary, person.EmailSecondary };
                recipientNames = new string[] { person.NameDisplay, person.NameDisplay };
            }
            else
            {
                recipientEmails = new string[] { person.EmailPrimary };
                recipientNames = new string[] { person.NameDisplay };
            }

        }
    }

    /// <summary>
    /// Get Text Manager Content
    /// </summary>
    /// <param name="contentId">Content Id</param>
    /// <param name="emailDigestFrequency">Email digest frequency</param>
    /// <param name="audience">Audience object</param>
    /// <returns>Text Manager Content</returns>
    private string GetTextManagerContent(string contentId, string emailDigestFrequency, Audience audience)
    {
        string tmContent = string.Empty;

        emailDigestFrequency = emailDigestFrequency == null ? string.Empty : emailDigestFrequency;
        string audienceType = (audience == null || (audience != null && audience.Type == null)) ? string.Empty : audience.Type;
        string cacheKey = CacheManager.CreateCacheKey(new string[] { contentId, emailDigestFrequency, audienceType });

        if (CacheManager.GetCache(cacheKey) == null)
        {
            if (emailDigestFrequency.Trim().Length > 0 && audienceType.Trim().Length > 0) tmContent = _textManager.GetContent(contentId +
                Constants.TEXT_MANAGER_ID_SEPARATOR + emailDigestFrequency + Constants.TEXT_MANAGER_ID_SEPARATOR + audienceType);
            if (tmContent.Trim().Length.Equals(0) && emailDigestFrequency.Trim().Length > 0) tmContent = _textManager.GetContent(contentId +
                Constants.TEXT_MANAGER_ID_SEPARATOR + emailDigestFrequency);
            if (tmContent.Trim().Length.Equals(0) && audienceType.Trim().Length > 0) tmContent = _textManager.GetContent(contentId +
                Constants.TEXT_MANAGER_ID_SEPARATOR + audienceType);
            if (tmContent.Trim().Length.Equals(0))
            {
                // Remove "year of" from audience type
                int audienceYear = 0;
                if (int.TryParse(audienceType.Substring(audienceType.Length - 4), out audienceYear))
                {
                    string audienceTypeWithoutYear = audienceType.Substring(0, audienceType.Length - 4);
                    tmContent = _textManager.GetContent(contentId + Constants.TEXT_MANAGER_ID_SEPARATOR + audienceTypeWithoutYear);
                    if (tmContent.Trim().Length.Equals(0))
                    {
                        // Remove "round" for admit
                        if (audience.IsAdmit)
                        {
                            string round = audienceTypeWithoutYear.Substring(audienceTypeWithoutYear.Length - 2).ToLower();
                            if (round.Equals("r1") || round.Equals("r2") || round.Equals("r3"))
                            {
                                string audienceTypeWithoutRoundAndYear = audienceTypeWithoutYear.Substring(0, audienceTypeWithoutYear.Length - 2);
                                tmContent = _textManager.GetContent(contentId + Constants.TEXT_MANAGER_ID_SEPARATOR + audienceTypeWithoutRoundAndYear);
                                // Audience type with only round removed
                                if (tmContent.Trim().Length.Equals(0))
                                {
                                    tmContent = _textManager.GetContent(contentId + Constants.TEXT_MANAGER_ID_SEPARATOR + audienceTypeWithoutRoundAndYear + audienceYear.ToString());
                                }
                            }
                        }
                    }
                }
            }
            if (tmContent.Trim().Length.Equals(0)) tmContent = _textManager.GetContent(contentId);
            tmContent = tmContent.Replace(Constants.TEXT_MANAGER_CONTENT_SPACE, NBSP).Replace(Constants.TEXT_MANAGER_CONTENT_BLANK, string.Empty);

            CacheManager.InsertCache(cacheKey, Constants.EMAIL_DIGEST_DEPENDENCE_CACHE_KEY, tmContent, true);
        }
        else tmContent = (string)CacheManager.GetCache(cacheKey);

        return tmContent;
    }

    ///<summary>
    ///Update email digest frequency, call made from link in email body
    ///<param name ="univid">
    /// <returns>A status (0 - success, 1 - fail)</returns>
    public int UpdateDigestFrequency(string univid)
    {
        int status = 0;
        Person person = new Person(univid);


        string parameters = string.Empty;
        Exception digestException = null;
        string exceptionSource = "EmailDigestServiceUtils.UpdateDigestFrequency";
        DAL oDAL = new DAL();
        try
        {
            oDAL.SetPersonEmailDigestFreqeuncy(univid);
        }
        catch (Exception ex)
        {
            parameters = "Parameters:" + Constants.LINE_BREAK + Constants.LINE_BREAK +
           "Message: " + "Updating Email digest frequency Failed" + Constants.LINE_BREAK +
           "Email Digest Frequency: " + person.DigestFrequency + Constants.LINE_BREAK +
           "Univid: " + univid + Constants.LINE_BREAK +
            Constants.LINE_BREAK;
            digestException = new Exception(parameters, ex);
            digestException.Source = exceptionSource;
            Messaging.SendErrorMail(digestException, HttpContext.Current);
            status = 1;
        }

        // Return result
        return status;
    }

    #endregion
}
