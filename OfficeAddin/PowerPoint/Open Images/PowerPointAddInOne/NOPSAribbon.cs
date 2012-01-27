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
            //MessageBox.Show("Error Message", "Error Title", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            MessageBox.Show(@"
Copyright (C) 2011 Hasanat Kazmi & Herkko Hietanen (Helsinki Institute for Information Technology), Sponsored by Google Summer of Code 2011

Permission is hereby granted, free of charge, to any person obtaining a copy of this software and associated documentation files (the ""Software""), to deal in the Software without restriction, including without limitation the rights to use, copy, modify, merge, publish, distribute, sublicense, and/or sell copies of the Software, and to permit persons to whom the Software is furnished to do so, subject to the following conditions:

The above copyright notice and this permission notice shall be included in all copies or substantial portions of the Software.

THE SOFTWARE IS PROVIDED ""AS IS"", WITHOUT WARRANTY OF ANY KIND, EXPRESS OR IMPLIED, INCLUDING BUT NOT LIMITED TO THE WARRANTIES OF MERCHANTABILITY, FITNESS FOR A PARTICULAR PURPOSE AND NONINFRINGEMENT. IN NO EVENT SHALL THE AUTHORS OR COPYRIGHT HOLDERS BE LIABLE FOR ANY CLAIM, DAMAGES OR OTHER LIABILITY, WHETHER IN AN ACTION OF CONTRACT, TORT OR OTHERWISE, ARISING FROM, OUT OF OR IN CONNECTION WITH THE SOFTWARE OR THE USE OR OTHER DEALINGS IN THE SOFTWARE.",
            "About", MessageBoxButtons.OK);
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



    }
}
