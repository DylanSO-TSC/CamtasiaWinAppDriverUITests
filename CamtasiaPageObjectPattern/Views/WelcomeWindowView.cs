using OpenQA.Selenium.Appium.Windows;

namespace CamtasiaPageObjectPattern.Views
{
   public class WelcomeWindowView : BaseView
   {
      //View Elements
      public WindowsElement _newProjectButton => _camtasia.FindElementByAccessibilityId( "t-WelcomeWindow_NewProjectButton" );

      public WelcomeWindowView( WindowsDriver<WindowsElement> camtasia )
      {
         _camtasia = camtasia;
      }

      public void NewProject()
      {
         var currentWindowHandle = _camtasia.CurrentWindowHandle;
         try
         {
            _newProjectButton.Click();
            WaitForNextWindow( currentWindowHandle );
         }
         catch { }
      }
   }
}
