#!/usr/bin/python

#This class was advised to me named search manager in proposal but infact its module manager.

from modules import *
import modules
from xml.dom.minidom import Document

class LoadModules(object):
    '''
    this should be called only once, when the server is being starting up! 
    '''

    searchModules = list()
    suggestModules = list()

    def __init__(self):
        self.loadModules()

    def loadModules(self):
        '''
        this function searches modules in modules folder
        '''

        for module in dir(modules):
            obj = eval("modules."+module)
            if "Search" in dir(obj):
                self.searchModules.append(obj)
            if "Suggest" in dir(obj):
                self.suggestModules.append(obj)

                #print obj.name

                #temp = obj.Search("Pakistan")
                #print temp.__class__.__name__
                #print module
                #print temp.results



class DriveSuggest(object): # there is no point to save suggestions in database etc... client shouldn't call it very often.. thats the key

    counter = 0

    def __init__(self, driver, query, returntype = None, module = None): #query is the term to find suggest for
        self.query = query
        self.w = driver
        self.module = module
        self.returntype = returntype

    def next(self):   

        if self.module == None:
            try:
                module = self.w.suggestModules[self.counter]
            except:
                return None
            self.counter += 1

        elif isinstance(self.module, int):
            try:
                module = self.w.suggestModules[self.module]
            except:
                return None            

        else:
            module = self.module # supposing that its instance of module type now

        suggestions = module.Suggest(self.query)
        
        if self.returntype == 'xml':
            return self.result_xml(module.name , suggestions.results )

        return (module.name , suggestions.results )

    def result(self):
        '''alias of next()'''
        return self.next()

    def result_xml(self, moduleName, results):
        doc = Document()
        source_tag = doc.createElement("source")
        source_tag.setAttribute("name", moduleName)

        for suggestion in results:
            suggestion_tag = doc.createElement("suggestion")
            suggestion_text = doc.createTextNode(suggestion)
            suggestion_tag.appendChild(suggestion_text)
            source_tag.appendChild( suggestion_tag )

        doc.appendChild(source_tag)    

        return doc.toprettyxml(indent = " ")


if __name__ == "__main__":
    w = LoadModules()
    #print len(w.searchModules)
    #print len(w.suggestModules)
    s = DriveSuggest(w, "dog", returntype = 'xml', module = w.suggestModules[0])
    print s.result()

