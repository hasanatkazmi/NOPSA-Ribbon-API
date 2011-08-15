#!/usr/bin/python2.6


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



class DriveSuggest(object): 
    '''
    there is no point to save suggestions in database etc... client shouldn't call it very often.. thats the key
    but in future we can add memcache layer to make it faster
    this class is a bit generic and also has some funtionality which isn't being used by tornado. But command line access may need it in future 
    '''

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


#on first run, this will create all tables in the database
from db import *   
from binascii import crc32
#look for imported vars in db_generate.py
#db-aware
class ManageDB(object):
    SUCCESS = 900
    ALREADY_IN_DB = 901
    TYPE_MISMATCH = 902
    ALREADY_ATTATCHED_TO_EACH_OTHER = 903
    FAILURE = 904
    INCREMENT = '905'
    DECREMENET = '906'
    IMAGE_NOT_IN_DB = '907'
    TAG_NOT_IN_DB = '908'


    @staticmethod
    def if_new_insert_image_in_db(image):   #this image must conform with the image returned by each module (i.e. a dictionary of stated keys)
        '''
        a future crawler will use this funtion
        moreover, we also need to use this function for 'caching' images locally
        '''
        try:
            #print "I M MASSSSDDEE", image
            #print "I M MASSSSDDEE", image['url']
            id = crc32(image['url'])
        except:
            raise
        try:
            result = image_table.insert().execute( 
                id = id, 
                image_holder = image['contexturl'],
                image = image['url'],
                width = image['width'],
                height = image['height'],
                rights = image['rights'],
                creator = image['creator'],
                source = image['module'],
            )
            return (ManageDB.SUCCESS, id)
        except IntegrityError as e:
            #print "This image was already there in the database"
            return (ManageDB.ALREADY_IN_DB, id)

    @staticmethod
    def add_tag_to_db(tag):   #tag is text
        '''
        both change_relevancy and attatch_tag_to_image can use it internally, we don't need to call it directly
        '''
        try:
            id = crc32(tag.strip().encode("utf-8"))
        except:
            raise
        try:            
            result = tags_table.insert().execute( 
                id = id,
                tag = tag.strip().encode("utf-8"),
            )
            return (ManageDB.SUCCESS, id)
        except IntegrityError as e:
            #already there in the database , just ignore
            return (ManageDB.ALREADY_IN_DB, id)   

    @staticmethod
    def attatch_tag_to_image(tag, image, relevancy = 0, update = False): 
        '''
        (though not natural (as in human sense) but change_relevancy can be treated as wrapper of this function; you can even insert using change_relevancy function)
        '''

        def register_relevance_change(tag, image, relevance):   #tag and image should be their respective ids, relevance should be signed int        
            try:
                result = relevence_history_table.insert().execute( 
                    tagid = tag,
                    imageid = image,
                    relevancychange = int(relevancy),
                )
                return ManageDB.SUCCESS
            except:
                return ManageDB.FAILURE


        #tag if int is taken as id in tags table, if string, then its taken as actual tag
        #if tag is neither int or str / unicode it will fire an error.. make sure its either of the two
        if isinstance(tag, str) or isinstance(tag, unicode):
            #tag = result.id #modifing tag which is string to id (which corresponds to tag's id)
            status, tag = ManageDB.add_tag_to_db( tag )

        elif isinstance(tag, int):
            #check if the tag being refered by its id and if it isnt in the database then it means either client is bad guy or its his error            
            s = select( [tags_table], tags_table.c.id == tag )
            result = engine.execute(s).fetchone()
            if result == None:  #that means image isn't in the database whose id is being sent in from client - bad guy!
                return ManageDB.TAG_NOT_IN_DB
                

        if isinstance(image, dict):
            status, image = ManageDB.if_new_insert_image_in_db(image)

        elif isinstance(image, int):
            #check if the image is being refered by its id and if it isnt in the database then it means either client is bad guy or its his error            
            s = select( [image_table], image_table.c.id == image )
            result = engine.execute(s).fetchone()
            if result == None:  #that means image isn't in the database whose id is being sent in from client - bad guy!
                return ManageDB.IMAGE_NOT_IN_DB

        elif isinstance(image, str) or isinstance(image, unicode):
            return ManageDB.TYPE_MISMATCH


        s = select([relation_table], and_(relation_table.c.tagid == tag, relation_table.c.imageid == image))
        result = engine.execute(s).fetchone()

        if not result is None: #update now
            if update is False: #didnt as for updating
                return ManageDB.ALREADY_ATTATCHED_TO_EACH_OTHER
            else: #updating
                if relevancy == ManageDB.INCREMENT:
                    result = engine.execute( 
                        relation_table.update().
                            where( and_(relation_table.c.tagid == tag, relation_table.c.imageid == image) ).
                            values(relevancy=relation_table.c.relevancy+1)
                    )
                    register_relevance_change(tag, image, 1)
                elif relevancy == ManageDB.DECREMENET:
                    result = engine.execute( 
                        relation_table.update().
                            where( and_(relation_table.c.tagid == tag, relation_table.c.imageid == image) ).
                            values(relevancy=relation_table.c.relevancy-1)
                    )
                    register_relevance_change(tag, image, -1)
                else:
                    try:
                        #this is data evaluation
                        int(relevancy)                        
                    except:
                        return ManageDB.TYPE_MISMATCH

                    try:
                        if not isinstance(relevancy, unicode): raise
                        if not(relevancy[0] == "-" or relevancy[0] == "+"): raise
                    except:
                        result = engine.execute( 
                            relation_table.update().
                                where( and_(relation_table.c.tagid == tag, relation_table.c.imageid == image) ).
                                values(relevancy = relevancy )
                        )
                        register_relevance_change(tag, image, relevancy)
                        return ManageDB.SUCCESS

                    result = engine.execute( 
                        relation_table.update().
                            where( and_(relation_table.c.tagid == tag, relation_table.c.imageid == image) ).
                            values(relevancy=relation_table.c.relevancy +  int(relevancy) )
                    )        
                    register_relevance_change(tag, image, int(relevancy))

                return ManageDB.SUCCESS

        else: #insert now
            if relevancy == ManageDB.INCREMENT: relevancy = 1
            elif relevancy == ManageDB.DECREMENET: relevancy = -1
            try:
                result = relation_table.insert().execute( 
                    tagid = tag,
                    imageid = image,
                    relevancy = relevancy,
                )
                register_relevance_change(tag, image, relevancy)
                return ManageDB.SUCCESS
            except:
                return ManageDB.FAILURE

            #or if relevancy is change by number or absolute change, in either case its one call
            try:
                #this is data evaluation
                int(relevancy)
            except:
                return ManageDB.TYPE_MISMATCH

            try:
                if not isinstance(relevancy, unicode): raise
                if not(relevancy[0] == "-" or relevancy[0] == "+"): raise
            except:
                return ManageDB.TYPE_MISMATCH

            try:
                result = relation_table.insert().execute( 
                    tagid = tag,
                    imageid = image,
                    relevancy = int(relevancy),
                )
                register_relevance_change(tag, image, int(relevancy))
                return ManageDB.SUCCESS
            except:
                return ManageDB.FAILURE
            
    
    @staticmethod
    def change_relevancy(tag, image, new_relevancy):
        '''
        Client can do ALL insert operations by just calling this method 
        '''
        return ManageDB.attatch_tag_to_image(tag, image, relevancy = new_relevancy, update = True)
        

    @staticmethod
    def relevancy_history(tag, image):  #tag and image should be ids of respective
        s = select( [relevence_history_table], and_( relevence_history_table.c.tagid == tag, relevence_history_table.c.imageid == image )  )
        result = engine.execute(s).fetchall()
        relevance_change = []
        when = []
        
        for entry in result:
            relevance_change.append( entry.relevancychange )
            when.append( entry.created_at )

        return {"changes": relevance_change, "datetime": when}


    @staticmethod
    def register_image_search(images, modulename, query):  #this is list of images (image is the dictionary format )
        '''
        must be called by the search modules to register new images in the database
        images MUST be a list of images, if not, it will raise exception
        it will also add default tag of each image
        '''
        for image in images:
            #print "YESSS", image
            image["module"] = modulename
            ManageDB.if_new_insert_image_in_db(image)
            #it wont add this entry in relevany table if it already exists, so stay cool
            ManageDB.attatch_tag_to_image(query, image, relevancy=1)
    
    @staticmethod
    def search_images(query, module): #module here is module's name
        '''
        query is unicode representation of search string
        '''
        #query = query.replace(' ', '%')
        #query_to_id = crc32(query) #if query matches a tag, then tag's id would be crc32 of tag.
        #s = select([relation_table]).filter(relation_table.c.
        #result = engine.execute(s).fetchone()
        #query = query.strip()

        #we can further make the search better by referering to ontology API at NOPSA (http://nopsa.hiit.fi/pmg/index.php/ontology/index)
        #IF search isnt giving good results, incorporate this
        possible_tags = []
        exact =  query.strip().encode("utf-8")
        possible_tags += query.split() 
        possible_tags = list(set(possible_tags))
        possible_tags = [tag.encode("utf-8") for tag in possible_tags]

        #TODO should also try to fetch snonyms of query

        toreturn = []
        
        #print "crc of exact is: " , crc32(exact)
        #s = select([relation_table.c.imageid], relation_table.c.tagid == crc32(exact) ) # I forgot to filter the module out
        #s = select([relation_table.c.imageid], from_obj=[ relation_table.join( image_table, and_( relation_table.c.imageid == image_table.c.id, image_table.c.source == module, relation_table.c.tagid == crc32(exact)) )  ]  )
        #print s.__str__()
        #print crc32(exact)
        s = select([image_table], from_obj=[ relation_table.join( image_table, and_( relation_table.c.imageid == image_table.c.id, image_table.c.source == module, relation_table.c.tagid == crc32(exact)) )  ]  )
        #print s.__str__()
        #print crc32(exact)


