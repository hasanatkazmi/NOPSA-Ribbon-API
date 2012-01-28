using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using WebSocketSharp;
using System.Diagnostics;
using System.Xml;
using System.IO;
using System.Collections;
using System.Windows.Forms;
using System.Threading;

namespace PowerPointAddInOne
{
    /*
     * NOPSAImageHolder & NOPSAImageTagHolder are unit image and tag.
     * Search_Data & Suggest_Data are the final full data to hold
     */

    public class NOPSAImageHolder
    {
        public String img_id = "";
        public String img_source = "";
        public String img_rights = "";
        public String img_holder = "";
        public String img_url = "";
        public String img_creator = "";
        public String img_height = "";
        public String img_width = "";
        public String img_prirority = "";

        public List<NOPSAImageTagHolder> tags = new List<NOPSAImageTagHolder>();
    }

    public class NOPSAImageTagHolder
    {
        public String tag_id = "";
        public String tag_relevence = "";
        public String tag_string = ""; //the actual tag
    }

    public static class Search_Data
    {

        public static string query;
        public static List<String> sources = new List<String>(); //a list of sources as they appear in search
        public static List<List<NOPSAImageHolder>> images = new List<List<NOPSAImageHolder>>(); // a double arraylist, col shows sources (same order), rows shows image

        //public static ArrayList temp = new ArrayList();
        public static void init(string query_)
        {
            query = query_;
            sources.Clear();
            images.Clear();

        }
    }
   

    public static class Suggest_Data 
    {
        
        public static string query;
        public static List<String> sources = new List<String>(); //a list of sources as they appear in suggestions
        public static List<List<String>> suggestions = new List<List<string>>(); // a double arraylist, col shows sources (same order), rows shows suggestions

        //public static ArrayList temp = new ArrayList();
        public static void init(string query_)
        {
            query = query_;
            sources.Clear();
            suggestions.Clear();
        }
    }

    public class XmlProcessor
    {
        public static SearchUserControl suc; //referece

        String xml;
        public XmlProcessor(String xml) 
        {
            this.xml = xml;
            parse();
        }

