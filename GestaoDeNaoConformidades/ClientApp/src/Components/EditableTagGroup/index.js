import React, { useState, useRef, useEffect, useCallback } from 'react';
import { Tag, Tooltip, Icon, Select } from 'antd';
import './index.scss';

const { Option } = Select;

function EditableTagGroup({ 
    addTagTitle = 'Nova tag', 
    initialTags = [], 
    groupTitle, 
    options, 
    onAddTag, 
    onRemoveTag, 
    readOnly = false, 
    tagKey,
    tagDescription
}) {
    const [inputVisible, setInputVisible] = useState(false);
    const [tags, setTags] = useState([]);

    const inputRef = useRef(null);

    useEffect(() => {
        if(initialTags.length > 0) {
            let tags = [];
            if(options && options.length > 0) {
                tags = options.filter(opt => initialTags.findIndex(t => t[tagKey] == opt.key) > -1);
            } else {
                if(tagKey && tagDescription) {
                    tags = initialTags.map(tag => ({key: tag[tagKey], description: tag[tagDescription]}));
                }
            }

            setTags(tags);
        }
    }, [initialTags, options]);

    useEffect(() => {
        if(inputVisible)
            inputRef.current && inputRef.current.focus();

    }, [inputVisible, inputRef]);

    const handleClose = useCallback(removedTag => async event => {
        try {
            const newTags = tags.filter(tag => tag.key !== removedTag.key);
        
            onRemoveTag && await onRemoveTag(removedTag);

            setTags(newTags);
        } catch(error) {
            console.warn(error);
        }
    }, [tags, onRemoveTag]);

    const showInput = () => {
        setInputVisible(true);
    };

    const handleInputChange = useCallback(async (value, option) => {
        let key = option.key;
        let description = option.props.children;

        const newTags = [...tags];
        const tagIndex = newTags.findIndex(tag => tag.key == key);

        if(tagIndex === -1) {
            try {
                const newTag = {
                    key,
                    description
                };
                newTags.push(newTag);
    
                onAddTag && await onAddTag(newTag);
    
                setTags(newTags);
                setInputVisible(false);
            } catch(error) {
                console.warn(error);
            }
        }
    }, [tags, onAddTag]);

    const handleInputBlur = () => setInputVisible(false);

    return (
        <div className="editable-tag-group-wrapper">
            {groupTitle && (
                <div className="group-title">
                    <h3>
                        {groupTitle}
                    </h3>
                </div>
            )}
            {tags.length > 0 && tags.map((tag, index) => {
                const isLongTag = tag.description.length > 20;
                const tagElem = (
                    <Tag key={tag.key} closable={!readOnly} onClose={handleClose(tag)}>
                        {isLongTag ? `${tag.description.slice(0, 20)}...` : tag.description}
                    </Tag>
                );
                return isLongTag ? (
                    <Tooltip title={tag.description} key={tag.key}>
                        {tagElem}
                    </Tooltip>
                ) : (
                    tagElem
                );
            })}
            {inputVisible && (
                <Select 
                    size="small" 
                    ref={inputRef}
                    onBlur={handleInputBlur} 
                    onDeselect={handleInputBlur} 
                    onChange={handleInputChange}
                    style={{ width: 100 }}
                    open
                > 
                    {options.length > 0 && options.map(opt => 
                        <Option key={opt.key}>
                            {opt.description}
                        </Option>    
                    )}
                </Select>
            )}
            {(!inputVisible && !readOnly) && (
                <Tag onClick={showInput} style={{ background: '#fff', borderStyle: 'dashed' }}>
                    <Icon type="plus" /> {addTagTitle}
                </Tag>
            )}
        </div>
    );
}

export default EditableTagGroup;
