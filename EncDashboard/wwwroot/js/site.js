
function handleSelectChange(select) {
    showLoader();
    $.ajax({
        url: '/Loan/filterQuery', // Replace with your actual URL
        type: 'GET',
        data: { view: select.value },
        success: function (data) {
            
            // Assuming data is an array of column names, e.g., ["Name", "Amount", "Date"]

            // Create the table element
            var table = $('<table class="table"></table>');

            // Create the table header
            var thead = $('<thead class="thead-dark"></thead>');
            var tr = $('<tr></tr>');
            $.each(data.columns, function (index, columnName) {
                tr.append('<th scope="row">' + columnName + '</th>');
            });
            thead.append(tr);
            table.append(thead);
            var tbody = $('<tbody></tbody>');
            $.each(data.loanRecords, function (index, rowData) {
                var row = $('<tr></tr>');
                $.each(rowData.fields, function (key, value) {
                    
                    row.append('<td>' +value+ '</td>');
                     
                 });
                 tbody.append(row);
             });
            table.append(tbody);
            // Replace the content of the specified div with the generated table
              
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