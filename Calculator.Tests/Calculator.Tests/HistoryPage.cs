using System;
using System.Collections.Generic;
using System.Linq;
using OpenQA.Selenium;

namespace Calculator.Tests.Pages
{
    public class HistoryPage
    {
        private IWebDriver driver;

        public HistoryPage(IWebDriver driver)
        {
            this.driver = driver;
        }

        public IWebElement CalculatorBtn => driver.FindElement(By.XPath("//button[text()='Calculator']"));

        public IWebElement ClearBtn => driver.FindElement(By.XPath("//button[@id = 'clear']"));

        public IWebElement HeaderAmount => driver.FindElement(By.XPath("//tr[@class = 'header']//th[text() = 'Amount']"));

        public IWebElement CalculatedData => driver.FindElement(By.XPath("//tr[@class = 'data']//td[text() = '100']"));

        //many
        public int RowCount => driver.FindElements(By.XPath("//tr[@class = 'data']")).Count;
        
        //select specific row
        public List<string> GetRowData(int rowNumber)
        {
            List<string> result = new List<string>();
            List<IWebElement> cells = driver.FindElements(By.XPath("//tr[@class = 'data']["+ rowNumber +"]//td")).ToList();
            foreach (IWebElement cell in cells)
            {
                result.Add(cell.Text);
            }     
            return result;
        }
    }

}