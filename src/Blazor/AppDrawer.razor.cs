// Copyright (c) 2020 Allan Mobley. All rights reserved.
// Licensed under the MIT License. See License.txt in the project root for license information.

using Microsoft.AspNetCore.Components;
using Microsoft.JSInterop;
using System.Runtime.InteropServices;
using System.Threading.Tasks;

namespace Mobsites.Blazor
{
    /// <summary>
    /// UI component for organizing access to destinations and other functionality in the app.
    /// </summary>
    public sealed partial class AppDrawer
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

        private string triggerMarker;

        /// <summary>
        /// A unique class marker on an element being used to toggle the <see cref="AppDrawer" />. 
        /// Defaults to built-in trigger class or the TopAppBar nav trigger class. 
        /// </summary>
        [Parameter]
        public string TriggerMarker
        {
            get => triggerMarker ?? ".mdc-drawer-trigger, .mdc-top-app-bar__navigation-icon";
            set
            {
                if (!string.IsNullOrWhiteSpace(value))
                {
                    triggerMarker = value.StartsWith(".")
                        ? value
                        : "." + value;
                }
            }
        }

        /// <summary>
        /// Whether to have a modal dismissable drawer across all device sizes. 
        /// Defaults to a responsive mode (false). 
        /// </summary>
        [Parameter] public bool ModalOnly { get; set; }

        /// <summary>
        /// Call back event for notifying another component that this property changed. 
        /// </summary>
        [Parameter] public EventCallback<bool> ModalOnlyChanged { get; set; }

        private int? responsiveBreakpoint;

        /// <summary>
        /// The css media breakpoint (in pixels) at which the drawer goes from modal to fixed in responsive mode.
        /// The default is 900px, which also the minmum allowed.
        /// </summary>
        [Parameter]
        public int? ResponsiveBreakpoint
        {
            get => responsiveBreakpoint ?? 900;
            set
            {
                if (value != null && value >= 0)
                {
                    responsiveBreakpoint = value;
                }
            }
        }

        /// <summary>
        /// Whether this is being used above or below our TopAppBar component. 
        /// Do not set otherwise. 
        /// </summary>
        [Parameter] public bool? AboveTopAppBar { get; set; }

        /// <summary>
        /// Call back event for notifying another component that this property changed. 
        /// </summary>
        [Parameter] public EventCallback<bool?> AboveTopAppBarChanged { get; set; }

        /// <summary>
        /// Close app drawer.
        /// </summary>
        public Task Toggle() => this.jsRuntime.InvokeVoidAsync(
            "Mobsites.Blazor.AppDrawers.toggle",
            Index)
            .AsTask();

        /// <summary>
        /// Close app drawer.
        /// </summary>
        public Task Open() => this.jsRuntime.InvokeVoidAsync(
            "Mobsites.Blazor.AppDrawers.open",
            Index)
            .AsTask();

        /// <summary>
        /// Close app drawer.
        /// </summary>
        public Task Close() => this.jsRuntime.InvokeVoidAsync(
            "Mobsites.Blazor.AppDrawers.close",
            Index)
            .AsTask();

        /// <summary>
        /// Call back event for notifying another component that this property changed. 
        /// </summary>
        [Parameter] public EventCallback<int?> ResponsiveBreakpointChanged { get; set; }

        /// <summary>
        /// Content to render.
        /// </summary>
        [JSInvokable]
        public void SetIndex(int index)
        {
            if (Index < 0)
            {
                Index = index;
            }
        }

        /// <summary>
        /// Clear all state for this UI component and any of its dependents from browser storage.
        /// </summary>
        public ValueTask ClearState() => this.ClearState<AppDrawer, Options>();



        /****************************************************
        *
        *  NON-PUBLIC INTERFACE
        *
        ****************************************************/

        /// <summary>
        /// Whether component environment is Blazor WASM or Server.
        /// </summary>
        internal bool IsWASM => RuntimeInformation.IsOSPlatform(OSPlatform.Create("WEBASSEMBLY"));

