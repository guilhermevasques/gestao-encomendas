using Sonae.GestaoEncomendas.MVC.Repository.Interfaces;
using Sonae.GestaoEncomendas.MVC.Models;

namespace Sonae.GestaoEncomendas.MVC.Services{
    public class EncomendaService : IEncomendaService { 
        private readonly IProdutoRepository _produtoRepository; 
        private readonly IEncomendaRepository _encomendaRepository; 
        
        public EncomendaService(IProdutoRepository produtoRepository, IEncomendaRepository encomendaRepository) {
        
            _produtoRepository = produtoRepository; 
            _encomendaRepository = encomendaRepository; 
            
        } 
        
        public void CriarEncomenda(int produtoId, int quantidade) { 
            
            var produto = _produtoRepository.GetProdutoById(produtoId); 
            
            if (produto == null || produto.QuantidadeEmEstoque < quantidade) 
                throw new Exception("Produto não disponível em estoque."); 
            
            var encomenda = new Encomenda { 
                                    ProdutoId = produtoId, 
                                    Quantidade = quantidade, 
                                    DataCriacao = DateTime.Now, 
                                    DataExpiracao = DateTime.Now.AddMinutes(5),
                                }; 
            produto.QuantidadeEmEstoque -= quantidade; 
            
            _produtoRepository.AtualizarProduto(produto); 
            _encomendaRepository.CriarEncomenda(encomenda); 
        }

        public List<Encomenda> GetTodasEncomendas()
        {
            return _encomendaRepository.GetTodasEncomendas();
        }
        
        public Encomenda ConsultarEncomenda(int id) { 
            
            return _encomendaRepository.GetEncomendaById(id); 

        } 
        
        public void FinalizarEncomenda(int id) { 
            
            var encomenda = _encomendaRepository.GetEncomendaById(id); 
            
            if (encomenda == null) 
                throw new Exception("Encomenda não encontrada."); 
                
            encomenda.Concluida = true; 
            _encomendaRepository.AtualizarEncomenda(encomenda); 
        } 
        
        public void VerificarEncomendasExpiradas() { 
            
            var encomendas = _encomendaRepository.GetEncomendasPendentes(); 
            
            foreach (var encomenda in encomendas) { 
                if (encomenda.DataExpiracao <= DateTime.Now && !encomenda.Concluida) { 
                    var produto = _produtoRepository.GetProdutoById(encomenda.ProdutoId); 
                    produto.QuantidadeEmEstoque += encomenda.Quantidade; 
                    _produtoRepository.AtualizarProduto(produto); 
                    encomenda.Concluida = true; 
                    _encomendaRepository.AtualizarEncomenda(encomenda); 
                } 
            } 
        }
    }
}