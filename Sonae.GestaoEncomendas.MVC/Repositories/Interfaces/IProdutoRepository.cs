using Sonae.GestaoEncomendas.MVC.Models;

namespace Sonae.GestaoEncomendas.MVC.Repository.Interfaces{
    public interface IProdutoRepository { 
        Produto GetProdutoById(int id); 
        void AtualizarProduto(Produto produto); 
    }
}