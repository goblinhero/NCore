namespace NCore.Nancy
{
    public class StaticRoutes
    {
        public StaticRoutes(string baseRoute)
        {
            Post = baseRoute;
        }

        public string Get => Post + "/{id}";
        public string Post { get; }
        public string Put => Post + "/{id}";
        public string Delete => Post + "/{id}";
    }
}