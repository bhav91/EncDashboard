
function handleSelectChange(select) {
    showLoader();
    $.ajax({
        url: '/Loan/filterQuery', 
        type: 'GET',
        data: { view: select },
        success: function (data) {
            hideLoader();
            var SN = 1;
            var table = $('<table class="table table-hover"></table>');

            var thead = $('<thead class="thead-dark"></thead>');
            var tr = $('<tr></tr>');
            tr.append('<th scope="row">' + 'SN' + '</th>');
            $.each(data.columns, function (index, columnName) {
                tr.append('<th scope="row">' + columnName.split(":")[1] + '</th>');
            });
            thead.append(tr);
            table.append(thead);
            var tbody = $('<tbody></tbody>');
            $.each(data.loanRecords, function (index, rowData) {
                var row = $('<tr></tr>');
                row.append('<td>' + SN + '</td>');
                $.each(rowData.fields, function (key, value) {
                    data.columns.forEach((column) => {
                        if (column.split(":")[0].toUpperCase() == key.toUpperCase()) {
                            row.append('<td>' + value.split(" ")[0] + '</td>');
                        }
                    });
                   
                     
                });
                SN += 1;
                tbody.append(row);
            });
            SN = 0;
            table.append(tbody);
             
            $('#partialViewContainer').html(table);
            
        },
        error: function () {
            alert('An error occurred while loading the partial view.');
        }
    });
}

function showLoader() {
    // Show your loader here, for example:
    $('#loader').show();
}

function hideLoader() {
    // Hide your loader here, for example:
    $('#loader').hide();
}