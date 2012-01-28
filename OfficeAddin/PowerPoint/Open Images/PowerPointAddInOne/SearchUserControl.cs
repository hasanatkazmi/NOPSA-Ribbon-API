using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Diagnostics;
using System.Threading;
using Microsoft.Office.Core;
using Microsoft.Win32;

namespace PowerPointAddInOne
{

    
    public partial class SearchUserControl : UserControl
    {
        CommunicationManager cm;
        private DateTime searchtb_timestamp = new DateTime(); //used to monitor last textchange event of text box
        public String text_to_search = ""; // or suggest

        public int resultsStartIndex = 0; //nth item from which we start showing search results

        public bool search_data_recieved = false;
            
        public SearchUserControl()
        {
            Thread.CurrentThread.Name = "GUI_THREAD";
            // this shuts up 'other thread accessing UI' type errors
            //System.Windows.Forms.Control.CheckForIllegalCrossThreadCalls = false;

            InitializeComponent();
            //this.searchTB.KeyDown += new System.Windows.Forms.KeyEventHandler(this.searchTB_OnKeyDownHandler);
            this.SizeChanged += new EventHandler(this.SearchUserControl_SizeChanged);
    
            cm = new CommunicationManager(this);

            searchtb_timestamp = DateTime.UtcNow;
                        
        }


        private void SearchUserControl_Load(object sender, EventArgs e)
        {
            // Use the AutoCompleteMode that suits you.
            searchTB.AutoCompleteMode = AutoCompleteMode.Suggest;
            // Since we are using custom suggestions you
            // should use this source.
            // Use the other non-custom sources if you
            // don't want to use custom suggestions.
            searchTB.AutoCompleteSource = AutoCompleteSource.CustomSource;
            // And finally add the above suggestions to the CustomSource

            loading_search.Top = (Height - loading_search.Height) / 2;
            loading_search.Left = (Width - loading_search.Width) / 2;
            loading_search.Hide();



            //ATTEMPTS
            //ThemeColor th;
            //this.BackColor = Color.FromArgb(th.RGB);




            //set colorschemes of ui components according to color scheme..
            //THIS IS NOT WORKING
            const string OfficeCommonKey =  @"Software\Microsoft\Office\12.0\Common";
            const string OfficeThemeValueName = "Theme";
            const int ThemeBlue = 1;
            const int ThemeSilver = 2;
            const int ThemeBlack = 3;

            using (RegistryKey key = Registry.CurrentUser.OpenSubKey(OfficeCommonKey, false))
            {
                int theme = (int)key.GetValue(OfficeThemeValueName, 1);

                switch (theme)
                {
                    case ThemeBlue:
                        //...
                        settings_listbox.BackColor = Color.FromArgb(224, 199, 178);
                        this.BackColor = Color.FromArgb(224, 199, 178);
                        Debug.WriteLine("blue");
                        break;
                    case ThemeSilver:
                        //...
                        Debug.WriteLine("silver");
                        break;
                    case ThemeBlack:
                        //...
                        Debug.WriteLine("black");
                        this.BackColor = Color.FromArgb(161, 161, 161);
                        settings_listbox.BackColor = Color.FromArgb(161, 161, 161);
                        break;
                    default:
                        //...
                        Debug.WriteLine("def");
                        break;
                }
            }
        }

        //search button
        private void button1_Click(object sender, EventArgs e)
        {
            Debug.Write("you entered :" + searchTB.Text );
            //cm.suggest(searchTB.Text);
            if (searchTB.Text.Trim() == "")
            {
                MessageBox.Show("Please enter something");
                return;
            }
            if (settings_listbox.CheckedItems.Count == 0)
            {
                MessageBox.Show("Please choose at least one data source in 'settings'");
                return;
            }
            String[] temp = new String[5];
            settings_listbox.CheckedItems.CopyTo(temp, 0);
            cm.search(searchTB.Text, temp);
            search_data_recieved = false;
            scrollpanel.Controls.Clear();
            loading_search.Show();
            //loadscrollpanel();

        }

        void SearchUserControl_SizeChanged(object sender, EventArgs e)
        {
            //throw new NotImplementedException();
            //Debug.WriteLine("size changed");
            //Debug.WriteLine("width: " + this.Width + "  height: " + this.Height);
            //CALL IT SO THAT IT GETS REFRESHED
            //loadscrollpanel(); or refresh the screeen

        }

