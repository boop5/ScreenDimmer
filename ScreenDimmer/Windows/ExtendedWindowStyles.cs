using System;
using System.Diagnostics.CodeAnalysis;

namespace ScreenDimmer.Windows
{
    [Flags]
    [SuppressMessage("ReSharper", "InconsistentNaming")]
    public enum ExtendedWindowStyles
    {
        WS_EX_TOOLWINDOW = 0x00000080,
        EX_TRANSPARENT = 0x00000020
    }
}