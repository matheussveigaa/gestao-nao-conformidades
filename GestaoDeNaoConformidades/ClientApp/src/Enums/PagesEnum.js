import React from 'react';
import PathEnum from "./PathEnum";
import CadastroNaoConformidades from "../Pages/CadastroNaoConformidades";
import ListagemNaoConformidades from '../Pages/ListagemNaoConformidades';
import AtribuirAcoesCorretivas from '../Pages/AtribuirAcoesCorretivas';

export default [
    {
        path: PathEnum.CADASTRO_NAO_CONFORMIDADES,
        component: CadastroNaoConformidades,
        description: 'Cadastro de não conformidades'
    },
    {
        path: PathEnum.ATRIBUIR_ACOES_CORRETIVAS,
        component: AtribuirAcoesCorretivas,
        description: 'Atribuir ações corretivas'
    },
    {
        path: PathEnum.INDEX,
        component: ListagemNaoConformidades,
        description: 'Listagem de não conformidades'
    }
];