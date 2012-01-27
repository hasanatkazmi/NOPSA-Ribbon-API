using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Office.Tools.Ribbon;
using System.Diagnostics;
using Microsoft.Office.Tools;

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



    }
}
