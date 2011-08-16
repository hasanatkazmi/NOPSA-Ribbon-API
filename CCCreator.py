#!/usr/bin/python2.6

import tornado


class CCCreator(tornado.web.RequestHandler):
    def get(self):
        try:
            content = "<license><license_url>" + self.get_argument("license_url") + "</license_url>"
            content = content +  "<license_name>" + self.get_argument("license_name") + "</license_name>"
            content = content +  "<license_button>" + self.get_argument("license_button") + "</license_button>"
            content = content +  "<deed_url>" + self.get_argument("deed_url") + "</deed_url></license>"

            self.write(  content  )

        except:
            self.write( "<error>Generic Error</error>" )

    #def post(self):



