using Sonae.GestaoEncomendas.MVC.Models;
using Sonae.GestaoEncomendas.MVC.Repository.Interfaces;

namespace Sonae.GestaoEncomendas.MVC.Repository{
    public class ProdutoRepository : IProdutoRepository { 
        private readonly List<Produto> _produtos = new(); 
        
        public ProdutoRepository() { 
            _produtos = new List<Produto> { 
                new Produto { 
                    Id = 1, 
                    Nome = "Meu Produto de Exemplo", 
                    QuantidadeEmEstoque = 100 
                } 
            }; 
        }

        public Produto GetProdutoById(int id) { 
            return _produtos.FirstOrDefault(p => p.Id == id); 
        }
        
        public void AtualizarProduto(Produto produto) { 
            var index = _produtos.FindIndex(p => p.Id == produto.Id); 
            
            if (index != -1) { 
                _produtos[index] = produto; 
            } 
        }
    }
}