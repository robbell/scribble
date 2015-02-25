namespace Scribble.Web.ViewModels
{
    public class PostUrlViewModel
    {
        public int Year { get; set; }
        public int Month { get; set; }
        public string UrlTitle { get; set; }

        public string Url
        {
            get { return string.Format("{0}/{1:00}/{2}", Year, Month, UrlTitle); }
        }
    }
}