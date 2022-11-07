let $payBtn = $("#order_btn");
let products;
let billInput = $("#EDP_BILL_NO");
let amountInput = $("#EDP_AMOUNT");
let $onlineForm = $("#onlineForm");
$(document).ready(function () {
    let ids = baskets.GetIds();
    $.ajax({
        type: "POST",
        url: "/Product/GetProductsByIdList",
        dataType: 'text',
        beforeSend: function (xhr) { // for ASP.NET auto deserialization
            xhr.setRequestHeader("Content-type", "application/json");
        },
        data: JSON.stringify({
            idList: baskets.GetIds()
        }),
        statusCode: {
            401: function () {
                location.reload();
            }
        },
        success: function (res, textStatus, xhr) {
            if (textStatus == "success") {
                let list = JSON.parse(res);
                products = list;
                if (list.length < 1) {
                    return;
                }
                let html = '';
                let k = 0;
                for (let i in baskets.ProductList) {
                    let product = baskets.ProductList[i];
                    let found = false;
                    let normalCount = false;
                    let countUpdate = false;
                    let foundProduct = list.filter(r=>r.id == product.Id);
                    if (foundProduct.length > 0) {
                        found = true;
                        product = foundProduct[0]; // real product
                        
                        if (product.count < baskets.ProductList[i].Count) {
                            baskets.ProductList[i].Count = product.count;
                            baskets.SaveList();
                            countUpdate = true;
                        }
                    }
                    
                   normalCount = baskets.ProductList[i].Count > 0;
                    
                        
                    html += ''
                        + '<tr id="curProduct' + product.id + '" data-id="' + product.id + '" class="productItems '+(found && normalCount ? '' : 'outStock')+'">'
                        + '<td>'
                        + '<div class="product-image">'
                        + (found && normalCount ? '<input type="hidden" value=' + product.price + ' name="OrderedProducts[' + k + '].Price">' : '')
                        + '<img src="' + product.mainImage.fullPath + '" alt="">'
                        + '</div>'
                        + '<div class="product-title">' + product.translation.nameTranslation + '</div>'
                        + '  </td>'
                        + '  <td>' + product.sku + '</td>'
                        + '  <td><span class="price">' + product.price + '</span><span>  <i class="dram" >AMD</i></span></td>'
                        + '<input type="hidden" id="basketProductPrice' + product.id + '" value="' + product.price + '">'
                        + '  <td class="col-md-2">'
                        + '      <div class="input-group">'
                        + '          <span class="input-group-btn">'
                        + '              <button data-id="' + product.id + '" type="button" class="btn btn-default btn-number '+(found && normalCount ? 'remProductNum' : '')+'" data-type="minus" data-field="quant[1]">'
                        + '                  <span class="glyphicon glyphicon-minus"></span>'
                        + '              </button>'
                        + '          </span>'
                        + (found && normalCount ? '<input type="hidden" id="curItemHiddenCount' + product.id + '" name="OrderedProducts[' + k + '].Count" value="' + baskets.GetNum(product.id) + '"/>' : '')
                        + (found && normalCount? '<input type="hidden" name="OrderedProducts[' + k + '].Id" value="' + product.id + '"/>' : '')
                        + '          <input data-id="' + product.id + '" id="basketProductNum' + product.id + '" type="text" name="quant[1]" class="backetNumInput form-control input-number" value="' + baskets.GetNum(product.id) + '" min="1" max="10">'
                        + '              <span class="input-group-btn">'
                        + '                  <button data-id="' + product.id + '" type="button" class="btn btn-default btn-number '+(found && normalCount ? 'addProductNum' : '')+'" data-type="plus" data-field="quant[1]">'
                        + '                      <span class="glyphicon glyphicon-plus"></span>'
                        + '                  </button>'
                        + '              </span>'
                        + '      </div>'
                        + '  </td>'
                        + '      <td>'
                        + '          <span class="together" id="basketProductTotalPrice' + product.id + '">0 <i class="dram" >AMD</i></span>'
                        + '      </td>'
                        + '      <td>'
                        + '          <a href="#" class="remove-item remProduct" data-id="' + product.id + '"><span class="glyphicon glyphicon-remove"></span></a>'
                        + '      </td>'
                        + '</tr>'


                    if (found && normalCount)
                        k++;

                }
                $(".basketBody").html(html);

                baskets.BindEvents();
            }
        },
        error: function (xhr, textStatus, err) {
            // disable loader
            self.loaderWidget = _core.getService('Loading').Service;
            self.loaderWidget.disableBlur(self.divClass);

            callback(null);
        }
    });
});
$payBtn.click(function () {
    let onlineCheck = $("#onlineCheck");
    let $oflineForm = $("#payOflineForm");
    let isChecked = onlineCheck[0].checked;
    if (!isChecked) {
        $oflineForm.submit();
    }
    else {
        const form = document.querySelector('#payOflineForm');
        let data = Object.fromEntries(new FormData(form).entries());
        let current_products = baskets.ProductList;
        data.OrderedProducts = [];
        current_products.forEach(function(item) {
            data.OrderedProducts.push({
                id: item.Id,
                Count: item.Count,
            })
        })
        //data.OrderedProducts = products;
        console.log('posting', data);
        $.ajax({
            type: "POST",
            url: "/test/pay/online",
            data: { model: data },
            success: function (r) {
                if (r.error) {
                    alert(r.error);
                    return;
                }
                amountInput.val(r.amount);
                billInput.val(r.billId);
                $onlineForm.submit();
            }
        });
    }
});