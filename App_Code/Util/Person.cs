#region Namespaces
using System;
using System.Data;
using System.Collections;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Xml;
using System.Linq;
#endregion

/// <summary>
/// Summary description for Person
/// </summary>
public class Person
{

    //name
    //gsb email
    //preferred email
    //last digest
    //digest frequecy
    //subscribed groups
    //affiliation
    //group

    #region Members
    private string _univid;
    private string _uid;
    private string _publicid;
    private string _nameFirst;
    private string _nameLast;
    private string _emailPrimary;
    private string _emailSecondary;
    private string _digestFrequency;
    private Boolean _disableReminders;
    private string _subscribedGroups;
    private DateTime? _lastLogin;
    private DateTime? _lastDigestSent;
    //private string[] _affiliation;
    private string _role;
    private IList _audiences;
    private string _adminRole;
    private bool _isMatriculated;
    private bool _doesPersonBelongToAdmitGroupOnly;
    private bool _doesPersonBelongToNonAdmitGroupOnly;
    private bool _doesPersonBelongToAnyAdmitGroup;
    private bool _doesPersonBelongToAnyNonAdmitGroup;
    private bool _exhibitAdmitBehaviourForOutlook;
    public bool _doesPersonBelongToDissimilarAdmitAndNonAdmitAudience;
    public bool _doesPersonBelongToSameAdmitAndNonAdmitAudience;
    public bool _doesPersonBelongToALesserNonAdmitAudience;
    public bool _doesPersonBelongToAGreaterNonAdmitAudience;
    private bool _canPersonAddMessageToOutlook;
    private bool _doesPersonBelongToStaff;
    private bool _doesPersonBelongToFaculty;
    private bool _doesPersonBelongToAdmin;
    private bool _doesPersonBelongToNonAffiliates;

    private List<int> _admitYears = new List<int>();
    private List<int> _nonAdmitYears = new List<int>();
    public bool _admitOutlookTriggerOn;
    public bool _nonAdmitOutlookTriggerOn;

    #endregion

    #region Constructors
    /// <summary>
    /// Person object
    /// </summary>
    public Person()
    {
    }

    /// <summary> 
    /// Create a new object populated using DB data 
    /// </summary> 
    /// <param name="id">Univ Id or Public Id</param>
    public Person(string id)
    {
    }


    /// <summary> 
    /// Create a new object populated using DB data 
    /// </summary> 
    /// this method id only for utility to delete duplicate courses
    /// <param name="id">Univ Id or Public Id</param>
    public Person(bool isUID, string id)
    {
    }

    #endregion

    #region Public Properties
    /// <summary>
    /// Property relating to database column univid
    /// </summary>
    public string Univid
    {
        get { return _univid; }
        set { _univid = value; }
    }


    public string Uid
    {
        get { return _uid; }
        set { _uid = value; }
    }

    public string PublicID
    {
        get { return _publicid; }
    }

    public string NameFirst
    {
        get { return _nameFirst; }
        set { _nameFirst = value; }
    }

    public string NameLast
    {
        get { return _nameLast; }
        set { _nameLast = value; }
    }

    public string NameDisplay
    {
        get { return NameFirst + " " + NameLast; }
    }

    public string NameDisplayLF
    {
        get { return NameLast + ", " + NameFirst; }
    }

    public string EmailGSB
    {
        get
        {
                return Uid.ToLower() ;
        }
    }

    public string EmailPrimary
    {
        get { return _emailPrimary; }
    }

    public string EmailSecondary
    {
        get { return _emailSecondary; }
    }

    public DateTime? LastLogin
    {
        get { return _lastLogin; }
    }

    public DateTime? LastDigestSent
    {
        get { return _lastDigestSent; }
    }

    public Boolean DisableReminders
    {
        get { return _disableReminders; }
        set { _disableReminders = value; }
    }

    public string DigestFrequency
    {
        get { return _digestFrequency; }
        set { _digestFrequency = value; }
    }

    public IList Audiences
    {
        get { return _audiences; }
        set { _audiences = value; }
    }

    public Boolean IsAuthor
    {
        get
        {

            if (_adminRole == "author" || _adminRole == "admin" || _adminRole == "author-student" || _adminRole == "author-student-leader" || _adminRole == "author-faculty" || _adminRole == "masteradmin")
            {
                return true;
            }
            else
            {
                return false;
            }
        }
    }


    public Boolean IsAuthorStudent
    {
        get
        {

            if (_adminRole == "author-student")
            {
                return true;
            }
            else
            {
                return false;
            }
        }
    }

