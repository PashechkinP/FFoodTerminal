using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FFoodTerminal.Domain.ViewModels.Order
{
    public class CreateOrderViewModel
    {
        public int Id { get; set; }

        [Display(Name = "Количество")]
        [Range(1, 100, ErrorMessage = "Количество должно быть от 1 до 100")]
        public int Quantity { get; set; }

        [Display(Name = "Дата создания")]
        public DateTime DateCreated { get; set; }

        [Display(Name = "Адрес")]
        [Required(ErrorMessage = "Укажите адрес")]
        [MinLength(1, ErrorMessage = "Адрес должен быть больше 1 символов")]
        [MaxLength(200, ErrorMessage = "Адрес должен быть меньше 200 символов")]
        public string Address { get; set; }

        [Display(Name = "Имя")]
        [Required(ErrorMessage = "Укажите имя")]
        [MaxLength(20, ErrorMessage = "Имя должно иметь длину меньше 20 символов")]
        [MinLength(1, ErrorMessage = "Имя должно иметь длину больше 1 символов")]
        public string FirstName { get; set; }

        [Display(Name = "Фамилия")]
        [MaxLength(50, ErrorMessage = "Фамилия должно иметь длину меньше 50 символов")]
        [MinLength(1, ErrorMessage = "Фамилия должно иметь длину больше 1 символов")]
        public string LastName { get; set; }

        public long ProductEntityId { get; set; }

        public string Login { get; set; }
    }
}
