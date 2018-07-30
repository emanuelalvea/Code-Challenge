using C1B.GestionInterna.Entities;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace CodeChallenge.Entities.Models
{
    public class Contact : BaseEntity
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public string Company { get; set; }

        public string ProfileImage { get; set; }

        [EmailAddress]
        public string Email { get; set; }

        public DateTime? Birtdate { get; set; }

        public string WorkPhoneNumber { get; set; }

        public string PersonalPhoneNumber { get; set; }

        public string Address { get; set; }
    }
}
