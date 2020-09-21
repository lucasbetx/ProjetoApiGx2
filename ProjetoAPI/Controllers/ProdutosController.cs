using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web.Http.Description;
using ProjetoAPI.Models;
using ProjetoAPI.Models.Context;
using ProjetoAPI.Models.Entities;

namespace ProjetoAPI.Controllers
{
    public class ProdutosController : ApiController
    {
        private BancoContext db = new BancoContext();

        Usuario user = new Usuario();

        [Authorize(Roles = "Admin, User")]
        public IQueryable<Produto> GetProdutos()
        {
            return db.Produtos;
        }

        [Authorize(Roles = "Admin, User")]
        [ResponseType(typeof(Produto))]
        public IHttpActionResult GetProdutoId(int id)
        {
            Produto produto = db.Produtos.Find(id);
            if (produto == null)
            {
                return NotFound();
            }

            return Ok(produto);
        }

        ////[Authorize(Roles = "Admin")]
        //[HttpGet, Route("api/produtos/{nome?}")]
        //[ResponseType(typeof(Produto))]
        //public IHttpActionResult GetProdutoNome(string nome)
        //{
        //    Produto produto = db.Produtos.SingleOrDefault(m => m.Nome == nome);


        //    if (produto == null)
        //    {
        //        return NotFound();
        //    }

        //    return Ok(produto);
        //}

        [Authorize(Roles = "Admin")]
        [Authorize(Roles = "Admin")]
        [ResponseType(typeof(void))]
        public IHttpActionResult PutProduto(int id, Produto produto)
        {

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != produto.Id)
            {
                return BadRequest();
            }


            db.Entry(produto).State = EntityState.Modified;
            produto.DataAlteracao = DateTime.Now;

            try
            {
                if (user.Ativo == true)
                {
                    db.SaveChanges();
                }
                else
                {
                    return Json("Erro, usuario inativo.");
                }
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!ProdutoExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return StatusCode(HttpStatusCode.NoContent);
        }

        [Authorize(Roles = "Admin")]
        [ResponseType(typeof(Produto))]
        public IHttpActionResult PostProduto(Produto produto)
        {

            Usuario usuario = new Usuario();

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            produto.DataCadastro = DateTime.Now;

            var teste = usuario.GetEnable();

            if (user.Ativo == true)
            {
                db.Produtos.Add(produto);
                db.SaveChanges();
                return CreatedAtRoute("DefaultApi", new { id = produto.Id }, produto);
            }
            else
            {
                return Json("Erro, usuario inativo.");

            }

        }

        [Authorize(Roles = "Admin")]
        [ResponseType(typeof(Produto))]
        public IHttpActionResult DeleteProduto(int id)
        {
            Produto produto = db.Produtos.Find(id);
            if (produto == null)
            {
                return NotFound();
            }

            if (user.Ativo)
            {
                db.Produtos.Remove(produto);
                db.SaveChanges();

                return Ok(produto);
            }
            else
            {
                return Json("Erro, usuario inativo.");
            }
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool ProdutoExists(int id)
        {
            return db.Produtos.Count(e => e.Id == id) > 0;
        }
    }
}