namespace Domain;

public class User
{
    public int Id { get; set; }
    public string Surame { get; set; }
    public string Lastname { get; set; }
    public string Email { get; set; }

    public DateTime BirthDate { get; set; }
    public int Permission { get; set; }
}