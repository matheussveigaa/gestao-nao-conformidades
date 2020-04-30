import BaseRequest from './BaseRequest';
import PathEnum from '../Enums/PathEnum';

const BASE_URL = `${PathEnum.BASE_URL}/api/acao-corretiva`;

class AcaoCorretivaService extends BaseRequest{
    constructor() {
        super();
    }

    obterTodasAcoesCorretivasPorNaoConformidade(naoConformidadeID) {
        return this.call({ method: 'GET', url: `${BASE_URL}/${naoConformidadeID}`});
    }

    salvarAcaoCorretiva(data) {
        return this.call({ method: 'POST', url: `${BASE_URL}`, data });
    }
}

export default new AcaoCorretivaService();