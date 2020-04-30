import React, { useMemo, useState, useEffect, useCallback } from 'react';
import { Modal, Row, Col, DatePicker, Input } from 'antd';
import './index.scss';
import moment from 'moment';

const { TextArea } = Input;

function AcaoCorretivaModal({ acaoCorretiva, visible, onOk, onCancel }) {

    const [internalAcaoCorretiva, setInternalAcaoCorretiva] = useState({});

    const modalTitle = useMemo(() => acaoCorretiva ? `Atualizando ação corretiva ${acaoCorretiva.acaoCorretivaID}` : 'Cadastrando ação corretiva', [acaoCorretiva]);

    useEffect(() => {
        if(acaoCorretiva)
            setInternalAcaoCorretiva(acaoCorretiva);
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
                        />
                    </Col>
                </Row>
            </div>
        </Modal>
    );
}

export default AcaoCorretivaModal;
