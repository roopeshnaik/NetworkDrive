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
using System.ComponentModel;

/// <summary>
/// Summary description for Constant Class
/// </summary>
public class Constants
{

    //delete utility for courses
    public const string Config_ApplicationUID = "ApplicationUID";
    public const string Config_ConnectionString = "ConnectionString";


    public const string APPLICATION_SETTING_KEY_SEPARATOR = ".";
    public const string CACHE_KEY_SEPARATOR = "_";
    public const string CONTROL_ID_SEPARATOR = "_";
    public const string EMAIL_ADDRESS_SEPARATOR = ",";
    public const string LINE_BREAK = "<br/>";
    public const string SEPARATOR_COMMA = ",";
    public const string SEPARATOR_PIPE = "|";

    public const string TEXT_MANAGER_CONTENT_BLANK = "{BLANK}";
    public const string TEXT_MANAGER_CONTENT_SPACE = "{SPACE}";
    public const string TEXT_MANAGER_ID_SEPARATOR = "_";
    public const string Const_CommaSeparator = ",";

    public const string NEVER_LOGIN = "Never Login";
    public const string NEVER_SENT = "Never Sent";
    public const string NOT_APPLICABLE = "NA";
    public const string TBD = "TBD";

    public const string FALSE = "false";
    public const string TRUE = "true";

    public const string EMPTY_MESSAGE = "Empty message";
    public const string NO_AUDIENCE_VALUE = "no_audience";

    // Cache dependence keys
    public const string APPLICATION_DEPENDENCE_CACHE_KEY = "comm_center";
    public const string EMAIL_DIGEST_DEPENDENCE_CACHE_KEY = "comm_center_email_digest";
    public const string GET_MESSAGE_DEPENDENCE_CACHE_KEY = "comm_center_get_message";
    public const string AUDIENCE_TYPE = "audience_type";
    public const string SUMMARY_BY_AUDIENCE_REPORT = "SUMMARY BY AUDIENCE";
    public const string SUMMARY_BY_CATEGORY_REPORT = "SUMMARY BY CATEGORY";
    public const string SUBSCRITIONS_BY_STUDENT_REPORT = "SUBSCRITIONS BY STUDENT";
    public const string CATEGORY_NAME = "category_name";
    public const string CATEGORY_ID = "category_id";
    public const string AUDIENCE_NAME = "audience_name";
    public const string STUDENT_NAME = "StudentName";
	public const string REPORT_DATE = "ReportDate";
    public const string CATEGORY_CODE = "category_code";
    public const string EXCEL_RESPONSE_TYPE = @"application/vnd.ms-excel";
    public const string AUDIENCE_SUBSCRIPTION_REPORT_NAME = "AudienceSubscriptionReport";
    public const string CATEGORY_SUBSCRIPTION_REPORT_NAME = "GroupSubsriptionReport";
    public const string ALL_CATEGORY_SUBSCRIPTION_REPORT = "SubscriptionReport";
    public const string OFFICEXML_EXCEL_RESPONSE_TYPE = "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet";
    public const string EMAIL_DIGEST_DATE = "EmailDigestDate";
    public const string EMAIL_DIGEST_FREQUENCY = "Frequency";
    public const string EMAIL_DIGEST_TOTAL_COUNT = "TotalCount";
    public const string EMAIL_DIGEST_AUDIENCE_GROUP = "AudienceGroup";
    public const string EMAIL_DIGEST_REPORT = "EmailDigestReport";
    public const string ADD_TO_CALENDAR_REPORT = "AddToCalendarReport";
    public const string ADD_TO_CALENDAR_REPORTSTARTDATE = "ReportStartDate";
    public const string ADD_TO_CALENDAR_REPORTENDDATE = "ReportEndDate";
    public const string ADD_TO_CALENDAR_MESSAGETYPE = "MessageType";
    public const string ADD_TO_CALENDAR_MESSAGEDATE = "MessageDate";
    public const string ADD_TO_CALENDAR_MESSAGENAME = "MessageName";
    public const string ADD_TO_CALENDAR_CATEGORY = "Category";
    public const string ADD_TO_CALENDAR_TOTALCOUNT = "Totalcount";
    public const string ADD_TO_CALENDAR_AUDIENCEGROUPOFUSER = "AudienceGroupofuser";
    public const string ADD_TO_CALENDAR_MESSAGESTATUS = "MessageStatus";
    public const string ADD_TO_CALENDAR_STARTDATETIME = "StartDateTime";
    public const string ADD_TO_CALENDAR_ENDDATETIME = "EndDateTime";
    public const string ADD_TO_CALENDAR_TOTALADDED = "TotalAdded";
    public const string ADD_TO_CALENDAR_REPORT_LABEL = "Summary Statistics for Add to calendar for :";
    public const string ADD_TO_CALENDAR_EVENTS = "Events";
    public const string ADD_TO_CALENDAR_DEADLINES = "Deadlines";
    public const string ADD_TO_CALENDAR_ACAD_CALENDAR = "Academic Calendars";
    public const string ADD_TO_CALENDAR_ENROL_SECTION = "Enrolled Sections";
    public const string ADD_TO_CALENDAR_TOTAL_ADDED = "Total Added";
    


