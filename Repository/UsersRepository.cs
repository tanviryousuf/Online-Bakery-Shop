using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using TanvirBakery.Models;


namespace TanvirBakery.Repository
{
    public class UsersRepository : Repository<User>
    {
        public UsersRepository(BakeryContext context)
            : base(context)
        {

        }
    }

}