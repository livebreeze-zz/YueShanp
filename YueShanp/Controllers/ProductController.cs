using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Net;
using System.Web.Mvc;

using YueShanp.Models;
using YueShanp.Models.Interface;

namespace YueShanp.Controllers
{
    [Authorize]
    public class ProductController : Controller
    {
        #region private fileds and constructor
        private ICustomerRepository customerRepository;
        private IProductRepository productRepository;
        private ICostItemRepository costItemRepository;

        public ProductController()
        {
            this.customerRepository = new CustomerRepository();
            this.productRepository = new ProductRepository();
            this.costItemRepository = new CostItemRepository();
        }
        #endregion       

        #region Product master
        // GET: ProductsMaster
        [AllowAnonymous]
        public ActionResult ProductsMaster(int? customerId)
        {
            if (customerId == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
                //return RedirectToAction("Index", "Customer");
            }

            var customer = this.customerRepository.Get((int)customerId);
            if (customer == null)
            {
                return HttpNotFound();
            }

            var viewModel = new ProductsMasterViewModel()
            {
                Customer = customer,
                Products = this.productRepository.GetAll((int)customerId)
            };

            return View(viewModel);
        }

        // GET: Product/Details/5
        [AllowAnonymous]
        public ActionResult ProductDetail(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            Product product = this.productRepository.Get((int)id);
            if (product == null)
            {
                return HttpNotFound();
            }

            return View(product);
        }

        // GET: Product/Create
        public ActionResult Create(int customerId)
        {
            var customer = this.customerRepository.Get(customerId);
            var product = new Product()
            {
                Customer = customer
            };

            return View(product);
        }

        // POST: Product/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,Name,UnitPrice,Note,Customer")] Product product)
        {
            if (ModelState.IsValid)
            {
                product.Customer = this.customerRepository.Get(product.Customer.Id);

                EntityHelper<Product>.CreateBaseEntity(product, User.Identity.Name);

                this.productRepository.CreateProductQuoted(product);
                return RedirectToAction("ProductsMaster", new { CustomerId = product.Customer.Id });
            }
            else
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
        }

        // GET: Product/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            Product product = this.productRepository.Get((int)id);
            if (product == null)
            {
                return HttpNotFound();
            }

            return View(product);
        }

        // POST: Product/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,Name,UnitPrice,Note,Creator,CreateTime,Customer")] Product product)
        {
            if (ModelState.IsValid)
            {
                EntityHelper<Product>.EditBaseEntity(product, User.Identity.Name);
                this.productRepository.Update(product);
                return RedirectToAction("ProductsMaster", new { CustomerId = product.Customer.Id });
            }

            return View(product);
        }

        // GET: Product/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            var product = this.productRepository.Get((int)id);
            if (product == null)
            {
                return HttpNotFound();
            }

