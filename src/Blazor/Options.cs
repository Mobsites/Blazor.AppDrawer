// Copyright (c) 2020 Allan Mobley. All rights reserved.
// Licensed under the MIT License. See License.txt in the project root for license information.

namespace Mobsites.Blazor
{
    public partial class AppDrawer
    {
        internal class Options : StatefulComponentOptions
        {
            /************************************************************************
            *
            *   Non-null enum and int members with a zero value do not need to be
            *   serialized as they would default to zero on C# object initialization.
            *   
            *   (For nullable types...well null is null.)
            *
            *   Setting their options equivalent to null will keep them from
            *   being serialized.
            *
            *   This saves space in browser storage.
            *
            *   Caveat: If the options are passed into a javascript function,
            *   then, obviously, any such members depended on there will have to 
            *   be accounted for there as not defined or null and, thus,
            *   equivalent to zero.
            *
            ***********************************************************************/

            /// <summary>
            /// Option for whether to have a modal dismissable drawer across all device sizes. 
            /// </summary>
            public bool ModalOnly { get; set; }

            /// <summary>
            /// Option for the css media breakpoint (in pixels) at which the drawer goes from modal to fixed in responsive mode.
            /// </summary>
            public int? ResponsiveBreakpoint { get; set; }

            /// <summary>
            /// Option for whether this is being used above or below our TopAppBar component.
            /// </summary>
            public bool? AboveTopAppBar { get; set; }

            /// <summary>
            /// Option for a unique class marker on an element being used to toggle the <see cref="AppDrawer" />. 
            /// </summary>
            public string TriggerMarker { get; set; }
        }
    }
}