namespace PowerPointAddInOne
{
    partial class CCLicense
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

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

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.webBrowser = new System.Windows.Forms.WebBrowser();
            this.loading_pb = new System.Windows.Forms.PictureBox();
            ((System.ComponentModel.ISupportInitialize)(this.loading_pb)).BeginInit();
            this.SuspendLayout();
            // 
            // webBrowser
            // 
            this.webBrowser.Dock = System.Windows.Forms.DockStyle.Fill;
            this.webBrowser.Location = new System.Drawing.Point(0, 0);
            this.webBrowser.MinimumSize = new System.Drawing.Size(20, 20);
            this.webBrowser.Name = "webBrowser";
            this.webBrowser.Size = new System.Drawing.Size(504, 388);
            this.webBrowser.TabIndex = 0;
            this.webBrowser.DocumentCompleted += new System.Windows.Forms.WebBrowserDocumentCompletedEventHandler(this.webBrowser_DocumentCompleted);
            this.webBrowser.Navigating += new System.Windows.Forms.WebBrowserNavigatingEventHandler(this.webBrowser_Navigating);
            // 
            // loading_pb
            // 
            this.loading_pb.BackColor = System.Drawing.Color.Transparent;
            this.loading_pb.Dock = System.Windows.Forms.DockStyle.Fill;
            this.loading_pb.Image = global::PowerPointAddInOne.Properties.Resources.ajax_loader;
            this.loading_pb.Location = new System.Drawing.Point(0, 0);
            this.loading_pb.Name = "loading_pb";
            this.loading_pb.Size = new System.Drawing.Size(504, 388);
            this.loading_pb.SizeMode = System.Windows.Forms.PictureBoxSizeMode.CenterImage;
            this.loading_pb.TabIndex = 1;
            this.loading_pb.TabStop = false;
            this.loading_pb.Visible = false;
            // 
            // CCLicense
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(504, 388);
            this.Controls.Add(this.loading_pb);
            this.Controls.Add(this.webBrowser);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "CCLicense";
            this.ShowIcon = false;
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "CCLicense";
            this.TopMost = true;
            ((System.ComponentModel.ISupportInitialize)(this.loading_pb)).EndInit();
            this.ResumeLayout(false);

        }


        #endregion

        private System.Windows.Forms.WebBrowser webBrowser;
        private System.Windows.Forms.PictureBox loading_pb;
    }
}