using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StudentDBConsoleApp
{
    internal class Program
    {
        static void Main(string[] args)
        {   /*  When using context, it is good idea to wrap it in using statement
            to ensure that any source is freed up when we are finished with the context */
            using (var db = new StudentContext())
            {
                Console.WriteLine("Enter the Student's first name:");//  This console.WriteLine prompts the user to enter the first name
                string fname = Console.ReadLine();

                Console.WriteLine("Now, enter the last name:");
                string lname = Console.ReadLine();

                Console.WriteLine("Which course are you enrolled in?");
                string cname = Console.ReadLine();

                Console.WriteLine("What is your email address?");
                string email = Console.ReadLine();

                //CREATING A NEW INSTANCE
                var student = new Student { FirstName = fname, LastName = lname, CourseName = cname, Email = email };    //  We create a new instance of Student class with the name assigned
                db.Students.Add(student);                           //  We can now add this instance to the Students         
                db.SaveChanges();                                   //  This will save and push all changes made to the context

                //QUERY
                var query = from s in db.Students
                            orderby s.FirstName
                            select s;
                Console.WriteLine("\nStudents personal information:");
                foreach (var item in query)
                {
                    Console.WriteLine(item.FirstName + " " + item.LastName + ", " + item.CourseName + ", " + item.Email);
                }
                Console.WriteLine("Press any key to exit the program!");        //  Propmpts the user if he/she want to exit the program
                Console.ReadKey();                                              //  Closes the program
            }
        }
        //  The class for Student database
        public class Student
        {
            //  These are the students' properties
            public int id { get; set; }
            public string FirstName { get; set; }
            public string LastName { get; set; }
            public string CourseName { get; set; }
            public string Email { get; set; }
        }    
        public class StudentContext : DbContext     /*  The StudentContext is derived from DbContext type which is a base type for Entity Framework
                                                    The context represents a session with the database and allows to query inside the data  */
        {
            public DbSet<Student> Students { get; set; }    // Dbset is the property inside the DbContext that allows us to query inside the instances 
        }
    }
}