        private void parse() 
        {
            if (this.xml.Contains("notice message")) return; //REMOVE NOTICE MESSAGE FROM API

            else if (this.xml.Contains("<code>") && !this.xml.Contains("900"))
            {
                //Debug.WriteLine("XML code error: " + this.xml.ToString());
                return;
            }

            List<String> suggestions = new List<String>();
            String source = "";

            List<NOPSAImageHolder> images = new List<NOPSAImageHolder>();
            List<NOPSAImageTagHolder> img_tags = new List<NOPSAImageTagHolder>(); //dont forget to empty it after a 'round'

            //bool issearch = false;

            bool img_start = false;
            bool tag_start = false;

            String img_id = "";
            String img_rights = "";
            String img_holder = "";
            String img_url = "";
            String img_creator = "";
            String img_height = "";
            String img_width = "";
            String img_prirority = "";

            String tag_id = "";
            String tag_relevence = "";
            String tag_string = "";

            //Debug.WriteLine(this.xml);
            using (XmlReader reader = XmlReader.Create(new StringReader( this.xml )))
            {
                while (reader.Read())
                {
                    switch (reader.NodeType)
                    {
                        case XmlNodeType.Element: // The node is an element.
                            //Debug.Write("<" + reader.Name);
                            //Debug.WriteLine(">");
                            if (reader.Name == "source") // true for suggest
                            {
                                String recieved_name = reader["name"];
                                if (recieved_name != null && !recieved_name.Equals(""))
                                {
                                    //Debug.WriteLine("suggest recieved name isnt null '" + recieved_name + "'");
                                    source = recieved_name;
                                }
                                //Debug.WriteLine("its suggestion " + reader["name"]); // only true for suggesiton
                            }
                            else if (reader.Name == "search") // true for search
                            {
                                String recieved_name = reader["source"];
                                if (recieved_name != null && !recieved_name.Equals(""))
                                {
                                    //Debug.WriteLine("search recieved name isnt null " + recieved_name);
                                    source = recieved_name;
                                }
                            }
                            else if (reader.Name == "suggestion") // its suggestion element
                            {
                                String temp = reader.ReadString().Trim(); //suggestion text
                                //Debug.WriteLine("temp: " + temp);
                                suggestions.Add(temp);
                            }
                            else if (reader.Name == "image") // its search->image element
                            {
                                //Debug.WriteLine("starting image");
                                img_start = true; //so that other tags inside <image> can know that its their turn (not of someone with same name but outside <image>)
                            }
                            else if (reader.Name == "id") // currently multile tags start with id
                            {
                                if (img_start && !tag_start) // it must be related to <image>
                                {
                                    img_id = reader.ReadString().Trim();
                                }
                                if (img_start && tag_start) // it must be related to <image>..<tag>HERE</tag></image>
                                {
                                    tag_id = reader.ReadString().Trim();
                                }
                            }
                            else if (reader.Name == "rights")
                            {
                                if (img_start)
                                {
                                    img_rights = reader.ReadString().Trim();
                                }
                            }
                            else if (reader.Name == "image_holder")
                            {
                                if (img_start)
                                {
                                    img_holder = reader.ReadString().Trim();
                                }
                            }
                            else if (reader.Name == "url")
                            {
                                if (img_start)
                                {
                                    img_url = reader.ReadString().Trim();
                                }
                            }
                            else if (reader.Name == "creator")
                            {
                                if (img_start)
                                {
                                    img_creator = reader.ReadString().Trim();
                                }
                            }
                            else if (reader.Name == "height")
                            {
                                if (img_start)
                                {
                                    img_height = reader.ReadString().Trim();
                                }
                            }
                            else if (reader.Name == "width")
                            {
                                if (img_start)
                                {
                                    img_width = reader.ReadString().Trim();
                                }
                            }
                            else if (reader.Name == "prirority")
                            {
                                if (img_start)
                                {
                                    img_prirority = reader.ReadString().Trim();
                                }
                            }
                            else if (reader.Name == "tag")
                            {
                                if (img_start)
                                {
                                    tag_start = true; // telling others that tag has started
                                    //Debug.WriteLine("its tag");
                                }
                            }
                            else if (reader.Name == "relevancy")
                            {
                                if (img_start && tag_start)
                                {
                                    tag_relevence = reader.ReadString().Trim();
                                }
                            }
                            else if (reader.Name == "string")
                            {
                                if (img_start && tag_start)
                                {
                                    tag_string = reader.ReadString().Trim();
                                }
                            }
                            break;
                        case XmlNodeType.Text: //Display the text in each element.
                            //Debug.WriteLine(reader.Value);
                            break;
                        case XmlNodeType.EndElement: //Display the end of the element.
                            //Debug.Write("</" + reader.Name);
                            //Debug.WriteLine(">");
                            if (reader.Name == "image") // its search->image element
                            {
                                //Debug.WriteLine("ending image");
                                img_start = false; 
                                //now put image in container
                                /*
                                Debug.WriteLine("img_id: " + img_id);
                                Debug.WriteLine("img_rights: " + img_rights);
                                Debug.WriteLine("img_holder: " + img_holder);
                                Debug.WriteLine("img_url: " + img_url);
                                Debug.WriteLine("img_creator: " + img_creator);
                                Debug.WriteLine("img_height: " + img_height);
                                Debug.WriteLine("img_width: " + img_width);
                                Debug.WriteLine("img_prirority: " + img_prirority);
                                */
                                NOPSAImageHolder temp_img = new NOPSAImageHolder();
                                temp_img.img_source = source;
                                temp_img.img_id = img_id;
                                temp_img.img_rights = img_rights;
                                temp_img.img_holder = img_holder;
                                temp_img.img_url = img_url;
                                temp_img.img_creator = img_creator;
                                temp_img.img_height = img_height;
                                temp_img.img_width = img_width;
                                temp_img.img_prirority = img_prirority;

                                temp_img.tags = img_tags;
                                img_tags = new List<NOPSAImageTagHolder>();

                                images.Add(temp_img);

                                Debug.WriteLine("this is cm, source: " + source + " , url: " + img_url);
                            }
                            else if (reader.Name == "tag")
                            {
                                if (img_start)
                                {
                                    tag_start = false;
                                    //Debug.WriteLine("its tag closing");

                                    NOPSAImageTagHolder temp_tag_holder = new NOPSAImageTagHolder();
                                    temp_tag_holder.tag_id = tag_id;
                                    temp_tag_holder.tag_relevence = tag_relevence;
                                    temp_tag_holder.tag_string = tag_string;

                                    //Debug.WriteLine("id: "+tag_id);
                                    //Debug.WriteLine("relevence: "+tag_relevence);
                                    //Debug.WriteLine("string: " + tag_string);

                                    img_tags.Add(temp_tag_holder);
                                }
                            }

                            break;
                    }
                }
            }

            //Debug.WriteLine("some data processed");
            if (suggestions.Count != 0) // so it means the returned xml was from a suggestions query
            {
                Suggest_Data.sources.Add(source);
                Suggest_Data.suggestions.Add(suggestions);

                suc.ActiveControl.Invoke((MethodInvoker)delegate
                {
                    // runs on UI thread
                    suc.update_suggestions();
                });
                
            }
            else if (images.Count != 0)
            {
                //Debug.WriteLine("images arnt zero");
                Search_Data.sources.Add(source);
                Search_Data.images.Add(images);
                
                //WE MAY RUN SOME SHUFFLING ALGO HERE
                if (suc.InvokeRequired)
                {
                    suc.ActiveControl.Invoke(new MethodInvoker(delegate
                    {
                        suc.update_images();
                    }));
                }
                else 
                {
                    suc.update_images();
                }

                /*
                suc.ActiveControl.Invoke((MethodInvoker)delegate
                {
                    // runs on UI thread
                    suc.update_images();
                });
                */ 
            }
            source = ""; //not required but helps in debugging
        }

    }

