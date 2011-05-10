#!/usr/bin/python

import urllib
import simplejson

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
        return [[ image["url"], image["height"], image["width"] ] for image in results]
        

if __name__ == "__main__":
    w = Google("pakistan")
    print w.results

