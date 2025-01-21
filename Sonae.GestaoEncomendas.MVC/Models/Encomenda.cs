namespace Sonae.GestaoEncomendas.MVC.Models{
    public class Encomenda { 
        public int Id { get; set; } 
        public int ProdutoId { get; set; } 
        public int Quantidade { get; set; } 
        public DateTime DataCriacao { get; set; } 
        public DateTime? DataExpiracao { get; set; } 
        public bool Concluida { get; set; } 
    }
}