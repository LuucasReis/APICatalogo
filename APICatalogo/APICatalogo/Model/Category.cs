using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace APICatalogo.Model
{
    public class Category
    {
        [Key]
        public int Id { get; set; }
        [Required(ErrorMessage ="{0} é obrigatório!!")]
        [Display(Name ="Nome")]
        public string Name { get; set; }
        [ValidateNever]
        [Display(Name ="Imagem")]
        public string ImageUrl { get; set; }
        [Display(Name = "Produtos")]
        public ICollection<Product> Products { get; set; }
        public Category(string name, string imageUrl)
        {
            Name = name;
            ImageUrl = imageUrl;
            Products = new List<Product>();
        }

        public Category()
        {
            Products = new List<Product>();
        }
    }
}
