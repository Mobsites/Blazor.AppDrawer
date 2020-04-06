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
    init: function (instance, options) {
        window.Mobsites.Blazor.AppDrawer.instance = instance;
        window.Mobsites.Blazor.AppDrawer.options = options;
        if (options.modalOnly) {
            this.initModalDrawer();
        }
        else if (window.matchMedia('(max-width: ' + options.responsiveBreakpoint + 'px)').matches) {
            this.initModalDrawer();
            this.initResizeEvent();
        }
        else {
            this.initPermanentDrawer();
            this.initResizeEvent();
        }
    },
    refresh: function (instance, options) {
        this.init(instance, options);
    },
    initModalDrawer: function () {
        const drawerButton = document.getElementById('mobsites-blazor-app-drawer-button') || document.querySelector('.mdc-top-app-bar__navigation-icon, .mobsites-blazor-app-drawer-button');
        if (drawerButton) {
            drawerButton.classList.remove('hide-mobsites-blazor-app-drawer-button');
        }
        const drawerElement = document.querySelector('.mdc-drawer');
        if (drawerElement) {
            drawerElement.classList.add('mdc-drawer--modal');
        }
        if (drawerElement && (!window.Mobsites.Blazor.AppDrawer.initialized || window.Mobsites.Blazor.AppDrawer.options.destroy)) {
            if (window.Mobsites.Blazor.AppDrawer.self) {
                window.Mobsites.Blazor.AppDrawer.self.destroy();
            }
            // Modal uses the MDCDrawer component, which will instantiate MDCList automatically.
            const drawer = MDCDrawer.attachTo(drawerElement);
            drawer.open = false;
            window.Mobsites.Blazor.AppDrawer.initialized = true;
            window.Mobsites.Blazor.AppDrawer.self = drawer;
        }
        this.initModalEvents();
    },
    initPermanentDrawer: function () {
        const drawerButton = document.getElementById('mobsites-blazor-app-drawer-button') || document.querySelector('.mdc-top-app-bar__navigation-icon, .mobsites-blazor-app-drawer-button');
        if (drawerButton) {
            drawerButton.classList.add('hide-mobsites-blazor-app-drawer-button');
        }
        const drawerElement = document.querySelector('.mdc-drawer');
        if (drawerElement) {
            drawerElement.classList.remove('mdc-drawer--modal');
        }
        const listElement = document.querySelector('.mdc-drawer .mdc-list, .mdc-drawer .mdc-drawer__content');
        if (listElement && (!window.Mobsites.Blazor.AppDrawer.initialized || window.Mobsites.Blazor.AppDrawer.options.destroy)) {
            if (window.Mobsites.Blazor.AppDrawer.self) {
                window.Mobsites.Blazor.AppDrawer.self.destroy();
            }
            // For permanently visible drawers, the list must be instantiated for appropriate keyboard interaction.
            const list = new MDCList(listElement);
            list.wrapFocus = true;
            window.Mobsites.Blazor.AppDrawer.initialized = true;
            window.Mobsites.Blazor.AppDrawer.self = list;
        }
    },
    initResizeEvent: function () {
        window.addEventListener('resize', this.invokeNetRefresh);
    },
    initModalEvents: function () {
        // Event for closing modal drawer when navigation or action occurs.
        const listElement = document.querySelector('.mdc-drawer .mdc-list, .mdc-drawer .mdc-drawer__content');
        if (listElement) {
            listElement.addEventListener('click', this.closeDrawerClickEvent);
        }
        // Event for toggling modal drawer when .mobsites-blazor-app-drawer-button class is specified.
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
    invokeNetRefresh: function () {
        // Prevent window.resize event from firing .Net invokeMethodAsync('Refresh', true) more than once.
        clearTimeout(window.Mobsites.Blazor.AppDrawer.timeoutId);
        // Delay the resize handling by 200ms
        window.Mobsites.Blazor.AppDrawer.timeoutId = setTimeout(() => window.Mobsites.Blazor.AppDrawer.instance.invokeMethodAsync('Refresh', true), 200);
    }
}