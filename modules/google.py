#!/usr/bin/python

import urllib
import simplejson
from xml.dom import minidom 

from base import SuggestBase, SearchBase

class Search(SearchBase):
    '''
    For Search Only. No suggestions
    '''
    MAX = 80
    cursor = 0
    def __init__(self, query, images = MAX):
        self.args = {
            "q" : query ,
            "as_rights" : "(cc_publicdomain|cc_attribute|cc_sharealike|cc_noncommercial|cc_nonderived)" ,
            "rsz" : "8" ,
        }
        self.search()
       

    def search(self):
        results = []
        for page in range(0, self.MAX, 8):
            self.args["start"]=str(page)
            url = "http://ajax.googleapis.com/ajax/services/search/images?v=1.0&" + urllib.urlencode(self.args)
            pageresult = self.nextpage(url)
            if pageresult == []: break
            results.append(pageresult)
        
        self.results = results        


    def nextpage(self, url):
        search_results = urllib.urlopen(url)
        json = simplejson.loads(search_results.read())
        response = json["responseData"]
        if response is None: return [] #results have ended
        results = json["responseData"]["results"]
        #print json["responseData"]["cursor"]["pages"]
        if not isinstance(image["url"], unicode): 
            image["url"] = image["url"].decode("utf-8")
        return [[ image["url"], image["height"], image["width"] ] for image in results]
        



class Suggest(SuggestBase):
    '''
    http://google.com/complete/search?output=toolbar&q=pakistan
    '''    
    
    #argument limit will be ignored but follows protocol
    def __init__(self, query, limit = "20"):
        self.args = {
            "q" : query ,
            "output" : "toolbar" ,
        }
        self.results = self.search()


    def search(self):
        url = "http://google.com/complete/search?" + urllib.urlencode(self.args)
        search_results = urllib.urlopen(url)
        dom = minidom.parseString(search_results.read())
        toreturn = list()
        for i in dom.getElementsByTagName("suggestion"):
            toreturn.append( i.getAttributeNode('data').value )

        return toreturn


if __name__ == "__main__":
    w = Suggest("pakistan")
    print w.results