    public Boolean IsAuthorStudentLeader
    {
        get
        {

            if (_adminRole == "author-student-leader")
            {
                return true;
            }
            else
            {
                return false;
            }
        }
    }

    public Boolean IsAdmin
    {
        get
        {
            if (_adminRole == "admin" || _adminRole == "masteradmin")
            {
                return true;
            }
            else
            {
                return false; //$
            }
        }
    }

    public Boolean IsMasteradmin
    {
        get
        {
            if (_adminRole == "masteradmin")
            {
                return true;
            }
            else
            {
                return false; //$
            }
        }
    }

    /// <summary>
    /// Check if user is an admit by checking his/she audience record(s)
    /// </summary>
    public bool IsAdmit
    {
        get
        {
            bool isAdmit = false;

            if (_audiences != null && _audiences.Count > 0)
            {
                foreach (Audience audience in _audiences)
                {
                    if (audience.IsAdmit)
                    {
                        isAdmit = true;
                        break;
                    }
                }
            }

            return isAdmit;
        }
    }



    /// <summary>
    /// Check if person belongs to matricluated workgroup
    /// </summary>

    public bool IsMatriculated
    {
        get { return _isMatriculated; }
    }
    /// <summary>
    /// Check if person belongs to Admit workgroup only
    /// </summary>
    public bool DoesPersonBelongToAdmitGroupOnly
    {
        get { return _doesPersonBelongToAdmitGroupOnly; }
    }
    /// <summary>
    /// Check if person belongs to NonAdmit workgroup only
    /// </summary>
    public bool DoesPersonBelongToNonAdmitGroupOnly
    {
        get { return _doesPersonBelongToNonAdmitGroupOnly; }
    }
    /// <summary>
    /// Check if person belongs to any admit workgroup
    /// </summary>
    public bool DoesPersonToAnyAdmitGroup
    {
        get { return _doesPersonBelongToAnyAdmitGroup; }
    }
    /// <summary>
    /// Check if person belongs to any student workgroup
    /// </summary>
    public bool DoesPersonBelongToAnyNonAdmitGroup
    {
        get { return _doesPersonBelongToAnyNonAdmitGroup; }
    }
    /// <summary>
    /// check admit behavior for calendar/archive
    /// </summary>
    public bool ExhibitAdmitBehaviourForOutlook
    {
        get { return _exhibitAdmitBehaviourForOutlook; }
    }
    /// <summary>
    /// Check if person can add message to outlook
    /// </summary>
    public bool CanAddtoOutLook
    {
        get { return _canPersonAddMessageToOutlook; }
    }

    private bool DoesPersonBelongToAdmitGroupOnlyInternal()
    {
        _doesPersonBelongToAdmitGroupOnly = true;
        if (_audiences != null && _audiences.Count > 0)
        {
            foreach (Audience audience in _audiences)
            {
                if (!audience.IsAdmit)
                {
                    _doesPersonBelongToAdmitGroupOnly = false;
                    break;
                }
            }
        }
        else
        {
            _doesPersonBelongToAdmitGroupOnly = false;
        }
        return _doesPersonBelongToAdmitGroupOnly;
    }


    private bool DoesPersonBelongToNonAdmitGroupOnlyInternal()
    {
        _doesPersonBelongToNonAdmitGroupOnly = true;
        if (_audiences != null && _audiences.Count > 0)
        {
            foreach (Audience audience in _audiences)
            {
                if (audience.IsAdmit)
                {
                    _doesPersonBelongToNonAdmitGroupOnly = false;
                    break;
                }
            }
        }
        else
        {
            _doesPersonBelongToNonAdmitGroupOnly = false;
        }
        return _doesPersonBelongToNonAdmitGroupOnly;
    }

    private bool DoesPersonBelongToAnyAdmitGroupInternal()
    {
        _doesPersonBelongToAnyAdmitGroup = false;
        if (_audiences != null && _audiences.Count > 0)
        {
            foreach (Audience audience in _audiences)
            {
                if (audience.IsAdmit)
                {
                    _doesPersonBelongToAnyAdmitGroup = true;

                    break;
                }
            }
        }
        return _doesPersonBelongToAnyAdmitGroup;
    }

    private bool DoesPersonBelongToAnyNonAdmitGroupInternal()
    {
        _doesPersonBelongToAnyNonAdmitGroup = false;
        if (_audiences != null && _audiences.Count > 0)
        {
            foreach (Audience audience in _audiences)
            {
                if (!audience.IsAdmit)
                {
                    _doesPersonBelongToAnyNonAdmitGroup = true;
                    break;
                }
            }
        }
        return _doesPersonBelongToAnyNonAdmitGroup;
    }

