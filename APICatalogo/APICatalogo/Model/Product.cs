using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace APICatalogo.Model
{
    public class Product
    {
        [Key]
        public int Id { get; set; }
        [Required(ErrorMessage = "{0} é obrigatório!!")]
        [Display(Name ="Nome")]
        public string Name { get; set; }
        [Required(ErrorMessage = "{0} é obrigatório!!")]
        [Display(Name ="Descrição")]
        public string Description { get; set; }
        [Required(ErrorMessage = "{0} é obrigatório!!")]
        [Display(Name ="Preço")]
        public double Price { get; set; }
        [ValidateNever]
        [Display(Name ="Imagem")]
        public string ImageUrl { get; set; }
        [Required(ErrorMessage = "{0} é obrigatório!!")]
        [Display(Name ="Estoque")]
        public float Quantity { get; set; }
        [Display(Name ="Data Cadastro")]
        [DataType(DataType.Date)]
        public DateTime CreationDate { get; set; }
        [Required(ErrorMessage = "{0} é obrigatório!!")]
        [Display(Name ="Categoria")]
        public int CategoryId { get; set; }
        [ForeignKey("CategoryId")]
        [JsonIgnore]
        public Category Category { get; set; }

        public Product()
        {
        }

        public Product(string name, string description, double price, string imageUrl, float quantity, Category category)
        {
            Name = name;
            Description = description;
            Price = price;
            ImageUrl = imageUrl;
            Quantity = quantity;
            CreationDate = DateTime.Now;
            Category = category;
        }
    }
}
