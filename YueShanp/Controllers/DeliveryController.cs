using System.Linq;
using System.Net;
using System.Web.Mvc;
using YueShanp.Models;
using YueShanp.Models.Interface;

namespace YueShanp.Controllers
{
    [Authorize]
    public class DeliveryController : Controller
    {
        private ICustomerRepository customerRepository;
        private IProductRepository productRepository;
        private IDeliveryRepository deliveryRepository;

        public DeliveryController()
        {
            this.customerRepository = new CustomerRepository();
            this.productRepository = new ProductRepository();
            this.deliveryRepository = new DeliveryRepository();
        }

        public ActionResult DeliveryIndex()
        {
            return View();
        }

        // GET: Delivery orders
        [AllowAnonymous]
        public ActionResult DeliveryOrderList(int? customerId)
        {
            if (customerId == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            var deliveryOrders = this.deliveryRepository.GetAll((int)customerId);

            return View(deliveryOrders.ToList());
        }
    }
}