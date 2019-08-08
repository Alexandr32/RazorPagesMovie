using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using RazorPagesMovie.Models;

namespace RazorPagesMovie.Pages.Movies
{
    public class IndexModel : PageModel
    {
        private readonly RazorPagesMovie.Models.RazorPagesMovieContext _context;

        public IndexModel(RazorPagesMovie.Models.RazorPagesMovieContext context)
        {
            _context = context;
        }

        /// <summary>
        /// Строка для поиска
        /// </summary>
        [BindProperty(SupportsGet = true)]
        public string SearchString { get; set; }

        /// <summary>
        /// Содержит список жанров.
        /// </summary>
        public SelectList Genres { get; set; }

        /// <summary>
        /// Содержит конкретный жанр, выбранный пользователем,
        /// </summary>
        [BindProperty(SupportsGet = true)]
        public string MovieGenre { get; set; }

        public IList<Movie> Movie { get;set; }

        public async Task OnGetAsync()
        {
            // Запрос LINQ, который извлекает все жанры из базы данных.
            IQueryable<string> genreQuery = from m in _context.Movie
                                            orderby m.Genre
                                            select m.Genre;

            var movies = from m in _context.Movie
                         select m;

            if (!string.IsNullOrEmpty(SearchString))
            {
                // Выборка по условию поиску
                movies = movies.Where(s => s.Title.Contains(SearchString));
            }

            if (!string.IsNullOrEmpty(MovieGenre))
            {
                // Выборка по условию выбранного жанра
                movies = movies.Where(x => x.Genre == MovieGenre);
            }

            // Список жанров SelectList создается путем проецирования отдельных жанров.
            Genres = new SelectList(await genreQuery.Distinct().ToListAsync());


            // Запросы LINQ не выполняются, если они определяются или изменяются
            // путем вызова метода(например, Where, Contains или OrderBy).Вместо этого
            // выполнение запроса откладывается. Это означает, что вычисление выражения
            // откладывается до тех пор, пока не будет выполнена итерация его реализованного
            // значения или не будет вызван метод ToListAsync.
            Movie = await movies.ToListAsync();
        }
    }
}
