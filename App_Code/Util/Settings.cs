using System;
using System.Data;
using System.Configuration;
using System.Linq;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Xml.Linq;

/// <summary>
/// Summary description for Settings
/// </summary>
public class Settings
{
    #region Constructor
    public Settings()
	{
		//
		// TODO: Add constructor logic here
		//
    }
    #endregion

    #region Get Application Settings
    /// <summary>
    /// Retrieve "no message" text
    /// </summary>
    /// <param name="messageType">Message Type. E.g. all, subscription, required, audience (use to retrieve audience typed message)</param>
    /// <param name="viewFilter">Message View Filter. E.g. all, deadlines, events, announcements, (staff, unidentified) --> applicable to audience typed message only</param>
    /// <param name="selectedCategoryCount">No of categories selected; 0 (zero) for audience typed message</param>
    /// <param name="hasSubscription">User subscribed to any of the selected categories (applicable when at least one category is selected); false for audience typed message</param>
    /// <returns></returns>
    //public static string NoMessageText(string messageType, string viewFilter, int selectedCategoryCount, bool hasSubscription)
    //{
    //    string noMessageText = string.Empty;
    //    string settingKey = string.Format("no.message.text.{0}.{1}", messageType, viewFilter);
    //    if (selectedCategoryCount.Equals(1)) settingKey += ".category";
    //    else if (selectedCategoryCount > 1) settingKey += ".categories";
    //    if (selectedCategoryCount > 0)
    //    {
    //        if (hasSubscription) settingKey += ".subscribed";
    //        else settingKey += ".unsubscribed";
    //    }
    //    string cacheKey = CacheManager.CreateCacheKey(new string[] { settingKey });

    //    if (CacheManager.GetCache(cacheKey) == null)
    //    {
    //        DAL oDAL = new DAL();

    //        noMessageText = oDAL.GetAppSetting(settingKey);

    //        oDAL.Dispose();

    //        // Add to cache
    //        CacheManager.InsertCache(cacheKey, Constants.EMAIL_DIGEST_DEPENDENCE_CACHE_KEY, noMessageText, true);
    //    }
    //    else noMessageText = (string)CacheManager.GetCache(cacheKey);

    //    return noMessageText;
    //}

    //public static int AcademicCalendarRetry()
    //{
    //    int academicCalendarRetry = 0;
    //    string settingKey = "academic.calendar.retry";
    //    string cacheKey = CacheManager.CreateCacheKey(new string[] { settingKey });

    //    if (CacheManager.GetCache(cacheKey) == null)
    //    {
    //        DAL oDAL = new DAL();

    //        try
    //        {
    //            academicCalendarRetry = int.Parse(oDAL.GetAppSetting(settingKey));
    //        }
    //        catch { }

    //        oDAL.Dispose();

    //        // Add to cache
    //        CacheManager.InsertCache(cacheKey, Constants.EMAIL_DIGEST_DEPENDENCE_CACHE_KEY, academicCalendarRetry, true);
    //    }
    //    else academicCalendarRetry = (int)CacheManager.GetCache(cacheKey);

    //    return academicCalendarRetry;
    //}

    //public static int AcademicCalendarRetrySleep()
    //{
    //    int academicCalendarRetrySleep = 0;
    //    string settingKey = "academic.calendar.retry.sleep.seconds";
    //    string cacheKey = CacheManager.CreateCacheKey(new string[] { settingKey });

    //    if (CacheManager.GetCache(cacheKey) == null)
    //    {
    //        DAL oDAL = new DAL();

    //        try
    //        {
    //            academicCalendarRetrySleep = int.Parse(oDAL.GetAppSetting(settingKey));
    //        }
    //        catch { }

    //        oDAL.Dispose();

    //        // Add to cache
    //        CacheManager.InsertCache(cacheKey, Constants.EMAIL_DIGEST_DEPENDENCE_CACHE_KEY, academicCalendarRetrySleep, true);
    //    }
    //    else academicCalendarRetrySleep = (int)CacheManager.GetCache(cacheKey);

    //    return academicCalendarRetrySleep;
    //}

    public static string[] AcademicCalendarSelectedAudiences(bool useCache)
    {
        string[] academicCalendarSelectedAudiences = null;
        //string settingKey = "academic.calendar.selected.audiences";
        //string cacheKey = CacheManager.CreateCacheKey(new string[] { settingKey });

        //// Get value from cache
        //if (useCache && CacheManager.GetCache(cacheKey) != null)
        //{
        //    academicCalendarSelectedAudiences = ((string)CacheManager.GetCache(cacheKey)).Split(new string[] { Constants.SEPARATOR_COMMA }, 
        //        StringSplitOptions.RemoveEmptyEntries);
        //}

        //// Get value from DB
        //if (academicCalendarSelectedAudiences == null || academicCalendarSelectedAudiences.Length == 0)
        //{
        //    string val;
        //    DAL oDAL = new DAL();
        //    val = oDAL.GetAppSetting(settingKey);
        //    oDAL.Dispose();

        //    if (val.Trim().Length > 0)
        //    {
        //        academicCalendarSelectedAudiences = val.Split(new string[] { Constants.SEPARATOR_COMMA }, StringSplitOptions.RemoveEmptyEntries);

        //        // save to cache
        //        if (useCache) CacheManager.InsertCache(cacheKey, Constants.APPLICATION_DEPENDENCE_CACHE_KEY, val, true);
        //    }
        //}

        return academicCalendarSelectedAudiences;
    }

