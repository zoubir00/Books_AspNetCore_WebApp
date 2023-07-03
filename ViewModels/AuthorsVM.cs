namespace BookWebApp.ViewModels
{
    public class AuthorsVM
    {
        public int Id { get; set; }
        public string FullName { get; set; }
        public string ImageURL { get; set; }
    }
    public class AuthorwithBooksVM
    {
        public int Id { get; set; }

        public string FullName { get; set; }
        public string ImageURL { get; set; }
        public List<string> BooksTitle { get; set; }
    }
}
