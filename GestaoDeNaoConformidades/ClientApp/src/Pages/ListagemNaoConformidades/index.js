import React, { useState, useEffect } from 'react';
import './index.scss';
import { Skeleton } from 'antd';

import { Table, Divider, Tag } from 'antd';
import MessageUtils from '../../Utils/MessageUtils';
import NaoConformidadeService from '../../Services/NaoConformidadeService';
import { Link } from 'react-router-dom';
import PathEnum from '../../Enums/PathEnum';

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

    useEffect(() => {
        fetchNaoConformidades();
    }, []);

    const fetchNaoConformidades = async () => {
        try {
            let naoConformidades = await NaoConformidadeService.obterTodasNaoConformidades();
            setDataSource(naoConformidades);
        } catch(error) {
            MessageUtils.swalError(error);
        } finally {
            setLoading(false);
        }
    };

    return (
        <div className="listagem-nao-conformidades-wrapper">
            <Skeleton active loading={loading} paragraph={{ rows: 10 }}>
                <Table
                    columns={columns}
                    dataSource={dataSource}
                    rowKey={(r, index) => index}
                    scroll={{x: true}}
                />
            </Skeleton>
        </div>
    );
}

export default ListagemNaoConformidades;
