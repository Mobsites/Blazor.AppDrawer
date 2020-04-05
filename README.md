[![Build Status](https://dev.azure.com/allanmobleyjr/Blazor%20AppDrawer/_apis/build/status/Publish%20to%20Nuget?branchName=master)](https://dev.azure.com/allanmobleyjr/Blazor%20AppDrawer/_build/latest?definitionId=5&branchName=master)

# Blazor AppDrawer
A Blazor component that utilizes the [MDC Drawer](https://material.io/develop/web/components/drawers/) library to organize access to destinations and other functionality in a Blazor app.

<image src="src/assets/modal-only-mode.png" alt="Image of Modal Only Mode" width="165" height="" />&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<image src="src/assets/responsive-mode.png" alt="Image of Responsive Mode" width="650" height="" />

## For
* Blazor WebAssembly
* Blazor Server

## Dependencies

###### .NETStandard 2.0
* Microsoft.AspNetCore.Components (>= 3.1.2)
* Microsoft.AspNetCore.Components.Web (>= 3.1.2)

## Design and Development
The design and development of this component library was heavily guided by Steve Sanderson's [talk](https://youtu.be/QnBYmTpugz0) and [example](https://github.com/SteveSandersonMS/presentation-2020-01-NdcBlazorComponentLibraries), in which he outlines the best approach to building and deploying a reusable component library.

As for the non-C# implementation of this component library, obviously Google's MDC Navigation Drawer [docs](https://material.io/develop/web/components/drawers/) were consulted.

After much thought, the full implementation of Google's MDC Navigation Drawer was (for now) decided against in favor of a mobile-first approach. As a result, the dismissible and permanent variants were left out, and only the modal variant and a hybrid variant called responsive made it in. (The responsive being the modal and permanent variants combined, transition occurring on a responsive media breakpoint.)

Also, unlike some of the other MDC drawer components out there for Blazor, this one was designed without requiring the user to wire up a C# onclick event handler or callback function for toggling of the drawer in modal mode. I wanted the implementation to be as close to pure html tags and css as possible.

At the end of the day, a C# onclick event handler or callback function must interop with javascript to get the desired toggling effect and, thus, seemed like an unnecessary burden on the user when the same effect could be achieved via predefined css classes.

The component, therefore, works seamlessly with `Blazor TopAppBar` *without the user having to take any special steps*. In the case of using a custom app bar, as the samples demonstrate, simply applying a couple of css classes is all that is necessary.

## Getting Started
1. Install [Nuget](https://www.nuget.org/packages/Mobsites.Blazor.MaterialDesign.AppDrawer/):

```shell
dotnet add package Mobsites.Blazor.MaterialDesign.AppDrawer --version 1.0.0-preview2
```

2. Add the following link tag to `index.html` (WebAssembly) or `_Host.cshtml` (Server) just above the closing `</head>` tag, along with your other link tags:

```html
<link href="_content/Mobsites.Blazor.MaterialDesign.AppDrawer/bundle.css" rel="stylesheet" />
```

3. Add the following script tag to `index.html` (WebAssembly) or `_Host.cshtml` (Server) just above the closing `</body>` tag, along with your other script tags:

```html
<script src="_content/Mobsites.Blazor.MaterialDesign.AppDrawer/bundle.js"></script>
```

4. Add the following using statement to the `_Imports.razor` file:

```html
@using Mobsites.Blazor.MaterialDesign.Components
```

5. Add the following markup to the `MainLayout.razor` file:

```html
<!-- Add optional app bar here or below inside <AppContent> tag -->
<AppDrawer ModalOnly="true">
    <!-- Note the flag set to true above. For responsive mode, set to false or leave attribute off altogether -->
    <!-- Add optional header here. Keep outside of <AppDrawerContent> tag to avoid scrolling -->

    <!-- Required -->
    <AppDrawerContent>
        <!-- Add navigation content or app functionality here -->
        <!-- Scrollable content (separate from scrollable content in <MainContent> tag below)  -->
    </AppDrawerContent>
</AppDrawer>
<!-- Required for responsive mode, recommended for modal only mode -->
<AppContent>

    <!-- Add optional app bar here (be sure to set <AppContent ContainsAppBar="true"> above) -->

    <MainContent>
        <!-- typically @Body -->
        <!-- Scrollable content (separate from scrollable content in <AppDrawerContent> tag above)  -->
    </MainContent>
</AppContent>
```

6. Add the `Blazor TopAppBar` component or a custom app bar (shown below). Either can be placed above the `<AppDrawer>` tag or inside the `<AppContent>` tag. While adding an app bar is optional, being able to toggle the drawer when in a modal state is a must, so employ a button with the class `.app-drawer-button` at the ver least if going barless:

```html
<!-- Note the use of the class .app-bar here -->
<!-- This is provided out of the box, but not necessary -->
<!-- The css for this is shown below -->
<div class="app-bar">
    <!-- The class .app-drawer-button below is wired up to toggle the drawer on button click -->
    <button class="app-drawer-button mr-auto" >
        <span class="oi oi-menu"></span>
    </button>
</div>
```

7. Add header (optional):

```html
<!-- The image src defaults to blazor icon -->
<AppDrawerHeader Title="Blazor AppDrawer" 
                 Subtitle="Modal Only Mode" 
                 ImageSource="" 
                 ImageWidth="192" 
                 ImageHeight="192" />
```

or

```html
<AppDrawerHeader>
    <!-- The image src defaults to blazor icon -->
    <AppDrawerHeaderImage ImageSource="" ImageWidth="192" ImageHeight="192" />
    <AppDrawerHeaderTitle>Blazor AppDrawer</AppDrawerHeaderTitle>
    <AppDrawerHeaderSubtitle>Modal Only Mode</AppDrawerHeaderSubtitle>
</AppDrawerHeader>
```

## Responsive Mode Breakpoint
The `<AppDrawer>` tag has an attribute of the name `ResponsiveBreakpoint` that accepts an integer value. The default and absolute minimum is 900 (in pixels). Anything above that will change the responsive mode media breakpoint at which the component transitions between a modal state and a fixed state. Anything below that will be ignored.

## Possible CSS Styling Conflicts

This component defaults to the below css style rules, which may conflict with what you already have in place. Some of these styles are necessary for the responsive mode to work properly. Modal drawers are elevated above most of the app’s UI and generally don’t affect the screen’s layout grid, so you should be able to get by if changing any of these defaults when using the modal only mode.

```css
@use "@material/drawer/mdc-drawer";
@use "@material/list/mdc-list";

body {
    display: flex;
    height: 100vh;
}

app {
    position: relative;
    display: flex;
    flex-direction: column;
    width: 100%;
}
  
.mdc-drawer-app-content {
    flex: auto;
    overflow: auto;
    position: relative;
}

.main-content {
    overflow: auto;
    height: 100%;
    padding-left: 2rem !important;
    padding-right: 1.5rem !important;
    padding-top: 4.5rem;
}

.app-bar {
    position: absolute;
    top: 0;
    width: 100%;
    height: 64px;
    display: flex;
    align-items: center;
    background-color: #f7f7f7;
    border-bottom: 1px solid #d6d5d5;
    justify-content: flex-end;
    padding-left: 1.5rem!important;
    padding-right: 1.5rem!important;
    z-index: 4;
}

.hide-drawer-button {
    display: none;
}

.mdc-drawer {
    background-image: linear-gradient(180deg, rgb(5, 39, 103) 0%, #3a0647 70%);
}

.mdc-drawer__content .oi {
    width: 2rem;
    font-size: 1.1rem;
    vertical-align: text-top;
    top: -2px;
}

.mdc-drawer__content .nav-item {
    font-size: 0.9rem;
    padding-bottom: 0.5rem;
}

.mdc-drawer__content .nav-item:first-of-type {
    padding-top: 1rem;
}

.mdc-drawer__content .nav-item:last-of-type {
    padding-bottom: 1rem;
}

.mdc-drawer__content .nav-item a {
    color: #d7d7d7;
    border-radius: 4px;
    height: 3rem;
    display: flex;
    align-items: center;
    line-height: 3rem;
}

.mdc-drawer__content .nav-item a.active {
    background-color: rgba(255,255,255,0.25);
    color: white;
}

.mdc-drawer__content .nav-item a:hover {
    background-color: rgba(255,255,255,0.1);
    color: white;
}

.mdc-drawer__header {
    padding-top: 1rem;
    padding-bottom: 1rem;
    background-color: rgba(0,0,0,0.4);
    color: white;
}

.mdc-drawer .mdc-drawer__title , .mdc-drawer .mdc-drawer__subtitle {
    Color: white;
}

.mdc-drawer__image-container {
    text-align: center;
}

// only apply this style if below top app bar
.drawer-below-app-bar {
    z-index: 7;
}

@media (min-width: 768px) {
    app {
        flex-direction: row;
    }
}
```
