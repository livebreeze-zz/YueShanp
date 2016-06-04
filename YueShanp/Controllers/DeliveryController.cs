using System.Linq;
using System.Net;
using System.Web.Mvc;
using YueShanp.Filter;
using YueShanp.Models;
using YueShanp.Models.Interface;

namespace YueShanp.Controllers
{
    [WebAuthorize]
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
            ViewData[nameof(AddDelivery)] = string.Empty;
            var customerList = this.customerRepository.GetAll();
            var viewModel = new DeliveryIndexViewModel() { CustomerList = customerList };

            return View(viewModel);
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

        public ActionResult ModalPrePrintDeliveryOrder()
        {
            return View();
        }
    }
}