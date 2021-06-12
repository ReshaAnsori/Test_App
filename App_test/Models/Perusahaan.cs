using System;
using System.ComponentModel.DataAnnotations;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace TestApp.Models
{
    public class Perusahaan
    {
        [Key]
        public int ID { get; set; }
        [Display(Name = "Nama Perusahaan")]
        [Required]
        public string Comp_nama { get; set; }
    }
}