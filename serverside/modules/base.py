#!/usr/bin/python2.6


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
    results = []

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
    whereas each member of resutls MUST be a result type (result is a dictionary with default items and their values, result is defined below
    '''
    results = [] 

    '''
    create a soft copy of result and CRUD on it and add in results, look at some current implementation for example
    '''
    result = {
            'url'  :   u''  , #url of the image (absolute url, no html encoding, utf-d)
            'height'  :   None  ,
            'width'  :   None  ,
            'contexturl'  :   u''  , #page where image is located
            'rights'  :   u''  , #known copy rights on the image
            'creator'  :   u''  , #creator / photographer of the image
                }

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



