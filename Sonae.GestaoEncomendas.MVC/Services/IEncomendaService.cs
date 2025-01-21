using Sonae.GestaoEncomendas.MVC.Models;

namespace Sonae.GestaoEncomendas.MVC.Services{
    public interface IEncomendaService { 
        void CriarEncomenda(int produtoId, int quantidade); 
        Encomenda ConsultarEncomenda(int id); 
        void FinalizarEncomenda(int id); 
        void VerificarEncomendasExpiradas();

        List<Encomenda> GetTodasEncomendas();
    }
}