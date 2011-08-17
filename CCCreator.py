#!/usr/bin/python2.6

import tornado

html_content = '''
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN"
	  "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
 <html xmlns="http://www.w3.org/1999/xhtml" xml:lang="en" lang="en">
   <head>
<style>


html {
	margin: 0;
	/* setting border: 0 hoses ie6 win window inner well border */
	padding: 0;
  }

body {
	font-family: Verdana, 'trebuchet ms', sans-serif;
	font-size: 13px;
	line-height : 140%;
	border-top: 10px solid #99CC66;
	margin : 0;
	padding : 0;
  }


a {
  color : blue;
  }

.pagebody {
  margin-left : 50px;
  width : 700px;
  margin-top : 50px;
  height : 1000px;
  }

.pagetitle {
  font-size : 20px;
  font-weight : normal;
  margin-top : 30px;
  }




/* this is stuff for our widget builder */

#customize {
  padding : 10px;
  padding-bottom : 30px;
  clear : both;
  }

#preview {
  padding-top : 20px;
  padding-left : 20px;
  padding-bottom : 40px;
  }

#step1, #step2 {
  clear : both;
  }


.instruction {
  padding : 10px;
  border-top : 1px solid silver;
  font-weight : bold;
  margin-top : 30px;
  }

.field {
  padding-top : 20px;
  clear : both;
  }

.button-style {
  font-size : 16px;
  padding : 5px;
  }

.field-label {
  float: left;
  display: inline;
  width: 150px;
  margin-right: 5px;
  padding-top: 4px;
  text-align: right;
  font-size : 12px;
  font-weight : bold;
  }

.field-input {
  float: left;
  display: inline;
  width: 325px;
  font-weight : normal;
  color : gray;
  font-size : 11px;
  }





/* this is stuff for the cc widget */

.cc_js_body {
  background-color: #fff;
  padding: 0;
  margin: 0;
}

#cc_js_header-main {
    width:  93%;
    min-width:  700px;
    margin: 0 3%;
    padding: 10px 0 2px 1%;
    text-align: left;
    font-size: 11px !important;
}

/* -- elements */

a.cs_js_a, a:link.cs_js_a {
  text-decoration: none;
  color: #00b;
}

a:hover.cs_js_a {
  text-decoration: underline;
}

/**
 * This was written by CC as a demonstration of how to interoperate
 * with the Creative Commons JsWidget.  No rights reserved.
 * 
 * See README for a little more detail.
 */

#cc_js_license_selected
{
    border:         none;
    text-align:     left;
    margin-bottom: 2.7%;
		width: 100%;
    font-size : 13px;
    font-weight : normal;
}


#cc_js_license_selected img { 
  display : block;
  }


#cc_js_jurisdiction_box
{
    /* border: 1px solid black; */
    padding: 0.5% 2% 1% 2%;
    margin-bottom: 1%;
}

#cc_js_lic-menu h2
{
    /* text-decoration: underline; */
    /* border-bottom: 1px solid black; */
    padding: 3% 0 1% 0;
    border: none;
}

#cc_js_lic-result
{
    padding: 0;
    margin: 0;
}
    
select#cc_js_jurisdiction
{
    margin-bottom: 2%;
}

textarea#cc_js_result
{
    width: 91%;
    border: 1px solid #ccc;
    color: gray;
    margin-top: 1%;
}

a.cc_js_a img
{
    border: none;
    text-decoration: none;
}

#cc_js_more_info
{
    border: 1px solid #eee;
    padding: 0.5% 2% 1% 2%;
    margin-bottom: 1%;
    margin-top: 1%;
		width: 87%;
}
#cc_js_more_info table
{
    width: 65%;
}

#cc_js_more_info .header
{
    width: 35%;
}

#cc_js_more_info input
{
    width: 100%;
    border: 1px solid #ccc;
}

#cc_js_required
{
	
    border: 1px solid #ccc;
    padding: 0.5% 2% 1% 2%;
    margin-bottom: 1%;
    margin-top: 1%;
    /* background: #efefef; */
    background: #eef6e6;
		width: 95%;
}

#cc_js_work_title
{
    font-style: italic;
}


#cc_js_optional
{
    border: 1px solid #eee;
    padding: 0.5% 2% 1% 2%;
    margin-bottom: 1%;
    margin-top: 1%;
		width: 87%;
}


.cc_js_cc-button
{
    padding-bottom: 1%;
}

.cc_js_info
{
    vertical-align: middle;
}

img.cc_js_info {
	float: right;
}

#cc_js_lic-menu p {
	width: 18em;
	padding: 3px 0 5px 0;
}


#cc_js_jurisdiction_box {
  float : left;
  }
  
.cc_js_tooltip {
    background:     white;
    border:         2px solid gray;
    padding:        3px 10px 3px 10px;
    width:          240px;
    text-align:     left;
    font-size: 11px;
    }

.cc_js_tooltip .cc_js_icon
{
    float:          left; 
    padding-right:  4%;
    padding-bottom: 20%;
}

.cc_js_tooltipimage
{
    border:         2px solid gray;
}

.cc_js_infobox
{
    cursor:         help;
}

.cc_js_question
{
    cursor:         help;
    color:          #00b;
}

#cc_js_lic-menu p {
	margin: 0 0 5px 0;
}


</style>


     <!--<link rel="stylesheet" href="example-widget-style.css" />-->
     <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
     <!-- Written by Creative Commons in 2007.  No rights reserved; see COPYING for more details. -->
	 <!-- edited by Hasanat @ HIIT -->
   </head>
   <body>
     <div id="cc_js_widget_container">
       <script type="text/javascript" src="http://api.creativecommons.org/jswidget/tags/0.97/complete.js?locale=en_US&want_a_license=definitely"></script>
     </div>
     
     <script type="text/javascript">	 
		function showChosenLicenseURI() {
			return document.getElementById("cc_js_result_uri").value;
		}

		function showChosenLicenseName() {
			return document.getElementById("cc_js_result_name").value;
		}

		function showChosenLicenseImageURL() {
			return document.getElementById("cc_js_result_img").value;
	}</script>
	
	<button style="margin-left:30px;" onClick="window.location = '?lurl='+ showChosenLicenseURI() +'&lname='+ showChosenLicenseName() +'&liurl=' + showChosenLicenseImageURL() + ''";>Apply License</button>
	
   </body>
 </html>
 '''


class CCCreator(tornado.web.RequestHandler):
    def get(self):
        try:
            content = "<license><license_url>" + self.get_argument("lurl") + "</license_url>"
            content = content +  "<license_name>" + self.get_argument("lname") + "</license_name>"
            content = content +  "<license_image>" + self.get_argument("liurl") + "</license_image></license>"

            self.write(  content  )
        except:
            self.write(html_content)




