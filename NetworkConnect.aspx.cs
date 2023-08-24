using System;
using System.Collections;
using System.Collections.Generic;
using System.Configuration;
using System.IO;
using System.Linq;
using System.Security.Claims;
using System.Text.RegularExpressions;

public partial class WebForm1 : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {

    }

    protected void findTemplate_Click(object sender, EventArgs e)
    {
        //try
        //{


        //    Audience aud = new Audience();
        //    aud.Name = audience.Items[audience.SelectedIndex].Text;
        //    aud.Type = audience.Items[audience.SelectedIndex].Value;
        //    aud.AudienceName = audience.Items[audience.SelectedIndex].Text;
        //    lblTemplatePath.Text = Util.FindMatchingTemplate(aud);
        //    message.Text = Util.GetTemplateHtml(aud);

        //    EmailDigestServiceUtils email = new EmailDigestServiceUtils();
        //    email.FormatEmail(aud, "daily", new DateTime());

        //}
        //catch (Exception ex)
        //{
        //    message.Text = ex.Message;
        //}
    }
}