    //public static string EmailAddToOutlookUrl()
    //{
    //    string addToOutlookUrl = string.Empty;
    //    string settingKey = "email.add.to.outlook.url";
    //    string cacheKey = CacheManager.CreateCacheKey(new string[] { settingKey });

    //    if (CacheManager.GetCache(cacheKey) == null)
    //    {
    //        DAL oDAL = new DAL();
             
    //        addToOutlookUrl = oDAL.GetAppSetting(settingKey);

    //        oDAL.Dispose();

    //        // Add to cache
    //        CacheManager.InsertCache(cacheKey, Constants.EMAIL_DIGEST_DEPENDENCE_CACHE_KEY, addToOutlookUrl, true);
    //    }
    //    else addToOutlookUrl = (string)CacheManager.GetCache(cacheKey);

    //    return addToOutlookUrl;
    //}

    public static bool EmailDigestEnabled(string audienceType)
    {
        //bool emailDigestEnabled = false;
        //string settingKey = "email.digest.enabled";

        //audienceType = audienceType == null ? string.Empty : audienceType;
        //string cacheKey = CacheManager.CreateCacheKey(new string[] { settingKey, audienceType });

        //if (CacheManager.GetCache(cacheKey) == null)
        //{
        //    DAL oDAL = new DAL();
        //    string val;

        //    val = oDAL.GetAppSetting(settingKey + Constants.APPLICATION_SETTING_KEY_SEPARATOR + audienceType);
        //    if (val.Trim().Length.Equals(0)) val = oDAL.GetAppSetting(settingKey);

        //    oDAL.Dispose();

        //    if (val.Trim().ToLower().Equals(Constants.TRUE.ToLower())) emailDigestEnabled = true;
        //    else emailDigestEnabled = false;

        //    // Add to cache
        //    CacheManager.InsertCache(cacheKey, Constants.EMAIL_DIGEST_DEPENDENCE_CACHE_KEY, emailDigestEnabled, true);
        //}
        //else emailDigestEnabled = (bool)CacheManager.GetCache(cacheKey);

        return true;
    }

    //public static bool EmailDigestEnableContentLinks(string audienceType)
    //{
    //    bool emailDigestEnableContentLinks = false;
    //    string settingKey = "email.digest.enable.content.links";

    //    audienceType = audienceType == null ? string.Empty : audienceType;
    //    string cacheKey = CacheManager.CreateCacheKey(new string[] { settingKey, audienceType });

    //    if (CacheManager.GetCache(cacheKey) == null)
    //    {
    //        DAL oDAL = new DAL();
    //        string val;

    //        val = oDAL.GetAppSetting(settingKey + Constants.APPLICATION_SETTING_KEY_SEPARATOR + audienceType);
    //        if (val.Trim().Length.Equals(0)) val = oDAL.GetAppSetting(settingKey);

    //        oDAL.Dispose();

    //        if (val.Trim().ToLower().Equals(Constants.TRUE.ToLower())) emailDigestEnableContentLinks = true;
    //        else emailDigestEnableContentLinks = false;

    //        // Add to cache
    //        CacheManager.InsertCache(cacheKey, Constants.EMAIL_DIGEST_DEPENDENCE_CACHE_KEY, emailDigestEnableContentLinks, true);
    //    }
    //    else emailDigestEnableContentLinks = (bool)CacheManager.GetCache(cacheKey);

    //    return emailDigestEnableContentLinks;
    //}

    public static bool EmailDigestIncludeCanceled(string audienceType)
    {
        //bool emailDigestIncludeCanceled = false;
        //string settingKey = "email.digest.include.canceled";

        //audienceType = audienceType == null ? string.Empty : audienceType;
        //string cacheKey = CacheManager.CreateCacheKey(new string[] { settingKey, audienceType });

        //if (CacheManager.GetCache(cacheKey) == null)
        //{
        //    DAL oDAL = new DAL();
        //    string val;

        //    val = oDAL.GetAppSetting(settingKey + Constants.APPLICATION_SETTING_KEY_SEPARATOR + audienceType);
        //    if (val.Trim().Length.Equals(0)) val = oDAL.GetAppSetting(settingKey);

        //    oDAL.Dispose();

        //    if (val.Trim().ToLower().Equals(Constants.TRUE.ToLower())) emailDigestIncludeCanceled = true;
        //    else emailDigestIncludeCanceled = false;

        //    // Add to cache
        //    CacheManager.InsertCache(cacheKey, Constants.EMAIL_DIGEST_DEPENDENCE_CACHE_KEY, emailDigestIncludeCanceled, true);
        //}
        //else emailDigestIncludeCanceled = (bool)CacheManager.GetCache(cacheKey);

        return false;
    }

