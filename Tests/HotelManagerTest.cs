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
    public class HotelManagerTest
    {
        private static Hotel _hotel;
        private static Hotel _hotel2;
        private static IEnumerable<Hotel> _hoteles;

        [ClassInitialize]
        public static void GlobalInitilization(TestContext testContext)
        {
            _hotel = new Hotel()
            {
                HotelID = 40,
                Nombre = "nombreHotelTest",
                Pais = "paisTest",
                Latitud = 1,
                Longitud = 2,
                Descripcion = "descripcionTest",
                Activo = true
            };

            _hotel2 = new Hotel()
            {
                HotelID = 41,
                Nombre = "nombreHotelTest2",
                Pais = "paisTest2",
                Latitud = 89.43,
                Longitud = 0.4533,
                Descripcion = "descripcionTest2",
                Activo = true
            };

            _hoteles = new List<Hotel> { _hotel, _hotel2 };
        }

        [TestMethod]
        public async Task GetHotelesTest()
        {
            Mock<InterfaceHotelRepository> hotelRepositoryMock = new Mock<InterfaceHotelRepository>();
            hotelRepositoryMock.Setup(r => r.GetAllHoteles()).ReturnsAsync(_hoteles);
            HotelManager hotelManag = new HotelManager(hotelRepositoryMock.Object);
            IEnumerable<Hotel> hotelesResultado = await hotelManag.GetAllHoteles();
            Assert.AreEqual(hotelesResultado, _hoteles);
        }

        [TestMethod]
        public async Task GetHotelTest()
        {
            Mock<InterfaceHotelRepository> hotelRepositoryMock = new Mock<InterfaceHotelRepository>();
            hotelRepositoryMock.Setup(r => r.GetHotel(40)).ReturnsAsync(_hotel);
            HotelManager hotelManag = new HotelManager(hotelRepositoryMock.Object);
            Hotel hotelResultado = await hotelManag.GetHotel(40);
            Assert.AreEqual(hotelResultado, _hotel);
        }

        [TestMethod]
        public async Task InsertHotelTest()
        {
            Mock<InterfaceHotelRepository> hotelRepositoryMock = new Mock<InterfaceHotelRepository>();
            hotelRepositoryMock.Setup(r => r.InsertHotel(_hotel)).ReturnsAsync(true);
            HotelManager hotelManag = new HotelManager(hotelRepositoryMock.Object);
            bool hotelResultado = await hotelManag.InsertHotel(_hotel);
            Assert.IsTrue(hotelResultado);
        }

        [TestMethod]
        public async Task UpdateHotelTest()
        {
            Mock<InterfaceHotelRepository> hotelRepositoryMock = new Mock<InterfaceHotelRepository>();
            hotelRepositoryMock.Setup(r => r.UpdateHotel(_hotel)).ReturnsAsync(true);
            HotelManager hotelManag = new HotelManager(hotelRepositoryMock.Object);
            bool hotelResultado = await hotelManag.UpdateHotel(_hotel);
            Assert.IsTrue(hotelResultado);
        }

        [TestMethod]
        public async Task DeleteHotelTest()
        {
            Mock<InterfaceHotelRepository> hotelRepositoryMock = new Mock<InterfaceHotelRepository>();
            hotelRepositoryMock.Setup(r => r.DeleteHotel(_hotel)).ReturnsAsync(true);
            HotelManager hotelManag = new HotelManager(hotelRepositoryMock.Object);
            bool hotelResultado = await hotelManag.DeleteHotel(_hotel);
            Assert.IsTrue(hotelResultado);
        }
    }
}
