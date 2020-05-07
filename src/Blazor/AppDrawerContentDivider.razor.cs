// Copyright (c) 2020 Allan Mobley. All rights reserved.
// Licensed under the MIT License. See License.txt in the project root for license information.

using Microsoft.AspNetCore.Components;

namespace Mobsites.Blazor
{
    /// <summary>
    /// Child component for adding a content divider to the <see cref="AppDrawerContent"/> component.
    /// </summary>
    public sealed partial class AppDrawerContentDivider
    {
        private string style;

        /// <summary>
        /// Styles for directly affecting this component go here.
        /// </summary>
        [Parameter]
        public override string Style
        {
            get
            {
                string color = string.IsNullOrWhiteSpace(this.Color) ? null : $"border-bottom-color: {this.Color};";

                return color + style;
            }
            set => style = value;
        }

        /// <summary>
        /// The foreground color for this component's dark mode.
        /// </summary>
        [Parameter] public override string DarkModeColor { get; set; } = "rgba(255, 255, 255, 0.3)";

        /// <summary>
        /// The foreground color for this component's light mode.
        /// </summary>
        [Parameter] public override string LightModeColor { get; set; } = "rgba(0, 0, 0, 0.15)";
    }
}