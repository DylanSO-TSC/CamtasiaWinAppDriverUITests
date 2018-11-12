using OpenQA.Selenium.Appium.Windows;
using System;
using System.Linq;
using System.Threading;

namespace CamtasiaPageObjectPattern.Views
{
   public class BaseView
   {
      protected static WindowsDriver<WindowsElement> _camtasia;

      public void WaitForNextWindow( string currentWindowHandle )
      {
         for ( int attempt = 0; attempt < 15; attempt++ )
         {
            var mainWindow = _camtasia.WindowHandles.FirstOrDefault( h => h != currentWindowHandle );
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
