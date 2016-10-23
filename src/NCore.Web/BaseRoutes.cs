namespace NCore.Web
{
    public class BaseRoutes
    {
        public BaseRoutes(string baseRoute, string routePrefix = null)
        {
            Base = $"{routePrefix}{baseRoute}";
        }
        public string Base { get; }
    }
}