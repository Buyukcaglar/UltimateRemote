function initApp() {

    // https://stackoverflow.com/a/72781420/3092383
    // Disable browser zoom Ctrl + Mouse Wheel
    document.addEventListener("wheel", function touchHandler(e) { if (e.ctrlKey) { e.preventDefault(); } }, { passive: false });

    window.addEventListener("keydown", function (e) {
        if (e.keyCode === 114 || (e.ctrlKey && e.keyCode === 70)) {
            e.preventDefault();
        }
    })

    const detectOS = function () {
        const platform = window.navigator.platform,
            windowsPlatforms = ['Win32', 'Win64', 'Windows', 'WinCE'],
            customScrollbarsClass = 'custom-scrollbars';

        windowsPlatforms.indexOf(platform) != -1 && document.documentElement.classList.add(customScrollbarsClass);
    };

    detectOS();    
    //setContentHeight();
    //setCardActions();
}

var isUpdatingContentHeight = false;

function setContentHeight() {
    //https://stackoverflow.com/questions/47112393/getting-the-iphone-x-safe-area-using-javascript
    var safeAreaTop = parseInt(getComputedStyle(document.documentElement).getPropertyValue("--sat"), 0);
    var safeAreaBottom = parseInt(getComputedStyle(document.documentElement).getPropertyValue("--sab"), 0);

    var windowInnerHeight = window.innerHeight;
    var navBarclientHeight = document.getElementsByClassName('navbar')[0].clientHeight;
    //var height = (windowInnerHeight + safeAreaBottom + safeAreaTop) - (navBarclientHeight);
    var height = windowInnerHeight - navBarclientHeight;

    //var dLine = "<span>setContentHeight | safeAreaTop: " + safeAreaTop + " safeAreaBottom: " + safeAreaBottom + " windowInnerHeight: " + windowInnerHeight + " navBarclientHeight: " + navBarclientHeight + " height: " + height + "</span><br/>";     
    //$("#hDebug").append(dLine);
    //console.log("setContentHeight safeAreaBottom: " + safeAreaBottom + " safeAreaTop:" + safeAreaTop);    
    //var height = (window.innerHeight + safeAreaBottom + safeAreaTop) - (document.getElementsByClassName('navbar')[0].clientHeight);

    document.getElementsByClassName('content-inner')[0].style.height = height + "px";

    addEventListener("resize", (event) => {

        if (isUpdatingContentHeight === false) {
            isUpdatingContentHeight = true;
            setTimeout(function () {

                //var height = window.innerHeight - document.getElementsByClassName('navbar')[0].clientHeight;
                //console.log("window on resize safeAreaBottom: " + safeAreaBottom + " safeAreaTop:" + safeAreaTop);

                var windowInnerHeight = window.innerHeight;
                var navBarclientHeight = document.getElementsByClassName('navbar')[0].clientHeight;
                //var height = (windowInnerHeight + safeAreaBottom + safeAreaTop) - (navBarclientHeight);
                var height = windowInnerHeight - navBarclientHeight;

                //var dLine2 = "<span>window on resize | safeAreaTop: " + safeAreaTop + " safeAreaBottom: " + safeAreaBottom + " windowInnerHeight: " + window.innerHeight + " clientHeight: " + clientHeight + " height: " + height + "</span><br/>"; 
                //$("#hDebug").append(dLine2);
                //var height = (window.innerHeight + safeAreaBottom + safeAreaTop) - (document.getElementsByClassName('navbar')[0].clientHeight);

                document.getElementsByClassName('content-inner')[0].style.height = height + "px";
                isUpdatingContentHeight = false;
            }, 50);
        }
    });
}

