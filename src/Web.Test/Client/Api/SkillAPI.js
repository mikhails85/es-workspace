export default {
    getSkillList: () => {
        return fetch('/api/skills/list', {
            method: 'get'
        }).then(function (response) {
            return response.json();
        }).then(function (response) {
            return response;
        });
    },
    
    addSkill: (skill) => {
        return fetch('/api/skills/addskill', {
            method: 'post',
            body: skill
        }).then(function (response) {
            return response.json();
        }).then(function (response) {
            return response;
        });
    }   
}