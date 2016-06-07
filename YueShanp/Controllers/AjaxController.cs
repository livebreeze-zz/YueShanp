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

        #region const
        private const string ISSUCCESS = "IsSuccess";
        private const string ERRORMESSAGE = "ErrorMessage";
        private const string VALIDATIONERRORMESSAGE = "validation error!";
        private const string CUSTOMERS = "Customers";
        #endregion

        private Dictionary<string, object> responseDic = new Dictionary<string, object>()
            {
                { ISSUCCESS, false },
                { ERRORMESSAGE, string.Empty }
            };

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

        public JsonResult GetCustomerList()
        {
            var customers = this.customerRepository.GetAll();
            this.responseDic[ISSUCCESS] = true;
            this.responseDic.Add(CUSTOMERS, customers);

            return Json(this.responseDic, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public JsonResult AddDeliveryOrder([Bind(Include = "Id,DeliveryOrderNumber,CustomerSONumber,DeliveryOrderDate,ReceivableMonth,Customer,DeliveryOrderDetailList")] DeliveryOrder deliveryOrderModel)
        {
            if (!ModelState.IsValid)
            {
                this.responseDic[ERRORMESSAGE] = VALIDATIONERRORMESSAGE;
                return Json(this.responseDic);
            }

            deliveryOrderModel.CreateTime = deliveryOrderModel.LastEditTime = DateTime.Now;
            deliveryOrderModel.Creator = deliveryOrderModel.LastEditor = User.Identity.Name;
            deliveryOrderModel.EntityStatus = EntityStatus.Enabled;

            try
            {
                this.deliveryRepository.CreateDeliveryOrder(deliveryOrderModel);
                this.responseDic[ISSUCCESS] = true;
            }
            catch (Exception ex)
            {
                this.responseDic[ERRORMESSAGE] = ex.ToString();
            }

            return Json(this.responseDic);
        }
    }
}