        public void loadscrollpanel( bool wasbackclicked)
        {
            scrollpanel.AutoScroll = true;
            //scrollpanel.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            scrollpanel.Location = new System.Drawing.Point(0, searchBtn.Bottom);
            scrollpanel.Size = new System.Drawing.Size(searchBtn.Right, Height - searchBtn.Bottom - next_page_btn.Height);

            //also set the position of next back buttons here
            //next_page_btn.Top = scrollpanel.Bottom;
            //next_page_btn.Left = Width - next_page_btn.Width;
            next_page_btn.Visible = true;
            if (resultsStartIndex != 0) back_page_btn.Visible = true;

            //Debug.WriteLine("search btn top: "+ searchBtn.Top + "  search btn left: "+ searchBtn.Left);
            //int end;
            

            //scrollpanel.AutoScroll = true;

            //scrollpanel.ResumeLayout(false);
            //this.ResumeLayout(false);
            //this.Refresh();
           
            //scrollpanel.Height = ni0.Height * 5;

            //this.Refresh();

            loading_search.Hide();
            scrollpanel.Controls.Clear();

                        
            /*
            NOPSAImageHolder temp = new NOPSAImageHolder();
            temp.img_url = "http://nopsa.hiit.fi/pmg/images/square_3846620713_9b60cdf7c2_t.jpg";
            temp.img_creator = "Hasanat";
            temp.img_holder = "img holder url";
            temp.img_id = "1234567";
            temp.img_rights = "Google";
            temp.img_source = "http://sourceuri";
            temp.tags = new List<NOPSAImageTagHolder>();
            NOPSAImageTagHolder temptag = new NOPSAImageTagHolder();
            temptag.tag_id = "111111";
            temptag.tag_relevence = "1";
            temptag.tag_string = "good tag";
            temp.tags.Add(temptag);
            NOPSAImageTagHolder temptag2 = new NOPSAImageTagHolder();
            temptag2.tag_id = "222222";
            temptag2.tag_relevence = "2";
            temptag2.tag_string = "bad tag";
            temp.tags.Add(temptag2);
            NOPSAImageTagHolder temptag3 = new NOPSAImageTagHolder();
            temptag3.tag_id = "333333";
            temptag3.tag_relevence = "3";
            temptag3.tag_string = "i m tag 3 :P";
            temp.tags.Add(temptag3);
            NOPSAImageTagHolder temptag4 = new NOPSAImageTagHolder();
            temptag4.tag_id = "444444";
            temptag4.tag_relevence = "4";
            temptag4.tag_string = "yo baby";
            //temp.tags.Add(temptag4);
            NOPSAImageTagHolder temptag5 = new NOPSAImageTagHolder();
            temptag5.tag_id = "555555";
            temptag5.tag_relevence = "5";
            temptag5.tag_string = "tag 5 bitch";
            //temp.tags.Add(temptag5);

            MessageBox.Show("creating nopsaimage");
            try
            {
                NopsaImage nicheck = new NopsaImage(temp, cm);
            }
            catch (Exception ex)
            {
                MessageBox.Show("NopsaImage couldn't failed to create with temp as arg " + ex.StackTrace);
            }
            NopsaImage ni0 = new NopsaImage(temp, cm);
            ni0.Location = new Point(0, 0);
            scrollpanel.Controls.Add(ni0);


            NopsaImage ni1 = new NopsaImage(temp, cm);
            ni1.Location = new Point(0, ni0.Height);
            scrollpanel.Controls.Add(ni1);

            NopsaImage ni2 = new NopsaImage(temp, cm);
            ni2.Location = new Point(0, ni1.Height * 2);
            scrollpanel.Controls.Add(ni2);

            NopsaImage ni3 = new NopsaImage(temp, cm);
            ni3.Location = new Point(0, ni2.Height * 3);
            scrollpanel.Controls.Add(ni3);
            */


            /*
            int nloc = 0;
            int counter = 10;
            while (counter!= 0)
            {
                int source_num = resultsStartIndex % Search_Data.sources.Count;
                int img_num = resultsStartIndex / Search_Data.sources.Count;
                resultsStartIndex++;
                //Debug.WriteLine("source: " + source_num);
                //Debug.WriteLine("image: " + img_num);

                int madcounter = 0;
                NOPSAImageHolder temp;
                try
                {
                    temp = Search_Data.images[source_num][img_num];
                    NopsaImage n = new NopsaImage(temp);
                    n.Location = new Point(0, nloc);
                    nloc = nloc + n.Height;
                    scrollpanel.Controls.Add(n);
                    counter--;

                }
                catch(Exception e)
                {
                    madcounter++;
                    if (madcounter == 30) break;
                    continue;
                }

            }
            */


            //////
            //////////////
            //////////////////
            ///////////////////// INTRODUCE SOME SHUFFING ALGO HERE
            ///////////////////// I AM HARDCODING .. THIS IS BAD

            if (!wasbackclicked) // i.e. next page was clicked
            {
                int nloc = 0;
                for (int j = 0; j < Search_Data.sources.Count; j++)
                {
                    for (int i = resultsStartIndex; i < Search_Data.images[j].Count; i++)
                    {
                        NopsaImage n = new NopsaImage(Search_Data.images[j][i], cm);
                        n.Location = new Point(0, nloc);
                        nloc = nloc + n.Height;
                        scrollpanel.Controls.Add(n);
                        if (i == 10 + resultsStartIndex)
                        {
                            resultsStartIndex = resultsStartIndex + 10;
                            break;
                        }
                    }
                }
            }
            else  //back page clicked
            {
                int nloc = 0;
                for (int j = 0; j < Search_Data.sources.Count; j++)
                {
                    //for (int i = resultsStartIndex; i > Search_Data.images[j].Count - 10 ; i--)
                    for (int i = 10; i > -1; i--)
                    {
                        NopsaImage n = new NopsaImage(Search_Data.images[j][resultsStartIndex], cm);
                        n.Location = new Point(0, nloc);
                        nloc = nloc + n.Height;
                        scrollpanel.Controls.Add(n);

                        
                        if (resultsStartIndex == 0)
                        {
                            break;
                        } 

                        resultsStartIndex--;                        
                    }

                }
 
            }

            this.Width = this.Width + 1; //just to trigger onresize

           
        }

