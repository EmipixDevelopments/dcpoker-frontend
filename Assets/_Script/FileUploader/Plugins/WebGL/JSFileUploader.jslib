mergeInto(LibraryManager.library,
    {
        SelectFile: function(gameObjectNamePtr, methodNamePtr, filterPtr) {
            gameObjectName = Pointer_stringify(gameObjectNamePtr);
            methodName = Pointer_stringify(methodNamePtr);
            filter = Pointer_stringify(filterPtr);
            
            // Delete if element exist
            var fileInput = document.getElementById(gameObjectName)
            if (fileInput) {
                document.body.removeChild(fileInput);
            }

            fileInput = document.createElement('input');
            fileInput.setAttribute('id', gameObjectName);
            fileInput.setAttribute('type', 'file');
            fileInput.setAttribute('style','display:none;');
            fileInput.setAttribute('style','visibility:hidden;');
            if (filter) {
                fileInput.setAttribute('accept', filter);
            }
            fileInput.onclick = function (event) {
                // File dialog opened
                this.value = null;
            };
            fileInput.onchange = function (event) {
                
                var reader = new FileReader();
                reader.readAsDataURL(event.target.files[0]);
                
                reader.onload = function () {
                    SendMessage(gameObjectName, methodName, reader.result);
                };
                reader.onerror = function (error) {
                    console.log('Error: ', error);
                };
                

                // Remove after file selected
                //document.body.removeChild(fileInput);
            }
            document.body.appendChild(fileInput);

            document.onmouseup = function() {
                fileInput.click();
                document.onmouseup = null;
            }
        },
    });