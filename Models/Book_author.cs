namespace BookWebApp.Models
{
    public class Book_author
    {
        public int Id { get; set; }

        public int bookId { get; set; }
        public Book Book { get; set; }

        public int AuthorId { get; set; }
        public Author Author { get; set; }
    }
}
