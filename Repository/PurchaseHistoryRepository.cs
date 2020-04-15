using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using TanvirBakery.Models;


namespace TanvirBakery.Repository
{
    public class PurchaseHistoryRepository:Repository<PurchaseHistory>
    {
        public PurchaseHistoryRepository(BakeryContext context)
            : base(context)
        {

        }
    }
}