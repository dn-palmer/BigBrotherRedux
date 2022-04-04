//Inside SeleniumTest.cs

using NUnit.Framework;

using OpenQA.Selenium;

using OpenQA.Selenium.Chrome;

using OpenQA.Selenium.Firefox;

using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;

using System.IO;
using System.Net.Http;
using BBDisplay.Classes;
using System.Threading.Tasks;

namespace TestingProject
{

    public class Tests

    {

        IWebDriver driver;

        [OneTimeSetUp]

        public void Setup()

        {

            //Below code is to get the drivers folder path dynamically.

            //You can also specify chromedriver.exe path dircly ex: C:/MyProject/Project/drivers

            string path = Directory.GetParent(Environment.CurrentDirectory).Parent.Parent.FullName;

            //Creates the ChomeDriver object, Executes tests on Google Chrome

            driver = new ChromeDriver(path + @"\drivers\");

            //If you want to Execute Tests on Firefox uncomment the below code

            // Specify Correct location of geckodriver.exe folder path. Ex: C:/Project/drivers

            //driver= new FirefoxDriver(path + @"\drivers\");

        }
        #region HomePage

        [Test]
        public void HomeNavHome()
        {

        }

        [Test]
        public void HomeNavIPButtonToUserIPPage()
        {

        }

        [Test]
        public void HomeNavPageRefButtonToPageRefPage()
        {

        }


        [Test]
        public void HomeNavUserInteractionButtonToUserInteractionPage()
        {

        }

        [Test]
        public void HomeNavSessionsButtonToSessionsPage()
        {
            driver.Navigate().GoToUrl("https://localhost:7281");


            var element = driver.FindElements(By.ClassName("rightDiv"));
            element[0].Click();

            Assert.AreEqual(driver.Url, "https://localhost:7281/UserIPDatas");
        }

        [Test]
        public void HomePurpleNavIPButtonToUserIPPage()
        {
            driver.Navigate().GoToUrl("https://localhost:7281");


            var element = driver.FindElements(By.ClassName("rightDiv"));
            element[0].Click();

            Assert.AreEqual(driver.Url, "https://localhost:7281/UserIPDatas");


        }

        [Test]
        public void HomePurpleNavPageRefButtonToPageRefPage()
        {
            driver.Navigate().GoToUrl("https://localhost:7281");


            var element = driver.FindElements(By.ClassName("rightDiv"));

            element[3].Click();

            Assert.AreEqual(driver.Url, "https://localhost:7281/PageReference");

        }


        [Test]
        public void HomePurpleNavUserInteractionButtonToUserInteractionPage()
        {
            driver.Navigate().GoToUrl("https://localhost:7281");


            var element = driver.FindElements(By.ClassName("rightDiv"));

            element[1].Click();

            Assert.AreEqual(driver.Url, "https://localhost:7281/UserInteractions");
        }

        [Test]
        public void HomePurpleSessionsButtonToSessionsPage()
        {
            driver.Navigate().GoToUrl("https://localhost:7281");


            var element = driver.FindElements(By.ClassName("rightDiv"));

            element[2].Click();

            Assert.AreEqual(driver.Url, "https://localhost:7281/Sessions");


        }

        #endregion

        #region PageRefTests
        [Test]
        public void PageRefEntries()
        {

            Task.Run(async () =>
            {

                driver.Navigate().GoToUrl("https://localhost:7281/PageReferences");


                //Make sure the details button is displaying
                Assert.IsTrue(driver.FindElement(By.ClassName("detailsButton")).Displayed);




                //HttpClient client = new HttpClient();
                //PageReferenceClean cleaner = new PageReferenceClean();
                //Make sure text is correct

                /*
                var data = await client.GetStringAsync("http://52.168.32.232/BigBrotherRedux/PageReference/ReadAll");
                data = cleaner.RemoveSquareBraces(data);
                List<string> pageInf = cleaner.PreppedData(cleaner.CleanAPIResponse(data));
                var model = cleaner.IndexPrepPageReference(pageInf);
                foreach (var text in model)
                {
                    Console.WriteLine(text);
                }*/
/*
                List<IWebElement> lstTdElem = new List<IWebElement>(driver.FindElements(By.TagName("td")));
                if (lstTdElem.Count > 0)
                {
                    for (int i=0; i < lstTdElem.Count; i++)
                    {
                        Assert.Equals(lstTdElem[i], model[i]);

                    }
                }*/
            }
            
            ).GetAwaiter().GetResult();
           
        }

        [Test]

        public void DetailsButton()
        {
            driver.Navigate().GoToUrl("https://localhost:7281/PageReferences");


            //Make sure the create new button is displaying for all entries
            Assert.IsTrue(driver.FindElement(By.ClassName("detailsButton")).Displayed);


            //Make sure each button is going to the correct entry
            ReadOnlyCollection<IWebElement> Entries = driver.FindElements(By.ClassName("detailsButton"));

            for (int i = 0; i < Entries.Count; i++)
            {
                Assert.Equals(Entries[i], $"/PageReferences/Details/{i}");
            }
           
        }

        [Test]

        public void CreateButton()
        {
            driver.Navigate().GoToUrl("https://localhost:7281/PageReferences");


            //Make sure the create new button is displaying for all entries
            Assert.IsTrue(driver.FindElement(By.ClassName("newEntry")).Displayed);

            //Make sure each button is going to the correct entry
            ReadOnlyCollection<IWebElement> Entries = driver.FindElements(By.ClassName("detailsButton"));

            for (int i = 0; i < Entries.Count; i++)
            {
                Assert.Equals(Entries[i], $"/PageReferences/Create");
            }
        }

        [Test]

        public void DeleteButton()
        {
            driver.Navigate().GoToUrl("https://localhost:7281/PageReferences");


            //Make sure the create new button is displaying for all entries
            Assert.IsTrue(driver.FindElement(By.ClassName("deleteButton")).Displayed);


            //Make sure each button is going to the correct entry
            ReadOnlyCollection<IWebElement> Entries = driver.FindElements(By.ClassName("deleteButton"));

            for (int i = 0; i < Entries.Count; i++)
            {
                Assert.Equals(Entries[i], $"/PageReferences/Delete/{i}");
            }
        }

        [Test]

        public void DeleteEntry()
        {
            //navigate to the page reference page from the home page

            //check entries on page

            //Select delte entry

            //Select back

            //ensure entries are the same
        }

        [Test]

        public void BackDetails()
        {
            //navigate to the page reference page from the home page

            //check entries on page

            //Select delte entry

            //Select back

            //ensure entries are the same

        }

        [Test]

        public void BackDelete()
        {
            //navigate to the page reference page from the home page

            //check entries on page

            //Select delte entry

            //Select back

            //ensure entries are the same
        }

        [Test]

        public void BackCreate()
        {
            //navigate to the page reference page from the home page


            //check entries on page

            //Select create entry

            //Select back

            //ensure entries are the same
        }
        #endregion PageRefTests

        [Test]

        public void verifyLogo()
        {

            driver.Navigate().GoToUrl("https://localhost:7281/");

            Assert.IsTrue(driver.FindElement(By.ClassName("rightDiv")).Displayed);

        }

        [OneTimeTearDown]

        public void TearDown()

        {

            driver.Quit();

        }

    }

}