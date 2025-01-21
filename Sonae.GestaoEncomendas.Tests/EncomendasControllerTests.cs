using Moq;
using Xunit;
using Microsoft.AspNetCore.Mvc;
using Sonae.GestaoEncomendas.MVC.Controllers;
using Sonae.GestaoEncomendas.MVC.Models;
using Sonae.GestaoEncomendas.MVC.Services;
using System.Collections.Generic;

namespace Sonae.GestaoEncomendas.MVC.Tests
{
    public class EncomendasControllerTests
    {
        [Fact]
        public void Index_ReturnsViewWithEncomendas()
        {
            var mockService = new Mock<IEncomendaService>();
            var expectedEncomendas = new List<Encomenda> { new Encomenda { Id = 1 }, new Encomenda { Id = 2 } };
            mockService.Setup(service => service.GetTodasEncomendas()).Returns(expectedEncomendas);

            var controller = new EncomendasController(mockService.Object);

            var result = controller.Index();

            var viewResult = Assert.IsType<ViewResult>(result);
            var model = Assert.IsAssignableFrom<IEnumerable<Encomenda>>(viewResult.ViewData.Model);
            Assert.Equal(2, model.Count());
        }

        [Fact]
        public void CriarEncomenda_ValidModel_ReturnsOkResult()
        {
            var mockService = new Mock<IEncomendaService>();
            var encomendaRequest = new EncomendaRequest { ProdutoId = 1, Quantidade = 2 };

            var controller = new EncomendasController(mockService.Object);

            var result = controller.CriarEncomenda(encomendaRequest);

            var okResult = Assert.IsType<OkObjectResult>(result);
            Assert.Equal("Encomenda criada com sucesso.", okResult.Value);
            mockService.Verify(service => service.CriarEncomenda(encomendaRequest.ProdutoId, encomendaRequest.Quantidade), Times.Once);
        }

        [Fact]
        public void ConsultarEncomenda_EncomendaExists_ReturnsOkWithEncomenda()
        {
            var mockService = new Mock<IEncomendaService>();
            var encomendaId = 1;
            var expectedEncomenda = new Encomenda { Id = encomendaId };
            mockService.Setup(service => service.ConsultarEncomenda(encomendaId)).Returns(expectedEncomenda);

            var controller = new EncomendasController(mockService.Object);

            var result = controller.ConsultarEncomenda(encomendaId);

            var okResult = Assert.IsType<OkObjectResult>(result);
            var model = Assert.IsType<Encomenda>(okResult.Value);
            Assert.Equal(expectedEncomenda, model);
        }

        [Fact]
        public void ConsultarEncomenda_EncomendaDoesNotExist_ReturnsNotFound()
        {
            var mockService = new Mock<IEncomendaService>();
            var encomendaId = 1;
            mockService.Setup(service => service.ConsultarEncomenda(encomendaId)).Returns((Encomenda)null);

            var controller = new EncomendasController(mockService.Object);

            var result = controller.ConsultarEncomenda(encomendaId);

            var notFoundResult = Assert.IsType<NotFoundObjectResult>(result);
            Assert.Equal("Encomenda não encontrada.", notFoundResult.Value);
        }

        [Fact]
        public void FinalizarEncomenda_ValidRequest_ReturnsOkResult()
        {
            var mockService = new Mock<IEncomendaService>();
            var encomendaId = 1;

            var controller = new EncomendasController(mockService.Object);

            var result = controller.FinalizarEncomenda(encomendaId);

            var okResult = Assert.IsType<OkObjectResult>(result);
            Assert.Equal("Encomenda finalizada com sucesso.", okResult.Value);
            mockService.Verify(service => service.FinalizarEncomenda(encomendaId), Times.Once);
        }

        [Fact]
        public void FinalizarEncomenda_InvalidRequest_ReturnsBadRequest()
        {
            var mockService = new Mock<IEncomendaService>();
            var encomendaId = 1;
            mockService.Setup(service => service.FinalizarEncomenda(encomendaId)).Throws(new Exception("Erro ao finalizar encomenda."));

            var controller = new EncomendasController(mockService.Object);

            var result = controller.FinalizarEncomenda(encomendaId);

            var badRequestResult = Assert.IsType<BadRequestObjectResult>(result);
            Assert.Equal("Erro ao finalizar encomenda.", badRequestResult.Value);
        }
    }
}
