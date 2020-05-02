import React from 'react';
import { Card, Row, Col, DatePicker, Input, Select, Button } from 'antd';
import moment from 'moment';

const { Option } = Select;

function ListagemNaoConformidadesFiltros({ filtros, onChangeDate, onChange, onChangeSelect, aplicarFiltros, limparFiltros, departamentoOptions }) {
    return (
        <Card title="Filtros" style={{ marginBottom: 10 }}>
            <Row type="flex" justify="space-between" style={{ marginBottom: 10 }}>
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
                            style={{ width: '100%' }}
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
                            style={{ width: '100%' }}
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
    );
}

export default ListagemNaoConformidadesFiltros;