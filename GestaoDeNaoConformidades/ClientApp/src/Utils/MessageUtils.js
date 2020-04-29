import Swal from 'sweetalert2'

class MessageUtils {
    swalError(message){
        Swal.fire({
            title: 'Erro',
            html: `<strong>${message}</strong>`,
            icon: 'error',
            customClass: {
                confirmButton: 'ant-btn ant-btn-primary',
                cancelButton: 'ant-btn'
            },
        });
    }

    swalSuccess(message){
        Swal.fire({
            icon: 'success',
            html: `<strong>${message}</strong>`,
            customClass: {
                confirmButton: 'ant-btn ant-btn-primary',
                cancelButton: 'ant-btn'
            },
        });
    }

    swalWarning(message){
        Swal.fire({
            icon: 'warning',
            title: 'Atenção',
            html: `<strong>${message}</strong>`,
            customClass: {
                confirmButton: 'ant-btn ant-btn-primary',
                cancelButton: 'ant-btn'
            },
        });
    }

    swalInfo(message){
        Swal.fire({
            icon: 'info',
            title: 'Informação',
            html: `<strong>${message}</strong>`,
            customClass: {
                confirmButton: 'ant-btn ant-btn-primary',
                cancelButton: 'ant-btn'
            },
        });
    }
}

export default new MessageUtils();