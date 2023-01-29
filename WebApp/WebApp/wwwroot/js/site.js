// Please see documentation at https://docs.microsoft.com/aspnet/core/client-side/bundling-and-minification
// for details on configuring this project to bundle and minify static web assets.

// Write your JavaScript code.
localStorage.setItem('ApiUrl', $('#ApiUrl').val());

$(function () {
    if ($('.inputState').length > 0) {
        getStates().then(states => {
            states.map(addToStateDropDown)
        })
    }

    //var modal = $('#confirmModalContainer .modal');
    //modal.modal({
    //    backdrop: 'static',
    //    keyboard: false
    //});

    //modal.on('shown.bs.modal', function () {
    //    $('#confirmButton').focus();
    //});

    //$('#confirmButton').click(function () {
    //    // Your code here
    //});

});

function addToStateDropDown(state) {
    $('.inputState').append(
        $('<option></option>').val(state.sigla).html(state.nome)
    );
}

