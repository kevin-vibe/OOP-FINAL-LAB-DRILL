using System;
using System.Collections.Generic;
using System.Linq;

 
// PERSON CLASS (PARENT)
// Stores common personal information.
 
class Person
{
    public string FirstName { get; set; }
    public string Surname { get; set; }
    public string Gender { get; set; }

    public Person(string firstName, string surname, string gender)
    {
        FirstName = firstName;
        Surname = surname;
        Gender = gender;
    }
}

// 
// STUDENT CLASS (CHILD)
// Inherits personal information from Person.
// Handles student information.
// 
class Student : Person
{
    public string StudentNumber { get; set; }
    public string Occupation { get; set; }
    public string CountryCode { get; set; }
    public string AreaCode { get; set; }
    public string PhoneNumber { get; set; }

    public Student(
        string studentNumber,
        string surname,
        string firstName,
        string occupation,
        string gender,
        string countryCode,
        string areaCode,
        string phoneNumber)
        : base(firstName, surname, gender)
    {
        StudentNumber = studentNumber;
        Occupation = occupation;
        CountryCode = countryCode;
        AreaCode = areaCode;
        PhoneNumber = phoneNumber;
    }

    // Get the complete phone number.
    public string GetCompletePhoneNumber()
    {
        return "+" + CountryCode + " " + AreaCode + " " + PhoneNumber;
    }

    // Display student information.
    public void DisplayInformation()
    {
        Console.WriteLine("----------------------------------------");
        Console.WriteLine("Student Number : " + StudentNumber);
        Console.WriteLine("Name           : " + FirstName + " " + Surname);
        Console.WriteLine("Occupation     : " + Occupation);
        Console.WriteLine("Gender         : " + Gender);
        Console.WriteLine("Country        : " + Country.GetCountryName(CountryCode));
        Console.WriteLine("Phone Number   : " + GetCompletePhoneNumber());
    }
}

// 
// COUNTRY CLASS
// Handles the supported ASEAN countries.
// 
class Country
{
    public string Name { get; set; }
    public string Code { get; set; }

    public Country(string name, string code)
    {
        Name = name;
        Code = code;
    }

    // Retrieve all supported countries.
    public static List<Country> GetCountries()
    {
        return new List<Country>
        {
            new Country("Malaysia", "60"),
            new Country("Indonesia", "62"),
            new Country("Philippines", "63"),
            new Country("Singapore", "65"),
            new Country("Thailand", "66")
        };
    }

    // Get country name using its code.
    public static string GetCountryName(string code)
    {
        Country? country = GetCountries()
            .FirstOrDefault(c => c.Code == code);

        return country == null ? "Unknown Country" : country.Name;
    }

    // Check whether the country code is valid.
    public static bool IsValidCode(string code)
    {
        return GetCountries().Any(c => c.Code == code);
    }
}

// 
// PHONEBOOK CLASS
// Manages the collection of student records.
// 
class Phonebook
{
    private List<Student> students = new List<Student>();

    // Add a new student.
    public bool AddStudent(Student student)
    {
        if (FindStudent(student.StudentNumber) != null)
        {
            return false;
        }

        students.Add(student);
        return true;
    }

    // Find a student using their student number.
    public Student? FindStudent(string studentNumber)
    {
        return students.FirstOrDefault(
            s => s.StudentNumber.Equals(
                studentNumber,
                StringComparison.OrdinalIgnoreCase));
    }

    // Check whether a student number is already used.
    public bool IsStudentNumberTaken(string studentNumber)
    {
        return FindStudent(studentNumber) != null;
    }

    // Retrieve all students alphabetically by surname.
    public List<Student> GetAllStudents()
    {
        return students
            .OrderBy(s => s.Surname)
            .ThenBy(s => s.FirstName)
            .ToList();
    }

    // Retrieve students from selected countries.
    public List<Student> GetStudentsByCountryCodes(List<string> countryCodes)
    {
        return students
            .Where(s => countryCodes.Contains(s.CountryCode))
            .OrderBy(s => s.Surname)
            .ThenBy(s => s.FirstName)
            .ToList();
    }
}


// 
// INPUT HELPER CLASS
// Handles input and validation methods.
// 
class InputHelper
{
    // Read input that cannot be empty.
    public static string ReadRequired(string message)
    {
        while (true)
        {
            Console.Write(message);
            string input = (Console.ReadLine() ?? "").Trim();

            if (!string.IsNullOrWhiteSpace(input))
            {
                return input;
            }

            Console.WriteLine("This field cannot be empty.");
        }
    }

    // Read and validate gender.
    public static string ReadGender()
    {
        while (true)
        {
            Console.Write("Gender (M/F): ");
            string gender = (Console.ReadLine() ?? "").Trim().ToUpper();

            if (gender == "M" || gender == "F")
            {
                return gender;
            }

            Console.WriteLine("Invalid gender. Please enter M or F.");
        }
    }

