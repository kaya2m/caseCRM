function setupAjaxWithBearerToken() {
    $.ajaxSetup({
        headers: {
            'Authorization': 'Bearer ' + localStorage.getItem("bearerToken")
        }
    });
}