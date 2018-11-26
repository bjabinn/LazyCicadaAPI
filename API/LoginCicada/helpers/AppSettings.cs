namespace LoginCicada.Helpers{
    public class AppSettings{
        public string Secret{get; set;}
        public string redisServer{get; set;}
        public int redisPort{get; set;}
        public int ttlRedis {get; set;}

        public int ttlToken {get; set;}

        public string redisPassword{get; set;}
    }
}