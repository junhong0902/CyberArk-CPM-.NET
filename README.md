# CyberArk CPM - .NET SDK - RestAPI - Selenium Driver (Complete Integration)
This CPM plugin utilized CyberArk's .NET SDK. In the C#, I integrate RestAPI and Selenium web driver. 

## Verify and logon
Verify and logon uses RestAPI call to verify the credentials of the acccount.

## Change
Password Change process uses Selenium web driver to perform mouseclick (in this case it click the point relative to a element)
If button or link can directly be found via web-element, we directly perform click on the element.

### Requirement
As this plugin uses chrome driver, chrome driver path is mandatory and needs to be created in privateark client.
```
Mandatory parameter: Username, Address, Port, ChromeDriverPath
ChromeDriverPath is custom made parameter, need to update in privateark client
```
