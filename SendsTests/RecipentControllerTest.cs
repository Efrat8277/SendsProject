using Microsoft.AspNetCore.Mvc;
using SendsProject.Classes;
using SendsProject.Controllers;
using SendsTests.PackageTest;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SendsTests
{
    public class RecipentControllerTest
    {
        private readonly FakeContext fakeContext = new FakeContext();

        [Fact]
        public void Get_Result()
        {
            var controller = new RecipientController(fakeContext);
            var result = controller.Get();
            Assert.IsType<List<Recipient>>(result);
        }

        [Fact]
        public void GetById_ReturnOk()
        {
            var id = 2;
            var controller = new RecipientController(fakeContext);
            var result = controller.Get(id);
            Assert.IsType<OkObjectResult>(result);
        }
        [Fact]
        public void GetById_ReturnNotFound()
        {
            var id = 9;
            var controller = new RecipientController(fakeContext);
            var result = controller.Get(id);
            Assert.IsType<NotFoundResult>(result);
        }

        [Fact]
        public void Add_ReturnOk()
        {
            var package = new Recipient { RecipientId =2, Identity ="123456789", Name ="eli", Phone ="0548967589", Address ="hahagana"};
            var controller = new RecipientController(fakeContext);
            var result = controller.Post(package);
            Assert.IsType<ConflictResult>(result);
        }
    }
}
