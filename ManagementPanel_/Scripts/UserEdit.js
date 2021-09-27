
document.getElementById("loader").style.display = "none";
function Control() {
    document.getElementById("loader").style.display = "block";
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
        swal.fire("Uyarı !", "Kullanıcı kodu alanı boş geçilemez.", "warning");
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
                Save();
            }
            else {
                swal.fire("Hata !", result.ErrorMessage, "error");
                document.getElementById("loader").style.display = "none";
            }
        }

    });
}

function Save() {
    let name = $("#Name").val();
    let surname = $("#Surname").val();
    let username = $("#Username").val();
    let phone = $("#Phone").val();
    let password = $("#Password").val();
    let email = $("#Email").val();
    let date = $("#Date").val();
    
    var obj = {
        Name: name,
        Surname: surname,
        Username: username,
        Password: password,
        Phone: phone,
        Email: email,
        Date: date
    }


    $.ajax({
        url: "/User/AddUser",
        data: {
            model: obj
        },
        type: "POST",
        success: function (result) {
            document.getElementById("loader").style.display = "none";
            if (result.Status) {
                clean();               
                Swal.fire({
                    title: "Başarılı!",
                    text: result.Message,
                    icon: "success",
                    confirmButtonColor: "#3085d6",
                    confirmButtonText: "Tamam",
                    showCancelButton: false,
                }).then(function (result) {
                    debugger;
                    if (result.isConfirmed) {
                        window.location = '/Home/Index';
                    }
                });

            }
            else {
                swal.fire("Hata !", result.ErrorMessage, "error");
            }

        }

    });
}

function validateEmail(email) {
    const re = /^(([^<>()[\]\\.,;:\s@"]+(\.[^<>()[\]\\.,;:\s@"]+)*)|(".+"))@((\[[0-9]{1,3}\.[0-9]{1,3}\.[0-9]{1,3}\.[0-9]{1,3}\])|(([a-zA-Z\-0-9]+\.)+[a-zA-Z]{2,}))$/;
    return re.test(String(email).toLowerCase());
}

function validatePhoneNumber(input_str) {
    var re = /^[\+]?[(]?[0-9]{3}[)]?[-\s\.]?[0-9]{3}[-\s\.]?[0-9]{4,6}$/im;

    return re.test(input_str);
}

function passwordShowHide() {
    var x = document.getElementById("Password");
    if (x.type === "password") {
        x.type = "text";
    } else {
        x.type = "password";
    }
}

function clean() {
    $("#Name").val("");
    $("#Surname").val("");
    $("#Phone").val("");
    $("#Password").val("");
    $("#Email").val("");
    $("#Date").val("");
}