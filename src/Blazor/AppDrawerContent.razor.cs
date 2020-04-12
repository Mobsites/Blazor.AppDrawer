// Copyright (c) 2020 Allan Mobley. All rights reserved.
// Licensed under the MIT License. See License.txt in the project root for license information.

using Microsoft.AspNetCore.Components;

namespace Mobsites.Blazor
{
    /// <summary>
    /// Blazor child component for adding scrollable content to the <see cref="AppDrawer"/> component.
    /// Things like navigation destinations and other app functionality, such as a restart option, should live here.
    /// </summary>
    public partial class AppDrawerContent
    {
        /// <summary>
        /// Content to render.
        /// </summary>
        [Parameter] public RenderFragment ChildContent { get; set; }
        
        internal string Color => Parent.Color;
        
        protected override void OnParametersSet()
        {
            // This will check for valid parent.
            base.OnParametersSet();
            base.Parent.AppDrawerContent = this;
        }

        internal void SetOptions(AppDrawer.Options options)
        {
           
        }

        internal void CheckState(AppDrawer.Options options)
        {
            
        }
    }
}