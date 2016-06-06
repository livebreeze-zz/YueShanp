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
        public JsonResult AddDeliveryOrder([Bind(Include = "Id,DeliveryOrderNumber,CustomerSONumber,DeliveryOrderDate,ReceivableMonth,Customer,DeliveryOrderDetailList")] DeliveryOrder deliveryOrderModel)
        {
            var responseDic = new Dictionary<string, object>()
            {
                { "IsSuccess", false },
                { "ErrorMessage", string.Empty }
            };

            if (!ModelState.IsValid)
            {
                responseDic["ErrorMessage"] = "validation error!";
                return Json(responseDic);
            }

            deliveryOrderModel.CreateTime = deliveryOrderModel.LastEditTime = DateTime.Now;
            deliveryOrderModel.Creator = deliveryOrderModel.LastEditor = User.Identity.Name;
            deliveryOrderModel.EntityStatus = EntityStatus.Enabled;
           
            try
            {
                this.deliveryRepository.CreateDeliveryOrder(deliveryOrderModel);
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