// Copyright (c) 2020 Allan Mobley. All rights reserved.
// Licensed under the MIT License. See License.txt in the project root for license information.

using Microsoft.AspNetCore.Components;
using Microsoft.JSInterop;
using System;
using System.Threading.Tasks;

namespace Mobsites.Blazor
{
    /// <summary>
    /// Blazor component that utilizes the MDC Drawer library to organize access to destinations and other functionality in a Blazor app.
    /// </summary>
    public partial class AppDrawer : IDisposable
    {
        private DotNetObjectReference<AppDrawer> self;

        /// <summary>
        /// Whether the component has been completely initialized, including its JavaScript representation.
        /// </summary>
        private bool initialized;

        /// <summary>
        /// Content to render.
        /// </summary>
        [Parameter] public RenderFragment ChildContent { get; set; }


        /// <summary>
        /// Child reference. (Assigned by child.)
        /// </summary>
        internal AppDrawerHeader AppDrawerHeader { get; set; }

        /// <summary>
        /// Child reference. (Assigned by child.)
        /// </summary>
        internal AppDrawerContent AppDrawerContent { get; set; }

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
        /// Set this to true to have a modal dismissable drawer across all device sizes. 
        /// Defaults to a responsive mode (false). 
        /// </summary>
        [Parameter] public bool ModalOnly { get; set; }

        /// <summary>
        /// Call back event for notifying another component that this property changed. 
        /// </summary>
        [Parameter] public EventCallback<bool> ModalOnlyChanged { get; set; }

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
                if (value is null)
                {
                    responsiveBreakpoint = 900;
                }
                else if (value >= 0)
                {
                    responsiveBreakpoint = value;
                } 
            } 
        }

        /// <summary>
        /// Call back event for notifying another component that this property changed. 
        /// </summary>
        [Parameter] public EventCallback<int?> ResponsiveBreakpointChanged { get; set; }

        protected async override Task OnAfterRenderAsync(bool firstRender)
        {
            if (self is null)
            {
                 self = DotNetObjectReference.Create(this);
            }

            if (firstRender)
            {
                await Initialize();
            }
            else
            {
                await Refresh();
            }
        }

        private async Task Initialize()
        {
            var options = this.KeepState 
                ? this.UseSessionStorageForState
                    ? await this.Storage.Session.GetAsync<Options>(nameof(AppDrawer))
                    : await this.Storage.Local.GetAsync<Options>(nameof(AppDrawer))
                : null;

            if (options is null)
            {
                options = this.SetOptions();
            }
            else
            {
                await this.CheckState(options);
            }

            // Destroy any lingering js representation.
            options.Destroy = true;
            
            this.initialized = await this.jsRuntime.InvokeAsync<bool>(
                "Mobsites.Blazor.AppDrawer.init",
                self,
                options);

            await Save(options);
        }


        /// <summary>
        /// Refreshes the drawer when in responsive mode.
        /// </summary>
        [JSInvokable]
        public async Task Refresh(bool destroy = false)
        {
            var options = this.KeepState 
                ? this.UseSessionStorageForState
                    ? await this.Storage.Session.GetAsync<Options>(nameof(AppDrawer))
                    : await this.Storage.Local.GetAsync<Options>(nameof(AppDrawer))
                : null;

            // Use current state if...
            if (this.initialized || options is null)
            {
                options = this.SetOptions();
            }

            options.Destroy = destroy;

            this.initialized = await this.jsRuntime.InvokeAsync<bool>(
                "Mobsites.Blazor.AppDrawer.refresh",
                self,
                options);
            
            await Save(options);
        }

        internal Options SetOptions()
        {
            var options = new Options 
            {
                ModalOnly = this.ModalOnly,
                ResponsiveBreakpoint = this.ResponsiveBreakpoint
            };

            base.SetOptions(options);
            this.AppDrawerHeader?.SetOptions(options);
            // this.AppDrawerContent?.SetOptions(options);

            return options;
        }

        internal async Task CheckState(Options options)
        {
            if (this.ModalOnly != options.ModalOnly)
            {
                await this.ModalOnlyChanged.InvokeAsync(options.ModalOnly);
            }
            if (this.ResponsiveBreakpoint != options.ResponsiveBreakpoint)
            {
                await this.ResponsiveBreakpointChanged.InvokeAsync(options.ResponsiveBreakpoint);
            }

            await base.CheckState(options);
            await this.AppDrawerHeader?.CheckState(options);
            // await this.AppDrawerContent?.CheckState(options);
        }

        private async Task Save(Options options)
        {
            // Clear destory before saving.
            options.Destroy = false;

            if (this.KeepState)
            {
                if (this.UseSessionStorageForState)
                {
                    await this.Storage.Session.SetAsync(nameof(AppDrawer), options);
                }
                else
                {
                    await this.Storage.Local.SetAsync(nameof(AppDrawer), options);
                }
            }
            else
            {
                await this.Storage.Session.RemoveAsync<Options>(nameof(AppDrawer));
                await this.Storage.Local.RemoveAsync<Options>(nameof(AppDrawer));
            }
        }

        public void Dispose()
        {
            self?.Dispose();
            this.initialized = false;
        }
    }
}