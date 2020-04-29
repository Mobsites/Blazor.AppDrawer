// Copyright (c) 2020 Allan Mobley. All rights reserved.
// Licensed under the MIT License. See License.txt in the project root for license information.

using System.Threading.Tasks;
using Microsoft.AspNetCore.Components;

namespace Mobsites.Blazor
{
    /// <summary>
    /// Subcomponent for adding header content to the <see cref="AppDrawer"/> component.
    /// Things like account switchers and titles should live here.
    /// </summary>
    public partial class AppDrawerHeader
    {
        /// <summary>
        /// Content to render.
        /// </summary>
        [Parameter] public RenderFragment ChildContent { get; set; }

        /// <summary>
        /// Whether to inherit a parent's colors (dark, light, or normal modes).
        /// </summary>
        [Parameter] public override bool InheritParentColors { get; set; } = true;

        /// <summary>
        /// The foreground color for this component's dark mode.
        /// </summary>
        [Parameter] public override string DarkModeBackgroundColor { get; set; } = "rgba(0,0,0,0.4)";

        /// <summary>
        /// The foreground color for this component's light mode.
        /// </summary>
        [Parameter] public override string LightModeBackgroundColor { get; set; } = "rgba(0,0,0,0.05)";

        protected override void OnParametersSet()
        {
            // This will check for valid parent.
            base.OnParametersSet();
            base.Parent.AppDrawerHeader = this;
        }

        internal void SetOptions(AppDrawer.Options options)
        {
            
        }

        internal async Task CheckState(AppDrawer.Options options)
        {
            
        }
    }
}