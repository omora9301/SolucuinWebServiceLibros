using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Services;
using WebServiceLibros.Models.EF;
using System.Data.SqlClient;
using System.ServiceModel;

namespace WebServiceLibros.Models
{
    /// <summary>
    /// Descripción breve de WebServiceLibros
    /// </summary>
    [WebService(Namespace = "http://tempuri.org/")]
    [WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]
    [System.ComponentModel.ToolboxItem(false)]
    // Para permitir que se llame a este servicio web desde un script, usando ASP.NET AJAX, quite la marca de comentario de la línea siguiente. 
    // [System.Web.Script.Services.ScriptService]
    public class WebServiceLibros : System.Web.Services.WebService
    {
        LibreriaEntities2 db = new LibreriaEntities2();

        [WebMethod]
        public List<ClassLibrosExt> Obtener()
        {
            //List<Libros> ls = db.Libros.Include("Categorias").ToList();
            List<spObtener_Result> ls = db.spObtener().ToList();
            List<ClassLibrosExt> cl = new List<ClassLibrosExt>();
            foreach (spObtener_Result item in ls)
            {
                ClassLibrosExt lib = new ClassLibrosExt();
                ClassCategoriasExt ce = new ClassCategoriasExt();
                lib.Id = item.Id;
                lib.Titulo = item.Titulo;
                lib.Autor = item.Autor;
                lib.Categoria = item.Categoria;
                lib.Editorial = item.Editorial;
                lib.Edicion = item.Edicion;
                lib.ISBN = item.ISBN;

                ce.IdC = item.IdC;
                ce.NombreC = item.NombreC;
                
                lib.Categorias = ce;
                cl.Add(lib);
            }
            db.Dispose();
            return cl;
        }
        [WebMethod]
        public List<ClassCategoriasExt> ObtenerCategoria() 
        {
            List<Categorias> ls = db.Categorias.ToList();
            List<ClassCategoriasExt> cc = new List<ClassCategoriasExt>();
            foreach (Categorias item in ls)
            {
                ClassCategoriasExt ce = new ClassCategoriasExt();
                ce.IdC = item.IdC;
                ce.NombreC = item.NombreC;
                cc.Add(ce);
            }
            return cc;
        }
        [WebMethod]
        public void Agregar(ClassLibrosExt l)
        {
            Libros cle = new Libros();
            cle.Titulo = l.Titulo;
            cle.Autor = l.Autor;
            cle.Categoria = l.Categoria;
            cle.Editorial = l.Editorial;
            cle.Edicion = l.Edicion;
            cle.ISBN = l.ISBN;

            db.Libros.Add(cle);
            db.SaveChanges();
            db.Dispose();
        }
        [WebMethod]
        public ClassLibrosExt IdObtener(int id)
        {
            Libros item = db.Libros.Include("Categorias").Where(x => x.Id == id).FirstOrDefault();
            //Libros ls = db.Libros.Find(id);
            ClassLibrosExt lib = new ClassLibrosExt();
            ClassCategoriasExt ce = new ClassCategoriasExt();
            lib.Id = item.Id;
            lib.Titulo = item.Titulo;
            lib.Autor = item.Autor;
            lib.Categoria = item.Categoria;
            lib.Editorial = item.Editorial;
            lib.Edicion = item.Edicion;
            lib.ISBN = item.ISBN;

            ce.IdC = item.Categoria.Value;
            ce.NombreC = item.Categorias.NombreC;
            lib.Categorias = ce;

            db.Dispose();
            return lib;
        }
        [WebMethod]
        public void Editar(ClassLibrosExt l)
        {
            Libros li = db.Libros.Find(l.Id);
            li.Titulo = l.Titulo;
            li.Autor = l.Autor;
            li.Categoria = l.Categoria;
            li.Editorial = l.Editorial;
            li.Edicion = l.Edicion;
            li.ISBN = l.ISBN;
            db.SaveChanges();
            db.Dispose();

        }
        [WebMethod]
        public void Eliminar(int id)
        {
            Libros li = db.Libros.Where(x => x.Id == id).FirstOrDefault();
            db.Libros.Remove(li);
            db.SaveChanges();
            db.Dispose();
        }
        [WebMethod]
        public List<ClassLibrosExt> Buscar(string valor)
        {
            List<Libros> l = db.Libros.Where(x => x.Titulo.Contains(valor) || x.Autor.Contains(valor) || x.ISBN.Contains(valor)).ToList();
            List<ClassLibrosExt> ls = new List<ClassLibrosExt>();
            foreach (Libros item in l)
            {
                ClassLibrosExt lib = new ClassLibrosExt();
                ClassCategoriasExt ce = new ClassCategoriasExt();

                lib.Titulo = item.Titulo;
                lib.Autor = item.Autor;
                lib.Categoria = item.Categoria;
                lib.Editorial = item.Editorial;
                lib.Edicion = item.Edicion;
                lib.ISBN = item.ISBN;

                ce.IdC = item.Categorias.IdC;
                ce.NombreC = item.Categorias.NombreC;
                lib.Categorias = ce;

                ls.Add(lib);
            }
            db.Dispose();
            return ls;
        }
        //[WebMethod]
        //public void ValidarRepetido(Libros l)
        //{
        //    List<Libros> ls = db.Libros.Where(x => x.ISBN.Contains(l.ISBN)).ToList();
        //    db.Dispose();
        //    if (ls.Count > 0)
        //    {
        //        throw new ApplicationException($"Actualmente {l.ISBN } ya existe ");
        //    }
        //}
    }
}
