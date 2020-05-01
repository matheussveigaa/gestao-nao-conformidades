import { Col, Descriptions, Row, Skeleton, Table, Divider, Button, message, Tooltip } from 'antd';
import moment from 'moment';
import React, { useEffect, useState, useMemo, useCallback } from 'react';
import EditableTagGroup from '../../Components/EditableTagGroup';
import PathEnum from '../../Enums/PathEnum';
import MessageUtils from '../../Utils/MessageUtils';
import './index.scss';
import AcaoCorretivaModal from './AcaoCorretivaModal';
import AcaoCorretivaService from '../../Services/AcaoCorretivaService';

function AtribuirAcoesCorretivas({ location, history }) {

    const [naoConformidade, setNaoConformidade] = useState({});
    const [dataSource, setDataSource] = useState([]);
    const [loading, setLoading] = useState(true);
    const [apresentarModalAcaoCorretiva, setApresentarModalAcaoCorretiva] = useState(false);
    const [acaoCorretivaSelecionada, setAcaoCorretivaSelecionada] = useState('');
    const [tableLoading, setTableLoading] = useState(false);
    const [podeEditarAcaoCorretiva, setPodeEditarAcaoCorretiva] = useState(false);

    const columns = useMemo(() => {
        const columns = [
            {
                title: 'Nro. Ação corretiva',
                dataIndex: 'acaoCorretivaID',
                key: 'acaoCorretivaID'
            },
            {
                title: 'Até quando',
                dataIndex: 'ateQuando',
                key: 'ateQuando',
                render: text => text && moment(text).format('DD/MM/YYYY')
            },
            {
                title: 'O que fazer',
                dataIndex: 'oqueFazer',
                key: 'oqueFazer'
            },
            {
                title: 'Por que fazer',
                dataIndex: 'porqueFazer',
                key: 'porqueFazer'
            },
            {
                title: 'Ações',
                key: 'action',
                render: (text, record) => (
                    <span>
                        <a onClick={visualizarAcaoCorretiva(record)}>
                            Visualizar ação
                        </a>
                        <Divider type="vertical" />
                        <a onClick={editarAcaoCorretiva(record)}>
                            Editar ação
                        </a>
                    </span>
                ),
            },
        ];

        return columns;
    }, [])

    useEffect(() => {
        if(naoConformidade.naoConformidadeID)
            fetchAcoesCorretivas();
    }, [naoConformidade]);

    useEffect(() => {
        if (location.state) {
            setNaoConformidade(location.state);
        } else {
            MessageUtils.swalWarning('Nenhuma não conformidade selecionada.');
            history.push(PathEnum.INDEX);
        }
    }, [location]);

    const fetchAcoesCorretivas = useCallback(async () => {
        try {
            setTableLoading(true);

            let acoes = await AcaoCorretivaService.obterTodasAcoesCorretivasPorNaoConformidade(naoConformidade.naoConformidadeID);
            setDataSource(acoes);

        } catch(error) {
            MessageUtils.swalError(error);
        } finally {
            setLoading(false);
            setTableLoading(false);
        }
    }, [naoConformidade]);

    const inserirAcaoCorretiva = () => {
        setPodeEditarAcaoCorretiva(true);
        setAcaoCorretivaSelecionada(null);
        setApresentarModalAcaoCorretiva(true);
    };

    const onCancelModalAcaoCorretiva = () => {
        setApresentarModalAcaoCorretiva(false);
        setAcaoCorretivaSelecionada('');
    };

    const visualizarAcaoCorretiva = acao => event => {
        event.preventDefault();

        setPodeEditarAcaoCorretiva(false);
        setAcaoCorretivaSelecionada(acao);
        setApresentarModalAcaoCorretiva(true);
    };

    const editarAcaoCorretiva = acao => event => {
        event.preventDefault();

        setPodeEditarAcaoCorretiva(true);
        setAcaoCorretivaSelecionada(acao);
        setApresentarModalAcaoCorretiva(true);
    };

    const onOkModalAcaoCorretiva = useCallback(async acaoCorretiva => {
        try {
            let successMessage = acaoCorretiva.acaoCorretivaID ? 'Ação corretiva atualizada com sucesso.' : 'Ação corretiva cadastrada com sucesso.';

            message.loading({ content: 'Registrando ação corretiva...', duration: 10000 });

            if(acaoCorretiva.acaoCorretivaID) {
                await AcaoCorretivaService.atualizarAcaoCorretiva({ ...acaoCorretiva });
            } else {
                await AcaoCorretivaService.salvarAcaoCorretiva({ ...acaoCorretiva, naoConformidadeID: naoConformidade.naoConformidadeID});
            }

            MessageUtils.swalSuccess(successMessage);

            setApresentarModalAcaoCorretiva(false);
            setAcaoCorretivaSelecionada('');
        } catch(error) {
            MessageUtils.swalError(error);
        } finally {
            message.destroy();
            await fetchAcoesCorretivas();
        }
    }, [fetchAcoesCorretivas, naoConformidade]);

    const voltarParaListagem = () => {
        history.push(PathEnum.INDEX);
    };

    return (
        <div className="atribuir-acoes-corretivas-wrapper">
            <AcaoCorretivaModal
                onOk={onOkModalAcaoCorretiva}
                onCancel={onCancelModalAcaoCorretiva}
                visible={apresentarModalAcaoCorretiva}
                acaoCorretiva={acaoCorretivaSelecionada}
                readOnly={!podeEditarAcaoCorretiva}
            />
            <Skeleton active loading={loading} paragraph={{rows: 10}}>
                <Row className="btn-group">
                    <Tooltip title="Voltar para listagem">
                        <Button onClick={voltarParaListagem} icon="step-backward"/>
                    </Tooltip>
                    <Button 
                        onClick={inserirAcaoCorretiva} 
                        type="primary" 
                        icon="plus"
                    >
                        Cadastrar ação corretiva
                    </Button>
                </Row>
                <Row style={{ marginBottom: 10 }}>
                    <Descriptions
                        title="Informações da não conformidade"
                        column={{ xxl: 2, xl: 2, lg: 2, md: 2, sm: 1, xs: 1 }}
                    >
                        <Descriptions.Item label="Nro. Não conformidade">
                            {naoConformidade.naoConformidadeID}
                        </Descriptions.Item>
                        <Descriptions.Item label="Data ocorrência">
                            {moment(naoConformidade.dataOcorrencia).format('DD/MM/YYYY')}
                        </Descriptions.Item>
                        <Descriptions.Item label="Ocorrência">
                            {naoConformidade.descricao}
                        </Descriptions.Item>
                    </Descriptions>
                    <Row style={{ marginBottom: 10 }}>
                        <Col xs={24} sm={24} md={12} xxl={12}>
                            <EditableTagGroup
                                readOnly
                                groupTitle="Departamentos envolvidos"
                                initialTags={naoConformidade.departamentos}
                                tagKey="departamentoID"
                                tagDescription="nome"
                            />
                        </Col>
                    </Row>
                    <Row>
                        <Table
                            dataSource={dataSource}
                            columns={columns}
                            rowKey={(r, index) => index}
                            scroll={{x: true}}
                            loading={tableLoading}
                        />
                    </Row>
                </Row>
            </Skeleton>
        </div>
    );
}

export default AtribuirAcoesCorretivas;