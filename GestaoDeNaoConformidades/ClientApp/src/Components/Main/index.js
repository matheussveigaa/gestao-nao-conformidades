import React, { useState, useCallback } from 'react';
import { Layout, Menu, Icon } from 'antd';
import './index.scss';
import { withRouter, Link } from 'react-router-dom';
import PagesUtils from '../../Utils/PagesUtils';
import PathEnum from '../../Enums/PathEnum';

const { Header, Sider, Content } = Layout;

function Main({ children, location }) {

    const [collapsed, setCollapsed] = useState(true);

    const toggle = useCallback(collapsed => {
        setCollapsed(collapsed);
    }, [collapsed]);

    return (
        <Layout style={{ minHeight: "100vh" }}>
            <Sider breakpoint="sm" onCollapse={toggle} collapsible collapsed={collapsed}>
                <div className="logo" />
                <Menu theme="dark" mode="inline">
                    <Menu.Item key="1">
                        <Icon type="ordered-list" />
                        <span>
                            Listagem de não conformidades
                        </span>
                        <Link to={PathEnum.INDEX}/>
                    </Menu.Item>
                    <Menu.Item key="2">
                        <Icon type="form" />
                        <span>
                            Cadastro de não conformidades
                        </span>
                        <Link to={PathEnum.CADASTRO_NAO_CONFORMIDADES}/>
                    </Menu.Item>
                    {/* <Menu.Item key="2">
                        <Icon type="select" />
                        <span>
                            Atribuir ações corretivas
                        </span>
                        <Link to={PathEnum.ATRIBUIR_ACOES_CORRETIVAS}/>
                    </Menu.Item>
                    <Menu.Item key="3">
                        <Icon type="upload" />
                        <span>nav 3</span>
                    </Menu.Item> */}
                </Menu>
            </Sider>
            <Layout>
                <Header style={{ padding: 0 }}>
                </Header>
                <Content
                    style={{
                        margin: '24px 16px',
                        padding: 24,
                        background: '#fff',
                        minHeight: 280,
                        boxShadow: '0 0 5px rgba(0,0,0,.1)'
                    }}
                >
                    <div className="page-title">
                        <h2>
                            {PagesUtils.getPageDescription(location.pathname)}
                        </h2>
                    </div>
                    {children}
                </Content>
            </Layout>
        </Layout>
    );
}

export default withRouter(Main);