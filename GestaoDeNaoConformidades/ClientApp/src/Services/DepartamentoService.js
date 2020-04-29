import BaseRequest from './BaseRequest';
import PathEnum from '../Enums/PathEnum';

const BASE_URL = `${PathEnum.BASE_URL}/api/departamento`;

class DepartamentoService extends BaseRequest {
    constructor() {
        super();
    }

    obterDepartamentos() {
        return this.call({ method: 'GET', url: `${BASE_URL}`})
    }
}

export default new DepartamentoService();