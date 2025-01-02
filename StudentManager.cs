using Labb3.Data;
using Labb3.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Labb3
{
    public class StudentManager
    {
        public static void DisplayStudents()
        {
            /* Display all students.
             * The user can choose whether they want to see the students
             * sorted by first or last name and whether it should be
             * sorted in ascending or descending order.
             */
            using (var context = new SchoolContext())
            {
                Console.WriteLine("Do you want to sort the students by first or last name?");
                Console.WriteLine("1. First name");
                Console.WriteLine("2. Last name");
                string? sortChoice = Console.ReadLine();
                Console.WriteLine("");
                switch (sortChoice)
                {
                    // Sort students by first name
                    case "1":
                        Console.WriteLine("Do you want to sort the students in ascending or descending order?");
                        Console.WriteLine("1. Ascending");
                        Console.WriteLine("2. Descending");
                        string? firstNameOrderChoice = Console.ReadLine();
                        Console.WriteLine("");
                        switch (firstNameOrderChoice)
                        {
                            // Sort students by first name, ascending
                            case "1":
                                var students = context.Students.OrderBy(s => s.FirstName).ToList();
                                foreach (var student in students)
                                {
                                    Console.WriteLine(student.FirstName + " " + student.LastName);
                                }
                                break;
                            // Sort students by first name, descending
                            case "2":
                                var students2 = context.Students.OrderByDescending(s => s.FirstName).ToList();
                                foreach (var student in students2)
                                {
                                    Console.WriteLine(student.FirstName + " " + student.LastName);
                                }
                                break;
                            default:
                                Console.WriteLine("Invalid choice");
                                break;
                        }
                        break;
                    // Sort students by last name
                    case "2":
                        Console.WriteLine("Do you want to sort the students in ascending or descending order?");
                        Console.WriteLine("1. Ascending");
                        Console.WriteLine("2. Descending");
                        string? orderChoice = Console.ReadLine();
                        Console.WriteLine("");
                        switch (orderChoice)
                        {
                            // Sort students by last name, ascending
                            case "1":
                                var students = context.Students.OrderBy(s => s.LastName).ToList();
                                foreach (var student in students)
                                {
                                    Console.WriteLine(student.FirstName + " " + student.LastName);
                                }
                                break;
                            // Sort students by last name, descending
                            case "2":
                                var students2 = context.Students.OrderByDescending(s => s.LastName).ToList();
                                foreach (var student in students2)
                                {
                                    Console.WriteLine(student.FirstName + " " + student.LastName);
                                }
                                break;
                            default:
                                Console.WriteLine("Invalid choice");
                                break;
                        }
                        break;
                    default:
                        Console.WriteLine("Invalid choice");
                        break;
                }
            }

            Console.WriteLine("\nPress any key to continue...");
            Console.ReadLine();
            Console.Clear();

        }

        public static void DisplayAllCourses()
        {
            using (var context = new SchoolContext())
            {
                // Prints a list of all courses
                var courses = context.Courses.ToList();
                foreach (var c in courses)
                {
                    Console.WriteLine(c.CourseId + ". " + c.CourseName);
                }
            }
        }

        public static void DisplayStudentsInCourse()
        {
            /* Display all students in a certain class.
             * The user must first see a list of all the classes that exist,
             * then the user can choose one of the classes and then
             * all the students in that class will be printed.
             */
            DisplayAllCourses();

            using (var context = new SchoolContext())
            {
                Console.WriteLine("Which course do you want to see? Type a corresponding number.");
                string? courseChoice = Console.ReadLine();
                Console.WriteLine("");

                // Fetch all courses
                var courses = context.Courses.ToList();

                // Try to parse the input and check if the course exists
                if (int.TryParse(courseChoice, out int courseChoiceInt) && courses.Any(c => c.CourseId == courseChoiceInt))
                {
                    // Fetch students for the selected course
                    var studentsInCourse = context.Grades
                        .Where(g => g.CourseId == courseChoiceInt)
                        .Select(g => g.Student)
                        .Distinct() // Ensures no duplicate students in case of multiple grades
                        .ToList();

                    // Get the course name
                    string courseName = courses.First(c => c.CourseId == courseChoiceInt).CourseName;
                    Console.WriteLine($"Students in {courseName}:");

                    // Check if there are students enrolled in the course
                    if (studentsInCourse.Any())
                    {
                        // Print all students
                        foreach (var student in studentsInCourse)
                        {
                            Console.WriteLine($"{student.FirstName} {student.LastName}");
                        }
                    }
                    else
                    {
                        Console.WriteLine("No students are enrolled in this course.");
                    }
                }
                else
                {
                    Console.WriteLine("Invalid choice. Please select a valid course ID.");
                }
            }

            Console.WriteLine("\nPress any key to continue...");
            Console.ReadLine();
            Console.Clear();
        }




        public static void DisplayGradesFromCourse()
        {
            /* Display a list of all courses and the average grade
             * that the students got in that course as well as the highest
             * and lowest grade that someone got in the course.
             * The user immediately gets a list of all courses in the database,
             * the average grade and the highest and lowest grade for each course.
             */
            DisplayAllCourses();
            using (var context = new SchoolContext())
            {
                Console.WriteLine("Which course do you want to see? Type a corresponding number.");
                string? courseChoice = Console.ReadLine();
                Console.WriteLine("");

                // Fetch all courses
                var courses = context.Courses.ToList();

                // Try to parse the input and check if the course exists
                if (int.TryParse(courseChoice, out int courseChoiceInt) && courses.Any(c => c.CourseId == courseChoiceInt))
                {
                    // Fetch grades for the selected course
                    var grades = context.Grades
                        .Where(g => g.CourseId == courseChoiceInt)
                        .Select(g => g.Value)
                        .ToList();

                    // Grade to numeric mapping
                    Dictionary<string, int> gradeToPoints = new Dictionary<string, int>
            {
                { "A", 5 },
                { "B", 4 },
                { "C", 3 },
                { "D", 2 },
                { "E", 1 },
                { "F", 0 }
            };

                    // Numeric to grade mapping
                    Dictionary<int, string> pointsToGrade = new Dictionary<int, string>
            {
                { 5, "A" },
                { 4, "B" },
                { 3, "C" },
                { 2, "D" },
                { 1, "E" },
                { 0, "F" }
            };

                    // Ensure valid grades and calculate statistics
                    var validGrades = grades
                        .Where(grade => gradeToPoints.ContainsKey(grade))
                        .Select(grade => gradeToPoints[grade])
                        .ToList();

                    if (validGrades.Count > 0)
                    {
                        double averagePoints = validGrades.Average();
                        int roundedPoints = (int)Math.Round(averagePoints);

                        string averageGrade = pointsToGrade.ContainsKey(roundedPoints) ? pointsToGrade[roundedPoints] : "?";
                        int highestPoints = validGrades.Max();
                        int lowestPoints = validGrades.Min();

                        string highestGrade = pointsToGrade[highestPoints];
                        string lowestGrade = pointsToGrade[lowestPoints];

                        Console.WriteLine($"Average Points: {averagePoints:F2}");
                        Console.WriteLine($"Average Grade: {averageGrade}");
                        Console.WriteLine($"Highest Grade: {highestGrade}");
                        Console.WriteLine($"Lowest Grade: {lowestGrade}");
                    }
                    else
                    {
                        Console.WriteLine("No valid grades available for this course.");
                    }
                }
                else
                {
                    Console.WriteLine("Invalid choice.");
                }
            }

            Console.WriteLine("\nPress any key to continue...");
            Console.ReadLine();
            Console.Clear();
        }



        public static void AddStudent()
        {
            using (var context = new SchoolContext())
            {
                // Prompt for first name
                Console.WriteLine("Enter the student's first name:");
                string? firstName = Console.ReadLine();
                while (string.IsNullOrWhiteSpace(firstName))
                {
                    Console.WriteLine("First name cannot be empty. Please enter a valid first name:");
                    firstName = Console.ReadLine();
                }

                // Prompt for last name
                Console.WriteLine("Enter the student's last name:");
                string? lastName = Console.ReadLine();
                while (string.IsNullOrWhiteSpace(lastName))
                {
                    Console.WriteLine("Last name cannot be empty. Please enter a valid last name:");
                    lastName = Console.ReadLine();
                }

                // Prompt for Social Security Number
                Console.WriteLine("Enter the student's personal number (YYYYMMDDNNNN):");
                long socialSecurityNumber;
                while (true)
                {
                    string? input = Console.ReadLine();
                    if (long.TryParse(input, out socialSecurityNumber) && input.Length == 12)
                    {
                        if (context.Students.Any(s => s.SocialSecurityNumber == socialSecurityNumber))
                        {
                            Console.WriteLine("A student with that personal number already exists. Please enter a different personal number (YYYYMMDDNNNN):");
                        }
                        else
                        {
                            break;
                        }
                    }
                    else
                    {
                        Console.WriteLine("Invalid format. Please enter a 12-digit personal number (YYYYMMDDNNNN):");
                    }
                }

                // Display available classes
                Console.WriteLine("Available classes:");
                var classes = context.Classes.ToList();
                foreach (var cls in classes)
                {
                    Console.WriteLine($"{cls.ClassId}: {cls.Name}");
                }

                // Prompt for ClassId
                Console.WriteLine("Enter the class ID for the student:");
                int classId;
                while (true)
                {
                    string? input = Console.ReadLine();
                    if (int.TryParse(input, out classId) && classes.Any(c => c.ClassId == classId))
                    {
                        break;
                    }
                    else
                    {
                        Console.WriteLine("Invalid class ID. Please enter a valid class ID from the list above:");
                    }
                }

                // Create a new student object
                var newStudent = new Student
                {
                    FirstName = firstName,
                    LastName = lastName,
                    SocialSecurityNumber = socialSecurityNumber,
                    ClassId = classId
                };

                // Add and save the new student
                context.Students.Add(newStudent);
                context.SaveChanges();

                Console.WriteLine("Student added successfully.");
            }

            Console.WriteLine("\nPress any key to continue...");
            Console.ReadLine();
            Console.Clear();
        }
    }
}
