using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccessLayer
{
    public class StandardRepository : Repository<Standard>, IStandardRepository
    {
        private SchoolDBEntities _standardEntities;

        public StandardRepository() : base(new SchoolDBEntities())
        {
            
        }
        public StandardRepository(SchoolDBEntities stanContext) : base(stanContext)
        {
            _standardEntities = stanContext;
            
        }



    }
}
