namespace BookStoreApiV2.Models
{
    public class Cart
    {
        public int Id { get; set; }
        public int BookId { get; set; }
        public int UserId { get; set; }
        public Book Book { get; set; }
        public string BuyerUsername { get; set; }
        public List<CartItem> CartItems { get; set; }
    }

    public class CartItem
    {
        public int Id { get; set; }
        public int BookId { get; set; }
        public Book Book { get; set; }
        public int Quantity { get; set; }
        public int CartId { get; set; }
        public Cart Cart { get; set; }
    }
}
