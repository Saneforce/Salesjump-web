function fillData1(xldata){				
                type = $('#ddlType').val();
                $.ajax({
                    type: "POST",
                    contentType: "application/json; charset=utf-8",
                    async: false,
                    url: "Scheme_Master.aspx/getProduct",
                    data: "{'Type':'" + type + "'}",
                    dataType: "json",
                    success: function (data) {
                        ProDtl = data.d;
                    },
                    error: function (jqXHR, exception) {
                        console.log(jqXHR);
                        console.log(exception);
                    }
                });
                tbl = $('#tbl');
                $(tbl).find('thead tr').remove();
                $(tbl).find('tbody tr').remove();

                var ofPro = ProDtl.filter(item => item.pTypes == "O");
                ProDtl = ProDtl.filter(item => item.pTypes != "O");
                str = '<th>Product Name</th><th>Scheme </th><th>Free</th><th>Discount%</th><th>Package Offer</th><th>Against Product</th><th>Offer Product</th><th>Add</th>';
                $(tbl).find('thead').append('<tr>' + str + '</tr>');

                var ofStr = `<option value="0">Select</option>`;
                if (ofPro.length > 0) {
                    ofPro.forEach((obj) => ofStr += `<option value=${obj.pCode}>${obj.pName}</option>`);
                }
                var prdCodeex = '';
                var maids = 0,mid=0;
                xldata.forEach((element, index, array) => {
                var pkg = '';
                var agp = '';
                var agPro = element.OfferProduct;
                   

               
                    maids = Number(mid)+1;
                    mid++;
                console.log(maids);
                if (element.Package == 'Y') {
                    pkg = 'checked';
                }
                if (element.Against == 'Y') {
                    agp = 'checked';
                }

                if (prdCodeex != element.ProductCode) {
                    str = `<td style="vertical-align: middle;padding: 2px 2px;"> <input type="hidden" class="AutoID" name="AutoID" value=${maids} /><input type="hidden" name="pCode" value=${element.ProductCode} />   <span>${element.ProductName} </span></td>`;
                    str += `<td style="padding: 2px 2px; vertical-align: middle;"><input type="text" class="form-control " id="txtScheme" name="txtScheme" value="${ element.Scheme }" /></td><td style="padding: 2px 2px;    vertical-align: middle;"><input type="text" class="form-control " id="txtFree" name="txtFree"  value=${element.Free} /></td><td style="padding: 2px 2px;    vertical-align: middle;"><input type="text" class="form-control " id="txtDiscount" name="txtDiscount"  value=${element.Discount} /></td><td style="text-align: center;padding: 2px 2px;"><p class="onoff"><input type="checkbox" class="packages" ${pkg} value="1" id="checkboxID${index + 1}"/><label class="packageslbl" for="checkboxID${index + 1}"></label></p></td>`;
                    str += `<td style="text-align: center;padding: 2px 2px;"><p class="onoff"><input type="checkbox" ${agp} class="against"  value="1" id="against${(index + 1)}"/><label for="against${(index + 1)}" class="againstlbl"></label></p></td>`;
                    str += `<td style="padding: 2px 2px; vertical-align: middle;"><select class="form-control" disabled="disabled" name="allType" >${ofStr}</select></td><td style="padding: 2px 2px; vertical-align: middle;""><button type="button"  class="btn btn-default addpro btn-md"><span class="glyphicon glyphicon-plus  " style="color:#378b2c"></span></button></td>`;
                    $(tbl).find('tbody').append('<tr>' + str + '</tr>');

                    if (element.Against == 'Y') {
                        $(tbl).find('tr').eq(index+1).find('select[name="allType"]').prop('disabled', '');
                    }
                    else {
                        $(tbl).find('tr').eq(index+1).find('select[name="allType"]').prop('disabled', 'disabled');
                    }
                    //$(tbl).find('tr').eq(index+1).find('select[name="allType"] :selected').text(agPro);

					 $(tbl).find('tr').eq(index+1).find('select[name="allType"] option').each(function () {
            	    if ($(this).text().toLowerCase() == agPro.toLowerCase()) {
                    this.selected = true;
                    return;
               		 }});
                    prdCodeex = element.ProductCode;
                    }
                    else {

                    str = `<td style="vertical-align: middle;padding: 2px 2px; text-align:center"><input type="hidden" class="AutoID" name="AutoID" value=${maids} /> <input type="hidden" name="pCode" value=${element.ProductCode} />   <span>"</span></td>`;
                    str += `<td style="padding: 2px 2px; vertical-align: middle;"><input type="text" class="form-control " id="txtScheme" name="txtScheme" value=${element.Scheme} /></td><td style="padding: 2px 2px;    vertical-align: middle;"><input type="text" class="form-control " id="txtFree" name="txtFree"  value=${element.Free} /></td><td style="padding: 2px 2px;    vertical-align: middle;"><input type="text" class="form-control " id="txtDiscount" name="txtDiscount"  value=${element.Discount} /></td><td style="text-align: center;padding: 2px 2px;"><p class="onoff"><input type="checkbox" class="packages" ${pkg} value="1" id="checkboxID${index + 1}"/><label class="packageslbl" for="checkboxID${index + 1}"></label></p></td>`;
                    str += `<td style="text-align: center;padding: 2px 2px;"><p class="onoff"><input type="checkbox" ${agp} class="against"  value="1" id="against${(index + 1)}"/><label for="against${(index + 1)}" class="againstlbl"></label></p></td>`;
                    str += `<td style="padding: 2px 2px; vertical-align: middle;"><select class="form-control" disabled="disabled" name="allType" >${ofStr}</select></td><td style="padding: 2px 2px; vertical-align: middle;""><button type="button"  class="btn btn-default delpro btn-md"><span class="glyphicon glyphicon-minus  " style="color:#378b2c"></span></button></td>`;
                    $(tbl).find('tbody').append('<tr class="oldrow">' + str + '</tr>');

                    if (element.Against == 'Y') {
                        $(tbl).find('tr').eq(index+1).find('select[name="allType"]').prop('disabled', '');
                    }
                    else {
                        $(tbl).find('tr').eq(index+1).find('select[name="allType"]').prop('disabled', 'disabled');
                    }
                   // $(tbl).find('tr').eq(index+1).find('select[name="allType"] :selected').text(agPro);
					$(tbl).find('tr').eq(index+1).find('select[name="allType"] option').each(function () {
            	    if ($(this).text().toLowerCase() == agPro.toLowerCase()){
                    this.selected = true;
                    return;
               		 }});
                    prdCodeex = element.pCode;
                    }

                    });
            $('.btnDelete').prop('disabled', '');
                    }
