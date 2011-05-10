#!/usr/bin/python

import urllib
import simplejson
import abc

class SuggestBase(object):
    '''
    All modules implementing suggestions should inherit this abstract class.
    This class provides a protocol for all suggestions modules to follow.
    All funtions need to be implemented by inheriting class
    '''

    '''
    this should save results of API call after it has finished. resutls should have following format:
    results is a list of unicode strings i.e. [u'one', u'two', .....]
    '''
    results = "" 

    @abc.abstractmethod
    def __init__(self, query, limit):
        '''
        query: term which should be searched
        limit: number of results to return, its mandatory to set default value for your service.
            do not expect that number of results would be equal to limit. Results returned can be less than limit
        '''
        raise NotImplementedError("Not Implemented: __init__")


class SearchBase(object):
    '''
    All modules implementing search should inherit this abstract class.
    This class provides a protocol for all search modules to follow.
    All funtions need to be implemented by inheriting class
    '''

    '''
    this should save results of API call after it has finished. resutls should have following format:
    FORMAT YET HAS TO BE DECIDED UPON
    '''
    results = "" 

    @abc.abstractmethod
    def __init__(self, query, images):
        '''
        query: term which should be searched
        limit: number of results to return, its mandatory to set default value for your service.
            do not expect that number of results would be equal to limit. Results returned can be less than limit
        '''
        raise NotImplementedError("Not Implemented: __init__")

    @abc.abstractmethod
    def search(self):
        '''
        fetching of data from foriegn source (HTTP API) should be made throught this function
        '''
        raise NotImplementedError("Not Implemented: search")



