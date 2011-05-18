#!/usr/bin/python

import urllib
from xml.dom import minidom 

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
        self.results = self.search()
       

    def search(self):
        url = "http://nopsa.hiit.fi/index.php/api/search?" + urllib.urlencode(self.args)
        search_results = urllib.urlopen(url)
        dom = minidom.parseString(search_results.read())
        toreturn = list()
        for i in dom.getElementsByTagName("file"):
            toreturn.append( [
                        i.getElementsByTagName("baseName")[0].childNodes[0].data.replace("photo", "http://nopsa.hiit.fi/images/square") , #adding in this manner is a bad habbit but can't help.. no API for this avaliable.
                        i.getElementsByTagName("originalSource")[0].childNodes[0].data
                            ])

        return toreturn
     

class Suggest(SuggestBase):
    '''
    http://nopsa.hiit.fi/index.php/ontology/getHyponyms?lemma=coach&format=xml
    '''    
    
    #argument limit will be ignored but follows protocol
    def __init__(self, query, limit = "20"):
        self.args = {
            "lemma" : query ,
            "format" : "xml" ,
        }
        self.results = self.search()


    def search(self):
        url = "http://nopsa.hiit.fi/index.php/ontology/getHyponyms?" + urllib.urlencode(self.args)
        search_results = urllib.urlopen(url)
        dom = minidom.parseString(search_results.read())
        toreturn = list()
        for i in dom.getElementsByTagName("hyponym"):
            toreturn.append( i.childNodes[0].data )

        return toreturn

if __name__ == "__main__":
    w = Search("pakistan")
    print w.results
    #t = Suggest("dog")
    #print t.results

