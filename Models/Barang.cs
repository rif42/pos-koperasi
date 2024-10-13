using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace pos_koperasi.Models;

public class Barang
{
    public int Id { get; set; }

    [Display(Name = "Nama Barang")]
    public string? NamaBarang { get; set; }

    [Display(Name = "Terakhir Update")]
    [DataType(DataType.Date)]
    public DateTime TerakhirUpdate { get; set; }

    [Column(TypeName = "decimal(18, 3)")]
    public decimal Harga { get; set; }
    public decimal Stock { get; set; }
}