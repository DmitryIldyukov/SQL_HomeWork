using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SQLHW.Models;

namespace SQLHW.Repositories
{
    interface IStudentRepository
    {
        void Add( Student student );
        Student GetById( int id );
        List<Student> GetAll();
    }
}