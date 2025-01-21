using Microsoft.AspNetCore.Mvc;
using Sonae.GestaoEncomendas.MVC.Models;
using Sonae.GestaoEncomendas.MVC.Services;

namespace Sonae.GestaoEncomendas.MVC.Controllers{

    [ApiController] [Route("api/[controller]")] 
    public class EncomendasController : Controller { 
        private readonly IEncomendaService _encomendaService; 
        public EncomendasController(IEncomendaService encomendaService) { 
            _encomendaService = encomendaService; 
        }

        public IActionResult Index() { 
            var encomendas = _encomendaService.GetTodasEncomendas(); 
            return View(encomendas); 
        } 
        
        [HttpPost] 
        public IActionResult CriarEncomenda([FromBody] EncomendaRequest request) { 
            try { 
                _encomendaService.CriarEncomenda(request.ProdutoId, request.Quantidade); 
                return Ok("Encomenda criada com sucesso."); 
            } 
            catch (Exception ex) { 
                return BadRequest(ex.Message); 
            } 
        } 

        [HttpGet("{id}")] 
        public IActionResult ConsultarEncomenda(int id) { 
            var encomenda = _encomendaService.ConsultarEncomenda(id); 
            if (encomenda == null) 
                return NotFound("Encomenda não encontrada."); 
            
            return Ok(encomenda); 
        } 
        
        [HttpPost("{id}/finalizar")] 
        public IActionResult FinalizarEncomenda(int id) { 
            try { 
                _encomendaService.FinalizarEncomenda(id); 
                return Ok("Encomenda finalizada com sucesso."); 
            } 
            catch (Exception ex) { 
                return BadRequest(ex.Message); 
            } 
        }
    }
 }