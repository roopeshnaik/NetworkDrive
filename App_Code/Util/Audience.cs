#region Namespaces
using System;
using System.Data.SqlClient;
using System.Linq;
using System.Xml;
#endregion

/// <summary>
/// Summary description for Audience
/// </summary>
public class Audience
{
    #region Global Data Members

    private const string AUDIENCE_GROUP_ADMIT = "admit";
    private const string AUDIENCE_GROUP_MBA = "mba";
    private const string AUDIENCE_GROUP_OTHER = "other";
    private const string AUDIENCE_GROUP_PHD = "phd";
    private const string AUDIENCE_GROUP_MSX = "msx";
    private const string AUDIENCE_GROUP_STUDENT = "student";

    private int? _id;
    private string _type;
    private string _name;
    private bool _isActive = false;
    private bool _isAdmit = false;
    private bool _isMBA = false;
    private bool _isMBAAdmit = false;
    private bool _isOther = false;
    private bool _isPhD = false;
    private bool _isPhDAdmit = false;
    private bool _isMSx = false;
    private bool _isMSxAdmit = false;
    private bool _isAudienceRestricted = false;
    private bool _canAddEventToOutlook = false;

    private string _audName;
    private bool _isStudent = false;
    private string _workGroup;
    //private string _admitRound;
    //private string _audYear;
    //private string _audClass;
    //private string _date_modified_source;
    private string _createdBy;
    private string _audCode;
    private string _audType;
    private int _audIsActive;
    private int _organization_id;
    private string _modified_by;
    private string _modified_Date;
    private string _addToOutlook;


    #endregion

    #region Constructors
    /// <summary>
    /// Message audience
    /// </summary>
    public Audience()
    {

    }

    public Audience(string audienceType)
    {

    }

    /// <summary>
    /// Message audience
    /// </summary>
    /// <param name="dr">Audience data reader</param>
    public Audience(SqlDataReader dr)
    {
        SetProperties(dr);
    }
    #endregion

    #region Properties
    /// <summary>
    /// Audience Id
    /// </summary>
    public int? Id
    {
        get { return _id; }
        set { _id = value; }
    }

    /// <summary>
    /// Audience name
    /// </summary>
    public string Name
    {
        get { return _name; }
        set { _name = value; }
    }

    /// <summary>
    /// Audience Type
    /// </summary>
    public string Type
    {
        get { return _type; }
        set { _type = value; }
    }

    /// <summary>
    /// Active/Inactive Audience
    /// </summary>
    public bool IsActive
    {
        get { return _isActive; }
        set { _isActive = value; }
    }

    /// <summary>
    /// Admit audience type
    /// </summary>
    public bool IsAdmit
    {
        get
        {
            if (_type.ToLower().Contains(AUDIENCE_GROUP_ADMIT.ToLower())) _isAdmit = true;
            return _isAdmit;
        }
    }

    /// <summary>
    /// MBA audience type
    /// </summary>
    public bool IsMBA
    {
        get
        {
            if (_type.ToLower().Contains(AUDIENCE_GROUP_MBA.ToLower())) _isMBA = true;
            return _isMBA;
        }
    }

    /// <summary>
    /// MBA admit audience type
    /// </summary>
    public bool IsMBAAdmit
    {
        get
        {
            if (IsAdmit && IsMBA) _isMBAAdmit = true;
            return _isMBAAdmit;
        }
    }

    /// <summary>
    /// Other audience type
    /// </summary>
    public bool IsOther
    {
        get
        {
            if (_type.ToLower().Contains(AUDIENCE_GROUP_OTHER.ToLower())) _isOther = true;
            return _isOther;
        }
    }

    /// <summary>
    /// PhD audience type
    /// </summary>
    public bool IsPhD
    {
        get
        {
            if (_type.ToLower().Contains(AUDIENCE_GROUP_PHD.ToLower())) _isPhD = true;
            return _isPhD;
        }
    }

    /// <summary>
    /// PhD admit audience type
    /// </summary>
    public bool IsPhDAdmit
    {
        get
        {
            if (IsAdmit && IsPhD) _isPhDAdmit = true;
            return _isPhDAdmit;
        }
    }

    /// <summary>
    /// MSx audience type
    /// </summary>
    public bool IsMSx
    {
        get
        {
            if (_type.ToLower().Contains(AUDIENCE_GROUP_MSX.ToLower())) _isMSx = true;
            return _isMSx;
        }
    }

