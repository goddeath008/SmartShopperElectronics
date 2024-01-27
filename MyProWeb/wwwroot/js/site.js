// Please see documentation at https://docs.microsoft.com/aspnet/core/client-side/bundling-and-minification
// for details on configuring this project to bundle and minify static web assets.
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

// Write your JavaScript code.
