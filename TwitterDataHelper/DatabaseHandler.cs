using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MongoDB.Bson;
using MongoDB.Driver;
using System.Diagnostics;
using MongoDB.Driver.Linq;

namespace TwitterDataHelper
{
    public class DatabaseHandler
    {
        private MongoClient client;
        private IMongoDatabase database;

        public DatabaseHandler(string connectionString, string databaseName)
        {
            client = new MongoClient(connectionString);
            database = client.GetDatabase(databaseName);
        }


        private async Task testMethod()
        {
            var collection = database.GetCollection<BsonDocument>("tweets");

            using (IAsyncCursor<BsonDocument> cursor = await collection.FindAsync(new BsonDocument()))
            {
                while (await cursor.MoveNextAsync())
                {
                    IEnumerable<BsonDocument> batch = cursor.Current;
                    foreach (BsonDocument document in batch)
                    {
                        Debug.WriteLine(document);
                    }
                }
            }
        }

        public int CountUsers()
        {
            var result = database.RunCommand<BsonDocument>("{distinct:'tweets',key:'user'}");
            return result.ToString().Split(',').Length-1;
        }


        public List<string> TwitterUsersThatlinkTheMost()
        {
            var match = new BsonDocument
            {
                {"$match", new BsonDocument
                    {
                        {"text",new BsonDocument
                            {
                                {
                                    "$regex","@\\w+"
                                }
                            }
                        }
                    }

                }
            };
            var group = new BsonDocument
            {
                { "$group",
                   new BsonDocument
                   {
                       { new BsonDocument
                           {
                            {
                               "_id","$user"
                            }
                           }
                       },
                       {
                           "Count", new BsonDocument
                           {
                               {
                                   "$sum", 1
                               }
                           }
                       }
                   }
                }
            };
            var sort = new BsonDocument
            {
                {
                    "$sort", new BsonDocument
                    {
                        { new BsonDocument
                            {
                                {
                                   "Count", -1
                                }
                            }
                        }
                    }
                }
            };
            var limit = new BsonDocument
            {
                {
                    "$limit", 10
     
                }
            };
            var pipeline = new[] { match,group,sort,limit };
            var collection = database.GetCollection<BsonDocument>("tweets");
            var result = collection.Aggregate<BsonDocument>(pipeline).ToList();
            List<String> finalResult = new List<string>();
            foreach( var item in result)
            {
                finalResult.Add(item["_id"].ToString());
            }
            return finalResult;
        }

        public List<string> MostAtciveUsers()
        {
            var group = new BsonDocument
            {
                { "$group",
                   new BsonDocument
                   {
                       { new BsonDocument
                           {
                            {
                               "_id","$user"
                            }
                           }
                       },
                       {
                           "Count", new BsonDocument
                           {
                               {
                                   "$sum", 1
                               }
                           }
                       }
                   }
                }
            };
            var sort = new BsonDocument
            {
                {
                    "$sort", new BsonDocument
                    {
                        { new BsonDocument
                            {
                                {
                                   "Count", -1
                                }
                            }
                        }
                    }
                }
            };
            var limit = new BsonDocument
            {
                {
                    "$limit", 10

                }
            };
            var pipeline = new[] { group, sort, limit };
            var collection = database.GetCollection<BsonDocument>("tweets");
            var result = collection.Aggregate<BsonDocument>(pipeline).ToList();
            List<String> finalResult = new List<string>();
            foreach (var item in result)
            {
                finalResult.Add(item["_id"].ToString());
            }
            return finalResult;
        }

        public List<string> UserHappyness(int happiness)
        {
            var group = new BsonDocument
            {
                { "$group",
                   new BsonDocument
                   {
                       { new BsonDocument
                           {
                            {
                               "_id","$user"
                            }
                           }
                       },
                       {"Count", new BsonDocument
                            {
                                { "$sum",1 }
                            }
                       }
                   }
                }
            };

            var match = new BsonDocument
            {
                {"$match", new BsonDocument
                    {
                        {"polarity", happiness }
                    }

                }
            };

            var sort = new BsonDocument
            {
                {
                    "$sort", new BsonDocument
                    {
                        { new BsonDocument
                            {
                                {
                                   "Count", -1
                                }
                            }
                        }
                    }
                }
            };
            var limit = new BsonDocument
            {
                {
                    "$limit", 5

                }
            };
            var pipeline = new[] { match,group, sort, limit };
            var collection = database.GetCollection<BsonDocument>("tweets");
            var result = collection.Aggregate<BsonDocument>(pipeline).ToList();
            List<String> finalResult = new List<string>();
            foreach (var item in result)
            {
                finalResult.Add(item["_id"].ToString());
            }
            return finalResult;
        }

        public void MostMentionedUsers()
        {
            var match = new BsonDocument
            {
                {"$match", new BsonDocument
                    {
                        {"text",new BsonDocument
                            {
                                {
                                    "$regex","@\\w+"
                                }
                            }
                        }
                    }

                }
            };
            var split = new BsonDocument
            {
                {"$split", new BsonDocument
                    {
                        {"text","@" }
                    }
                }
            };
            var pipeline = new[] { match, split };
            var collection = database.GetCollection<BsonDocument>("tweets");
            var result = collection.Aggregate<BsonDocument>(pipeline).ToList();
            foreach (var item in result)
            {
                Debug.WriteLine(item.ToString());
            }
        }
    }
}

