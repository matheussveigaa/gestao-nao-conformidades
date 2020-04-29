import React, { useState } from 'react';

function useBoolInput(initialState){
    const [value, setValue] = useState(initialState);

    function onChangeHandle(e){
        if(e.target){
            setValue(e.target.checked);
        }
    }

    return {
        value,
        onChange: onChangeHandle,
        checked: value,
    }
}

export default useBoolInput;