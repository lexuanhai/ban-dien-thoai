(function ($) {
    var self = this;
    self.Data = [];
    self.ProductImages = [];
    self.ProductServerImages = [];
    self.ProductUpdateImage = [];

    self.IsUpdate = false;
    // quantity start
    self.ProductIdQuantity = 0;
    self.Sizes = [];
    self.ListUpdateQuantity = [];
    self.ListDeletedQuantity = [];
    self.Colors = [];
    self.QuantityEnity = {
        product_id:0,
        color_id: 0,
        totalimport: 0,
        priceimprot: 0,
        pricesell: 0,
        totalsell: 0,
        capacity:""
    };
    // quantity end
    self.Product = {
        id: null,
        name: null,
        category_id: "",
        avatar: "",
        price: "",
        color: "",
        quantity: "",
        short_desc: "",
        description: "",
        specifications: "",
        status: "",
        endow: "",
        type: "",
        differentiate: "",
        promotion: "",
        commodities: ""
    }
    self.ProductSearch = {
        name: "",
        role: null,
        PageIndex: tedu.configs.pageIndex,
        PageSize: tedu.configs.pageSize
    }
    self.lstRole = [];

    self.addSerialNumber = function () {
        var index = 0;
        $("table tbody tr").each(function (index) {
            $(this).find('td:nth-child(1)').html(index + 1);
        });
    };
    self.Files = {};

    self.RenderTableHtml = function (data) {
        var html = "";
        if (data != "" && data.length > 0) {
            var index = 0;
            for (var i = 0; i < data.length; i++) {
                debugger
                var item = data[i];
                html += "<tr>";
                html += "<td>" + (++index) + "</td>";
                //if (item.avatar != null) {
                //    html += "<td> <img src=/product-image/" + item.avatar + " class=\"item-image\" /></td>";
                //} else {
                //    html += "<td> <img src=/image-default/default.png class=\"item-image\" /></td>";
                //}
                html += "<td>" + (item.Product != null && item.Product.name != "" ?item.Product.name:"") + "</td>";
                html += "<td>" + (item.capacity != null ? item.capacity : "") + "</td>";
                html += "<td>" + (item.Colors != null && item.Colors.name != "" ? item.Colors.name : "") + "</td>";
                html += "<td>" + item.priceimprotstr + "</td>";
                html += "<td>" + item.pricesellstr + "</td>";
                html += "<td>" + (item.totalimport != null ? item.totalimport:"") + "</td>";
                html += "<td>" + (item.totalsell != null ? item.totalsell:"") + "</td>";
                html += "<td>" + (item.totalinventory != null ? item.totalinventory:"" ) + "</td>";
                html += "<td style=\"text-align: center;\">" +

                    //(item.status == 0 ? "<button  class=\"btn btn-dark custom-button\" onClick=UpdateStatus(" + item.id + ",1)><i class=\"bi bi-eye custom-icon\"></i></button>" : "<button  class=\"btn btn-secondary custom-button\" onClick=UpdateStatus(" + item.id + ",0)><i class=\"bi bi-eye-slash custom-icon\"></i></button>") +
                    "<button  class=\"btn btn-success custom-button\" onClick=\"AddQuantityView(" + item.Product.id + ")\"><i  class=\"bi bi-calculator custom-icon\"></i></button>" +
                    "<button  class=\"btn btn-primary custom-button\" onClick=\"UpdateView(" + item.id + ")\"><i  class=\"bi bi-pencil-square custom-icon\"></i></button>" +                   
                    "<button  class=\"btn btn-danger custom-button\" onClick=\"Deleted(" + item.id + ")\"><i  class=\"bi bi-trash custom-icon\"></i></button>" +

                    "</td>";

                html += "</tr>";
            }
        }
        else {
            html += "<tr><td colspan=\"10\" style=\"text-align:center\">Không có dữ liệu</td></tr>";
        }
        $("#tblData").html(html);
    };

    // Quantity start
    self.InitQuantity = function () {
        self.GetColor();
       /* self.GetSize();*/
        $(".btn-create-row-quantity").click(function () {
            var html = self.AddRowHtml();
            $("#quantity #tblData").append(html);
            $(".no-data").hide();
        });

        self.ValidateQuanity();
    }
  
    self.DeletedHtml = function (tag) {
        $(tag).closest(".new").remove();
    }
    self.AddQuantityView = function (productId) {
        window.location.href = "/admin/tao-moi-so-luong/?productId=" + productId;
    }
   
    self.GetColorByIdOrAll = function (colorId, isRenderAPI) {
        var html = "";
        //if (isRenderAPI) {
        //    html = "<select class=\"form-select colors\" required disabled><option value=\"\"> Chọn màu </option>";
        //}
        //else {
        //    html = "<select class=\"form-select colors\" required><option value=\"\"> Chọn màu </option>";
        //}
        if (self.Colors != null && self.Colors.length > 0) {
            for (var i = 0; i < self.Colors.length; i++) {
                var item = self.Colors[i];
                if (item.id == colorId) {
                    html += "<option value=\"" + item.id + "\" selected>" + (item.name != "" ? item.name : (item.code != "" ? item.code : "")) + "</option>";
                }
                else {
                    html += "<option value=\"" + item.id + "\">" + (item.name != "" ? item.name : (item.code != "" ? item.code : "")) + "</option>";
                }
            }
        }
        html += "</select>";
        return html;
    }

    self.GetSizeByIdOrAll = function (sizeId, isRenderAPI) {
        var html = "";
        if (isRenderAPI) {
            html = "<select class=\"form-select sizes\" required disabled><option value=\"\"> Chọn kích thước </option>";
        }
        else {
            html = "<select class=\"form-select sizes\" required><option value=\"\"> Chọn kích thước </option>";
        }
        if (self.Sizes != null && self.Sizes.length > 0) {
            for (var i = 0; i < self.Sizes.length; i++) {
                var item = self.Sizes[i];
                if (item.id == sizeId) {
                    html += "<option value=\"" + item.id + "\" selected>" + (item.name != "" ? item.name : (item.code != "" ? item.code : "")) + "</option>";
                }
                else {
                    html += "<option value=\"" + item.id + "\">" + (item.name != "" ? item.name : (item.code != "" ? item.code : "")) + "</option>";
                }
            }
        }
        html += "</select>";
        return html;
    }

    self.GetColor = function () {
        $.ajax({
            url: '/Admin/Colors/GetAll',
            type: 'GET',
            dataType: 'json',
            beforeSend: function () {
            },
            complete: function () {
            },
            success: function (response) {

                if (response.Data != null && response.Data.length > 0) {
                    self.Colors = response.Data;
                    var html = self.GetColorByIdOrAll(0);
                    $("#color_id").append(html);
                }
            }
        });
    };

    self.ValidateQuanity = function () {

        $("#form-submit").validate({
            rules:
            {
                color_id: {
                    required: true,
                },
                totalimport: {
                    required: true,
                },
                priceimprot: {
                    required: true
                },
                totalsell: {
                    required: true
                }
            },
            messages:
            {
                color_id: {
                    required: "Bạn chưa chọn màu sắc",
                },
                totalimport: {
                    required: "Bạn chưa nhập giá nhập",
                },
                priceimprot: {
                    required: "Bạn chưa nhập giá bán nhập",
                },
                totalsell: {
                    required: "Bạn chưa nhập dung lượng lưu trữ",
                },
            },
            submitHandler: function (form) {
                self.GetValue();
                if (self.IsUpdate) {
                    self.Update(self.Product);
                    if (self.ProductImages != null && self.ProductImages != "") {
                        self.UploadFileImageProduct(self.Product.id);
                    }
                    if (self.ProductUpdateImage != null && self.ProductUpdateImage.length > 0) {
                        self.RemoveImageServer(self.ProductUpdateImage);
                    }
                }
                else {
                    self.AddQuantity(self.QuantityEnity);
                }
            }
        });
    }

    self.AddQuantity = function (_quantities) {
       
        $.ajax({
            url: '/Admin/ProductQuantity/Add',
            type: 'POST',
            dataType: 'json',
            data: {
                quantities: _quantities
            },
            beforeSend: function () {
                //Loading('show');
            },
            complete: function () {
                //Loading('hiden');
            },
            success: function (response) {
                if (response.success) {
                    if (self.ProductImages != null && self.ProductImages != "") {
                        self.UploadFileImageProduct(response.id);
                    }
                    tedu.notify('Thêm mới dữ liệu thành công', 'success');
                    //self.GetDataPaging(true);
                    //window.location.href = '/admin/quan-ly-san-pham';
                }
                else {
                    if (response.isNameExist) {
                        tedu.notify('Tên đã tồn tại', 'error');
                        //$(".product-name-exist").show().text("Tên đã tồn tại");
                    }
                }
            }
        })

    }
    self.UpdateServerQuantity = function (_quantities) {
        $.ajax({
            url: '/Admin/ProductQuantity/Update',
            type: 'POST',
            dataType: 'json',
            data: {
                quantities: _quantities
            },
            beforeSend: function () {
                //Loading('show');
            },
            complete: function () {
                //Loading('hiden');
            },
            success: function (response) {
                if (response.success) {
                    tedu.notify('Cập nhật dữ liệu thành công', 'success');
                    if (self.ProductIdQuantity > 0) {
                        self.Quantity(self.ProductIdQuantity);
                    }
                    //window.location.href = '/admin/quan-ly-san-pham';
                }
                else {
                    tedu.notify('Cập nhật dữ liệu không thành công', 'error');
                }
            }
        })
    }
    self.DeletedServerQuantity = function (quantities) {
        $.ajax({
            url: '/Admin/ProductQuantity/Deleted',
            type: 'POST',
            dataType: 'json',
            data: {
                ids: quantities
            },
            beforeSend: function () {
                //Loading('show');
            },
            complete: function () {
                //Loading('hiden');
            },
            success: function (response) {
                if (response.success) {
                    tedu.notify('Xóa dữ liệu thành công', 'success');
                    //window.location.href = '/admin/quan-ly-san-pham';
                }
                else {
                    tedu.notify('Cập nhật dữ liệu không thành công', 'error');
                }
            }
        })
    }
    self.GetValue = function () {
        self.QuantityEnity.product_id = $("#product_id").val();
        self.QuantityEnity.color_id = $("#color_id").val();
        self.QuantityEnity.totalimport = $("#totalimport").val();
        self.QuantityEnity.priceimprot = $("#priceimprot").val();
        self.QuantityEnity.pricesell = $("#pricesell").val();
        self.QuantityEnity.capacity = $("#capacity").val();
        
    }
    // Quantity end

    self.UpdateView = function (id) {
        if (id != null && id != "") {
            //$(".btn-submit-format").text("Cập nhật");
            //$(".product-title").text("Cập nhật sản phẩm");
            $(".custom-format").attr("disabled", "disabled");
            self.GetById(id, self.RenderHtmlByObject);
            self.Product.id = id;

            $(".product-update").show();
            $(".product-list").hide();
            self.IsUpdate = true;
        }
    }
    self.Quantity = function (id) {
        self.GetProductQuantityForProductId(id);
        self.ProductIdQuantity = id;
    }

    self.GetById = function (id, renderCallBack) {
        if (id != null && id != "") {
            $.ajax({
                url: '/Admin/Product/GetById',
                type: 'GET',
                dataType: 'json',
                data: {
                    id: id
                },
                beforeSend: function () {
                },
                complete: function () {
                },
                success: function (response) {
                    if (response.Data != null) {
                        renderCallBack(response.Data);
                        self.Id = id;

                    }
                }
            })
        }
    }

    self.UpdateStatus = function (id, status) {
        $.ajax({
            url: '/Admin/Product/UpdateStatus',
            type: 'GET',
            dataType: 'json',
            data: {
                id: id,
                status: status
            },
            beforeSend: function () {
                //Loading('show');
            },
            complete: function () {
                ////Loading('hiden');
            },
            success: function (response) {
                if (response.success) {
                    //self.GetImageByProductId(id);
                    self.GetDataPaging(true);
                    tedu.notify('Cập nhật trạng thái thành công', 'success');
                }
            }
        })
    }

    self.WrapPaging = function (recordCount, callBack, changePageSize) {
        var totalsize = Math.ceil(recordCount / tedu.configs.pageSize);
        //Unbind pagination if it existed or click change pagesize
        if ($('#paginationUL a').length === 0 || changePageSize === true) {
            $('#paginationUL').empty();
            $('#paginationUL').removeData("twbs-pagination");
            $('#paginationUL').unbind("page");
        }
        //Bind Pagination Event
        $('#paginationUL').twbsPagination({
            totalPages: totalsize,
            visiblePages: 7,
            first: '<<',
            prev: '<',
            next: '>',
            last: '>>',
            onPageClick: function (event, p) {
                tedu.configs.pageIndex = p;
                setTimeout(callBack(), 200);
            }
        });
    }
    self.Deleted = function (id) {
        if (id != null && id != "") {
            tedu.confirm('Bạn có chắc muốn xóa sản phẩm này?', function () {
                $.ajax({
                    type: "POST",
                    url: "/Admin/Product/Delete",
                    data: { id: id },
                    beforeSend: function () {
                        // tedu.start//Loading();
                    },
                    success: function () {
                        tedu.notify('Đã xóa thành công', 'success');
                        //tedu.stop//Loading();
                        //loadData();
                        self.GetDataPaging(true);
                    },
                    error: function () {
                        tedu.notify('Has an error', 'error');
                        tedu.stop//Loading();
                    }
                });
            });
        }
    }


    self.GetDataPaging = function (isPageChanged) {

        self.ProductSearch.PageIndex = tedu.configs.pageIndex;
        self.ProductSearch.PageSize = tedu.configs.pageSize;

        $.ajax({
            url: '/Admin/ProductQuantity/GetAllPaging',
            type: 'GET',
            data: self.ProductSearch,
            dataType: 'json',
            beforeSend: function () {
                //Loading('show');
            },
            complete: function () {
                //Loading('hiden');
            },
            success: function (response) {
                self.RenderTableHtml(response.data.Results);
                $('#lblTotalRecords').text(response.data.RowCount);
                if (response.data.RowCount != null && response.data.RowCount > 0) {
                    self.WrapPaging(response.data.RowCount, function () {
                        GetDataPaging();
                    }, isPageChanged);
                }

            }
        })

    };



    // Set value default
    self.SetValueDefault = function () {
        self.Product.Id = null;
        $("#fullname").val("").attr("placeholder", "Nhập tên người dùng");
        $("#mobile").val("").attr("placeholder", "Nhập số điện thoại");
        $("#birthday").val("").attr("placeholder", "Ngày sinh");
        $("#email").val("").attr("placeholder", "Email");
        $("#username").val("").attr("placeholder", "Tên đăng nhập");
        $("#password").val("").attr("placeholder", "Mật khẩu");
        $("#address").val("").attr("placeholder", "Địa chỉ");
        $("#confirm_password").val("").attr("placeholder", "Nhập lại mật khẩu");
        $(".box-avatar").css("display", "none");
    }
    // Get User

    //self.AddProduct = function (userView) {
    //    $.ajax({
    //        url: '/Admin/Product/Add',
    //        type: 'POST',
    //        dataType: 'json',
    //        data: {
    //            ProductModelView: userView
    //        },
    //        beforeSend: function () {
    //            //Loading('show');
    //        },
    //        complete: function () {
    //            //Loading('hiden');
    //        },
    //        success: function (response) {
    //            if (response.success) {
    //                if (self.ProductImages != null && self.ProductImages != "") {
    //                    self.UploadFileImageProduct(response.id);
    //                }
    //                tedu.notify('Thêm mới dữ liệu thành công', 'success');
    //                self.GetDataPaging(true);
    //                window.location.href = '/admin/quan-ly-san-pham';
    //            }
    //            else {
    //                if (response.isNameExist) {
    //                    tedu.notify('Tên đã tồn tại', 'error');
    //                    //$(".product-name-exist").show().text("Tên đã tồn tại");
    //                }
    //            }
    //        }
    //    })
    //}

    //self.Update = function (userView) {
    //    $.ajax({
    //        url: '/Admin/Product/Update',
    //        type: 'POST',
    //        dataType: 'json',
    //        data: {
    //            ProductModelView: userView
    //        },
    //        beforeSend: function () {
    //            //Loading('show');
    //        },
    //        complete: function () {
    //            //Loading('hiden');
    //        },
    //        success: function (response) {
    //            if (response.success) {
    //                tedu.notify('Cập nhật dữ liệu thành công', 'success');
    //                self.GetDataPaging(true);
    //                window.location.href = '/admin/quan-ly-san-pham';
    //            }

    //        }
    //    })
    //}

    self.GetAllCategories = function () {
        $.ajax({
            url: '/Admin/Category/GetAll',
            type: 'GET',
            dataType: 'json',
            beforeSend: function () {
                Loading('show');
            },
            complete: function () {
                Loading('hiden');
            },
            success: function (response) {
                var html = "<option value =\"\">Chọn danh mục sản phẩm</option>";
                var htmlSearch = "<option value =\"\">Xem tất cả</option>"
                if (response.Data != null && response.Data.length > 0) {
                    for (var i = 0; i < response.Data.length; i++) {
                        var item = response.Data[i];
                        html += "<option value =" + item.id + ">" + item.name + "</option>";
                        htmlSearch += "<option value =" + item.id + ">" + item.name + "</option>";
                    }
                }
                $("#productcategoryid").html(html);
                $(".categorylist").html(htmlSearch);
            }
        })
    }


    self.RemoveImageServer = function (productUpdateImages) {
        $.ajax({
            url: '/Admin/Product/RemoveImage',
            type: 'POST',
            dataType: 'json',
            data: {
                images: productUpdateImages
            },
            beforeSend: function () {
                Loading('show');
            },
            complete: function () {
                Loading('hiden');
            },
            success: function (response) {

            }
        })
    }




    self.RenderHtmlByObject = function (view) {
        $("#productname").val(view.name);
        $("#productcategoryid").val(view.category_id);
        //$("#productprice").val(view.price);
        //$("#productcolor").val(view.color);
        //if (view.color != null && view.color != "") {
        //    $("#producttypecolor").val(0);
        //}
        //else {
        //    $("#producttypecolor").val(1);
        //}
        self.Product.avatar = view.avatar;
        $("#productquantity").val(view.quantity);
        $("#productshort_desc").val(view.short_desc);

        $("#productpricesell").val(view.price_sell);
        $("#productreduced").val(view.price_reduced);
        $("#productpriceimport").val(view.price_import);
        $("#producttrademark").val(view.trademark);
        $("#productstatus").val(view.status);

        if (view.ImageModelView != null && view.ImageModelView.length > 0) {
            self.ProductServerImages = view.ImageModelView;
            /*  self.ProductUpdateImage = view.ImageModelView;*/
            for (var i = 0; i < view.ImageModelView.length; i++) {
                var item = view.ImageModelView[i];
                var html = "";
                html = "<div class=\"box-image\" style=\"background-image:url(/product-image/" + item.name + ")\"><span onclick=\"removeImageViewServer(" + item.id + ",this)\" class='remove-image'>X</span></div>";

                $(".productimages").append(html);
            }
        }

        /* CKEDITOR.instances.productspecifications.setData(view.specifications);*/
        /*$("#productspecifications").val(view.specifications);*/
        //$("#productendow").val(view.endow);
        //$(".box-image").css({ "background-image": "url('/product-image/" + view.avatar + "')", "display": "block" });  
    }

    self.UploadFileImageProduct = function (productid) {
        var dataImage = new FormData();
        if (self.ProductImages != null && self.ProductImages) {
            for (var i = 0; i < self.ProductImages.length; i++) {
                dataImage.append(productid, self.ProductImages[i]);
            }
        }

        $.ajax({
            url: '/Admin/ProductQuantity/UploadImageProduct',
            type: 'POST',
            contentType: false,
            processData: false,
            data: dataImage,
            beforeSend: function () {
                //Loading('show');
            },
            complete: function () {
                //Loading('hiden');
            },
            success: function (response) {
                //if (response.success) {
                //    self.GetDataPaging(true);
                //    $('#_addUpdate').modal('hide');
                //}
            }
        })
    }
    self.removeImageViewServer = function (id, tag) {
        if (self.IsUpdate) {
            if (self.ProductServerImages != null && self.ProductServerImages.length > 0) {
                var indeximage = self.ProductServerImages.find(p => p.id == id);
                if (indeximage != null) {
                    self.ProductUpdateImage.push(indeximage);
                    $(tag).parent().remove();
                }
            }
        }
    }
    self.removeImage = function (nameimage, tag) {
        if (self.ProductImages != null && self.ProductImages.length > 0) {
            var indeximage = self.ProductImages.findIndex(p => p.name == nameimage);
            if (indeximage >= 0) {
                self.ProductImages.splice(indeximage, 1);
                $(tag).parent().remove();
            }
        }
    }


    $(document).ready(function () {

        self.GetDataPaging();

    

        self.GetAllCategories();


        $(".modal").on("hidden.bs.modal", function () {
            $(this).find('form').trigger('reset');
            $("form").validate().resetForm();
            $("label.error").hide();
            $(".error").removeClass("error");
        });

        $(".btn-addorupdate").click(function () {
            $(".custom-format").removeAttr("disabled");
            $("#titleModal").text("Thêm mới tài khoản");
            $(".txtPassword").show();
            $(".btn-submit-format").text("Thêm mới");
            self.IsUpdate = false;
            $('#userModal').modal('show');
        })
        $('#select-right').on('change', function () {
            $('input.form-search').val("");
            self.ProductSearch.name = null;
            self.ProductSearch.categoryId = $(this).val();
            self.GetDataPaging(true);
        });

        $('#ddlShowPage').on('change', function () {
            tedu.configs.pageSize = $(this).val();
            tedu.configs.pageIndex = 1;
            self.GetDataPaging(true);
        });

        $('input.form-search').on('input', function (e) {
            self.ProductSearch.name = $(this).val();
            self.GetDataPaging(true);
        });


        $('#productimages').on('change', function () {
            var fileUpload = $(this).get(0);
            var files = fileUpload.files;
            if (files != null && files.length > 0) {
                var fileExtension = ['jpeg', 'jpg', 'png'];

                for (var i = 0; i < files.length; i++) {
                    var html = "";
                    if ($.inArray(files[i].type.split('/')[1].toLowerCase(), fileExtension) == -1) {
                        alert("Only formats are allowed : " + fileExtension.join(', '));
                    }
                    else {
                        files[i].name = files[i].name.replace(/ /g, "").toLowerCase();
                        self.ProductImages.push(files[i]);
                        var src = URL.createObjectURL(files[i]);

                        html = "<div class=\"box-image\" style=\"background-image:url(" + src + ")\"><span onclick=\"removeImage('" + files[i].name + "',this)\" class='remove-image'>X</span></div>";

                        $(".productimages").append(html);
                    }
                }
            }

        });

        $(".btn-back").click(function () {
            window.location.reload();
        })
        // Sử dụng cho quản lý số lượng sản phẩm
        self.InitQuantity();
    })
})(jQuery);