using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SQLHW.Models;

namespace SQLHW.Repositories
{
    class TogetherRawSqlRepository : ITogetherRepository
    {
        private readonly string _connectionString;
        public TogetherRawSqlRepository( string connectionString )
        {
            _connectionString = connectionString;
        }
        public void Add( Together together )
        {
            using ( var connection = new SqlConnection( _connectionString ) )
            {
                connection.Open();
                using ( SqlCommand command = connection.CreateCommand() )
                {
                    command.CommandText =
                        @"insert into [Together]
                        values
                            (@studentsId, @groupsId)
                        select SCOPE_IDENTITY()";

                    command.Parameters.Add( "@studentsId", SqlDbType.Int ).Value = together.StudentId;
                    command.Parameters.Add( "@groupsId", SqlDbType.Int ).Value = together.GroupId;

                    command.ExecuteNonQuery();
                }
            }
        }
        public List<Student> GetStudentsByIdGroups( int groupsId )
        {
            var result = new List<Student>();

            using ( var connection = new SqlConnection( _connectionString ) )
            {
                connection.Open();
                using ( SqlCommand command = connection.CreateCommand() )
                {
                    command.CommandText =
                        @"select [StudentsId], [GroupsId] from [Together]
                        where [GroupsId] = @groupsId";
                    command.Parameters.Add( "@groupsId", SqlDbType.Int ).Value = groupsId;

                    using ( var reader = command.ExecuteReader() )
                    {
                        while ( reader.Read() )
                        {
                            result.Add( new Student
                            {
                                Id = Convert.ToInt32( reader[ "StudentsId" ] )
                            } );
                        }
                    }
                }
            }
            return result;
        }

        public List<Together> GetIdStudentAndGroup()
        {
            var result = new List<Together>();
            using ( var connection = new SqlConnection( _connectionString ) )
            {
                connection.Open();
                using ( SqlCommand command = connection.CreateCommand() )
                {
                    command.CommandText =
                        @"select [StudentsId], [GroupsId]
                        from [Together]";

                    using ( var reader = command.ExecuteReader() )
                    {
                        while ( reader.Read() )
                        {
                            result.Add( new Together
                            {
                                StudentId  = Convert.ToInt32( reader[ "StudentsId" ] ),
                                GroupId = Convert.ToInt32( reader[ "GroupsId" ] )
                            } );
                        }
                    }
                }
            }
            return result;
        }
    }
}
