using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using TanvirBakery.Models;

namespace TanvirBakery.Repository
{
    public class CategoriesRepository:Repository<Category>
    {
        public CategoriesRepository(BakeryContext context)
            : base(context)
        {

        }
    }
}