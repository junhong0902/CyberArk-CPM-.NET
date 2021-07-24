using System.Collections.Generic;
using CyberArk.Extensions.Plugins.Models;
using CyberArk.Extensions.Utilties.CPMPluginErrorCodeStandarts;
using CyberArk.Extensions.Utilties.Logger;
using CyberArk.Extensions.Utilties.CPMParametersValidation;
using System;

using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Interactions;

// Change the Template name space
namespace CPMPlugin.DLL
{
    /*
     * Base Action class should contain common plug-in functionality and parameters.
     * For specific action functionality and parameters use the action classes.
     */
    abstract public class BaseAction : AbsAction
    {
        #region Properties

        internal ParametersManager ParametersAPI { get; private set; }

        #endregion

        #region constructor
        /// <summary>
        /// BaseAction Ctor. Do not change anything unless you would like to initialize local class members
        /// The Ctor passes the logger module and the plug-in account's parameters to base.
        /// Do not change Ctor's definition not create another.
        /// <param name="accountList"></param>
        /// <param name="logger"></param>
        public BaseAction(List<IAccount> accountList, ILogger logger)
            : base(accountList, logger)
        {
            // Init ParametersManager
            ParametersAPI = new ParametersManager();
        }
        #endregion

        /// <summary>
        /// Handle the general RC and error message.
        /// <summary>
        /// <param name="ex"></param>
        /// <param name="platformOutput"></param>
        internal int HandleGeneralError(Exception ex, ref PlatformOutput platformOutput)
        {
            ErrorCodeStandards errCodeStandards = new ErrorCodeStandards();
            Logger.WriteLine(string.Format("Received exception: {0}.", ex), CyberArk.Extensions.Utilties.Logger.LogLevel.ERROR);
            platformOutput.Message = errCodeStandards.ErrorStandardsDict[PluginErrors.STANDARD_DEFUALT_ERROR_CODE_IDX].ErrorMsg;
            return errCodeStandards.ErrorStandardsDict[PluginErrors.STANDARD_DEFUALT_ERROR_CODE_IDX].ErrorRC; 
        }

        public static object cSendKey(IWebDriver driver, string input)
        {
            Actions actions = new Actions(driver);
            actions.SendKeys(input).Perform();
            return null;
        }

        public static object cDoubleClick(IWebDriver driver, IWebElement element, int x, int y)
        {
            Actions actions = new Actions(driver);
            actions.MoveToElement(element, 0, 0).Build().Perform();
            actions.MoveByOffset(x, y).Build().Perform();
            actions.DoubleClick().Perform();
            return null;
        }

        public static object cClick(IWebDriver driver, IWebElement element, int x, int y)
        {
            Actions actions = new Actions(driver);
            actions.MoveToElement(element, 0, 0).Build().Perform();
            actions.MoveByOffset(x, y).Build().Perform();
            actions.Click().Perform();
            return null;
        }

    }
}