function setCardActions() {
    // Card actions
    // -------------------------    

    // Collapse card
    const cardActionCollapse = function () {

        // Elements
        const buttonClass = '[data-card-action=collapse]',
            cardCollapsedClass = 'card-collapsed';

        // Setup
        document.querySelectorAll(buttonClass).forEach(function (button) {
            button.addEventListener('click', function (e) {
                e.preventDefault();

                const parentContainer = button.closest('.card'),
                    collapsibleContainer = parentContainer.querySelectorAll(':scope > .collapse');

                if (parentContainer.classList.contains(cardCollapsedClass)) {
                    parentContainer.classList.remove(cardCollapsedClass);
                    collapsibleContainer.forEach(function (toggle) {
                        new bootstrap.Collapse(toggle, {
                            show: true
                        });
                    });
                }
                else {
                    parentContainer.classList.add(cardCollapsedClass);
                    collapsibleContainer.forEach(function (toggle) {
                        new bootstrap.Collapse(toggle, {
                            hide: true
                        });
                    });
                }
            });
        });
    };

    // Card fullscreen mode
    const cardActionFullscreen = function () {

        // Elements
        const buttonAttribute = '[data-card-action=fullscreen]',
            buttonClass = 'text-body',
            buttonContainerClass = 'd-inline-flex',
            cardFullscreenClass = 'card-fullscreen',
            collapsedClass = 'collapsed-in-fullscreen',
            scrollableContainerClass = 'content-inner',
            fullscreenAttr = 'data-fullscreen';

        // Configure
        document.querySelectorAll(buttonAttribute).forEach(function (button) {
            button.addEventListener('click', function (e) {
                e.preventDefault();

                // Get closest card container
                const cardFullscreen = button.closest('.card');

                // Toggle required classes
                cardFullscreen.classList.toggle(cardFullscreenClass);

                // Toggle classes depending on state
                if (!cardFullscreen.classList.contains(cardFullscreenClass)) {
                    button.removeAttribute(fullscreenAttr);
                    button.firstElementChild.classList.remove('ph-arrows-in');
                    button.firstElementChild.classList.add('ph-arrows-out');
                    cardFullscreen.querySelectorAll(`:scope > .${collapsedClass}`).forEach(function (collapsedElement) {
                        collapsedElement.classList.remove('show');
                    });
                    document.querySelector(`.${scrollableContainerClass}`).classList.remove('overflow-hidden');
                    button.closest(`.${buttonContainerClass}`).querySelectorAll(`:scope > .${buttonClass}:not(${buttonAttribute})`).forEach(function (actions) {
                        actions.classList.remove('d-none');
                    });
                }
                else {
                    button.setAttribute(fullscreenAttr, 'active');
                    button.firstElementChild.classList.remove('ph-arrows-out');
                    button.firstElementChild.classList.add('ph-arrows-in');
                    cardFullscreen.removeAttribute('style');
                    cardFullscreen.querySelectorAll(`:scope > .collapse:not(.show)`).forEach(function (collapsedElement) {
                        collapsedElement.classList.add('show', `.${collapsedClass}`);
                    });
                    document.querySelector(`.${scrollableContainerClass}`).classList.add('overflow-hidden');
                    button.closest(`.${buttonContainerClass}`).querySelectorAll(`:scope > .${buttonClass}:not(${buttonAttribute})`).forEach(function (actions) {
                        actions.classList.add('d-none');
                    });
                }
            });
        });
    };

    cardActionCollapse();
    cardActionFullscreen();
}

