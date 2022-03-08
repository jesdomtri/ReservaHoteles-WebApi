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
    public class UsuarioManagerTest
    {
        private static Usuario _usuario;
        private static Usuario _usuario2;
        private static IEnumerable<Usuario> _usuarios;

        [ClassInitialize]
        public static void GlobalInitilization(TestContext testContext)
        {
            _usuario = new Usuario()
            {
                UsuarioID = 40,
                Nombre = "nombreTest",
                Apellidos = "apellidoTest",
                Mail = "test@mail.com",
                Direccion = "casaTest"
            };

            _usuario2 = new Usuario()
            {
                UsuarioID = 41,
                Nombre = "nombreTest2",
                Apellidos = "apellidoTest2",
                Mail = "test2@mail.com",
                Direccion = "casaTest2"
            };

            _usuarios = new List<Usuario> { _usuario, _usuario2 };
        }

        [TestMethod]
        public async Task GetUsuariosTest()
        {
            Mock<InterfaceUsuarioRepository> usuarioRepositoryMock = new Mock<InterfaceUsuarioRepository>();
            usuarioRepositoryMock.Setup(r => r.GetAllUsuarios()).ReturnsAsync(_usuarios);
            UsuarioManager usuarioManag = new UsuarioManager(usuarioRepositoryMock.Object);
            IEnumerable<Usuario> usuariosResultado = await usuarioManag.GetAllUsuarios();
            Assert.AreEqual(usuariosResultado, _usuarios);
        }

        [TestMethod]
        public async Task GetUsuarioTest()
        {
            Mock<InterfaceUsuarioRepository> usuarioRepositoryMock = new Mock<InterfaceUsuarioRepository>();
            usuarioRepositoryMock.Setup(r => r.GetUsuario(40)).ReturnsAsync(_usuario);
            UsuarioManager usuarioManag = new UsuarioManager(usuarioRepositoryMock.Object);
            Usuario usuarioResultado = await usuarioManag.GetUsuario(40);
            Assert.AreEqual(usuarioResultado, _usuario);
        }

        [TestMethod]
        public async Task InsertUsuarioTest()
        {
            Mock<InterfaceUsuarioRepository> usuarioRepositoryMock = new Mock<InterfaceUsuarioRepository>();
            usuarioRepositoryMock.Setup(r => r.InsertUsuario(_usuario)).ReturnsAsync(true);
            UsuarioManager usuarioManag = new UsuarioManager(usuarioRepositoryMock.Object);
            bool usuarioResultado = await usuarioManag.InsertUsuario(_usuario);
            Assert.IsTrue(usuarioResultado);
        }

        [TestMethod]
        public async Task UpdateUsuarioTest()
        {
            Mock<InterfaceUsuarioRepository> usuarioRepositoryMock = new Mock<InterfaceUsuarioRepository>();
            usuarioRepositoryMock.Setup(r => r.UpdateUsuario(_usuario)).ReturnsAsync(true);
            UsuarioManager usuarioManag = new UsuarioManager(usuarioRepositoryMock.Object);
            bool usuarioResultado = await usuarioManag.UpdateUsuario(_usuario);
            Assert.IsTrue(usuarioResultado);
        }

        [TestMethod]
        public async Task DeleteUsuarioTest()
        {
            Mock<InterfaceUsuarioRepository> usuarioRepositoryMock = new Mock<InterfaceUsuarioRepository>();
            usuarioRepositoryMock.Setup(r => r.DeleteUsuario(_usuario)).ReturnsAsync(true);
            UsuarioManager usuarioManag = new UsuarioManager(usuarioRepositoryMock.Object);
            bool usuarioResultado = await usuarioManag.DeleteUsuario(_usuario);
            Assert.IsTrue(usuarioResultado);
        }
    }
}
