// Copyright (c) 2020 Allan Mobley. All rights reserved.
// Licensed under the MIT License. See License.txt in the project root for license information.

using Microsoft.AspNetCore.Components;
using Microsoft.JSInterop;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Mobsites.Blazor
{
    /// <summary>
    /// Blazor component that utilizes the MDC Drawer library to organize access to destinations and other functionality in a Blazor app.
    /// </summary>
    public partial class AppDrawer : IDisposable
    {
        private DotNetObjectReference<AppDrawer> self;
        [Inject] protected IJSRuntime jsRuntime { get; set; }

        /// <summary>
        /// Use this css class marker on a div wrapper around main content when using the <see cref="AppDrawer"/> component.
        /// Or use the <see cref="AppContent"/> component to contain the main content.
        /// </summary>
        public static string AppContentMarker => "mdc-drawer-app-content";

        /// <summary>
        /// Use this as the id or as a class marker for the main content in your Blazor app.
        /// </summary>
        public static string MainContentMarker => "blazor-main-content";

        /// <summary>
        /// Use this as the id or as a class marker for the app drawer button in your Blazor app.
        /// Note: This is not necessary when using Blazor Top App Bar.
        /// </summary>
        public static string AppDrawerButtonMarker => "mobsites-blazor-app-drawer-button";

        /// <summary>
        /// All html attributes outside of the class attribute go here. Use the Class attribute property to add css classes.
        /// </summary>
        [Parameter(CaptureUnmatchedValues = true)] public Dictionary<string, object> ExtraAttributes { get; set; } 

        /// <summary>
        /// The <see cref="AppDrawerHeader"/> (optional) and the <see cref="AppDrawerContent"/> (required).
        /// </summary>
        [Parameter] public RenderFragment ChildContent { get; set; }

        /// <summary>
        /// Css classes for affecting this component go here.
        /// </summary>
        [Parameter] public string Class { get; set; }

        /// <summary>
        /// Set this to true to have a modal dismissable drawer across all device sizes. 
        /// Defaults to a responsive mode (false). 
        /// </summary>
        [Parameter] public bool ModalOnly { get; set; }

        /// <summary>
        /// The css media breakpoint (in pixels) at which the drawer goes from modal to fixed in responsive mode.
        /// The default is 900px, which also the minmum allowed.
        /// </summary>
        [Parameter] public int ResponsiveBreakpoint { get; set; }

        // Minimum breakpoint allowed.
        private const int responsiveBreakpoint = 900;

        protected async override Task OnAfterRenderAsync(bool firstRender)
        {
            var options = new {
                ModalOnly,
                ResponsiveBreakpoint = ResponsiveBreakpoint > 900
                    ? ResponsiveBreakpoint
                    : responsiveBreakpoint
            };

            if (self is null)
            {
                 self = DotNetObjectReference.Create(this);
            }

            if (firstRender)
            {
                await jsRuntime.InvokeVoidAsync(
                    "Mobsites.Blazor.AppDrawer.init",
                    self,
                    options);
            }
            else
            {
                await Refresh(teardown: false);
            }
        }

        /// <summary>
        /// Refreshes the drawer when in responsive mode.
        /// </summary>
        [JSInvokable]
        public async Task Refresh(bool teardown)
        {
            var options = new {
                ModalOnly,
                ResponsiveBreakpoint = ResponsiveBreakpoint > 900
                    ? ResponsiveBreakpoint
                    : responsiveBreakpoint,
                Teardown = teardown
            };

            await jsRuntime.InvokeVoidAsync(
                "Mobsites.Blazor.AppDrawer.refresh",
                self,
                options);
        }

        public void Dispose()
        {
            self?.Dispose();
        }
    }
}