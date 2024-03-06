using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Practice888.Models
{
    public class SaleDetail
    {
        [Key,DatabaseGenerated(DatabaseGeneratedOption.Identity)]   
        public int SaleDetailId { get; set; }
        [ForeignKey("SaleMaster")]
        public int SaleId { get; set; }
        public string? ProductName { get; set; }
        public string? Photo { get; set; }
        public DateTime? Date { get; set; }
        public decimal? Price { get; set; }
        public SaleMaster? SaleMaster { get; set; }  
    }
}
