
$(document).ready(function () {
    $('#example').DataTable({
        ajax: {
            url: '/Home/GetUserList',
            dataSrc: '',
            type: "POST"
        },
        language: {
            "emptyTable": "Gösterilecek ver yok.",
            "processing": "Veriler yükleniyor",
            "sDecimal": ".",
            "sInfo": "_TOTAL_ kayıttan _START_ - _END_ arasındaki kayıtlar gösteriliyor",
            "sInfoFiltered": "(_MAX_ kayıt içerisinden bulunan)",
            "sInfoPostFix": "",
            "sInfoThousands": ".",
            "sLengthMenu": "Sayfada _MENU_ kayıt göster",
            "sLoadingRecords": "Yükleniyor...",
            "sSearch": "Ara:",
            "sZeroRecords": "Eşleşen kayıt bulunamadı",
            "oPaginate": {
                "sFirst": "İlk",
                "sLast": "Son",
                "sNext": "Sonraki",
                "sPrevious": "Önceki"
            },
            "oAria": {
                "sSortAscending": ": artan sütun sıralamasını aktifleştir",
                "sSortDescending": ": azalan sütun sıralamasını aktifleştir"
            },
            "select": {
                "rows": {
                    "_": "%d kayıt seçildi",
                    "0": "",
                    "1": "1 kayıt seçildi"
                }
            }
        },
        autoWidth: false,
        responsive: true,
        "ordering": false,
        "paging": true,
        "columnDefs": [{
            //render: $.fn.dataTable.render.moment( 'DD/MM/YYYY HH:mm' )
            "render": function (data) {
                return moment(data).format('DD.MM.YYYY');
            },
            "targets": 4
        }],
        columns: [
            {
                data: null, title: 'Ad Soyad',
                render: function (data, type, row) {
                    return data.Name + " " + data.Surname;
                }
            },
            { data: "Username", title: 'Kullanıcı Adı' },
            { data: "Email", title: 'Email' },
            { data: "Phone", title: 'Telefon Num' },
            { data: "Date", title: 'Doğum Tarihi' },
            {
                data: null,
                title:"Rol",
                visible: admin,
                render: function (data, type, row) {
                    if(data.Admin == true)
                        return 'Admin';
                    else
                        return 'Kullanıcı';
                }
            },
            {
                data: null,
                visible: admin,
                with:"auto",
                render: function (data, type, row) {

                    return '<div class="row">' +
                        '<div class="col-sm-8 col-md-4" >' +
                        '<button id="myBtn" class="btn btn-sm btn-info" onclick="Guncelle(' + data.ID + ')">Güncelle</button>' +
                        '</div >' +
                        '<div class="col-sm-2 col-md-4">' +
                        ' <button onclick="Sil(' + data.ID + ')" class="btn btn-sm btn-danger">Sil</button>' +
                        ' </div>' +
                        ' </div >';
                }
            }
        ]


    });
});

function Sil(obj) {
    debugger;
    Swal.fire({
        title: "Uyarı!",
        text: "Silmek istediğinize emin misiniz?",
        icon: "warning",
        cancelButtonColor: "#DD6B55",
        confirmButtonColor: "#3085d6",
        confirmButtonText: "Evet",
        cancelButtonText: "Hayır",
        showCancelButton: true,
    }).then(function (result) {
        debugger;
        if (result.isConfirmed)
            Delete(obj);
    });

}

document.getElementById("loader").style.display = "none";
function Guncelle(Id) {
    document.getElementById("loader").style.display = "block";
    $.ajax({
        url: "/User/EditUser",
        data: {
            id: Id
        },
        type: "POST",
        success: function (result) {
            document.getElementById("loader").style.display = "none";
            $("#mDiv").append(result);
        }
    });
}

