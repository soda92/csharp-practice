using SQLite;

namespace book_manager_wpf.Models
{
    public class Book
    {
        [PrimaryKey, AutoIncrement]
        public int Id { get; set; } // Id is a value type (int), so it's non-nullable by default and initialized to 0.
        public string ISBN { get; set; } 
        public string Name { get; set; } 
        public string Author { get; set; } 
        public int Count { get; set; }

        // Parameterless constructor for SQLite-net-pcl and to satisfy CS8618
        public Book()
        {
            ISBN = string.Empty;
            Name = string.Empty;
            Author = string.Empty;
        }
    }
}