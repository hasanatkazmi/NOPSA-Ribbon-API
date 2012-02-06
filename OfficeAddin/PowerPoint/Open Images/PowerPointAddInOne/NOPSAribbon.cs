using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Office.Tools.Ribbon;
using System.Diagnostics;
using Microsoft.Office.Tools;
using System.Windows.Forms;


namespace PowerPointAddInOne
{
    public partial class NOPSAribbon
    {

        //ThisAddIn myrib = new ThisAddIn(Globals.Factory.GetRibbonFactory()
        //ThisAddIn myrib = new ThisAddIn(Globals.Factory.GetRibbonFactory(), );

        //public Microsoft.Office.Tools.CustomTaskPane searchCTP;

        private void NOPSAribbon_Load(object sender, RibbonUIEventArgs e)
        {
            searchBtn.ControlSize = Microsoft.Office.Core.RibbonControlSize.RibbonControlSizeLarge;
            
        }

        private void searchBtn_Click(object sender, RibbonControlEventArgs e)
        {
            if (ThisAddIn.searchCTP.Visible)
            {
                ThisAddIn.searchCTP.Visible = false;
            }
            else
            {
                ThisAddIn.searchCTP.Visible = true;
            }
        }

        void cc_btn_Click(object sender, Microsoft.Office.Tools.Ribbon.RibbonControlEventArgs e)
        {
            CCLicense cl = new CCLicense();
            cl.Show();

        }

        private void about_btn_Click(object sender, RibbonControlEventArgs e)
        {

            //change the link to reflect permanent one
            new AboutForm("Open Images", "http://ec2-107-20-212-167.compute-1.amazonaws.com:8000/index.htm").Show();
        }

                
        private void contact_btn_Click(object sender, RibbonControlEventArgs e)
        {
            DialogResult ans = MessageBox.Show(@"
Click OK to open your default email client to sent email to Herkko Hietanen <herkko.hietanen@hiit.fi> 
", "Contact", MessageBoxButtons.OKCancel);
            if (ans == System.Windows.Forms.DialogResult.OK)
            {
                // will this work in non debug environment?
                System.Diagnostics.Process.Start(@"mailto:herkko.hietanen@hiit.fi&subject=Open Images (contact)&body=%0D%0DSent from NOPSA's Open Images");
            }
            else 
            {
                //do nothing
            }
        }

        private void help_btn_Click(object sender, RibbonControlEventArgs e)
        {
            //change this to permanent url
            System.Diagnostics.Process.Start(@"http://ec2-107-20-212-167.compute-1.amazonaws.com:8000/help.htm");
        }

        private void settings_img_Click(object sender, RibbonControlEventArgs e)
        {
            new SettingsForm().Show();
        }



    }
}
