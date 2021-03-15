using System;
using System.Collections.Generic;
using System.Text;

namespace MultipleSelection
{
    public class Employee
    {
        public string Name { get; set; }
        public string Position { get; set; }
        public string Office { get; set; }

        public Employee(string name, string position, string office)
        {
            Name = name;
            Position = position;
            Office = office;
        }
    }
}