#SELECT relation.imageid
#FROM relation
#INNER JOIN images
#ON (relation.imageid=images.id And images.source='google' And relation.tagid=-298042806)

        #print "sql: " , s.__str__()
        #print s.__str__()
        result = engine.execute(s).fetchmany(150)
        #myor = "or_(" + "".join(["image_table.c.id == " + str(image.imageid) + ", " for image in result]) + ")"
        #myor = eval(myor)
        #s = select([image_table], myor)
        #never fetch more than 150 exact images
        #print "sql: ", s.__str__()
        #result = engine.execute(s).fetchmany(150)

        #this is the algo which decides the balance b/w images
        total_exact_images = len(result)
        total_partial_images = 0
        total_images_in_one_partial_tag = 0
        if total_exact_images < 50:
            total_partial_images = 110-total_exact_images
            total_images_in_one_partial_tag = total_partial_images / len(possible_tags)



        for image in result:
            image_dict = dict(zip(image_table.columns.keys(), image))

            s = select([relation_table.c.tagid, relation_table.c.relevancy], relation_table.c.imageid == image_dict["id"] )
            result = engine.execute(s).fetchall()
            image_tags = []
            for tag in result:
                image_tag = {}
                image_tag["id"] = tag.tagid
                image_tag["relevancy"] = tag.relevancy
                s = select([tags_table.c.tag], tags_table.c.id == tag.tagid )
                resultx = engine.execute(s).fetchone()
                if resultx:
                    image_tag["tag"] = resultx.tag
                    image_tags.append(image_tag)
            
            image_dict["tags"] = image_tags
            image_dict["prirority"] = "exact"
            toreturn.append(image_dict)

        



        #open("out-"+ module +".txt", "w").write(str(toreturn))
        
        #this one is to find matches for parts of query string, e.g. if query is "hello world", it finds results for BOTH "hello" and "world"
        #pervious loop(the one for exact matching) and this loop has somwhat idendical code , when updating this loop, consult that one
        if total_images_in_one_partial_tag != 0:
            for possible_tag in possible_tags:
                #s = select([relation_table.c.imageid], relation_table.c.tagid == crc32(possible_tag) ) 
                #s = select([relation_table.c.imageid], from_obj=[ relation_table.join( image_table, and_( relation_table.c.imageid == image_table.c.id, image_table.c.source == module, relation_table.c.tagid == crc32(possible_tag)) )  ]  )
                s = select([image_table], from_obj=[ relation_table.join( image_table, and_( relation_table.c.imageid == image_table.c.id, image_table.c.source == module, relation_table.c.tagid == crc32(possible_tag)) )  ]  )
                result = engine.execute(s).fetchmany( total_images_in_one_partial_tag )
                #myor = "or_(" + "".join(["image_table.c.id == " + str(image.imageid) + ", " for image in result]) + ")"
                #myor = eval(myor)
                #s = select([image_table], myor )
                #result = engine.execute(s).fetchmany( total_images_in_one_partial_tag )

                image_tags = []
                for image in result:
                    image_dict = dict(zip(image_table.columns.keys(), image))

                    s = select([relation_table.c.tagid, relation_table.c.relevancy], relation_table.c.imageid == image_dict["id"] )
                    result = engine.execute(s).fetchall()
                    image_tags = []
                    for tag in result:
                        image_tag = {}
                        image_tag["id"] = tag.tagid
                        image_tag["relevancy"] = tag.relevancy
                        s = select([tags_table.c.tag], tags_table.c.id == tag.tagid )
                        result = engine.execute(s).fetchone()
                        image_tag["tag"] = result.tag

                        image_tags.append(image_tag)
                    
                    image_dict["tags"] = image_tags
                    image_dict["prirority"] = "partial"
                    toreturn.append(image_dict)

        return toreturn
        

    @staticmethod
    def api_key_add(comment):
        try:
            i = api_table.insert()
            i.execute(comment = comment)

            s = select( [api_table.c.api_key], api_table.c.comment == comment )
            s = s.execute()
            s = s.fetchone()
            return s.api_key
        except:
            return ManageDB.FAILURE

    @staticmethod
    def api_is_key(key):
        try:
            s = select( [api_table.c.api_key], api_table.c.api_key == key )
            s = s.execute()
            s = s.fetchone()
            if s == None: return False
            else: return True
        except:
            return ManageDB.FAILURE
    

    @staticmethod
    def api_key_delete(key):
        try:
            result = api_table.delete(api_table.c.api_key == key).execute()
            ManageDB.SUCCESS
        except:
            ManageDB.FAILURE
    

    @staticmethod
    def api_all():
        try:
            s = select( [api_table ])
            s = s.execute()
            s = s.fetchall()

            return [ {"key":row.api_key, "comment":row.comment} for row in s ]
        except:
            ManageDB.FAILURE

