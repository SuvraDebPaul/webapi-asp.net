
public class Category
{
    public Guid CategoryId{get;set;}
    public required string CategoryName{get;set;}
    public string? CategoryDescription{get;set;} = string.Empty;
    public DateTime CreatedAt{get;set;}
}
