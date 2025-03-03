﻿namespace API.DTOs;

public class CostPlanEntryDto
{
    public required string Category { get; set; }
    public required decimal Total { get; set; }
    public string Comment { get; set; } = null!;
}
