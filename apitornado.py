
import tornado.web
from module_manager import DriveAPIKeys

'''
apiadmin folder in web folder totally depends on this file 
'''

##########################################################################################################################
############# DONT FORGET TO CHANGE THE DEFAULT, THIS IS ON GITHUB!! #####################################################
##########################################################################################################################

api_granter_password = "soawesomeman"

##########################################################################################################################
##########################################################################################################################



class Newkey(tornado.web.RequestHandler):
    def get(self):
        self.write('<html><body>'
                    '<h1>Create a new key</h1>'
                   '<form action="" method="post">'
                   '<p>master password for editor: <input type="text" name="editor"></p>'
                   '<p>email or any other contact information of the person: <input type="text" name="comment"></p>'
                   '<p><input type="submit" value="Submit"></p>'
                   '</form></body></html>')

    def post(self):
        #self.set_header("Content-Type", "text/plain")
        #self.write("You wrote " + self.get_argument("message"))
        try:
            thepass = self.get_argument("editor")
            if thepass != api_granter_password: raise
        except:
            self.write('<html><body>'
                        '<h1>You are not authorized editor</h1>'
                       '<p>ssush away... !</p>'
                       '</body></html>')
            return

        comment = ""
        try:
            comment = self.get_argument("comment")
        except:
            self.write('<html><body>'
                        '<h1>Write something as identity</h1>'
                       '</body></html>')
            return

        key = DriveAPIKeys().add(comment)
        try:
            int(key) #if its error , it will be some error code which is int only
            self.write('<html><body>'
                        '<h1>Some DB error... call the police!</h1>'
                       '</body></html>')
            return
        except:            
            self.write('<html><body>'
                        '<h1>New Key Created</h1>'
                       '<p>The new Key created for the user is:<br /><b>' + key + '</b></p>'
                       '</body></html>')

class Allkeys(tornado.web.RequestHandler):
    def get(self):
        self.write('<html><body>'
                    '<h1>Auth First</h1>'
                   '<form action="" method="post">'
                   '<p>master password for editor: <input type="text" name="editor"></p>'
                   '<p><input type="submit" value="Submit"></p>'
                   '</form></body></html>')
        try:
            thepass = self.get_argument("editor")
            if thepass != api_granter_password: raise
        except:
            self.write('<html><body>'
                        '<h1>You are not authorized editor</h1>'
                       '<p>ssush away... !</p>'
                       '</body></html>')
            return

    def post(self):
        #checking for auth
        try:
            thepass = self.get_argument("editor")
            if thepass != api_granter_password: raise
        except:
            self.write('<html><body>'
                        '<h1>You are not authorized editor</h1>'
                       '<p>ssush away... !</p>'
                       '</body></html>')
            return


        #ok authorized        
        self.write('<html><body>'
                    '<h1>List of all Keys granted</h1>'
                    '<table>')

        keys = DriveAPIKeys().return_all_keys()
        self.write('<tr><td><b>Key</b></td><td><b>Comment / Contact etc</b></td><td></td></tr>')
        for key in keys:
            #self.write(key)
            self.write('<tr><td>'+key['key']+'</td><td>'+key['comment']+'</td><td><a href="delkey?key='+key['key']+'">delete</a></td></tr>')

        self.write('</table></form></body></html>')


class Deletekeys(tornado.web.RequestHandler):
    def get(self):
        delwhom = ""
        try:
            delwhom = self.get_argument("key")
        except:
            self.write('<html><body><h1>You should NOT be here... </h1></body></html>')
            return

        self.write('<html><body>'
                        '<h1>You are not authorized editor</h1>'
                       '<p>ssush away... !</p>'
                   '<h1>Auth First</h1>'
                   '<form action="" method="post">'
                   '<p>master password for editor: <input type="text" name="editor"></p>'
                   '<<input type="hidden" name="key" value="'+delwhom+'"></p>'
                   '<p><input type="submit" value="Submit"></p>'
                   '</form></body></html>')
        return

        
    def post(self):
        #checking for auth
        try:
            thepass = self.get_argument("editor")
            if thepass != api_granter_password: raise
        except:
            self.write('<html><body>'
                        '<h1>You are not authorized editor</h1>'
                       '<p>ssush away... !</p>'
                       '</body></html>')
            return


        #ok authorized
        try:
            key_to_del = self.get_argument("key")
        except:
            self.write('<html><body>'
                        '<h1> you really SHOULD NOT be here </h1>'
                        '<table>')

        r = DriveAPIKeys().delete_api(key_to_del)

        self.write('<html><body>'
                    '<h1>kel deleted</h1>'
                    '<table>')