        private DotNetObjectReference<AppDrawer> self;

        /// <summary>
        /// Net reference passed into javascript representation.
        /// </summary>
        internal DotNetObjectReference<AppDrawer> Self
        {
            get => self ?? (Self = DotNetObjectReference.Create(this));
            set => self = value;
        }

        /// <summary>
        /// The index to this object's javascript representation in the object store.
        /// </summary>
        internal int Index { get; set; } = -1;

        /// <summary>
        /// Dom element reference passed into javascript representation.
        /// </summary>
        internal ElementReference ElemRef { get; set; }

        /// <summary>
        /// Child reference. (Assigned by child.)
        /// </summary>
        internal AppDrawerContent Content { get; set; }

        /// <summary>
        /// Life cycle method for when component has been rendered in the dom and javascript interopt is fully ready.
        /// </summary>
        protected async override Task OnAfterRenderAsync(bool firstRender)
        {
            if (firstRender)
            {
                await Initialize();
            }
            else
            {
                await Update();
            }
        }

        /// <summary>
        /// Initialize state and javascript representations.
        /// </summary>
        private async Task Initialize()
        {
            var options = await this.GetState<AppDrawer, Options>();

            if (options is null)
            {
                options = this.GetOptions();
            }
            else
            {
                await this.CheckState(options);
            }

            this.initialized = await this.jsRuntime.InvokeAsync<bool>(
                "Mobsites.Blazor.AppDrawers.init",
                Self,
                new
                {
                    Drawer = this.ElemRef,
                    Content = this.Content?.ElemRef
                },
                options);

            await this.Save<AppDrawer, Options>(options);
        }


        /// <summary>
        /// Update state.
        /// </summary>
        private async Task Update()
        {
            var options = await this.GetState<AppDrawer, Options>();

            // Use current state if...
            if (this.initialized || options is null)
            {
                options = this.GetOptions();
            }

            await this.jsRuntime.InvokeVoidAsync(
                $"Mobsites.Blazor.AppDrawers.update",
                Index,
                options);

            await this.Save<AppDrawer, Options>(options);
        }

        /// <summary>
        /// Get current or storage-saved options for keeping state.
        /// </summary>
        internal Options GetOptions()
        {
            var options = new Options
            {
                ModalOnly = this.ModalOnly,
                ResponsiveBreakpoint = this.ResponsiveBreakpoint,
                AboveTopAppBar = this.AboveTopAppBar,
                TriggerMarker = this.TriggerMarker
            };

            base.SetOptions(options);

            return options;
        }

        /// <summary>
        /// Check whether storage-retrieved options are different than current
        /// and thereby need to notify parents of change when keeping state.
        /// </summary>
        internal async Task CheckState(Options options)
        {
            bool stateChanged = false;

            if (this.ModalOnly != options.ModalOnly)
            {
                await this.ModalOnlyChanged.InvokeAsync(options.ModalOnly);
                stateChanged = true;
            }
            if (this.ResponsiveBreakpoint != options.ResponsiveBreakpoint)
            {
                await this.ResponsiveBreakpointChanged.InvokeAsync(options.ResponsiveBreakpoint);
                stateChanged = true;
            }
            if (this.AboveTopAppBar != options.AboveTopAppBar)
            {
                await this.AboveTopAppBarChanged.InvokeAsync(options.AboveTopAppBar);
                stateChanged = true;
            }

            bool baseStateChanged = await base.CheckState(options);

            if (stateChanged || baseStateChanged)
                StateHasChanged();
        }

        /// <summary>
        /// Called by GC.
        /// </summary>
        public override void Dispose()
        {
            jsRuntime.InvokeVoidAsync("Mobsites.Blazor.AppDrawers.destroy", Index);
            self?.Dispose();
            base.Dispose();
        }
    }
}