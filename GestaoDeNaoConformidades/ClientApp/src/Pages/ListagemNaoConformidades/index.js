import React, { useState, useEffect, useCallback } from 'react';
import './index.scss';
import { Skeleton, Card, Row, Col, DatePicker, Table, Divider, Tag, Input, Select, Button } from 'antd';
import MessageUtils from '../../Utils/MessageUtils';
import NaoConformidadeService from '../../Services/NaoConformidadeService';
import { Link } from 'react-router-dom';
import PathEnum from '../../Enums/PathEnum';
import DepartamentoService from '../../Services/DepartamentoService';
import moment from 'moment';

const { Option } = Select;

const columns = [
    {
        title: 'Nro. Não conformidade',
        dataIndex: 'naoConformidadeID',
        key: 'naoConformidadeID',
        render: text => text,
    },
    {
        title: 'Data ocorrência',
        dataIndex: 'dataOcorrencia',
        key: 'dataOcorrencia',
        render: text => text && new Date(text).toLocaleDateString(),
    },
    {
        title: 'Departamentos',
        dataIndex: 'departamentos',
        key: 'departamentos',
        render: departamentos => (
            <span>
                {departamentos.map(departamento => 
                    <Tag key={departamento.departamentoID}>
                        {departamento.nome.toUpperCase()}
                    </Tag>
                )}
            </span>
        ),
    },
    {
        title: 'Ações',
        key: 'action',
        render: (text, record) => (
            <span>
                <Link to={{ pathname: PathEnum.CADASTRO_NAO_CONFORMIDADES, state: record }}>
                    Visualizar
                </Link>
                <Divider type="vertical" />
                <Link to={{ pathname: PathEnum.ATRIBUIR_ACOES_CORRETIVAS, state: record }}>
                    Atribuir ações corretivas
                </Link>
            </span>
        ),
    },
];

function ListagemNaoConformidades(props) {
    const [dataSource, setDataSource] = useState([]);
    const [loading, setLoading] = useState(true);
    const [filtros, setFiltros] = useState('');
    const [departamentoOptions, setDepartamentoOptions] = useState([]);

    useEffect(() => {
        initFetch()
    }, []);

    useEffect(() => {
        if(filtros)
            aplicarFiltros();
    }, [filtros]);

    const initFetch = async () => {
        try {
            await fetchDepartamentos();
            await fetchNaoConformidades();
        } catch(error) {
            MessageUtils.swalError(error);
        } finally {
            setLoading(false);
        }
    };

    const fetchNaoConformidades = async () => {
        try {
            let naoConformidades = await NaoConformidadeService.obterTodasNaoConformidades();
            setDataSource(naoConformidades);
        } catch(error) {
            MessageUtils.swalError(error);
        }
    };

    const fetchDepartamentos = async () => {
        try {
            let departamentos = await DepartamentoService.obterDepartamentos();
            
            setDepartamentoOptions(departamentos);
        } catch(error) {
            MessageUtils.swalError(error);
        }
    };
    
    const onChangeDate = useCallback((dateAsMoment, dateAsString) => {
        let newFiltros = { ...filtros, dataOcorrencia: dateAsMoment.toDate().toDateString()};
        setFiltros(newFiltros);
    }, [filtros]);

    const onChange = useCallback(event => {
        let prop = event.target.id;
        let value = event.target.value;

        let newFiltros = { ...filtros, [prop]: value };

        setFiltros(newFiltros);
    }, [filtros]);

    const onChangeSelect = useCallback(value => {
        let newFiltros = { ...filtros, departamentos: value };
        setFiltros(newFiltros);
    }, [filtros]);

    const aplicarFiltros = useCallback(async () => {
        try {
            setLoading(true);
            let query = { ...filtros };
            query.departamentos = filtros.departamentos ? filtros.departamentos.map(id => Number.parseInt(id)) : [];

            let naoConformidades = await NaoConformidadeService.obterNaoConformidadesPorDataOcorrenciaOuDescricaoOuDepartamentos(query);

            setDataSource(naoConformidades);
        } catch(error) {
            MessageUtils.swalError(error);
        } finally {
            setLoading(false);
        }
    }, [filtros]);

    const limparFiltros = async () => {
        try {
            setLoading(true);
            setFiltros('');

            await fetchNaoConformidades();
        } catch(error) {
            MessageUtils.swalError(error);
        } finally {
            setLoading(false);
        }
    };

    return (
        <div className="listagem-nao-conformidades-wrapper">
            <Card title="Filtros" style={{marginBottom: 10}}>
                <Row type="flex" justify="space-between" style={{marginBottom: 10}}>
                    <Col 
                        xs={24} 
                        sm={24} 
                        md={7} 
                        lg={7} 
                        xxl={7}
                    >
                        <Row>
                            <label>
                                Data ocorrência:
                            </label>
                        </Row>
                        <Row>
                            <DatePicker
                                disabledTime
                                style={{width: '100%'}}
                                value={filtros.dataOcorrencia && moment(filtros.dataOcorrencia)}
                                onChange={onChangeDate}
                                format='DD/MM/YYYY'
                            />
                        </Row>
                    </Col>
                    <Col 
                        xs={24} 
                        sm={24} 
                        md={7} 
                        lg={7} 
                        xxl={7}
                    >
                        <Row>
                            <label>
                                Ocorrência:
                            </label>
                        </Row>
                        <Row>
                            <Input
                                id="descricao"
                                value={filtros.descricao}
                                onChange={onChange}
                            />
                        </Row>
                    </Col>
                    <Col 
                        xs={24} 
                        sm={24} 
                        md={7} 
                        lg={7} 
                        xxl={7}
                    >
                        <Row>
                            <label>
                                Departamentos:
                            </label>
                        </Row>
                        <Row>
                            <Select 
                                onChange={onChangeSelect}
                                style={{width: '100%'}}
                                mode="multiple"
                                value={filtros.departamentos}
                            > 
                                {departamentoOptions.length > 0 && departamentoOptions.map(departamento => 
                                    <Option key={departamento.departamentoID}>
                                        {departamento.nome}
                                    </Option>    
                                )}
                            </Select>
                        </Row>
                    </Col>
                </Row>
                <Row type="flex" className="btn-group">
                    <Button type="primary" onClick={aplicarFiltros}>
                        Aplicar filtros
                    </Button>
                    <Button onClick={limparFiltros}>
                        Limpar filtros
                    </Button>
                </Row>
            </Card>
            <Skeleton active loading={loading} paragraph={{ rows: 10 }}>
                <Card>
                    <Table
                        columns={columns}
                        dataSource={dataSource}
                        rowKey={(r, index) => index}
                        scroll={{x: true}}
                    />
                </Card>
            </Skeleton>
        </div>
    );
}

export default ListagemNaoConformidades;
