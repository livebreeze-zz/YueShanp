using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web.Mvc;
using YueShanp.Models;
using YueShanp.Models.Interface;

namespace YueShanp.Controllers
{
    public class ModalController : Controller
    {
        private ICustomerRepository customerRepository;
        private IProductRepository productRepository;

        public ModalController()
        {
            this.customerRepository = new CustomerRepository();
            this.productRepository = new ProductRepository();
        }

        public ActionResult ModalAddProduct(int? id)
        {
            var customer = this.customerRepository.Get((int)id);
            if (customer == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.NotFound);
            }

            var products = this.productRepository.GetAll(customer.Id);

            ViewBag.Customer = customer;
            ViewBag.Products = products;

            return View();
        }
    }
}