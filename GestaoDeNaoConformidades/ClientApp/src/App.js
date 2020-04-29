import { BrowserRouter as Router, Route, Redirect, Switch } from "react-router-dom";
import React from 'react';
import PagesEnum from "./Enums/PagesEnum";
import Main from "./Components/Main";
import {ConfigProvider} from 'antd';
import pt_BR from 'antd/es/locale/pt_BR';
import 'antd/dist/antd.css';
import PathEnum from "./Enums/PathEnum";

function App(props){

    return (
        <ConfigProvider locale={pt_BR}>
            <Router>
                <Main>
                    <Switch>
                        {PagesEnum.map((route, index) =>
                            <Route
                                key={index}
                                path={route.path}
                                exact={route.exact}
                                component={route.component}
                            />   
                        )}
                        <Redirect to={PathEnum.CADASTRO_NAO_CONFORMIDADES} />
                    </Switch>
                </Main>
            </Router>
        </ConfigProvider>
    );
}

export default App;