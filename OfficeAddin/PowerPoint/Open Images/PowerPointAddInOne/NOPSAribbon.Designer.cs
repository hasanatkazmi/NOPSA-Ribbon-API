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
            this.CC = this.Factory.CreateRibbonGroup();
            this.About = this.Factory.CreateRibbonGroup();
            this.Contact = this.Factory.CreateRibbonGroup();
            this.Help = this.Factory.CreateRibbonGroup();
            this.searchBtn = this.Factory.CreateRibbonButton();
            this.cc_btn = this.Factory.CreateRibbonButton();
            this.about_btn = this.Factory.CreateRibbonButton();
            this.contact_btn = this.Factory.CreateRibbonButton();
            this.help_btn = this.Factory.CreateRibbonButton();
            this.tab12.SuspendLayout();
            this.Search.SuspendLayout();
            this.CC.SuspendLayout();
            this.About.SuspendLayout();
            this.Contact.SuspendLayout();
            this.Help.SuspendLayout();
            // 
            // tab12
            // 
            this.tab12.ControlId.ControlIdType = Microsoft.Office.Tools.Ribbon.RibbonControlIdType.Office;
            this.tab12.Groups.Add(this.Search);
            this.tab12.Groups.Add(this.CC);
            this.tab12.Groups.Add(this.About);
            this.tab12.Groups.Add(this.Contact);
            this.tab12.Groups.Add(this.Help);
            this.tab12.Label = "Open Images";
            this.tab12.Name = "tab12";
            // 
            // Search
            // 
            this.Search.Items.Add(this.searchBtn);
            this.Search.Label = "Search";
            this.Search.Name = "Search";
            // 
            // CC
            // 
            this.CC.Items.Add(this.cc_btn);
            this.CC.Label = "Creative Common";
            this.CC.Name = "CC";
            // 
            // About
            // 
            this.About.Items.Add(this.about_btn);
            this.About.Label = "About";
            this.About.Name = "About";
            // 
            // Contact
            // 
            this.Contact.Items.Add(this.contact_btn);
            this.Contact.Label = "Contact Us";
            this.Contact.Name = "Contact";
            // 
            // Help
            // 
            this.Help.Items.Add(this.help_btn);
            this.Help.Label = "Help";
            this.Help.Name = "Help";
            // 
            // searchBtn
            // 
            this.searchBtn.Image = ((System.Drawing.Image)(resources.GetObject("searchBtn.Image")));
            this.searchBtn.Label = " ";
            this.searchBtn.Name = "searchBtn";
            this.searchBtn.ShowImage = true;
            this.searchBtn.ShowLabel = false;
            this.searchBtn.Click += new Microsoft.Office.Tools.Ribbon.RibbonControlEventHandler(this.searchBtn_Click);
            // 
            // cc_btn
            // 
            this.cc_btn.ControlSize = Microsoft.Office.Core.RibbonControlSize.RibbonControlSizeLarge;
            this.cc_btn.Image = global::PowerPointAddInOne.Properties.Resources.cc;
            this.cc_btn.Label = " ";
            this.cc_btn.Name = "cc_btn";
            this.cc_btn.ShowImage = true;
            this.cc_btn.Click += new Microsoft.Office.Tools.Ribbon.RibbonControlEventHandler(this.cc_btn_Click);
            // 
            // about_btn
            // 
            this.about_btn.ControlSize = Microsoft.Office.Core.RibbonControlSize.RibbonControlSizeLarge;
            this.about_btn.Image = global::PowerPointAddInOne.Properties.Resources.about;
            this.about_btn.Label = " ";
            this.about_btn.Name = "about_btn";
            this.about_btn.ShowImage = true;
            this.about_btn.Click += new Microsoft.Office.Tools.Ribbon.RibbonControlEventHandler(this.about_btn_Click);
            // 
            // contact_btn
            // 
            this.contact_btn.ControlSize = Microsoft.Office.Core.RibbonControlSize.RibbonControlSizeLarge;
            this.contact_btn.Image = global::PowerPointAddInOne.Properties.Resources.contact;
            this.contact_btn.Label = " ";
            this.contact_btn.Name = "contact_btn";
            this.contact_btn.ShowImage = true;
            this.contact_btn.Click += new Microsoft.Office.Tools.Ribbon.RibbonControlEventHandler(this.contact_btn_Click);
            // 
            // help_btn
            // 
            this.help_btn.ControlSize = Microsoft.Office.Core.RibbonControlSize.RibbonControlSizeLarge;
            this.help_btn.Image = global::PowerPointAddInOne.Properties.Resources.help;
            this.help_btn.Label = " ";
            this.help_btn.Name = "help_btn";
            this.help_btn.ShowImage = true;
            this.help_btn.Click += new Microsoft.Office.Tools.Ribbon.RibbonControlEventHandler(this.help_btn_Click);
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
            this.About.ResumeLayout(false);
            this.About.PerformLayout();
            this.Contact.ResumeLayout(false);
            this.Contact.PerformLayout();
            this.Help.ResumeLayout(false);
            this.Help.PerformLayout();

        }

        #endregion

        internal Microsoft.Office.Tools.Ribbon.RibbonTab tab12;
        internal Microsoft.Office.Tools.Ribbon.RibbonGroup Search;
        internal Microsoft.Office.Tools.Ribbon.RibbonButton searchBtn;
        internal Microsoft.Office.Tools.Ribbon.RibbonGroup CC;
        internal Microsoft.Office.Tools.Ribbon.RibbonButton cc_btn;
        internal Microsoft.Office.Tools.Ribbon.RibbonGroup About;
        internal Microsoft.Office.Tools.Ribbon.RibbonButton about_btn;
        internal Microsoft.Office.Tools.Ribbon.RibbonGroup Contact;
        internal Microsoft.Office.Tools.Ribbon.RibbonButton contact_btn;
        internal Microsoft.Office.Tools.Ribbon.RibbonGroup Help;
        internal Microsoft.Office.Tools.Ribbon.RibbonButton help_btn;

    }

    partial class ThisRibbonCollection
    {
        internal NOPSAribbon NOPSAribbon
        {
            get { return this.GetRibbon<NOPSAribbon>(); }
        }
    }
}