    public static bool EmailDigestIncludeSubscriptionSection(string audienceType)
    {
        bool emailDigestIncludeSubscriptionSection = false;
        //string settingKey = "email.digest.include.subscription.section";

        //audienceType = audienceType == null ? string.Empty : audienceType;
        //string cacheKey = CacheManager.CreateCacheKey(new string[] { settingKey, audienceType });

        //if (CacheManager.GetCache(cacheKey) == null)
        //{
        //    DAL oDAL = new DAL();
        //    string val;

        //    val = oDAL.GetAppSetting(settingKey + Constants.APPLICATION_SETTING_KEY_SEPARATOR + audienceType);
        //    if (val.Trim().Length.Equals(0)) val = oDAL.GetAppSetting(settingKey);

        //    oDAL.Dispose();

        //    if (val.Trim().ToLower().Equals(Constants.TRUE.ToLower())) emailDigestIncludeSubscriptionSection = true;
        //    else emailDigestIncludeSubscriptionSection = false;

        //    // Add to cache
        //    CacheManager.InsertCache(cacheKey, Constants.EMAIL_DIGEST_DEPENDENCE_CACHE_KEY, emailDigestIncludeSubscriptionSection, true);
        //}
        //else emailDigestIncludeSubscriptionSection = (bool)CacheManager.GetCache(cacheKey);

        return emailDigestIncludeSubscriptionSection;
    }

    public static bool EmailDigestIncludeFomoSection(string audienceType)
    {
        bool emailDigestIncludeFomoSection = false;
        //string settingKey = "email.digest.include.fomo.section";

        //audienceType = audienceType == null ? string.Empty : audienceType;
        //string cacheKey = CacheManager.CreateCacheKey(new string[] { settingKey, audienceType });

        //if (CacheManager.GetCache(cacheKey) == null)
        //{
        //    DAL oDAL = new DAL();
        //    string val;

        //    val = oDAL.GetAppSetting(settingKey + Constants.APPLICATION_SETTING_KEY_SEPARATOR + audienceType);
        //    if (val.Trim().Length.Equals(0)) val = oDAL.GetAppSetting(settingKey);

        //    oDAL.Dispose();

        //    if (val.Trim().ToLower().Equals(Constants.TRUE.ToLower())) emailDigestIncludeFomoSection = true;
        //    else emailDigestIncludeFomoSection = false;

        //    // Add to cache
        //    CacheManager.InsertCache(cacheKey, Constants.EMAIL_DIGEST_DEPENDENCE_CACHE_KEY, emailDigestIncludeFomoSection, true);
        //}
        //else emailDigestIncludeFomoSection = (bool)CacheManager.GetCache(cacheKey);

        return emailDigestIncludeFomoSection;
    }

    public static int EmailDigestRetry()
    {
        int emailDigestRetry = 0;
        //string settingKey = "email.digest.retry";
        //string cacheKey = CacheManager.CreateCacheKey(new string[] { settingKey });

        //if (CacheManager.GetCache(cacheKey) == null)
        //{
        //    DAL oDAL = new DAL();

        //    try
        //    {
        //        emailDigestRetry = int.Parse(oDAL.GetAppSetting(settingKey));
        //    }
        //    catch { }

        //    oDAL.Dispose();

        //    // Add to cache
        //    CacheManager.InsertCache(cacheKey, Constants.EMAIL_DIGEST_DEPENDENCE_CACHE_KEY, emailDigestRetry, true);
        //}
        //else emailDigestRetry = (int)CacheManager.GetCache(cacheKey);

        return emailDigestRetry;
    }

    public static int EmailDigestRetrySleep()
    {
        int emailDigestRetrySleep = 0;
        //string settingKey = "email.digest.retry.sleep.second";
        //string cacheKey = CacheManager.CreateCacheKey(new string[] { settingKey });

        //if (CacheManager.GetCache(cacheKey) == null)
        //{
        //    DAL oDAL = new DAL();

        //    try
        //    {
        //        emailDigestRetrySleep = int.Parse(oDAL.GetAppSetting(settingKey));
        //    }
        //    catch { }

        //    oDAL.Dispose();

        //    // Add to cache
        //    CacheManager.InsertCache(cacheKey, Constants.EMAIL_DIGEST_DEPENDENCE_CACHE_KEY, emailDigestRetrySleep, true);
        //}
        //else emailDigestRetrySleep = (int)CacheManager.GetCache(cacheKey);

        return emailDigestRetrySleep;
    }

