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
