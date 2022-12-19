using Application.UseCases.Administrator.Article.Dtos;

namespace Application.UseCases.dtosGlobal.DtoEntities;

public class DtoOutputOrder
{
    public int Id { get; set; }
    
    public DateTime CreationDate { get; set; }
    
    public DateTime TakeDateTime { get; set; }
    public decimal TotalOrderPrice { get; set; }
    
    public bool IsFullyPrepared { get; set; }
    
    public List<OrderContent> OrderContents { get; set; }


    public class OrderContent
    {
        public DtoOutputArticle Article { get; set; }
    
        public decimal Quantity { get; set; }
        
        public bool Prepared { get; set; }
    }
}