    public static string EmailDigestSelect(string emailDigestFrequency, string audienceType)
    {
        string emailDigestSelect = string.Empty;
        string settingKey = "email.digest.select";

        //audienceType = audienceType == null ? string.Empty : audienceType;
        //if (emailDigestFrequency.Trim().Length > 0) settingKey += Constants.APPLICATION_SETTING_KEY_SEPARATOR + emailDigestFrequency;
        //string cacheKey = CacheManager.CreateCacheKey(new string[] { settingKey, audienceType });

        //if (CacheManager.GetCache(cacheKey) == null)
        //{
        //    DAL oDAL = new DAL();

        //    emailDigestSelect = oDAL.GetAppSetting(settingKey + Constants.APPLICATION_SETTING_KEY_SEPARATOR + audienceType);
        //    if (emailDigestSelect.Trim().Length.Equals(0)) emailDigestSelect = oDAL.GetAppSetting(settingKey);

        //    oDAL.Dispose();

        //    // Add to cache
        //    CacheManager.InsertCache(cacheKey, Constants.EMAIL_DIGEST_DEPENDENCE_CACHE_KEY, emailDigestSelect, true);
        //}
        //else emailDigestSelect = (string)CacheManager.GetCache(cacheKey);

        return emailDigestSelect;
    }

    public static string EmailDigestSort(string emailDigestFrequency, string audienceType)
    {
        string emailDigestSort = string.Empty;
        string settingKey = "email.digest.sort";

        //audienceType = audienceType == null ? string.Empty : audienceType;
        //if (emailDigestFrequency.Trim().Length > 0) settingKey += Constants.APPLICATION_SETTING_KEY_SEPARATOR + emailDigestFrequency;
        //string cacheKey = CacheManager.CreateCacheKey(new string[] { settingKey, audienceType });

        //if (CacheManager.GetCache(cacheKey) == null)
        //{
        //    DAL oDAL = new DAL();

        //    emailDigestSort = oDAL.GetAppSetting(settingKey + Constants.APPLICATION_SETTING_KEY_SEPARATOR + audienceType);
        //    if (emailDigestSort.Trim().Length.Equals(0)) emailDigestSort = oDAL.GetAppSetting(settingKey);

        //    oDAL.Dispose();

        //    // Add to cache
        //    CacheManager.InsertCache(cacheKey, Constants.EMAIL_DIGEST_DEPENDENCE_CACHE_KEY, emailDigestSort, true);
        //}
        //else emailDigestSort = (string)CacheManager.GetCache(cacheKey);

        return emailDigestSort;
    }

    public static string EmailErrorNotification(string moduleName)
    {
        string val = "";
        string moduleKey = moduleName.Trim();
        if (moduleKey.Length > 0) moduleKey = "." + moduleKey;
        string settingKey = "email.error.notification";
        //string cacheKey = CacheManager.CreateCacheKey(new string[] { settingKey + moduleKey });

        //if (CacheManager.GetCache(cacheKey) == null)
        //{
        //    DAL oDAL = new DAL();

        //    val = oDAL.GetAppSetting(settingKey + moduleKey);
        //    if (val.Trim().Equals(string.Empty) && moduleKey.Length > 0) val = oDAL.GetAppSetting(settingKey);

        //    oDAL.Dispose();

        //    // Add to cache
        //    CacheManager.InsertCache(cacheKey, Constants.APPLICATION_DEPENDENCE_CACHE_KEY, val, true);
        //}
        //else val = (string)CacheManager.GetCache(cacheKey);

        return val;
    }

    public static string EmailFrom()
    {
        string val = "";
        string settingKey = "email.from";
        //string cacheKey = CacheManager.CreateCacheKey(new string[] { settingKey });

        //if (CacheManager.GetCache(cacheKey) == null)
        //{
        //    DAL oDAL = new DAL();

        //    val = oDAL.GetAppSetting(settingKey);

        //    oDAL.Dispose();

        //    // Add to cache
        //    CacheManager.InsertCache(cacheKey, Constants.APPLICATION_DEPENDENCE_CACHE_KEY, val, true);
        //}
        //else val = (string)CacheManager.GetCache(cacheKey);

        return val;
    }

    public static string EmailFromName()
    {
        string val = "";
        string settingKey = "email.from.name";
        //string cacheKey = CacheManager.CreateCacheKey(new string[] { settingKey });

        //if (CacheManager.GetCache(cacheKey) == null)
        //{
        //    DAL oDAL = new DAL();

        //    val = oDAL.GetAppSetting(settingKey);

        //    oDAL.Dispose();

        //    // Add to cache
        //    CacheManager.InsertCache(cacheKey, Constants.APPLICATION_DEPENDENCE_CACHE_KEY, val, true);
        //}
        //else val = (string)CacheManager.GetCache(cacheKey);

        return val;
    }

