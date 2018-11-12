using OpenQA.Selenium.Appium.Windows;
using System;
using System.Linq;
using System.Threading;

namespace CamtasiaPageObjectPattern.Views
{
   public class SplashScreenView : BaseView
   {
      public SplashScreenView( WindowsDriver<WindowsElement> camtasia )
      {
         _camtasia = camtasia;
      }

      public void WaitForSplashScreen()
      {
         var splashScreenHandle = _camtasia.CurrentWindowHandle;

         for ( int attempt = 0; attempt < 15; attempt++ )
         {
            var mainWindow = _camtasia.WindowHandles.FirstOrDefault( h => h != splashScreenHandle );
            if ( mainWindow != null )
            {
               _camtasia.SwitchTo().Window( mainWindow );
               break;
            }

            Thread.Sleep( TimeSpan.FromSeconds( 1 ) );
         }
      }
   }
}
