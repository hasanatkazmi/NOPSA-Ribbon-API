#!/usr/bin/python

import urllib
import simplejson

class WikimediaSuggest:
    '''
    API used:
    #http://commons.wikimedia.org/w/api.php?action=opensearch&search=pakistan&limit=20
    '''
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
        '''
        query has to wikimedia article title not any search term
        '''
        self.args = {
            "action" : "query" ,
            "generator" : "images" ,
            "prop" : "imageinfo" ,
            "iiprop" : "url" ,
            "titles" : query ,
            "format" : "json" ,
        }
        self.query = query
        self.results = self.search()


    def search(self):
        '''
        uses this url:
        http://commons.wikimedia.org/w/api.php?action=query&generator=images&titles=Pakistan%20International%20Airlines&format=xml&prop=imageinfo&iiprop=url
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
        images = json["query"]["pages"].values()
        for image in images:
            toreturn["images"].append( image["imageinfo"][0]["url"] ) #we should show image["imageinfo"][0]["descriptionurl"] for credits to user
        try:
            toreturn["next"] = json["query-continue"]["images"]["gimcontinue"]
        except:
            toreturn["next"] = None

        return toreturn



w = Wikimedia("Pakistan International Airlines")
print w.results
