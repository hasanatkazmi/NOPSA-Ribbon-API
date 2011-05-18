
from sqlalchemy import schema, types
import datetime

def now():
    return datetime.datetime.now()

metadata = schema.MetaData()


tags_table = schema.Table('tags', metadata,
    schema.Column('id', types.Integer, primary_key=True, autoincrement=True),
    schema.Column('tag', types.Unicode(255), nullable=False),
    schema.Column('relevancy', types.Integer, default=0),
    schema.Column('created_at', types.DateTime(), default=now()),
)

image_table = schema.Table('images', metadata,
    schema.Column('id', types.Integer, primary_key=True, autoincrement=True),
    schema.Column('image_holder', types.Unicode(255)), #web page where image is. also written as 'identifier' at some places
    schema.Column('image', types.Unicode(255), nullable=False), #absolute url of image
    schema.Column('tag_id',  types.Integer, schema.ForeignKey('tags.id'), nullable=False), #absolute url of image
    schema.Column('source_module', types.Unicode(255),  nullable=False), #which module initially created this row
    schema.Column('created_at', types.DateTime(), default=now()),
)

from sqlalchemy.engine import create_engine

engine = create_engine('mysql+mysqldb://root:513@localhost/ribbon', echo=True)
metadata.bind = engine
metadata.create_all(checkfirst=True)
