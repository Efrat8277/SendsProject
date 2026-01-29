//using Microsoft.AspNetCore.Mvc;
//using SendsProject.Classes;
//using SendsProject.Controllers;
//using SendsTests.PackageTest;
//using System;
//using System.Collections.Generic;
//using System.Linq;
//using System.Text;
//using System.Threading.Tasks;

//namespace SendsTests
//{
//    public class DeliveryPersonControllerTest
//    {
//        private readonly FakeContext fakeContext = new FakeContext();

//        [Fact]
//        public void Get_Result()
//        {
//            var controller = new DeliveryPersonController(fakeContext);
//            var result = controller.Get();
//            Assert.IsType<List<DeliveryPerson>>(result);
//        }

//        [Fact]
//        public void GetById_ReturnOk()
//        {
//            var id = 1;
//            var controller = new DeliveryPersonController(fakeContext);
//            var result = controller.Get(id);
//            Assert.IsType<OkObjectResult>(result);
//        }
//        [Fact]
//        public void GetById_ReturnNotFound()
//        {
//            var id = 9;
//            var controller = new DeliveryPersonController(fakeContext);
//            var result = controller.Get(id);
//            Assert.IsType<NotFoundResult>(result);
//        }

//        [Fact]
//        public void Add_ReturnOk()
//        {
//            var deliver = new DeliveryPerson  { DeliveryPersonId = 1, Name = "hgkjfd", Phone = "055555555" } ;
//            var controller = new DeliveryPersonController(fakeContext);
//            var result = controller.Post(deliver);
//            Assert.IsType<ConflictResult>(result);
//        }
//    }
//}