    private bool DoesPersonBelongToStaffGroupInternal()
    {
        _doesPersonBelongToStaff = false;

        if (IsStaff)
        {
            _doesPersonBelongToStaff = true;
        }
        return _doesPersonBelongToStaff;
    }

    private bool DoesPersonBelongToFacultyGroupInternal()
    {
        _doesPersonBelongToFaculty = false;

        if (IsFaculty)
        {
            _doesPersonBelongToFaculty = true;
        }
        return _doesPersonBelongToFaculty;
    }

    private bool DoesPersonBelongToAdminGroupInternal()
    {
        _doesPersonBelongToAdmin = false;

        if (IsAdmin || IsMasteradmin)
        {
            _doesPersonBelongToAdmin = true;
        }
        return _doesPersonBelongToAdmin;
    }

    private bool DoesPersonBelongToNonAffiliatesInternal()
    {
        _doesPersonBelongToNonAffiliates = false;

        if (_adminRole == "none" && !IsAdmit && _audiences != null && _audiences.Count > 0)
        {
            _doesPersonBelongToNonAffiliates = true;
        }
        return _doesPersonBelongToNonAffiliates;
    }



    private bool IsMatricualtedInternal()
    {
        if (_univid != null)
            _isMatriculated = Convert.ToBoolean(_dal.GetMatriculatedPerson(_univid));
        return _isMatriculated;
    }

    private bool ExhibitAdmitBehaviourForOutlookInternal()
    {

        // Case 1: When viewer is part of admit group, show admit behavior,
        // Case 2: When viewer is part of admit and student group and also the matriculated workgroup 
        // show admit behavior.
        // Case 3: When viewer is part of admit and student workgroup (and NOT matriculated workgroup), show student behavior
        _exhibitAdmitBehaviourForOutlook = false;

        // Case 1: Person belongs to Admit Group only
        if (_doesPersonBelongToAdmitGroupOnly && !_doesPersonBelongToAnyNonAdmitGroup && !_isMatriculated)
        {
            _exhibitAdmitBehaviourForOutlook = true;
            return _exhibitAdmitBehaviourForOutlook;
        }

        // Case 2: If person belong to Admit, Non-Admit and matricualted; If the Admit and Non-Admit year is same then he is admit
        if (_doesPersonBelongToAnyAdmitGroup && _doesPersonBelongToAnyNonAdmitGroup && _isMatriculated)
        {
            // Check if the Admit and Non-Admit year is same
            if (!_doesPersonBelongToDissimilarAdmitAndNonAdmitAudience)
            {
                _exhibitAdmitBehaviourForOutlook = true;
                return _exhibitAdmitBehaviourForOutlook;
            }
        }

        return _exhibitAdmitBehaviourForOutlook;
    }

    private bool CanPersonAddMessageToOutlookInternal()
    {
        _canPersonAddMessageToOutlook = false;

        

        return _canPersonAddMessageToOutlook;
    }






    private void BuildAdmitAndNonAdmitYears()
    {
        if (_audiences != null && _audiences.Count > 0)
        {
            foreach (Audience audience in _audiences)
            {
                int year;
                if ((audience.Type.Length > 4) && (Int32.TryParse(audience.Type.Substring(audience.Type.Length - 4), out year)))
                {
                    if (audience.IsAdmit)
                        _admitYears.Add(year);
                    else
                        _nonAdmitYears.Add(year);
                }

                if (audience.IsAdmit && audience.CanAddEventToOutlook)
                    _admitOutlookTriggerOn = true;
                if (!audience.IsAdmit && audience.CanAddEventToOutlook)
                    _nonAdmitOutlookTriggerOn = true;
            }
        }
    }

    private bool DoesPersonBelongToDissimilarAdmitAndNonAdmitAudienceInternal()
    {
        _doesPersonBelongToDissimilarAdmitAndNonAdmitAudience = false;

        if ((_admitYears.Count > 0) && (_nonAdmitYears.Count > 0)) // He belong to multiple so check
        {
            if (_nonAdmitYears.Count == 1)  // Single Non-Admit
            {
                if (!_nonAdmitYears.Contains(_admitYears.First()))
                    _doesPersonBelongToDissimilarAdmitAndNonAdmitAudience = true;
            }
            else
                _doesPersonBelongToDissimilarAdmitAndNonAdmitAudience = true;
        }

        return _doesPersonBelongToDissimilarAdmitAndNonAdmitAudience;
    }


