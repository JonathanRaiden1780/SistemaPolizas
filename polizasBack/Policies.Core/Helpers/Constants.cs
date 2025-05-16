namespace Policies.Core.Helpers
{
    public static class Constants
    {
        #region StartUp
        public const string BDControl = "BDControl";
        public const string OriginsPolicy = "OriginsPolicy";
        public const string VirtualDirectory = "VirtualDirectory";
        public const string Bearer = "Bearer";
        public const string BearerSpace = "Bearer ";
        public const string ExpirationMinutes = "JwtConfig:ExpiracionMinutos";
        public const string Redirect404 = "App:404";
        public const string DefinitionName = "Authorization";
        public const string BearerFormat = "JWT";
        public const string Username = "Username";
        public const string DbSeguridad = "DbSeguridad";
        public const string Dto = "Dto";
        public const string SwaggerExtensionXml = ".xml";
        public const string Char = "3NEbI=!#%^&&**(()_++)*^%#|I1lcrL5hIdrLq9Wa9udr!tuz?5tr4frobog1&PawRAc8riM1&7!$&(r+ST*f3ITHi";

        #endregion

        #region SwaggerConfig

        public const string ServiceVersion = "v1";
        public const string Encrypt = "encrypt";
        public const string EnvironmentVariable = "environment-variable";

        #endregion

        #region Policies
        public const string Policies = "policies";
        public const string PoliciesTitle = "Policies Manager";
        public const string SwaggerPathPolicies= "../swagger/policies/swagger.json";
       
        public const string RoutePolicies = "policies/";
        public const string GetPoliciesAsync = "all";
        public const string GetPoliciesById = "by-id";
        public const string CreatePolicy = "create";
        public const string UpdatePolicy = "update";
        public const string ChangeStatus = "status";
        

        #endregion

        #region Auth
        public const string Auth = "auth";
        public const string AuthTitle = "Security Manager";
        public const string SwaggerPathAuth= "../swagger/auth/swagger.json";
       
        public const string RouteAuth = "auth/";
        public const string Login = "login";
        public const string PassChange = "change-password";
        

        #endregion

        #region Clients
        public const string Clients = "clients";
        public const string ClientsTitle = "Clients Manager";
        public const string SwaggerPathClients = "../swagger/clients/swagger.json";

        public const string RouteClients = "clients/";
        public const string GetClientsById = "by-id";
        public const string GetClientByUser = "by-user";
        public const string CreateClient = "create";


        #endregion

        #region Config api

        public const string ContentType = "application/json";

        #endregion


        #region simbolos

        public const string Slash = "/";
        public const string Plus = "+";
        public const string Equal = "=";
        public const string DoubleEqual = "==";
        public const string Punto = ".";
        public const string Coma = ",";
        public const string SlashBase64 = "_";
        public const string Base64Plus = "-";

        #endregion


        #region globasepoliciespolicies

        public const string OriginService = "Policies Service";
        public const string InternalServerError = "Internal server error";
        public const string ReferenceVirtualDirectory = "VirtualDirectory";
        public const string ReferenceRootPath = "RootPath";
        public const string Authorization = "Authorization";
        public const string SecretKey = "SecretKeyJwt";
        public const string SecurityManager = "SecurityManagerKey";
        public const string Pipe = "|";
        public const string String = "string";

        #endregion

        #region claims
        public const string Email = "email";
        public const string ExiId = "exid";
        public const string Timespan = "timespan";
        #endregion



    }
}
