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
        /// Styles for affecting this component go here.
        /// </summary>
        [Parameter] public string Style { get; set; }

        /// <summary>
        /// Set this to true to have a modal dismissable drawer across all device sizes. 
        /// Defaults to a responsive mode (false). 
        /// </summary>
        [Parameter] public bool ModalOnly { get; set; }

        private int? responsiveBreakpoint = 900;

        /// <summary>
        /// The css media breakpoint (in pixels) at which the drawer goes from modal to fixed in responsive mode.
        /// The default is 900px, which also the minmum allowed.
        /// </summary>
        [Parameter] public int? ResponsiveBreakpoint
        { 
            get => responsiveBreakpoint; 
            set 
            { 
                if (value != null && value > 900)
                {
                    responsiveBreakpoint = value;
                } 
            } 
        }
        
        /// <summary>
        /// Whether to not use a background color image. 
        /// </summary>
        [Parameter] public bool NoBackgroundColorImage { get; set; }

        /// <summary>
        /// The direction of color flow. Defaults to BackgroundImageColorDirections.TopToBottom.
        /// </summary>
        [Parameter] public BackgroundImageColorDirections BackgroundImageColorDirection { get; set; } = BackgroundImageColorDirections.TopToBottom;

        private string backgroundImageStartColor = "#052767";

        /// <summary>
        /// Any valid css color usage (rgb, hex, or color name). Defaults to #052767.
        /// </summary>
        [Parameter] public string BackgroundImageStartColor
        { 
            get => backgroundImageStartColor; 
            set 
            { 
                if (!string.IsNullOrEmpty(value))
                {
                    backgroundImageStartColor = value;
                } 
            } 
        }
        
        private string backgroundImageEndColor = "#3a0647";

        /// <summary>
        /// Any valid css color usage (rgb, hex, or color name). Defaults to #3a0647.
        /// </summary>
        [Parameter] public string BackgroundImageEndColor
        { 
            get => backgroundImageEndColor; 
            set 
            { 
                if (!string.IsNullOrEmpty(value))
                {
                    backgroundImageEndColor = value;
                } 
            } 
        }

        public enum BackgroundImageColorDirections
        {
            BottomToTop = 0,
            LeftToRight = 90,
            TopToBottom = 180,
            RightToLeft = 270
            
        }

        protected async override Task OnAfterRenderAsync(bool firstRender)
        {
            var options = new {
                ModalOnly,
                ResponsiveBreakpoint,
                Destroy = true
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
                await Refresh(destroy: false);
            }
        }

        /// <summary>
        /// Refreshes the drawer when in responsive mode.
        /// </summary>
        [JSInvokable]
        public async Task Refresh(bool destroy)
        {
            var options = new {
                ModalOnly,
                ResponsiveBreakpoint,
                Destroy = destroy
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