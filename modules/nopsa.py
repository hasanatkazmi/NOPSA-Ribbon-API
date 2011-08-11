#!/usr/bin/python2.6


import urllib
from xml.dom import minidom 
import copy

from base import SuggestBase, SearchBase

name = "nopsa"

class Search(SearchBase):
    '''
    For Search Only. No suggestions
    '''
    MAX = 80
    def __init__(self, query, images = MAX):
        #http://nopsa.hiit.fi/pmg/index.php/api/search?apikey=37461832B01D9696&tags=%2Blahore&order_attr=rank&mode=any&per_page=&page=1&encoding=html&output_type=xml&yt1=Query
        self.args = {
            u"apikey" : u"37461832B01D9696",
            u"tags" : "+"+query.encode('utf-8') , #why do we put '+' ?
            u"order_attr" : u"rank" ,
            u"mode" : u"any" ,
            u"per_page" : str(images).encode('utf-8') ,
            u"page" : u"1" ,
            u"encoding" : u"html" ,
            u"output_type" : u"xml" ,
            u"yt1" : u"Query" ,
        }
        self.results = self.search()
       

    def search(self):
        url = u"http://nopsa.hiit.fi/pmg/index.php/api/search?" + urllib.urlencode(self.args)
        search_results = urllib.urlopen(url)
        search_results = search_results.read()
        #print search_results
        dom = minidom.parseString(search_results)
        toreturn = list()
        for i in dom.getElementsByTagName("file"):
            #toreturn.append( [
            #            i.getElementsByTagName("baseName")[0].childNodes[0].data.replace("photo", "http://nopsa.hiit.fi/pmg/images/square") , #adding in this manner is a bad habbit but can't help.. no API for this avaliable.
            #            i.getElementsByTagName("originalSource")[0].childNodes[0].data
            #                ])
            my_result = copy.copy(self.result)
            my_result["url"] = i.getElementsByTagName("baseName")[0].childNodes[0].data.replace("photo", "http://nopsa.hiit.fi/pmg/images/square")  #adding in this manner is a bad habbit but can't help.. no API for this avaliable.
            my_result["contexturl"] = i.getElementsByTagName("originalSource")[0].childNodes[0].data
            my_result["rights"] = "Creative Commons"

            toreturn.append(my_result)

        return toreturn
     

class Suggest(SuggestBase):
    '''
    http://nopsa.hiit.fi/pmg/index.php/ontology/getHyponyms?lemma=coach&format=xml
    '''    
    
    #argument limit will be ignored but follows protocol
    def __init__(self, query, limit = "20"):
        self.args = {
            u"lemma" : query.encode('utf-8') ,
            u"format" : u"xml" ,
        }
        self.results = self.search()


    def search(self):
        url = u"http://nopsa.hiit.fi/pmg/index.php/ontology/getHyponyms?" + urllib.urlencode(self.args)
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

