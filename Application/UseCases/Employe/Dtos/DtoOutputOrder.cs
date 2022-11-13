namespace Application.UseCases.Employe.Dtos;

public class DtoOutputOrder
{
    public int Id { get; set; }
    
    public DateTime Date { get; set; }
    
    public decimal TotalOrderPrice { get; set; }
    
    public List<OrderContent> OrderContents { get; set; }


    public class OrderContent
    {
        public Article Article { get; set; }
    
        public decimal Quantity { get; set; }
    }

    public class Article
    {
        public int Id { get; set; }
        public string Nametag { get; set; }
        public decimal Price { get; set; }
        public int PricingType { get; set; }
    }
}