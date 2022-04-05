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
        // This will test if the big brother display button on the home page goes to the home page
        [Test]
        public void bigBrotherDisplayToHome()
        {
            driver.Navigate().GoToUrl("http://34.125.84.24/");
            driver.FindElement(By.LinkText("Big Brother Display")).Click();
            String actualUrl = "http://34.125.84.24/";
            String expectedUrl = driver.Url;
            Assert.AreEqual(expectedUrl, actualUrl);
        }

        // This will test if the home button on the home page goes to the home page
        [Test]
        public void homeToHome()
        {
            driver.Navigate().GoToUrl("http://34.125.84.24/");
            driver.FindElement(By.LinkText("Home")).Click();
            String actualUrl = "http://34.125.84.24/";
            String expectedUrl = driver.Url;
            Assert.AreEqual(expectedUrl, actualUrl);
        }

        // This will test if the login button on the home page goes to the login page
        [Test]
        public void toLogin()
        {
            driver.Navigate().GoToUrl("http://34.125.84.24/");
            driver.FindElement(By.LinkText("Login")).Click();
            String actualUrl = "http://34.125.84.24/Identity/Account/Login";
            String expectedUrl = driver.Url;
            Assert.AreEqual(expectedUrl, actualUrl);
        }

        // This will test if the user ip data button on the home page goes to the user ip data page while logged in
        [Test]
        public void toUserIPData()
        {
            driver.Navigate().GoToUrl("http://34.125.84.24/");
            driver.FindElement(By.LinkText("User IP Data")).Click();
            String actualUrl = "http://34.125.84.24/UserIpDatas";
            String expectedUrl = driver.Url;
            Assert.AreEqual(expectedUrl, actualUrl);
        }

        // This will test if the page reference data button on the home page goes to the page reference data page while logged in
        [Test]
        public void toPageReferenceData()
        {
            driver.Navigate().GoToUrl("http://34.125.84.24/");
            driver.FindElement(By.LinkText("Page Reference Data")).Click();
            String actualUrl = "http://34.125.84.24/PageReferences";
            String expectedUrl = driver.Url;
            Assert.AreEqual(expectedUrl, actualUrl);
        }

        // This will test if the user interactions data button on the home page goes to the user interactions data page while logged in
        [Test]
        public void toUserInteractionsData()
        {
            driver.Navigate().GoToUrl("http://34.125.84.24/");
            driver.FindElement(By.LinkText("User Interactions Data")).Click();
            String actualUrl = "http://34.125.84.24/UserInteractions";
            String expectedUrl = driver.Url;
            Assert.AreEqual(expectedUrl, actualUrl);
        }

        // This will test if the sessions button on the home page goes to the sessions page while logged in
        [Test]
        public void toSessions()
        {
            driver.Navigate().GoToUrl("http://34.125.84.24/");
            driver.FindElement(By.LinkText("Sessions")).Click();
            String actualUrl = "http://34.125.84.24/Sessions";
            String expectedUrl = driver.Url;
            Assert.AreEqual(expectedUrl, actualUrl);
        }

        // This will test if the register button on the home page goes to the registration page while logged in as an admin
        [Test]
        public void toRegister()
        {
            driver.Navigate().GoToUrl("http://34.125.84.24/");
            driver.FindElement(By.LinkText("Register")).Click();
            String actualUrl = "http://34.125.84.24/Identity/Account/Login?ReturnUrl=%2FIdentity%2FAccount%2FRegister";
            String expectedUrl = driver.Url;
            Assert.AreEqual(expectedUrl, actualUrl);
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