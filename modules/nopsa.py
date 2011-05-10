#!/usr/bin/python

import urllib
import simplejson

from base import SuggestBase, SearchBase

class Search(SearchBase):
    '''
    For Search Only. No suggestions
    '''
    MAX = 80
    def __init__(self, query, images = MAX):
        #http://nopsa.hiit.fi/index.php/api/search?apikey=37461832B01D9696&tags=%2Blahore&order_attr=rank&mode=any&per_page=&page=1&encoding=html&output_type=xml&yt1=Query
        self.args = {
            "apikey" : "37461832B01D9696",
            "tags" : "+"+query , #why do we put '+' ?
            "order_attr" : "rank" ,
            "mode" : "any" ,
            "per_page" : str(images) ,
            "page" : "1" ,
            "encoding" : "html" ,
            "output_type" : "xml" ,
            "yt1" : "Query" ,
        }
        self.search()
       

    def search(self):
        url = "http://nopsa.hiit.fi/index.php/api/search?" + urllib.urlencode(self.args)
        search_results = urllib.urlopen(url)
        self.results = search_results.read()
        #json = simplejson.loads(search_results.read())
        #response = json["responseData"]
        #if response is None: return [] #results have ended
        #results = json["responseData"]["results"]
        #print json["responseData"]["cursor"]["pages"]
        #return [[ image["url"], image["height"], image["width"] ] for image in results]
     

if __name__ == "__main__":
    w = Nopsa("pakistan")
    print w.results

