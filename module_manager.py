#!/usr/bin/python

#This class was advised to me named search manager in proposal but infact its module manager.

from modules import *
import modules

class Driver(object):

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
            elif "Suggest" in dir(obj):
                self.suggestModules.append(obj)

                #temp = obj.Search("Pakistan")
                #print temp.__class__.__name__
                #print module
                #print temp.results


if __name__ == "__main__":
    w = Driver()
    
