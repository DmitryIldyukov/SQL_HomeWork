using System;
using System.Collections.Generic;
using SQLHW.Repositories;
using SQLHW.Models;
using System.Text.RegularExpressions;


namespace SQLHW
{
    class Program
    {
        private static string _connectionString = @"Data Source=DMITRY;Database=University;Trusted_Connection=True;";

        static void Main( string[] args )
        {
            IStudentRepository studentRepository = new StudentRawSqlRepository( _connectionString );
            IGroupRepository groupRepository = new GroupRawSqlRepository( _connectionString );
            ITogetherRepository togetherRepository = new TogetherRawSqlRepository( _connectionString );

            Console.WriteLine( "Доступные команды:" );
            Console.WriteLine( "add-student - добавить студента" );
            Console.WriteLine( "add-group - добавить группу" );
            Console.WriteLine( "add-student-in-group - добавить студента в группу" );
            Console.WriteLine( "get-students - вывести список студентов" );
            Console.WriteLine( "get-groups - вывести список групп" );
            Console.WriteLine( "get-students-with-Id-group - вывести студентов по id группы" );
            Console.WriteLine( "exit - выйти из приложения" );
            while ( true )
            {
                string command = Console.ReadLine();

                if ( command == "add-student" )
                {
                    Console.Write( "Введите имя студента: " );
                    string name = Console.ReadLine();
                    Console.Write( "Введите возраст студента: " );
                    int age = Convert.ToInt32((Console.ReadLine()));

                    studentRepository.Add( new Student
                    {
                        Name = name,
                        Age = age
                    } );                    
                    Console.WriteLine( "Success" );
                }
                else if ( command == "add-group" )
                {
                    Console.Write( "Введите название группы: " );
                    string name = Console.ReadLine();

                    groupRepository.Add( new Groups
                    {
                        Name = name
                    } );
                    Console.WriteLine( "Success" );
                }

                else if ( command == "add-student-in-group" )
                {
                    int studentId;
                    int groupId;

                    Console.Write( "Введите Id студента: " );
                    studentId = Convert.ToInt32( Console.ReadLine() );
                    Console.Write( "Введите Id группы: " );
                    groupId = Convert.ToInt32( Console.ReadLine() );
                    togetherRepository.Add( new Together    
                    {
                        StudentId = studentId,
                        GroupId = groupId
                    } );
                    Console.WriteLine( "Success" );
                }

                else if ( command == "get-students" )
                {
                    List<Student> students = studentRepository.GetAll();
                    foreach ( Student student in students )
                    {
                        Console.WriteLine( $"Id: {student.Id}, Name: {student.Name}, Age: {student.Age}" );
                    }
                }

                else if ( command == "get-groups" )
                {
                    List<Groups> groups = groupRepository.GetAll();
                    foreach ( Groups group in groups )
                    {
                        Console.WriteLine( $"Id: {group.Id}, Name: {group.Name}" );
                    }
                }

                else if ( command == "get-students-with-Id-group" )
                {
                    Console.Write( "Введите id группы: " );
                    int groupsId = int.Parse( Console.ReadLine() );
                    List<Together> togethers = togetherRepository.GetIdStudentAndGroup();
                    var student = new Student();
                    foreach ( var together in togethers )
                    {
                        if ( together.GroupId == groupsId )
                        {
                            student = studentRepository.GetById( together.StudentId );
                            Console.WriteLine( $"Id: {student.Id}, Name: {student.Name}" );
                        }
                    }
                }          

                else if ( command == "exit" )
                {
                    return;
                }
                else
                {
                    Console.WriteLine( "Команда не найдена" );
                }
            }
        }
    }
}