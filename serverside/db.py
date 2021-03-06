#!/usr/bin/python2.6

from sqlalchemy import schema, types
from sqlalchemy.sql import select, and_, or_, not_
from sqlalchemy.exc import * #importing all errors
from sqlalchemy.orm import sessionmaker

import datetime
import config

def now():
    return datetime.datetime.utcnow()

def random_string():
    import string
    import random
    return ''.join(random.choice(string.ascii_uppercase + string.digits) for x in range(12))


metadata = schema.MetaData()


tags_table = schema.Table('tags', metadata,
    schema.Column('id', types.Integer, primary_key=True),
    #schema.Column('image_id', types.Integer, nullable=False),    
    schema.Column('tag', types.Unicode(255), nullable=False, unique=True),
    #schema.Column('relevancy', types.Integer, default=0),
    schema.Column('created_at', types.DateTime(), default=now()),
)

image_table = schema.Table('images', metadata,
    schema.Column('id', types.Integer, primary_key=True), #this would be crc32 of absolute url of the image
    schema.Column('image_holder', types.Unicode(255)), #web page where image is. also written as 'identifier' at some places
    schema.Column('image', types.Unicode(255), nullable=False), #absolute url of image
    schema.Column('width', types.Integer),
    schema.Column('height', types.Integer),
    schema.Column('rights', types.Unicode(255)),
    schema.Column('creator', types.Unicode(255)),
    schema.Column('source', types.Unicode(255)),  
    schema.Column('created_at', types.DateTime(), default=now()),
)

relation_table = schema.Table('relation', metadata,
    schema.Column('id', types.Integer, primary_key=True, autoincrement=True),
    schema.Column('tagid', types.Integer, nullable=False),
    schema.Column('imageid', types.Integer, nullable=False),
    schema.Column('relevancy', types.Integer),
)

api_table = schema.Table('api_keys', metadata,
    schema.Column('id', types.Integer, primary_key=True, autoincrement=True),
    schema.Column('api_key', types.Unicode(255), default = random_string() , primary_key=True),
    schema.Column('comment', types.Unicode(255)),
    schema.Column('created_at', types.DateTime(), default=now()),
)

#this table will grow insanely as this is dump of all relevance changes. This MUST never be called for user. Only admin should call it so that queries can be limited
relevence_history_table = schema.Table('relevence_history', metadata,
    schema.Column('tagid', types.Integer, nullable=False),
    schema.Column('imageid', types.Integer, nullable=False),
    schema.Column('relevancychange', types.Integer, nullable=False),
    schema.Column('created_at', types.DateTime(), default=now()),
)


from sqlalchemy.engine import create_engine

#this config.string comes from config.py
#engine = create_engine(config.database_string, echo=True)
engine = create_engine(config.database_string)

metadata.bind = engine
metadata.create_all(checkfirst=True)

Session = sessionmaker(bind = engine)
session = Session()


