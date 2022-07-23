namespace Project_WebApi.DTO
{
    public class ProductDTO
    {
        public int productId { get; set; }
        public string productName { get; set; }
        public float productPrice { get; set; }
        public string productImage { get; set; }

        public string Details { get; set; }
        public int productDiscount { get; set; }
        public int categoryId { get; set; }

        public string productCategory { get; set; }
    }
}