function initDropdownSubmenu() {
    // Classes
    const menuClass = 'dropdown-menu',
        submenuClass = 'dropdown-submenu',
        menuToggleClass = 'dropdown-toggle',
        disabledClass = 'disabled',
        showClass = 'show';

    if (submenuClass) {

        // Toggle submenus on all levels
        document.querySelectorAll(`.${menuClass} .${submenuClass}:not(.${disabledClass}) .${menuToggleClass}`).forEach(function (link) {
            link.addEventListener('click', function (e) {
                e.stopPropagation();
                e.preventDefault();

                // Toggle classes
                link.closest(`.${submenuClass}`).classList.toggle(showClass);
                link.closest(`.${submenuClass}`).querySelectorAll(`:scope > .${menuClass}`).forEach(function (children) {
                    children.classList.toggle(showClass);
                });

                // When submenu is shown, hide others in all siblings
                for (let sibling of link.parentNode.parentNode.children) {
                    if (sibling != link.parentNode) {
                        sibling.classList.remove(showClass);
                        sibling.querySelectorAll(`.${showClass}`).forEach(function (submenu) {
                            submenu.classList.remove(showClass);
                        });
                    }
                }
            });
        });

        // Hide all levels when parent dropdown is closed
        document.querySelectorAll(`.${menuClass}`).forEach(function (link) {
            if (!link.parentElement.classList.contains(submenuClass)) {
                link.parentElement.addEventListener('hidden.bs.dropdown', function (e) {
                    link.querySelectorAll(`.${menuClass}.${showClass}`).forEach(function (children) {
                        children.classList.remove(showClass);
                    });
                });
            }
        });
    }
}

function switchTheme() {
    var currentTheme = localStorage.getItem('theme');

    if (currentTheme === null || currentTheme === 'undefined' || currentTheme === 'dark') {
        setLightTheme();
        adjustThemeSwitcher();
        return;
    }

    setDarkTheme();
    adjustThemeSwitcher();
}

function adjustThemeSwitcher() {
    var currentTheme = localStorage.getItem('theme');

    if (currentTheme === 'dark') {
        removeClass(document.getElementById('themeSwitcher'), 'ph-moon');
        addClass(document.getElementById('themeSwitcher'), 'ph-sun');
        document.getElementsByClassName('dropdown-item themeSwitcher')[0].lastChild.nodeValue = "Switch to light theme";
    }
    else {
        removeClass(document.getElementById('themeSwitcher'), 'ph-sun');
        addClass(document.getElementById('themeSwitcher'), 'ph-moon');
        document.getElementsByClassName('dropdown-item themeSwitcher')[0].lastChild.nodeValue = "Switch to dark theme";
    }
}

function hasClass(el, className) {
    if (el === null)
        return;
    if (el.classList)
        return el.classList.contains(className);
    return !!el.className.match(new RegExp('(\\s|^)' + className + '(\\s|$)'));
}

function addClass(el, className) {
    if (el === null)
        return;
    if (el.classList)
        el.classList.add(className)
    else if (!hasClass(el, className))
        el.className += " " + className;
}

function removeClass(el, className) {
    if (el === null)
        return;
    if (el.classList)
        el.classList.remove(className)
    else if (hasClass(el, className)) {
        var reg = new RegExp('(\\s|^)' + className + '(\\s|$)');
        el.className = el.className.replace(reg, ' ');
    }
}

function initSwiper(swiperId, slidesPerView, breakPoints) {

    const swiper = new Swiper('#' + swiperId, {
        direction: 'horizontal',
        slidesPerView: slidesPerView,
        spaceBetween: 10,
        loop: false,

        pagination: {
            el: '.swiper-pagination',
            type: 'bullets'
        },

        navigation: {
            nextEl: '.carousel-control-next',
            prevEl: '.carousel-control-prev',
            disabledClass: 'disabled_swiper_button'
        },
        breakpoints: breakPoints
    });

}

function initLightGallery(containerId, itemId) {
    lightGallery(document.getElementById(containerId), {
        plugins: [lgHash, lgPager, lgShare, lgThumbnail, lgVideo],
        selector: itemId,
        download: false,
        autoplayFirstVideo: false,
        youTubePlayerParams: {
            showinfo: 0,
            controls: 0,
            playsinline: 1
        }
    });
}

function hideInLibraryItems() {
    var nodeList = document.querySelectorAll("span.badge-inlibrary[data-productid]");
    if (nodeList.length > 0) {
        nodeList.forEach(function (el, idx, arr) {
            addClass(el, "d-none")
        });
    }
}