class DriveSearch(object): 
    '''
    '''

    counter = 0

    def __init__(self, driver, query, returntype = None, module = None): #query is the term to search for
        self.query = query
        self.w = driver
        self.module = module
        self.returntype = returntype


    def next(self):

        if self.module == None:
            try:
                module = self.w.searchModules[self.counter]
            except:
                return None
            self.counter += 1

        elif isinstance(self.module, int):
            try:
                module = self.w.searchModules[self.module]
            except:
                return None            

        else:
            module = self.module # supposing that its instance of module type now

        images = module.Search(self.query)
        #registering search to the database
        ManageDB.register_image_search( images.results , module.name, self.query)
        #now retriving from database.. using search_images funtion
        #return images.results
        results = ManageDB.search_images(self.query, module.name)
        #open("out.txt", "w").write(str(results))
        #return results
        if self.returntype == 'xml':
            return self.result_xml(module.name , self.query , results )

    def result(self):
        '''alias of next()'''
        return self.next()

    def result_xml(self, moduleName, query ,  results):

        doc = Document()
        search_tag = doc.createElement("search")
        search_tag.setAttribute("source", moduleName)
        search_tag.setAttribute("query", query)


        for image in results:
            image_tag = doc.createElement("image")     
            
            temp = doc.createElement("id")
            temp.appendChild( doc.createTextNode( str(image['id']) ) )
            image_tag.appendChild( temp )

            temp = doc.createElement("rights")
            temp.appendChild( doc.createTextNode( image['rights'] ) )
            image_tag.appendChild( temp )

            temp = doc.createElement("image_holder")
            temp.appendChild( doc.createTextNode( image['image_holder'] ) )
            image_tag.appendChild( temp )

            temp = doc.createElement("url")
            temp.appendChild( doc.createTextNode( image['image'] ) )
            image_tag.appendChild( temp )

            temp = doc.createElement("creator")
            temp.appendChild( doc.createTextNode( image['creator'] ) )
            image_tag.appendChild( temp )

            temp = doc.createElement("height")
            temp.appendChild( doc.createTextNode( str(image['height']) ) )
            image_tag.appendChild( temp )

            temp = doc.createElement("width")
            temp.appendChild( doc.createTextNode( str(image['width']) ) )
            image_tag.appendChild( temp )

            temp = doc.createElement("prirority")
            temp.appendChild( doc.createTextNode( image['prirority'] ) )
            image_tag.appendChild( temp )

            temp = doc.createElement("source")
            temp.appendChild( doc.createTextNode( image['source'] ) )
            image_tag.appendChild( temp )

            temp = doc.createElement("tags")

            for tag in image['tags']:

                temp0 = doc.createElement("tag")

                temp2 = doc.createElement("id")
                temp2.appendChild( doc.createTextNode( str(tag['id']) ) )
                temp0.appendChild(temp2)

                temp2 = doc.createElement("relevancy")
                temp2.appendChild( doc.createTextNode( str(tag['relevancy']) ) )
                temp0.appendChild(temp2)

                temp2 = doc.createElement("string")
                temp2.appendChild( doc.createTextNode( tag['tag'] ) )
                temp0.appendChild(temp2)

                temp.appendChild(temp0)

            image_tag.appendChild( temp )


            search_tag.appendChild(image_tag)

        doc.appendChild(search_tag)

        return doc.toprettyxml(indent = " ")



