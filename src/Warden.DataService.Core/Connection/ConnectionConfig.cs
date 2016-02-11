using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Warden.Core.Domain.Exceptions;

namespace Warden.DataService.Core.Connection
{
    public class ConnectionConfig
    {
        private MongoClient mongoClient;
        private IMongoDatabase mongoDb;
        
        
      
        public ConnectionConfig(string host = "localhost",
                               int port = 27017,
                               string databaseName = "",
                               int timeoutSec = 5)
        {
            Host = host;
            Port = port;
            DatabaseName = databaseName;
            this.mongoClient = null;
            this.mongoDb = null;
            Timeout = timeoutSec;
        }

        public string Host { get; set; }
        public string DatabaseName { get; set; }
        public int Port { get; set; }
        public int Timeout { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public IMongoDatabase Database { get { return this.mongoDb; } }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public bool isConnected()
        {
            bool connected = false;
            try
            {
                connected = this.mongoClient != null &&
                            this.mongoClient.Cluster.Description.State == MongoDB.Driver.Core.Clusters.ClusterState.Connected;
            }
            catch (TimeoutException)
            {

            }
            return connected;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="databaseName"></param>
        /// <returns></returns>
        public async Task dropDatabase(string databaseName)
        {
            try
            {
                await this.mongoClient.DropDatabaseAsync(databaseName);
            }
            catch (TimeoutException e)
            {
                throw new DatabaseConnectionException(e.Message);
            }
        }



        /// <summary>
        /// 
        /// </summary>
        public void connect()
        {
            connect(Host, Port, DatabaseName, Timeout);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="connectionString"></param>
        public bool connect(string host,
                            int port,
                            string databaseName,
                            int timeout = 5)
        {
            DatabaseName = databaseName;
            Host = host;
            Port = port;
            bool connected = false;
            var clientSettings = new MongoClientSettings
            {
                Server = new MongoServerAddress(host, port),
                ClusterConfigurator = builder =>
                {
                    builder.ConfigureCluster(settings =>
                        settings.With(serverSelectionTimeout: TimeSpan.FromSeconds(timeout)));
                }
            };

            this.mongoClient = new MongoClient(clientSettings);
            this.mongoDb = mongoClient.GetDatabase(databaseName);

            // Force an operation to detect whether connection is successful or not
            try
            {
                var databases = mongoClient.ListDatabasesAsync().Result;
                databases.MoveNextAsync();
            }
            catch (Exception)
            {
                return connected;
            }
            return isConnected();
        }
    }
}
