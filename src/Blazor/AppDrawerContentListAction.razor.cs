// Copyright (c) 2020 Allan Mobley. All rights reserved.
// Licensed under the MIT License. See License.txt in the project root for license information.

using Microsoft.AspNetCore.Components;

namespace Mobsites.Blazor
{
    /// <summary>
    /// Child component for adding an organized list to the <see cref="AppDrawerContentList"/> component.
    /// </summary>
    public partial class AppDrawerContentListAction
    {
        /// <summary>
        /// Content to render.
        /// </summary>
        [Parameter] public RenderFragment ChildContent { get; set; }

        /// <summary>
        /// A URL or a URL fragment that the hyperlink points to.
        /// </summary>
        [Parameter] public string Href { get; set; }

        /// <summary>
        /// Whether this action is activated.
        /// </summary>
        [Parameter] public bool IsActivated { get; set; }
    }
}