using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace YueShanp.Models.Interface
{
    interface ICostItemRepository
    {
        void CreateProductCostItem(CostItem instance);

        CostItem Get(int costItemId);

        IQueryable<CostItem> GetAll(int productId);

        void Update(CostItem instance);

        void Delete(CostItem instance);

        void SaveChanges();
    }
}
