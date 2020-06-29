namespace QuoteMe.Contracts.V1
{
    public static class ApiRoutes
    {
        public const string Root = "api";
        public const string Version = "v1";
        public const string Base = Root + "/" + Version;

        public static class Clients
        {
            public const string GetAll = Base + "/clients";
            public const string Get = Base + "/clients/{clientId}";
            public const string Create = Base + "/clients";
            public const string Update = Base + "/clients";
            public const string Delete = Base + "/clients/{clientId}";
        }

    }
}