    private void SetSameGreaterLesserFlags()
    {
        _doesPersonBelongToAGreaterNonAdmitAudience = false;
        _doesPersonBelongToALesserNonAdmitAudience = false;
        _doesPersonBelongToSameAdmitAndNonAdmitAudience = false;

        if ((_admitYears.Count > 0) && (_nonAdmitYears.Count > 0)) // He belong to multiple so check
        {
            int admitYear = _admitYears.First();
            foreach (var noAdmityear in _nonAdmitYears)
            {
                if (noAdmityear > admitYear)
                    _doesPersonBelongToAGreaterNonAdmitAudience = true;
                else if (noAdmityear < admitYear)
                    _doesPersonBelongToALesserNonAdmitAudience = true;
                else if (noAdmityear == admitYear)
                    _doesPersonBelongToSameAdmitAndNonAdmitAudience = true;
            }
        }
    }


    /// <summary>
    /// Check if user has "faculty" as primary affiliation
    /// </summary>
    public bool IsFaculty
    {
        get
        {
            Dictionary<string, bool> affiliations = (Dictionary<string, bool>)Affiliations();
            foreach (string affiliation in affiliations.Keys)
            {
                if (affiliation.Trim().ToLower() == "faculty")
                {
                    if (affiliations[affiliation]) return true;
                }
            }

            return false;
        }
    }

    /// <summary>
    /// Check if user has "staff" as primary affiliation
    /// </summary>
    public bool IsStaff
    {
        get
        {
            Dictionary<string, bool> affiliations = (Dictionary<string, bool>)Affiliations();
            foreach (string affiliation in affiliations.Keys)
            {
                if (affiliation.Trim().ToLower() == "staff")
                {
                    if (affiliations[affiliation]) return true;
                }
            }

            return false;
        }
    }

    /// <summary>
    /// Check if user is a student by checking his/she audience record(s)
    /// </summary>
    public bool IsStudent
    {
        get
        {
            bool isStudent = false;

            if (_audiences != null && _audiences.Count > 0)
            {
                foreach (Audience audience in _audiences)
                {
                    if (audience.IsMBA || audience.IsPhD || audience.IsMSx)
                    {
                        isStudent = true;
                        break;
                    }
                }
            }

            return isStudent;
        }
    }

    #endregion

    #region Public Methods


    /// <summary>
    /// Set last digest sent
    /// </summary>
    public void SetLastDigestSent()
    {


    }




    #endregion

    #region Relations
    /// <summary>
    /// Return a collection of key/value pair of affiliations
    /// </summary>
    public IDictionary Affiliations()
    {
        Dictionary<string, bool> affiliations = new Dictionary<string, bool>();

        //SqlDataReader rd = _dal.GetPersonAffiliations(_uid);
        //if (rd != null)
        //{
        //    if (rd.HasRows)
        //    {
        //        rd.Read();

        //        affiliations.Add(rd["affiliation"].ToString(),
        //            Convert.IsDBNull(rd["is_primary"]) ? false : (rd["is_primary"].ToString().Equals("0") ? false : true));
        //    }
        //    rd.Close();
        //}

        return affiliations;
    }

    /// <summary>
    /// Array list of all email digest messages
    /// </summary>
    /// <returns>An array list of messages</returns>
    public IList EmailDigestMessages()
    {
        return EmailDigestMessages(null, DateTime.MinValue);
    }

    /// <summary>
    /// Array list of all email digest messages for a selected date
    /// </summary>
    /// <param name="messageSelectDate">Date from which the messages are to be selected</param>
    /// <returns>An array list of messages</returns>
    public IList EmailDigestMessages(DateTime messageSelectDate)
    {
        return EmailDigestMessages(null, messageSelectDate);
    }

    /// <summary>
    /// Array list of all email digest messages for selected audience types and date
    /// </summary>
    /// <param name="audienceTypes">An array of audience types</param>
    /// <param name="messageSelectDate">Date from which the messages are to be selected</param>
    /// <returns>An array list of messages</returns>
    public IList EmailDigestMessages(string[] audienceTypes, DateTime messageSelectDate)
    {
        // Make sure univid/audience type is defined
        if (_digestFrequency == null || _digestFrequency.Trim().Length.Equals(0) || (_univid == null || _univid.Trim().Length.Equals(0)) &&
            (audienceTypes == null || audienceTypes.Length.Equals(0)) && (_audiences == null || _audiences.Count.Equals(0)))
        {
            throw new Exception("No person information");
        }
        else if (_univid != null && _univid.Trim().Length > 0 && (audienceTypes == null || audienceTypes.Length.Equals(0)))
        {
            if (_audiences != null && _audiences.Count > 0)
            {
                audienceTypes = new string[_audiences.Count];
                for (int i = 0; i < _audiences.Count; i++) audienceTypes[i] = ((Audience)_audiences[i]).Type;
            }
        }

        return GetEmailDigestMessages(_digestFrequency, audienceTypes, _univid, messageSelectDate);
    }

