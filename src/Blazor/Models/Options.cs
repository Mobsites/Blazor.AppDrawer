// Copyright (c) 2020 Allan Mobley. All rights reserved.
// Licensed under the MIT License. See License.txt in the project root for license information.

namespace Mobsites.Blazor
{
    public partial class AppDrawer
    {
        internal class Options : OptionsBase
        {
            public bool ModalOnly { get; set; }
            public int? ResponsiveBreakpoint { get; set; }
            public string Title { get; set; }
            public string Subtitle { get; set; }
            public bool UseImage { get; set; }
            public bool Destroy { get; set; }
        }
    }
}