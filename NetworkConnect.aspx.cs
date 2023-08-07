using System;
using System.Collections;
using System.Collections.Generic;
using System.Configuration;
using System.IO;
using System.Linq;
using System.Text.RegularExpressions;

namespace NetworkDrive
{
    public partial class WebForm1 : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }

        protected void findTemplate_Click(object sender, EventArgs e)
        {
            try
            {
                Audience aud = new Audience();
                if (audience.Items[audience.SelectedIndex].Value.Contains("MBA"))
                {
                    aud.Type = "mba";
                }

                if (audience.Items[audience.SelectedIndex].Value.Contains("MSx"))
                {
                    aud.Type = "msx";
                }

                if (audience.Items[audience.SelectedIndex].Value.Contains("MBA 2025 Admit"))
                {
                    aud.Type = "mba admit";
                }

                if (audience.Items[audience.SelectedIndex].Value.Contains("MSx 2024 Admit"))
                {
                    aud.Type = "msx admit";
                }

                aud.AudienceName = audience.Items[audience.SelectedIndex].Value;
                lblTemplatePath.Text = Util.FindMatchingTemplate(aud);
                message.Text = Util.GetTemplateHtml(aud);
            }
            catch (Exception ex)
            {
                message.Text = ex.Message;
            }
        }
    }
}