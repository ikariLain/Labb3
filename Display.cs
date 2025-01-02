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

namespace Lab_3_Database
{
    public class Display
    {
        public static void Run()
        {
            while (true)
            {
                ShowMenu();

                string input = Console.ReadLine();

                if (Enum.TryParse(input, out MenuOption option))
                {
                    HandleMenuOption(option);
                }
                else
                {
                    Console.WriteLine("Invalid choice. Please select a valid option.");
                }
            }
        }
        
        public enum MenuOption
        {
            DisplayAllStaff =1,
            DisplayStudent,
            DisplayStudentsFromClass,
            DisplayGradesFromLastMonth,
            DisplayGradesFromCourse,
            AddStudent,
            AddStaff,
            Exit    
        }

        private static void ShowMenu()
        {
            Console.WriteLine("Menu options:");
            Console.WriteLine("1 - Display all staff");
            Console.WriteLine("2 - Display all students");
            Console.WriteLine("3 - Display all students from a class");
            Console.WriteLine("4 - Display all grades from set last month");
            Console.WriteLine("5 - Display all grades from one course");
            Console.WriteLine("6 - Add a new student value");
            Console.WriteLine("7 - Add a new staff value");
            Console.WriteLine("8 - Exit");
        }

        private static void HandleMenuOption (MenuOption option)
        {
            switch (option)
            {
                case MenuOption.DisplayAllStaff:
                    StaffManager.DisplayStaff();
                    break;
                case MenuOption.DisplayStudent:
                    StudentManager.DisplayStudents();
                    break;
                case MenuOption.DisplayStudentsFromClass:
                    StudentManager.DisplayStudentsInCourse();
                    break;
                case MenuOption.DisplayGradesFromLastMonth:
                    GradeManager.DisplayGradesFromLastMonth();
                    break;
                case MenuOption .DisplayGradesFromCourse:
                    StudentManager.DisplayGradesFromCourse();
                    break;
                case MenuOption.AddStudent:
                    StudentManager.AddStudent();
                    break;
                case MenuOption.AddStaff:
                    StaffManager.AddStaff();
                    break;
                case MenuOption.Exit:
                    return;
                default:
                    Console.WriteLine("Invalid choice");
                    break;
            }
            Console.WriteLine();
        }
    }
}

