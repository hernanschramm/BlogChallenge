using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using WebApplication1.Models;

namespace Blog.Models
{
    public class Post
    {
        [ScaffoldColumn(false)]
        public int Id { get; set; }

        [Required(ErrorMessage = "{0} es obligatorio")]
        [MinLength(5, ErrorMessage = "{0} debe contener más o 5 caracteres")]
        [Display(Name = "Titulo")]
        public string Titulo { get; set; }

        [Required(ErrorMessage = "{0} es obligatorio")]
        [MinLength(5, ErrorMessage = "{0} debe contener más o 10 caracteres")]
        [Display(Name = "Contenido")]
        public string Contenido { get; set; }
        [MinLength(5, ErrorMessage = "{0} debe contener más o 10 caracteres")]
        [Display(Name = "Categoria")]
        public string Categoria { get; set; }
        [DataType(DataType.Date)]
        public Nullable<System.DateTime> FechaDeCreacion { get; set; }
        public byte[] Imagen { get; set; }

    }
}