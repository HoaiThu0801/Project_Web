$(document).ready(function () {
    var count = 0;
    $('.cart-plus').click(function () {
        count++;
        $('#number-shopping').html(count);
    });
});
$('#Email').keyup(function () {
    var email = $('#Email').val();
    if (!(/^[a-zA-Z0-9.!#$%&'*+/=?^_`{|}~-]+@[a-zA-Z0-9-]+(?:\.[a-zA-Z0-9-]+)*$/).test(email)) {
        $('#Email').css('background', 'yellow')
        $('#showError_Email').html('Hãy nhập email hợp lệ, VD: huutuong@gmail.com');
        $('#showError_Email').css('color', 'red');
        $('#showError_Email').css('font-weight', 'bold');
        return false;
    }
    else {
        $('#Email').css('background', 'white')
        $('#showError_Email').html('');
        return true;
    }

});
$('#Fullname').click(function () {
    var fullname = $('#Fullname').val();
    if (fullname.length <= 0) {
        $('#Fullname').css('background', 'yellow')
        $('#showError_Fullname').html('Họ tên không được trống!');
        $('#showError_Fullname').css('color', 'red');
        $('#showError_Fullname').css('font-weight', 'bold');
        return false;
    }
    else {
        $('#Fullname').css('background', 'white')
        $('#showError_Fullname').html('');
        return true;
    }
});
$('#Fullname').keyup(function () {
    var fullname = $('#Fullname').val();
    if (fullname.length <= 0) {
        $('#Fullname').css('background', 'yellow')
        $('#showError_Fullname').html('Họ tên không được trống!');
        $('#showError_Fullname').css('color', 'red');
        $('#showError_Fullname').css('font-weight', 'bold');
        return false;
    }
    else {
        $('#Fullname').css('background', 'white')
        $('#showError_Fullname').html('');
        return true;
    }

});
$('#Address').click(function () {
    var address = $('#Address').val();
    if (address.length <= 0) {
        $('#Address').css('background', 'yellow')
        $('#showError_Address').html('Địa chỉ không được trống!');
        $('#showError_Address').css('color', 'red');
        $('#showError_Address').css('font-weight', 'bold');
        return false;
    }
    else {
        $('#Address').css('background', 'white')
        $('#showError_Address').html('');
        return true;
    }
});
$('#Address').keyup(function () {
    var address = $('#Address').val();
    if (address.length <= 0) {
        $('#Address').css('background', 'yellow')
        $('#showError_Address').html('Địa chỉ không được trống!');
        $('#showError_Address').css('color', 'red');
        $('#showError_Address').css('font-weight', 'bold');
        return false;
    }
    else {
        $('#Address').css('background', 'white')
        $('#showError_Address').html('');
        return true;
    }
});
$('#PhoneNumber').keyup(function () {
    var phonenumber = $('#PhoneNumber').val();
    var vnf_regex = /((09|03|07|08|05)+([0-9]{8})\b)/g;
    if (!(vnf_regex.test(phonenumber))) {
        $('#PhoneNumber').css('background', 'yellow')
        $('#showError_PhoneNumber').html('Hãy nhập số điện thoại hợp lệ, VD:03xxxxxxxx!');
        $('#showError_PhoneNumber').css('color', 'red');
        $('#showError_PhoneNumber').css('font-weight', 'bold');
        return false;
    }
    else {
        $('#PhoneNumber').css('background', 'white')
        $('#showError_PhoneNumber').html('');
        return true;
    }
});