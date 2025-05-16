namespace OrderServices.DTO
{
    public class AddItemRequest
    {
        public int ProductId { get; set; }
        public bool CheckOnly { get; set; } = false;
    }
}