        private void searchTB_OnKeyDownHandler(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Return)
            {
                //Debug.WriteLine("down key pressed!! " + this.searchTB.Text);

                //apprently this line stops the default beep sound when enter key is pressed!!!
                e.SuppressKeyPress = true;
            }
            
        }

        protected override void OnResize(System.EventArgs e)
        {
            if (ThisAddIn.isstarted)
            {
                //searchTB.Width = ThisAddIn.searchUC.Width - 40;
                searchTB.Width = ThisAddIn.searchUC.Width - 80;
                searchBtn.Width = 33;
                settings_btn.Width = 33;

                searchTB.Left = 2;
                searchBtn.Left = searchTB.Width + settings_btn.Width + 6;
                settings_btn.Left = searchTB.Width + 6;
                settings_btn.Top = searchBtn.Top;

                //scrollpanel.Left = Width - scrollpanel.Width;
                scrollpanel.Width = Width - 2 ;

                if (scrollpanel.Controls.Count > 0) // that means there is something in there
                {
                    if ((scrollpanel.Controls[0].Width * 2) < Width)
                    {
                        if (scrollpanel.Controls.Count > 1) // ie. atleast there are 2 controls so we need to arrange them
                        {
                            if (scrollpanel.Controls[0].Left == scrollpanel.Controls[1].Left) // it means that this rearrangement hasnt been done before so lets rearrange
                            {
                                for (int i = 0; i < scrollpanel.Controls.Count; i++)
                                {
                                    if (i % 2 == 0)
                                    {
                                        scrollpanel.Controls[i].Left = 0;
                                    }
                                    else
                                    {
                                        scrollpanel.Controls[i].Left = scrollpanel.Controls[i - 1].Left + scrollpanel.Controls[i].Width + 4;
                                    }
                                    if (i == 0 || i == 1)
                                    {
                                        scrollpanel.Controls[i].Top = scrollpanel.Controls[0].Top;
                                    }
                                    else
                                    {
                                        if (i % 2 == 0)
                                        {
                                            scrollpanel.Controls[i].Top = scrollpanel.Controls[i - 2].Top + scrollpanel.Controls[i].Height;
                                        }
                                        else
                                        {
                                            scrollpanel.Controls[i].Top = scrollpanel.Controls[i - 1].Top;
                                        }

                                    }
                                }
                            }
                        }
                    }
                    if ((scrollpanel.Controls[0].Width * 2) + 60 > Width)
                    {
                        if (scrollpanel.Controls.Count > 1)
                        {
                            if (scrollpanel.Controls[0].Top == scrollpanel.Controls[1].Top) // that means previosuly it had two imagesboxes per line, but now we have to make it one (the default virtical one)
                            {
                                Debug.WriteLine("awesomeness");
                                for (int i = 1; i < scrollpanel.Controls.Count; i++)
                                {
                                    scrollpanel.Controls[i].Left = 0;
                                    scrollpanel.Controls[i].Top = scrollpanel.Controls[i - 1].Top + scrollpanel.Controls[i].Height;
                                }
                            }
                        }
                    }
                }

                Debug.WriteLine("suc onresize");
            }
        }


