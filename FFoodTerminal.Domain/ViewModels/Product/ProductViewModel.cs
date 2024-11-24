using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FFoodTerminal.Domain.ViewModels.Product
{
    public class ProductViewModel // используется как посредник, если надо заполнять не все свойства например
    {
        public int Id { get; set; }

        [Display(Name = "Название")]
        [Required(ErrorMessage = "Введите название")]
        [MinLength(2, ErrorMessage = "Минимальная длина должна быть больше двух символов")]
        public string Name { get; set; }

        [Display(Name = "Описание")]
        public string Description { get; set; }

        [Display(Name = "Категория")]
        [Required(ErrorMessage = "Укажите категорию")]
        [MinLength(2, ErrorMessage = "Минимальная длина должна быть больше двух символов")]
        public string Category { get; set; }

        [Display(Name = "Макс. скорость")]
        public int Speed { get; set; }

        [Display(Name = "Разгон")]
        public float Acceleration { get; set; }

        [Display(Name = "Расход топлива")]
        public float FuelConsumption { get; set; }

        [Display(Name = "Дорожный просвет")]
        public int Clearance { get; set; }

        [Display(Name = "Стоимость")]
        [Required(ErrorMessage = "Укажите стоимость")]
        public decimal Price { get; set; }

        public IFormFile Avatar { get; set; }

        public byte[]? Image { get; set; }
    }
}
