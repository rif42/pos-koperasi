using System.ComponentModel.DataAnnotations;

namespace pos_koperasi.Models;

public class Barang
{
    public int Id { get; set; }
    public string? NamaBarang { get; set; }
    
    [DataType(DataType.Date)]
    public DateTime TerakhirUpdate { get; set; }
    public decimal Harga { get; set; }
    public decimal Stock { get; set; }
}