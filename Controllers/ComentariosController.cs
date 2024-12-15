using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Sistema_de_Comentários_e_Respostas.Controllers
{
    public class ComentariosController : Controller
    {
        private ForumContext db = new ForumContext();
        public ActionResult Index()
        {
            var comentarios = db.Comentarios
                .Where(c => c.ComentarioPaiId == null)
                .OrderByDescending(c => c.DataCriacao)
                .ToList();
            return View(comentarios);
        }
        public ActionResult Detalhes(int id)
        {
            var comentario = db.Comentarios.Include("Respostas").FirstOrDefault(c => c.Id == id);
            if (comentario == null) return HttpNotFound();

            return View(comentario);
        }
        [HttpPost]
        public ActionResult Criar(Comentario comentario)
        {
            if (ModelState.IsValid)
            {
                comentario.DataCriacao = DateTime.Now;
                db.Comentarios.Add(comentario);
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(comentario);
        }
        public ActionResult Responder(int id)
        {
            ViewBag.ComentarioPaiId = id;
            return View();
        }
        [HttpPost]
        public ActionResult Responder(Comentario comentario)
        {
            if (ModelState.IsValid)
            {
                comentario.DataCriacao = DateTime.Now;
                db.Comentarios.Add(comentario);
                db.SaveChanges();
                return RedirectToAction("Detalhes", new { id = comentario.ComentarioPaiId });
            }
            return View(comentario);
        }
        public ActionResult Editar(int id)
        {
            var comentario = db.Comentarios.Find(id);
            if (comentario == null) return HttpNotFound();

            return View(comentario);
        }

        [HttpPost]
        public ActionResult Editar(Comentario comentario)
        {
            if (ModelState.IsValid)
            {
                db.Entry(comentario).State = System.Data.Entity.EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Detalhes", new { id = comentario.Id });
            }
            return View(comentario);
        }
        public ActionResult Excluir(int id)
        {
            var comentario = db.Comentarios.Find(id);
            if (comentario == null) return HttpNotFound();

            return View(comentario);
        }

        [HttpPost, ActionName("Excluir")]
        public ActionResult ExcluirConfirmado(int id)
        {
            var comentario = db.Comentarios.Include("Respostas").FirstOrDefault(c => c.Id == id);
            if (comentario != null)
            {
                db.Comentarios.Remove(comentario);
                db.SaveChanges();
            }
            return RedirectToAction("Index");
        }
    }
}