    //Messages
    public const string SL_EDIT_STAFF_ADMIN_MESSAGE = @"Student leaders may only edit student-leaders/students created messages.";
    public const string SL_EDIT_REQ_PUBLISHED_MESSAGE = "This required message has been published. It may only be edited by administrators.";
    public const string SL_EDIT_REQ_MESSAGE = @"This is a required message. It may only be edited by staffs/administrators.";
    public const string STD_EDIT_STAFF_ADMIN_MESSAGE = "Students may only edit student/student leader created events.";

    //Student reports
    public const string SL_RPT_SUBSCRIPTION_DESCRIPTION = @"View the total number of subscribers or names of individual subscribers for a student organization at a particular point in time by selecting Report Date below.  GSBPride student leaders may send an {0} for GSBPride subscriber data.";
    public const string SL_RPT_ADD_TO_OUTLOOK_DESCRIPTION = @"View the number of times a deadline or event has been added to students’ personal calendar by selecting a date range below.";
    public const string RPT_FF_BROWSER_NOTE = @"Note: Mozilla Firefox users should save the file first and then open using excel. Excel 2007 or greater is required for reports exceeding 65K rows.";
    public const string RPT_UNAUTH_ORG_KEY = @"reports.groups.exclude";
    public const string EMAIL_ID = @"MyGSBDigest@gsb.stanford.edu";    

    public const int MAX_SPONSOR_COUNT = 5;
    public const string AUDIENCE_ID = "audience_id";
    public const string AUDIENCE_ISACTIVE = "is_active";
//Category Management
    public const string Cat_ISACTIVE = "is_active";
    public const string Cat_ISSTDCAT = "is_stdcat";

    //Audience management
    public const string ISMBA = "MBA";
    public const string ISSLOAN = "MSx";
    public const string ISPHD = "PhD";
    public const string PERSON_ACTIVE = "ACTIVE";

}

public static class AuthorAffiliation
{
    public static readonly string Admin = "admin";
    public static readonly string Staff = "staff";
    public static readonly string Student = "student";
    public static readonly string StudentLeader = "student_leader";
}


public static class EnumExtensionMethods
{
    public static string GetEnumDescription(this Enum enumValue)
    {
        var fieldInfo = enumValue.GetType().GetField(enumValue.ToString());
        var descriptionAttributes = (DescriptionAttribute[])fieldInfo.GetCustomAttributes(typeof(DescriptionAttribute), false);
        return descriptionAttributes.Length > 0 ? descriptionAttributes[0].Description : enumValue.ToString();
    }
}

#region Emunerations
/// <summary>
/// Month
/// </summary>
public enum Month
{
    Jan = 1,
    Feb = 2,
    Mar = 3,
    Apr = 4,
    May = 5,
    Jun = 6,
    Jul = 7,
    Aug = 8,
    Sep = 9,
    Oct = 10,
    Nov = 11,
    Dec = 12
}

/// <summary>
/// Actions for managing event
/// </summary>
public enum GSBAction
{
    Delete,
    Insert,
    Other,
    GetCalendaritem,
    Update
}

public enum MessageStatus
{
    InProgress = 1,
    AwaitingApproval = 2,
    Published = 3,
    Canceled = 4,
    Deleted = 5
}

public enum UberCategoryEnum
{
    Academic = 1,
    Career = 2,
    StudentOrg = 3,
    AllElse = 4
}

#endregion

#region Data Structures
/// <summary>
/// Email Digest Frequency
/// </summary>
public struct EmailDigestFrequency
{
    public const string Daily = "daily";
    public const string Weekly = "weekly";
}

/// <summary>
/// Exchange Module Names
/// </summary>
public struct ExchangeModule
{
    public const string AcademicCalendar = "academic.calendar";
    public const string Default = "";
    public const string EnrolledSection = "enrolled.section";
    public const string messagePush = "published.messages.push";
    public const string messageDeleted = "message.deleted";
}

/// <summary>
/// Outlook Importance attribute
/// </summary>
public struct Importance
{
    public const string High = "high";
    public const string Low = "low";
    public const string Normal = "normal";
}

/// <summary>
/// Outlook Is Private attribute
/// </summary>
public struct IsPrivate
{
    public const string False = "false";
    public const string True = "true";
}

/// <summary>
/// Message status
/// </summary>
public struct MessageStatusValues
{
    public const string InProgress = "in progress";
    public const string AwaitingApproval = "awaiting approval";
    public const string Published = "published";
    public const string Canceled = "canceled";
    public const string Deleted = "deleted";
}

/// <summary>
/// Message type
/// </summary>
public struct MessageType
{
    public const string Announcement = "announcement";
    public const string Event = "event";
    public const string Deadline = "deadline";
    public const string EnrolledSection = "enrolled section";
    public const string AcademicCalendar = "academic calendar";
}


/// <summary>
/// Navigation View
/// </summary>
public struct NavigationView
{
    public const string Author = "author";
    public const string MyGSB = "mygsb";
}

/// <summary>
/// Outlook Shown As attribute
/// </summary>
public struct ShownAs
{
    public const string Busy = "busy";
    public const string Free = "free";
    public const string OutOfOffice = "out of office";
    public const string Tentative = "tentative";
}
#endregion
