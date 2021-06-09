using System;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;

namespace Lab3.Models
{
    public class Person
    {

        [Required]
        public string FirstName
        {
            get;
            set;
        }

        [Required]
        public string LastName
        {
            get;
            set;
        }

        [Required]
        public int Age
        {
            get;
            set;
        }

        [Required]
        public string EmailAddress
        {
            get;
            set;
        }

        [Required]
        public DateTime DateOfBirth
        {
            get;
            set;
        }

        [Required]
        public string Password
        {
            get;
            set;
        }

        [Required]
        public string Description
        {
            get;
            set;
        }
    }
}