    public static string EmailPublicFolder(string audienceType)
    {
        //DAL oDAL = new DAL();
        string val = "";

        //audienceType = audienceType == null ? string.Empty : audienceType;
        //val = oDAL.GetAppSetting("email.public.folder" + Constants.APPLICATION_SETTING_KEY_SEPARATOR + audienceType);
        //if (val.Trim().Length.Equals(0)) val = oDAL.GetAppSetting("email.public.folder");

        //oDAL.Dispose();

        return val;
    }

    //public static string EmailToOverride()
    //{
    //    string val;
    //    string settingKey = "email.override";
    //    string cacheKey = CacheManager.CreateCacheKey(new string[] { settingKey });

    //    if (CacheManager.GetCache(cacheKey) == null)
    //    {
    //        DAL oDAL = new DAL();

    //        val = oDAL.GetAppSetting(settingKey);

    //        oDAL.Dispose();

    //        // Add to cache
    //        CacheManager.InsertCache(cacheKey, Constants.APPLICATION_DEPENDENCE_CACHE_KEY, val, true);
    //    }
    //    else val = (string)CacheManager.GetCache(cacheKey);

    //    return val;
    //}

    //public static string EmailBCC()
    //{
    //    string val;
    //    string settingKey = "email.bcc";
    //    string cacheKey = CacheManager.CreateCacheKey(new string[] { settingKey });

    //    if (CacheManager.GetCache(cacheKey) == null)
    //    {
    //        DAL oDAL = new DAL();

    //        val = oDAL.GetAppSetting(settingKey);

    //        oDAL.Dispose();

    //        // Add to cache
    //        CacheManager.InsertCache(cacheKey, Constants.APPLICATION_DEPENDENCE_CACHE_KEY, val, true);
    //    }
    //    else val = (string)CacheManager.GetCache(cacheKey);

    //    return val;
    //}

    public static string EncryptionKey()
    {
        string val = "";
        string settingKey = "encryption.key";
        //string cacheKey = CacheManager.CreateCacheKey(new string[] { settingKey });

        //if (CacheManager.GetCache(cacheKey) == null)
        //{
        //    DAL oDAL = new DAL();

        //    val = oDAL.GetAppSetting(settingKey);

        //    oDAL.Dispose();

        //    // Add to cache
        //    CacheManager.InsertCache(cacheKey, Constants.APPLICATION_DEPENDENCE_CACHE_KEY, val, true);
        //}
        //else val = (string)CacheManager.GetCache(cacheKey);

        return val;
    }

    public static int EnrolledSectionsRetry()
    {
        int enrolledsectionsRetry = 0;
        string settingKey = "enrolled.sections.retry";
        //string cacheKey = CacheManager.CreateCacheKey(new string[] { settingKey });

        //if (CacheManager.GetCache(cacheKey) == null)
        //{
        //    DAL oDAL = new DAL();

        //    try
        //    {
        //        enrolledsectionsRetry = int.Parse(oDAL.GetAppSetting(settingKey));
        //    }
        //    catch { }

        //    oDAL.Dispose();

        //    // Add to cache
        //    CacheManager.InsertCache(cacheKey, Constants.EMAIL_DIGEST_DEPENDENCE_CACHE_KEY, enrolledsectionsRetry, true);
        //}
        //else enrolledsectionsRetry = (int)CacheManager.GetCache(cacheKey);

        return enrolledsectionsRetry;
    }

    //public static int EnrolledSectionsRetrySleep()
    //{
    //    int enrolledsectionsRetrySleep = 0;
    //    string settingKey = "enrolled.sections.retry.sleep.seconds";
    //    string cacheKey = CacheManager.CreateCacheKey(new string[] { settingKey });

    //    if (CacheManager.GetCache(cacheKey) == null)
    //    {
    //        DAL oDAL = new DAL();

    //        try
    //        {
    //            enrolledsectionsRetrySleep = int.Parse(oDAL.GetAppSetting(settingKey));
    //        }
    //        catch { }

    //        oDAL.Dispose();

    //        // Add to cache
    //        CacheManager.InsertCache(cacheKey, Constants.EMAIL_DIGEST_DEPENDENCE_CACHE_KEY, enrolledsectionsRetrySleep, true);
    //    }
    //    else enrolledsectionsRetrySleep = (int)CacheManager.GetCache(cacheKey);

    //    return enrolledsectionsRetrySleep;
    //}

    //public static string[] EnrolledSectionsSelectedAudiences(bool useCache)
    //{
    //    string[] enrolledSectionsSelectedAudiences = null;
    //    string settingKey = "enrolled.sections.selected.audiences";
    //    string cacheKey = CacheManager.CreateCacheKey(new string[] { settingKey });

