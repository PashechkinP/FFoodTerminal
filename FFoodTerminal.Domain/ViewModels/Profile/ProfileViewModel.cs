﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FFoodTerminal.Domain.ViewModels.Profile
{
    public class ProfileViewModel
    {
        public long Id { get; set; }

        [Required(ErrorMessage = "Укажите возраст")]
        [Range(0, 100, ErrorMessage = "Диапазон возраста должен быть от 0 до 100")]
        public byte Age { get; set; }

        [Required(ErrorMessage = "Укажите адрес")]
        [MinLength(5, ErrorMessage = "Минимальная длина должна быть больше 5 символов")]
        [MaxLength(200, ErrorMessage = "Максимальная длина должна быть меньше 200 символов")]
        public string Address { get; set; }

        [Required(ErrorMessage = "Укажите имя")]
        public string UserName { get; set; }
    }
}
