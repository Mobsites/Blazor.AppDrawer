// Copyright (c) 2020 Allan Mobley. All rights reserved.
// Licensed under the MIT License. See License.txt in the project root for license information.

using Microsoft.AspNetCore.Components;
using System;
using System.Collections.Generic;


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
        /// Parent container.
        /// </summary>
        [CascadingParameter] internal AppDrawer Parent { get; set; }

        /// <summary>
        /// All html attributes outside of the class attribute go here. Use the Class attribute property to add css classes.
        /// </summary>
        [Parameter(CaptureUnmatchedValues = true)] public Dictionary<string, object> ExtraAttributes { get; set; }

        /// <summary>
        /// The header content.
        /// </summary>
        [Parameter] public RenderFragment ChildContent { get; set; }

        /// <summary>
        /// Css classes for affecting this child component go here.
        /// </summary>
        [Parameter] public string Class { get; set; }

        /// <summary>
        /// Title content. Will be ignored if this component also has child content.
        /// </summary>
        [Parameter] public string Title { get; set; }

        /// <summary>
        /// Subtitle content. Will be ignored if this component also has child content.
        /// </summary>
        [Parameter] public string Subtitle { get; set; }

        /// <summary>
        /// Whether to show a brand image.
        /// </summary>
        [Parameter] public bool UseImage { get; set; }

        private string imageSource = "_content/Mobsites.Blazor.AppDrawer/blazor.png";
        
        /// <summary>
        /// Image source override. Defaults to '_content/Mobsites.Blazor.AppDrawer/blazor.png'.
        /// </summary>
        [Parameter] public string ImageSource 
        { 
            get => imageSource; 
            set 
            { 
                if (!string.IsNullOrEmpty(value))
                {
                    imageSource = value;
                } 
            } 
        }

        private int imageWidth = 192;
        
        /// <summary>
        /// Image width (px) override. Defaults to 192px.
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
        /// Image height (px) override. Defaults to 192px.
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

        protected override void OnParametersSet()
        {
            if (Parent is null)
            {
                throw new ArgumentNullException(nameof(Parent), $"This component must have a parent of type {nameof(AppDrawer)}!");
            }
        }
    }
}