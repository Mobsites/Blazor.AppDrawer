// Copyright (c) 2020 Allan Mobley. All rights reserved.
// Licensed under the MIT License. See License.txt in the project root for license information.

import { MDCDrawer } from "@material/drawer/index";

if (!window.Mobsites) {
    window.Mobsites = {
        Blazor: {

        }
    };
}

window.Mobsites.Blazor.AppDrawers = {
    store: [],
    init: function (dotNetObjRef, elemRefs, options) {
        try {
            const index = this.add(new Mobsites_Blazor_AppDrawer(dotNetObjRef, elemRefs, options));
            dotNetObjRef.invokeMethodAsync('SetIndex', index);
            this.initResizeEvent();
            return true;
        } catch (error) {
            console.log(error);
            return false;
        }
    },
    add: function (appDrawer) {
        for (let i = 0; i < this.store.length; i++) {
            if (this.store[i] == null) {
                this.store[i] = appDrawer;
                return i;
            }
        }
        const index = this.store.length;
        this.store[index] = appDrawer;
        return index;
    },
    update: function (index, options) {
        this.store[index].update(options);
    },
    destroy: function (index) {
        this.store[index].destroy();
        this.store[index] = null;
    },
    initResizeEvent: function () {
        if (this.store.length == 1)
            window.addEventListener('resize', this.resize);
    },
    resize() {
        // Prevent window.resize event from double firing.
        clearTimeout(window.Mobsites.Blazor.AppDrawers.timeoutId);
        // Delay the resize handling by 200ms
        window.Mobsites.Blazor.AppDrawers.timeoutId = setTimeout(() => {
            window.Mobsites.Blazor.AppDrawers.store.forEach(drawer => {
                if (drawer) {
                    drawer.determineModalClassUse();
                    drawer.determineTriggerVisibility();
                }
            });
        }, 200);
    },
    toggle: function (index) {
        this.store[index].toggle();
    },
    open: function (index) {
        this.store[index].open = true;
    },
    close: function (index) {
        this.store[index].open = false;
    }
}

class Mobsites_Blazor_AppDrawer extends MDCDrawer {
    constructor(dotNetObjRef, elemRefs, options) {
        super(elemRefs.drawer);
        this.open = false;
        this.dotNetObjRef = dotNetObjRef;
        this.elemRefs = elemRefs;
        this.dotNetObjOptions = options;
        this.initModalEvents();
        this.determineModalClassUse();
        this.determineTriggerVisibility();
    }
    update(options) {
        this.dotNetObjOptions = options;
        this.determineModalClassUse();
        this.determineTriggerVisibility();
    }
    initModalEvents() {
        var self = this;
        self.elemRefs.content.addEventListener('click', () => {
            self.open = false;
        });

        var trigger = document.querySelector(self.dotNetObjOptions.triggerMarker);
        if (trigger) {
            trigger.addEventListener('click', () => {
                self.toggle();
            });
        }
    }
    toggle() {
        this.open = !this.open;
    }
    determineModalClassUse() {
        if (this.dotNetObjOptions.modalOnly || window.matchMedia('(max-width: ' + this.dotNetObjOptions.responsiveBreakpoint + 'px)').matches) {
            this.elemRefs.drawer.classList.add('mdc-drawer--modal');
        } else {
            this.elemRefs.drawer.classList.remove('mdc-drawer--modal');
        }
    }
    determineTriggerVisibility() {
        var trigger = document.querySelector(this.dotNetObjOptions.triggerMarker);
        if (trigger && this.dotNetObjOptions) {
            if (this.dotNetObjOptions.modalOnly || window.matchMedia('(max-width: ' + this.dotNetObjOptions.responsiveBreakpoint + 'px)').matches) {
                trigger.classList.remove('mobsites-blazor-display-none');
            } else {
                trigger.classList.add('mobsites-blazor-display-none');
            }
        }
    }
}