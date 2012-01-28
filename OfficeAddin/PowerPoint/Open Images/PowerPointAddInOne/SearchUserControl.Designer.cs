using System.Diagnostics;

namespace PowerPointAddInOne
{
    partial class SearchUserControl
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

        #region Component Designer generated code

        /// <summary> 
        /// Required method for Designer support - do not modify 
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(SearchUserControl));
            this.searchTB = new System.Windows.Forms.TextBox();
            this.scrollpanel = new System.Windows.Forms.Panel();
            this.settings_btn = new System.Windows.Forms.Button();
            this.back_page_btn = new System.Windows.Forms.Button();
            this.loading_search = new System.Windows.Forms.PictureBox();
            this.next_page_btn = new System.Windows.Forms.Button();
            this.searchBtn = new System.Windows.Forms.Button();
            this.settings_listbox = new System.Windows.Forms.CheckedListBox();
            ((System.ComponentModel.ISupportInitialize)(this.loading_search)).BeginInit();
            this.SuspendLayout();
            // 
            // searchTB
            // 
            this.searchTB.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.Suggest;
            this.searchTB.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.CustomSource;
            this.searchTB.Font = new System.Drawing.Font("Microsoft Sans Serif", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.searchTB.Location = new System.Drawing.Point(13, 20);
            this.searchTB.Name = "searchTB";
            this.searchTB.Size = new System.Drawing.Size(146, 24);
            this.searchTB.TabIndex = 1;
            this.searchTB.TextChanged += new System.EventHandler(this.searchTB_TextChanged);
            this.searchTB.KeyDown += new System.Windows.Forms.KeyEventHandler(this.searchTB_KeyDown);
            // 
            // scrollpanel
            // 
            this.scrollpanel.Location = new System.Drawing.Point(21, 73);
            this.scrollpanel.Name = "scrollpanel";
            this.scrollpanel.Size = new System.Drawing.Size(200, 100);
            this.scrollpanel.TabIndex = 3;
            // 
            // settings_btn
            // 
            this.settings_btn.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("settings_btn.BackgroundImage")));
            this.settings_btn.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Zoom;
            this.settings_btn.FlatAppearance.BorderSize = 0;
            this.settings_btn.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.settings_btn.Font = new System.Drawing.Font("Microsoft Sans Serif", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.settings_btn.Location = new System.Drawing.Point(188, 21);
            this.settings_btn.Name = "settings_btn";
            this.settings_btn.Size = new System.Drawing.Size(33, 23);
            this.settings_btn.TabIndex = 7;
            this.settings_btn.UseVisualStyleBackColor = true;
            this.settings_btn.Click += new System.EventHandler(this.settings_btn_Click);
            // 
            // back_page_btn
            // 
            this.back_page_btn.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.back_page_btn.Image = ((System.Drawing.Image)(resources.GetObject("back_page_btn.Image")));
            this.back_page_btn.Location = new System.Drawing.Point(0, 344);
            this.back_page_btn.Margin = new System.Windows.Forms.Padding(3, 3, 0, 0);
            this.back_page_btn.Name = "back_page_btn";
            this.back_page_btn.Size = new System.Drawing.Size(32, 32);
            this.back_page_btn.TabIndex = 6;
            this.back_page_btn.UseVisualStyleBackColor = true;
            this.back_page_btn.Visible = false;
            this.back_page_btn.Click += new System.EventHandler(this.back_page_btn_Click);
            // 
            // loading_search
            // 
            this.loading_search.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.loading_search.Image = ((System.Drawing.Image)(resources.GetObject("loading_search.Image")));
            this.loading_search.Location = new System.Drawing.Point(98, 207);
            this.loading_search.Name = "loading_search";
            this.loading_search.Size = new System.Drawing.Size(42, 44);
            this.loading_search.SizeMode = System.Windows.Forms.PictureBoxSizeMode.CenterImage;
            this.loading_search.TabIndex = 5;
            this.loading_search.TabStop = false;
            // 
            // next_page_btn
            // 
            this.next_page_btn.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.next_page_btn.Image = ((System.Drawing.Image)(resources.GetObject("next_page_btn.Image")));
            this.next_page_btn.Location = new System.Drawing.Point(262, 344);
            this.next_page_btn.Margin = new System.Windows.Forms.Padding(3, 3, 0, 0);
            this.next_page_btn.Name = "next_page_btn";
            this.next_page_btn.Size = new System.Drawing.Size(32, 32);
            this.next_page_btn.TabIndex = 4;
            this.next_page_btn.UseVisualStyleBackColor = true;
            this.next_page_btn.Visible = false;
            this.next_page_btn.Click += new System.EventHandler(this.next_page_btn_Click);
            // 
            // searchBtn
            // 
            this.searchBtn.FlatAppearance.BorderSize = 0;
            this.searchBtn.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.searchBtn.Font = new System.Drawing.Font("Microsoft Sans Serif", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.searchBtn.Image = ((System.Drawing.Image)(resources.GetObject("searchBtn.Image")));
            this.searchBtn.Location = new System.Drawing.Point(217, 20);
            this.searchBtn.Name = "searchBtn";
            this.searchBtn.Size = new System.Drawing.Size(33, 23);
            this.searchBtn.TabIndex = 2;
            this.searchBtn.UseVisualStyleBackColor = true;
            this.searchBtn.Click += new System.EventHandler(this.button1_Click);
            // 
            // settings_listbox
            // 
            this.settings_listbox.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.settings_listbox.CheckOnClick = true;
            this.settings_listbox.FormattingEnabled = true;
            this.settings_listbox.Items.AddRange(new object[] {
            "All",
            "NOPSA",
            "Google Images",
            "Library of Congress",
            "Wikimedia"});
            this.settings_listbox.Location = new System.Drawing.Point(161, 50);
            this.settings_listbox.Name = "settings_listbox";
            this.settings_listbox.Size = new System.Drawing.Size(112, 90);
            this.settings_listbox.TabIndex = 8;
            this.settings_listbox.Visible = false;
            this.settings_listbox.MouseLeave += new System.EventHandler(settings_listbox_MouseLeave);
            this.settings_listbox.SelectedValueChanged += new System.EventHandler(settings_listbox_SelectedValueChanged);
            // 
            // SearchUserControl
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.settings_listbox);
            this.Controls.Add(this.settings_btn);
            this.Controls.Add(this.back_page_btn);
            this.Controls.Add(this.loading_search);
            this.Controls.Add(this.next_page_btn);
            this.Controls.Add(this.scrollpanel);
            this.Controls.Add(this.searchBtn);
            this.Controls.Add(this.searchTB);
            this.Name = "SearchUserControl";
            this.Size = new System.Drawing.Size(294, 376);
            this.Load += new System.EventHandler(this.SearchUserControl_Load);
            ((System.ComponentModel.ISupportInitialize)(this.loading_search)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        void settings_listbox_SelectedValueChanged(object sender, System.EventArgs e)
        {
            if (settings_listbox.CheckedItems.Contains("All") && settings_listbox.SelectedItems.Contains("All"))
            {
                for (int i = 0; i < settings_listbox.Items.Count; i++)
                {
                    settings_listbox.SetItemCheckState(i, System.Windows.Forms.CheckState.Checked);
                }
            }
            else if (settings_listbox.CheckedItems.Count+1 == settings_listbox.Items.Count && (!settings_listbox.CheckedItems.Contains("All")))
            {
                //assuming that All would always be at position 0
                settings_listbox.SetItemCheckState( 0 , System.Windows.Forms.CheckState.Checked);
            }
            else if (settings_listbox.CheckedItems.Count < settings_listbox.Items.Count)
            {
                //assuming that All would always be at position 0
                settings_listbox.SetItemCheckState(0, System.Windows.Forms.CheckState.Unchecked);
            }
        }

        void settings_listbox_MouseLeave(object sender, System.EventArgs e)
        {
            System.Threading.Thread.Sleep(500);
            settings_listbox.Hide();
        }



        #endregion

        private System.Windows.Forms.TextBox searchTB;
        private System.Windows.Forms.Button searchBtn;
        private System.Windows.Forms.Panel scrollpanel;
        private System.Windows.Forms.Button next_page_btn;
        private System.Windows.Forms.PictureBox loading_search;
        private System.Windows.Forms.Button back_page_btn;
        private System.Windows.Forms.Button settings_btn;
        private System.Windows.Forms.CheckedListBox settings_listbox;
    }
}
