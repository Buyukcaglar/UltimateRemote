var _dotNetref;

export function registerPlaylistNameChangeEvents(dotNetRef) {
    _dotNetref = dotNetRef;
    
    var layoutNameElements = document.querySelectorAll("span[contenteditable='true']");
    layoutNameElements.forEach(function (el) { 
        el.addEventListener("blur", (evt) => {             
            _dotNetref.invokeMethodAsync('OnPlaylistNameChange', el.dataset.playlistid, el.innerText);
        });
    });   
   
}

export function registerPlaylistNameChangeEvent(playlistid, dotNetRef) {
    _dotNetref = dotNetRef;    
   let el = document.getElementById("playlistname-"+playlistid);
   el.addEventListener("blur", (evt) => {             
            _dotNetref.invokeMethodAsync('OnPlaylistNameChange', el.dataset.playlistid, el.innerText);
        });
}