public class GetAllCategoryDto
{
    public Guid CategoryId{get;set;}
    public required string CategoryName{get;set;}
    public string? CategoryDescription{get;set;} = string.Empty;
}