    /// <summary>
    /// Return an array list of outlook events by message type
    /// </summary>
    /// <param name="messageType">Message type</param>
    /// <param name="selectStartDate">Start date for events selection (nullable --> DateTime.MinValue; default to last quarter's end date plus 1)</param>
    /// <param name="selectEndDate">End date for events selection (nullable --> DateTime.MaxValue; default to start date plus 10 years)</param>
    /// <param name="currentRunDateTime">Current run date and time (nullable --> DateTime.MinValue)</param>
    /// <param name="includeStaleEvent">Indicate if to include past quarter events</param>
   
    /// <summary>
    /// Convert person object to xml
    /// </summary>
    

    /// <summary>
    /// Academic calendar
    /// </summary>
    /// <param name="currentRunDateTime">Current run date and time</param>
    /// <param name="selectStartDate">Start date from which the section booking information are to be selected</param>
    /// <param name="selectEndDate">End date from which the section booking information are to be selected</param>
    /// <returns>Array list of academic calendar</returns>
    public IList<Message> AcademicCalendar(DateTime currentRunDateTime, DateTime selectStartDate, DateTime selectEndDate)
    {
        return GetAcademicCalendar(currentRunDateTime, selectStartDate, selectEndDate);
    }

    /// <summary>
    /// Enrolled section
    /// </summary>
    /// <param name="reservationID">Reservation ID for the enrolled section</param>
    /// <param name="selectStartDate">Start date from which the section booking information are to be selected</param>
    /// <param name="selectEndDate">End date from which the section booking information are to be selected</param>
    /// <param name="includeWaitlisted">Include waitlisted sections</param>
    /// <returns>Array list of enrolled sections</returns>
    public IList<Message> EnrolledSection(int reservationID, DateTime selectStartDate, DateTime selectEndDate, bool includeWaitlisted)
    {
        return GetEnrolledSections(reservationID, DateTime.MinValue, selectStartDate, selectEndDate, includeWaitlisted);
    }

    /// <summary>
    /// Enrolled sections
    /// </summary>
    /// <param name="currentRunDateTime">Current run date and time</param>
    /// <param name="selectStartDate">Start date from which the section booking information are to be selected</param>
    /// <param name="selectEndDate">End date from which the section booking information are to be selected</param>
    /// <param name="includeWaitlisted">Include waitlisted sections</param>
    /// <returns>Array list of enrolled sections</returns>
    public IList<Message> EnrolledSections(DateTime currentRunDateTime, DateTime selectStartDate, DateTime selectEndDate, bool includeWaitlisted)
    {
        return GetEnrolledSections(int.MinValue, currentRunDateTime, selectStartDate, selectEndDate, includeWaitlisted);
    }

    /// <summary>
    /// Messages
    /// </summary>
    /// <returns>Messages collection</returns>
    public IList<Message> Messages()
    {
        return Messages(null, null, null, DateTime.MinValue, DateTime.MinValue, int.MinValue, int.MinValue, int.MinValue, string.Empty, false);
    }

