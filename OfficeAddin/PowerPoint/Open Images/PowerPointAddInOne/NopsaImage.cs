using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Diagnostics;
using System.Reflection;
using System.Resources;

namespace PowerPointAddInOne
{
    
    public partial class NopsaImage : UserControl
    {

        public NOPSAImageHolder imageholder;
        public CommunicationManager cm;
        bool isimageloaded = false;
        

        public NopsaImage(NOPSAImageHolder imgholder, CommunicationManager cm)
        {

            InitializeComponent();


            imageholder = imgholder;

            line.X1 = 0;
            line.X2 = Width;
            line.Y1 = Height - 1;
            line.Y2 = Height - 1;

            this.cm = cm;
            
        }


        String newtagtb_defaulttext = " + add a tag";

        //simple binary sort
        void sorttags()
        {
            for (int i = 0; i < this.imageholder.tags.Count; i++)
            {
                for (int j = i+1; j < this.imageholder.tags.Count; j++)
                {
                    if (Int32.Parse(this.imageholder.tags[i].tag_relevence) < Int32.Parse(this.imageholder.tags[j].tag_relevence))
                    {
                        NOPSAImageTagHolder temp = this.imageholder.tags[i];
                        this.imageholder.tags[i] = this.imageholder.tags[j];
                        this.imageholder.tags[j] = temp;
                    }
                }
            }
        }

