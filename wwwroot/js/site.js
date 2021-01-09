//Date picker
$('.datepicker').datepicker({
    autoclose: true
})

$("#addItem").click(function (e) {
    e.preventDefault();
    debugger;

    var itemId = $('select[name="ItemId"]')[$('select[name="ItemId"]').length - 1].value;
    var quantity = $('input[name="Quantity"]')[$('input[name="Quantity"]').length - 1].value;
    var data = { itemId: itemId, quantity: quantity };    

    $.ajax('/Orders/BlankItem', {
        method: 'post',
        contentType: 'application/x-www-form-urlencoded',        
        data: data,
        traditional: true,    
        success: function (html) {
            $("#editorRows").append(html);
        }
    });
    
    return false;
});