    /// <summary>
    /// Messages
    /// </summary>
    /// <param name="messageType">Message type</param>
    /// <param name="messageCategories">An array of message category ids</param>
    /// <param name="audienceTypes">An array of audience types</param>
    /// <param name="selectStartDate">Start date from which the messages are to be selected</param>
    /// <param name="selectEndDate">End date from which the messages are to be selected</param>
    /// <param name="minMessages">Minimum messages to be selected</param>
    /// <param name="pageSize">Number of messages per page</param>
    /// <param name="pageNumber">Current page number</param>
    /// <param name="sortClause">Message sort criteria</param>
    /// <param name="mandatoryOnly">Indicate if to select mandatory messages only</param>
    /// <returns>Messages collection</returns>
    public IList<Message> Messages(string messageType, string[] messageCategories, string[] audienceTypes, DateTime selectStartDate,
        DateTime selectEndDate, int minMessages, int pageSize, int pageNumber, string sortClause, bool mandatoryOnly)
    {
        CheckPersonAndAudienceInfo(audienceTypes);

        // In this case we are more interested with message by audience type (i.e. showing all message even those that were unsubscribed
        // by the user). Hence, the default call is by audience type. Note that if univid is passed then the stored procedure will use
        // this information to filter messages that have been unsubscribed by the user (functionality of email digest)
        if (audienceTypes != null && audienceTypes.Length > 0)
        {
            return GetMessages(messageType, messageCategories, audienceTypes, null, selectStartDate, selectEndDate, minMessages,
                pageSize, pageNumber, sortClause, mandatoryOnly);
        }
        else if (_audiences != null && _audiences.Count > 0)
        {
            audienceTypes = new string[_audiences.Count];
            for (int i = 0; i < _audiences.Count; i++) audienceTypes[i] = ((Audience)_audiences[i]).Type;
            return GetMessages(messageType, messageCategories, audienceTypes, _univid, selectStartDate, selectEndDate, minMessages,
                pageSize, pageNumber, sortClause, mandatoryOnly);
        }
        else return GetMessages(messageType, messageCategories, null, _univid, selectStartDate, selectEndDate, minMessages, pageSize,
                pageNumber, sortClause, mandatoryOnly);

    }

    /// <summary>
    /// Enrolled section in xml document
    /// </summary>
    /// <param name="reservationID">Reservation ID for the enrolled section</param>
    /// <param name="selectStartDate">Start date from which the section booking information are to be selected</param>
    /// <param name="selectEndDate">End date from which the section booking information are to be selected</param>
    /// <param name="includeWaitlisted">Include waitlisted sections</param>
    /// <returns>Xml document</returns>
    public XmlDocument XmlEnrolledSection(int reservationID, DateTime selectStartDate, DateTime selectEndDate, bool includeWaitlisted)
    {
        return GetXmlEnrolledSections(reservationID, DateTime.MinValue, selectStartDate, selectEndDate, includeWaitlisted);
    }

    /// <summary>
    /// Enrolled sections in xml document
    /// </summary>
    /// <param name="selectStartDate">Start date from which the section booking information are to be selected</param>
    /// <param name="selectEndDate">End date from which the section booking information are to be selected</param>
    /// <param name="includeWaitlisted">Include waitlisted sections</param>
    /// <returns>Xml document</returns>
    public XmlDocument XmlEnrolledSections(DateTime selectStartDate, DateTime selectEndDate, bool includeWaitlisted)
    {
        return GetXmlEnrolledSections(int.MinValue, DateTime.MinValue, selectStartDate, selectEndDate, includeWaitlisted);
    }

    /// <summary>
    /// Messages in Xml document
    /// </summary>
    /// <returns>Xml document</returns>
    public XmlDocument XmlMessages()
    {
        return XmlMessages(null, null, null, DateTime.MinValue, DateTime.MinValue, int.MinValue, int.MinValue, int.MinValue, string.Empty, false, false);
    }

    /// <summary>
    /// Messages in Xml document
    /// </summary>
    /// <param name="messageType">Message type</param>
    /// <param name="messageCategories">An array of message category ids</param>
    /// <param name="audienceTypes">An array of audience types</param>
    /// <param name="selectStartDate">Start date from which the messages are to be selected</param>
    /// <param name="selectEndDate">End date from which the messages are to be selected</param>
    /// <param name="minMessages">Minimum messages to be selected</param>
    /// <param name="pageSize">Number of messages per page</param>
    /// <param name="pageNumber">Current page number</param>
    /// <param name="sortClause">Message sort criteria</param>
    /// <param name="mandatoryOnly">Indicate if to select mandatory messages only</param>
    /// <returns>Xml document</returns>
    public XmlDocument XmlMessages(string messageType, string[] messageCategories, string[] audienceTypes, DateTime selectStartDate,
        DateTime selectEndDate, int minMessages, int pageSize, int pageNumber, string sortClause, bool mandatoryOnly, bool calendarOnly)
    {
        CheckPersonAndAudienceInfo(audienceTypes);

        // In this case we are more interested with message by audience type (i.e. showing all message even those that were unsubscribed
        // by the user). Hence, the default call is by audience type. Note that if univid is passed then the stored procedure will use
        // this information to filter messages that have been unsubscribed by the user (functionality of email digest)
        if (audienceTypes != null && audienceTypes.Length > 0)
        {
            return GetXmlMessages(messageType, messageCategories, audienceTypes, null, selectStartDate, selectEndDate, minMessages,
                pageSize, pageNumber, sortClause, mandatoryOnly, calendarOnly);
        }
        else if (_audiences != null && _audiences.Count > 0)
        {
            audienceTypes = new string[_audiences.Count];
            for (int i = 0; i < _audiences.Count; i++) audienceTypes[i] = ((Audience)_audiences[i]).Type;
            return GetXmlMessages(messageType, messageCategories, audienceTypes, _univid, selectStartDate, selectEndDate, minMessages,
                pageSize, pageNumber, sortClause, mandatoryOnly, calendarOnly);
        }
        else return GetXmlMessages(messageType, messageCategories, null, _univid, selectStartDate, selectEndDate, minMessages, pageSize,
                pageNumber, sortClause, mandatoryOnly, calendarOnly);

    }
    #endregion

