var _dotNetref;

export function registerLayoutNameChangeEvents(dotNetRef) {
    _dotNetref = dotNetRef;
    var layoutNameElements = document.querySelectorAll("span[contenteditable='true']");
    layoutNameElements.forEach(function (el) { 
        el.addEventListener("blur", (evt) => { 
            _dotNetref.invokeMethodAsync('OnLayoutNameChange', el.dataset.layoutindex, el.innerText);
        });
    });
}