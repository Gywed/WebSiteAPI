namespace Domain;

public class Article
{
    public int Id { get; set; }
    public string Nametag { get; set; }
    public decimal Price { get; set; }
    public int PricingType { get; set; }

    private int _stock;

    public int Stock
    {
        get => _stock;
        set
        {
            if (value < 0)
                throw new ArgumentException($"The stock can never be under 0");

            _stock = value;
        }
    }
    
    public Category Category { get; set; }
    public Brand Brand { get; set; }
    public string ImagePath { get; set; }
}