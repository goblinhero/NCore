namespace NCore.Web
{
    public class StaticRoutes: BaseRoutes
    {
        public StaticRoutes(string baseRoute, string routePrefix = null) 
            : base(baseRoute, routePrefix)
        {
        }

        public string Get => Base + "/{id}";
        public string Post => Base;
        public string Put => Base + "/{id}";
        public string Delete => Base + "/{id}";
    }
}