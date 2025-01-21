using Sonae.GestaoEncomendas.MVC.Models;
using Sonae.GestaoEncomendas.MVC.Repository.Interfaces;

namespace Sonae.GestaoEncomendas.MVC.Repository{
    public class EncomendaRepository : IEncomendaRepository { 
        private readonly List<Encomenda> _encomendas = new(); 
        
        public List<Encomenda> GetTodasEncomendas(){
            return _encomendas.ToList();
        }

        public Encomenda GetEncomendaById(int id) { 
            return _encomendas.FirstOrDefault(e => e.Id == id); 
        } 
        
        public void CriarEncomenda(Encomenda encomenda) { 
            encomenda.Id = _encomendas.Count + 1; 
            _encomendas.Add(encomenda); 
        } 
        
        public void AtualizarEncomenda(Encomenda encomenda) { 
            var index = _encomendas.FindIndex(e => e.Id == encomenda.Id); 
            if (index != -1) {
                _encomendas[index] = encomenda; 
            } 
        } 
        
        public List<Encomenda> GetEncomendasPendentes() { 
            return _encomendas.Where(e => !e.Concluida).ToList(); 
        } 
    }
}