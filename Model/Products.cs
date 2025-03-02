namespace API_Server_QLBANHANG.Model
{
    public class Products
    {
        public int ProductID { get; set; }
        public string Name { get; set; }
        public int TypeId { get; set; }
        public DateTime Importday { get; set; }
        public string ImageUrl { get; set; }
        // Trường mới
        public int Price { get; set; }   // Giá gốc của sản phẩm
        public int Discount { get; set; }    // Giảm giá (%)
        public bool HotTrend { get; set; }   // Sản phẩm đang hot/trend
    }
}