    //    // Get value from cache
    //    if (useCache && CacheManager.GetCache(cacheKey) != null)
    //    {
    //        enrolledSectionsSelectedAudiences = ((string)CacheManager.GetCache(cacheKey)).Split(new string[] { Constants.SEPARATOR_COMMA }, 
    //            StringSplitOptions.RemoveEmptyEntries);
    //    }

    //    // Get value from DB
    //    if (enrolledSectionsSelectedAudiences == null || enrolledSectionsSelectedAudiences.Length == 0)
    //    {
    //        string val;
    //        DAL oDAL = new DAL();
    //        val = oDAL.GetAppSetting(settingKey);
    //        oDAL.Dispose();

    //        if (val.Trim().Length > 0)
    //        {
    //            enrolledSectionsSelectedAudiences = val.Split(new string[] { Constants.SEPARATOR_COMMA }, StringSplitOptions.RemoveEmptyEntries);

    //            // save to cache
    //            if (useCache) CacheManager.InsertCache(cacheKey, Constants.APPLICATION_DEPENDENCE_CACHE_KEY, val, true);
    //        }
    //    }

    //    return enrolledSectionsSelectedAudiences;
    //}

    //public static string ExchangeCallbackKey(string moduleName)
    //{
    //    string val;
    //    string moduleKey = moduleName.Trim();
    //    if (moduleKey.Length > 0) moduleKey = "." + moduleKey;
    //    string settingKey = "exchange.callback.key";
    //    string cacheKey = CacheManager.CreateCacheKey(new string[] { settingKey + moduleKey });

    //    if (CacheManager.GetCache(cacheKey) == null)
    //    {
    //        DAL oDAL = new DAL();

    //        val = oDAL.GetAppSetting(settingKey + moduleKey);
    //        if (val.Trim().Equals(string.Empty) && moduleKey.Length > 0) val = oDAL.GetAppSetting(settingKey);

    //        oDAL.Dispose();

    //        // Add to cache
    //        CacheManager.InsertCache(cacheKey, Constants.APPLICATION_DEPENDENCE_CACHE_KEY, val, true);
    //    }
    //    else val = (string)CacheManager.GetCache(cacheKey);

    //    return val;
    //}

    //public static string ExchangeGSBEmailDomain()
    //{
    //    string emailGSBDomain = string.Empty;
    //    string settingKey = "exchange.gsb.email.domain";
    //    string cacheKey = CacheManager.CreateCacheKey(new string[] { settingKey });

    //    if (CacheManager.GetCache(cacheKey) == null)
    //    {
    //        DAL oDAL = new DAL();

    //        emailGSBDomain = oDAL.GetAppSetting(settingKey);

    //        oDAL.Dispose();

    //        // Add to cache
    //        CacheManager.InsertCache(cacheKey, Constants.EMAIL_DIGEST_DEPENDENCE_CACHE_KEY, emailGSBDomain, true);
    //    }
    //    else emailGSBDomain = (string)CacheManager.GetCache(cacheKey);

    //    return emailGSBDomain;
    //}

    //public static string ExchangePasscode(string moduleName)
    //{
    //    string val;
    //    string moduleKey = moduleName.Trim();
    //    if (moduleKey.Length > 0) moduleKey = "." + moduleKey;
    //    string settingKey = "exchange.passcode";
    //    string cacheKey = CacheManager.CreateCacheKey(new string[] { settingKey + moduleKey });

    //    if (CacheManager.GetCache(cacheKey) == null)
    //    {
    //        DAL oDAL = new DAL();

    //        val = oDAL.GetAppSetting(settingKey + moduleKey);
    //        if (val.Trim().Equals(string.Empty) && moduleKey.Length > 0) val = oDAL.GetAppSetting(settingKey);

    //        oDAL.Dispose();

    //        // Add to cache
    //        CacheManager.InsertCache(cacheKey, Constants.APPLICATION_DEPENDENCE_CACHE_KEY, val, true);
    //    }
    //    else val = (string)CacheManager.GetCache(cacheKey);

    //    return val;
    //}

    //public static int ExchangePrimarySmtpAddressCacheHours()
    //{
    //    int val = 0;
    //    string settingKey = "exchange.primary.smtp.address.cache.hours";
    //    string cacheKey = CacheManager.CreateCacheKey(new string[] { settingKey });

    //    if (CacheManager.GetCache(cacheKey) == null)
    //    {
    //        DAL oDAL = new DAL();

    //        try
    //        {
    //            val = Convert.ToInt32(oDAL.GetAppSetting(settingKey));
    //        }
    //        catch { }

    //        oDAL.Dispose();

