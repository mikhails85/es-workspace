export default {
    getEmployeeList: (page, size, search) => {
        return fetch('/api/employees/list?page='+page+'&size='+size+'&search='+search, {
            method: 'get'
        }).then(function (response) {
            return response.json();
        }).then(function (response) {
            return response;
        });
    },

    getEmployee: (id) => {
        return fetch('/api/employees/'+id+'/getemployee', {
            method: 'get'
        }).then(function (response) {
            return response.json();
        }).then(function (response) {
            return response;
        });
    },

    addEmployee: (employee) => {
        return fetch('/api/employees/addemployee', {
            method: 'post',
            body: employee
        }).then(function (response) {
            return response.json();
        }).then(function (response) {
            return response;
        });
    },

    updateEmployee: (employee) => {
        return fetch('/api/employees/'+employee.id+'/updateemployee', {
            method: 'put',
            body: employee
        }).then(function (response) {
            return response.json();
        }).then(function (response) {
            return response;
        });
    },

    deleteEmployee: (id) => {
        return fetch('/api/employees/'+employee.id+'/deleteemployee', {
            method: 'delete'
        }).then(function (response) {
            return response.json();
        }).then(function (response) {
            return response;
        });
    },

    addProject: (project) => {
        return fetch('/api/employees/addproject', {
            method: 'post',
            body: project
        }).then(function (response) {
            return response.json();
        }).then(function (response) {
            return response;
        });
    },

    deleteProject: (id, projectId) => {
        return fetch('/api/employees/'+id+'/deleteproject/'+projectId, {
            method: 'delete'
        }).then(function (response) {
            return response.json();
        }).then(function (response) {
            return response;
        });
    }
}