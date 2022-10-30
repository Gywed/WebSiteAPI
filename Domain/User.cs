namespace Domain;

public class User
{
    public int Id { get; set; }
    public string Surame { get; set; }
    public string Lastname { get; set; }
    public string Email { get; set; }

    private int _age;

    public int Age
    {
        get=>_age;
        set
        {
            if (value < 1)
                throw new ArgumentException($"Age cannot be under 1");
            _age = value;
        }
    }
    public int Permission { get; set; }
}