    //        // Add to cache
    //        CacheManager.InsertCache(cacheKey, Constants.APPLICATION_DEPENDENCE_CACHE_KEY, val, true);
    //    }
    //    else val = (int)CacheManager.GetCache(cacheKey);

    //    return val;
    //}

    //public static string ExchangeSource(string moduleName)
    //{
    //    string val;
    //    string moduleKey = moduleName.Trim();
    //    if (moduleKey.Length > 0) moduleKey = "." + moduleKey;
    //    string settingKey = "exchange.source";
    //    string cacheKey = CacheManager.CreateCacheKey(new string[] { settingKey + moduleKey });

    //    if (CacheManager.GetCache(cacheKey) == null)
    //    {
    //        DAL oDAL = new DAL();

    //        val = oDAL.GetAppSetting(settingKey + moduleKey);
    //        if (val.Trim().Equals(string.Empty) && moduleKey.Length > 0) val = oDAL.GetAppSetting(settingKey);

    //        oDAL.Dispose();

    //        // Add to cache
    //        CacheManager.InsertCache(cacheKey, Constants.APPLICATION_DEPENDENCE_CACHE_KEY, val, true);
    //    }
    //    else val = (string)CacheManager.GetCache(cacheKey);

    //    return val;
    //}

    //public static string ExchangeWebServiceURL()
    //{
    //    string val;
    //    string settingKey = "exchange.web.service.url";
    //    string cacheKey = CacheManager.CreateCacheKey(new string[] { settingKey });

    //    if (CacheManager.GetCache(cacheKey) == null)
    //    {
    //        DAL oDAL = new DAL();

    //        val = oDAL.GetAppSetting(settingKey);

    //        oDAL.Dispose();

    //        // Add to cache
    //        CacheManager.InsertCache(cacheKey, Constants.APPLICATION_DEPENDENCE_CACHE_KEY, val, true);
    //    }
    //    else val = (string)CacheManager.GetCache(cacheKey);

    //    return val;
    //}

    

    
    //public static string CategoryPrefix()
    //{
    //    string val;
    //    string settingKey = "commcenter.prefix";
    //    string cacheKey = CacheManager.CreateCacheKey(new string[] { settingKey });

    //    if (CacheManager.GetCache(cacheKey) == null)
    //    {
    //        DAL oDAL = new DAL();

    //        val = oDAL.GetAppSetting(settingKey);

    //        oDAL.Dispose();

    //        // Add to cache
    //        CacheManager.InsertCache(cacheKey, Constants.APPLICATION_DEPENDENCE_CACHE_KEY, val, true);
    //    }
    //    else val = (string)CacheManager.GetCache(cacheKey);

    //    return val;

    //}

    //public static string AudienceAdmitRound()
    //{
    //    string val;
    //    string settingKey = "admit.round";
    //    string cacheKey = CacheManager.CreateCacheKey(new string[] { settingKey });

    //    if (CacheManager.GetCache(cacheKey) == null)
    //    {
    //        DAL oDAL = new DAL();

    //        val = oDAL.GetAppSetting(settingKey);

    //        oDAL.Dispose();

    //        // Add to cache
    //        CacheManager.InsertCache(cacheKey, Constants.APPLICATION_DEPENDENCE_CACHE_KEY, val, true);
    //    }
    //    else val = (string)CacheManager.GetCache(cacheKey);

    //    return val;

    //}

    //public static int AudienceYear()
    //{
    //    int val;
    //    string settingKey = "audience.year";
    //    string cacheKey = CacheManager.CreateCacheKey(new string[] { settingKey });

    //    if (CacheManager.GetCache(cacheKey) == null)
    //    {
    //        DAL oDAL = new DAL();

    //        val = int.Parse(oDAL.GetAppSetting(settingKey));

    //        oDAL.Dispose();

    //        // Add to cache
    //        CacheManager.InsertCache(cacheKey, Constants.APPLICATION_DEPENDENCE_CACHE_KEY, val, true);
    //    }
    //    else val = (int)CacheManager.GetCache(cacheKey);

    //    return val;
    //}

    //public static string AudienceType()
    //{
    //    string val;
    //    string settingKey = "audience.type";
    //    string cacheKey = CacheManager.CreateCacheKey(new string[] { settingKey });

    //    if (CacheManager.GetCache(cacheKey) == null)
    //    {
    //        DAL oDAL = new DAL();

    //        val = (oDAL.GetAppSetting(settingKey));

    //        oDAL.Dispose();

    //        // Add to cache
    //        CacheManager.InsertCache(cacheKey, Constants.APPLICATION_DEPENDENCE_CACHE_KEY, val, true);
    //    }
    //    else val = (string)CacheManager.GetCache(cacheKey);

    //    return val;
    //}

    //public static string AdmitErrorMessage()
    //{
    //    string val;
    //    string settingKey = "admit.audience.note";
    //    string cacheKey = CacheManager.CreateCacheKey(new string[] { settingKey });

