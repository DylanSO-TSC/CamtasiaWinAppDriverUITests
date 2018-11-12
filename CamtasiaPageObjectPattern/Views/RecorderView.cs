using OpenQA.Selenium;
using OpenQA.Selenium.Appium;
using OpenQA.Selenium.Appium.Windows;
using System;
using System.Threading;

namespace CamtasiaPageObjectPattern.Views
{
    public class RecorderView
    {
        private static WindowsDriver<WindowsElement> _desktop;
        WindowsElement _recorder => _desktop.FindElementByName("Camtasia Recorder 2018");
        AppiumWebElement _recorderCentral => _recorder.FindElementByName("RecorderCentral");
        AppiumWebElement _recordButton => _recorderCentral.FindElementByName("BtnRecord");
        AppiumWebElement _closeButton => _recorder.FindElementByName("BtnClose");


        public RecorderView(WindowsDriver<WindowsElement> desktop)
        {
            _desktop = desktop;
        }

        public void StartRecording()
        {
            _recordButton.Click();
        }

        public void StopRecording()
        {
            _desktop.Keyboard.PressKey(Keys.F10);
        }

        public void RecordABit(int RecordLength)
        {
            StartRecording();

            Thread.Sleep(TimeSpan.FromSeconds(RecordLength));

            StopRecording();

            Thread.Sleep(TimeSpan.FromSeconds(2));
        }

        public void CloseRecorder()
        {
            try
            {
                _closeButton.Click();
            }
            catch { }
        }
    }
}
