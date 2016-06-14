using System;
using System.Collections.Generic;
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

        [AllowAnonymous]
        public ActionResult PrePrintDeliveryOrder(string doNumber)
        {
            var delivery = this.deliveryRepository.Get(doNumber.ToInt());
            //var viewModel = new PrePrintDeliveryOrderViewModel()
            //{
            //    CustomerName = delivery.Customer.Name,
            //    DeliveryOrderDate = delivery.DeliveryOrderDate,
            //    DeliveryOrderDetailList = delivery.DeliveryOrderDetailList,
            //    TotalAmount = delivery.TotalAmount
            //};

            var viewModel = new PrePrintDeliveryOrderViewModel()
            {
                CustomerName = "黃龍",
                DeliveryOrderDate = DateTime.Now,
                DeliveryOrderDetailList = new List<DeliveryOrderDetail>()
                {
                    new DeliveryOrderDetail()
                    {
                        Qty = 5,
                        Product = new Product()
                        {
                            Name = "產品一號"
                        },
                        DeliveryUnitPrice = 15

                    },
                    new DeliveryOrderDetail()
                    {
                        Qty = 4,
                        Product = new Product()
                        {
                            Name = "產品二號"
                        },
                        DeliveryUnitPrice = 18

                    },
                    new DeliveryOrderDetail()
                    {
                        Qty = 2,
                        Product = new Product()
                        {
                            Name = "產品三號"
                        },
                        DeliveryUnitPrice = 987
                    }
                },
                TotalAmount = 1580
            };

            return View(viewModel);
        }

        #region Modal
        public ActionResult ModalPrePrintDeliveryOrder()
        {
            return View();
        }
        #endregion
    }
}