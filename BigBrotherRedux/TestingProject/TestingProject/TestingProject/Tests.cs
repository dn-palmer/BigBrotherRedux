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
        // This will test if the big brother display button on the home page goes to the home page
        [Test]
        public void bigBrotherDisplayToHome()
        {
            driver.Navigate().GoToUrl("http://34.125.84.24/");
            driver.FindElement(By.LinkText("BB-Monitoring")).Click();
            String actualUrl = "http://34.125.84.24/";
            String expectedUrl = driver.Url;
            Assert.AreEqual(expectedUrl, actualUrl);
        }

        // This will test if the home button on the home page goes to the home page
        [Test]
        public void homeToHome()
        {
            driver.Navigate().GoToUrl("http://34.125.84.24/");
            driver.FindElement(By.LinkText("BB-Monitoring")).Click();
            String actualUrl = "http://34.125.84.24/";
            String expectedUrl = driver.Url;
            Assert.AreEqual(expectedUrl, actualUrl);
        }

        // This will test if the login button on the home page goes to the login page
        [Test]
        public void toLogin()
        {
            driver.Navigate().GoToUrl("http://34.125.84.24/");
            driver.FindElement(By.Id("logout")).Click();

            driver.FindElement(By.Id("login")).Click();
            String actualUrl = "http://34.125.84.24/Identity/Account/Login";
            String expectedUrl = driver.Url;
            Assert.AreEqual(expectedUrl, actualUrl);
        }

        // This will test if the user ip data button on the home page goes to the user ip data page while logged in
        [Test]
        public void toUserIPData()
        {
            driver.Navigate().GoToUrl("http://34.125.84.24/");
            driver.FindElement(By.LinkText("Visitor Data")).Click();
            String actualUrl = "http://34.125.84.24/UserIpDatas";
            String expectedUrl = driver.Url;
            Assert.AreEqual(expectedUrl, actualUrl);
        }

        // This will test if the page reference data button on the home page goes to the page reference data page while logged in
        [Test]
        public void toPageReferenceData()
        {
            driver.Navigate().GoToUrl("http://34.125.84.24/");
            driver.FindElement(By.LinkText("Site Data")).Click();
            String actualUrl = "http://34.125.84.24/PageReferences";
            String expectedUrl = driver.Url;
            Assert.AreEqual(expectedUrl, actualUrl);
        }

        // This will test if the user interactions data button on the home page goes to the user interactions data page while logged in
        [Test]
        public void toUserInteractionsData()
        {
            driver.Navigate().GoToUrl("http://34.125.84.24/");
            driver.FindElement(By.LinkText("Interaction Data")).Click();
            String actualUrl = "http://34.125.84.24/UserInteractions";
            String expectedUrl = driver.Url;
            Assert.AreEqual(expectedUrl, actualUrl);
        }

        // This will test if the sessions button on the home page goes to the sessions page while logged in
        [Test]
        public void toSessions()
        {
            driver.Navigate().GoToUrl("http://34.125.84.24/");
            driver.FindElement(By.LinkText("Session Data")).Click();
            String actualUrl = "http://34.125.84.24/Sessions";
            String expectedUrl = driver.Url;
            Assert.AreEqual(expectedUrl, actualUrl);
        }

        // This will test if the register button on the home page goes to the registration page while logged in as an admin
        [Test]
        public void toRegister()
        {
            driver.Navigate().GoToUrl("http://34.125.84.24/");
            
            driver.FindElement(By.Id("logout")).Click();

            driver.FindElement(By.Id("register")).Click();
            String actualUrl = "http://34.125.84.24/Identity/Account/Login?ReturnUrl=%2FIdentity%2FAccount%2FRegister";
            String expectedUrl = driver.Url;
            Assert.AreEqual(expectedUrl, actualUrl);
        }
        #endregion

        #region UserIP
        /// <summary>
        /// Checks the format for all entries on the User IP page.
        /// </summary>
        [Test]
        public void UserIPEntries()
        {
            Task.Run(async () =>
            {
                driver.Navigate().GoToUrl("http://34.125.84.24/UserIpDatas");

                HttpClient client = new HttpClient();
                IPDataClean cleaner = new IPDataClean();

                var data = await client.GetStringAsync("http://34.125.193.123/BigBrotherRedux/UserIPData/ReadAll");
                data = cleaner.RemoveSquareBraces(data);
                List<string> userIPInf = cleaner.PreppedData(cleaner.CleanAPIResponse(data));
                var model = cleaner.IndexPrepIPData(userIPInf);

                List<IWebElement> lstTdElem = new List<IWebElement>(driver.FindElements(By.TagName("td")));

                for (int i = 0; i < lstTdElem.Count; i++)
                {
                    Console.WriteLine("element: " + lstTdElem[i].Text);
                }

                if (lstTdElem.Count > 0)
                {
                    for (int i = 0; i < model.Count; i++)
                    {
                        Assert.AreEqual(lstTdElem[(9 * i)].Text, model[i].UserIP);
                        Assert.AreEqual(lstTdElem[1 + (9 * i)].Text, model[i].CountryCode);
                        Assert.AreEqual(lstTdElem[2 + (9 * i)].Text, model[i].CountryName);
                        Assert.AreEqual(lstTdElem[3 + (9 * i)].Text, model[i].StateOrRegion);
                        Assert.AreEqual(lstTdElem[4 + (9 * i)].Text, model[i].City);
                        Assert.AreEqual(lstTdElem[5 + (9 * i)].Text, model[i].ZipCode);
                        Assert.AreEqual(Convert.ToInt32(lstTdElem[6 + (9 * i)].Text), model[i].VisitCount);
                        Assert.AreEqual(lstTdElem[7 + (9 * i)].Text, model[i].DeviceType);
                    }
                }
            }).GetAwaiter().GetResult();
        }

        /// <summary>
        /// Checks the format for all entries on the User IP page.
        /// </summary>
        [Test]
        [TestCase("169.46.237.81")]
        [TestCase("83.225.100.223")]
        [TestCase("218.182.163.213")]
        [TestCase("164.255.62.67")]
        [TestCase("168.164.53.89")]
        [TestCase("51.69.123.101")]
        public void UserIPCreate(string ip)
        {
                driver.Navigate().GoToUrl("http://34.125.84.24/UserIpDatas");


                //Make sure the create new button is displaying for all entries
                Assert.IsTrue(driver.FindElement(By.ClassName("newEntry")).Displayed);

                //Make sure each button is going to the correct entry
                ReadOnlyCollection<IWebElement> Entries = driver.FindElements(By.ClassName("newEntry"));

                for (int i = 0; i<Entries.Count; i++)
                {
                    Console.WriteLine(Entries[i].GetAttribute("href"));
                    Assert.AreEqual(Entries[i].GetAttribute("href"), $"http://34.125.84.24/UserIPDatas/Create");
                }

                driver.Navigate().GoToUrl("http://34.125.84.24/UserIPDatas/Create");

                IWebElement UserIPWeb = driver.FindElement(By.Name("UserIP"));
                UserIPWeb.SendKeys(ip);

                var element = driver.FindElements(By.CssSelector("input[class = 'btn btn-danger']"));
                element[0].Click();
        }

        /// <summary>
        /// Checks to ensure that the Details button works properly.
        /// </summary>
        /// <param name="index">Index of the User IP to check.</param>
        [Test]
        [TestCase(0)]
        public void UserIPDetailsButton(int index)
        {
            Task.Run(async () =>
            {
                driver.Navigate().GoToUrl("http://34.125.84.24/UserIpDatas");

                //Make sure the create new button is displaying for all entries
                Assert.IsTrue(driver.FindElement(By.LinkText("Details")).Displayed);

                //Make sure each button is going to the correct entry
                ReadOnlyCollection<IWebElement> Entries = driver.FindElements(By.LinkText("Details"));

                HttpClient client = new HttpClient();
                IPDataClean cleaner = new IPDataClean();

                var data = await client.GetStringAsync("http://34.125.193.123/BigBrotherRedux/UserIPData/ReadAll");
                data = cleaner.RemoveSquareBraces(data);
                List<string> userIPInf = cleaner.PreppedData(cleaner.CleanAPIResponse(data));
                var model = cleaner.IndexPrepIPData(userIPInf);

                driver.Navigate().GoToUrl($"http://34.125.84.24/UserIpDatas/Details/{model[index].UserIP}");

            }).GetAwaiter().GetResult();
        }

        /// <summary>
        /// Checks to ensure that the Delete button is displaying for all entries.
        /// </summary>
        [Test]
        public void UserIPDeleteButton()
        {
            Task.Run(async () =>
            {
                driver.Navigate().GoToUrl("http://34.125.84.24/UserIpDatas");

                //Make sure the create new button is displaying for all entries
                Assert.IsTrue(driver.FindElement(By.LinkText("Delete")).Displayed);

                HttpClient client = new HttpClient();
                IPDataClean cleaner = new IPDataClean();

                var data = await client.GetStringAsync("http://34.125.193.123/BigBrotherRedux/UserIPData/ReadAll");
                data = cleaner.RemoveSquareBraces(data);
                List<string> userIPInf = cleaner.PreppedData(cleaner.CleanAPIResponse(data));
                var model = cleaner.IndexPrepIPData(userIPInf);

                //Make sure each button is going to the correct entry
                ReadOnlyCollection<IWebElement> Entries = driver.FindElements(By.LinkText("Delete"));

                for (int i = 0; i < Entries.Count; i++)
                {
                    Assert.AreEqual(Entries[i].GetAttribute("href"), $"http://34.125.84.24/UserIPDatas/Delete/{model[i].UserIP}");
                }
            }).GetAwaiter().GetResult();
        }

        [Test]
        [TestCase(1)]
        [TestCase(2)]
        [TestCase(3)]

        public void UserIPDeleteEntry(int caseState)
        {
            Task.Run(async () =>
            {
                driver.Navigate().GoToUrl("http://34.125.84.24/UserIpDatas/");


                //Make sure the create new button is displaying for all entries
                Assert.IsTrue(driver.FindElement(By.ClassName("deleteButton")).Displayed);


                HttpClient client = new HttpClient();
                IPDataClean cleaner = new IPDataClean();
                //Make sure text is correct
                var data = await client.GetStringAsync("http://34.125.193.123/BigBrotherRedux/UserIpData/ReadAll");
                data = cleaner.RemoveSquareBraces(data);
                List<string> pageInf = cleaner.PreppedData(cleaner.CleanAPIResponse(data));
                var model = cleaner.IndexPrepIPData(pageInf);


                for (int i = 0; i < model.Count; i++)
                {
                    Console.WriteLine(model[i].UserIP);
                }
                ReadOnlyCollection<IWebElement> Entries = driver.FindElements(By.ClassName("deleteButton"));

                Console.WriteLine(model[caseState - 1].UserIP);
                Console.WriteLine(Entries[caseState - 1].GetAttribute("href"));
                Assert.AreEqual(Entries[caseState - 1].GetAttribute("href"), $"http://34.125.84.24/UserIPDatas/Delete/{model[caseState - 1].UserIP}");
                var element = driver.FindElements(By.ClassName("deleteButton"));
                element[caseState - 1].Click();
                element = driver.FindElements(By.CssSelector("input[class = 'btn btn-danger']"));
                element[0].Click();

            }

            ).GetAwaiter().GetResult();

        }

        /// <summary>
        /// Makes sure the Back button on details works and nothing changes.
        /// </summary>
        [Test]
        public void UserIPBackDetails()
        {
            Task.Run(async () =>
            {
                //navigate to the page reference page from the home page
                driver.Navigate().GoToUrl("http://34.125.84.24/UserIpDatas");

                HttpClient client = new HttpClient();
                IPDataClean cleaner = new IPDataClean();

                var data = await client.GetStringAsync("http://34.125.193.123/BigBrotherRedux/UserIPData/ReadAll");
                data = cleaner.RemoveSquareBraces(data);
                List<string> userIPInf = cleaner.PreppedData(cleaner.CleanAPIResponse(data));
                var model = cleaner.IndexPrepIPData(userIPInf);

                List<string> UserIPs = new List<string>();

                for (int i = 0; i < model.Count; i++)
                {
                    Console.WriteLine(model[i].UserIP);
                    UserIPs.Add(model[i].UserIP);
                }

                //Init TR elements from table we found into list
                IList<IWebElement> trCollection = driver.FindElements(By.ClassName("card-body"));

                for (int i = 0; i < trCollection.Count; i++)
                {
                    Console.WriteLine(trCollection[i].Text);
                }

                driver.Navigate().GoToUrl($"http://34.125.84.24/UserIPDatas/Details/{model[0].UserIP}");

                //Select back
                var element = driver.FindElements(By.LinkText("Back to List"));
                element[0].Click();

                data = await client.GetStringAsync("http://34.125.193.123/BigBrotherRedux/UserIPData/ReadAll");
                data = cleaner.RemoveSquareBraces(data);
                userIPInf = cleaner.PreppedData(cleaner.CleanAPIResponse(data));
                model = cleaner.IndexPrepIPData(userIPInf);

                //ensure entries are the same
                for (int i = 0; i < UserIPs.Count; i++)
                {
                    Assert.AreEqual(UserIPs[i], model[i].UserIP);
                }
            }).GetAwaiter().GetResult();
        }

        /// <summary>
        /// Makes sure the Back button on Delete works and nothing changes
        /// </summary>
        [Test]
        public void UserIPsBackDelete()
        {
            Task.Run(async () =>
            {
                driver.Navigate().GoToUrl("http://34.125.84.24/UserIpDatas");

                HttpClient client = new HttpClient();
                IPDataClean cleaner = new IPDataClean();

                var data = await client.GetStringAsync("http://34.125.193.123/BigBrotherRedux/UserIPData/ReadAll");
                data = cleaner.RemoveSquareBraces(data);
                List<string> userInteractionInf = cleaner.PreppedData(cleaner.CleanAPIResponse(data));
                var model = cleaner.IndexPrepIPData(userInteractionInf);

                List<string> IPIDs = new List<string>();

                for (int i = 0; i < model.Count; i++)
                {
                    Console.WriteLine(model[i].UserIP);
                    IPIDs.Add(model[i].UserIP);
                }

                //Init TR elements from table we found into list
                IList<IWebElement> trCollection = driver.FindElements(By.TagName("tr"));

                for (int i = 0; i < trCollection.Count; i++)
                {
                    Console.WriteLine(trCollection[i].Text);
                }

                driver.Navigate().GoToUrl($"http://34.125.84.24/UserIpDatas/Delete/{model[0].UserIP}");

                var element = driver.FindElements(By.LinkText("Back to List"));
                element[0].Click();

                data = await client.GetStringAsync("http://34.125.193.123/BigBrotherRedux/UserIPData/ReadAll");
                data = cleaner.RemoveSquareBraces(data);
                userInteractionInf = cleaner.PreppedData(cleaner.CleanAPIResponse(data));
                model = cleaner.IndexPrepIPData(userInteractionInf);

                for (int i = 0; i < IPIDs.Count; i++)
                {
                    Assert.AreEqual(IPIDs[i], model[i].UserIP);
                }
            }).GetAwaiter().GetResult();
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
            Assert.IsTrue(driver.FindElement(By.LinkText("Add Page")).Displayed);

            //Make sure each button is going to the correct entry
            ReadOnlyCollection<IWebElement> Entries = driver.FindElements(By.LinkText("Add Page"));

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
                Assert.IsTrue(driver.FindElement(By.LinkText("Delete")).Displayed);

                HttpClient client = new HttpClient();
                PageReferenceClean cleaner = new PageReferenceClean();
                //Make sure text is correct
                var data = await client.GetStringAsync("http://34.125.193.123/BigBrotherRedux/PageReference/ReadAll");
                data = cleaner.RemoveSquareBraces(data);
                List<string> pageInf = cleaner.PreppedData(cleaner.CleanAPIResponse(data));
                var model = cleaner.IndexPrepPageReference(pageInf);



                //Make sure each button is going to the correct entry
                ReadOnlyCollection<IWebElement> Entries = driver.FindElements(By.LinkText("Delete"));

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
                Assert.IsTrue(driver.FindElement(By.LinkText("Delete")).Displayed);


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
                ReadOnlyCollection<IWebElement> Entries = driver.FindElements(By.LinkText("Delete"));

                Console.WriteLine(model[caseState - 1].PageId);
                Console.WriteLine(Entries[caseState - 1].GetAttribute("href"));
                Assert.AreEqual(Entries[caseState - 1].GetAttribute("href"), $"http://34.125.84.24/PageReferences/Delete/{model[caseState-1].PageId}"); 
                var element = driver.FindElements(By.LinkText("Delete"));
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
                IList<IWebElement> trCollection = driver.FindElements(By.ClassName("card"));
                
                for (int i = 0; i < trCollection.Count; i++)
                {
                    Console.WriteLine(trCollection[i].Text);
                    
                }



                driver.Navigate().GoToUrl($"http://34.125.84.24/PageReferences/Details/{model[0].PageId}");

                //Select back
                var element = driver.FindElements(By.LinkText("Back to List"));
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
                IList<IWebElement> trCollection = driver.FindElements(By.ClassName("card"));

                for (int i = 0; i < trCollection.Count; i++)
                {
                    Console.WriteLine(trCollection[i].Text);

                }



                driver.Navigate().GoToUrl($"http://34.125.84.24/PageReferences/Delete/{model[0].PageId}");

                //Select back
                var element = driver.FindElements(By.LinkText("Back to List"));
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
        #endregion

        #region Login
        // This will test if someone can login with proper credentials
        [Test]
        public void logInPass()
        {
            driver.Navigate().GoToUrl("http://34.125.84.24/Identity/Account/Login");
            IWebElement username = driver.FindElement(By.Name("Input.Email"));
            username.SendKeys("LoneWolf@email.com");
            IWebElement password = driver.FindElement(By.Name("Input.Password"));
            password.SendKeys("Passw0rd!");
            var element = driver.FindElements(By.Id("login-submit"));
            element[0].Click();
            String actualUrl = "http://34.125.84.24/";
            String expectedUrl = driver.Url;
            Assert.AreEqual(expectedUrl, actualUrl);

        }

        // This will test if someone can login with phony credentials
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

        public void logInFail()
        {
            driver.Navigate().GoToUrl("http://34.125.84.24/Identity/Account/Login");
            IWebElement username = driver.FindElement(By.Name("Input.Email"));
            username.SendKeys("Fake@email.com");
            IWebElement password = driver.FindElement(By.Name("Input.Password"));
            password.SendKeys("Password!");
            var element = driver.FindElements(By.Id("login-submit"));
            element[0].Click();
            String actualUrl = "http://34.125.84.24/Identity/Account/Login";
            String expectedUrl = driver.Url;
            Assert.AreEqual(expectedUrl, actualUrl);
        }
        #endregion

        #region UserInteractionsTests        
        /// <summary>
        /// Checks the format for all entries on the User Interaction page
        /// </summary>
        [Test]
        public void UserInteractionsEntries()
        {
            Task.Run(async () =>
            {
                driver.Navigate().GoToUrl("http://34.125.84.24/UserInteractions");

                HttpClient client = new HttpClient();
                UserInteractionsClean cleaner = new UserInteractionsClean();

                var data = await client.GetStringAsync("http://34.125.193.123/BigBrotherRedux/UserInteraction/ReadAll");
                data = cleaner.RemoveSquareBraces(data);
                List<string> userInteractionInf = cleaner.PreppedData(cleaner.CleanAPIResponse(data));
                var model = cleaner.IndexPrepUserInteractionsData(userInteractionInf);

                List<IWebElement> lstTdElem = new List<IWebElement>(driver.FindElements(By.TagName("td")));

                for (int i = 0; i < lstTdElem.Count; i++)
                {
                    Console.WriteLine("element: " + lstTdElem[i].Text);
                }

                if (lstTdElem.Count > 0)
                {
                    for (int i = 0; i < model.Count; i++)
                    {
                        Assert.AreEqual(Convert.ToInt32(lstTdElem[(5 * i)].Text), model[i].UserSessionID);
                        Assert.AreEqual(lstTdElem[1 + (5 * i)].Text, model[i].DateTime);
                        Assert.AreEqual(Convert.ToInt32(lstTdElem[2 + (5 * i)].Text), model[i].CurrentPageID);
                        Assert.AreEqual(lstTdElem[3 + (5 * i)].Text, model[i].InteractionLength);
                    }
                }
            }).GetAwaiter().GetResult();
        }

        /// <summary>
        /// Checks to ensure that the Details button works properly.
        /// </summary>
        /// <param name="index">Index of the User Interactio to check.</param>
        [Test]
        [TestCase(0)]
        public void UserInteractionsDetailsButton(int index)
        {
            Task.Run(async () =>
            {
                driver.Navigate().GoToUrl("http://34.125.84.24/UserInteractions");

                //Make sure the create new button is displaying for all entries
                Assert.IsTrue(driver.FindElement(By.ClassName("detailsButton")).Displayed);

                //Make sure each button is going to the correct entry
                ReadOnlyCollection<IWebElement> Entries = driver.FindElements(By.ClassName("detailsButton"));

                HttpClient client = new HttpClient();
                UserInteractionsClean cleaner = new UserInteractionsClean();

                var data = await client.GetStringAsync("http://34.125.193.123/BigBrotherRedux/UserInteraction/ReadAll");
                data = cleaner.RemoveSquareBraces(data);
                List<string> userInteractionInf = cleaner.PreppedData(cleaner.CleanAPIResponse(data));
                var model = cleaner.IndexPrepUserInteractionsData(userInteractionInf);

                driver.Navigate().GoToUrl($"http://34.125.84.24/UserInteractions/Details/{model[index].UserInteractionID}");

            }).GetAwaiter().GetResult();
        }

        /// <summary>
        /// Checks to ensure that the Create button works properly.
        /// </summary>
        /// <param name="date">Date of the interaction.</param>
        /// <param name="lengthOfInteraction">Ending date of the interaction.</param>
        /// <param name="sessionID">Session ID of the interaction.</param>
        /// <param name="pageID">Page ID of the interaction.</param>
        [Test]
        [TestCase("1-1-2022", "1-1-2022", "5", "6")]
        [TestCase("2-3-2022", "3-2-2022", "4", "5")]
        public void UserInteractionsCreateButton(string date, string lengthOfInteraction, string sessionID, string pageID)
        {
            driver.Navigate().GoToUrl("http://34.125.84.24/UserInteractions");

            //Make sure the create new button is displaying for all entries
            Assert.IsTrue(driver.FindElement(By.ClassName("newEntry")).Displayed);

            //Make sure each button is going to the correct entry
            ReadOnlyCollection<IWebElement> Entries = driver.FindElements(By.ClassName("newEntry"));

            for (int i = 0; i < Entries.Count; i++)
            {
                Console.WriteLine(Entries[i].GetAttribute("href"));
                Assert.AreEqual(Entries[i].GetAttribute("href"), $"http://34.125.84.24/UserInteractions/Create");
            }

            driver.Navigate().GoToUrl("http://34.125.84.24/UserInteractions/Create");

            IWebElement sessionIDWeb = driver.FindElement(By.Name("UserSessionID"));
            sessionIDWeb.SendKeys(sessionID);

            IWebElement dateWeb = driver.FindElement(By.Name("DateTime"));
            dateWeb.SendKeys(date);

            IWebElement pageIDWeb = driver.FindElement(By.Name("CurrentPageID"));
            pageIDWeb.SendKeys(pageID);

            IWebElement lengthOfIntWeb = driver.FindElement(By.Name("InteractionLength"));
            lengthOfIntWeb.SendKeys(lengthOfInteraction);

            var element = driver.FindElements(By.CssSelector("input[class = 'btn btn-primary']"));
            element[0].Click();
        }

        /// <summary>
        /// Checks to ensure that the Delete button is displaying for all entries.
        /// </summary>
        [Test]
        public void UserInteractionsDeleteButton()
        {
            Task.Run(async () =>
            {
                driver.Navigate().GoToUrl("http://34.125.84.24/UserInteractions");

                //Make sure the create new button is displaying for all entries
                Assert.IsTrue(driver.FindElement(By.ClassName("deleteButton")).Displayed);

                HttpClient client = new HttpClient();
                UserInteractionsClean cleaner = new UserInteractionsClean();

                var data = await client.GetStringAsync("http://34.125.193.123/BigBrotherRedux/UserInteraction/ReadAll");
                data = cleaner.RemoveSquareBraces(data);
                List<string> userInteractionInf = cleaner.PreppedData(cleaner.CleanAPIResponse(data));
                var model = cleaner.IndexPrepUserInteractionsData(userInteractionInf);

                //Make sure each button is going to the correct entry
                ReadOnlyCollection<IWebElement> Entries = driver.FindElements(By.ClassName("deleteButton"));

                for (int i = 0; i < Entries.Count; i++)
                {
                    Assert.AreEqual(Entries[i].GetAttribute("href"), $"http://34.125.84.24/UserInteractions/Delete/{model[i].UserInteractionID}");
                }
            }).GetAwaiter().GetResult();
        }

        /// <summary>
        /// Checks to ensure that the Delete button works properly.
        /// </summary>
        /// <param name="caseState">Entry to delete.</param>
        [Test]
        [TestCase(1)]
        [TestCase(2)]
        public void UserInteractionsDeleteEntry(int caseState)
        {
            Task.Run(async () =>
            {
                driver.Navigate().GoToUrl("http://34.125.84.24/UserInteractions");

                //Make sure the create new button is displaying for all entries
                Assert.IsTrue(driver.FindElement(By.ClassName("deleteButton")).Displayed);

                HttpClient client = new HttpClient();
                UserInteractionsClean cleaner = new UserInteractionsClean();

                var data = await client.GetStringAsync("http://34.125.193.123/BigBrotherRedux/UserInteraction/ReadAll");
                data = cleaner.RemoveSquareBraces(data);
                List<string> userInteractionInf = cleaner.PreppedData(cleaner.CleanAPIResponse(data));
                var model = cleaner.IndexPrepUserInteractionsData(userInteractionInf);

                for (int i = 0; i < model.Count; i++)
                {
                    Console.WriteLine(model[i].UserInteractionID);
                }

                ReadOnlyCollection<IWebElement> Entries = driver.FindElements(By.ClassName("deleteButton"));

                Console.WriteLine(model[caseState - 1].UserInteractionID);
                Console.WriteLine(Entries[caseState - 1].GetAttribute("href"));

                Assert.AreEqual(Entries[caseState - 1].GetAttribute("href"), $"http://34.125.84.24/UserInteractions/Delete/{model[caseState - 1].UserInteractionID}");

                var element = driver.FindElements(By.ClassName("deleteButton"));
                element[caseState - 1].Click();
                element = driver.FindElements(By.CssSelector("input[class = 'btn btn-danger']"));
                element[0].Click();
            }).GetAwaiter().GetResult();
        }

        /// <summary>
        /// Makes sure the Back button on details works and nothing changes.
        /// </summary>
        [Test]
        public void UserInteractionsBackDetails()
        {
            Task.Run(async () =>
            {
                //navigate to the page reference page from the home page
                driver.Navigate().GoToUrl("http://34.125.84.24/UserInteractions");

                HttpClient client = new HttpClient();
                UserInteractionsClean cleaner = new UserInteractionsClean();

                var data = await client.GetStringAsync("http://34.125.193.123/BigBrotherRedux/UserInteraction/ReadAll");
                data = cleaner.RemoveSquareBraces(data);
                List<string> userInteractionInf = cleaner.PreppedData(cleaner.CleanAPIResponse(data));
                var model = cleaner.IndexPrepUserInteractionsData(userInteractionInf);

                List<int> UserInteractionIDs = new List<int>();

                for (int i = 0; i < model.Count; i++)
                {
                    Console.WriteLine(model[i].UserInteractionID);
                    UserInteractionIDs.Add(model[i].UserInteractionID);
                }

                //Init TR elements from table we found into list
                IList<IWebElement> trCollection = driver.FindElements(By.ClassName("card"));

                for (int i = 0; i < trCollection.Count; i++)
                {
                    Console.WriteLine(trCollection[i].Text);
                }

                driver.Navigate().GoToUrl($"http://34.125.84.24/UserInteractions/Details/{model[0].UserInteractionID}");

                //Select back
                var element = driver.FindElements(By.LinkText("Back to List"));
                element[0].Click();

                data = await client.GetStringAsync("http://34.125.193.123/BigBrotherRedux/UserInteraction/ReadAll");
                data = cleaner.RemoveSquareBraces(data);
                userInteractionInf = cleaner.PreppedData(cleaner.CleanAPIResponse(data));
                model = cleaner.IndexPrepUserInteractionsData(userInteractionInf);

                //ensure entries are the same
                for (int i = 0; i < UserInteractionIDs.Count; i++)
                {
                    Assert.AreEqual(UserInteractionIDs[i], model[i].UserInteractionID);
                }
            }).GetAwaiter().GetResult();
        }

        /// <summary>
        /// Makes sure the Back button on Delete works and nothing changes
        /// </summary>
        [Test]
        public void UserInteractionsBackDelete()
        {
            Task.Run(async () =>
            {
                driver.Navigate().GoToUrl("http://34.125.84.24/UserInteractions");

                HttpClient client = new HttpClient();
                UserInteractionsClean cleaner = new UserInteractionsClean();

                var data = await client.GetStringAsync("http://34.125.193.123/BigBrotherRedux/UserInteraction/ReadAll");
                data = cleaner.RemoveSquareBraces(data);
                List<string> userInteractionInf = cleaner.PreppedData(cleaner.CleanAPIResponse(data));
                var model = cleaner.IndexPrepUserInteractionsData(userInteractionInf);

                List<int> UserInteractionIDs = new List<int>();

                for (int i = 0; i < model.Count; i++)
                {
                    Console.WriteLine(model[i].UserInteractionID);
                    UserInteractionIDs.Add(model[i].UserInteractionID);
                }

                //Init TR elements from table we found into list
                IList<IWebElement> trCollection = driver.FindElements(By.TagName("tr"));

                for (int i = 0; i < trCollection.Count; i++)
                {
                    Console.WriteLine(trCollection[i].Text);
                }

                driver.Navigate().GoToUrl($"http://34.125.84.24/UserInteractions/Delete/{model[0].UserInteractionID}");

                var element = driver.FindElements(By.ClassName("backButton"));
                element[0].Click();

                data = await client.GetStringAsync("http://34.125.193.123/BigBrotherRedux/UserInteraction/ReadAll");
                data = cleaner.RemoveSquareBraces(data);
                userInteractionInf = cleaner.PreppedData(cleaner.CleanAPIResponse(data));
                model = cleaner.IndexPrepUserInteractionsData(userInteractionInf);

                for (int i = 0; i < UserInteractionIDs.Count; i++)
                {
                    Assert.AreEqual(UserInteractionIDs[i], model[i].UserInteractionID);
                }
            }).GetAwaiter().GetResult();
        }
        #endregion

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
                //Assert.IsTrue(driver.FindElement(By.LinkText("Details")).Displayed);


                //Make sure each button is going to the correct entry
                ReadOnlyCollection<IWebElement> Entries = driver.FindElements(By.LinkText("Details"));

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

                    //Assert.AreEqual(lstElements[0].Text, modelSess[0].UserIPAddress);
                    //Assert.AreEqual(lstElements[1].Text, modelSess[0].DateTime);
                    //Assert.AreEqual(lstElements[2].Text, modelSess[0].LoggedIn);
                    //Assert.AreEqual(lstElements[3].Text, modelSess[0].PurchaseMade);

                }

            }

            ).GetAwaiter().GetResult();
        }



    
            #endregion SessionsTests

        #region Register
        // This will test if the registration page works for an admin once logged in
        [Test]
        public void registerPass()
        {
            driver.Navigate().GoToUrl("http://34.125.84.24/Identity/Account/Login?ReturnUrl=%2FIdentity%2FAccount%2FRegister");
            IWebElement username = driver.FindElement(By.Name("Input.Email"));
            username.SendKeys("Lonewolf@email.com");
            IWebElement password = driver.FindElement(By.Name("Input.Password"));
            password.SendKeys("Passw0rd!");
            var element = driver.FindElements(By.Id("login-submit"));
            element[0].Click();
            String actualUrl = "http://34.125.84.24/Identity/Account/Register";
            String expectedUrl = driver.Url;
            Assert.AreEqual(expectedUrl, actualUrl);
        }
        #endregion

        [OneTimeTearDown]
        public void TearDown()
        {
            driver.Quit();
        }
    }
}