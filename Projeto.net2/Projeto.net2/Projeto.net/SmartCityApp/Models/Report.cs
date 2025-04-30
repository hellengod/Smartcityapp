using SmartCityApp.Models;  // Adicione esta linha se a classe User estiver no mesmo namespace

public class Report
{
    public int Id { get; set; }
    public string Title { get; set; }
    public string Description { get; set; }
    public int UserId { get; set; }
    public User? User { get; set; }  // A propriedade User foi corrigida para permitir que seja null
    public DateTime CreatedAt { get; set; }

    // Construtor sem a necessidade de passar User
    public Report(string title, string description, int userId)
    {
        Title = title;
        Description = description;
        UserId = userId;
    }
}
