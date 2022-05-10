﻿using System;
using System.Threading.Tasks;
using System.Configuration;
using System.Collections.Generic;
using System.Net;
using Microsoft.Azure.Cosmos;

namespace CosmosGettingStartedTutorial
{
    class Program
    {
        // private static readonly string EndpointUri = ConfigurationManager.AppSettings["EndPointUri"];
        // private static readonly string PrimaryKey = ConfigurationManager.AppSettings["PrimaryKey"];
        // private static string databaseId = "ToDoList";
        // private static string containerId = "Items";
        private static Microsoft.Azure.Cosmos.Container _container;

        // <Main>
        public static async Task Main(string[] args)
        {
            Console.WriteLine("Hello World!");
            Program p = new Program();
            await p.connectCosmosDBAsync();
        }

        public async Task connectCosmosDBAsync()
        {
            try
            {
                var databaseID = "ToDoList";
                var containerID = "Items";
                CosmosClient cosmosClient = new CosmosClient("https://azure-cosmosdb-url","your-key", 
                                         new CosmosClientOptions()
                                         {
                                            ConnectionMode = ConnectionMode.Gateway,
                                         });
                Console.WriteLine("Beginning operations...\n");
                _container = cosmosClient.GetContainer(databaseID,containerID);
                var sqlQueryText = "SELECT c.data.status FROM c WHERE c.id = '7000xyz123'";
                // var sqlQueryText = "SELECT * FROM c WHERE c.id = '70002115'";
                QueryDefinition queryDefinition = new QueryDefinition(sqlQueryText);
                FeedIterator<Family> queryResultSetIterator = _container.GetItemQueryIterator<Family>(queryDefinition);
                List<Family> families = new List<Family>();

                while (queryResultSetIterator.HasMoreResults)
                {
                    FeedResponse<Family> currentResultSet = await queryResultSetIterator.ReadNextAsync();
                    foreach (Family family in currentResultSet)
                    {
                        families.Add(family);
                        Console.WriteLine("\tRead {0}\n", family);
                    }
            }
            }
            catch (CosmosException de)
            {
                Exception baseException = de.GetBaseException();
                Console.WriteLine("{0} error occurred: {1}", de.StatusCode, de);
            }
            catch (Exception e)
            {
                Console.WriteLine("Error: {0}", e);
            }
        }
    }
}