    #region Private Methods
    /// <summary>
    /// Check person unvid id and/or audience types
    /// </summary>
    /// <param name="audienceTypes">Audience types</param>
    private void CheckPersonAndAudienceInfo(string[] audienceTypes)
    {
        // Make sure univid is defined
        if ((_univid == null || _univid.Trim().Length.Equals(0)) && (audienceTypes == null || audienceTypes.Length.Equals(0)) &&
            (_audiences == null || _audiences.Count.Equals(0)))
        {
            throw new Exception("No person information");
        }
        // Make sure a least one audience
        else if (_univid != null && _univid.Trim().Length > 0 && (audienceTypes == null || audienceTypes.Length.Equals(0)) &&
            (_audiences == null || _audiences.Count.Equals(0)))
        {
            throw new Exception("No audience information");
        }
    }

    /// <summary>
    /// Get Addition person properties
    /// </summary>
    

    /// <summary>
    /// Get email digest messages
    /// </summary>
    /// <param name="emailDigestFrequency">Email digest frequency</param>
    /// <param name="audienceTypes">An array of audience types</param>
    /// <param name="univid">Univid</param>
    /// <param name="messageSelectDate">Date from which the messages are to be selected</param>
    /// <returns>Array list of messages</returns>
    private IList GetEmailDigestMessages(string emailDigestFrequency, string[] audienceTypes, string univid, DateTime messageSelectDate)
    {
        IList messages = new ArrayList();

        // SQL where and sort clauses
        string whereClause = string.Empty;
        string sortClause = string.Empty;
        if (audienceTypes != null && audienceTypes.Length > 0)
        {
            // combining "where" clause
            foreach (string audienceType in audienceTypes)
            {
                if (whereClause.Trim().Length > 0) whereClause += " OR ";
                whereClause += "(" + Settings.EmailDigestSelect(emailDigestFrequency, audienceType) + ")";
            }

            // sort clause is harder to combine. Use default sort clause if there are more than 1 audience types
            if (audienceTypes.Length == 1) sortClause = Settings.EmailDigestSort(emailDigestFrequency, audienceTypes[0]);
            else sortClause = Settings.EmailDigestSort(emailDigestFrequency, null);
        }
        else
        {
            whereClause = Settings.EmailDigestSelect(emailDigestFrequency, null);
            sortClause = Settings.EmailDigestSort(emailDigestFrequency, null);
        }

        SqlDataReader drMessages = _dal.GetMessages(null, audienceTypes, univid, messageSelectDate, DateTime.MinValue, int.MinValue,
            int.MinValue, int.MinValue, whereClause, sortClause, false, false);
        if (drMessages != null)
        {
            while (drMessages.Read())
            {
                Message message = new Message(drMessages);
                messages.Add(message);
            }
            drMessages.Close();
        }

        drMessages.Close();

        return messages;
    }

    /// <summary>
    /// Get academic calendar
    /// </summary>
    /// <param name="currentRunDateTime">Current date and time (nullable --> DateTime.MinValue)</param>
    /// <param name="selectStartDate">Message selection start date and time</param>
    /// <param name="selectEndDate">Message selection end date and time</param>
    /// <returns>Array list of academic calendar</returns>
    private IList<Message> GetAcademicCalendar(DateTime currentRunDateTime, DateTime selectStartDate, DateTime selectEndDate)
    {
        IList<Message> academicCalendar = new List<Message>();


        return academicCalendar;
    }

