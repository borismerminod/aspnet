﻿using Microsoft.AspNetCore.Mvc.Rendering;
using MVCMovie.Models;
using System.Collections.Generic;

namespace MVCMovie.Models
{
    public class MovieGenreViewModel
    {
        public List<Movie>? Movies { get; set; }
        public SelectList? Genres { get; set; }
        public string? MovieGenre { get; set; }
        public string? SearchString { get; set; }
    }

}
