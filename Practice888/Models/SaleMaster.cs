using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using static Practice888.Vm.VmSale;

namespace Practice888.Models
{
    [Table("SaleMasters",Schema ="gg")]
    public class SaleMaster
    {

        [Key, DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int SaleId { get; set; }
        public string? CustomerName { get; set; }
        public string? Gender { get; set; }
        [ForeignKey("Category")]
        public int CID { get; set; }
        public Category? Category { get; set; }
        public List<SaleDetail>? SaleDetails { get; set; }
    }
}
