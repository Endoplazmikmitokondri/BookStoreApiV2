namespace BookStoreApiV2.Models
{
    public class Cart
{
    public int Id { get; set; }
    public int BookId { get; set; }
    public Book Book { get; set; }
    public int BuyerId { get; set; }
    public User Buyer { get; set; }
    public int Quantity { get; set; }
    public DateTime CreatedDate { get; set; }
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
