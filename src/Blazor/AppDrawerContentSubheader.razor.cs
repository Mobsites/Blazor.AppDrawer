// Copyright (c) 2020 Allan Mobley. All rights reserved.
// Licensed under the MIT License. See License.txt in the project root for license information.

using Microsoft.AspNetCore.Components;

namespace Mobsites.Blazor
{
    /// <summary>
    /// Child component for adding a subheader to the <see cref="AppDrawerContent"/> component.
    /// </summary>
    public sealed partial class AppDrawerContentSubheader
    {
        /// <summary>
        /// Content to render.
        /// </summary>
        [Parameter] public RenderFragment ChildContent { get; set; }

        /// <summary>
        /// The foreground color for this component's dark mode.
        /// </summary>
        [Parameter] public override string DarkModeColor { get; set; } = "rgba(255, 255, 255, 0.3)";

        /// <summary>
        /// The foreground color for this component's light mode.
        /// </summary>
        [Parameter] public override string LightModeColor { get; set; } = "rgb(128, 128, 128)";
    }
}