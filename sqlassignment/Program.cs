using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.Collections;
using System.Web;

namespace sqlassignment
{

    //****************************************************************************************************************************
    //ANSWER TO QUESTION 1.1 : COURSE CLASS DEFINITION, also Instructor class defintition
    //****************************************************************************************************************************
    class Course
    {
        public string CourseId { get; set; }
        public string Subject { get; set; }
        public string CourseCode { get; set; }
        public string Title { get; set; }
        public string Location { get; set; }
        public string Instructor { get; set; }

    }

    class Instructor
    {
        public string InstructorName { get; set; }
        public string OfficeNumber { get; set; }
        public string EmailAddress { get; set; }
        
    }

    class Program
    {
        static void Main()
        {

            Console.ForegroundColor = ConsoleColor.Cyan;
            Console.BufferHeight = 9000; //show all output in console
            int entriesFound = 0;

            //Course[] myArray = new Course[300];
            var path = @"Courses.csv"; //this file is in the debug folder, if crashes, then it is missing from debug folder of project files :'( s a d

            ArrayList myArray = new ArrayList(); // array of objects to hold the course contents


            using (var textReader = new StreamReader(path))
            {
                string line = textReader.ReadLine();
                int skipCount = 0;
                while (line != null && skipCount < 1)
                {
                    line = textReader.ReadLine();
                    skipCount++;
                }

                while (line != null)
                {
                    string[] columns = line.Split(',');

                    //perform your logic 

                    Course newCourse = new Course();


                    newCourse.CourseCode = columns[1];
                    newCourse.CourseId = columns[2];

                    newCourse.Subject = columns[0];
                    newCourse.Title = columns[3];
                    newCourse.Location = columns[7];
                    newCourse.Instructor = columns[10];
                    myArray.Add(newCourse);

                    line = textReader.ReadLine();

                    entriesFound++;

                }
            }

            //****************************************************************************************************************************
            //ANSWER TO QUESTION 1 : HERE I AM PRINTING THE ARRAY CONTENTS FOR COURSE ARRAY AFTER CONVERTING FROM CSV TO ARRAY
            //****************************************************************************************************************************

            Console.WriteLine("Here are the array contents for question 1. Sorry but you have to scroll alll the way down :(");
            foreach (Course course in myArray) //here I am printing the contents of the Course Array
            {

                Console.WriteLine("\n Course ID: " + course.CourseId + "\n Subject: " + course.Subject + "\n Course Code: " + course.CourseCode + "\n Title: " + course.Title + "\n Location: " + course.Location + "\n Instructor: " + course.Instructor);
                Console.WriteLine();
                Console.WriteLine("----------------------------------------------------------------------");
            }

            Console.WriteLine("Number of Entries Found: " + entriesFound);




            Course[] myArr = myArray.ToArray(typeof(Course)) as Course[]; //convert the arraylist to an array

            //****************************************************************************************************************************

            //ANSWER TO QUESTION 1.2 A 

            //****************************************************************************************************************************

            IEnumerable<Course> IEECourses = (from c in myArr
                                              where c.Subject == "IEE"
                                              where Int32.Parse(c.CourseCode) >= 300
                                              orderby c.Instructor ascending
                                              select c
                                              );
            Console.WriteLine("----------------------------------------------------------------------");
            Console.WriteLine("Number of items found for 1.2A: " + IEECourses.Count());
            Console.WriteLine();
            Console.WriteLine();
            Console.WriteLine("Answer for 1.2 A: ");
            foreach (Course item in IEECourses)
            {

                Console.WriteLine("Instructor = {0}, Title= {1}", item.Instructor, item.Title);
                Console.WriteLine();

            }


            //****************************************************************************************************************************

            //ANSWER TO QUESTION 1.2 B

            //****************************************************************************************************************************
            Console.WriteLine("----------------------------------------------------------------------");
            Console.WriteLine("ANSWER FOR 1.2B");

            var CourseGroups = from c in myArr
                               group c by c.Subject into sgroup
                               
                               select new { Key = sgroup.Key, Count = sgroup.Count(), Subject = sgroup,

                               subgroups= 
                                    from c2 in sgroup
                               
                                    group c2 by c2.CourseCode into finalone
                                   
                                    where finalone.Count() >=2
                                   
                                    select new {Key = finalone.Key, courses = finalone, Count = finalone.Count()}
                                    
                               };


            Console.WriteLine("----------------------------------------------------------------------");

            Console.WriteLine();

            foreach (var group1 in CourseGroups)
            {

                Console.WriteLine("group: " + group1.Key);


         
                    {
                    foreach (var item in group1.subgroups) {

                        if (item.Count >= 2)
                        {
                            Console.WriteLine("\tCourse Code: " + group1.Key + item.Key + " has 2 or more courses");
                        }
                    }
                    }
                }
            

                //****************************************************************************************************************************

                //ANSWER TO QUESTION 1.3, 1.4

                //****************************************************************************************************************************

                int instructorsFound = 0;

                //Course[] myArray = new Course[300];
                var path2 = @"Instructors.csv"; //this file is in the debug folder, if crashes, then it is missing from debug folder of project files :'( s a d

                //ArrayList instructorArray = new ArrayList(); // array of objects to hold the course contents

                List<Instructor> instructorList = new List<Instructor>();
            

                using (var textReader = new StreamReader(path2))
                {
                    string line = textReader.ReadLine();
                    int skipCount = 0;
                    while (line != null && skipCount < 1)
                    {
                        line = textReader.ReadLine();
                        skipCount++;
                    }

                    while (line != null)
                    {
                        string[] columns = line.Split(',');
                        //perform your logic 

                        Instructor newInstructor = new Instructor();


                        newInstructor.InstructorName = columns[0];
                        newInstructor.OfficeNumber = columns[1];
                        newInstructor.EmailAddress = columns[2];

                       
                        instructorList.Add(newInstructor);

                        line = textReader.ReadLine();

                        instructorsFound++;

                    }

                }





            //****************************************************************************************************************************
            //ANSWER TO QUESTION 1.4 : HERE I AM PRINTING THE ARRAY CONTENTS FOR INSTRUCTOR LIST
            //****************************************************************************************************************************


            Console.WriteLine("----------------------------------------------------------------------");
            Console.WriteLine("----------------------------------------------------------------------");
            Console.WriteLine("----------------------------------------------------------------------");
            Console.WriteLine("Here are the array contents for Instructor list");
            foreach (Instructor teacher in instructorList) //here I am printing the contents of the Course Array
            {

                Console.WriteLine("\n Name: " + teacher.InstructorName + "\n Office Number: " + teacher.OfficeNumber + "\n Email: " + teacher.EmailAddress);
                Console.WriteLine();
                Console.WriteLine("----------------------------------------------------------------------");
            }

            Console.WriteLine("Number of Entries Found: " + instructorsFound);




            Instructor[] teacherArr = instructorList.ToArray(); //convert the arraylist to an array


            //****************************************************************************************************************************

            //ANSWER TO QUESTION 1.5

            //****************************************************************************************************************************


            Console.WriteLine();
            Console.WriteLine();
            Console.WriteLine("----------------------------------------------------------------------");
            Console.WriteLine("ANSWER TO QUESTION 1.5. ALL TEACHERS EMAILS AND COURSE CODES FOR 200 LEVEL COURSES.");

            var teachergroup = from instructor in teacherArr
                               from course in myArr
                               where course.Instructor == instructor.InstructorName
                               where Int32.Parse(course.CourseCode) >= 200
                               where Int32.Parse(course.CourseCode) < 300
                               orderby course.CourseCode ascending
                               select new { coursecode = course.CourseCode, subject = course.Subject, email = instructor.EmailAddress , name = instructor.InstructorName};
                               
                               
                          


            Console.WriteLine("----------------------------------------------------------------------");

            Console.WriteLine();

            foreach (var teacher in teachergroup)
            {

                Console.WriteLine("teacher: " + teacher.name);

               

                    Console.WriteLine("\tCourse Subject: " + teacher.subject +" course code: " + teacher.coursecode + " email: " + teacher.email);


                }
            }


        }


 }
            
       
    

    
    







            



       
           


           
        
    



    
        


    

