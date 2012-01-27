namespace PowerPointAddInOne
{
    partial class NOPSAribbon : Microsoft.Office.Tools.Ribbon.RibbonBase
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        public NOPSAribbon()
            : base(Globals.Factory.GetRibbonFactory())
        {
            InitializeComponent();
        }

        /// <summary> 
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Component Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(NOPSAribbon));
            this.tab12 = this.Factory.CreateRibbonTab();
            this.Search = this.Factory.CreateRibbonGroup();
            this.searchBtn = this.Factory.CreateRibbonButton();
            this.CC = this.Factory.CreateRibbonGroup();
            this.cc_btn = this.Factory.CreateRibbonButton();
            this.tab12.SuspendLayout();
            this.Search.SuspendLayout();
            this.CC.SuspendLayout();
            // 
            // tab12
            // 
            this.tab12.ControlId.ControlIdType = Microsoft.Office.Tools.Ribbon.RibbonControlIdType.Office;
            this.tab12.Groups.Add(this.Search);
            this.tab12.Groups.Add(this.CC);
            this.tab12.Label = "Open Images";
            this.tab12.Name = "tab12";
            // 
            // Search
            // 
            this.Search.Items.Add(this.searchBtn);
            this.Search.Label = "Search";
            this.Search.Name = "Search";
            // 
            // searchBtn
            // 
            this.searchBtn.Image = ((System.Drawing.Image)(resources.GetObject("searchBtn.Image")));
            this.searchBtn.Label = "Search";
            this.searchBtn.Name = "searchBtn";
            this.searchBtn.ShowImage = true;
            this.searchBtn.ShowLabel = false;
            this.searchBtn.Click += new Microsoft.Office.Tools.Ribbon.RibbonControlEventHandler(this.searchBtn_Click);
            // 
            // CC
            // 
            this.CC.Items.Add(this.cc_btn);
            this.CC.Label = "Creative Common";
            this.CC.Name = "CC";
            // 
            // cc_btn
            // 
            this.cc_btn.ControlSize = Microsoft.Office.Core.RibbonControlSize.RibbonControlSizeLarge;
            this.cc_btn.Image = ((System.Drawing.Image)(resources.GetObject("cc_btn.Image")));
            this.cc_btn.Label = "Create";
            this.cc_btn.Name = "cc_btn";
            this.cc_btn.ShowImage = true;
            this.cc_btn.Click += new Microsoft.Office.Tools.Ribbon.RibbonControlEventHandler(cc_btn_Click);
            // 
            // NOPSAribbon
            // 
            this.Name = "NOPSAribbon";
            this.RibbonType = "Microsoft.PowerPoint.Presentation";
            this.Tabs.Add(this.tab12);
            this.Tag = "Creative Commons";
            this.Load += new Microsoft.Office.Tools.Ribbon.RibbonUIEventHandler(this.NOPSAribbon_Load);
            this.tab12.ResumeLayout(false);
            this.tab12.PerformLayout();
            this.Search.ResumeLayout(false);
            this.Search.PerformLayout();
            this.CC.ResumeLayout(false);
            this.CC.PerformLayout();

        }

        #endregion

        internal Microsoft.Office.Tools.Ribbon.RibbonTab tab12;
        internal Microsoft.Office.Tools.Ribbon.RibbonGroup Search;
        internal Microsoft.Office.Tools.Ribbon.RibbonButton searchBtn;
        internal Microsoft.Office.Tools.Ribbon.RibbonGroup CC;
        internal Microsoft.Office.Tools.Ribbon.RibbonButton cc_btn;

    }

    partial class ThisRibbonCollection
    {
        internal NOPSAribbon NOPSAribbon
        {
            get { return this.GetRibbon<NOPSAribbon>(); }
        }
    }
}
