function handleSearchKeypress(e) {
    if (e.keyCode == 13) {
        searchProductsWithName($("#productSearch").val());
    }
}

$(function() {
    var timer;
    $("#productSearch").keyup(function() {
        clearTimeout(timer);
        var ms = 300; // milliseconds
        var val = this.value;
        timer = setTimeout(function() {
            searchProductsWithName(val);
        }, ms);
    });
});

function searchProductsWithName(name) {
    $.ajax({
        type: "POST",
        url: "/Products/Search",
        data: { search: name },
        success: function (data) {
            $('#searchResults tbody').empty();
            $('#searchResults tbody').append(data);
            
            $("#noProducts").hide();
            $("#searchResults").show();
        },
        statusCode: {
            204: function () { //No content
                $("#searchResults").hide();
                $("#noProducts").show();
            }
        }
    });
}

function addSearchResult(productId) {
    sendAdditionRequest(productId);
}

function sendAdditionRequest(productId) {
    var invoiceId = $("#entity-id").data("entity-id");
    $.ajax({
        type: "POST",
        url: "/Invoices/AddProduct",
        data: { invoiceId: invoiceId, productId: productId },
        success: function (data) {
            renderProducts();
            updateInvoiceTotal();
        }
    });
}

function validateInvoice() {
    $.ajax({
        type: "POST",
        url: "/Invoices/Validate",
        data: {id: invoiceId},
        success: function () {
            $("#btnValidate")
                .attr("id", "btnInvalidate")
                .text("Invalidate")
                .click(invalidateInvoice)
                .attr("class", "btn btn-danger");
            $("#searchGroup").hide();
        }
    });
}

function invalidateInvoice() {
    $.ajax({
        type: "POST",
        url: "/Invoices/Invalidate",
        data: {id: invoiceId},
        success: function () {
            $("#btnInvalidate")
                .attr("id", "btnValidate")
                .text("Validate")
                .click(validateInvoice)
                .attr("class", "btn btn-success");
            $("#searchGroup").show();
        }  
    });
}

function removeProduct(productId) {
    var invoiceId = $("#entity-id").data("entity-id");
    $.ajax({
        type: "POST",
        url: "/Invoices/RemoveProduct",
        data: { invoiceId: invoiceId, productId: productId },
        success: function () {
            $(this).parent().parent().remove();
            renderProducts();
            updateInvoiceTotal();
        }
    });
}

function updateInvoiceTotal() {
    var invoiceId = $("#entity-id").data("entity-id");
    $.ajax({
        type: "POST",
        url: "/Invoices/" + invoiceId + "/Total",
        success: function (data) {
            $("#invoiceTotal").text("Total: " + data.invoiceTotal.toFixed(2));
        }  
    });
}

function renderProducts() {
    var invoiceId = $("#entity-id").data("entity-id");
    $.ajax({
        type: "POST",
        url: "/Invoices/" + invoiceId + "/Products",
        success: function (data) {
            $('#invoiceProducts tbody').empty();
            $('#invoiceProducts tbody').append(data);
        }  
    });
}

$(document).ready(function () {
    updateInvoiceTotal();
    renderProducts();

    $("#btnInvalidate").on("click", function (){
        invalidateInvoice();
    });
    $("#btnValidate").on("click", function (){
        validateInvoice();
    });
    $("#btnProductSearch").click(function () {
        searchProductsWithName($("#productSearch").val());
    });
});