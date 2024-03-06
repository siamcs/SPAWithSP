namespace Practice888.Vm
{
    public class VmSale
    {
        public int SaleId { get; set; }
        public string? CustomerName { get; set; }
        public string? Gender { get; set; }

        public int CID { get; set; }
        public string? CatName { get; set; }

        public string[]? ProductName { get; set; }
        public string[]? Photo { get; set; }
        public DateTime[]? Date { get; set; }
        public decimal[]? Price { get; set; }
        public List<VmSaleDetail> SaleDetails { get; set; } = new List<VmSaleDetail>();
        public class VmSaleDetail
        {
            public int SaleDetailId { get; set; }
            public int SaleId { get; set; }
            public string? ProductName { get; set; }
            public string? Photo { get; set; }
            public DateTime? Date { get; set; }
            public decimal? Price { get; set; }

        }
    }
}
