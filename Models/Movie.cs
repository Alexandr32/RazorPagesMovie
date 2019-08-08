using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace RazorPagesMovie.Models
{
    public class Movie
    {
        public int ID { get; set; }

        [Display(Name = "Название"),
        StringLength(60, MinimumLength = 3),
        Required]
        public string Title { get; set; }

        [Display(Name = "Дата выпуска"),
        DataType(DataType.Date), // Атрибут DataType указывает тип данных (Date). С этим атрибутом: пользователю не требуется вводить сведения о времени в поле даты.  Отображается только дата, а не время.
        DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)] // Параметр ApplyFormatInEditMode указывает, что формат должен применяться при отображении значения для редактирования
        public DateTime ReleaseDate { get; set; }

        [Display(Name = "Жанр"),
        RegularExpression(@"^[A-Z]+[a-zA-Z""'\s-]*$"),
        Required,
        StringLength(30)]
        public string Genre { get; set; }

        [Display(Name = "Цена"),
        Range(1, 100),
        DataType(DataType.Currency),
        Column(TypeName = "decimal(18, 2)")] // позволяет Entity Framework Core корректно сопоставить Price с валютой в базе данных.
        public decimal Price { get; set; }

        [Display(Name = "Рейтинг"),
        RegularExpression(@"^[A-Z]+[a-zA-Z0-9""'\s-]*$"),
        StringLength(5),
        Required]
        public string Rating { get; set; }
    }
}