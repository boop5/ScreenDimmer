using SchadLucas.Configuration;
using ScreenDimmer.Root;
using ScreenDimmer.Screen;

namespace ScreenDimmer.Configuration
{
    public class ConfigurationService
    {
        private readonly EzConfiguration _configuration;

        public ConfigurationService(EzConfiguration configuration)
        {
            _configuration = configuration;
        }

        public T Get<T>(string key)
        {
            return _configuration.Get<T>(key);
        }

        public double GetDim(ScreenDataObject screen)
        {
            return Get<double>(GetDimName(screen));
        }

        public EzColor GetDye(ScreenDataObject screen)
        {
            var r = GetDyeR(screen);
            var g = GetDyeG(screen);
            var b = GetDyeB(screen);

            return new EzColor {R = r, G = g, B = b};
        }

        public void Set(string key, object value)
        {
            _configuration.Set(key, value);
        }

        public void UpdateScreenSettings(ScreenDataObject screen)
        {
            Set(GetDyeRName(screen), screen.Dye.R);
            Set(GetDyeGName(screen), screen.Dye.G);
            Set(GetDyeBName(screen), screen.Dye.B);
            Set(GetDimName(screen), screen.Dim);
        }

        private static string GetDimName(ScreenDataObject screen) => GetName(screen, "Dim");
        private static string GetDyeBName(ScreenDataObject screen) => GetName(screen, "B");
        private static string GetDyeGName(ScreenDataObject screen) => GetName(screen, "G");
        private static string GetDyeRName(ScreenDataObject screen) => GetName(screen, "R");

        private static string GetName(ScreenDataObject screen, string key) => $"{screen.DeviceName}_{key}";

        private byte GetDyeB(ScreenDataObject screen) => Get<byte>(GetDyeBName(screen));
        private byte GetDyeG(ScreenDataObject screen) => Get<byte>(GetDyeGName(screen));
        private byte GetDyeR(ScreenDataObject screen) => Get<byte>(GetDyeRName(screen));
    }
}