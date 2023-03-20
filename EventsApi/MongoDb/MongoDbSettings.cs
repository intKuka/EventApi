namespace EventsApi.MongoDb
{
    public class MongoDbSettings
    {
        public string Host { get; set; }
        public int Port { get; set; }
        public string Username { get; set; }
        public string Password { get; set; }
        //public string ConnectionString => $"mongodb://{Host}:{Port}";
        public string ConnectionString => $"mongodb://{Username}:{Password}@{Host}:{Port}";
        

    }
}
