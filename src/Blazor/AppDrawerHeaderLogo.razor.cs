// Copyright (c) 2020 Allan Mobley. All rights reserved.
// Licensed under the MIT License. See License.txt in the project root for license information.

using Microsoft.AspNetCore.Components;

namespace Mobsites.Blazor
{
    /// <summary>
    /// Child component for adding a logo to the <see cref="AppDrawerHeader"/> component.
    /// </summary>
    public sealed partial class AppDrawerHeaderLogo
    {
        private string src;

        /// <summary>
        /// Image source.
        /// </summary>
        [Parameter]
        public string Src
        {
            get => src;
            set
            {
                if (!string.IsNullOrEmpty(value))
                {
                    src = value;
                }
            }
        }

        private int imageWidth = 192;

        /// <summary>
        /// Image width. Defaults to 192px.
        /// </summary>
        [Parameter]
        public int ImageWidth
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
        [Parameter]
        public int ImageHeight
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

        /// <summary>
        /// The background color for this component. Accepts any valid css color usage.
        /// </summary>
        public override string BackgroundColor
        {
            get => "transparent";
            set => base.Color = value;
        }
    }
}