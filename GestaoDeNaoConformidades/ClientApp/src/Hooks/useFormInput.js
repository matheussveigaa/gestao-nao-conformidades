import React, { useState } from 'react';

function useFormInput(initialState){
    const [value, setValue] = useState(initialState);

    function onChangeHandle(e){
        if(e.target){
            setValue(e.target.value);
        }
    }

    return {
        value,
        onChange: onChangeHandle
    }
}

export default useFormInput;