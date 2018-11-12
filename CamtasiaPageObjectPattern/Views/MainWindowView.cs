using OpenQA.Selenium.Appium;
using OpenQA.Selenium.Appium.Windows;
using OpenQA.Selenium.Interactions;
using System;
using System.Collections.ObjectModel;
using System.Linq;
using System.Threading;

namespace CamtasiaPageObjectPattern.Views
{
   public class MainWindowView : BaseView
   {
      WindowsElement _continueTrial => _camtasia.FindElementByName("Continue Trial >");
      WindowsElement _recordButton => _camtasia.FindElementByAccessibilityId( "recordButton" );
      WindowsElement _selectionEndThumb => _camtasia.FindElementByAccessibilityId( "SelectionEndThumb" );
      WindowsElement _cutButton => _camtasia.FindElementByAccessibilityId( "cutButton" );
      WindowsElement _mediaScroll => _camtasia.FindElementByAccessibilityId( "mediaScrollViewer" );
      AppiumWebElement _timelineMedia => _mediaScroll.FindElementByAccessibilityId( "timelineMedia" );
      AppiumWebElement _wrapperRoot => _timelineMedia.FindElementsByAccessibilityId( "TimelineMediaWrapperRoot" )[0];
      AppiumWebElement _mediaRoot => _wrapperRoot.FindElementByAccessibilityId( "timelineMediaRoot" );
      AppiumWebElement _rightHandle => _mediaRoot.FindElementByAccessibilityId( "PART_RightTrimHandle" );
      ReadOnlyCollection<WindowsElement> _camtasiaTools => _camtasia.FindElementsByAccessibilityId( "toolButton" );

      //Cursor Effects
      WindowsElement _cursorEffects => _camtasiaTools[7];
      ReadOnlyCollection<WindowsElement> _effects => _camtasia.FindElementsByAccessibilityId( "effectControl" );
      WindowsElement _firstEffect => _effects[0];
      WindowsElement _addToSelectedMedia => _camtasia.FindElementByName( "Add to Selected Media" );
      //
      //Annotations
      WindowsElement _annotations => _camtasiaTools[3];
      WindowsElement _calloutView => _camtasia.FindElementByAccessibilityId( "CalloutView" );
      ReadOnlyCollection<AppiumWebElement> _callouts => _calloutView.FindElementsByClassName( "Button" );
      AppiumWebElement _firstCallout => _callouts.First( c => c.Displayed );
      //
      WindowsElement _shareButton => _camtasia.FindElementByAccessibilityId( "shareButton" );
      WindowsElement _shareDestination => _camtasia.FindElementByClassName( "ShareDestination" );
      WindowsElement _produceWithWaterMark => _camtasia.FindElementByAccessibilityId( "ProduceWithWatermarkButton" );
      WindowsElement _nextButton => _camtasia.FindElementByAccessibilityId( "12324" );
      WindowsElement _finishButton => _camtasia.FindElementByAccessibilityId( "12325" );
      WindowsElement _saveDialog => _camtasia.FindElementByName( "TechSmith Camtasia" );
      AppiumWebElement _noButton => _saveDialog.FindElementByName( "No" );

      public MainWindowView( WindowsDriver<WindowsElement> camtasia )
      {
         _camtasia = camtasia;
      }

      public void Maximize()
      {
         _camtasia.Manage().Window.Maximize();
      }

      public void NewRecording()
      {
         _recordButton.Click();

         Thread.Sleep( TimeSpan.FromSeconds( 5 ) );
      }

      public void DismissSaveDialog()
      {
         try
         {
            _noButton.Click();
         }
         catch { };
      }

      public void PerformCut()
      {
         new Actions( _camtasia ).MoveToElement( _selectionEndThumb, _selectionEndThumb.Size.Width, _selectionEndThumb.Size.Height - ( _selectionEndThumb.Size.Height / 3 ) ).Build().Perform();
         Thread.Sleep( TimeSpan.FromMilliseconds( 1000 ) );
         _camtasia.Mouse.MouseDown( null );
         Thread.Sleep( TimeSpan.FromMilliseconds( 1000 ) );
         new Actions( _camtasia ).MoveByOffset( 50, 0 ).Release().Click( _cutButton ).Build().Perform();
      }

      public void TrimMedia()
      {
         new Actions( _camtasia ).MoveToElement( _rightHandle, _rightHandle.Size.Width + 5, _rightHandle.Size.Height / 2 + 10 ).Build().Perform();
         new Actions( _camtasia ).ClickAndHold().MoveByOffset( -200, 0 ).Release().Build().Perform();
      }

      public void AddCursorEffect()
      {
         Thread.Sleep( TimeSpan.FromSeconds( 3 ) );
         _cursorEffects.Click();
         new Actions( _camtasia ).ContextClick( _effects.First( e => e.Displayed ) ).Build().Perform();
         Thread.Sleep( 1000 );
         _addToSelectedMedia.Click();
      }

      public void AddCallout()
      {
         _annotations.Click();
         new Actions( _camtasia ).DoubleClick( _callouts.First( c => c.Displayed ) ).Build().Perform();
         Thread.Sleep( 1000 );
      }

      public void ProduceCustom()
      {
         _shareButton.Click();
         _shareDestination.Click();
         try
         {
            _produceWithWaterMark.Click();
         }
         catch { }
         _nextButton.Click();
         _nextButton.Click();
         _nextButton.Click();
         _nextButton.Click();

         _finishButton.Click();

         Thread.Sleep( TimeSpan.FromSeconds( 5 ) );

         _finishButton.Click();
      }

      public void ContinueTrial()
      {
         try
         {
            _continueTrial.Click();
         }
         catch { };
      }
   }
}
