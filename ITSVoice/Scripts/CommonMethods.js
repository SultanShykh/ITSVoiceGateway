function resetValidation() {
    $('.field-validation-error').text('');
}

function DeleteConfirm(url, Id) {
    swal({
        title: 'Are you sure?',
        text: "You won't be able to revert this!",
        type: 'warning',
        buttons: {
            confirm: {
                text: 'Yes, delete it!',
                className: 'btn btn-success'
            },
            cancel: {
                visible: true,
                className: 'btn btn-danger'
            }
        }
    }).then((Delete) => {
        if (Delete) {
            CrudScript.makeAjaxRequest('POST', url + "/"+Id).then(function (data) {
                if (data.status == true) {
                    ShowDivSuccess(data.message);
                }
                else {
                    ShowDivError(data.message);
                }
            });
            
        } else {
            swal.close();
        }
    });
}

function DeleteConfirmGET(url, Id) {
    swal({
        title: 'Are you sure?',
        text: "You won't be able to revert this!",
        type: 'warning',
        buttons: {
            confirm: {
                text: 'Yes, delete it!',
                className: 'btn btn-success'
            },
            cancel: {
                visible: true,
                className: 'btn btn-danger'
            }
        }
    }).then((Delete) => {
        if (Delete) {
            CrudScript.makeAjaxRequest('GET', url).then(function (data) {
                if (data.status == true) {
                    ShowDivSuccess(data.message);
                }
                else {
                    ShowDivError(data.message);
                }
            });

        } else {
            swal.close();
        }
    });
}

function ShowDivError(msg) {
    swal("Error!!", msg, {
        icon: "error",
        buttons: {
            confirm: {
                text: "OK",
                value: true,
                visible: true,
                className: 'btn btn-danger',
                closeModal: true
            }
        },
    });
}
function HideDivError() {
    $('#lblError').html('');
    $('#divError').hide();
}

function ShowDivSuccess(msg) {
    swal({
        text: msg,
        icon: "success",
        buttons: {
            confirm: {
                text: "OK",
                value: true,
                visible: true,
                className: "btn btn-success",
                closeModal: true
            }
        }
    }).then(() => {
        location.reload();
    });
}

function ShowDivSuccess1(msg) {
    swal({
        text: msg,
        icon: "/Media/loading_gif.gif"
    });
}

var ajaxScript = {
    makeAjaxRequest: function (methodType,url,params,FormData) {
        return new Promise(function (resolve, reject) {
            if (methodType == "GET") {
                if (params == null) {
                    debugger
                    $.get(url).done(function (response) {
                        resolve(response)
                    }).fail(function (response) {
                        reject(response);
                    })
                } else {
                    $.get(url, params).done(function (response) {
                        resolve(response)
                    }).fail(function (response) {
                        reject(response);
                    })
                }
            } else {
                if (params == null) {
                    $.post(url).done(function (response) {
                        resolve(response)
                    }).fail(function (response) {
                        reject(response);
                    });
                }
                else {
                    $.post(url, params).done(function (response) {
                        resolve(response)
                    }).fail(function (response) {
                        reject(response);
                    });
                }
            }
            })
		}
    }