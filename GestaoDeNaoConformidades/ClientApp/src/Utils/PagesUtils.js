import PagesEnum from "../Enums/PagesEnum";

class PagesUtils {

    getPageDescription(path) {
        let page = PagesEnum.find(p => p.path === path);

        if(page)
            return page.description;
        
        return 'Página não mapeada';
    }

}

export default new PagesUtils();