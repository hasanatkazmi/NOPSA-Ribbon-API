namespace PowerPointAddInOne
{
    partial class SettingsForm
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
            this.options_list = new System.Windows.Forms.ListBox();
            this.categorieslabel = new System.Windows.Forms.Label();
            this.imagesize_group1 = new System.Windows.Forms.GroupBox();
            this.imagesize_radio_largeimage = new System.Windows.Forms.RadioButton();
            this.imagesize_radio_mediumimage = new System.Windows.Forms.RadioButton();
            this.imagesize_radio_smallimage = new System.Windows.Forms.RadioButton();
            this.ok_btn = new System.Windows.Forms.Button();
            this.cancel_button = new System.Windows.Forms.Button();
            this.displaylicense_groupBox1 = new System.Windows.Forms.GroupBox();
            this.displaylicense_checkBox1 = new System.Windows.Forms.CheckBox();
            this.imagesize_group1.SuspendLayout();
            this.displaylicense_groupBox1.SuspendLayout();
            this.SuspendLayout();
            // 
            // options_list
            // 
            this.options_list.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.options_list.FormattingEnabled = true;
            this.options_list.ItemHeight = 15;
            this.options_list.Items.AddRange(new object[] {
            "Display License",
            "Image Size"});
            this.options_list.Location = new System.Drawing.Point(12, 42);
            this.options_list.Name = "options_list";
            this.options_list.ScrollAlwaysVisible = true;
            this.options_list.Size = new System.Drawing.Size(120, 304);
            this.options_list.Sorted = true;
            this.options_list.TabIndex = 0;
            this.options_list.SelectedIndexChanged += new System.EventHandler(this.options_list_SelectedIndexChanged);
            // 
            // categorieslabel
            // 
            this.categorieslabel.AutoSize = true;
            this.categorieslabel.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.categorieslabel.Location = new System.Drawing.Point(13, 15);
            this.categorieslabel.Name = "categorieslabel";
            this.categorieslabel.Size = new System.Drawing.Size(63, 15);
            this.categorieslabel.TabIndex = 1;
            this.categorieslabel.Text = "Categories";
            // 
            // imagesize_group1
            // 
            this.imagesize_group1.Controls.Add(this.imagesize_radio_largeimage);
            this.imagesize_group1.Controls.Add(this.imagesize_radio_mediumimage);
            this.imagesize_group1.Controls.Add(this.imagesize_radio_smallimage);
            this.imagesize_group1.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.imagesize_group1.Location = new System.Drawing.Point(147, 42);
            this.imagesize_group1.Name = "imagesize_group1";
            this.imagesize_group1.Size = new System.Drawing.Size(315, 70);
            this.imagesize_group1.TabIndex = 2;
            this.imagesize_group1.TabStop = false;
            this.imagesize_group1.Text = "Default Image Downloaded Size";
            // 
            // imagesize_radio_largeimage
            // 
            this.imagesize_radio_largeimage.AutoSize = true;
            this.imagesize_radio_largeimage.Location = new System.Drawing.Point(239, 29);
            this.imagesize_radio_largeimage.Name = "imagesize_radio_largeimage";
            this.imagesize_radio_largeimage.Size = new System.Drawing.Size(54, 19);
            this.imagesize_radio_largeimage.TabIndex = 2;
            this.imagesize_radio_largeimage.Text = "Large";
            this.imagesize_radio_largeimage.UseVisualStyleBackColor = true;
            // 
            // imagesize_radio_mediumimage
            // 
            this.imagesize_radio_mediumimage.AutoSize = true;
            this.imagesize_radio_mediumimage.Checked = true;
            this.imagesize_radio_mediumimage.Location = new System.Drawing.Point(121, 29);
            this.imagesize_radio_mediumimage.Name = "imagesize_radio_mediumimage";
            this.imagesize_radio_mediumimage.Size = new System.Drawing.Size(70, 19);
            this.imagesize_radio_mediumimage.TabIndex = 1;
            this.imagesize_radio_mediumimage.TabStop = true;
            this.imagesize_radio_mediumimage.Text = "Medium";
            this.imagesize_radio_mediumimage.UseVisualStyleBackColor = true;
            // 
            // imagesize_radio_smallimage
            // 
            this.imagesize_radio_smallimage.AutoSize = true;
            this.imagesize_radio_smallimage.Location = new System.Drawing.Point(16, 29);
            this.imagesize_radio_smallimage.Name = "imagesize_radio_smallimage";
            this.imagesize_radio_smallimage.Size = new System.Drawing.Size(54, 19);
            this.imagesize_radio_smallimage.TabIndex = 0;
            this.imagesize_radio_smallimage.Text = "Small";
            this.imagesize_radio_smallimage.UseVisualStyleBackColor = true;
            // 
            // ok_btn
            // 
            this.ok_btn.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.ok_btn.Location = new System.Drawing.Point(297, 341);
            this.ok_btn.Name = "ok_btn";
            this.ok_btn.Size = new System.Drawing.Size(75, 23);
            this.ok_btn.TabIndex = 3;
            this.ok_btn.Text = "OK";
            this.ok_btn.UseVisualStyleBackColor = true;
            this.ok_btn.Click += new System.EventHandler(this.ok_btn_Click);
            // 
            // cancel_button
            // 
            this.cancel_button.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.cancel_button.Location = new System.Drawing.Point(387, 341);
            this.cancel_button.Name = "cancel_button";
            this.cancel_button.Size = new System.Drawing.Size(75, 23);
            this.cancel_button.TabIndex = 4;
            this.cancel_button.Text = "Cancel";
            this.cancel_button.UseVisualStyleBackColor = true;
            this.cancel_button.Click += new System.EventHandler(this.cancel_button_Click);
            // 
            // displaylicense_groupBox1
            // 
            this.displaylicense_groupBox1.Controls.Add(this.displaylicense_checkBox1);
            this.displaylicense_groupBox1.Location = new System.Drawing.Point(147, 148);
            this.displaylicense_groupBox1.Name = "displaylicense_groupBox1";
            this.displaylicense_groupBox1.Size = new System.Drawing.Size(314, 71);
            this.displaylicense_groupBox1.TabIndex = 3;
            this.displaylicense_groupBox1.TabStop = false;
            this.displaylicense_groupBox1.Text = "License and Credits";
            // 
            // displaylicense_checkBox1
            // 
            this.displaylicense_checkBox1.AutoSize = true;
            this.displaylicense_checkBox1.Location = new System.Drawing.Point(13, 31);
            this.displaylicense_checkBox1.Name = "displaylicense_checkBox1";
            this.displaylicense_checkBox1.Size = new System.Drawing.Size(236, 17);
            this.displaylicense_checkBox1.TabIndex = 0;
            this.displaylicense_checkBox1.Text = "Also display License on page where image is";
            this.displaylicense_checkBox1.UseVisualStyleBackColor = true;
            // 
            // SettingsForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(474, 376);
            this.Controls.Add(this.displaylicense_groupBox1);
            this.Controls.Add(this.cancel_button);
            this.Controls.Add(this.ok_btn);
            this.Controls.Add(this.imagesize_group1);
            this.Controls.Add(this.categorieslabel);
            this.Controls.Add(this.options_list);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "SettingsForm";
            this.ShowInTaskbar = false;
            this.Text = "Preferences";
            this.imagesize_group1.ResumeLayout(false);
            this.imagesize_group1.PerformLayout();
            this.displaylicense_groupBox1.ResumeLayout(false);
            this.displaylicense_groupBox1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.ListBox options_list;
        private System.Windows.Forms.Label categorieslabel;
        private System.Windows.Forms.GroupBox imagesize_group1;
        private System.Windows.Forms.RadioButton imagesize_radio_largeimage;
        private System.Windows.Forms.RadioButton imagesize_radio_mediumimage;
        private System.Windows.Forms.RadioButton imagesize_radio_smallimage;
        private System.Windows.Forms.Button ok_btn;
        private System.Windows.Forms.Button cancel_button;
        private System.Windows.Forms.GroupBox displaylicense_groupBox1;
        private System.Windows.Forms.CheckBox displaylicense_checkBox1;
    }
}