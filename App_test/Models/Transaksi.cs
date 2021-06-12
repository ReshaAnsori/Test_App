using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace TestApp.Models
{
    public class Transaksi
    {
        [Key]
        public int ID { get; set; }
        [NotMapped]
        [Required]
        public int Brg_ID { get; set; }
        [NotMapped]
        [Required]
        public int Comp_ID { get; set; }

        [Display(Name = "Barang")]
        [NotMapped]
        public string Brg_nama { get; set; }

        [Display(Name = "Perusahaan")]
        [NotMapped]
        public string Comp_nama { get; set; }

        [Display(Name = "Kuantitas")]
        [Required]
        public int Qty { get; set; }
        [Display(Name = "Total Harga")]
        public decimal total { get; set; }

        public Barang Brg { get; set; }
        public Perusahaan Comp { get; set; }
    }
}