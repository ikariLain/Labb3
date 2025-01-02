using Labb3.Data;
using Labb3.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Labb3
{
    public class StaffManager
    {
        public static void DisplayStaffRoles()
        {
            using (var context = new SchoolContext())
            {
                var roles = context.Roles.ToList();

                Console.WriteLine("All roles:");
                foreach (var role in roles)
                {
                    Console.WriteLine(role.RoleId + ". " + role.RoleName);
                }
            }
        }

        public static void DisplayStaff()
        {
            DisplayStaffRoles();

            using (var context = new SchoolContext())
            {
                Console.WriteLine("Which role do you want to see? Type a corresponding number, or 0 to view all staff.");
                string? roleChoice = Console.ReadLine();

                
                if (roleChoice == "0")
                {
                    
                    IEnumerable<IGrouping<string?, Staff>> staffByRole = context.Staff.GroupBy(s => s.Role.RoleName);
                    foreach (var group in staffByRole)
                    {
                        Console.WriteLine("");
                        Console.WriteLine($"{group.Key}:");
                        foreach (var s in group)
                        {
                            Console.WriteLine($"{s.FirstName} {s.LastName}");
                        }
                    }
                }
                
                else if (int.TryParse(roleChoice, out int roleChoiceInt) && context.Roles.ToList().Any(r => r.RoleId == roleChoiceInt))
                {
                    var staff = context.Staff.Where(s => s.RoleId == int.Parse(roleChoice)).ToList();
                    foreach (var s in staff)
                    {
                        Console.WriteLine($"{s.FirstName} {s.LastName}");
                    }
                    Console.WriteLine("\nPress any key to continue...");
                    Console.ReadLine();  
                }
                else
                {
                    Console.WriteLine("Invalid choice");
                }
            }

            Console.WriteLine("\nPress any key to continue...");
            Console.ReadLine();
            Console.Clear();
        }


        public static void AddStaff()
        {
            using (var context = new SchoolContext())
            {
                
                Console.WriteLine("Enter the staff's first name:");
                string? firstName = Console.ReadLine();
                while (string.IsNullOrWhiteSpace(firstName))
                {
                    Console.WriteLine("First name cannot be empty. Please enter a valid first name:");
                    firstName = Console.ReadLine();
                }
                
                Console.WriteLine("Enter the staff's last name:");
                string? lastName = Console.ReadLine();
                while (string.IsNullOrWhiteSpace(lastName))
                {
                    Console.WriteLine("Last name cannot be empty. Please enter a valid last name:");
                    lastName = Console.ReadLine();
                }
                
                Console.WriteLine("Available roles:");
                var roles = context.Roles.ToList();
                foreach (var role in roles)
                {
                    Console.WriteLine($"{role.RoleId}: {role.RoleName}");
                }

                Console.WriteLine("Enter the role ID for this staff member:");
                int roleChoiceInt;
                while (true)
                {
                    string? roleChoice = Console.ReadLine();
                    if (int.TryParse(roleChoice, out roleChoiceInt) && roles.Any(r => r.RoleId == roleChoiceInt))
                    {
                        break;
                    }
                    else
                    {
                        Console.WriteLine("Invalid choice. Please enter a valid role ID from the list above:");
                    }
                }

                var selectedRole = roles.FirstOrDefault(r => r.RoleId == roleChoiceInt);
                if (selectedRole == null)
                {
                    Console.WriteLine("Error: The selected role does not exist.");
                    return;
                }

                var newStaff = new Staff
                {
                    FirstName = firstName,
                    LastName = lastName,
                    RoleId = roleChoiceInt,
                    Role = selectedRole
                };

                context.Staff.Add(newStaff);
                context.SaveChanges();

                Console.WriteLine("Staff added successfully.");
            }

            Console.WriteLine("\nPress any key to continue...");
            Console.ReadLine();
            Console.Clear();
        }
    }
}
