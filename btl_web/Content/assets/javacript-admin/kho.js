$(document).ready(function () {
    getListKho();
    $("#table").on('click', '.btnEdit', function () {
        var currow = $(this).closest('tr');
        var col = currow.find('td:eq(1)').text();
        getId(col);
    });
    $("#table").on('click', '.btnDelete', function () {
        var currow = $(this).closest('tr');
        var col = currow.find('td:eq(1)').text();
        Delete(col);
    });
});
function getListKho() {
    postData('GET', '/Kho/ListKho', null).then(function (data) {
       //debugger
        if (data != null) {
            var html = '';
            $.each(data, function (key, item) {
                html += '<tr>';
                html += '<td class="center"><label class="pos-rel"><input type="checkbox" class="ace" /><span class="lbl"></span></label></td>';
                html += '<td>' + item.Makho + '</td>';
                html += '<td>' + item.Tenkho + '</td>';
                html += '<td><div class="hidden-sm hidden-xs btn-group"><button class="btn btnEdit btn-xs btn-info" data-target="#myModal" data-toggle="modal">';
                html += '<i class="ace-icon fa fa-pencil bigger-120"></i></button><button class="btn btn-xs btn-danger btnDelete"><i class="ace-icon fa fa-trash-o bigger-120"></i></button>';
                html += '</div></td>';
                html += '</tr>';
            })
            $("#table").html(html);
        }
    })
};
function SaveChange() {
    const formData = new FormData();
    formData.append('id', $("#txtId").val());
    formData.append('Makho', $("#txtMaKho").val());
    formData.append('Tenkho', $("#txtTenKho").val());
    postData('POST', '/Kho/SaveChange', formData).then(function (data) {
        if (data.Error == true) {
            toastr["error"](data.Title);
        }
        else {
            toastr["success"](data.Title);
            getListKho();
            resetinput();
        }
    })
};
function Delete(Makho) {
    let formData = new FormData();
    formData.append('Makho', Makho);
    postData('DELETE', '/Kho/Delete', formData).then(function (msg) {
        if (msg.Error == true) {
            toastr["error"](msg.Title);
        }
        else {
            toastr["success"](msg.Title);
            getListKho();
        }
    })
};
function resetinput() {
    $("#txtId").val("0");
    $("#txtMaKho").val("");
    $("#txtTenKho").val("");
    document.getElementById('txtMaKho').disabled = false;
}

function getId(Makho) {
    let formData = new FormData();
    formData.append('Makho', Makho);
    postData('POST', '/Kho/getInfoId', formData).then(function (data) {
        if (data != null) {
            $("#txtId").val("1");
            $("#txtMaKho").val(data.Makho);
            document.getElementById('txtMaKho').disabled = true;
            $("#txtTenKho").val(data.Tenkho);
        }
        else {
            toastr["error"]("Khon tim thay ban ghi");
        }
    })
};
//ajax
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
//validate
    (function ($) {
        "use strict";

        /*==================================================================
        [ Validate ]*/
        var input = $('.validate-input .form-control');
        $('#btnsave').click(function () {
            var checks = true;

            for (var i = 0; i < input.length; i++) {
                if (validate(input[i]) == false) {
                    showValidate(input[i]);
                    checks = false;
                }
                else {
                    checks = true;
                }
            }
            if (checks) {
                SaveChange();
            };
            return checks;
        });
        $('.validate-form .form-control').each(function () {
            $(this).focus(function () {
                hideValidate(this);
            });
        });

        function validate(input) {
            if ($(input).attr('type') == 'email' || $(input).attr('name') == 'email') {
                if ($(input).val().trim().match(/^([a-zA-Z0-9_\-\.]+)@((\[[0-9]{1,3}\.[0-9]{1,3}\.[0-9]{1,3}\.)|(([a-zA-Z0-9\-]+\.)+))([a-zA-Z]{1,5}|[0-9]{1,3})(\]?)$/) == null) {
                    return false;
                }
            }
            else {
                if ($(input).val().trim() == '') {
                    return false;
                }
            }
        }

        function showValidate(input) {
            var thisAlert = $(input).parent();

            $(thisAlert).addClass('alert-validate');
        }
        function hideValidate(input) {
            var thisAlert = $(input).parent();

            $(thisAlert).removeClass('alert-validate');
        }
        /*==================================================================
        [ Show pass ]*/
        var showPass = 0;
        $('.btn-show-pass').on('click', function () {
            if (showPass == 0) {
                $(this).next('input').attr('type', 'text');
                $(this).find('i').removeClass('fa-eye');
                $(this).find('i').addClass('fa-eye-slash');
                showPass = 1;
            }
            else {
                $(this).next('input').attr('type', 'password');
                $(this).find('i').removeClass('fa-eye-slash');
                $(this).find('i').addClass('fa-eye');
                showPass = 0;
            }

        });

    })(jQuery);