    /// <summary>
    /// Get enrolled sections
    /// </summary>
    /// <param name="reservationID">Reservation ID</param>
    /// <param name="currentRunDateTime">Current date and time (nullable --> DateTime.MinValue)</param>
    /// <param name="selectStartDate">Start date from which the section booking information are to be selected</param>
    /// <param name="selectEndDate">End date from which the section booking information are to be selected</param>
    /// <param name="includeWaitlisted">Include waitlisted sections</param>
    /// <returns>Array list of enrolled sections</returns>
    private IList<Message> GetEnrolledSections(int reservationID, DateTime currentRunDateTime, DateTime selectStartDate,
        DateTime selectEndDate, bool includeWaitlisted)
    {
        IList<Message> enrolledSections = new List<Message>();



        return enrolledSections;
    }

    /// <summary>
    /// Get Messages
    /// </summary>
    /// <param name="messageType">Message type</param>
    /// <param name="messageCategories">An array of message category ids</param>
    /// <param name="audienceTypes">An array of audience types</param>
    /// <param name="univid">Univid</param>
    /// <param name="selectStartDate">Message selection start date and time</param>
    /// <param name="selectEndDate">Message selection end date and time</param>
    /// <param name="minMessages">Minimum number of messages</param>
    /// <param name="pageSize">Number of messages per page</param>
    /// <param name="pageNumber">Current page number</param>
    /// <param name="sortClause">Message sort criteria</param>
    /// <param name="mandatoryOnly">Indicate if to select mandatory messages only</param>
    /// <returns>Messages collection</returns>
    private IList<Message> GetMessages(string messageType, string[] messageCategories, string[] audienceTypes, string univid,
        DateTime selectStartDate, DateTime selectEndDate, int minMessages, int pageSize, int pageNumber, string sortClause, bool mandatoryOnly)
    {
        IList<Message> messages = new List<Message>();


        return messages;
    }

    /// <summary>
    /// Get enrolled sections in xml document
    /// </summary>
    /// <param name="reservationID">Reservation ID</param>
    /// <param name="currentRunDateTime">Current date and time (nullable --> DateTime.MinValue)</param>
    /// <param name="selectStartDate">Start date from which the section booking information are to be selected</param>
    /// <param name="selectEndDate">End date from which the section booking information are to be selected</param>
    /// <param name="includeWaitlisted">Include waitlisted sections</param>
    /// <returns>Xml document</returns>
    private XmlDocument GetXmlEnrolledSections(int reservationID, DateTime currentRunDateTime, DateTime selectStartDate, DateTime selectEndDate,
        bool includeWaitlisted)
    {
        XmlDocument xmlEnrolledSections = new XmlDocument();
        XmlNode xmlEnrolledSectionsNode = xmlEnrolledSections.CreateNode(XmlNodeType.Element, "messages", xmlEnrolledSections.NamespaceURI);

       
        return xmlEnrolledSections;
    }

    /// <summary>
    /// Get xml Messages
    /// </summary>
    /// <param name="messageType">Message type</param>
    /// <param name="messageCategories">An array of message category ids</param>
    /// <param name="audienceTypes">An array of audience types</param>
    /// <param name="univid">Univid</param>
    /// <param name="selectStartDate">Message selection start date and time</param>
    /// <param name="selectEndDate">Message selection end date and time</param>
    /// <param name="minMessages">Minimum number of messages</param>
    /// <param name="pageSize">Number of messages per page</param>
    /// <param name="pageNumber">Current page number</param>
    /// <param name="sortClause">Message sort criteria</param>
    /// <param name="mandatoryOnly">Indicate if to select mandatory messages only</param>
    /// <returns>XmlDocument of messages</returns>
    private XmlDocument GetXmlMessages(string messageType, string[] messageCategories, string[] audienceTypes, string univid,
        DateTime selectStartDate, DateTime selectEndDate, int minMessages, int pageSize, int pageNumber, string sortClause, bool mandatoryOnly, bool calendarOnly)
    {
        int totalMessageCount = int.MinValue;
        XmlDocument xmlMessages = new XmlDocument();
        XmlNode xmlMessagesNode = xmlMessages.CreateNode(XmlNodeType.Element, "messages", xmlMessages.NamespaceURI);

        
    return null;
    }

    /// <summary>
    /// Load person properties
    /// </summary>
    /// <param name="rd">Person SQL data Reader</param>
    private void LoadProperties(SqlDataReader rd)
    {
        rd.Read();

        this._univid = rd["univid"].ToString();
        this._uid = rd["uid"].ToString();
        this._publicid = rd["public_id"].ToString();
        this._nameFirst = rd["FNAME"].ToString();
        this._nameLast = rd["LNAME"].ToString();
        this._emailPrimary = rd["EMAIL"].ToString();
        this._emailSecondary = rd["secondary_email"].ToString();
    }
    #endregion
}
