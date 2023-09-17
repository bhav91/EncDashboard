function handleSelectChange(select) {
    // When an option is selected, trigger form submission
    alert(select.value)
    document.getElementById("viewsForm").submit();
}