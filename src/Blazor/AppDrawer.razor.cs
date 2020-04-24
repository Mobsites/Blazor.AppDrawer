// Copyright (c) 2020 Allan Mobley. All rights reserved.
// Licensed under the MIT License. See License.txt in the project root for license information.

using Microsoft.AspNetCore.Components;
using Microsoft.JSInterop;
using System;
using System.Threading.Tasks;

namespace Mobsites.Blazor
{
    /// <summary>
    /// UI component for organizing access to destinations and other functionality in the app.
    /// </summary>
    public partial class AppDrawer : IDisposable
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

        /// <summary>
        /// Clear all state for this UI component and any of its dependents from browser storage.
        /// </summary>
        public ValueTask ClearState() => this.ClearState<AppDrawer, Options>();



        /****************************************************
        *
        *  NON-PUBLIC INTERFACE
        *
        ****************************************************/
        
        private DotNetObjectReference<AppDrawer> self;
        protected DotNetObjectReference<AppDrawer> Self
        {
            get => self ?? (Self = DotNetObjectReference.Create(this));
            set => self = value;
        }

        /// <summary>
        /// Child reference. (Assigned by child.)
        /// </summary>
        internal AppDrawerHeader AppDrawerHeader { get; set; }

        /// <summary>
        /// Child reference. (Assigned by child.)
        /// </summary>
        internal AppDrawerContent AppDrawerContent { get; set; }

        protected async override Task OnAfterRenderAsync(bool firstRender)
        {
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
            var options = await this.GetState<AppDrawer, Options>();

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
                Self,
                options);

            await this.Save<AppDrawer, Options>(options);
        }


        /// <summary>
        /// Refreshes the drawer when in responsive mode.
        /// </summary>
        [JSInvokable]
        public async Task Refresh(bool destroy = false)
        {
            var options = await this.GetState<AppDrawer, Options>();

            // Use current state if...
            if (this.initialized || options is null)
            {
                options = this.SetOptions();
            }

            options.Destroy = destroy;

            this.initialized = await this.jsRuntime.InvokeAsync<bool>(
                "Mobsites.Blazor.AppDrawer.refresh",
                Self,
                options);

            await this.Save<AppDrawer, Options>(options);
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

        public override void Dispose()
        {
            self?.Dispose();
            this.initialized = false;
        }
    }
}