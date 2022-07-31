using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WebServiceLibros.Models.EF
{
    public class ClassLibrosExt
    {
        public int Id { get; set; }
        public string Titulo { get; set; }
        public string Autor { get; set; }
        public Nullable<int> Categoria { get; set; }
        public string Editorial { get; set; }
        public string Edicion { get; set; }
        public string ISBN { get; set; }
        public ClassCategoriasExt Categorias { get; set; }
    }
 
}