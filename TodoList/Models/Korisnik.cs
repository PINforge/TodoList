﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace TodoList.Models
{
    [Table("korisnici")]
    public class Korisnik
    {
        [Key]
        [Column("korisnicko_ime")]
        [Required]
        [Display(Name = "Korisničko ime")]
        public string KorisnickoIme { get; set; }

        [Display(Name = "Email")]
        [Required]
        [EmailAddress]
        public string Email { get; set; }

        public string Lozinka { get; set; }

        [Display(Name = "Prezime")]
        [Required]
        public string Prezime { get; set; }

        [Display(Name = "Ime")]
        [Required]
        public string Ime { get; set; }

        public string PrezimeIme
        {
            get
            {
                return Prezime + " " + Ime;
            }
        }

        [Column("ovlast")]
        [Display(Name = "Ovlast")]
        [ForeignKey("Ovlast")]
        public string SifraOvlasti { get; set; }

        public virtual Ovlast Ovlast { get; set; }

        [Display(Name = "Lozinka")]
        [DataType(DataType.Password)]
        [Required]
        [NotMapped]
        public string LozinkaUnos { get; set; }

        [Display(Name = "Ponovljena lozinka")]
        [DataType(DataType.Password)]
        [Required]
        [NotMapped]
        [Compare("LozinkaUnos", ErrorMessage = "Lozinke moraju biti jednake")]
        public string LozinkaUnos2 { get; set; }
    }
}