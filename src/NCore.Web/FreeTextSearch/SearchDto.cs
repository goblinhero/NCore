namespace NCore.Web.FreeTextSearch
{
    public class SearchDto
    {
        protected bool Equals(SearchDto other)
        {
            return Id == other.Id;
        }

        public override bool Equals(object obj)
        {
            if (ReferenceEquals(null, obj)) return false;
            if (ReferenceEquals(this, obj)) return true;
            if (obj.GetType() != this.GetType()) return false;
            return Equals((SearchDto) obj);
        }

        public override int GetHashCode()
        {
            return Id.GetHashCode();
        }

        public long Id { get; set; }
    }
}