    //    if (CacheManager.GetCache(cacheKey) == null)
    //    {
    //        DAL oDAL = new DAL();

    //        val = (oDAL.GetAppSetting(settingKey));

    //        oDAL.Dispose();

    //        // Add to cache
    //        CacheManager.InsertCache(cacheKey, Constants.APPLICATION_DEPENDENCE_CACHE_KEY, val, true);
    //    }
    //    else val = (string)CacheManager.GetCache(cacheKey);

    //    return val;
    //}

    //public static bool StaffCanAddToOutlook()
    //{
    //    bool staffCanAddToOutlook;
    //    string settingKey = "addToOutlookTrigger.staff";
    //    string cacheKey = CacheManager.CreateCacheKey(new string[] { settingKey });

    //    if (CacheManager.GetCache(cacheKey) == null)
    //    {
    //        DAL oDAL = new DAL();
    //        string val;
    //        val = (oDAL.GetAppSetting(settingKey));

    //        oDAL.Dispose();

    //         if (val.Trim().ToLower().Equals(Constants.TRUE.ToLower())) staffCanAddToOutlook = true;
    //            else staffCanAddToOutlook = false;


    //        // Add to cache
    //        CacheManager.InsertCache(cacheKey, Constants.APPLICATION_DEPENDENCE_CACHE_KEY, staffCanAddToOutlook, true);
    //    }
    //    else staffCanAddToOutlook = (bool)CacheManager.GetCache(cacheKey);
    //    return staffCanAddToOutlook;
    //}

    // public static bool FacultyCanAddToOutlook()
    //{
    //    bool facultyCanAddToOutlook;
    //    string settingKey = "addToOutlookTrigger.faculty";
    //    string cacheKey = CacheManager.CreateCacheKey(new string[] { settingKey });

    //    if (CacheManager.GetCache(cacheKey) == null)
    //    {
    //        DAL oDAL = new DAL();
    //        string val;
    //        val = (oDAL.GetAppSetting(settingKey));

    //        oDAL.Dispose();

    //         if (val.Trim().ToLower().Equals(Constants.TRUE.ToLower())) facultyCanAddToOutlook = true;
    //            else facultyCanAddToOutlook = false;


    //        // Add to cache
    //        CacheManager.InsertCache(cacheKey, Constants.APPLICATION_DEPENDENCE_CACHE_KEY, facultyCanAddToOutlook, true);
    //    }
    //    else facultyCanAddToOutlook = (bool)CacheManager.GetCache(cacheKey);
    //    return facultyCanAddToOutlook;
    //}

     //public static bool AdminCanAddToOutlook()
     //{
     //    bool adminCanAddToOutlook;
     //    string settingKey = "addToOutlookTrigger.admin";
     //    string cacheKey = CacheManager.CreateCacheKey(new string[] { settingKey });

     //    if (CacheManager.GetCache(cacheKey) == null)
     //    {
     //        DAL oDAL = new DAL();
     //        string val;
     //        val = (oDAL.GetAppSetting(settingKey));

     //        oDAL.Dispose();

     //        if (val.Trim().ToLower().Equals(Constants.TRUE.ToLower())) adminCanAddToOutlook = true;
     //        else adminCanAddToOutlook = false;


     //        // Add to cache
     //        CacheManager.InsertCache(cacheKey, Constants.APPLICATION_DEPENDENCE_CACHE_KEY, adminCanAddToOutlook, true);
     //    }
     //    else adminCanAddToOutlook = (bool)CacheManager.GetCache(cacheKey);
     //    return adminCanAddToOutlook;
     //}

     //public static bool NonAffiiliatesCanAddToOutlook()
     //{
     //    bool nonAffiiliatesCanAddToOutlook;
     //    string settingKey = "addToOutlookTrigger.nonAffiiliates";
     //    string cacheKey = CacheManager.CreateCacheKey(new string[] { settingKey });

     //    if (CacheManager.GetCache(cacheKey) == null)
     //    {
     //        DAL oDAL = new DAL();
     //        string val;
     //        val = (oDAL.GetAppSetting(settingKey));

     //        oDAL.Dispose();

     //        if (val.Trim().ToLower().Equals(Constants.TRUE.ToLower())) nonAffiiliatesCanAddToOutlook = true;
     //        else nonAffiiliatesCanAddToOutlook = false;


     //        // Add to cache
     //        CacheManager.InsertCache(cacheKey, Constants.APPLICATION_DEPENDENCE_CACHE_KEY, nonAffiiliatesCanAddToOutlook, true);
     //    }
     //    else nonAffiiliatesCanAddToOutlook = (bool)CacheManager.GetCache(cacheKey);
     //    return nonAffiiliatesCanAddToOutlook;
     //}  

    #endregion
}
 