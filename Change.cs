using System.Collections.Generic;
using CyberArk.Extensions.Plugins.Models;
using CyberArk.Extensions.Utilties.Logger;
using CyberArk.Extensions.Utilties.Reader;
using System;

using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Interactions;

// Change the Template name space
namespace CPMPlugin.DLL
{
    public class Change : BaseAction
    {
        #region Consts

        public static readonly string USERNAME = "username";
        public static readonly string PORT = "port";

        #endregion

        #region constructor
        /// <summary>
        /// Logon Ctor. Do not change anything unless you would like to initialize local class members
        /// The Ctor passes the logger module and the plug-in account's parameters to base.
        /// Do not change Ctor's definition not create another.
        /// <param name="accountList"></param>
        /// <param name="logger"></param>
        public Change(List<IAccount> accountList, ILogger logger)
            : base(accountList, logger)
        {
        }
        #endregion

        #region Setter
        /// <summary>
        /// Defines the Action name that the class is implementing - Change
        /// </summary>
        override public CPMAction ActionName
        {
            get { return CPMAction.changepass; }
        }
        #endregion

        /// <summary>
        /// Plug-in Starting point function.
        /// </summary>
        /// <param name="platformOutput"></param>
        override public int run(ref PlatformOutput platformOutput)
        {
            Logger.MethodStart();

            #region Init

            int RC = 9999;

            #endregion 

            try
            {

                #region Fetch Account Properties (FileCategories)
                /*
                // Example: Fetch mandatory parameter - Username.
                // A mandatory parameter is a parameter that must be defined in the account.
                // TargetAccount.AccountProp is a dictionary that provides access to all the file categories of the target account.
                // An exception will be thrown if the parameter does not exist in the account.
                string username = ParametersAPI.GetMandatoryParameter(USERNAME, TargetAccount.AccountProp);

                // Example: Fetch optional parmetere - Port.
                // An optional parameter is a parameter that can be defined in the account or in the platform.
                // TargetAccount.ExtraInfoProp is a dictionary that provieds access to all the platform parameters of the target account.
                // An exception will be thrown if the parameter does not exist in neither the account or the platform.
                string strPort = ParametersAPI.GetOptionalParameter(PORT, TargetAccount.AccountProp, TargetAccount.ExtraInfoProp);

                // Note: To fetch Logon, Reconcile, Master or Usage account properties,
                // replace the TargetAccount object with the relevant account's object.

                #endregion

                #region Fetch Account's Passwords

                // Example : Fetch the target account's password.
                string targetAccountPassword = TargetAccount.CurrentPassword.convertSecureStringToString();

                // Example : Fetch the target account's new password.
                string targetAccountNewPassword = TargetAccount.NewPassword.convertSecureStringToString();
                */
                #endregion



                #region Logic
                /////////////// Put your code here ////////////////////////////

                string username = "admin01";
                string targetAccountPassword = "NewCyberArk1!";
                string targetAccountNewPassword = "CyberArk1";

                /////////////// Put your code here ////////////////////////////
                string url = "https://54.251.177.238:4712/Konfigurator/html/html-ui.html?anonym=true&app=Konfigurator";
                ChromeOptions options = new ChromeOptions();

                options.AcceptInsecureCertificates = true;
                options.AddArgument("--window-size=800,600");
                //options.AddArgument("--no-sandbox");
                options.AddArgument("--incognito");
                options.AddArgument("--headless"); //Comment this for debugging. CPM runs in headless mode!

                //IWebDriver driver = new ChromeDriver(@"C:\Users\junhong.choo\Documents\GitHub\Selenium-CSharp\Selenium-CSharp\Drivers\", options);
                IWebDriver driver = new ChromeDriver(@"C:\Program Files (x86)\CyberArk\Password Manager\bin\lib\", options);
                driver.Navigate().GoToUrl(url);
                System.Threading.Thread.Sleep(8000);
                try
                {
                    Console.WriteLine("location=");
                    Console.WriteLine(driver.FindElement(By.XPath("/html/body/div/canvas")).Location);
                }
                catch
                {
                    RC = 8000;
                    platformOutput.Message = "Connection failure";
                    Console.WriteLine("cannot find element");
                }


                IWebElement canvas = driver.FindElement(By.XPath("/html/body/div/canvas"));
                Actions actions = new Actions(driver);

                //Login
                cSendKey(driver, username);
                System.Threading.Thread.Sleep(500);
                cSendKey(driver, Keys.Tab);
                System.Threading.Thread.Sleep(500);
                cSendKey(driver, targetAccountPassword);
                System.Threading.Thread.Sleep(500);
                cSendKey(driver, Keys.Enter);
                System.Threading.Thread.Sleep(7000);

                //After Login, close stupid pop up
                Console.WriteLine("Close pop up!");
                cClick(driver, canvas, 690, 10);
                System.Threading.Thread.Sleep(3000);
                cSendKey(driver, Keys.Enter);
                System.Threading.Thread.Sleep(2000);

                //Change Password start here
                Console.WriteLine("Click User Preference");
                cClick(driver, canvas, 660, 30);
                System.Threading.Thread.Sleep(2000);

                cSendKey(driver, Keys.Tab);
                System.Threading.Thread.Sleep(500);
                cSendKey(driver, Keys.Tab);
                System.Threading.Thread.Sleep(500);
                cSendKey(driver, Keys.Tab);
                System.Threading.Thread.Sleep(500);
                cSendKey(driver, Keys.Tab);
                System.Threading.Thread.Sleep(500);
                cSendKey(driver, Keys.Space);
                System.Threading.Thread.Sleep(500);
                cSendKey(driver, targetAccountNewPassword);
                System.Threading.Thread.Sleep(500);
                cSendKey(driver, Keys.Tab);
                System.Threading.Thread.Sleep(500);
                cSendKey(driver, targetAccountNewPassword);
                System.Threading.Thread.Sleep(500);
                cSendKey(driver, Keys.Enter);
                System.Threading.Thread.Sleep(500);

                Console.WriteLine("After Password Change!");
                cSendKey(driver, Keys.Tab);
                System.Threading.Thread.Sleep(500);
                cSendKey(driver, Keys.Tab);
                System.Threading.Thread.Sleep(500);
                cSendKey(driver, Keys.Tab);
                System.Threading.Thread.Sleep(500);
                cSendKey(driver, Keys.Tab);
                System.Threading.Thread.Sleep(500);
                cSendKey(driver, Keys.Tab);
                System.Threading.Thread.Sleep(500);
                cSendKey(driver, Keys.Space);
                System.Threading.Thread.Sleep(500);

                //Logout
                Console.WriteLine("Logging Out!");
                cClick(driver, canvas, 740, 30);
                System.Threading.Thread.Sleep(2000);

                RC = 0;
                /////////////// Put your code here ////////////////////////////
                #endregion Logic

            }
            catch (Exception ex)
            {
                RC = HandleGeneralError(ex, ref platformOutput);
            }
            finally
            {
                Logger.MethodEnd();
            }

            // Important:
            // 1.RC must be set to 0 in case of success, or 8000-9000 in case of an error.
            // 2.In case of an error, platformOutput.Message must be set with an informative error message, as it will be displayed to end user in PVWA.
            //   In case of success (RC = 0), platformOutput.Message can be left empty as it will be ignored.
            return RC;

        }

    }
}
