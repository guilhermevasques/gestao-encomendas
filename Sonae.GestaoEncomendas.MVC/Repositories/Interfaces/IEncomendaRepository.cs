using Sonae.GestaoEncomendas.MVC.Models;

namespace Sonae.GestaoEncomendas.MVC.Repository.Interfaces{
    public interface IEncomendaRepository { 
        
        List<Encomenda> GetTodasEncomendas();
        Encomenda GetEncomendaById(int id); 
        void CriarEncomenda(Encomenda encomenda); 
        void AtualizarEncomenda(Encomenda encomenda); 

        List<Encomenda> GetEncomendasPendentes();
    }
}