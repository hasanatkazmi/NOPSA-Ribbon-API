using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Diagnostics;

namespace PowerPointAddInOne
{
    public partial class CCLicense : Form
    {
        public CCLicense()
        {
            InitializeComponent();

            webBrowser.Url = new Uri("http://ec2-107-20-212-167.compute-1.amazonaws.com:8000/CCCreator");
        }


        void webBrowser_Navigating(object sender, System.Windows.Forms.WebBrowserNavigatingEventArgs e)
        {
            //throw new System.NotImplementedException();
            webBrowser.Visible = false;
            loading_pb.Show();
        }

        void processxml(string xml)
        {
            String license_url;
            String license_name;
            String license_image;

            try
            {
                license_url = xml.Substring(xml.IndexOf("<license_url>") + 13, xml.IndexOf("</license_url>") - xml.IndexOf("<license_url>") - 13);
                Debug.WriteLine(license_url);
                license_name = xml.Substring(xml.IndexOf("<license_name>") + 14, xml.IndexOf("</license_name>") - xml.IndexOf("<license_name>") - 14);
                Debug.WriteLine(license_name);
                license_image = xml.Substring(xml.IndexOf("<license_image>") + 15, xml.IndexOf("</license_image>") - xml.IndexOf("<license_image>") - 15);
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lincese generation error." + ex.Message);
                return;
            }
            //Debug.WriteLine(license_url);
            //Debug.WriteLine(license_name);
            //Debug.WriteLine(license_image);
            ApplicationInteraction.addlicense(license_url, license_name, license_image);
        }

        void webBrowser_DocumentCompleted(object sender, System.Windows.Forms.WebBrowserDocumentCompletedEventArgs e)
        {
            //throw new System.NotImplementedException();

            if (webBrowser.DocumentText.IndexOf("<license>") == 0) // i.e. a license has been issued.
            {
                webBrowser.Visible = false;
                processxml(webBrowser.DocumentText);
                this.Visible = false; // todo: kill this form
                return;
            }

            webBrowser.Visible = true;
            loading_pb.Hide();
        }


    }
}
