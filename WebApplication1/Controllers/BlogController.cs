using Blog.Models;
using ImTools;
using OpenXmlPowerTools;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.IO;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using WebApplication1.Models;
using WebApplication1.Repository;

namespace WebApplication1.Controllers
{
    public class BlogController : Controller
    {
        private BlogContext db = new BlogContext();
        #region Atributos 
        private PostRepository _repo;

       

        #endregion
        #region Constructor
        public BlogController()
        {
            _repo = new PostRepository();
        }
        #endregion
        #region Index       
        // GET: Blog

        public ActionResult Index(string Filtros)


        {
            //var model = _repo.TraerTodos();
            //return View(model);

            if (!string.IsNullOrEmpty(Filtros))
            {
                var query = db.blogPosts.Where(o => o.Titulo.Contains(Filtros)).ToList();
                return View(query);
            }
            else
            {
                var model = _repo.TraerTodos();
                return View(model);
                //return View(db.blogPosts.Take(5).ToList());
            }






        }

        #endregion
        #region Detalles

        // GET: Blog/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            using (var db = new BlogContext())
            {
                Post post = db.blogPosts.Find(id);
                if (post == null)
                {
                    return HttpNotFound();
                }
                return View(post);
            }
        }
        #endregion
        #region C
        // GET: Blog/Create
        public ActionResult Create()
        {
            Post myNewPost = new Post(); 
            return View(myNewPost);
        }


        public ActionResult convertirImagen(int CodigoCliente)
        {
            var imagenCliente = db.blogPosts.Where(z => z.Id == CodigoCliente).FirstOrDefault();
            return File(imagenCliente.Imagen, "image/jpeg");
        }



        // POST: Blog/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(Post model, HttpPostedFileBase imagenCliente)
        {
            //HttpPostedFileBase File = Request.Files["ImageData"];
            //Byte[] imageBytes = ConvertToBytes(upload);

            if (imagenCliente != null && imagenCliente.ContentLength > 0)
            {
                byte[] imageData = null;
                using (var binaryReader = new BinaryReader(imagenCliente.InputStream))
                {
                    imageData = binaryReader.ReadBytes(imagenCliente.ContentLength);
                }
                //setear la imagen a la entidad que se creara
                model.Imagen = imageData;
            }





            try
            {
                if (ModelState.IsValid)
                {

                                  
                   

                    _repo.Alta(model);
                    return RedirectToAction("Index");
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
            return View(model);
        }

        #endregion
        #region Editar
        // GET: Blog/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.NotFound);
            }
            using (var db = new BlogContext())
            {
                Post post = db.blogPosts.Find(id);
                if (post == null)
                {
                    return HttpNotFound();
                }
                return View(post);
            }

        }


      




        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,Titulo,Contenido,Categoria,FechaDeCreacion,Estado")] Post post, HttpPostedFileBase imagenCliente)
        {


            if (imagenCliente != null && imagenCliente.ContentLength > 0)
            {
                byte[] imageData = null;
                using (var binaryReader = new BinaryReader(imagenCliente.InputStream))
                {
                    imageData = binaryReader.ReadBytes(imagenCliente.ContentLength);
                }
                //setear la imagen a la entidad que se creara
               post.Imagen = imageData;
            }

            if (ModelState.IsValid)
            {
                using (var db = new BlogContext())
                {
                    db.Entry(post).State = EntityState.Modified;
                    db.SaveChanges();
                    return RedirectToAction("Index");
                }
            }
            return View(post);
        }

        #endregion
        #region Eliminar
     // GET: Posts/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            using (var db = new BlogContext())
            {
                Post post = db.blogPosts.Find(id);
                if (post == null)
                {
                    return HttpNotFound();
                }
                return View(post);
            }
        }

        // POST: Posts/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            using (var db = new BlogContext())
            {
                Post post = db.blogPosts.Find(id);
                db.blogPosts.Remove(post);
                db.SaveChanges();
                return RedirectToAction("Index");
            }
        }

        public ActionResult ElegirEliminar(string searchString)
        {
            using (var db = new BlogContext())
            {
                var post = from d in db.blogPosts
                           select d;

                if (!String.IsNullOrEmpty(searchString))
                {
                    post = post.Where(s => s.Titulo.Contains(searchString));
                }
                
                return View(post.ToList());
            }
        }

        #endregion
        #region Categorias
        public ActionResult Economia()
        {
            using (var db = new BlogContext())
            {
                var articuloEconomia = db.blogPosts.Where(x => x.Categoria == "Economia");
 
                return View(articuloEconomia.ToList());
            }
        }

        public ActionResult Politica()
        {
            using (var db = new BlogContext())
            {
                var articuloPolitica = db.blogPosts.Where(x => x.Categoria == "Politica");
                return View(articuloPolitica.ToList());
            }
        }

        public ActionResult Deporte()
        {
            using (var db = new BlogContext())
            {
                var articuloDeporte = db.blogPosts.Where(x => x.Categoria == "Deporte");
                return View(articuloDeporte.ToList());
            }
        }

        public ActionResult Otro()
        {
            using (var db = new BlogContext())
            {
                var articuloOtro = db.blogPosts.Where(x => x.Categoria == "Otro");
                return View(articuloOtro.ToList());
            }
        }
        #endregion
        #region Listar para Edicion

        public ActionResult ElegirEdicion(string searchString)
        {
            using (var db = new BlogContext())
            {
                var post = from d in db.blogPosts
                           select d;

                if (!String.IsNullOrEmpty(searchString))
                {
                    post = post.Where(s => s.Titulo.Contains(searchString));
                    if (post.Count() == 0)
                    {
                        return Content("Titulo no encontrado");
                    }

                }

               
                return View(post.ToList());
            }
        }

        #endregion
    }
}