    public class CommunicationManagerEvents
    {

        //public SearchUserControl suc; //a referece

        // INFO: these handlers catch events raised inside web sockets lib
        public EventHandler onOpen = (o, e) =>
        {
            //Console.WriteLine("[WebSocket] Opened.");
            Debug.WriteLine("[WebSocket] Opened.");
        };

        public MessageEventHandler onMessage = (o, s) =>
        {
            //Debug.WriteLine("[WebSocket] Message: {0}", s);
            Debug.WriteLine("data recieved from server");
            XmlProcessor xmlp = new XmlProcessor(s);
            //xmlp.suc = this.suc;
        };

        public MessageEventHandler onError = (o, s) =>
        {
            //Console.WriteLine("[WebSocket] Error  : {0}", s);
            Debug.WriteLine("[WebSocket] Error  : {0}", s);
        };

        public EventHandler onClose = (o, e) =>
        {
            //Console.WriteLine("[WebSocket] Closed.");
            Debug.WriteLine("[WebSocket] Closed.");
        };

    }

    public class CommunicationManager
    {
        //public event MyHandler1 Event1; 
        //SearchUserControl suc;

        //public const String URL = "ws://localhost:8000/Socket";
        //public const String URL = "ws://119.154.71.227:8000/Socket";
        //public const String URL = "ws://nopsa.hiit.fi:8180/Socket";
        //public const String URL = "ws://ec2-75-101-217-90.compute-1.amazonaws.com/Socket";
        public const String URL = "ws://ec2-107-20-212-167.compute-1.amazonaws.com:8000/Socket";
        //public const String API_KEY = "2MWIRBXFMRYV"; //vm
        //public const String API_KEY = "ZF5YLDF2TJ85"; //nopsa
        public const String API_KEY = "6L5AG1FKOYT4"; //aws
        

        WebSocket ws;


        public void connect()
        {
            CommunicationManagerEvents cme = new CommunicationManagerEvents();
            //cme.suc = suc; //so that cme can pass referece to xml layer
            ws = new WebSocket(CommunicationManager.URL, cme.onOpen, cme.onMessage, cme.onError, cme.onClose);
            
        }

        public void send(String xml) {
            if (ws == null)
                connect();
            ws.Send(xml);
        }

        public void suggest(string query){
            //resetting data bank
            Suggest_Data.init(query);            
            
            String xml = "<?xml version=\"1.0\" ?><query api=\"" + CommunicationManager.API_KEY + "\" type=\"suggest\">"+ query +"</query>";
            send(xml);
        }

        public void addtag(String image_id, String tag)
        {
            String xml = "<?xml version=\"1.0\" ?><tagService  api=\"" + CommunicationManager.API_KEY + "\" type=\"add\" imageid=\"" + image_id + "\">" + tag + "</tagService>";
            send(xml);
        }

        public void increment_relevence(String image_id, String tag) // tag can be tag_id or tag (actual tag)
        {
            String xml = "<?xml version=\"1.0\" ?><relevanceService api=\"" + CommunicationManager.API_KEY + "\" type=\"increase\" imageid=\"" + image_id + "\">" + tag + "</relevanceService>";
            send(xml);
        }

        public void decrement_relevence(String image_id, String tag) // tag can be tag_id or tag (actual tag)
        {
            String xml = "<?xml version=\"1.0\" ?><relevanceService api=\"" + CommunicationManager.API_KEY + "\" type=\"decrease\" imageid=\"" + image_id + "\">" + tag + "</relevanceService>";
            send(xml);
        }

        public void search(string query, string[] sources)
        {
            //TODO: implement sendng sources to the server and server should also be made to do selective searches
            //resetting data bank
            Search_Data.init(query);

            String xml = "<?xml version=\"1.0\" ?><query api=\"" + CommunicationManager.API_KEY + "\" type=\"search\">"+ query +"</query>";
            send(xml);
        }

        public CommunicationManager(SearchUserControl suc_reference)
        {
            //this.suc = suc_reference;
            XmlProcessor.suc = suc_reference;
        }

    }
}
