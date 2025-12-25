namespace Usta.Domain.Core.ProvidedServiceAgg.Dtos
{
    public class ProvidedServiceDto
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string? Description { get; set; }
        public decimal MinPrice { get; set; }
        public string? ImageUrl { get; set; }
        public int CategoryId { get; set; }
        public string CategoryName { get; set; }
    }
}