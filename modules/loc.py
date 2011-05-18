#!/usr/bin/python

#LOC means library of Congress

import urllib
import urllib2
from xml.dom import minidom 
from base import SuggestBase, SearchBase

#Currently not implementing suggest.. API needs to be looked into.

class Search(SearchBase):
    '''
    '''
    MAX = 10
    def __init__(self, query, images = MAX):
        '''
        query has to LOC LCDB using LRU
        http://lx2.loc.gov:210/LCDB?operation=searchRetrieve&version=1.1&query=dc.title=%22abraham%20lincoln%22%20and%20dc.resourceType=graphic&recordSchema=dc&startRecord=1&maximumRecords=10

        NOTIC: query attribute if added here will break in search funtion because urlencode will also encode spaces etc which is against SRU's methods. So query attribute is added in search seperatly
        '''
        self.args = {
            "operation" : "searchRetrieve" ,
            "version" : "1.1" ,
            "recordSchema" : "dc" ,
            "startRecord" : "1" ,
            "maximumRecords" : images ,
        }
        self.query = query
        self.results = self.search()


    def search(self):
        '''

        '''
        #url = "http://lx2.loc.gov:210/LCDB?" + urllib.urlencode(self.args) + "&query=" + "dc.title=\"" + self.query + "\" and dc.resourceType=graphic"
        url =  "http://lx2.loc.gov:210/LCDB?" + urllib.urlencode(self.args) + "&query=" + "dc.title=%22" + self.query.replace(" ","%20") + "%22%20and%20dc.resourceType=graphic"
        #url = "http://lx2.loc.gov:210/LCDB?startRecord=1&operation=searchRetrieve&version=1.1&maximumRecords=10&recordSchema=dc&query=dc.title=%22abraham%20lincoln%22%20and%20dc.resourceType=graphic"
        print url
        #search_results = urllib.urlopen(url)
        search_results = urllib2.urlopen(url)
        #print "opened"
        #print search_results.read()
        #return "adsf"	
        dom = minidom.parseString(search_results.read())
        toreturn = list()
        for i in dom.getElementsByTagName("zs:recordData"):
            toadd = []
            try:
                toadd.append( i.getElementsByTagName("identifier")[0].childNodes[0].data)
            except: 
                # if there is no identifier that means we cant retrive the image to just pass it
                #toadd.append( "" )
                continue

            try:
                toadd.append( i.getElementsByTagName("rights")[0].childNodes[0].data )
            except: toadd.append( "" )
            
            try:
                toadd.append( i.getElementsByTagName("creator")[0].childNodes[0].data )
            except: toadd.append( "" )

            toreturn.append( toadd )

            #toreturn.append( [
            #            i.getElementsByTagName("identifier")[0].childNodes[0].data ,
            #            i.getElementsByTagName("rights")[0].childNodes[0].data,
            #            i.getElementsByTagName("creator")[0].childNodes[0].data,
            #                ])

        return toreturn


if __name__ == "__main__":
    #w = Wikimedia("Pakistan International Airlines")
    #print w.results

    w = Search("abraham lincoln")
    print w.results