function validatePhoneNumber(input_str) {
    var re = /^[\+]?[(]?[0-9]{3}[)]?[-\s\.]?[0-9]{3}[-\s\.]?[0-9]{4,6}$/im;

    return re.test(input_str);
}
function validateEmail(email) {
    const re = /^(([^<>()[\]\\.,;:\s@"]+(\.[^<>()[\]\\.,;:\s@"]+)*)|(".+"))@((\[[0-9]{1,3}\.[0-9]{1,3}\.[0-9]{1,3}\.[0-9]{1,3}\])|(([a-zA-Z\-0-9]+\.)+[a-zA-Z]{2,}))$/;
    return re.test(String(email).toLowerCase());
}

function Control() {
    let name = $("#Name").val();
    let surname = $("#Surname").val();
    let username = $("#Username").val();
    let phone = $("#Phone").val();
    let password = $("#Password").val();
    let email = $("#Email").val();
    let date = $("#Date").val();

    if (name == "" || name == null) {
        swal.fire("Uyarı !", "Ad alanı boş geçilemez.", "warning");
        return;
    }
    else if (surname == "" || surname == null) {
        swal.fire("Uyarı !", "Soyad alanı boş geçilemez.", "warning");
        return;
    }
    else if (username == "" || username == null) {
        swal.fire("Uyarı !", "Kullanıcı Kodu alanı boş geçilemez.", "warning");
        return;
    }
    else if (password == "" || password == null) {
        swal.fire("Uyarı !", "Şifre alanı boş geçilemez.", "warning");
        return;
    }
    else if (phone == "" || phone == null) {
        swal.fire("Uyarı !", "Telefon numarası alanı boş geçilemez.", "warning");
        return;
    }
    else if (validatePhoneNumber(phone) && phone.toString().replace(/\D/g, "").length != 10) {
        swal.fire("Uyarı !", "Telefon numarasını kontrol ediniz.", "warning");
        return;
    }
    else if (email == "" || email == null) {
        swal.fire("Uyarı !", "Email alanı boş geçilemez.", "warning");
        return;
    }
    else if (!validateEmail(email)) {
        swal.fire("Uyarı !", "Email alanı boş geçilemez.", "warning");
        return;
    }
    else if (date == "" || date == null) {
        swal.fire("Uyarı !", "Doğum Tarihi alanı boş geçilemez.", "warning");
        return;
    }

    $.ajax({
        url: "/User/GetEmailPhoneUsernameControl",
        data: {
            email: email,
            phone: phone,
            username: username
        },
        type: "POST",
        success: function (result) {
            debugger;
            if (result.Status) {
                Update();
            }
            else {
                swal.fire("Hata !", result.ErrorMessage, "error");
            }

        }

    });
}




function Update() {
    let id = $("#ID").val();
    let name = $("#Name").val();
    let surname = $("#Surname").val();
    let phone = $("#Phone").val();
    let password = $("#Password").val();
    let email = $("#Email").val();
    let date = $("#Date").val();
        
    var obj = {
        ID: id,
        Name: name,
        Surname: surname,
        Password: password,
        Phone: phone,
        Email: email,
        Date: date
    }


    $.ajax({
        url: "/User/UpdateUser",
        data: {
            model: obj
        },
        type: "POST",
        success: function (result) {
            if (result.Status) {
                swal.fire("Başarılı !", result.Message, "success");
                spanClick();
                $('#example').DataTable().ajax.reload();
            }
            else {
                swal.fire("Hata !", result.ErrorMessage, "error");
            }

        }

    });
}

function spanClick() {
    $("#mDiv").empty();
}

function passwordShowHide2() {
    var x = document.getElementById("Password");
    if (x.type === "password") {
        x.type = "text";
    } else {
        x.type = "password";
    }
}

function Delete(obj) {
    document.getElementById("loader").style.display = "block";
    $.ajax({
        url: "/User/DeleteUser",
        data: {
            id: obj
        },
        type: "POST",
        success: function (result) {
            debugger;
            if (result.Status) {
                swal.fire("Başarılı !", result.Message, "success");
                $('#example').DataTable().ajax.reload();
            }
            else {
                swal.fire("Hata !", result.ErrorMessage, "error");
            }
            document.getElementById("loader").style.display = "none";
        }

    });
}

function Refresh() {
    $('#example').DataTable().ajax.reload();
}