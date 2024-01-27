namespace MyProWeb.Models.Domain
{
    public class CartModel
    {
        public int IdProduct { get; set; }
        public string ProductName { get; set; }
        public string Image { get; set; }
        public double DonGia { get; set; }
        public int Quantity{ get; set; }
        public double Total => DonGia*Quantity;
    }
}
