import BaseRequest from "./BaseRequest";
import PathEnum from '../Enums/PathEnum';
import qs from 'qs';

const BASE_URL = `${PathEnum.BASE_URL}/api/nao-conformidade`;

class NaoConformidadeService extends BaseRequest{
    constructor() {
        super();
    }

    inserirNaoConformidade(data) {
        return this.call({ method: 'POST', url: `${BASE_URL}`, data });
    }

    obterTodasNaoConformidades() {
        return this.call({ method: 'GET', url: `${BASE_URL}`});
    }

    obterNaoConformidadesPorDataOcorrenciaOuDescricaoOuDepartamentos(query) {
        let queryString = qs.stringify(query);

        return this.call({ method: 'GET', url: `${BASE_URL}/por-filtro?${queryString}`});
    }
}

export default new NaoConformidadeService();