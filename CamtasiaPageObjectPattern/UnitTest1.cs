using System;
using System.IO;
using System.Threading;
using FluentAssertions;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace CamtasiaPageObjectPattern
{
   [TestClass]
   public class UnitTest1 : Sessions
   {

      [ClassInitialize]
      public static void ClassInitialize( TestContext context )
      {
         Setup( context );
      }
      [ClassCleanup]
      public static void ClassCleanup()
      {
         TearDown();
      }

      [TestMethod]
      public void TestMethod1()
      {
         _splashScreenView.WaitForSplashScreen();

         _welcomeWindowView.NewProject();

         _mainWindowView.ContinueTrial();

         _mainWindowView.Maximize();

         _mainWindowView.NewRecording();

         _recorderView.RecordABit( 10 );

         _mainWindowView.PerformCut();

         _mainWindowView.TrimMedia();

         _mainWindowView.AddCursorEffect();

         Thread.Sleep( TimeSpan.FromSeconds( 5 ) );

         _mainWindowView.AddCallout();

         _mainWindowView.ProduceCustom();

         bool fileExists = CheckForProducedMp4();
         fileExists.Should().Be( true );
      }

      private bool CheckForProducedMp4()
      {
         string myDocumentsPath = Environment.GetFolderPath( Environment.SpecialFolder.MyDocuments );
         return Directory.Exists( myDocumentsPath + "\\Camtasia\\Untitled Project" );
      }
   }
}
