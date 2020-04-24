// Copyright (c) 2020 Allan Mobley. All rights reserved.
// Licensed under the MIT License. See License.txt in the project root for license information.

namespace Mobsites.Blazor
{
    public partial class AppDrawer
    {
        /// <summary>
        /// Use this css class marker on a div wrapper around main content when using the <see cref="AppDrawer"/> component.
        /// Or use the <see cref="AppContent"/> component to contain the main content.
        /// </summary>
        public const string AppContentMarker = "mdc-drawer-app-content";

        /// <summary>
        /// Use this as the id or as a class marker for the main content in your Blazor app.
        /// </summary>
        public const string MainContentMarker = "blazor-main-content";

        /// <summary>
        /// Use this as the id or as a class marker for the app drawer button in your Blazor app.
        /// Note: This is not necessary when using Blazor Top App Bar.
        /// </summary>
        public const string AppDrawerButtonMarker = "mobsites-blazor-app-drawer-button";
    }
}