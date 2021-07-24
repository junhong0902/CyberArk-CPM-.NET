using System.Collections.Generic;
using CyberArk.Extensions.Plugins.Models;
using CyberArk.Extensions.Utilties.Logger;
using CyberArk.Extensions.Utilties.Reader;
using System;

using HttpUtils;

using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Interactions;

// Change the Template name space
namespace CPMPlugin.DLL
{
    public class Verify : BaseAction
    {
        #region Consts

        public static readonly string PORT = "port";

        public string username;
        public string address;
        public string targetAccountPassword;
        #endregion

        #region constructor
        /// <summary>
        /// Logon Ctor. Do not change anything unless you would like to initialize local class members
        /// The Ctor passes the logger module and the plug-in account's parameters to base.
        /// Do not change Ctor's definition not create another.
        /// <param name="accountList"></param>
        /// <param name="logger"></param>
        public Verify(List<IAccount> accountList, ILogger logger)
            : base(accountList, logger)
        {
        }
        #endregion

        #region Setter
        /// <summary>
        /// Defines the Action name that the class is implementing - Verify
        /// </summary>
        override public CPMAction ActionName
        {
            get { return CPMAction.verifypass; }
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
                // Example: Fetch mandatory parameter - Username.
                // A mandatory parameter is a parameter that must be defined in the account.
                // TargetAccount.AccountProp is a dictionary that provides access to all the file categories of the target account.
                // An exception will be thrown if the parameter does not exist in the account.
                //username = ParametersAPI.GetMandatoryParameter("username", TargetAccount.AccountProp);
                //address = ParametersAPI.GetMandatoryParameter("address", TargetAccount.AccountProp);
                // Example: Fetch optional parmetere - Port.
                // An optional parameter is a parameter that can be defined in the account or in the platform.
                // TargetAccount.ExtraInfoProp is a dictionary that provieds access to all the platform parameters of the target account.
                // An exception will be thrown if the parameter does not exist in neither the account or the platform.
                //string strPort = ParametersAPI.GetOptionalParameter(PORT, TargetAccount.AccountProp, TargetAccount.ExtraInfoProp);

                // Note: To fetch Logon, Reconcile, Master or Usage account properties,
                // replace the TargetAccount object with the relevant account's object.

                #endregion

                #region Fetch Account's Passwords

                // Example : Fetch the target account's password.
                //targetAccountPassword = TargetAccount.CurrentPassword.convertSecureStringToString();

                // Example : Fetch the target account's new password.
                //string targetAccountNewPassword = TargetAccount.NewPassword.convertSecureStringToString();

                #endregion
            }
            catch
            {
                RC = 9000;
                platformOutput.Message = "Failed to retrieve information from CPM";
            }


           


            try
            {
                #region Logic


                string username = "admin01";
                string targetAccountPassword = "CyberArk1";
                string address = "54.251.177.238:4712";
                //string targetAccountNewPassword = "NewCyberArk1!";

                /////////////// Put your code here ////////////////////////////

                // If want to call restapi directly from dll
                string BASEURL = @"https://" + address + @"/Konfigurator/REST";
                var client = new RestClient(BASEURL, HttpVerb.POST);

                string parameter = @"/login?userName=" + username + @"&pass=" + targetAccountPassword;
                Console.WriteLine(BASEURL + parameter);

                string result = client.MakeRequest(parameter);
                Console.WriteLine(result);

                if (result.Trim().Contains("<entry><content>"))
                {
                    RC = 0;
                }
                else if (result.Trim().Contains("Check username and password"))
                {
                    RC = 8100;
                    platformOutput.Message = "Wrong Credential";
                    Console.WriteLine("Wrong Credential");
                }
                else
                {
                    RC = 8000;
                    platformOutput.Message = "Connection failure";
                    Console.WriteLine("Connection failure");
                }
                
                //Logout
                result = client.MakeRequest("/logout");
                //Console.WriteLine(result);
                //dynamic respond = JsonConvert.DeserializeObject(json);
                //string ValidID = respond.TicketID;

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
