using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace TestApp.Models
{
    public class Barang
    {
        [Key]
        public int ID { get; set; }
        [Display(Name = "Nama Barang")]
        [Required]
        public string Brg_nama { get; set; }
        [Display(Name = "Harga")]
        [Required]
        public decimal Brg_harga { get; set; }
    }
}
