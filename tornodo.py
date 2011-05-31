

import tornado.ioloop
import tornado.web

class MainHandler(tornado.web.RequestHandler):
    html = '''<html><body>
                    <p>
                        This is very basic service to show the current abilities of the system.<br />
                        There is no suggest for LOC, because there is no API<br />
                        In Image search for LOC, Image URL isn't given because there isn't API for that but it can be infered. <br/ >
                        In Google, NOPSA and wikimedia rights are not shown because they are already known for all images <br />
                        The search term for LOC MUST be obtained from the suggest results of LOC <br />
                    </p>
                    <form action="/" method="post">
                   <input type="text" name="message">
                   <select name="source">
                    <option>Google</option>
                    <option>Wikimedia</option> 
                    <option>LOC</option>
                    <option>NOPSA</option>
                   </select>
                   <select name="mode">
                    <option>Search</option>
                    <option>Suggest</option>
                   </select>
                   <input type="submit" value="Submit">
                   </form></body></html>'''

    def get(self):
        self.write(self.html)

    def post(self):
        self.write(self.html)
        #self.set_header("Content-Type", "text/plain")
        #self.write("You wrote " + self.get_argument("message"))
        self.write("Term: " + self.get_argument("message"))
        self.write("<br />Source: " + self.get_argument("source"))
        self.write("<br />Mode: " + self.get_argument("mode"))

        if self.get_argument("mode") == "Suggest":
            if self.get_argument("source") == "Google":
                from modules.google import Suggest
            elif self.get_argument("source") == "LOC":
                self.write("<br/><br/>API limitation, cant be implemented")
                return
            elif self.get_argument("source") == "NOPSA":
                from modules.nopsa import Suggest
            elif self.get_argument("source") == "Wikimedia":
                from modules.wikimedia import Suggest
        
            w = Suggest( self.get_argument("message") )
            for i in w.results:
                self.write("<p>" + i + "</p>")

        if self.get_argument("mode") == "Search":
            if self.get_argument("source") == "Google":
                from modules.google import Search
                w = Search( self.get_argument("message") )
                #w = Search("pakistan")
                self.write("<p>")
                for image in w.results:
                    self.write( "<br/>Image URL: " + image[0][0] )
                    self.write( "<br/>Image Source: " + image[0][3] )
                    self.write( "<br/>Image Heigth: " + image[0][1] )
                    self.write( "<br/>Image Width: " + image[0][2] )
                    self.write( "<br/>")
                self.write("</p>")

            elif self.get_argument("source") == "LOC":
                from modules.loc import Search
                w = Search( self.get_argument("message") )
                #w = Search("pakistan")
                self.write("<p>")
                for image in w.results:
                    self.write( "<br/>Image URL: (no API avaliable) " + image[2] )
                    self.write( "<br/>Image Source: " + image[0] )
                    self.write( "<br/>Image Rights: " + image[1] )
                    self.write( "<br/>")
                self.write("</p>")

            elif self.get_argument("source") == "NOPSA":
                from modules.nopsa import Search
                w = Search( self.get_argument("message") )
                #w = Search("pakistan")
                self.write("<p>")
                for image in w.results:
                    self.write( "<br/>Image URL: " + image[0] )
                    self.write( "<br/>Image Source: " + image[1] )
                    self.write( "<br/>")
                self.write("</p>")

            elif self.get_argument("source") == "Wikimedia":
                from modules.wikimedia import Search
                w = Search( self.get_argument("message") )
                #w = Search("pakistan")
                self.write("If you are searching a term, it should be obtained from suggest (of wikimedia)<p>")
                for image in w.results:
                    self.write( "<br/>Image URL: " + image )
                    self.write( "<br/>Image Source: " + "http://commons.wikimedia.org/wiki/"+ self.get_argument("message").strip().replace(" ", "_") )
                    self.write( "<br/>")
                self.write("</p>")
        
            #w = Suggest( self.get_argument("message") )
            #for i in w.results:
            #    self.write("<p>" + i + "</p>")

application = tornado.web.Application([
    (r"/", MainHandler),
])

if __name__ == "__main__":
    application.listen(8000)
    tornado.ioloop.IOLoop.instance().start()
