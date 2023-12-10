using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace WebApplication1.Models
{
    public class Product
    {
        [Key]
        public int Id { get; set; }
        public string Name { get; set; }

        [ForeignKey("CategoryId")]
        public int CategoryId { get; set; }  
        public Category Category { get; set; }
    }
}