class DriveTags(object): 
    '''
    these drive classes are proxy classes, tornado should only call these classes, moreover, if any interpreter is made, and so should that
    '''

    def __init__(self, tag, returntype = None):
        self.tag = tag # this class doesn't care if tag is int or unicode
        self.returntype = returntype

    def create(self, image):
        #this image can be int or unicode but its expected to be int because unicode here makes no sense
        result = ManageDB.attatch_tag_to_image(self.tag, image, relevancy = 1)
        
        if self.returntype == 'xml':
            return self.result_xml( result )

        return result


    def result_xml(self, result):
        doc = Document()
        code_tag = doc.createElement("code")
        code_text = doc.createTextNode(str(result))
        code_tag.appendChild(code_text)
        doc.appendChild(code_tag)    

        return doc.toprettyxml(indent = " ")


class DriveRelevance(object): 
    '''
    these drive classes are proxy classes, tornado should only call these classes, moreover, if any interpreter is made, and so should that
    '''

    def __init__(self, tag, image,  returntype = None):
        #tag and image can be either unicode or list (as used at other places) but this class doesnt care about that
        self.tag = tag
        self.image = image
        self.returntype = returntype

    def increase(self):
        result = ManageDB.change_relevancy(self.tag, self.image, ManageDB.INCREMENT)
        
        if self.returntype == 'xml':
            return self.result_xml( result )

        return result

    def decrease(self):
        result = ManageDB.change_relevancy(self.tag, self.image, ManageDB.DECREMENET)
        
        if self.returntype == 'xml':
            return self.result_xml( result )

        return result

    def change(self, value, operation): #operation can be '+' or '-' (string) 
        result = ManageDB.change_relevancy(self.tag, self.image, operation + str(value) )
        
        if self.returntype == 'xml':
            return self.result_xml( result )

        return result

    def set(self, value): #operation can be '+' or '-' (string) 
        result = ManageDB.change_relevancy(self.tag, self.image, value)
        
        if self.returntype == 'xml':
            return self.result_xml( result )

        return result

    def giveHistory(self):  
        #CAUTION tag and image must be int ..
        return ManageDB.relevancy_history(self.tag, self.image)

    def result_xml(self, result):
        doc = Document()
        code_tag = doc.createElement("code")
        code_text = doc.createTextNode(str(result))
        code_tag.appendChild(code_text)
        doc.appendChild(code_tag)    

        return doc.toprettyxml(indent = " ")

