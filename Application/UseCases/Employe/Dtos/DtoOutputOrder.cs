namespace Application.UseCases.Employe.Dtos;

public class DtoOutputOrder
{
    public string Date { get; set; }
    
    public double TotalOrderPrice { get; set; }
    
    public IEnumerable<OrderContent> OrderContents { get; set; }


    public class OrderContent
    {
        public int Id { get; set; }
    
        public Order Order { get; set; }
    
        public Article Article { get; set; }
    
        public double Quantity { get; set; }
    }
    
    public class Order
    {
        public int id { get; set; }
    
        public string creationDate { get; set; }
    
        public int iduser { get; set; }
    }

    public class Article
    {
        public int Id { get; set; }
        public string Nametag { get; set; }
        public double Price { get; set; }
        public int PricingType { get; set; }
    }
}