// blockUI global overrides
// https://jquery.malsup.com/block/#options
$.blockUI.defaults.css.border = 'none';
$.blockUI.defaults.css.backgroundColor = 'transparent';


$.blockUI.defaults.css = {};
$.blockUI.defaults.overlayCss = {};

function blockPage(message) {
    var msg = '<div id="blockContainer" class="bg-transparent text-body text-center">' +
        '<i class="spinner ph-circle-notch me-1"></i>' +
        '<span>' + message + '</span>' +
        '</div>';
    $.blockUI({ message: msg });
}

function updateBlockPageMessage(message) {
    if ($("#blockContainer").length > 0)
        $("#blockContainer span").html(message);
}

function blockPagePopUp(popUpElId) {
    $.blockUI({ message: $('#' + popUpElId) });
    $('#' + popUpElId + 'Close').click(function () {
        $.unblockUI();
        return false;
    });
}

function blockPagePopup(message, closeButtonIcon = 'x-square', closeButtonLabel = 'Close', textAlign = 'justify') {
    var popUp = '<div id="bpConfirmPopUp" class="bg-transparent text-body"><div class="text-' + textAlign + ' p-1">' + message + '</div>' +
        '<button id="bpPopupClose" class="btn btn-sm btn-default text-body border">' + closeButtonLabel + '<i class="ph-' + closeButtonIcon + ' ph-duotone ms-1"></i></button>' +
        '</div>';

    $.blockUI({
        message: popUp,
        onBlock: function () {
            document.getElementById("bpPopupClose").addEventListener("click", (event) => {
                $.unblockUI();
            });
        }
    });
}

function blockPageWithConfirm(message, confirmFunctionName, dotNetRef, textAlign = 'justify') {
    var popUp = '<div id="bpConfirmPopUp" class="bg-transparent text-body"><div class="text-' + textAlign + ' p-1">' + message + '</div>' +
        '<button id="confirmNo" class="btn btn-sm btn-default text-body border">No<i class="ph-hand-palm ph-duotone ms-1"></i></button>' +
        '<button id="confirmYes" class="btn btn-sm btn-default text-body border ms-2">Yes<i class="ph-thumbs-up ph-duotone ms-1"></i></button>' +
        '</div>';

    $.blockUI({
        message: popUp,
        onBlock: function () {
            document.getElementById("confirmNo").addEventListener("click", (event) => {
                $.unblockUI();
                return false;
            });
            document.getElementById("confirmYes").addEventListener("click", (event) => {
                dotNetRef.invokeMethodAsync(confirmFunctionName);
                $.unblockUI();
            });
        }
    });
}

function blockPageWithConfirmWithParam(message, confirmFunctionName, confirmFunctionParam, dotNetRef, textAlign = 'justify') {
    var popUp = '<div id="bpConfirmPopUp" class="bg-transparent text-body"><div class="text-' + textAlign + ' p-1">' + message + '</div>' +
        '<button id="confirmNo" class="btn btn-sm btn-default text-body border">No<i class="ph-hand-palm ph-duotone ms-1"></i></button>' +
        '<button id="confirmYes" class="btn btn-sm btn-default text-body border ms-2">Yes<i class="ph-thumbs-up ph-duotone ms-1"></i></button>' +
        '</div>';

    $.blockUI({
        message: popUp,
        onBlock: function () {
            document.getElementById("confirmNo").addEventListener("click", (event) => {
                console.log("confirmNo");
                $.unblockUI();
                return false;
            });
            document.getElementById("confirmYes").addEventListener("click", (event) => {
                console.log("confirmYes");
                dotNetRef.invokeMethodAsync(confirmFunctionName, confirmFunctionParam);
                $.unblockUI();
            });
        }
    });
}
function unBlock() {
    $.unblockUI();
}


function scrollToId(id) {
    const element = document.getElementById(id);
    if (element instanceof HTMLElement) {
        element.scrollIntoView({
            behavior: "smooth",
            block: "start",
            inline: "nearest"
        });
    }
}