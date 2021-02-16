using api_server_users.Controllers;
using api_server_users.DataBase.Entities;
using api_server_users.Models;
using api_server_users.Repositories.Interfaces;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using Xunit;

namespace api_server_users_test
{
    public class RepositoryTest
    {
        Mock<IApplicationUserRepository> mockUserRepository;
        Mock<ITokenRepository> mockTokenRepository;

        public RepositoryTest()
        {
            mockUserRepository = new Mock<IApplicationUserRepository>();
            mockTokenRepository = new Mock<ITokenRepository>();
        }

        /// <summary>
        /// Cadastrar usuário
        /// </summary>
        [Fact]
        public void AddUser()
        {
            // Setup
            var expectedUser = new ApplicationUser();

            var newUser = new ApplicationUser()
            {
                FullName = "Eloisa Caetano de Oliveira",
                Email = "eloisa@gmail.com",
                UserName = "eloisa@gmail.com",
                PhoneNumber = "123456",
                PasswordHash = "Eloisa@123"
            };

            var newUserDTO = new UserAddDTO()
            {
                Name = newUser.FullName,
                PhoneNumber = newUser.PhoneNumber,
                Email = newUser.Email,
                Password = newUser.PasswordHash,
                PasswordConfirmation = newUser.PasswordHash
            };

            mockUserRepository.Setup(x => x.Add(newUser, newUser.PasswordHash)).Returns(new IdentityResult());

            // Injeção
            var userController = new UserController(mockUserRepository.Object, mockTokenRepository.Object);

            // Action
            var result = userController.Add(newUserDTO);

            // Assert
            var viewResult = Assert.IsType<ViewResult>(result);

            var model = Assert.IsAssignableFrom<IEnumerable<ApplicationUser>>(viewResult.ViewData.Model);

            Assert.Equal(expectedUser, model.FirstOrDefault());
        }

        /// <summary>
        /// Login
        /// </summary>
        [Fact]
        public void Login()
        {
            // Setup
            var expectedUser = new ApplicationUser();
            var email = "maycon@gmail.com";
            var password = "Eloisa@2016";

            // Injeção
            var userController = new UserController(mockUserRepository.Object, mockTokenRepository.Object);

            // Action
            var result = userController.Login(email, password);

            // Assert
            var viewResult = Assert.IsType<ViewResult>(result);

            var model = Assert.IsAssignableFrom<IEnumerable<ApplicationUser>>(viewResult.ViewData.Model);

            Assert.Equal(expectedUser, model.FirstOrDefault());
        }

        /// <summary>
        /// Obter usuário
        /// </summary>
        [Fact]
        public void GetAllUsers()
        {
            // Setup
            var expectedUserCount = 5;

            mockUserRepository.Setup(x => x.Get()).Returns(new List<ApplicationUser>());

            // Injeção
            var userController = new UserController(mockUserRepository.Object, mockTokenRepository.Object);

            // Action
            var result = userController.GetAll();

            // Assert
            var viewResult = Assert.IsType<ViewResult>(result);

            var model = Assert.IsAssignableFrom<IEnumerable<ApplicationUser>>(viewResult.ViewData.Model);

            Assert.Equal(expectedUserCount, model.Count());

        }
    }
}
