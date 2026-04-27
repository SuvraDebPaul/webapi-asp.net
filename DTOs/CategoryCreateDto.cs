public class CategoryCreateDto //Data Transfer Object = DTO
{
    public required string CategoryName{get;set;}
    public string? CategoryDescription{get;set;} = string.Empty;
}