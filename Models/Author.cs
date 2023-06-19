namespace BookWebApp.Models
{
    public class Author
    {
        public int Id { get; set; }
        public string FullName { get; set; }

        // navigation properties

        public List<Book_author> Book_Authors { get; set; }
    }
}
