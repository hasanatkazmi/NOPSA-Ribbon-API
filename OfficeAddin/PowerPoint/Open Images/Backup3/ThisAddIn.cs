using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml.Linq;
using PowerPoint = Microsoft.Office.Interop.PowerPoint;
using Office = Microsoft.Office.Core;
using System.Diagnostics;

namespace PowerPointAddInOne
{
    public partial class ThisAddIn
    {
        public static Boolean isstarted = false;
        public static SearchUserControl searchUC = new SearchUserControl();
        public static Microsoft.Office.Tools.CustomTaskPane searchCTP;

        private void ThisAddIn_Startup(object sender, System.EventArgs e)
        {
            searchCTP = this.CustomTaskPanes.Add( ThisAddIn.searchUC, "Search");
            searchCTP.Width = 350;
            
            ThisAddIn.isstarted = true;


            ApplicationInteraction.PowerPointApplication = this.Application;

        }

        private void ThisAddIn_Shutdown(object sender, System.EventArgs e)
        {
        }


        #region VSTO generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InternalStartup()
        {
            this.Startup += new System.EventHandler(ThisAddIn_Startup);
            this.Shutdown += new System.EventHandler(ThisAddIn_Shutdown);
        }
        
        #endregion
    }
}