    // Read and validate country code.
    public static string ReadCountryCode()
    {
        while (true)
        {
            Console.Write("Enter Country Code: ");
            string code = (Console.ReadLine() ?? "").Trim();

            if (Country.IsValidCode(code))
            {
                return code;
            }

            Console.WriteLine("Invalid country code. Please select one of the listed countries.");
        }
    }
}


// 
// PHONEBOOK UI CLASS
// Handles user interaction and phonebook
// operations storing, editing,
// and searching student records.
//
class PhonebookUI
{
    private Phonebook phonebook;

    // Constructor receives the Phonebook object.
    public PhonebookUI(Phonebook phonebook)
    {
        this.phonebook = phonebook;
    }


    // 
    // STORE STUDENT
    // Collects information and adds a record.
    // 
    public void StoreStudent()
    {
        Console.WriteLine("\n========== STORE STUDENT ==========");

        string studentNumber =
            InputHelper.ReadRequired("Student Number: ");

        if (phonebook.IsStudentNumberTaken(studentNumber))
        {
            Console.WriteLine("A student with that number already exists.");
            return;
        }

        string surname = InputHelper.ReadRequired("Surname: ");
        string firstName = InputHelper.ReadRequired("First Name: ");
        string occupation = InputHelper.ReadRequired("Occupation: ");
        string gender = InputHelper.ReadGender();

        Console.WriteLine("\nAvailable Countries:");

        foreach (Country country in Country.GetCountries())
        {
            Console.WriteLine(country.Name + " - " + country.Code);
        }

        string countryCode = InputHelper.ReadCountryCode();
        string areaCode = InputHelper.ReadRequired("Area Code: ");
        string phoneNumber = InputHelper.ReadRequired("Phone Number: ");

        Student newStudent = new Student(
            studentNumber,
            surname,
            firstName,
            occupation,
            gender,
            countryCode,
            areaCode,
            phoneNumber
        );

        if (phonebook.AddStudent(newStudent))
        {
            Console.WriteLine("\nStudent record successfully stored!");
        }
        else
        {
            Console.WriteLine("\nUnable to store student record.");
        }
    }


    // 
    // EDIT STUDENT
    // Finds a student and updates a field.
    // 
    public void EditStudent()
    {
        Console.WriteLine("\n========== EDIT STUDENT ==========");

        string studentNumber =
            InputHelper.ReadRequired("Enter Student Number: ");

        Student? student = phonebook.FindStudent(studentNumber);

        if (student == null)
        {
            Console.WriteLine("Student record not found.");
            return;
        }

        Console.WriteLine("\nStudent Found:");
        student.DisplayInformation();

        while (true)
        {
            Console.WriteLine("\n========== EDIT MENU ==========");
            Console.WriteLine("1. Student Number");
            Console.WriteLine("2. Surname");
            Console.WriteLine("3. Gender");
            Console.WriteLine("4. Occupation");
            Console.WriteLine("5. Country Code");
            Console.WriteLine("6. Area Code");
            Console.WriteLine("7. Phone Number");
            Console.WriteLine("8. Return");
            Console.WriteLine("===============================");
            Console.Write("Select field to edit: ");

            string choice = Console.ReadLine() ?? "";

            switch (choice)
            {
                case "1":
                    string newStudentNumber =
                        InputHelper.ReadRequired("Enter New Student Number: ");

                    if (!student.StudentNumber.Equals(
                        newStudentNumber,
                        StringComparison.OrdinalIgnoreCase)
                        && phonebook.IsStudentNumberTaken(newStudentNumber))
                    {
                        Console.WriteLine("That student number is already being used.");
                    }
                    else
                    {
                        student.StudentNumber = newStudentNumber;
                        Console.WriteLine("Student number updated successfully.");
                    }
                    break;

                case "2":
                    student.Surname =
                        InputHelper.ReadRequired("Enter New Surname: ");

                    Console.WriteLine("Surname updated successfully.");
                    break;

                case "3":
                    student.Gender = InputHelper.ReadGender();

                    Console.WriteLine("Gender updated successfully.");
                    break;

                case "4":
                    student.Occupation =
                        InputHelper.ReadRequired("Enter New Occupation: ");

                    Console.WriteLine("Occupation updated successfully.");
                    break;

                case "5":
                    Console.WriteLine("\nAvailable Countries:");

                    foreach (Country country in Country.GetCountries())
                    {
                        Console.WriteLine(country.Name + " - " + country.Code);
                    }

                    student.CountryCode = InputHelper.ReadCountryCode();

                    Console.WriteLine("Country code updated successfully.");
                    break;

                case "6":
                    student.AreaCode =
                        InputHelper.ReadRequired("Enter New Area Code: ");

                    Console.WriteLine("Area code updated successfully.");
                    break;

                case "7":
                    student.PhoneNumber =
                        InputHelper.ReadRequired("Enter New Phone Number: ");

                    Console.WriteLine("Phone number updated successfully.");
                    break;

                case "8":
                    return;

                default:
                    Console.WriteLine("Invalid choice. Please try again.");
                    break;
            }

            Console.WriteLine("\nUpdated Student Information:");
            student.DisplayInformation();
        }
    }