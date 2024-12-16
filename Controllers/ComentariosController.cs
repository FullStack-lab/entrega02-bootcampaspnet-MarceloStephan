using System;
using System.Collections.Generic;
using System.Data.Entity;
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
                .Include(c => c.Respostas)
                .ToList();
            return View(comentarios);
        }
        public ActionResult Detalhes(int id)
        {
            var comentario = db.Comentarios.Include("Respostas").FirstOrDefault(c => c.Id == id);
            if (comentario == null) return HttpNotFound();

            return View(comentario);
        }
        public ActionResult Criar()
        {
            return View();
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
            var comentarioPai = db.Comentarios.Find(id);
            if (comentarioPai == null)
            {
                return HttpNotFound();
            }

            var novoComentario = new Comentario
            {
                ComentarioPaiId = id
            };

            return View(novoComentario);
        }
        [HttpPost]
        public ActionResult Responder(Comentario comentario)
        {
            if (ModelState.IsValid)
            {
                comentario.DataCriacao = DateTime.Now;

                if (comentario.ComentarioPaiId.HasValue)
                {
                    var comentarioPai = db.Comentarios.Find(comentario.ComentarioPaiId.Value);
                    if (comentarioPai == null)
                    {
                        return HttpNotFound();
                    }
                }

                db.Comentarios.Add(comentario);
                db.SaveChanges();
                return RedirectToAction("Detalhes", new { id = comentario.ComentarioPaiId });
            }
            return View(comentario);
        }
        public ActionResult Editar(int id, string texto)
        {
            
            var comentario = db.Comentarios.Find(id);
            if (comentario == null)
                return HttpNotFound();

            return View(comentario);
        }

        [HttpPost]
        public ActionResult Editar(Comentario comentario)
        {
            if (ModelState.IsValid)
            {
                var comentarioExistente = db.Comentarios.AsNoTracking().FirstOrDefault(c => c.Id == comentario.Id);
                if (comentarioExistente == null)
                    return HttpNotFound();
                comentario.DataCriacao = comentarioExistente.DataCriacao;

                db.Entry(comentario).State = System.Data.Entity.EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
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
                var respostas = db.Comentarios.Where(c => c.ComentarioPaiId == id).ToList();
                foreach (var resposta in respostas)
                {
                    db.Comentarios.Remove(resposta);
                }
                db.Comentarios.Remove(comentario);
                db.SaveChanges();
            }

            return RedirectToAction("Index");
        }

    }
}