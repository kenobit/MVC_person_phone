﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Person_Phone.Models
{
    public class PhoneViewModel
    {
        [Key]
        public int Id { get; set; }

        [Display(Name = "Номер телефона")]
        public string PhoneNumber { get; set; }

        [Display(Name = "Тип")]
        public string PhoneType { get; set; }

        public int UserId { get; set; }

        public PhoneViewModel()
        {

        }

        public PhoneViewModel(string type, string number, int userId)
        {
            this.PhoneType = type;
            this.PhoneNumber = number;
            this.UserId = userId;
        }
    }
}