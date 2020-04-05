// Copyright (c) 2020 Allan Mobley. All rights reserved.
// Licensed under the MIT License. See License.txt in the project root for license information.

using Microsoft.AspNetCore.Components;
using System;
using System.Collections.Generic;


namespace Mobsites.Blazor
{
    /// <summary>
    /// Blazor child component for adding a subheader to the <see cref="AppDrawerContent"/> component.
    /// </summary>
    public partial class AppDrawerContentSubheader
    {
        /// <summary>
        /// Parent container.
        /// </summary>
        [CascadingParameter] internal AppDrawerContent Parent { get; set; }
        
        /// <summary>
        /// All html attributes outside of the class attribute go here. Use the Class attribute property to add css classes.
        /// </summary>
        [Parameter(CaptureUnmatchedValues = true)] public Dictionary<string, object> ExtraAttributes { get; set; }

        /// <summary>
        /// The subheader text.
        /// </summary>
        [Parameter] public RenderFragment ChildContent { get; set; }

        /// <summary>
        /// Css classes for affecting this child component go here.
        /// </summary>
        [Parameter] public string Class { get; set; }

        protected override void OnParametersSet()
        {
            if (Parent is null)
            {
                throw new ArgumentNullException(nameof(Parent), $"This component must have a parent of type {nameof(AppDrawerContent)}!");
            }
        }
    }
}