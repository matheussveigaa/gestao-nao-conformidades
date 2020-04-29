function groupBy(xs, f){
    return xs.reduce((r, v, i, a, k = f(v)) => ((r[k] || (r[k] = [])).push(v), r), {});
}

function containsObject(array, obj, prop){
    let i = array.length;
    while (i--) {
        if (array[i][prop] === obj[prop])
            return true;
    }
    return false;
}

function isValueValid(value){
    return value !== undefined && value !== null && value !== '' && JSON.stringify(value) !== '{}' && JSON.stringify(value) !== '[]';
}
function getRandomDarkColor () {
    var letters = '0123456789';
    var color = '#';
    for (var i = 0; i < 6; i++) {
      color += letters[Math.floor(Math.random() * 10)];
    }
    return color;
}

export default { groupBy, containsObject, isValueValid, getRandomDarkColor };