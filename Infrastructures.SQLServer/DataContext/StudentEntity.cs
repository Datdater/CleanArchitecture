﻿using System.ComponentModel.DataAnnotations;

namespace Infrastructures.SQLServer.DataContext;
public class StudentEntity
{
    public required int Id { get; set; }
    [MaxLength(50)]
    public required string Name { get; set; }
    public string? Address { get; set; }
    public string? Phone { get; set; }
}