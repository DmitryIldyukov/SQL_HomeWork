using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SQLHW.Models;

namespace SQLHW.Repositories
{
    interface IGroupRepository
    {
        void Add( Groups group );
        Groups GetById( int id );
        List<Groups> GetAll();
    }
}
