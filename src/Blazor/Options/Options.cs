// Copyright (c) 2020 Allan Mobley. All rights reserved.
// Licensed under the MIT License. See License.txt in the project root for license information.

namespace Mobsites.Blazor
{
    public partial class AppDrawer
    {
        internal class Options : StatefulComponentOptions
        {
            public bool ModalOnly { get; set; }
            public int? ResponsiveBreakpoint { get; set; }
            public bool Destroy { get; set; }
        }
    }
}