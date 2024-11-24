using System.ComponentModel.DataAnnotations.Schema;

namespace RazorDemo.Models
{
    public class Article
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Body { get; set; }
        public int UserId { get; set; }
        public User User { get; set; }

        
    }
}
