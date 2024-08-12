namespace NadinSoftProject.Models.Dto
{
    public class ProductDto
    {
        public int Id { get; set; }
        public string ProductName { get; set; } = string.Empty;
        public string ManufacturePhone { get; set; } = string.Empty;
        public string ManufactureEmail { get; set; } = string.Empty;
        public bool IsAvailable { get; set; }
    }
}
