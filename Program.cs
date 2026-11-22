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