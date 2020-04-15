using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using TanvirBakery.Models;


namespace TanvirBakery.Repository
{
    public class ProductsRepository:Repository<Item>
    {
        public ProductsRepository(BakeryContext context)
            : base(context)
        {

        }


    }
}