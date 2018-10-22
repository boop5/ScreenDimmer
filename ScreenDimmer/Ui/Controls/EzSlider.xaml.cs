using System.Windows;
using DependencyPropertyHelper = SchadLucas.Wpf.Utilities.DependencyPropertyHelper;

namespace ScreenDimmer.Ui.Controls
{
    public partial class EzSlider
    {
        public static readonly DependencyProperty SliderNameProperty = DependencyPropertyHelper.Register<EzSlider, string>(nameof(SliderName));
        public static readonly DependencyProperty SliderValueProperty = DependencyPropertyHelper.Register<EzSlider, byte>(nameof(SliderValue));
        public static readonly DependencyProperty SliderMinProperty = DependencyPropertyHelper.Register<EzSlider, double>(nameof(SliderMin));
        public static readonly DependencyProperty SliderMaxProperty = DependencyPropertyHelper.Register<EzSlider, double>(nameof(SliderMax));
        public static readonly DependencyProperty SliderTickFrequencyProperty = DependencyPropertyHelper.Register<EzSlider, double>(nameof(SliderTickFrequency));
        public static readonly DependencyProperty SliderSmallChangeProperty = DependencyPropertyHelper.Register<EzSlider, double>(nameof(SliderSmallChange));
        public static readonly DependencyProperty SliderLargeChangeProperty = DependencyPropertyHelper.Register<EzSlider, double>(nameof(SliderLargeChange));

        public EzSlider()
        {
            InitializeComponent();
        }

        public double SliderLargeChange
        {
            get => (double) GetValue(SliderLargeChangeProperty);
            set => SetValue(SliderLargeChangeProperty, value);
        }

        public double SliderMax
        {
            get => (double) GetValue(SliderMaxProperty);
            set => SetValue(SliderMaxProperty, value);
        }

        public double SliderMin
        {
            get => (double) GetValue(SliderMinProperty);
            set => SetValue(SliderMinProperty, value);
        }

        public string SliderName
        {
            get => (string) GetValue(SliderNameProperty);
            set => SetValue(SliderNameProperty, value);
        }

        public double SliderSmallChange
        {
            get => (double) GetValue(SliderSmallChangeProperty);
            set => SetValue(SliderSmallChangeProperty, value);
        }

        public double SliderTickFrequency
        {
            get => (double) GetValue(SliderTickFrequencyProperty);
            set => SetValue(SliderTickFrequencyProperty, value);
        }

        public byte SliderValue
        {
            get => (byte) GetValue(SliderValueProperty);
            set => SetValue(SliderValueProperty, value);
        }
    }
}