class DriveAPIKeys(object):

    WRONG_API = "7500"

    def add(self, comment):
        return ManageDB.api_key_add(comment)

    def is_api(self, api_key):
        return ManageDB.api_is_key(api_key)

    def delete_api(self, api_key):
        return ManageDB.api_key_delete(api_key)

    def return_all_keys(self):
        return ManageDB.api_all()



if __name__ == "__main__":
    w = LoadModules()
    #print len(w.searchModules)
    #print len(w.suggestModules)
    #s = DriveSearch(w, "dear dog", returntype = 'xml', module = w.searchModules[3])
    #print s.result()
    #open("out.txt", "w").write(s.result())

    #print ManageDB.if_new_insert_image_in_db( {'width': None, 'contexturl': u'http://hdl.loc.gov/loc.pnp/cph.3c29163', 'rights': u'Publication may be restricted. For information see "New York World-Telegram ...,"', 'url': u'http://hdl.loc.gov/loc.pnp/cph.3c29163', 'creator': u'', 'height': None} )
    #print "THIS IS THE RETURNED STUFF ", test
    #print ManageDB.add_tag_to_db("i m a tag baby")
    #print "THIS IS THE RETURNED STUFF ", test
    #print ManageDB.attatch_tag_to_image(u'i m a tag babydd',1913113442)
    #print ManageDB.attatch_tag_to_image('fgh',{'width': None, 'contexturl': u'http://hdl.loc.gov/loc.pnp/cph.3c29163', 'rights': u'Publication may be restricted. For information see "New York World-Telegram ...,"', 'url': u'http://hdl.loc.gov/loc.pnp/cph.3c29163', 'creator': u'', 'height': None},update=True,relevancy = 300)
    #print "THIS IS THE RETURNED STUFF ", test
    #print ManageDB.change_relevancy('kutta hai sala',{'width': '500', 'contexturl': 'http://www.flickr.com/photos/ny156uk/817118407/', 'rights': 'Creative Commons', 'url': u'http://farm2.static.flickr.com/1224/817118407_355fe32c6a.jpg', 'creator': u'', 'module': 'google', 'height': '333'}, ManageDB.INCREMENT)
    #print ManageDB.change_relevancy('kutta hai sala',1321143460, ManageDB.INCREMENT)
    #print "OEEEEEEEEEEEEEEEe", ManageDB.api_key_add("heyyy")
    #print ManageDB.api_is_key('67158ZE0dIIWB')
    #ManageDB.api_key_delete('L58L7CLX1PIB')
    #print ManageDB.api_all()
    print ManageDB.relevancy_history("1994928747", "551117815")



