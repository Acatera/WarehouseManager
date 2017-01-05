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

function addSearchResult(product) {
    var html = "<tr>";
    html += "<td>" + product["ProductName"] + "</td>";
    html += "<td>" + product["Price"] + "</td>";
    html += "<td><button id=\"btnAddProductId" + product["ProductId"] + "\" class=\"btn btn-warning btn-sm\">Add</button></td>";
    html += "</tr>";

    $("#productsTable").append(html);
    $("#btnAddProductId" + product["ProductId"]).on("click", function () {
        var productId = this.id;
        productId = productId.replace("btnAddProductId", "");
        console.log("Product id is " + productId);
        sendAdditionRequest(productId);
    });
}

function searchProductsWithName(name) {
    $.ajax({
        type: "POST",
        url: "/Products/Search",
        data: { search: name },
        success: function (data) {
            $("#productsTable").find("tr:gt(0)").remove();
            if (data.length === 0) {
                $("#searchResults").hide();
                $("#noProducts").show();
            } else {
                $("#noProducts").hide();
                $("#searchResults").show();
                for (var i = 0; i < data.length; i++) {
                    addSearchResult(data[i]);
                }
            }
        }
    });
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
            //alert("Invoice validated. Additional alterations are not permitted.");
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
            //alert("Invoice invalidated. Alterations are now permitted.");
        }  
    });
}

function removeProduct(productId) {
    var invoiceId = $("#entity-id").data("entity-id");
    var dataId = $(this).data("product-id");
    console.log("invoiceId:" + invoiceId);
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