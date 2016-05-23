using System.Collections.Generic;
using System.Web.Mvc;
using YueShanp.Models;
using YueShanp.Models.Interface;

namespace YueShanp.Controllers
{
    public class AjaxController : Controller
    {
        private ICustomerRepository customerRepository;
        private IProductRepository productRepository;
        private IDeliveryRepository deliveryRepository;

        public AjaxController()
        {
            this.customerRepository = new CustomerRepository();
            this.productRepository = new ProductRepository();
            this.deliveryRepository = new DeliveryRepository();
        }

        public JsonResult GetProductList(int customerId)
        {
            var productList = this.productRepository.GetAll(customerId);

            return Json(productList);
        }

        public JsonResult AddDeliveryOrderUrl()
        {

            return Json(new Dictionary<string, object>()
            {
                { "IsSuccess", true }
            });
        }
    }
}