using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using System.Diagnostics;
using Microsoft.Office.Interop.PowerPoint;
using System.Threading;

namespace PowerPointAddInOne
{
    class Util
    {
        static public string GetUserDataPath()
        {
            string dir = Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData);
            dir = System.IO.Path.Combine(dir, "Open Images");
            if (!Directory.Exists(dir))
                Directory.CreateDirectory(dir);
            return dir;
        }

        public class Downloader
        {
            public System.ComponentModel.BackgroundWorker downloader_bg;

            String source;

            public Downloader(String source)
            {
                this.source = source;

                downloader_bg = new System.ComponentModel.BackgroundWorker();
                downloader_bg.DoWork += new System.ComponentModel.DoWorkEventHandler(downloader_bg_DoWork);
                downloader_bg.RunWorkerAsync();
                
            }

            void downloader_bg_DoWork(object sender, System.ComponentModel.DoWorkEventArgs e)
            {
                ApplicationInteraction.PowerPointApplication.ActiveWindow.Selection.SlideRange.Shapes.AddPicture(source,
                    Microsoft.Office.Core.MsoTriState.msoFalse,
                    Microsoft.Office.Core.MsoTriState.msoCTrue,
                    0,
                    0);
            }

        }

    }

    class ApplicationInteraction
    {
        public static Application PowerPointApplication;

        public static List<NOPSAImageHolder> imagesadded = new List<NOPSAImageHolder>();
        //public static List<int> shape_ids = new List<int>();
        public static List<int> shape_uuids = new List<int>();

        public static bool credits_slide_added = false;

        public static bool license_added = false;

        public static void add_credits_slide()
        {
            /*
            //first updating list of images which are in the document
            //actually this step will delete images from shape_ids which were deleted by user after downloading
            foreach (Slide slide in ApplicationInteraction.PowerPointApplication.ActivePresentation.Slides)
            {
                foreach (Shape shape in slide.Shapes)
                {
                    //Debug.WriteLine("shape id: " + shape.Id);
                    bool temp = false;
                    foreach (int hash in shape_uuids)
                    {
                        if (shape.GetHashCode() == hash)
                        {
                            Debug.WriteLine("one match found");
                            temp = true;
                        }
                    }
                    if (temp == false) // i.e. shape wasnt added by user or he has deleted it
                    {
                        //here i have to check if this shape was added using addin.. how to do it?
                    }
                }
            }
            */

            int s_padding = 0;
            if (license_added) s_padding = s_padding + 1;

            if (credits_slide_added)
            {
                //delete the last slide
                ApplicationInteraction.PowerPointApplication.ActivePresentation.Slides[ApplicationInteraction.PowerPointApplication.ActivePresentation.Slides.Count - s_padding].Delete();
            }

            credits_slide_added = true;

            
            //adding slide on last page
            int totalslides = ApplicationInteraction.PowerPointApplication.ActivePresentation.Slides.Count ;
            CustomLayout pptlayout = ApplicationInteraction.PowerPointApplication.ActivePresentation.Slides[1].CustomLayout; // it starts from 1.. (strage but true)
            //CustomLayout pptlayout = ApplicationInteraction.PowerPointApplication.ActivePresentation
            Slide addedslide =  ApplicationInteraction.PowerPointApplication.ActivePresentation.Slides.AddSlide( totalslides+1 - s_padding, pptlayout);
            addedslide.Layout = PpSlideLayout.ppLayoutBlank;


            //addedslide.Design.SlideMaster.Shapes.AddTextbox(Microsoft.Office.Core.MsoTextOrientation.msoTextOrientationHorizontal, 10, 10, 200, 30).Select(Microsoft.Office.Core.MsoTriState.msoFalse);
            //Shape addedtext = addedslide.Design.SlideMaster.Shapes.AddTextbox(Microsoft.Office.Core.MsoTextOrientation.msoTextOrientationHorizontal, 80, 20, 200, 30);

            Shape ackn = addedslide.Shapes.AddTextbox(Microsoft.Office.Core.MsoTextOrientation.msoTextOrientationHorizontal, 80, 40, 300, 30);
            ackn.TextFrame.TextRange.Text = "Photo Acknowledgements";
            ackn.TextFrame.TextRange.Font.Underline = Microsoft.Office.Core.MsoTriState.msoTrue;
            ackn.TextFrame.TextRange.Font.Bold = Microsoft.Office.Core.MsoTriState.msoTrue;
            ackn.TextFrame.WordWrap = Microsoft.Office.Core.MsoTriState.msoTrue;
            


            float top = ackn.Top + ackn.Height + 10;
            float element_width = 540;

            foreach (NOPSAImageHolder image in imagesadded)
            {
                Shape addedtext = addedslide.Shapes.AddTextbox(Microsoft.Office.Core.MsoTextOrientation.msoTextOrientationHorizontal, 80, top, element_width, 12);
                addedtext.TextFrame.TextRange.Text = "Rights / Creator: " + image.img_rights + " "  + image.img_creator;
                addedtext.TextFrame.TextRange.Font.Bold = Microsoft.Office.Core.MsoTriState.msoTrue;
                addedtext.TextFrame.WordWrap = Microsoft.Office.Core.MsoTriState.msoTrue;
                addedtext.TextFrame.TextRange.Font.Size = 10;

                Shape addedurl = addedslide.Shapes.AddTextbox(Microsoft.Office.Core.MsoTextOrientation.msoTextOrientationHorizontal, 80, top + addedtext.Height - 6, element_width, 12);
                addedurl.TextFrame.TextRange.Text = "Source: " + image.img_holder;
                addedurl.TextFrame.WordWrap = Microsoft.Office.Core.MsoTriState.msoTrue;
                addedurl.ActionSettings[PpMouseActivation.ppMouseClick].Hyperlink.Address = image.img_holder;
                addedurl.TextFrame.TextRange.Font.Size = 9;

                top = addedurl.Top + addedurl.Height;
            }
            
            //deleting addedslide
            //ApplicationInteraction.PowerPointApplication.ActivePresentation.Slides.FindBySlideID(addedslide.SlideID).Delete()


            //Debug.WriteLine("total shapes: " + ApplicationInteraction.PowerPointApplication.ActiveWindow.Selection.ShapeRange.Count);
            //int totalshapes = ApplicationInteraction.PowerPointApplication.ActiveWindow.Selection.ShapeRange.Count;

            
        }
        
        public static void download_image_to_doc(NOPSAImageHolder imageholder)
        {
            //Debug.WriteLine(Util.GetUserDataPath());

            //ApplicationInteraction.PowerPointApplication.ActiveWindow.Selection.SlideRange.Shapes.AddPicture(Path.Combine(Util.GetUserDataPath() , "citywall.jpg") ,
            //new Util.Downloader(imageholder.img_url);


            //ApplicationInteraction.imagesadded.Add(imageholder);

            //downloading image
            //office hangs when loading image, even when run in seperate thread or backgroundworker. when you insert image from url, office hangs (without using addin), so I suppose its problem at their end

            try
            {
                Shape addedpic = ApplicationInteraction.PowerPointApplication.ActiveWindow.Selection.SlideRange.Shapes.AddPicture(
                                    imageholder.img_url,
                                    Microsoft.Office.Core.MsoTriState.msoFalse,
                                    Microsoft.Office.Core.MsoTriState.msoCTrue,
                                    0,
                                    0);

                shape_uuids.Add(addedpic.GetHashCode());
                imagesadded.Add(imageholder);

                ApplicationInteraction.add_credits_slide();

            }
            catch (Exception ex)
            {
                //System.Windows.Forms.MessageBox.Show("Dear paba, here comes a error :) ");
                //System.Windows.Forms.MessageBox.Show("-" + ex.Message);
                //System.Windows.Forms.MessageBox.Show("-" + ex.StackTrace);
                TextWriter tw = new StreamWriter(Environment.GetFolderPath(Environment.SpecialFolder.Desktop) + "\\ribbon.txt");
                tw.WriteLine("Message:");
                tw.WriteLine(ex.Message);
                tw.WriteLine("StackTrace:");
                tw.WriteLine(ex.StackTrace);
                tw.WriteLine("Source:");
                tw.WriteLine(ex.Source);
                tw.WriteLine("helplink"); 
                tw.WriteLine(ex.HelpLink);
                tw.Close();

             }
        }

        public static void addlicense(String license_url, String license_name, String license_imageurl)
        {
            if (license_added)
            {
                //delete the last slide
                ApplicationInteraction.PowerPointApplication.ActivePresentation.Slides[ApplicationInteraction.PowerPointApplication.ActivePresentation.Slides.Count].Delete();
            }

            license_added = true;

            int left = 80;

            int initial_top = 340;

            //adding slide on last page
            int totalslides = ApplicationInteraction.PowerPointApplication.ActivePresentation.Slides.Count;
            CustomLayout pptlayout = ApplicationInteraction.PowerPointApplication.ActivePresentation.Slides[1].CustomLayout; // it starts from 1.. (strage but true)
            Slide addedslide = ApplicationInteraction.PowerPointApplication.ActivePresentation.Slides.AddSlide(totalslides + 1, pptlayout);
            addedslide.Layout = PpSlideLayout.ppLayoutBlank;


            Shape ackn = addedslide.Shapes.AddTextbox(Microsoft.Office.Core.MsoTextOrientation.msoTextOrientationHorizontal, left, initial_top, 600, 30);
            ackn.TextFrame.TextRange.Text = license_name;
            ackn.TextFrame.TextRange.Font.Underline = Microsoft.Office.Core.MsoTriState.msoTrue;
            //ackn.TextFrame.TextRange.Font.Bold = Microsoft.Office.Core.MsoTriState.msoTrue;
            ackn.TextFrame.WordWrap = Microsoft.Office.Core.MsoTriState.msoTrue;

            ackn = addedslide.Shapes.AddTextbox(Microsoft.Office.Core.MsoTextOrientation.msoTextOrientationHorizontal, left, initial_top + 30, 600, 30);
            ackn.TextFrame.TextRange.Text = license_url;
            //ackn.TextFrame.TextRange.Font.Underline = Microsoft.Office.Core.MsoTriState.msoTrue;
            //ackn.TextFrame.TextRange.Font.Bold = Microsoft.Office.Core.MsoTriState.msoTrue;
            ackn.TextFrame.WordWrap = Microsoft.Office.Core.MsoTriState.msoTrue;
            ackn.ActionSettings[PpMouseActivation.ppMouseClick].Hyperlink.Address = license_url;

            Debug.WriteLine(license_imageurl);

            ackn = addedslide.Shapes.AddPicture(
                                    license_imageurl,
                                    Microsoft.Office.Core.MsoTriState.msoFalse,
                                    Microsoft.Office.Core.MsoTriState.msoCTrue,
                                    left+9,
                                    initial_top + 64);

        }

    }
}
