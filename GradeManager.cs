using Labb3;
using Labb3.Data;
using Labb3.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.ComponentModel.Design;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Labb3
{
    public class GradeManager
    {
        public static void DisplayGradesFromLastMonth()
        {
           
            using (var context = new SchoolContext())
            {
                DateOnly lastMonth = DateOnly.FromDateTime(DateTime.Now.AddDays(-30));

                
                var grades = context.Grades
                    .Include(g => g.Student)   
                    .Include(g => g.Course)    
                    .Include(g => g.Staff)     
                    .Where(g => g.Date > lastMonth) 
                    .ToList();

                if (grades == null || grades.Count == 0)
                {
                    Console.WriteLine("No grades set in the last month.");
                }
                else
                {
                    foreach (var grade in grades)
                    {
                        Console.WriteLine($"{grade.Student.FirstName} {grade.Student.LastName} - " +
                                          $"{grade.Course.CourseName}: {grade.Value}. " +
                                          $"Graded on {grade.Date:yyyy-MM-dd}");
                    }
                }
            }

            Console.WriteLine("\nPress any key to continue...");
            Console.ReadLine();
            Console.Clear(); 
        }
    }
}
