using Application;
using Domain.Models;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Threading.Tasks;
using Moq;
using Infrastructure.Repositories;
using System.Collections.Generic;

namespace Tests
{
    [TestClass]
    public class ReservaManagerTest
    {
        private static Reserva _reserva;
        private static Reserva _reserva2;
        private static IEnumerable<Reserva> _reservas;

        [ClassInitialize]
        public static void GlobalInitilization(TestContext testContext)
        {
            _reserva = new Reserva()
            {
                ReservaID = 40,
                CheckIn = System.DateTime.Now,
                CheckOut = System.DateTime.Now,
                FechaReserva = System.DateTime.Now,
                HotelID = 3,
                UsuarioID = 8,
                Estado = EstadoReserva.Reservado
            };

            _reserva2 = new Reserva()
            {
                ReservaID = 41,
                CheckIn = System.DateTime.Now,
                CheckOut = System.DateTime.Now,
                FechaReserva = System.DateTime.Now,
                HotelID = 2,
                UsuarioID = 7,
                Estado = EstadoReserva.Reservado
            };

            _reservas = new List<Reserva> { _reserva, _reserva2 };
        }

        [TestMethod]
        public async Task GetReservasTest()
        {
            Mock<InterfaceReservaRepository> reservaRepositoryMock = new Mock<InterfaceReservaRepository>();
            reservaRepositoryMock.Setup(r => r.GetAllReservas()).ReturnsAsync(_reservas);
            ReservaManager reservaManag = new ReservaManager(reservaRepositoryMock.Object);
            IEnumerable<Reserva> reservasResultado = await reservaManag.GetAllReservas();
            Assert.AreEqual(reservasResultado, _reservas);
        }

        [TestMethod]
        public async Task GetReservaTest()
        {
            Mock<InterfaceReservaRepository> reservaRepositoryMock = new Mock<InterfaceReservaRepository>();
            reservaRepositoryMock.Setup(r => r.GetReserva(40)).ReturnsAsync(_reserva);
            ReservaManager reservaManag = new ReservaManager(reservaRepositoryMock.Object);
            Reserva reservaResultado = await reservaManag.GetReserva(40);
            Assert.AreEqual(reservaResultado, _reserva);
        }

        [TestMethod]
        public async Task InsertReservaTest()
        {
            Mock<InterfaceReservaRepository> reservaRepositoryMock = new Mock<InterfaceReservaRepository>();
            reservaRepositoryMock.Setup(r => r.InsertReserva(_reserva)).ReturnsAsync(true);
            ReservaManager reservaManag = new ReservaManager(reservaRepositoryMock.Object);
            bool reservaResultado = await reservaManag.InsertReserva(_reserva);
            Assert.IsTrue(reservaResultado);
        }

        [TestMethod]
        public async Task UpdateReservaTest()
        {
            Mock<InterfaceReservaRepository> reservaRepositoryMock = new Mock<InterfaceReservaRepository>();
            reservaRepositoryMock.Setup(r => r.UpdateReserva(_reserva)).ReturnsAsync(true);
            ReservaManager reservaManag = new ReservaManager(reservaRepositoryMock.Object);
            bool reservaResultado = await reservaManag.UpdateReserva(_reserva);
            Assert.IsTrue(reservaResultado);
        }

        [TestMethod]
        public async Task DeleteReservaTest()
        {
            Mock<InterfaceReservaRepository> reservaRepositoryMock = new Mock<InterfaceReservaRepository>();
            reservaRepositoryMock.Setup(r => r.DeleteReserva(_reserva)).ReturnsAsync(true);
            ReservaManager reservaManag = new ReservaManager(reservaRepositoryMock.Object);
            bool reservaResultado = await reservaManag.DeleteReserva(_reserva);
            Assert.IsTrue(reservaResultado);
        }

        [TestMethod]
        public async Task CancelTest()
        {
            Mock<InterfaceReservaRepository> reservaRepositoryMock = new Mock<InterfaceReservaRepository>();
            reservaRepositoryMock.Setup(reservaRepo => reservaRepo.GetReserva(40)).ReturnsAsync(_reserva);
            reservaRepositoryMock.Setup(reservaRepo => reservaRepo.UpdateReserva(_reserva)).ReturnsAsync(true);
            ReservaManager reservaManag = new ReservaManager(reservaRepositoryMock.Object);
            Reserva reservaCancelada = await reservaManag.CancelarReserva(40);
            Assert.AreEqual(reservaCancelada.Estado, EstadoReserva.Cancelado);
        }
    }
}
