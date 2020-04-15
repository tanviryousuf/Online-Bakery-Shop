using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using TanvirBakery.Models;

namespace TanvirBakery.Repository
{
    public class BrandsRepository : Repository<Brand>
    {
          
        public BrandsRepository(BakeryContext context)
            : base(context)
        {

        }
    }

}
