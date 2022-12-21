using Application.UseCases.Administrator.Article.Dtos;

namespace Application.UseCases.dtosGlobal.DtoEntities;

public class DtoOutputOrderHistory
{
    public int Id { get; set; }
    
    public DateTime CreationDate { get; set; }
    
    public DateTime TakenDateTime { get; set; }
    public decimal TotalOrderPrice { get; set; }

    public List<OrderHistoryContent> OrderHistoryContents { get; set; }


    public class OrderHistoryContent
    {
        public DtoOutputArticle Article { get; set; }
    
        public decimal Quantity { get; set; }
    }
}