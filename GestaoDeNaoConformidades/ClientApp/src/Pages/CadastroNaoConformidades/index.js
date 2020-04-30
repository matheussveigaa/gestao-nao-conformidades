import React, { useState, useCallback, useEffect, useMemo } from 'react';
import './index.scss';
import { Input, Row, Col, DatePicker, Skeleton, Button, message, Tooltip } from 'antd';
import EditableTagGroup from '../../Components/EditableTagGroup';
import DepartamentoService from '../../Services/DepartamentoService';
import NaoConformidadeService from '../../Services/NaoConformidadeService';
import MessageUtils from '../../Utils/MessageUtils';
import moment from 'moment';
import PathEnum from '../../Enums/PathEnum';

const { TextArea } = Input;

function CadastroNaoConformidades({ location, history }) {

    const [naoConformidade, setNaoConformidade] = useState({});
    const [departamentoOptions, setDepartamentoOptions] = useState([]);
    const [loading, setLoading] = useState(true);

    const readOnly = useMemo(() => !!naoConformidade.naoConformidadeID, [naoConformidade]);

    useEffect(() => {
        initFetch();
    }, []);

    useEffect(() => {
        if(location.state) {
            setNaoConformidade(location.state);
        }
    }, [location]);

    const initFetch = async () => {
        await fetchDepartamentoOptions();
        setLoading(false);
    };

    const fetchDepartamentoOptions = async () => {
        try {
            let departamentos = await DepartamentoService.obterDepartamentos();
            let departamentoOptions = departamentos.map(d => ({ key: d.departamentoID, description: d.nome }));

            setDepartamentoOptions(departamentoOptions);
        } catch(error) {
            MessageUtils.swalError(error);
        }
    };

    const onChangeDate = useCallback((dateAsMoment, dateAsString) => {
        let newNaoConformidade = { ...naoConformidade, dataOcorrencia: dateAsMoment };
        setNaoConformidade(newNaoConformidade);
    }, [naoConformidade]);

    const onChange = useCallback(event => {
        let prop = event.target.id;
        let value = event.target.value;

        let newNaoConformidade = { ...naoConformidade, [prop]: value };

        setNaoConformidade(newNaoConformidade);
    }, [naoConformidade]);

    const onAddDepartamento = useCallback(departamento => {
        let newNaoConformidade = { ...naoConformidade };

        if(naoConformidade.departamentos) {
            newNaoConformidade.departamentos.push({ departamentoID: departamento.key });
        } else {
            newNaoConformidade.departamentos = [{ departamentoID: departamento.key }];
        }
        setNaoConformidade(newNaoConformidade);
    }, [naoConformidade]);

    const onRemoveDepartamento = useCallback(departamento => {
        let newNaoConformidade = { ...naoConformidade };

        newNaoConformidade.departamentos = newNaoConformidade.departamentos.filter(d => d.departamentoID != departamento.key);

        setNaoConformidade(newNaoConformidade);
    }, [naoConformidade]);

    const inserirNaoConformidade = useCallback(async () => {
        try {
            message.loading({ content: 'Registrando as informações...', duration: 10000})

            let naoConformidadeDTO = {...naoConformidade};
            naoConformidadeDTO.departamentos = naoConformidade.departamentos ? naoConformidade.departamentos.map(d => ({ departamentoID: Number.parseInt(d.departamentoID) })) : [];

            await NaoConformidadeService.inserirNaoConformidade(naoConformidadeDTO);

            MessageUtils.swalSuccess('Não conformidade registrada com sucesso!');
        } catch(error) {
            MessageUtils.swalError(error);
        } finally {
            message.destroy();
        }
    }, [naoConformidade]);

    const voltarParaListagem = () => {
        history.push(PathEnum.INDEX);
    };

    return (
        <div className="cadastro-nao-conformidades-wrapper">
            <Row className="btn-group" type="flex">
                <Tooltip title="Voltar para listagem">
                    <Button onClick={voltarParaListagem} icon="step-backward"/>
                </Tooltip>
                {!readOnly && (
                    <Button 
                        onClick={inserirNaoConformidade} 
                        type="primary" 
                        icon="check"
                        disabled={readOnly}
                    >
                        Registrar não conformidade
                    </Button>
                )}
            </Row>
            <Skeleton active loading={loading} paragraph={{rows: 10}}>
                <Row style={{marginBottom: 10}}>
                    <Col xs={24} sm={24} md={12}>
                        <Row>
                            <label>
                                Data ocorrencia: <span className="field-required">*</span>
                            </label>
                        </Row>
                        <Row>
                            <DatePicker
                                disabledTime
                                value={naoConformidade.dataOcorrencia && moment(naoConformidade.dataOcorrencia)}
                                onChange={onChangeDate}
                                format='DD/MM/YYYY'
                                disabled={readOnly}
                            />
                        </Row>
                    </Col>
                </Row>
                <Row style={{marginBottom: 10}}>
                    <Col xs={24} sm={24} md={24} xxl={12}>
                        <label>
                            Descreva a ocorrência: <span className="field-required">*</span>
                        </label>
                        <TextArea
                            id="descricao"
                            rows={10}
                            value={naoConformidade.descricao}
                            onChange={onChange}
                            disabled={readOnly}
                        />
                    </Col>
                </Row>
                <Row>
                    <Col xs={24} sm={24} md={12} xxl={12}>
                        <EditableTagGroup
                            readOnly={readOnly}
                            addTagTitle="Departamento"
                            groupTitle="Departamentos envolvidos"
                            initialTags={naoConformidade.departamentos}
                            tagKey="departamentoID"
                            onAddTag={onAddDepartamento}
                            onRemoveTag={onRemoveDepartamento}
                            options={departamentoOptions}
                        />
                    </Col>
                </Row>
            </Skeleton>
        </div>
    );
}

export default CadastroNaoConformidades;