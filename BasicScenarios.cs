//******************************************************************************
//
/*
Copyright (c) 2016 Appium Committers. All rights reserved.

Licensed to you under the Apache License, Version 2.0 (the
"License"); you may not use this file except in compliance
with the License.  You may obtain a copy of the License at
http://www.apache.org/licenses/LICENSE-2.0 

Unless required by applicable law or agreed to in writing,
software distributed under the License is distributed on an
"AS IS" BASIS, WITHOUT WARRANTIES OR CONDITIONS OF ANY
KIND, either express or implied.  See the License for the
specific language governing permissions and limitations
under the License.
*/
//
//******************************************************************************

using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using OpenQA.Selenium.Appium;
using OpenQA.Selenium.Appium.Windows;
using OpenQA.Selenium.Remote;

namespace CalculatorTest
{
    [TestClass]
    public class BasicScenarios
    {
        protected const string WindowsApplicationDriverUrl = "http://127.0.0.1:4723/wd/hub";
        protected static WindowsDriver<WindowsElement> CalculatorSession;
        protected static WindowsElement CalculatorResult;
        protected static string OriginalCalculatorMode;

        [ClassInitialize]
        public static void Setup(TestContext context)
        {
            // Launch the calculator app
            AppiumOptions appCapabilities = new AppiumOptions();
            appCapabilities.AddAdditionalCapability("app", "Microsoft.WindowsCalculator_8wekyb3d8bbwe!App");
            appCapabilities.AddAdditionalCapability("platformName", "Windows");
            appCapabilities.AddAdditionalCapability("deviceName", "WindowsPC");
            CalculatorSession = new WindowsDriver<WindowsElement>(new Uri(WindowsApplicationDriverUrl), appCapabilities);
            Assert.IsNotNull(CalculatorSession);
            CalculatorSession.Manage().Timeouts().ImplicitWait = TimeSpan.FromSeconds(2);

            // Make sure we're in standard mode
            CalculatorSession.FindElementByAccessibilityId("TogglePaneButton").Click();
            OriginalCalculatorMode = CalculatorSession.FindElementByAccessibilityId("NavButton").Text;
            CalculatorSession.FindElementByAccessibilityId("Standard").Click();


            CalculatorSession.FindElementByAccessibilityId("ClearEntry").Click();
            CalculatorSession.FindElementByAccessibilityId("num7Button").Click();
            CalculatorResult = CalculatorSession.FindElementByAccessibilityId("CalculatorResults");
            Assert.IsNotNull(CalculatorResult);
        }

        [ClassCleanup]
        public static void TearDown()
        {
            // Restore original mode before closing down
            CalculatorSession.FindElementByAccessibilityId("TogglePaneButton").Click();
            CalculatorSession.FindElementByAccessibilityId("NavButton").Click();

            CalculatorResult = null;
            CalculatorSession.Dispose();
            CalculatorSession = null;
        }

        [TestInitialize]
        public void Clear()
        {
            CalculatorSession.FindElementByAccessibilityId("ClearEntry").Click();
            Assert.AreEqual("Display is  0 ", CalculatorResult.Text);
        }

        [TestMethod]
        public void Addition()
        {
            CalculatorSession.FindElementByAccessibilityId("num1Button").Click();
            CalculatorSession.FindElementByAccessibilityId("plusButton").Click();
            CalculatorSession.FindElementByAccessibilityId("num7Button").Click();
            CalculatorSession.FindElementByAccessibilityId("equalButton").Click();
            Assert.AreEqual("Display is  8 ", CalculatorResult.Text);
        }

        [TestMethod]
        public void Combination()
        {
            CalculatorSession.FindElementByAccessibilityId("num7Button").Click();
            CalculatorSession.FindElementByAccessibilityId("multiplyButton").Click();
            CalculatorSession.FindElementByAccessibilityId("num9Button").Click();
            CalculatorSession.FindElementByAccessibilityId("plusButton").Click();
            CalculatorSession.FindElementByAccessibilityId("num1Button").Click();
            CalculatorSession.FindElementByAccessibilityId("equalButton").Click();
            CalculatorSession.FindElementByAccessibilityId("divideButton").Click();
            CalculatorSession.FindElementByAccessibilityId("num8Button").Click();
            CalculatorSession.FindElementByAccessibilityId("equalButton").Click();
            Assert.AreEqual("Display is  8 ", CalculatorResult.Text);
        }

        [TestMethod]
        public void Division()
        {
            CalculatorSession.FindElementByAccessibilityId("num8Button").Click();
            CalculatorSession.FindElementByAccessibilityId("num8Button").Click();
            CalculatorSession.FindElementByAccessibilityId("divideButton").Click();
            CalculatorSession.FindElementByAccessibilityId("num1Button").Click();
            CalculatorSession.FindElementByAccessibilityId("num1Button").Click();
            CalculatorSession.FindElementByAccessibilityId("equalButton").Click();
            Assert.AreEqual("Display is  8 ", CalculatorResult.Text);
        }

        [TestMethod]
        public void Multiplication()
        {
            CalculatorSession.FindElementByAccessibilityId("num9Button").Click();
            CalculatorSession.FindElementByAccessibilityId("multiplyButton").Click();
            CalculatorSession.FindElementByAccessibilityId("num9Button").Click();
            CalculatorSession.FindElementByAccessibilityId("equalButton").Click();
            Assert.AreEqual("Display is  81 ", CalculatorResult.Text);
        }

        [TestMethod]
        public void Subtraction()
        {
            CalculatorSession.FindElementByAccessibilityId("num9Button").Click();
            CalculatorSession.FindElementByAccessibilityId("minusButton").Click();
            CalculatorSession.FindElementByAccessibilityId("num1Button").Click();
            CalculatorSession.FindElementByAccessibilityId("equalButton").Click();
            Assert.AreEqual("Display is  8 ", CalculatorResult.Text);
        }
    }
}
