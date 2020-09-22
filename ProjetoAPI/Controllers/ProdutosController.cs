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
        [HttpGet, Route("api/produtos/GetProdutoId/{id}")]
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

        [Authorize(Roles = "Admin, User")]
        [HttpGet, Route("api/produtos/ProdutoNome/{nome}")]
        [ResponseType(typeof(Produto))]
        public IHttpActionResult ProdutoNome(string nome)
        {
            Produto produto = db.Produtos.SingleOrDefault(m => m.Nome == nome);


            if (produto == null)
            {
                return NotFound();
            }

            return Ok(produto);
        }

        [Authorize(Roles = "Admin")]
        [Authorize(Roles = "Ativo")]
        [HttpPut, Route("api/produtos/PutProduto/{id}")]
        [ResponseType(typeof(void))]
        public IHttpActionResult PutProduto(int id, Produto produto)
        {

            var getInfo = produto.GetId(id);
            

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != produto.Id)
            {
                return BadRequest();
            }

            produto.DataCadastro = getInfo.DataCadastro;
            produto.DataAlteracao = DateTime.Now;
            db.Entry(produto).State = EntityState.Modified;

            try
            {
                db.SaveChanges();
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
        [Authorize(Roles = "Ativo")]
        [ResponseType(typeof(Produto))]
        public IHttpActionResult PostProduto(Produto produto)
        {

            Usuario usuario = new Usuario();

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            produto.DataCadastro = DateTime.Now;

            db.Produtos.Add(produto);
            db.SaveChanges();
            return CreatedAtRoute("DefaultApi", new { id = produto.Id }, produto);

        }

        //Authorize(Roles = "Admin")]
        //[Authorize(Roles = "Ativo")]
        //[ResponseType(typeof(Produto))]
        //[HttpPost, Route("api/produtos/DeleteAnyProdutos")]
        //public IHttpActionResult DeleteMultipleProdutos([FromBody] Produto produto)
        //{
            
        //    if (produto == null)
        //    {
        //        return NotFound();
        //    }

        //    db.Produtos.Remove(produto);
        //    db.SaveChanges();

        //    return Ok(produto);
        //}

        [Authorize(Roles = "Admin")]
        [Authorize(Roles = "Ativo")]
        [ResponseType(typeof(Produto))]
        public IHttpActionResult DeleteProduto(int id)
        {
            Produto produto = db.Produtos.Find(id);
            if (produto == null)
            {
                return NotFound();
            }

            db.Produtos.Remove(produto);
            db.SaveChanges();

            return Ok(produto);
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