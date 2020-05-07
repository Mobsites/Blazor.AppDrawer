// Copyright (c) 2020 Allan Mobley. All rights reserved.
// Licensed under the MIT License. See License.txt in the project root for license information.

using Microsoft.AspNetCore.Components;

namespace Mobsites.Blazor
{
    /// <summary>
    /// Subcomponent for adding scrollable content to the <see cref="AppDrawer"/> component.
    /// Things like navigation destinations and other app functionality, such as a restart option, should live here.
    /// </summary>
    public sealed partial class AppDrawerContent
    {
        /****************************************************
        *
        *  PUBLIC INTERFACE
        *
        ****************************************************/

        /// <summary>
        /// Content to render.
        /// </summary>
        [Parameter] public RenderFragment ChildContent { get; set; }

        /// <summary>
        /// Whether to inherit a parent's colors (dark, light, or normal modes).
        /// </summary>
        [Parameter] public override bool InheritParentColors { get; set; } = true;



        /****************************************************
        *
        *  NON-PUBLIC INTERFACE
        *
        ****************************************************/

        /// <summary>
        /// Dom element reference passed into javascript representation.
        /// </summary>
        internal ElementReference ElemRef { get; set; }

        /// <summary>
        /// Life cycle method for when parameters from parent are set.
        /// </summary>
        protected override void OnParametersSet()
        {
            // This will check for valid parent.
            base.OnParametersSet();
            base.Parent.Content = this;
        }
    }
}