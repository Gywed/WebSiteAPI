namespace Domain;

public class Article
{
    public int Id { get; set; }
    public string Nametag { get; set; }
    public double Price { get; set; }
    public int PricingType { get; set; }

    private int _stock;

    public int Stock
    {
        get => _stock;
        set
        {
            if (value < 1)
                throw new ArgumentException($"The stock can never be under 0");

            _stock = value;
        }
    }
}