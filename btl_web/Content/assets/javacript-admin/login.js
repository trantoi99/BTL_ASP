async function postData(verb, url, data) {
    const response = await fetch(url, {
        method: verb,
        mode: 'cors',
        cache: 'default',
        credentials: 'same-origin',
        redirect: 'follow',
        referrerPolicy: 'no-referrer',
        body: data
    }).catch(error => console.error('Error', error));
    return response.json();
};
function login() {
    
    var remmember;
    if ($("#cbRemember").prop('checked'))
    {
        remmember = true;
    }
    else
    {
        remmember = false;
    }
    let formData = new FormData();
    formData.append('username', $('#txtusername').val());
    formData.append('password', $('#txtpassword').val());
    formData.append('remmember', remmember);
    postData('POST', '/Login/LoginUserName', formData).then(function (data) {
        if (data != null && data.Error == false) {
        window.location.href = '/admin/home';
        }
        else {
            alert(data.Title);
        }
    })
};
$(document).ready(function () {
    $('#btnlogin').click(function () {
        login()
    });
});