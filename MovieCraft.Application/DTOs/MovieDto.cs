﻿namespace MovieCraft.Application.DTOs;

public class MovieDto
{
    public int Id { get; set; }
    public string Title { get; set; } = default!;
    public string Overview { get; set; } = default!;
    public DateTime? ReleaseDate { get; set; }
    public string PosterPath { get; set; } = default!;
}