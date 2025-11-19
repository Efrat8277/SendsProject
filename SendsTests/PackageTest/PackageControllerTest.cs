using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using SendsProject.Classes;
using SendsProject.Controllers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SendsTests.PackageTest
{
    public class PackageControllerTest
    {
        private readonly FakeContext fakeContext = new FakeContext();

        [Fact]
        public void Get_Result()
        {
            var controller = new PackageController(fakeContext);
            var result = controller.Get();
            Assert.IsType<List<Package>>(result);
        }

        [Fact]
        public void GetById_ReturnOk()
        {
            var id=1;
            var controller = new PackageController(fakeContext);
            var result = controller.Get(id);
            Assert.IsType<OkObjectResult>(result);
        }

        [Fact]
        public void GetById_ReturnNotFound()
        {
            var id = 9;
            var controller = new PackageController(fakeContext);
            var result = controller.Get(id);
            Assert.IsType<NotFoundResult>(result);
        }

        [Fact]
        public void Add_ReturnOk()
        {
            var package = new Package { Id=3, Weight =5.6, SenderName ="hhhh", SendDate= new DateTime(),IsSentToRecipient =true};
            var controller = new PackageController(fakeContext);
            var result = controller.Post(package);
            Assert.IsType<OkObjectResult>(result);
        }

    }
}
