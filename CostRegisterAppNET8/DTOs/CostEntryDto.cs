﻿namespace API.DTOs;

public class CostEntryDto
{
    public int Id { get; set; }
    public required string Category { get; set; }
    public required decimal Total { get; set; }
    public required DateTime Date { get; set; }
    public string Comment { get; set; } = null!;
}
