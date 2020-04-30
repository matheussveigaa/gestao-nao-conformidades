import React, { useMemo, useState, useEffect, useCallback } from 'react';
import { Modal, Row, Col, DatePicker, Input } from 'antd';
import './index.scss';
import moment from 'moment';

const { TextArea } = Input;

function AcaoCorretivaModal({ acaoCorretiva, visible, onOk, onCancel, readOnly = false }) {

    const [internalAcaoCorretiva, setInternalAcaoCorretiva] = useState({});

    const modalTitle = useMemo(() => {
        if(!acaoCorretiva) {
            return 'Cadastrando ação corretiva';
        }
        
        if(acaoCorretiva && acaoCorretiva.acaoCorretivaID && !readOnly) {
            return `Editando ação corretiva ${acaoCorretiva.acaoCorretivaID}`;
        } else {
            return `Visualizando ação corretiva ${acaoCorretiva.acaoCorretivaID}`;
        }
    }, [acaoCorretiva]);

    const okButtonProps = useMemo(() => readOnly && ({ style: { display: 'none' } }), [readOnly]);

    useEffect(() => {
        if(acaoCorretiva)
            setInternalAcaoCorretiva(acaoCorretiva)
        else
            setInternalAcaoCorretiva({});
    }, [acaoCorretiva]);

    const onOkInternal = useCallback(() => {
        onOk && onOk(internalAcaoCorretiva);
    }, [internalAcaoCorretiva, onOk]);

    const onChange = useCallback(event => {
        let prop = event.target.id;
        let val = event.target.value;

        let newAcaoCorretiva = { ...internalAcaoCorretiva };
        newAcaoCorretiva[prop] = val;

        setInternalAcaoCorretiva(newAcaoCorretiva);
    }, [internalAcaoCorretiva]);

    const onChangeDate = useCallback((dateAsMoment, dateAsString) => {
        let newAcaoCorretiva = { ...internalAcaoCorretiva, ateQuando: dateAsMoment };
        setInternalAcaoCorretiva(newAcaoCorretiva);
    }, [internalAcaoCorretiva]);

    return (
        <Modal
            title={modalTitle}
            visible={visible}
            onOk={onOkInternal}
            onCancel={onCancel}
            okText="Salvar"
            cancelText="Cancelar"
            width="120vh"
            okButtonProps={okButtonProps}
        >
            <div className="acao-corretiva-modal-wrapper">
                <Row style={{marginBottom: 10}}>
                    <Col xs={24} sm={24} md={12}>
                        <Row>
                            <label>
                                Até quando: <span className="field-required">*</span>
                            </label>
                        </Row>
                        <Row>
                            <DatePicker
                                disabledTime
                                value={internalAcaoCorretiva.ateQuando && moment(internalAcaoCorretiva.ateQuando)}
                                onChange={onChangeDate}
                                format='DD/MM/YYYY'
                                disabled={readOnly}
                            />
                        </Row>
                    </Col>
                </Row>
                <Row style={{marginBottom: 10}}>
                    <Col xs={24} sm={24} md={11} xxl={11} style={{marginRight: '9vh'}}>
                        <label>
                            Descreva oque fazer: <span className="field-required">*</span>
                        </label>
                        <TextArea
                            id="oqueFazer"
                            rows={10}
                            value={internalAcaoCorretiva.oqueFazer}
                            onChange={onChange}
                            disabled={readOnly}
                        />
                    </Col>
                    <Col xs={24} sm={24} md={11} xxl={11}>
                        <label>
                            Descreva porque fazer: <span className="field-required">*</span>
                        </label>
                        <TextArea
                            id="porqueFazer"
                            rows={10}
                            value={internalAcaoCorretiva.porqueFazer}
                            onChange={onChange}
                            disabled={readOnly}
                        />
                    </Col>
                </Row>
                <Row style={{marginBottom: 10}}>
                    <Col xs={24} sm={24} md={11} xxl={11} style={{marginRight: '9vh'}}>
                        <label>
                            Descreva como fazer: <span className="field-required">*</span>
                        </label>
                        <TextArea
                            id="comoFazer"
                            rows={10}
                            value={internalAcaoCorretiva.comoFazer}
                            onChange={onChange}
                            disabled={readOnly}
                        />
                    </Col>
                    <Col xs={24} sm={24} md={11} xxl={11}>
                        <label>
                            Descreva onde fazer: <span className="field-required">*</span>
                        </label>
                        <TextArea
                            id="ondeFazer"
                            rows={10}
                            value={internalAcaoCorretiva.ondeFazer}
                            onChange={onChange}
                            disabled={readOnly}
                        />
                    </Col>
                </Row>
            </div>
        </Modal>
    );
}

export default AcaoCorretivaModal;
