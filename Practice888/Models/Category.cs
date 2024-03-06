using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Practice888.Models
{
    public class Category
    {
        [Key,DatabaseGenerated(DatabaseGeneratedOption.Identity)]   
        public int CID { get; set; }
        public string? CatName { get; set; }
        public IList<SaleMaster> SaleMasters { get; set;} = new List<SaleMaster>();
    }
}
