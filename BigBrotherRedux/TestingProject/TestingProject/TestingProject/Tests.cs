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
            driver.Navigate().GoToUrl("http://34.125.84.24/Identity/Account/Login");
            IWebElement username = driver.FindElement(By.Name("Input.Email"));
            username.SendKeys("lonewolf@email.com");
            IWebElement password = driver.FindElement(By.Name("Input.Password"));
            password.SendKeys("Passw0rd!");
            var element = driver.FindElements(By.Id("login-submit"));
            element[0].Click();

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

                driver.Navigate().GoToUrl("http://34.125.84.24/PageReferences");



                HttpClient client = new HttpClient();
                PageReferenceClean cleaner = new PageReferenceClean();
                //Make sure text is correct

                
                var data = await client.GetStringAsync("http://34.125.193.123/BigBrotherRedux/PageReference/ReadAll");
                data = cleaner.RemoveSquareBraces(data);
                List<string> pageInf = cleaner.PreppedData(cleaner.CleanAPIResponse(data));
                var model = cleaner.IndexPrepPageReference(pageInf);
           

                List<IWebElement> lstTdElem = new List<IWebElement>(driver.FindElements(By.TagName("td")));

                for (int i = 0; i < lstTdElem.Count; i++)
                {
                    Console.WriteLine("element: " + lstTdElem[i].Text);
                }
                if (lstTdElem.Count > 0)
                {
                    for (int i=0; i < model.Count; i++)
                    {
                        Assert.AreEqual(lstTdElem[(3 * i)].Text, model[i].DateAdded);
                        Assert.AreEqual(lstTdElem[1 + (3 * i)].Text, model[i].PageDescription);

                    }
                }
            }
            
            ).GetAwaiter().GetResult();
           
        }

        [Test]
        [TestCase(0)]
        [TestCase(1)]
        [TestCase(2)]
        [TestCase(3)]

        public void PageRefDetailsButton(int index)
        {

            Task.Run(async () =>
            {
                driver.Navigate().GoToUrl("http://34.125.84.24/PageReferences");


                //Make sure the create new button is displaying for all entries
                Assert.IsTrue(driver.FindElement(By.ClassName("detailsButton")).Displayed);


                //Make sure each button is going to the correct entry
                ReadOnlyCollection<IWebElement> Entries = driver.FindElements(By.ClassName("detailsButton"));



                HttpClient client = new HttpClient();
                PageReferenceClean cleaner = new PageReferenceClean();
                //Make sure text is correct
                var data = await client.GetStringAsync("http://34.125.193.123/BigBrotherRedux/PageReference/ReadAll");
                data = cleaner.RemoveSquareBraces(data);
                List<string> pageInf = cleaner.PreppedData(cleaner.CleanAPIResponse(data));
                var model = cleaner.IndexPrepPageReference(pageInf);


                driver.Navigate().GoToUrl($"http://34.125.84.24/PageReferences/Details/{model[index].PageId}");
       
            }

            ).GetAwaiter().GetResult();

        }

        [Test]
        [TestCase("1-1-2022", "The Pet Stores home page")]
        [TestCase("2-1-2022", "The Pet Stores animal page")]
        [TestCase("2-1-2022", "The Pet Stores merchandise page")]
        [TestCase("2-1-2022", "The Pet Stores login page")]

        public void PageRefCreateButton(string dateAdded, string description)
        {
            driver.Navigate().GoToUrl("http://34.125.84.24/PageReferences");


            //Make sure the create new button is displaying for all entries
            Assert.IsTrue(driver.FindElement(By.ClassName("newEntry")).Displayed);

            //Make sure each button is going to the correct entry
            ReadOnlyCollection<IWebElement> Entries = driver.FindElements(By.ClassName("newEntry"));

            for (int i = 0; i < Entries.Count; i++)
            {
                Console.WriteLine(Entries[i].GetAttribute("href"));
                Assert.AreEqual(Entries[i].GetAttribute("href"), $"http://34.125.84.24/PageReferences/Create");
            }

            driver.Navigate().GoToUrl("http://34.125.84.24/PageReferences/Create");

            IWebElement dateAddedWeb = driver.FindElement(By.Name("DateAdded"));
            dateAddedWeb.SendKeys(dateAdded);

            IWebElement pageDesWeb = driver.FindElement(By.Name("PageDescription"));
            pageDesWeb.SendKeys(description);


            var element = driver.FindElements(By.CssSelector("input[class = 'btn btn-primary']"));
            element[0].Click();

        }

        [Test]

        public void PageRefDeleteButton()
        {
            Task.Run(async () =>
            {
                driver.Navigate().GoToUrl("http://34.125.84.24/PageReferences");


                //Make sure the create new button is displaying for all entries
                Assert.IsTrue(driver.FindElement(By.ClassName("deleteButton")).Displayed);

                HttpClient client = new HttpClient();
                PageReferenceClean cleaner = new PageReferenceClean();
                //Make sure text is correct
                var data = await client.GetStringAsync("http://34.125.193.123/BigBrotherRedux/PageReference/ReadAll");
                data = cleaner.RemoveSquareBraces(data);
                List<string> pageInf = cleaner.PreppedData(cleaner.CleanAPIResponse(data));
                var model = cleaner.IndexPrepPageReference(pageInf);



                //Make sure each button is going to the correct entry
                ReadOnlyCollection<IWebElement> Entries = driver.FindElements(By.ClassName("deleteButton"));

                for (int i = 0; i < Entries.Count; i++)
                {
                    Assert.AreEqual(Entries[i].GetAttribute("href"), $"http://34.125.84.24/PageReferences/Delete/{model[i].PageId}");
                }
            }

            ).GetAwaiter().GetResult();
        }

        [Test]
        [TestCase(1)]
        [TestCase(2)]
        [TestCase(3)]

        public void PageRefDeleteEntry(int caseState)
        {
            Task.Run(async () =>
            {
                driver.Navigate().GoToUrl("http://34.125.84.24/PageReferences/");


                //Make sure the create new button is displaying for all entries
                Assert.IsTrue(driver.FindElement(By.ClassName("deleteButton")).Displayed);


                HttpClient client = new HttpClient();
                PageReferenceClean cleaner = new PageReferenceClean();
                //Make sure text is correct
                var data = await client.GetStringAsync("http://34.125.193.123/BigBrotherRedux/PageReference/ReadAll");
                data = cleaner.RemoveSquareBraces(data);
                List<string> pageInf = cleaner.PreppedData(cleaner.CleanAPIResponse(data));
                var model = cleaner.IndexPrepPageReference(pageInf);


                for (int i = 0; i < model.Count; i++)
                {
                    Console.WriteLine(model[i].PageId);
                }
                ReadOnlyCollection<IWebElement> Entries = driver.FindElements(By.ClassName("deleteButton"));

                Console.WriteLine(model[caseState - 1].PageId);
                Console.WriteLine(Entries[caseState - 1].GetAttribute("href"));
                Assert.AreEqual(Entries[caseState - 1].GetAttribute("href"), $"http://34.125.84.24/PageReferences/Delete/{model[caseState-1].PageId}"); 
                var element = driver.FindElements(By.ClassName("deleteButton"));
                element[caseState - 1].Click();
                element = driver.FindElements(By.CssSelector("input[class = 'btn btn-danger']"));
                element[0].Click();

            }

            ).GetAwaiter().GetResult();

        }


        /// <summary>
        /// Makes sure the back button on details works and nothing changes
        /// </summary>
        [Test]

        public void PageRefBackDetails()
        {
            Task.Run(async () =>
            {
                //navigate to the page reference page from the home page
                    driver.Navigate().GoToUrl("http://34.125.84.24/PageReferences/");
                //check entries on page


                HttpClient client = new HttpClient();
                PageReferenceClean cleaner = new PageReferenceClean();
                //Make sure text is correct
                var data = await client.GetStringAsync("http://34.125.193.123/BigBrotherRedux/PageReference/ReadAll");
                data = cleaner.RemoveSquareBraces(data);
                List<string> pageInf = cleaner.PreppedData(cleaner.CleanAPIResponse(data));
                var model = cleaner.IndexPrepPageReference(pageInf);
                List<int> PageIDs = new List<int>();
                for (int i = 0; i < model.Count; i++)
                {
                    Console.WriteLine(model[i].PageId);
                    PageIDs.Add(model[i].PageId);
                }
                //Init TR elements from table we found into list
                IList<IWebElement> trCollection = driver.FindElements(By.TagName("tr"));
                
                for (int i = 0; i < trCollection.Count; i++)
                {
                    Console.WriteLine(trCollection[i].Text);
                    
                }



                driver.Navigate().GoToUrl($"http://34.125.84.24/PageReferences/Details/{model[0].PageId}");

                //Select back
                var element = driver.FindElements(By.ClassName("backButton"));
                element[0].Click();
               


                //Make sure text is correct
                data = await client.GetStringAsync("http://34.125.193.123/BigBrotherRedux/PageReference/ReadAll");
                data = cleaner.RemoveSquareBraces(data);
                pageInf = cleaner.PreppedData(cleaner.CleanAPIResponse(data));
                model = cleaner.IndexPrepPageReference(pageInf);
                //ensure entries are the same
                for (int i = 0; i < PageIDs.Count; i++)
                {
                    Assert.AreEqual(PageIDs[i], model[i].PageId);
                }


            }

            ).GetAwaiter().GetResult();

        }

        /// <summary>
        /// Makes sure the back button on delete works and nothing changes
        /// </summary>
        [Test]

        public void PageRefBackDelete()
        {
            Task.Run(async () =>
            {
                //navigate to the page reference page from the home page
                driver.Navigate().GoToUrl("http://34.125.84.24/PageReferences/");
                //check entries on page


                HttpClient client = new HttpClient();
                PageReferenceClean cleaner = new PageReferenceClean();
                //Make sure text is correct
                var data = await client.GetStringAsync("http://34.125.193.123/BigBrotherRedux/PageReference/ReadAll");
                data = cleaner.RemoveSquareBraces(data);
                List<string> pageInf = cleaner.PreppedData(cleaner.CleanAPIResponse(data));
                var model = cleaner.IndexPrepPageReference(pageInf);
                List<int> PageIDs = new List<int>();
                for (int i = 0; i < model.Count; i++)
                {
                    Console.WriteLine(model[i].PageId);
                    PageIDs.Add(model[i].PageId);
                }
                //Init TR elements from table we found into list
                IList<IWebElement> trCollection = driver.FindElements(By.TagName("tr"));

                for (int i = 0; i < trCollection.Count; i++)
                {
                    Console.WriteLine(trCollection[i].Text);

                }



                driver.Navigate().GoToUrl($"http://34.125.84.24/PageReferences/Delete/{model[0].PageId}");

                //Select back
                var element = driver.FindElements(By.ClassName("backButton"));
                element[0].Click();



                //Make sure text is correct
                data = await client.GetStringAsync("http://34.125.193.123/BigBrotherRedux/PageReference/ReadAll");
                data = cleaner.RemoveSquareBraces(data);
                pageInf = cleaner.PreppedData(cleaner.CleanAPIResponse(data));
                model = cleaner.IndexPrepPageReference(pageInf);
                //ensure entries are the same
                for (int i = 0; i < PageIDs.Count; i++)
                {
                    Assert.AreEqual(PageIDs[i], model[i].PageId);
                }


            }

          ).GetAwaiter().GetResult();
        }

        [Test]

        public void PageRefBackCreate()
        {
            Task.Run(async () =>
            {
                //navigate to the page reference page from the home page
                driver.Navigate().GoToUrl("http://34.125.84.24/PageReferences/");
                //check entries on page


                HttpClient client = new HttpClient();
                PageReferenceClean cleaner = new PageReferenceClean();
                //Make sure text is correct
                var data = await client.GetStringAsync("http://34.125.193.123/BigBrotherRedux/PageReference/ReadAll");
                data = cleaner.RemoveSquareBraces(data);
                List<string> pageInf = cleaner.PreppedData(cleaner.CleanAPIResponse(data));
                var model = cleaner.IndexPrepPageReference(pageInf);
                List<int> PageIDs = new List<int>();
                for (int i = 0; i < model.Count; i++)
                {
                    Console.WriteLine(model[i].PageId);
                    PageIDs.Add(model[i].PageId);
                }
                //Init TR elements from table we found into list
                IList<IWebElement> trCollection = driver.FindElements(By.TagName("tr"));

                for (int i = 0; i < trCollection.Count; i++)
                {
                    Console.WriteLine(trCollection[i].Text);

                }



                driver.Navigate().GoToUrl($"http://34.125.84.24/PageReferences/Create/{model[0].PageId}");

                //Select back
                var element = driver.FindElements(By.ClassName("backButton"));
                element[0].Click();



                //Make sure text is correct
                data = await client.GetStringAsync("http://34.125.193.123/BigBrotherRedux/PageReference/ReadAll");
                data = cleaner.RemoveSquareBraces(data);
                pageInf = cleaner.PreppedData(cleaner.CleanAPIResponse(data));
                model = cleaner.IndexPrepPageReference(pageInf);
                //ensure entries are the same
                for (int i = 0; i < PageIDs.Count; i++)
                {
                    Assert.AreEqual(PageIDs[i], model[i].PageId);
                }


            }

         ).GetAwaiter().GetResult();
        }
        #endregion PageRefTests




        #region SessionsTests
        [Test]
        public void SessionsEntries()
        {
            Task.Run(async () =>
            {

                driver.Navigate().GoToUrl("http://34.125.84.24/Sessions");



                HttpClient client = new HttpClient();
                SessionsClean cleaner = new SessionsClean();
                //Make sure text is correct


                var data = await client.GetStringAsync("http://34.125.193.123/BigBrotherRedux/Session/ReadAll");
                data = cleaner.RemoveSquareBraces(data);
                List<string> pageInf = cleaner.PreppedData(cleaner.CleanAPIResponse(data));
                var model = cleaner.IndexPrepSessions(pageInf);

                List<IWebElement> lstTdElem = new List<IWebElement>(driver.FindElements(By.TagName("td")));
                if (lstTdElem.Count > 0)
                {
                    for (int i = 0; i < model.Count; i++)
                    {
                        Console.WriteLine(lstTdElem[0].Text);
                        Console.WriteLine(lstTdElem[1].Text);
                        Console.WriteLine(lstTdElem[2].Text);
                        Console.WriteLine(lstTdElem[3].Text);
                        Console.WriteLine(lstTdElem[4].Text);
                        Assert.AreEqual(lstTdElem[0 * (i + 3)].Text, model[i].UserIPAddress);

                        Assert.AreEqual(lstTdElem[1 * (i + 1)].Text, model[i].DateTime);
                        Assert.AreEqual(lstTdElem[2 * (i+1)].Text, model[i].LoggedIn);
                        Assert.AreEqual(lstTdElem[3 * (i + 1)].Text, model[i].PurchaseMade);

                    }
                }
            }

            ).GetAwaiter().GetResult();


        }

        [Test]
        public void SessionsDetailsButton()
        {

            Task.Run(async () =>
            {
                driver.Navigate().GoToUrl("http://34.125.84.24/Sessions");


                //Make sure the create new button is displaying for all entries
                Assert.IsTrue(driver.FindElement(By.ClassName("detailsButton")).Displayed);


                //Make sure each button is going to the correct entry
                ReadOnlyCollection<IWebElement> Entries = driver.FindElements(By.ClassName("detailsButton"));

                HttpClient client = new HttpClient();
                SessionsClean cleaner = new SessionsClean();
                //Make sure text is correct

                var data = await client.GetStringAsync("http://34.125.193.123/BigBrotherRedux/Session/ReadAll");
                data = cleaner.RemoveSquareBraces(data);
                List<string> pageInf = cleaner.PreppedData(cleaner.CleanAPIResponse(data));
                var model = cleaner.IndexPrepSessions(pageInf);
                foreach (var text in model)
                {
                    Console.WriteLine(text);
                }




                //Making sure all the entries in the database are valid urls
                for (int i = 1; i < Entries.Count + 1; i++)
                {
                    driver.Navigate().GoToUrl($"http://34.125.84.24/Sessions/Details/{i}");

                    List<IWebElement> lstElements = new List<IWebElement>(driver.FindElements(By.TagName("dd")));

                    var sessionData = await client.GetStringAsync($"http://34.125.193.123/BigBrotherRedux/Session/GetSession/{i}");
                    sessionData = cleaner.RemoveSquareBraces(sessionData);
                    List<string> pageInfSess = cleaner.PreppedData(cleaner.CleanAPIResponse(data));
                    var modelSess = cleaner.IndexPrepSessions(pageInfSess);

                    Assert.AreEqual(lstElements[0].Text, modelSess[0].UserIPAddress);
                    Assert.AreEqual(lstElements[1].Text, modelSess[0].DateTime);
                    Assert.AreEqual(lstElements[2].Text, modelSess[0].LoggedIn);
                    Assert.AreEqual(lstElements[3].Text, modelSess[0].PurchaseMade);

                }

            }

            ).GetAwaiter().GetResult();
        }



    
            #endregion SessionsTests

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