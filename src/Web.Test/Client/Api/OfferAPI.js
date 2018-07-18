export default {
    getOfferList: (page, size, search) => {
        return fetch('/api/offers/list?page='+page+'&size='+size+'&search='+search, {
            method: 'get'
        }).then(function (response) {
            return response.json();
        }).then(function (response) {
            return response;
        });
    },

    getOffer: (id) => {
        return fetch('/api/offers/'+id+'/getoffer', {
            method: 'get'
        }).then(function (response) {
            return response.json();
        }).then(function (response) {
            return response;
        });
    },

    addOffer: (offer) => {
        return fetch('/api/offers/addoffer', {
            method: 'post',
            body: offer
        }).then(function (response) {
            return response.json();
        }).then(function (response) {
            return response;
        });
    },

    updateOffer: (offer) => {
        return fetch('/api/offers/'+offer.id+'/updateoffer', {
            method: 'put',
            body: offer
        }).then(function (response) {
            return response.json();
        }).then(function (response) {
            return response;
        });
    },

    deleteOffer: (id) => {
        return fetch('/api/offers/'+offer.id+'/deleteoffer', {
            method: 'delete'
        }).then(function (response) {
            return response.json();
        }).then(function (response) {
            return response;
        });
    }
}