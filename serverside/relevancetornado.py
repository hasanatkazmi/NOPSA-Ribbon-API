#!/usr/bin/python2.6

import tornado
from module_manager import DriveAPIKeys, DriveRelevance

prefix = u'''
<!DOCTYPE html>
<!-- saved from url=(0032)http://html5demos.com/web-socket -->
<html lang="en"><head><meta http-equiv="Content-Type" content="text/html; charset=UTF-8">
<meta charset="utf-8">
<meta name="viewport" content="width=620">
<title>NOPSA addin for Office Suites</title>
<link rel="stylesheet" href="files/main.css" type="text/css">
<script src="files/h5utils.js"></script></head>
<body>
<section id="wrapper">
    <header>
      <h1>NOPSA addin for Office Suites</h1>
    </header>
<style>
#chat { width: 97%; }
.them { font-weight: bold; }
.them:before { content: 'server '; color: #bbb; font-size: 14px; }
.you { font-style: italic; }
.you:before { content: 'you '; color: #bbb; font-size: 14px; font-weight: bold; }
#log {
  overflow: auto;
  max-height: 300px;
  list-style: none;
  padding: 0;
/*  margin: 0;*/
}
#log li {
  border-top: 1px solid #ccc;
  margin: 0;
  padding: 10px 0;
}
</style>

<center>
<label style="float:left;width: 100%; padding: 5px 0px 5px 0px; background: #0C0; color:white;"><a  href="index.htm">About</a>  |  <a href="screenshots.htm">Screenshots</a>  |  <a href="api.htm">API Documentation</a> | <a href="demo.htm">API Demo</a> | <a href="download.htm">Download</a> | <a href="RelevanceTrend">Relevance Trends</a> | <a href="license.htm">License</a> | <a href="help.htm">Help</a></label>  
</center>

<article>

  <p id="mainp" style="padding-top:60px;">'''

suffix = u'''
  </p>

</article>

<p style="display:block;text-align:center;padding: 10px;margin: 10px 20px;	font-size: 0.8em;	text-align: center;	border-top: 1px solid #C9E0ED;color:grey;">
Copyright 2011 by Helsinki Institute for Information Technology HIIT.<br />
All Rights Reserved.
</p>

</section>
<a href="http://github.com/hasanatkazmi/NOPSA-Ribbon-API/"><img style="position: absolute; top: 0; left: 0; border: 0;" src="files/forkme_left_darkblue_121621.png" alt="Fork me on GitHub"></a>
<script src="files/prettify.packed.js"></script>

</body></html>'''


class RelevanceTrend(tornado.web.RequestHandler):
    def get(self):
        content = '''
            <p>Find Relevance Tred Over Time !</p>
            <form method="post" action="">
                <table>
                    <tr>
                        <td>Tag ID</td>
                        <td><input type="text" name="tagid"></td>
                    </tr>
                    <tr>
                        <td>Image ID</td>
                        <td><input type="text" name="imageid"></td>
                    </tr>
                    <tr>
                        <td>API Key</td>
                        <td><input type="text" name="apikey"></td>
                    </tr>
                    <tr>
                        <td></td>
                        <td><input type="submit"></td>
                    </tr>
                <table>
            </form> '''
        self.write(prefix + content + suffix)

    def post(self):
        apikey = ""
        imageid = ""
        tagid = ""
        try:
            apikey = self.get_argument("apikey")
            imageid = self.get_argument("imageid")
            tagid = self.get_argument("tagid")
            imageid = int(imageid)
            tagid = int(tagid)
        except:
            self.write(prefix + "<p>Please reconsider you inputs, they are flawed!</p>" + suffix)
            return

        granted = DriveAPIKeys().is_api( apikey )

        if granted != True:
            self.write(prefix + "<p>API key is not valid. If you think this is an error, please contact NOPSA at HIIT.</p>" + suffix)
            return


        data = DriveRelevance(tagid, imageid).giveHistory()

        self.write(prefix)
        self.write("<p><table><tr><td>Tag ID:</td><td>"+str(tagid)+"</td></tr><tr><td>Image ID:</td><td>"+str(imageid)+"</td></tr></p>")
        self.write("<table style='width:95%;'>")
        self.write("<tr style='background-color:#00bb00 ; color:white;'><td><b>Change</b></td><td><b>Date and Time</b></td></tr>")
        for i in range(len(data['changes'])): #changes or datatime should be of same length so we can use either
            changes = str(data['changes'][i])
            if changes[0] != '-': changes = '+' + changes
            self.write("<tr><td>"+ changes +"</td><td>"+ str(data['datetime'][i]) + " UTC" +"</td></tr>")

        self.write("</table>")
        self.write(suffix)



