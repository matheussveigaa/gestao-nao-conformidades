import { Card, Divider, Skeleton, Table, Tag } from 'antd';
import React, { useCallback, useEffect, useState } from 'react';
import { Link } from 'react-router-dom';
import PathEnum from '../../Enums/PathEnum';
import DepartamentoService from '../../Services/DepartamentoService';
import NaoConformidadeService from '../../Services/NaoConformidadeService';
import MessageUtils from '../../Utils/MessageUtils';
import './index.scss';
import ListagemNaoConformidadesFiltros from './ListagemNaoConformidadesFiltros';

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
        let newFiltros = { ...filtros, dataOcorrencia: dateAsMoment ? dateAsMoment.toDate().toDateString() : ''};
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
            <ListagemNaoConformidadesFiltros
                departamentoOptions={departamentoOptions}
                filtros={filtros}
                onChange={onChange}
                onChangeDate={onChangeDate}
                onChangeSelect={onChangeSelect}
                aplicarFiltros={aplicarFiltros}
                limparFiltros={limparFiltros}
            />
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
