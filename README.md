![Nuget](https://img.shields.io/nuget/v/Mobsites.Blazor.AppDrawer) ![Nuget](https://img.shields.io/nuget/dt/Mobsites.Blazor.AppDrawer) [![Build Status](https://dev.azure.com/Mobsites-US/Blazor%20App%20Drawer/_apis/build/status/Build?branchName=master)](https://dev.azure.com/Mobsites-US/Blazor%20App%20Drawer/_build/latest?definitionId=26&branchName=master)

# Blazor App Drawer

by <a href="https://www.mobsites.com"><img align="center" src="./src/assets/mobsites-logo.png" width="36" height="36" style="padding-top: 20px;" />obsites</a>

A Blazor UI component that utilizes the [MDC Drawer](https://material.io/develop/web/components/drawers/) library to organize access to destinations and other functionality in a Blazor app.

## [Demo](https://appdrawer.mobsites.com)

Tap the link above to go to a live demo. Try some of the options to get an idea of what's possible. Then reload the app in the browser and watch how the state was kept!

Check out its source code [here](./samples).

![Gif of Demo](src/assets/demo.gif)

## For

* Blazor WebAssembly
* Blazor Server

## Dependencies

###### .NETStandard 2.0

* Mobsites.Blazor.BaseComponents (>= 1.0.3)

## Design and Development

The design and development of this Blazor component was heavily guided by Microsoft's [Steve Sanderson](https://blog.stevensanderson.com/). He outlines a superb approach to building and deploying a reusable component library in this [presentation](https://youtu.be/QnBYmTpugz0) and [example](https://github.com/SteveSandersonMS/presentation-2020-01-NdcBlazorComponentLibraries).

As for the non-C# implementation of this library, obviously Google's MDC Navigation Drawer [docs](https://material.io/develop/web/components/drawers/) were consulted.

After much thought, the full implementation of Google's MDC Navigation Drawer was (for now) decided against in favor of a mobile-first approach. As a result, the dismissible and permanent variants were left out, and only the modal variant and a hybrid variant called responsive made it in. The responsive being the modal and permanent variants combined, transition occurring on a responsive media breakpoint.

## Getting Started

Check out our new [docs](https://www.mobsites.com/blazor/app-drawer) to help you get started.
