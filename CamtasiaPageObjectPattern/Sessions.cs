using CamtasiaPageObjectPattern.Views;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using OpenQA.Selenium.Appium.Windows;
using OpenQA.Selenium.Remote;
using System;
using System.Diagnostics;
using System.IO;

namespace CamtasiaPageObjectPattern
{
   public class Sessions
   {

      //protected static ReadOnlyCollection<WindowsElement> _camtasiaTools;
      private const string _editorPath = @"C:\Program Files\TechSmith\Camtasia 2018\CamtasiaStudio.exe";
      private const string _recorderPath = @"C:\Program Files\TechSmith\Camtasia 2018\CamRecorder.exe";
      private const string _winAppDriverPath = @"C:\Program Files (x86)\Windows Application Driver\WinAppDriver.exe";

      protected static WindowsDriver<WindowsElement> _desktopSession;
      protected static WindowsDriver<WindowsElement> _camtasiaSession;

      protected static SplashScreenView _splashScreenView;
      protected static WelcomeWindowView _welcomeWindowView;
      protected static MainWindowView _mainWindowView;
      protected static RecorderView _recorderView;

      protected static Process _winAppDriver = new Process();

      public static void Setup( TestContext context )
      {
         try
         {
            DeleteProductionFiles();
         }
         catch { }

         _winAppDriver.StartInfo.UseShellExecute = true;
         _winAppDriver.StartInfo.FileName = _winAppDriverPath;
         _winAppDriver.StartInfo.Verb = "runas";
         _winAppDriver.Start();

         //Desktop
         DesiredCapabilities _desktopCapabilities = new DesiredCapabilities();
         _desktopCapabilities.SetCapability( "app", "Root" );
         _desktopSession = new WindowsDriver<WindowsElement>( new Uri( "http://127.0.0.1:4723" ), _desktopCapabilities );
         _desktopSession.Manage().Timeouts().ImplicitlyWait( TimeSpan.FromSeconds( 1.5 ) );

         //Camtasia
         DesiredCapabilities _appCapabilities = new DesiredCapabilities();
         _appCapabilities.SetCapability( "app", _editorPath );
         _camtasiaSession = new WindowsDriver<WindowsElement>( new Uri( "http://127.0.0.1:4723" ), _appCapabilities );
         _camtasiaSession.Manage().Timeouts().ImplicitlyWait( TimeSpan.FromSeconds( 2.5 ) );

         //Views
         _splashScreenView = new SplashScreenView( _camtasiaSession );
         _welcomeWindowView = new WelcomeWindowView( _camtasiaSession );
         _mainWindowView = new MainWindowView( _camtasiaSession );
         _recorderView = new RecorderView( _desktopSession );
      }

      public static void TearDown()
      {
         _camtasiaSession.Close();
         _mainWindowView.DismissSaveDialog();
         _desktopSession.Close();
         _winAppDriver.Kill();
      }
      internal static void DeleteProductionFiles()
      {
         string myDocumentsPath = Environment.GetFolderPath( Environment.SpecialFolder.MyDocuments );
         Directory.Delete( myDocumentsPath + "\\Camtasia\\Untitled Project", true );
      }

   }
}
