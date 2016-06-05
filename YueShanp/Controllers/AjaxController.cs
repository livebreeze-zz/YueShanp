using System;
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

        [HttpPost]
        [ValidateAntiForgeryToken]
        public JsonResult AddDeliveryOrder([Bind(Include = "Id,DeliveryOrderNumber,CustomerSONumber,DeliveryOrderDate,ReceivableMonth,Customer,DeliveryOrderDetailList")] DeliveryOrder deliveryOrder)
        {
            deliveryOrder.CreateTime = deliveryOrder.LastEditTime = DateTime.Now;
            deliveryOrder.Creator = deliveryOrder.LastEditor = User.Identity.Name;
            deliveryOrder.EntityStatus = EntityStatus.Enabled;

            var responseDic = new Dictionary<string, object>()
            {
                { "IsSuccess", false },
                { "ErrorMessage", string.Empty }
            };

            try
            {
                this.deliveryRepository.CreateDeliveryOrder(deliveryOrder);
                responseDic["IsSuccess"] = true;
            }
            catch (Exception ex)
            {
                responseDic["ErrorMessage"] = ex.ToString();
            }

            return Json(responseDic);
        }
    }
}