namespace NCore.Web
{
    public class StaticRoutes
    {
        public StaticRoutes(string baseRoute)
        {
            Base = baseRoute;
        }

        public string Get => Base + "/{id}";
        public string Post => Base;
        public string Put => Base + "/{id}";
        public string Delete => Base + "/{id}";
        public string Base { get; }
    }
}