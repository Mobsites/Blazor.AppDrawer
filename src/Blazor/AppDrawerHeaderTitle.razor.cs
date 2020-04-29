// Copyright (c) 2020 Allan Mobley. All rights reserved.
// Licensed under the MIT License. See License.txt in the project root for license information.

using Microsoft.AspNetCore.Components;

namespace Mobsites.Blazor
{
    /// <summary>
    /// Child component for adding a title to the <see cref="AppDrawerHeader"/> component.
    /// </summary>
    public partial class AppDrawerHeaderTitle
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
        /// The background color for this component. Accepts any valid css color usage.
        /// </summary>
        public override string BackgroundColor
        {
            get => "transparent";
            set => base.Color = value;
        }
    }
}