        private void searchTB_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Return)
            {
                //MessageBox.Show("You pressed enter! Good job!");
                searchTB.Select(searchTB.Text.Length , 0);
                searchBtn.PerformClick();
            }
        }
         
        private void searchTB_TextChanged(object sender, EventArgs e)
        {
            //Debug.WriteLine("New text in the searchbox: " + searchTB.Text );
           
            TimeSpan ts = DateTime.UtcNow.Subtract(this.searchtb_timestamp);
            if (ts.TotalSeconds > 1.0) //i.e. if user hasn't changed text for atleast 1 sec .. only then fire suggest
            {
                if(searchTB.TextLength > 2) //search text must have something of length 3 or more.
                {
                    if (searchTB.Text.Trim() != this.text_to_search) // it should be something new (without whitespaces)
                    {
                        this.text_to_search = searchTB.Text.Trim();
                        //now as it has passed all requirements so lets call suggestions from server
                        //Debug.WriteLine("All suggest tests passed");

                        //PUT THIS BACK UP
                        cm.suggest(searchTB.Text);
                    }
                }
                
            }
            this.searchtb_timestamp = DateTime.UtcNow; //updating timestamp

        }

        //this is called frm xml layer
        public void update_suggestions()
        {
            Debug.WriteLine("Inside update_suggestions");

            searchTB.AutoCompleteCustomSource.Clear();
            for (int i = 0; i < Suggest_Data.suggestions.Count; i++)
            {
                searchTB.AutoCompleteCustomSource.AddRange(Suggest_Data.suggestions[i].ToArray());
            }

        }

        //this is called frm xml layer
        public void update_images()
        {
            Debug.WriteLine("Inside update_images");
            /*
            Debug.WriteLine("count: " + Search_Data.sources.Count);
            //Debug.WriteLine("count: " + Search_Data.images[0]);
            NOPSAImageHolder temp = Search_Data.images[0][0];
            Debug.WriteLine("img_id: " + temp.img_id);
            Debug.WriteLine("img_rights: " + temp.img_rights);
            Debug.WriteLine("img_holder: " + temp.img_holder);
            Debug.WriteLine("img_url: " + temp.img_url);
            Debug.WriteLine("img_creator: " + temp.img_creator);
            Debug.WriteLine("img_height: " + temp.img_height);
            Debug.WriteLine("img_width: " + temp.img_width);
            Debug.WriteLine("img_prirority: " + temp.img_prirority);
            */
            if (search_data_recieved == false) // if this is just new search result then
            {
                //first clean the panel for it
                scrollpanel.Controls.Clear();
                loadscrollpanel(false);
            }

            search_data_recieved = true;
          
            /*

            NopsaImage ni0 = new NopsaImage(Search_Data.images[0][0]);
            ni0.Location = new Point(0, 0);
            scrollpanel.Controls.Add(ni0);

            NopsaImage ni1 = new NopsaImage(Search_Data.images[0][1]);
            ni1.Location = new Point(0, ni0.Height);
            scrollpanel.Controls.Add(ni1);

            NopsaImage ni2 = new NopsaImage(Search_Data.images[0][2]);
            ni2.Location = new Point(0, ni1.Height * 2);
            scrollpanel.Controls.Add(ni2);

            NopsaImage ni3 = new NopsaImage(Search_Data.images[0][3]);
            ni3.Location = new Point(0, ni2.Height * 3);
            scrollpanel.Controls.Add(ni3);
            */
        }

        private void next_page_btn_Click(object sender, EventArgs e)
        {
            loadscrollpanel(false);
        }

        private void back_page_btn_Click(object sender, EventArgs e)
        {
            loadscrollpanel(true);
        }

        private void settings_btn_Click(object sender, EventArgs e)
        {
            if (settings_listbox.Visible) settings_listbox.Hide();
            else settings_listbox.Show();
        }


        
        //called by cm to raise suggest event
        //public Button give_gosuggestions { get { return gosuggestions; } }
        /*
        private void gosuggestions_Click_1(object sender, EventArgs e)
        {
            Debug.WriteLine("gosuggestion clicked");

            //Debug.WriteLine("one suggestion: " + Suggest_Data.suggestions[0][0]);
            //searchTB.AutoCompleteCustomSource.Add(Suggest_Data.suggestions[0][0]);
            //searchTB.AutoCompleteCustomSource.Add( (String)Suggest_Data.temp[0] );
            string[] suggestions = new string[] {
                "Google",
                "Google Images",
                "Yahoo",
                "Youtube"
                };
            searchTB.AutoCompleteCustomSource.AddRange(suggestions);
            //searchTB.AutoCompleteCustomSource.AddRange(suggestions);


        }
         */
    }
}
