using SmartCityApp.Models;

public class Report
{
    public int Id { get; set; }
    public string Title { get; set; }
    public string Description { get; set; }
    public int UserId { get; set; }
    public User User { get; set; }
    public DateTime CreatedAt { get; set; }
    // Constructor without the User parameter
    public Report(string title, string description, int userId)
    {
        Title = title;
        Description = description;
        UserId = userId;
    }
}