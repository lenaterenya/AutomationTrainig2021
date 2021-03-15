using System.Collections.Generic;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Support.UI;

namespace MultipleSelection
{
    public class Table
    {
        IWebDriver _driver;

        private readonly By _tableXpath = By.XPath("//table/tbody");
        private readonly By _rowTagName = By.TagName("tr");
        private readonly By _cellTagName = By.TagName("td");
        private readonly By _nextButtonId = By.Id("example_next");
        private readonly By _selectName = By.Name("example_length");

        public Table(IWebDriver driver)
        {
            _driver = driver;
        }


        public List<Employee> FetchEmployeesByAgeAndSalary(int definiteAge, int definiteSalary)
        {
            var employees = new List<Employee>();

           while (true)
           {
               var table = _driver.FindElement(_tableXpath);
               var rows = table.FindElements(_rowTagName);
               var nextButton = _driver.FindElement(_nextButtonId);

               foreach (var row in rows)
               {
                   var cells = row.FindElements(_cellTagName);
                   var name = cells[0].Text;
                   var position = cells[1].Text;
                   var office = cells[2].Text;
                   var age = int.Parse(cells[3].Text);
                   var salary = int.Parse(cells[5].GetAttribute("data-order"));

                   if ((age > definiteAge) && (salary <= definiteSalary))
                   {
                       employees.Add(new Employee(name,position,office));
                   }

               }

               if (nextButton.GetAttribute("class").Equals("paginate_button next disabled"))
               {
                   break;
               }
               nextButton.Click();
           }

           return employees;
        }

        public Table TableShowEntriesSetup(string value)
        {
            var select = _driver.FindElement(_selectName);
            SelectElement selectElement = new SelectElement(select);
            selectElement.SelectByValue(value);

            return this;
        }
    }
}
