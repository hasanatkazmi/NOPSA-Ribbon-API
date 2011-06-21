
from sqlalchemy import schema, types
import datetime

def now():
    return datetime.datetime.now()

metadata = schema.MetaData()


tags_table = schema.Table('tags', metadata,
    schema.Column('id', types.Integer, primary_key=True, autoincrement=True),
    #schema.Column('image_id', types.Integer, nullable=False),    
    schema.Column('tag', types.Unicode(255), nullable=False),
    #schema.Column('relevancy', types.Integer, default=0),
    schema.Column('created_at', types.DateTime(), default=now()),
)

image_table = schema.Table('images', metadata,
    schema.Column('id', types.Integer, primary_key=True, autoincrement=True),
    schema.Column('image_holder', types.Unicode(255)), #web page where image is. also written as 'identifier' at some places
    schema.Column('image', types.Unicode(255), nullable=False), #absolute url of image
    schema.Column('width', types.Integer),
    schema.Column('height', types.Integer),
    schema.Column('rights', types.Unicode(255)),
    schema.Column('creator', types.Unicode(255)), 
    schema.Column('created_at', types.DateTime(), default=now()),
)

tags_table = schema.Table('relation', metadata,
    schema.Column('id', types.Integer, primary_key=True, autoincrement=True),
    schema.Column('tagid', types.Integer, nullable=False),
    schema.Column('imageid', types.Integer, nullable=False),
    schema.Column('relevancy', types.Integer),
)


from sqlalchemy.engine import create_engine

engine = create_engine('mysql+mysqldb://root:513@localhost/ribbon', echo=True)
metadata.bind = engine
metadata.create_all(checkfirst=True)

