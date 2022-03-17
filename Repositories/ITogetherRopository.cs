using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SQLHW.Models;

namespace SQLHW.Repositories
{
    interface ITogetherRepository
    {
        void Add( Together together ); 
        List<Together> GetIdStudentAndGroup();
        List<Student> GetStudentsByIdGroups( int groupsId );
    }
}