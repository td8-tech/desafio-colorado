const ApiUrl = `${localStorage.getItem('ApiUrl')}/Cliente`;

$(async function () {
    $('#inputZip').mask('00000-000');
    let id = $('#idCliente').val()
    if (id != undefined && id !== "0") {
        let cliente = await GetClienteById(id);
        if (cliente != null) {
            
            $('#inputName').val(cliente.nome);
            $('#inputAddress').val(cliente.endereco);
            $('#inputAddress').val(cliente.endereco);
            $('#inputCity').val(cliente.cidade);
            $('#inputState').val(cliente.uf);
            $('#dataInsercao').val(cliente.dataInsercao);
        }
    }
});

async function findCep(cep) {
    $('#spinner-zip').show()
    let adress = await getAdressFromCep($(cep).val())
    if (adress != null && adress.erro == undefined) {
        $('#inputAddress').val(adress.logradouro);
        $('#inputCity').val(adress.localidade);
        $('#inputState').val(adress.uf);
    }
    $('#spinner-zip').hide()
}


$('#cadastroForm').on('submit', async function (e) {
    e.preventDefault();
    let formData = new FormData(this);
    formData.set('dataInsercao', new Date().toISOString());
    CadastrarCliente(Object.fromEntries(formData));
});

async function GetClienteById(id) {
    try {
        const response = await $.ajax({
            type: "GET",
            url: `${ApiUrl}/getbyid?id=${id}` ,
            dataType: "json",
            contentType: "application/json"
        });
        return response;
    } catch (error) {
        return error;
    }
}


async function GetCliente(id) {
    try {
        const response = await $.ajax({
            type: "GET",
            url: ApiUrl,
            dataType: "json",
            contentType: "application/json"
        });
        return response;
    } catch (error) {
        return error;
    }
}

async function CadastrarCliente(clienteModel) {
    try {
        const response = await $.ajax({
            type: clienteModel.id != 0 ? "PUT" : "POST",
            url: ApiUrl,
            data: JSON.stringify(clienteModel),
            dataType: "json",
            contentType: "application/json"
        });
        alert("success", response);
        window.location.href = '/';
    } catch (error) {
        alert("error", error);
    }
}

async function apagarCliente(id) {
    try {
        const response = await $.ajax({
            type: "DELETE",
            url: `${ApiUrl}/${id}`,
            dataType: "json",
            contentType: "application/json"
        });
        alert("Success");
    } catch (error) {
        console.log(error)
        alert("Error: " + error);
    }
}