    /// <summary>
    /// MSx admit audience type
    /// </summary>
    public bool IsMSxAdmit
    {
        get
        {
            if (IsAdmit && IsMSx) _isMSxAdmit = true;
            return _isMSxAdmit;
        }
    }

    /// <summary>
    /// Audience Active or not
    /// </summary>
    public int AudIsActive
    {
        get { return _audIsActive; }
        set { _audIsActive = value; }

    }
    /// <summary>
    /// Audience workgroup/organization name
    /// </summary>

    public string AudienceWorkgroup
    {
        get { return _workGroup; }
        set { _workGroup = value; }
    }

    public string AudienceName
    {
        get { return _audName; }
        set { _audName = value; }
    }

    /// <summary>
    /// Audience Admit Round
    /// </summary>

    //public string AudienceAdmitRound
    //{
    //    get { return _admitRound; }
    //    set { _admitRound = value; }
    //}
    //public string AudienceYear
    //{
    //    get { return _audYear; }
    //    set { _audYear = value; }
    //}

    //public string AudienceClass
    //{
    //    get { return _audClass; }
    //    set { _audClass = value; }
    //}

    public string AudienceCreatedBy
    {
        get { return _createdBy; }
        set { _createdBy = value; }
    }

    public string AudienceCode
    {
        get { return _audCode; }
        set { _audCode = value; }
    }

    public string AudienceType
    {
        get { return _audType; }
        set { _audType = value; }
    }

    public int AudOrganizationID
    {
        get { return _organization_id; }
        set { _organization_id = value; }
    }

    public string ModifiedBy
    {
        get { return _modified_by; }
        set { _modified_by = value; }
    }
    public string ModifiedDate
    {
        get { return _modified_Date; }
        set { _modified_Date = value; }
    }


    /// <summary>
    /// Audience is student
    /// </summary>
    public bool IsStudent
    {
        get
        {
            if (_type.ToLower().Contains(AUDIENCE_GROUP_STUDENT.ToLower())) _isStudent = true;
            return _isStudent;
        }
        set { _isStudent = value; }
    }

    /// <summary>
    /// Add to Outlook trigger
    /// </summary>
    public bool CanAddEventToOutlook
    {
        get { return _canAddEventToOutlook; }
        set { _canAddEventToOutlook = value; }
    }

    #endregion

    #region Public Methods
    /// <summary>
    /// Convert audience object to xml
    /// </summary>
    /// <returns>Audience XmlDocument</returns>
    public XmlDocument ToXml()
    {
        XmlDocument xmlAudience = new XmlDocument();
        XmlNode xmlAudienceNode = xmlAudience.CreateNode(XmlNodeType.Element, "audience", xmlAudience.NamespaceURI);

        if (_id == null)
        {
            throw new Exception("Audience object is empty");
        }
        else
        {
            XmlNode xmlIDNode = xmlAudience.CreateNode(XmlNodeType.Element, "id", xmlAudience.NamespaceURI);
            xmlIDNode.InnerText = _id.ToString();
            xmlAudienceNode.AppendChild(xmlIDNode);

            XmlNode xmlTypeNode = xmlAudience.CreateNode(XmlNodeType.Element, "type", xmlAudience.NamespaceURI);
            xmlTypeNode.InnerText = _type;
            xmlAudienceNode.AppendChild(xmlTypeNode);

            XmlNode xmlNameNode = xmlAudience.CreateNode(XmlNodeType.Element, "name", xmlAudience.NamespaceURI);
            xmlNameNode.InnerText = _name;
            xmlAudienceNode.AppendChild(xmlNameNode);

            XmlNode xmlIsActiveNode = xmlAudience.CreateNode(XmlNodeType.Element, "is_active", xmlAudience.NamespaceURI);
            xmlIsActiveNode.InnerText = _isActive ? "true" : "false";
            xmlAudienceNode.AppendChild(xmlIsActiveNode);
        }

        if (xmlAudienceNode.ChildNodes.Count > 0)
        {
            xmlAudience.AppendChild(xmlAudienceNode);
            return xmlAudience;
        }
        else return null;
    }
    #endregion

    #region Private Methods
    private void SetProperties(SqlDataReader dr)
    {
        try
        {
            _id = (int)dr["audience_id"];
            _type = dr["audience_type"].ToString();
            _name = dr["audience_name"].ToString();
            _isActive = (bool)dr["is_active"];
            if (dr["can_add_to_outlook"] != DBNull.Value)
                _canAddEventToOutlook = Convert.ToBoolean(Convert.ToInt32(dr["can_add_to_outlook"]));
        }
        catch
        { }
    }
    #endregion
}
