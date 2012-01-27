namespace PowerPointAddInOne
{
    partial class NopsaImage
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(NopsaImage));
            this.source_label = new System.Windows.Forms.Label();
            this.rights_label = new System.Windows.Forms.Label();
            this.tag_label = new System.Windows.Forms.Label();
            this.tagscrollpanel = new System.Windows.Forms.Panel();
            this.download_label = new System.Windows.Forms.Label();
            this.shapeContainer1 = new Microsoft.VisualBasic.PowerPacks.ShapeContainer();
            this.line = new Microsoft.VisualBasic.PowerPacks.LineShape();
            this.image = new System.Windows.Forms.PictureBox();
            this.downloadimg_img = new System.Windows.Forms.PictureBox();
            ((System.ComponentModel.ISupportInitialize)(this.image)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.downloadimg_img)).BeginInit();
            this.SuspendLayout();
            // 
            // source_label
            // 
            this.source_label.AutoSize = true;
            this.source_label.Font = new System.Drawing.Font("MS Reference Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.source_label.Location = new System.Drawing.Point(136, 9);
            this.source_label.Name = "source_label";
            this.source_label.Size = new System.Drawing.Size(67, 15);
            this.source_label.TabIndex = 0;
            this.source_label.Text = "hoon main";
            // 
            // rights_label
            // 
            this.rights_label.AutoSize = true;
            this.rights_label.Font = new System.Drawing.Font("MS Reference Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.rights_label.Location = new System.Drawing.Point(135, 24);
            this.rights_label.Name = "rights_label";
            this.rights_label.Size = new System.Drawing.Size(39, 15);
            this.rights_label.TabIndex = 2;
            this.rights_label.Text = "rights";
            this.rights_label.Click += new System.EventHandler(this.rights_label_Click);
            // 
            // tag_label
            // 
            this.tag_label.AutoSize = true;
            this.tag_label.Font = new System.Drawing.Font("MS Reference Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.tag_label.Location = new System.Drawing.Point(137, 39);
            this.tag_label.Name = "tag_label";
            this.tag_label.Size = new System.Drawing.Size(39, 15);
            this.tag_label.TabIndex = 3;
            this.tag_label.Text = "Tags:";
            this.tag_label.Click += new System.EventHandler(this.tag_label_Click);
            // 
            // tagscrollpanel
            // 
            this.tagscrollpanel.Font = new System.Drawing.Font("MS Reference Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.tagscrollpanel.Location = new System.Drawing.Point(170, 50);
            this.tagscrollpanel.Name = "tagscrollpanel";
            this.tagscrollpanel.Size = new System.Drawing.Size(138, 137);
            this.tagscrollpanel.TabIndex = 6;
            this.tagscrollpanel.Paint += new System.Windows.Forms.PaintEventHandler(this.tagscrollpanel_Paint);
            // 
            // download_label
            // 
            this.download_label.AutoSize = true;
            this.download_label.Font = new System.Drawing.Font("MS Reference Sans Serif", 6.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.download_label.Location = new System.Drawing.Point(30, 147);
            this.download_label.Name = "download_label";
            this.download_label.Size = new System.Drawing.Size(88, 12);
            this.download_label.TabIndex = 8;
            this.download_label.Text = "Download Image";
            this.download_label.Click += new System.EventHandler(this.download_label_Click);
            // 
            // shapeContainer1
            // 
            this.shapeContainer1.Location = new System.Drawing.Point(0, 0);
            this.shapeContainer1.Margin = new System.Windows.Forms.Padding(0);
            this.shapeContainer1.Name = "shapeContainer1";
            this.shapeContainer1.Shapes.AddRange(new Microsoft.VisualBasic.PowerPacks.Shape[] {
            this.line});
            this.shapeContainer1.Size = new System.Drawing.Size(308, 188);
            this.shapeContainer1.TabIndex = 9;
            this.shapeContainer1.TabStop = false;
            // 
            // line
            // 
            this.line.BorderColor = System.Drawing.Color.LightGray;
            this.line.Name = "line";
            this.line.X1 = 0;
            this.line.X2 = 50;
            this.line.Y1 = 179;
            this.line.Y2 = 186;
            // 
            // image
            // 
            this.image.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.image.ErrorImage = ((System.Drawing.Image)(resources.GetObject("image.ErrorImage")));
            this.image.InitialImage = ((System.Drawing.Image)(resources.GetObject("image.InitialImage")));
            this.image.Location = new System.Drawing.Point(5, 9);
            this.image.Name = "image";
            this.image.Size = new System.Drawing.Size(124, 101);
            this.image.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.image.TabIndex = 1;
            this.image.TabStop = false;
            this.image.LoadCompleted += new System.ComponentModel.AsyncCompletedEventHandler(this.image_LoadCompleted);
            this.image.Click += new System.EventHandler(this.image_Click);
            this.image.DoubleClick += new System.EventHandler(this.image_DoubleClick);
            this.image.MouseLeave += new System.EventHandler(this.image_MouseLeave);
            this.image.MouseHover += new System.EventHandler(this.image_MouseHover);
            // 
            // downloadimg_img
            // 
            this.downloadimg_img.Image = global::PowerPointAddInOne.Properties.Resources.download;
            this.downloadimg_img.Location = new System.Drawing.Point(7, 145);
            this.downloadimg_img.Name = "downloadimg_img";
            this.downloadimg_img.Size = new System.Drawing.Size(17, 24);
            this.downloadimg_img.TabIndex = 5;
            this.downloadimg_img.TabStop = false;
            this.downloadimg_img.Click += new System.EventHandler(this.downloadimg_img_Click);
            // 
            // NopsaImage
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.SystemColors.Control;
            this.Controls.Add(this.shapeContainer1);
            this.Controls.Add(this.image);
            this.Controls.Add(this.source_label);
            this.Controls.Add(this.rights_label);
            this.Controls.Add(this.tag_label);
            this.Controls.Add(this.tagscrollpanel);
            this.Controls.Add(this.downloadimg_img);
            this.Controls.Add(this.download_label);
            this.Name = "NopsaImage";
            this.Size = new System.Drawing.Size(308, 188);
            this.Load += new System.EventHandler(this.NopsaImage_Load);
            ((System.ComponentModel.ISupportInitialize)(this.image)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.downloadimg_img)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label source_label;
        private System.Windows.Forms.PictureBox image;
        private System.Windows.Forms.Label rights_label;
        private System.Windows.Forms.Label tag_label;
        //private System.Windows.Forms.PictureBox addtag_img;
        private System.Windows.Forms.PictureBox downloadimg_img;
        private System.Windows.Forms.Panel tagscrollpanel;
        //private System.Windows.Forms.Label add_a_tag;
        private System.Windows.Forms.Label download_label;
        private Microsoft.VisualBasic.PowerPacks.ShapeContainer shapeContainer1;
        private Microsoft.VisualBasic.PowerPacks.LineShape line;

    }
}
