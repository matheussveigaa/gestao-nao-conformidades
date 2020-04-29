import React, { useState } from 'react';

function useSelectInput(initialState){
    const [value, setValue] = useState(initialState);

    function onChangeHandle(e, option){
        if(option){
            setValue(option.value);
        }
    }

    return {
        value,
        onChange: onChangeHandle
    }
}

export default useSelectInput;