import Axios from 'axios';

class BaseRequest {
    constructor() {
        this.call = this.call.bind(this);
    }

    call(props){
        return new Promise((resolve, reject) => {
            Axios.request({responseType: props.responseType || 'json', ...props})
            .then(response => {
                let statusCode = response.status;

                switch(statusCode){
                    case 200: {
                        resolve(response.data);
                        break;
                    }
                    case 204: {
                        resolve();
                        break;
                    }
                    case 400: {
                        reject(response.data.Message);
                        break;
                    }
                    case 401: {
                        reject('Você não tem permissão para acessar este recurso!');
                        break;
                    }
                    case 403: {
                        reject('Você não tem permissão para fazer esta ação!');
                        break;
                    }
                    case 404: {
                        reject(response.data.Message);
                        break;
                    }
                    case 500: {
                        reject('Houve um erro interno no servidor, favor entrar em contato com o administrador do sistema!')
                        break;
                    }
                    default: {
                        reject('Nenhuma resposta valida recebida do servidor!');
                        break;
                    }
                }
            })
            .catch(error => {
                let statusCode = error.response.status;

                switch(statusCode){
                    case 400: {
                        reject(error.response.data.Message);
                        break;
                    }
                    case 401: {
                        reject('Você não tem permissão para acessar este recurso!');
                        break;
                    }
                    case 403: {
                        reject('Você não tem permissão para fazer esta ação!');
                        break;
                    }
                    case 404: {
                        reject(error.response.data.Message);
                        break;
                    }
                    case 500: {
                        reject('Houve um erro interno no servidor, por favor tente novamente mais tarde.')
                        break;
                    }
                    default: {
                        reject('Nenhuma resposta valida recebida do servidor, por favor tente novamente mais tarde.');
                        break;
                    }
                }
            }).finally(() => {
                
            });
        });
    }
}

export default BaseRequest;