using System.Text.Json.Serialization;

namespace BookWebApp.Models
{
    public class Author
    {
        public int Id { get; set; }
        public string FullName { get; set; }
        public string ImageURL { get; set; }

        // navigation properties
        [JsonIgnore]
        public List<Book_author> Book_Authors { get; set; }
    }
}
