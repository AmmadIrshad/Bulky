using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Diagnostics.CodeAnalysis;

namespace BulkyWeb.Models
{
    //table in database
    public class Category
    {
        //data annotation
        //server side validation if check in controller too
        [Key]
        public int Id { get; set; }
        [Required]
        [MaxLength(30)]
        [RegularExpression("^[a-zA-Z]+$", ErrorMessage = "Only alphabetic characters are allowed for the Name field.")]
        [DisplayName("Category Name")]
        public string? Name { get; set; }
        [DisplayName("Display Order")]
        [Range(1,100,ErrorMessage ="Display Order Must Be Between 1-100")]
        public int DisplayOrder { get; set; }
    }
}
