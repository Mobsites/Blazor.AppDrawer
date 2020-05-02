// Copyright (c) 2020 Allan Mobley. All rights reserved.
// Licensed under the MIT License. See License.txt in the project root for license information.

import { MDCDrawer } from "@material/drawer/index";
import { MDCList } from "@material/list/index";

if (!window.Mobsites) {
    window.Mobsites = {
        Blazor: {

        }
    };
}

window.Mobsites.Blazor.AppDrawer = {
    init: function (instance, elemRefs, options) {
        this.instance = instance;
        this.elemRefs = elemRefs;
        this.options = options;
        if (this.options.modalOnly) {
            this.initModalDrawer();
        }
        else if (window.matchMedia('(max-width: ' + this.options.responsiveBreakpoint + 'px)').matches) {
            this.initModalDrawer();
            this.initResizeEvent();
        }
        else {
            this.initPermanentDrawer();
            this.initResizeEvent();
        }
        this.determineDrawerButtonVisibility();
        return true;
    },
    refresh: function (instance, elemRefs, options) {
        if (options.modalOnly != this.options.modalOnly) {
            options.destroy = true;
        }
        return this.init(instance, elemRefs, options);
    },
    initModalDrawer: function () {
        this.elemRefs.drawer.classList.add('mdc-drawer--modal');
        if (!this.initialized || this.options.destroy) {
            if (this.self) {
                this.self.destroy();
            }
            // Modal uses the MDCDrawer component, which will instantiate MDCList automatically.
            const drawer = MDCDrawer.attachTo(this.elemRefs.drawer);
            drawer.open = false;
            this.initialized = true;
            this.self = drawer;
        }
        this.initModalEvents();
    },
    initPermanentDrawer: function () {
        this.elemRefs.drawer.classList.remove('mdc-drawer--modal');
        if (!this.initialized || this.options.destroy) {
            if (this.self) {
                this.self.destroy();
            }
            // For permanently visible drawers, the list must be instantiated for appropriate keyboard interaction.
            const list = new MDCList(this.elemRefs.content);
            list.wrapFocus = true;
            this.initialized = true;
            this.self = list;
        }
    },
    initResizeEvent: function () {
        window.addEventListener('resize', this.invokeNetRefresh);
    },
    initModalEvents: function () {
        // Event for closing modal drawer when navigation or action occurs.
        this.elemRefs.content.addEventListener('click', this.closeDrawerClickEvent);
        // Event for toggling modal drawer when .mobsites-blazor-app-drawer-button id or class is specified.
        // As for Blazor Top App Bar, that component sets this event for the .mdc-top-app-bar__navigation-icon.
        const drawerButton = document.getElementById('mobsites-blazor-app-drawer-button') || document.querySelector('.mobsites-blazor-app-drawer-button');
        if (drawerButton) {
            drawerButton.addEventListener('click', this.toggleDrawerClickEvent);
        }
    },
    toggleDrawerClickEvent: function () {
        window.Mobsites.Blazor.AppDrawer.self.open = !window.Mobsites.Blazor.AppDrawer.self.open;
    },
    openDrawerClickEvent: function () {
        window.Mobsites.Blazor.AppDrawer.self.open = true;
    },
    closeDrawerClickEvent: function () {
        window.Mobsites.Blazor.AppDrawer.self.open = false;
    },
    determineDrawerButtonVisibility: function () {
        const drawerButton = document.getElementById('mobsites-blazor-app-drawer-button') || document.querySelector('.mdc-top-app-bar__navigation-icon, .mobsites-blazor-app-drawer-button');
        if (drawerButton && this.options) {
            if (this.options.modalOnly || window.matchMedia('(max-width: ' + this.options.responsiveBreakpoint + 'px)').matches) {
                drawerButton.classList.remove('hide-mobsites-blazor-app-drawer-button');
            }
            else {
                drawerButton.classList.add('hide-mobsites-blazor-app-drawer-button');
            }
        }
    },
    invokeNetRefresh: function () {
        // Prevent window.resize event from double firing.
        clearTimeout(window.Mobsites.Blazor.AppDrawer.timeoutId);
        // Delay the resize handling by 200ms
        window.Mobsites.Blazor.AppDrawer.timeoutId = setTimeout(() => window.Mobsites.Blazor.AppDrawer.instance.invokeMethodAsync('Refresh', true), 200);
    }
}