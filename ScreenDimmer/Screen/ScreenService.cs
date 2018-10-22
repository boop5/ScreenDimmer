using System;
using System.Collections.Generic;
using System.Linq;
using ScreenDimmer.Extensions.System.Windows.Media;
using ScreenDimmer.Overlay;
using ScreenDimmer.Root;
using WpfScreenHelper;

namespace ScreenDimmer.Screen
{
    public class ScreenService
    {
        private readonly Dictionary<string, OverlayWindow> _overlays = new Dictionary<string, OverlayWindow>();

        public List<ScreenDataObject> Screens { get; private set; } = new List<ScreenDataObject>();

        /// <summary>
        ///     Dims the the provided <see cref="Screen" />.
        /// </summary>
        /// <param name="screen">The screen to dim.</param>
        /// <param name="value">
        ///     The value by which the screen will be dimmed. Accepts values between 0.0 and 1.0.
        /// </param>
        /// <exception cref="ArgumentOutOfRangeException">
        ///     Raised when <paramref name="value" /> is less than 0 or more than 1.
        /// </exception>
        public void Dim(ScreenDataObject screen, double value)
        {
            if (value < 0 || value > 1)
            {
                throw new ArgumentOutOfRangeException(nameof(value));
            }

            var overlay = GetOverlayFromScreen(screen);
            overlay.Dim(value);
        }

        public void Dye(ScreenDataObject screen, byte r, byte g, byte b, byte a = 255)
        {
            GetOverlayFromScreen(screen).Dye(r, g, b, a);
        }

        public double GetDim(ScreenDataObject screen) => GetOverlayFromScreen(screen).GetDim();

        public EzColor GetDye(ScreenDataObject screen)
        {
            var c = GetOverlayFromScreen(screen).Background.GetColor();

            return new EzColor {R = c.R, G = c.G, B = c.B};
        }

        public void Update()
        {
            var allScreens = WpfScreenHelper.Screen.AllScreens.ToList();

            foreach (var screen in allScreens)
            {
                // add new screens
                if (!Screens.Any(s => s.DeviceName == screen.DeviceName && s.IsPrimary == screen.Primary))
                {
                    var newScreen = BuildScreenFromHelper(screen);
                    Screens.Add(newScreen);
                }
            }

            // remove missing screens
            foreach (var screen in Screens)
            {
                if (!allScreens.Any(s => s.DeviceName == screen.DeviceName && s.Primary == screen.IsPrimary))
                {
                    Screens.Remove(screen);
                }
            }
        }

        private ScreenDataObject BuildScreenFromHelper(WpfScreenHelper.Screen screen)
        {
            var s = new ScreenDataObject
            {
                IsPrimary = screen.Primary,
                Bounds = screen.Bounds,
                WorkingArea = screen.WorkingArea,
                DeviceName = screen.DeviceName
            };

            s.Dye = GetDye(s);
            s.Dim = GetDim(s);

            return s;
        }

        private OverlayWindow GetOverlayFromScreen(ScreenDataObject screen)
        {
            if (!_overlays.ContainsKey(screen.DeviceName))
            {
                var window = new OverlayWindow(screen);
                window.Show();
                _overlays.Add(screen.DeviceName, window);
            }

            return _overlays[screen.DeviceName];
        }
    }
}