            return View(product);
        }

        // POST: Product/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            var product = this.productRepository.Get(id);
            if (product == null)
            {
                return HttpNotFound();
            }

            EntityHelper<Product>.EditBaseEntity(product, User.Identity.Name, EntityStatus.Deleted);
            this.productRepository.Update(product);
            //this.ProductRepository.Delete(product);

            return RedirectToAction("ProductsMaster", new { CustomerId = product.Customer.Id });
        }

        #endregion

        #region Product cost item
        public ActionResult ProductCostItems(int? productId)
        {
            if (productId == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            var product = this.productRepository.Get((int)productId);
            if (product == null)
            {
                return HttpNotFound();
            }

            var costItems = this.costItemRepository.GetAll((int)productId);

            return View(new ProductCostViewModel()
            {
                ProductId = (int)productId,
                ProductName = product.Name,
                CostItems = costItems
            });
        }

        public ActionResult ProductCostItemCreate(int? productId)
        {
            if (productId == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            var costItem = new CostItem()
            {
                Product = this.productRepository.Get((int)productId)
            };

            if (costItem.Product == null)
            {
                return HttpNotFound();
            }

            return View(costItem);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult ProductCostItemCreate([Bind(Include = "Name,UnitPrice,ItemQty,Product")]CostItem costItem)
        {
            if (ModelState.IsValid)
            {
                EntityHelper<CostItem>.CreateBaseEntity(costItem, User.Identity.Name);
                costItem.ItemType = ItemType.Normal;
                this.costItemRepository.CreateProductCostItem(costItem);

                return RedirectToAction("ProductCostItems", new { ProductId = costItem.Product.Id });
            }

            return View(costItem);
        }

        public ActionResult ProductCostItemEdit(int? costItemId)
        {
            if (costItemId == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            var costItem = this.costItemRepository.Get((int)costItemId);

            return View(costItem);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult ProductCostItemEdit([Bind(Include = "Id,Name,UnitPrice,ItemQty,ItemType,Product,Creator,CreateTime")]CostItem costItem)
        {
            if (ModelState.IsValid)
            {
                EntityHelper<CostItem>.EditBaseEntity(costItem, User.Identity.Name);
                this.costItemRepository.Update(costItem);

                return RedirectToAction("ProductCostItems", new { ProductId = costItem.Product.Id });
            }

            return View(costItem);
        }

        public ActionResult ProductCostItemDelete(int? costItemId)
        {
            if (costItemId == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            var costItem = this.costItemRepository.Get((int)costItemId);
            if (costItemId == null)
            {
                return HttpNotFound();
            }

            return View(costItem);
        }

        [HttpPost, ActionName("ProductCostItemDelete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteCostItemConfirmed(int costItemId)
        {
            var costItem = this.costItemRepository.Get(costItemId);

            EntityHelper<CostItem>.EditBaseEntity(costItem, User.Identity.Name, EntityStatus.Deleted);
            this.costItemRepository.Update(costItem);

            return RedirectToAction("ProductCostItems", new { ProductId = costItem.Product.Id });
        }
        #endregion

        #region print
        public ActionResult QuotedPrinter()
        {
            return View();
        }
        #endregion

        #region Product Lable

        [AllowAnonymous]
        public ActionResult PrintProductLabel(string id)
        {
            string sCode = string.Empty;
            //清除該頁輸出緩存，設置該頁無緩存   
            Response.Buffer = true;
            Response.ExpiresAbsolute = System.DateTime.Now.AddMilliseconds(0);
            Response.Expires = 0;
            Response.CacheControl = "no-cache";
            Response.AppendHeader("Pragma", "No-Cache");
            //將Code39條碼寫入記憶體流，並將其以 "image/Png" 格式輸出   
            MemoryStream oStream = new MemoryStream();
            try
            {
                Bitmap oBmp = GetCode39(id);
                oBmp.Save(oStream, System.Drawing.Imaging.ImageFormat.Png);
                oBmp.Dispose();
                Response.ClearContent();
                Response.ContentType = "image/Png";
                Response.BinaryWrite(oStream.ToArray());
            }
            finally
            {
                //釋放資源   
                oStream.Dispose();
            }

            return new EmptyResult();
        }

        private Bitmap GetCode39(string strSource)
        {
            int x = 5; //左邊界
            int y = 0; //上邊界
            int WidLength = 2; //粗BarCode長度
            int NarrowLength = 1; //細BarCode長度
            int BarCodeHeight = 24; //BarCode高度
            int intSourceLength = strSource.Length;
            string strEncode = "010010100"; //編碼字串 初值為 起始符號 *

            string AlphaBet = "0123456789ABCDEFGHIJKLMNOPQRSTUVWXYZ-. $/+%*"; //Code39的字母

            string[] Code39 = //Code39的各字母對應碼
                    {
                     /**//* 0 */ "000110100",  
                     /**//* 1 */ "100100001",  
                     /**//* 2 */ "001100001",  
                     /**//* 3 */ "101100000",
                     /**//* 4 */ "000110001",  
                     /**//* 5 */ "100110000",  
                     /**//* 6 */ "001110000",  
                     /**//* 7 */ "000100101",
                     /**//* 8 */ "100100100",  
                     /**//* 9 */ "001100100",  
                     /**//* A */ "100001001",  
                     /**//* B */ "001001001",
                     /**//* C */ "101001000",  
                     /**//* D */ "000011001",  
                     /**//* E */ "100011000",  
                     /**//* F */ "001011000",
                     /**//* G */ "000001101",  
                     /**//* H */ "100001100",  
                     /**//* I */ "001001100",  
                     /**//* J */ "000011100",
                     /**//* K */ "100000011",  
                     /**//* L */ "001000011",  
                     /**//* M */ "101000010",  
                     /**//* N */ "000010011",
                     /**//* O */ "100010010",  
                     /**//* P */ "001010010",  
                     /**//* Q */ "000000111",  
                     /**//* R */ "100000110",
                     /**//* S */ "001000110",  
                     /**//* T */ "000010110",  
                     /**//* U */ "110000001",  
                     /**//* V */ "011000001",
                     /**//* W */ "111000000",  
                     /**//* X */ "010010001",  
                     /**//* Y */ "110010000",  
                     /**//* Z */ "011010000",
                     /**//* - */ "010000101",  
                     /**//* . */ "110000100",  
                     /**//*' '*/ "011000100",
                     /**//* $ */ "010101000",
                     /**//* / */ "010100010",  
                     /**//* + */ "010001010",  
                     /**//* % */ "000101010",  
                     /**//* * */ "010010100"
                };
            strSource = strSource.ToUpper();
            //實作圖片
            Bitmap objBitmap = new Bitmap(
              ((WidLength * 3 + NarrowLength * 7) * (intSourceLength + 2)) + (x * 2),
              BarCodeHeight + (y * 2));
            Graphics objGraphics = Graphics.FromImage(objBitmap); //宣告GDI+繪圖介面
                                                                  //填上底色
            objGraphics.FillRectangle(Brushes.White, 0, 0, objBitmap.Width, objBitmap.Height);

            for (int i = 0; i < intSourceLength; i++)
            {
                //檢查是否有非法字元
                if (AlphaBet.IndexOf(strSource[i]) == -1 || strSource[i] == '*')
                {
                    objGraphics.DrawString("含有非法字元",
                      SystemFonts.DefaultFont, Brushes.Red, x, y);
                    return objBitmap;
                }
                //查表編碼
                strEncode = string.Format("{0}0{1}", strEncode,
                 Code39[AlphaBet.IndexOf(strSource[i])]);
            }

            strEncode = string.Format("{0}0010010100", strEncode); //補上結束符號 *

            int intEncodeLength = strEncode.Length; //編碼後長度
            int intBarWidth;

            for (int i = 0; i < intEncodeLength; i++) //依碼畫出Code39 BarCode
            {
                intBarWidth = strEncode[i] == '1' ? WidLength : NarrowLength;
                objGraphics.FillRectangle(i % 2 == 0 ? Brushes.Black : Brushes.White,
                 x, y, intBarWidth, BarCodeHeight);
                x += intBarWidth;
            }
            return objBitmap;
        }

        [AllowAnonymous]
        public ActionResult PrintProductLabel2(string id)
        {
            Response.ContentType = "image/gif";
            Response.BinaryWrite(this.GetAllRMALabel(id));
            return new EmptyResult();
            //return View();
        }

        private byte[] GetAllRMALabel(string pnNumber)
        {
            Bitmap image = new Bitmap(330, 130);
            Graphics graphics = Graphics.FromImage(image);
            graphics.FillRectangle(Brushes.White, 0, 0, 330, 130);
            //Font font = new Font("AdvC39b", 20f, FontStyle.Regular);
            Font font = new Font("Code128bWin", 20f, FontStyle.Regular);
            Font font2 = new Font("Times New Roman", 12f, FontStyle.Bold);
            graphics.DrawString("P/N: " + pnNumber, font2, SystemBrushes.ControlText, 0f, 0f);
            graphics.DrawString(string.Format("*{0}*", pnNumber), font, SystemBrushes.ControlText, 1f, 20f);
            MemoryStream stream = new MemoryStream();
            image.Save(stream, ImageFormat.Gif);
            graphics.Dispose();
            return stream.GetBuffer();
        }
        #endregion
    }
}
