using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace PersonDB_project.Models
{
    public class User
    {
        [Key]
        public int Id { get; set; }

            [Display(Name = "Имя")]
            //[StringLength(50,MinimumLength =3,ErrorMessage = "имя не может быть длиннее {0} и короче {1} символов")]
        public string FirstName { get; set; }

            [Display(Name = "Фамилия")]
            //[StringLength(50,MinimumLength =3,ErrorMessage = "Фамилия не может быть длиннее {0} и короче {1} символов")]
        public string LastName { get; set; }

           // [Range(0,100,ErrorMessage = "Возраст не может быть больше {1} и меньше {0} лет ")]
            [Display(Name = "Возраст")]
        public int Age { get; set; }

        public virtual ICollection<Phone> Phones { get; set; }
        public User()
        {
            Phones = new List<Phone>();
        }

    }
}