// Copyright (c) 2020 Allan Mobley. All rights reserved.
// Licensed under the MIT License. See License.txt in the project root for license information.

using System.Threading.Tasks;
using Microsoft.AspNetCore.Components;

namespace Mobsites.Blazor
{
    /// <summary>
    /// Blazor child component for adding header content to the <see cref="AppDrawer"/> component.
    /// This will not scroll with the rest of the drawer content if placed as direct decendent of the <see cref="AppDrawer"/> component. 
    /// Things like account switchers and titles should live here.
    /// </summary>
    public partial class AppDrawerHeader
    {
        /// <summary>
        /// Content to render.
        /// </summary>
        [Parameter] public RenderFragment ChildContent { get; set; }
        
        /// <summary>
        /// Title content. Will be ignored if this component also has child content.
        /// </summary>
        [Parameter] public string Title { get; set; }

        /// <summary>
        /// Call back event for notifying another component that this property changed. 
        /// </summary>
        [Parameter] public EventCallback<string> TitleChanged { get; set; }

        /// <summary>
        /// Subtitle content. Will be ignored if this component also has child content.
        /// </summary>
        [Parameter] public string Subtitle { get; set; }

        /// <summary>
        /// Call back event for notifying another component that this property changed. 
        /// </summary>
        [Parameter] public EventCallback<string> SubtitleChanged { get; set; }

        /// <summary>
        /// Whether to use image.
        /// </summary>
        [Parameter] public bool UseImage { get; set; }

        /// <summary>
        /// Call back event for notifying another component that this property changed. 
        /// </summary>
        [Parameter] public EventCallback<bool> UseImageChanged { get; set; }

        private string image;
        
        /// <summary>
        /// Image source.
        /// </summary>
        [Parameter] public string Image 
        { 
            get => image; 
            set 
            { 
                if (!string.IsNullOrEmpty(value))
                {
                    image = value;
                } 
            } 
        }

        private int imageWidth = 192;
        
        /// <summary>
        /// Image width. Defaults to 192px.
        /// </summary>
        [Parameter] public int ImageWidth 
        { 
            get => imageWidth; 
            set 
            { 
                if (value > 0)
                {
                    imageWidth = value;
                } 
            } 
        }

        private int imageHeight = 192;
        
        /// <summary>
        /// Image height. Defaults to 192px.
        /// </summary>
        [Parameter] public int ImageHeight 
        { 
            get => imageHeight; 
            set 
            { 
                if (value > 0)
                {
                    imageHeight = value;
                } 
            } 
        }

        internal string Color => Parent.Color;

        protected override void OnParametersSet()
        {
            // This will check for valid parent.
            base.OnParametersSet();
            base.Parent.AppDrawerHeader = this;
        }

        internal void SetOptions(AppDrawer.Options options)
        {
            options.Title = this.Title;
            options.Subtitle = this.Subtitle;
            options.UseImage = this.UseImage;
        }

        internal async Task CheckState(AppDrawer.Options options)
        {
            if (this.Title != options.Title)
            {
                await this.TitleChanged.InvokeAsync(options.Title);
            }

            if (this.Subtitle != options.Subtitle)
            {
                await this.SubtitleChanged.InvokeAsync(options.Subtitle);
            }

            if (this.UseImage != options.UseImage)
            {
                await this.UseImageChanged.InvokeAsync(options.UseImage);
            }
        }
    }
}