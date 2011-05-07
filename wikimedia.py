#!/usr/bin/python

import urllib
import simplejson

class WikimediaSuggest:
    def __init__(self, query, limit = "20"):
        self.args = {
            "search" : query ,
            "action" : "opensearch" ,
            "limit" : limit ,
        }
        self.query = query
        self.results = self.search()


    def search(self):
        url = "http://commons.wikimedia.org/w/api.php?" + urllib.urlencode(self.args)
        search_results = urllib.urlopen(url)
        json = simplejson.loads(search_results.read())
        response = json[1]
        return response
        

class Wikimedia:
    '''
    for each query, it fetches image file names. around 8 files a call is fetched, then for each file, it calls absolute path. So number of calls is more than number of files returned. Thus it is expensive and time consuming function. Can this be done in one call? question raised on mailing list and expecting answer shortly.
    '''
    MAX = 80
    def __init__(self, query, images = MAX):
        self.args = {
            "action" : "query" ,
            "prop" : "images" ,
            "titles" : query ,
            "format" : "json" ,
        }
        self.query = query
        self.results = self.search()


    def search(self):
        '''
        generates absolute path for each image
        '''
        images = self.get_images()
        toreturn = []
        self.args = {
            "action" : "query" ,
            "prop" : "imageinfo" ,
            "iiprop" : "url" ,
            "format" : "json" ,
        }
        
        #http://commons.wikimedia.org/w/api.php?action=query&titles=File:Pakistan%20Int%20AL%20B777-200ER%20AP-BGL.jpg&prop=imageinfo&iiprop=url&format=json
        for image in images:
            self.args["titles"] = image
            url = "http://commons.wikimedia.org/w/api.php?" + urllib.urlencode(self.args)
            search_results = urllib.urlopen(url)
            json = simplejson.loads(search_results.read())
            temp = json["query"]["pages"].values()[0]["imageinfo"][0]
            toreturn.append([temp["url"], temp["descriptionurl"]] )

        return toreturn
    
    def get_images(self):
        '''
        fetches images (not obsolute path) for the given query, query must be a string representing wikimedia page. such string can be obtained by WikimediaSuggest
        '''
        toreturn = []
        count = 0
        while count < self.MAX:
            url = "http://commons.wikimedia.org/w/api.php?" + urllib.urlencode(self.args)
            results = self.nextpage(url)
            toreturn = toreturn + results["images"]
            count += len(results["images"])
            if not results["next"] is None:
                self.args["imcontinue"] = results["next"]
            else:
                break

        return toreturn        


    def nextpage(self, url):
        toreturn = {"images" : []}
        search_results = urllib.urlopen(url)
        json = simplejson.loads(search_results.read())
        images = json["query"]["pages"].values()[0]["images"]
        for image in images:
            toreturn["images"].append( image["title"] ) 
        try:
            toreturn["next"] = json["query-continue"]["images"]["imcontinue"]
        except:
            toreturn["next"] = None

        return toreturn


#http://commons.wikimedia.org/w/api.php?action=opensearch&search=pakistan&limit=20

#http://commons.wikimedia.org/w/api.php?action=query&prop=images&titles=Pakistan%20International%20Airlines&format=xml

#http://commons.wikimedia.org/w/api.php?action=query&titles=File:Pakistan%20Int%20AL%20B777-200ER%20AP-BGL.jpg&prop=imageinfo&iiprop=url

w = Wikimedia("Pakistan International Airlines")
print w.results
