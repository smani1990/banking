using AutoMapper;
using banking.Automapping;
using banking.Controllers;
using banking.Models;
using banking.Models.DTO;
using banking.Repositories;
using Microsoft.AspNetCore.Mvc;
using Moq;

namespace Unittest
{
    public class UnitTest1
    {
        private readonly Mock<ISQLrepository> mockRepo;

        private readonly MapperConfiguration? mapper;


        private readonly AccountController? controller;
        public UnitTest1()
        {
            mockRepo = new Mock<ISQLrepository>();
            mapper = new MapperConfiguration(cfg =>
            {
                cfg.AddProfile(new AutoMapperProfiles());
            });
        }
        [Fact]
        public void CreateUser()
        { 
           
            
            var mapper1 = mapper.CreateMapper();
            var controller = new AccountController(mapper1, mockRepo.Object);
            var user = new NewUserAccountDTO()
            {
                Name = "Test",
                Password = "Test",
                account = new Account()
                {
                    Balance = 1000
                }
            };
            var user1 = new User()
            {
                Name = "Test",
                Password = "Test",
                account = new Account()
                {
                    Balance = 1000
                }
            };
            mockRepo.Setup(repo => repo.CreateAccountAsync(user1)).ReturnsAsync(user1);
            var result = controller.CreateUser(user);
            var okResult = result.Result as OkObjectResult;
            Assert.Equal(200, okResult.StatusCode);

        }
        [Fact]
        public void Login()
        {
            var mapper1 = mapper.CreateMapper();
            var controller = new AccountController(mapper1, mockRepo.Object);
            var user = new NewUserAccountDTO()
            {
                Name = "Test",
                Password = "Test"
            };
            var user1 = new User()
            {
                Name = "Test",
                Password = "Test"
            };
            mockRepo.Setup(repo => repo.ValidateAccountAsync(user.Name, user.Password)).ReturnsAsync(user1);
            var result = controller.Login(user.Name, user.Password);
            var okResult = result.Result as OkObjectResult;
            Assert.Equal(200, okResult.StatusCode);

        }
      
            
    }
}