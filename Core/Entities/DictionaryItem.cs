namespace Core.Entities;

/// <summary>
/// 字典项实体
/// </summary>
public class DictionaryItem
{
    public required string Id { get; set; }
    public required string Key { get; set; }
    public required string Value { get; set; } 
    public required string Description { get; set; }
}