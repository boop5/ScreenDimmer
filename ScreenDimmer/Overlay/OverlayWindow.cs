using System;
using System.Windows;
using System.Windows.Interop;
using System.Windows.Media;
using ScreenDimmer.Screen;
using ScreenDimmer.Windows;

namespace ScreenDimmer.Overlay
{
    internal sealed class OverlayWindow : Window
    {
        public OverlayWindow(ScreenDataObject screen)
        {
            WindowStyle = WindowStyle.None;
            WindowState = WindowState.Maximized;
            WindowStartupLocation = WindowStartupLocation.Manual;
            ResizeMode = ResizeMode.NoResize;
            Topmost = true;
            AllowsTransparency = true;
            ShowInTaskbar = false;
            
            Opacity = 0;
            Background = Brushes.Black;
            Left = screen.Bounds.Left;
            Top = screen.Bounds.Top;
            Width = screen.Bounds.Width;
            Height = screen.Bounds.Height;

            Loaded += OnLoaded;
        }

        public void Dim(double value)
        {
            if (value < 0 || value > 1)
            {
                throw new ArgumentOutOfRangeException(nameof(value));
            }

            Opacity = value;
        }

        public void Dye(byte r, byte g, byte b, byte a = 255)
        {
            var color = Color.FromArgb(a, r, g, b);
            var brush = new SolidColorBrush(color);

            Background = brush;
        }

        public double GetDim() => Opacity;

        protected override void OnSourceInitialized(EventArgs e)
        {
            base.OnSourceInitialized(e);
            var hwnd = new WindowInteropHelper(this).Handle;
            WindowsServices.SetWindowExTransparent(hwnd);
        }

        private void OnLoaded(object sender, RoutedEventArgs e)
        {
            var wndHelper = new WindowInteropHelper(this);
            var exStyle = WindowsServices.GetWindowLong(wndHelper.Handle, (int) GetWindowLongFields.EX_STYLE);
            exStyle |= (int) ExtendedWindowStyles.WS_EX_TOOLWINDOW;
            WindowsServices.SetWindowLong(wndHelper.Handle, (int) GetWindowLongFields.EX_STYLE, exStyle);
        }
    }
}