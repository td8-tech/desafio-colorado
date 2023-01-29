$(function () {
    GetCliente().then(loadClientes);
});

function loadClientes(lstClientes) {
    const rows = lstClientes.map(criarLinha);
    $('#clientTable tbody').append(rows);
}

$('#filterForm').on('submit', async function (e) {
    e.preventDefault();
    let formData = new FormData(this);
    let filteredData = await getFilteredData(formData);
    updateTable(filteredData);
});

$("#clearFilters").on("click", function () {
    $("#filterForm input").val("");
    $("#filterForm select").prop('selectedIndex', 0);
});

function updateTable(lstClientes) {
    $('#clientTable tbody').empty();
    const rows = lstClientes.map(criarLinha);
    $('#clientTable tbody').append(rows);
}

async function getFilteredData(formData) {
    console.log(formData)
    let nameFilter = formData.get('nameFilter');
    let addressFilter = formData.get('addressFilter');
    let cityFilter = formData.get('cityFilter');
    let stateFilter = formData.get('stateFilter');
    let dateFilter = formData.get('dateFilter');

    let lstClientes = await GetCliente();
    return lstClientes.filter(x => (nameFilter ? x.nome === nameFilter : true)
        && (addressFilter ? x.endereco === addressFilter : true)
        && (cityFilter ? x.cidade === cityFilter : true)
        && (stateFilter ? x.uf === stateFilter : true)
        && (dateFilter ? moment(moment(x.dataInsercao).format('YYYY-MM-DD')).isSame(moment(dateFilter).format('YYYY-MM-DD')) : true)
        );
}


function criarLinha(cliente) {
    return `<tr>
                <td>${cliente.id}</td>
                <td>${cliente.nome}</td>
                <td>${cliente.endereco}</td>
                <td>${cliente.cidade}</td>
                <td>${cliente.uf}</td>
                <td>${new Date(cliente.dataInsercao).toLocaleString()}</td>
                <td>
                    <a href="/Cliente/Index?id=${cliente.id}" class="btn edit-btn"><i class="fa-solid fa-pen-to-square"></i></a>
                    <a href="#" data-toggle="modal" data-target="#exampleModal" class="btn delete-btn" value="${cliente.id}"><i class="fas fa-trash-alt"></i></a>
                </td>
            </tr>`
}

$(document).on("click", ".delete-btn", function () {
    if (confirm('Realmente deseja excluir ?')) {
        var currentRow = $(this).closest("tr");
        apagarCliente($(this).attr('value'))
        currentRow.remove();
    }  
    
});