        void loadtags()
        {
            sorttags();

            int top_padding_factor = 0;
            for (int tagIndex = 0; tagIndex < this.imageholder.tags.Count; tagIndex++)
            {

                Label each_tag_label = new Label();
                each_tag_label.Name = "tag_label_" + this.imageholder.tags[tagIndex].tag_id;
                each_tag_label.Text = this.imageholder.tags[tagIndex].tag_string + " (" + this.imageholder.tags[tagIndex].tag_relevence + ")";
                //each_tag_label.Top = tag_label.Bottom + 6 + top_padding_factor;
                each_tag_label.Top = 6 + top_padding_factor;
                //each_tag_label.Left = tag_label.Left + 20;
                each_tag_label.Left = 20;
                //each_tag_label.Width = 100;
                each_tag_label.AutoSize = true;
                each_tag_label.MaximumSize = new System.Drawing.Size(100, 0); //max width is 100, height can go as much required
                //set maximum height here
                //this.Controls.Add(each_tag_label);
                tagscrollpanel.Controls.Add(each_tag_label);


                Button each_tag_min_btn = new Button();
                each_tag_min_btn.Image = (Image)Properties.Resources.ResourceManager.GetObject("down_red");

                each_tag_min_btn.Name = "tag_min_btn_" + this.imageholder.tags[tagIndex].tag_id;
                each_tag_min_btn.Top = each_tag_label.Top;
                //each_tag_min_btn.Left = each_tag_label.Left + each_tag_label.Width + 10;
                each_tag_min_btn.Left = each_tag_label.Left + 100;
                each_tag_min_btn.Height = 15;
                each_tag_min_btn.Width = 15;
                each_tag_min_btn.FlatStyle = FlatStyle.Flat;
                each_tag_min_btn.FlatAppearance.BorderSize = 0;
                each_tag_min_btn.Click += new EventHandler(each_tag_min_btn_Click);
                //this.Controls.Add(each_tag_min_btn);
                tagscrollpanel.Controls.Add(each_tag_min_btn);


                Button each_tag_max_btn = new Button();
                each_tag_max_btn.Image = (Image)Properties.Resources.ResourceManager.GetObject("up_green");

                each_tag_max_btn.Name = "tag_max_btn_" + this.imageholder.tags[tagIndex].tag_id;
                each_tag_max_btn.Top = each_tag_min_btn.Top;
                each_tag_max_btn.Left = each_tag_min_btn.Left + each_tag_min_btn.Width + 4;
                each_tag_max_btn.Height = 15;
                each_tag_max_btn.Width = 15;
                each_tag_max_btn.FlatStyle = FlatStyle.Flat;
                each_tag_max_btn.FlatAppearance.BorderSize = 0;
                each_tag_max_btn.Click += new EventHandler(each_tag_max_btn_Click);
                //this.Controls.Add(each_tag_max_btn);
                tagscrollpanel.Controls.Add(each_tag_max_btn);

                //top_padding_factor = top_padding_factor + each_tag_label.Height + 6;
                top_padding_factor = top_padding_factor + 15;

            }

            TextBox newtag_box = new TextBox();
            newtag_box.Name = "newtag_tb";
            newtag_box.Top = top_padding_factor+6;
            newtag_box.Left = 23;
            newtag_box.Width = 95;
            newtag_box.UseWaitCursor = false;
            newtag_box.Height = 30;
            newtag_box.Font = new System.Drawing.Font("MS Reference Sans Serif", 7.0F, System.Drawing.FontStyle.Italic, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            newtag_box.BackColor = this.BackColor;
            newtag_box.BorderStyle = System.Windows.Forms.BorderStyle.None;
            newtag_box.Text = newtagtb_defaulttext;

            newtag_box.Click += new EventHandler(newtag_box_Click);
            newtag_box.LostFocus += new EventHandler(newtag_box_LostFocus);
            newtag_box.KeyDown += new KeyEventHandler(newtag_box_KeyDown);

            tagscrollpanel.Controls.Add(newtag_box);

        }

        void newtag_box_KeyDown(object sender, KeyEventArgs e)
        {
            String tb_text = ((TextBox)sender).Text;
            if (e.KeyCode == Keys.Return)
            {
                e.SuppressKeyPress = true; //to stop beep
                /////

                if (tb_text.Trim() == "")
                {
                    ((TextBox)sender).Text = newtagtb_defaulttext;
                    image.Focus();
                    return;
                }

                // calling data layer to add this tag

                //now checking that we don't have this tag already

                for (int i = 0; i < imageholder.tags.Count; i++)
                {
                    if (imageholder.tags[i].tag_string == tb_text) //this tag is already there, no need to add
                    {
                        MessageBox.Show("This tag is already added. Please change relevence");
                        //it would be nice if we ask user that whether instead he would like to increment relevence of tag
                        return;
                    }
                }


                //Debug.WriteLine("add a new tag");
                NOPSAImageTagHolder newtag = new NOPSAImageTagHolder();
                //NOTICE THE LINE BELOW
                // here we are setting tag's id to tag itself because server can automatically convert from tag-> tag_id and right now we don't know what id will server assign this.
                newtag.tag_id = tb_text; //
                newtag.tag_relevence = "1"; //this is default set by server
                newtag.tag_string = tb_text; //
                //we don't and can't know tag_id ... so there must a method on the server where we can incr/ dec relevence based of tag string rather than id
                imageholder.tags.Add(newtag);

                trash_tag_scrollbar();
                //loadimage();
                loadtags();

                //despatch new tag to server (using communication manager)
                this.cm.addtag(imageholder.img_id, tb_text);

            }
        }

        void newtag_box_LostFocus(object sender, EventArgs e)
        {
            if (((TextBox)sender).Text.Trim() == "")
            {
                ((TextBox)sender).Text = newtagtb_defaulttext;
            }
        }

        void newtag_box_Click(object sender, EventArgs e)
        {
            if (((TextBox)sender).Text == newtagtb_defaulttext)
            {
                ((TextBox)sender).Font = new System.Drawing.Font("MS Reference Sans Serif", 7.0F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
                ((TextBox)sender).Text = "";
            }
        }

        void loadimage() //loads image
        {
            
            //this.image.ImageLocation = "http://hiit.fi/sites/all/themes/hiit/images/banners/citywall.jpg";
            //this.image.ImageLocation = "http://hh.se/images/18.70cf2e49129168da0158000108402/bladrar-ohuset.jpg";

            this.image.WaitOnLoad = false;
            this.image.LoadAsync(imageholder.img_url);

            //this.image.ImageLocation = imageholder.img_url;
            this.source_label.Text = "Source: " + imageholder.img_source;
            this.rights_label.Text = "Rights: " + imageholder.img_rights;
            Debug.WriteLine("url: " + imageholder.img_url);
            Debug.WriteLine("source: " + imageholder.img_source);

            loadtags();
        
        }


        void trash_tag_scrollbar()
        {
            tagscrollpanel.Controls.Clear();
        }

        private void NopsaImage_Load(object sender, EventArgs e)
        {
            tagscrollpanel.AutoScroll = true;
            tagscrollpanel.Location = new System.Drawing.Point(tag_label.Left, tag_label.Bottom);
            tagscrollpanel.Size = new System.Drawing.Size(Width-tag_label.Left , Height - tag_label.Bottom - 4);

            loadimage();
        }



        Size imagesize_temp; //used to keep track of previous image_Size incase of hover over and out. Dont use anywhere else
        void image_MouseHover(object sender, System.EventArgs e)
        {
            //Debug.WriteLine("mht: "+ SystemInformation.MouseHoverTime);
            if (!isimageloaded) return;
            imagesize_temp = this.image.Size;

            if (
                    (Width / this.image.Width) > (Height / this.image.Height)
                )
            {
                //Debug.WriteLine("formula on width");
                this.image.Width = this.image.Width +  (this.image.Width *(Height - this.image.Height) / this.image.Height ) - (2*this.image.Left);
                this.image.Height = Height - (2*this.image.Top);
            }
            else 
            {
                //Debug.WriteLine("formula on height");
                this.image.Height = this.image.Height + (this.image.Height * (Width - this.image.Width) / this.image.Width) - (2*this.image.Top);
                this.image.Width = Width - (2*this.image.Left);
            }

        }


        void image_MouseLeave(object sender, System.EventArgs e)
        {
            if (!isimageloaded) return;
            this.image.Size = imagesize_temp;
        }


        void each_tag_max_btn_Click(object sender, EventArgs e) // increments relevence of a tag
        {
            //the funtion below is has same logic .. (there is some code repetition but time is enemy)
            if (((Button)sender).Name.Contains("tag_max_btn_")) // so its confirm who triggered this funtion
            {
                String id_of_tag = ((Button)sender).Name.Replace("tag_max_btn_", "");

                for (int i = 0; i < imageholder.tags.Count; i++)
                {
                    if (imageholder.tags[i].tag_id == id_of_tag) //so this tag's relevence has been changed
                    {
                        int relevence = Int32.Parse(imageholder.tags[i].tag_relevence);
                        relevence = relevence + 1;
                        imageholder.tags[i].tag_relevence = relevence + "";


                        trash_tag_scrollbar();
                        //loadimage();
                        loadtags();

                        //despatching to communication manager
                        this.cm.increment_relevence(imageholder.img_id, id_of_tag);

                        break;
                    }
                }
            }
        }

        void each_tag_min_btn_Click(object sender, EventArgs e) // decrements relevence of a tag
        {
            //Debug.WriteLine("decr: " + ((Button)sender).Name);
            //tag_min_btn_
            if (((Button)sender).Name.Contains("tag_min_btn_")) // so its confirm who triggered this funtion
            {
                String id_of_tag = ((Button)sender).Name.Replace("tag_min_btn_", "");
                
                for (int i = 0; i < imageholder.tags.Count; i++)
                {
                    if (imageholder.tags[i].tag_id == id_of_tag) //so this tag's relevence has been changed
                    {
                        int relevence = Int32.Parse(imageholder.tags[i].tag_relevence);
                        relevence = relevence - 1;
                        imageholder.tags[i].tag_relevence = relevence + "";



                        trash_tag_scrollbar();
                        //loadimage();
                        loadtags();

                        //despatching to communication manager
                        this.cm.decrement_relevence(imageholder.img_id, id_of_tag);

                        break;
                    }
                }
            }
        }


        private void downloadimg_img_Click(object sender, EventArgs e) //downloads the image
        {
            //Debug.WriteLine("download the image");
            ApplicationInteraction.download_image_to_doc( this.imageholder );
        }


        private void image_LoadCompleted(Object sender, AsyncCompletedEventArgs e)
        {
            this.isimageloaded = true;
            imagesize_temp = this.image.Size;

            //check if image's size is small then container, then simple dont do anything
            if (InvokeRequired)
            {
                this.Invoke(new MethodInvoker(delegate
                {
                    if ((this.image.Image.PhysicalDimension.Width < this.image.Width)
                        &&
                        (this.image.Image.PhysicalDimension.Height < this.image.Height))
                    {
                        //Debug.WriteLine("image smaller than container");
                        return;
                    }
                }));
             }
             else
             {
                 if ((this.image.Image.PhysicalDimension.Width < this.image.Width)
                        &&
                        (this.image.Image.PhysicalDimension.Height < this.image.Height))
                    {
                        return;
                    }
             }
            

            //if image's size is more than the container
            float wr=0;
            float hr=0;
            if (InvokeRequired)
            {
                this.Invoke(new MethodInvoker(delegate
                {
                    wr = this.image.Image.PhysicalDimension.Width / this.image.Width;
                    hr = this.image.Image.PhysicalDimension.Height / this.image.Height;
                }));
            }
            else
            {
                wr = this.image.Image.PhysicalDimension.Width / this.image.Width;
                hr = this.image.Image.PhysicalDimension.Height / this.image.Height;
            }

            if (hr < wr)
            {
                double s = 1.0 / wr;
                if (InvokeRequired)
                {
                    // after we've done all the processing, 
                    this.Invoke(new MethodInvoker(delegate
                    {
                        // load the control with the appropriate data
                        this.image.Height = (int)((1-s) * this.image.Height);
                    }));
                }
                else
                {
                    this.image.Height = (int)((1-s) * this.image.Height);
                }

            }
            else
            {
                double s = 1.0 / hr;
                if (InvokeRequired)
                {
                    // after we've done all the processing, 
                    this.Invoke(new MethodInvoker(delegate
                    {
                        // load the control with the appropriate data
                        this.image.Width = (int)((1 - s) * this.image.Width);
                    }));
                }
                else
                {
                    this.image.Width = (int)((1-s) * this.image.Width);
                }

            }

            imagesize_temp = this.image.Size;

        }

        void image_DoubleClick(object sender, System.EventArgs e)
        {
            downloadimg_img_Click(this, new EventArgs());
        }

        private void rights_label_Click(object sender, EventArgs e)
        {

        }

        private void tag_label_Click(object sender, EventArgs e)
        {

        }

        private void tagscrollpanel_Paint(object sender, PaintEventArgs e)
        {

        }

        private void add_a_tag_Click(object sender, EventArgs e)
        {

        }

        private void image_Click(object sender, EventArgs e)
        {

        }

        private void download_label_Click(object sender, EventArgs e)
        {
            downloadimg_img_Click(this, new EventArgs());
        }


    }
}

