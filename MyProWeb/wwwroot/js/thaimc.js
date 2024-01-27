$(document).ready(function () {
    $(".ajax-add-to-cart").click(function () {
        $.ajax({
            url: "/customer/Myproweb/cart/AddToCart",
            data: {
                id: $(this).data("id"),
                SoLuong: 1,
                type: "ajax"
            },
            success: function (data) {
                Swal.fire({

                    icon: "success",
                    title: "Add To Cart Successfully!",
                    showConfirmButton: false,
                    timer: 1500
                });
            },
            error: function () {
                Swal.fire({
                    icon: "error",
                    title: "Failed!",
                    text: "Something went wrong!",
                    showConfirmButton: false,
                    timer: 1500
                });
            }
        });
    });
});
$(document).ready(function () {
    $(".ajax-add-to-cartt").click(function () {
        var quantity = $(this).closest('form').find('[name="SoLuong"]').val();

        $.ajax({
            url: "/customer/Myproweb/cart/AddToCart",
            data: {
                id: $(this).data("id"),
                SoLuong: quantity,
                type: "ajax"
            },
            success: function (data) {
                Swal.fire({

                    icon: "success",
                    title: "Add To Cart Successfully!",
                    showConfirmButton: false,
                    timer: 1500
                });
            },
            error: function () {
                Swal.fire({
                    icon: "error",
                    title: "Failed!",
                    text: "Something went wrong!",
                    showConfirmButton: false,
                    timer: 1500
                });
            }
        });
    });
});
$(document).ready(function () {
    $(".js-btn-plus").on("click", function () {
        updateQuantity($(this), 1);
    });

    $(".js-btn-minus").on("click", function () {
        updateQuantity($(this), -1);
    });

    function updateQuantity(button, delta) {
        var inputField = button.siblings(".form-control");
        var currentVal = parseInt(inputField.val());
        if (!isNaN(currentVal)) {
            var newVal = currentVal + delta;
            if (newVal > 0) {
                inputField.val(newVal);
            }
        }
    }
});