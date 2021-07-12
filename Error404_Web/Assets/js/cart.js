$('.plus').click(function () {
    var product = $(this).closest('.product')
    var q = product.data('quantity') + 1;
    product.data('quantity', q);
    updateProduct(product);
});

$('.minus').click(function () {
    var product = $(this).closest('.product')
    var q = Math.max(1, product.data('quantity') - 1);
    product.data('quantity', q);
    updateProduct(product);
});

$('.del').click(function () {
    var product = $(this).closest('.product')
    product.hide('blind', { direction: 'left' }, 500, function () {
        product.remove();
        updateProduct(product);
        if ($('.product').length == 0) {
            $('.cart-container .cart').hide();
            $('.cart-container .empty').show();
        }
    });
});

function updateProduct(product) {
    var quantity = product.data('quantity');
    var price = product.data('price');
    $('.product-quantity', product).text('x' + quantity);
    $('.product-price', product).text('$ ' + (price * quantity).toFixed(2));
    updateBill();
}

function updateBill() {
    var subtotal = 0;
    var salestax = 0;
    var shipping = 5;
    var total = 0;
    $('.product').each(function () {
        subtotal += $(this).data('quantity') * $(this).data('price');
    });
    salestax = subtotal * 0.05;
    total = subtotal + salestax + shipping;
    $('.subtotal .value').text('$ ' + subtotal.toFixed(2));
    $('.salestax .value').text('$ ' + salestax.toFixed(2));
    $('.total .value').text('$ ' + total.toFixed(2));
}



$(document).ready(function () {

    // Product data to be used in shop and in cart
    var products = {
        'Octocat Mug': ['Octocat Mug', "The mug you've been dreaming about. One sip from this ceramic 16oz fluid delivery system and you'll never go back to red cups.", 14, 'https://cdn.shopify.com/s/files/1/0051/4802/products/white-mug-1_1024x1024.jpg', 1],
        'Leather Coasters': ['Leather Coasters', "These coasters roll all of the greatest qualities into one: class, leather, and octocats. They also happen to protect surfaces from cold drinks.", 18, 'https://cdn.shopify.com/s/files/1/0051/4802/products/MG_1934_1024x1024.jpg', 2],
        'Octopint (Set of 2)': ['Octopint (Set of 2)', "Set of two heavyweight 16 oz. Octopint glasses for your favorite malty beverage.", 16, 'https://cdn.shopify.com/s/files/1/0051/4802/products/pint_1024x1024.jpg', 3],
        'Blacktocat 2.0 Tee': ['Blacktocat 2.0 Tee', "Check it. Blacktocat is back with a whole new direction. He's exited stealth mode and is ready for primetime, now with a stylish logo.", 25, 'https://cdn.shopify.com/s/files/1/0051/4802/products/blacktocat-3_1024x1024.jpg', 4],
        'Die Cut Stickers': ['Die Cut Stickers', "Need a huge Octocat sticker for your laptop, fridge, snowboard, or ceiling fan? Look no further!", 2, 'https://cdn.shopify.com/s/files/1/0051/4802/products/sticker-large_1024x1024.jpg', 5],
        'Pixelcat Shirt': ['Pixelcat Shirt', "Pixels are your friends. Show your bits in this super-comfy blue American Apparel tri-blend shirt with a pixelated version of your favorite aquatic feline", 25, 'https://cdn.shopify.com/s/files/1/0051/4802/products/8bit-1_1024x1024.jpg?145', 6]
    };
    // 'tên sp': [tên sp, mô tả, giá, ảnh, id]

    // Populates shop with items based on template and data in var products

    var $shop = $('.shop');
    var $cart = $('.cart-items');

    for (var item in products) {
        var itemName = products[item][0],
            itemDescription = products[item][1],
            itemPrice = products[item][2],
            itemImg = products[item][3],
            itemId = products[item][4],
            $template = $($('#productTemplate').html());

        $template.find('h1').text(itemName);
        $template.find('p').text(itemDescription);
        $template.find('.price').text('$' + itemPrice);
        $template.css('background-image', 'url(' + itemImg + ')');

        $template.data('id', itemId);
        $template.data('name', itemName);
        $template.data('price', itemPrice);
        $template.data('image', itemImg);

        $shop.append($template);
    }

    // Add item from the shop to the cart
    // If item is already in the cart, +1 to quantity
    // If not, creates the cart item based on template

    $('body').on('click', '.product .add', function () {
        var items = $cart.children(),
            $item = $(this).parents('.product'),
            $template = $($('#cartItem').html()),
            $matched = null,
            quantity = 0;

        $matched = items.filter(function (index) {
            var $this = $(this);
            return $this.data('id') === $item.data('id');
        });

        if ($matched.length) {
            quantity = +$matched.find('.quantity').val() + 1;
            $matched.find('.quantity').val(quantity);
            calculateSubtotal($matched);
        } else {
            $template.find('.cart-product').css('background-image', 'url(' + $item.data('image') + ')');
            $template.find('h3').text($item.data('name'));
            $template.find('.subtotal').text('$' + $item.data('price'));

            $template.data('id', $item.data('id'));
            $template.data('price', $item.data('price'));
            $template.data('subtotal', $item.data('price'));

            $cart.append($template);
        }

        updateCartQuantity();
        calculateAndUpdate();
    });

    // Calculates subtotal for an item

    function calculateSubtotal($item) {
        var quantity = $item.find('.quantity').val(),
            price = $item.data('price'),
            subtotal = quantity * price;
        $item.find('.subtotal').text('$' + subtotal);
        $item.data('subtotal', subtotal);
    }

    // Calculates and updates totals, taxes, shipping

    function calculateAndUpdate() {
        var subtotal = 0,
            items = $cart.children(),
            // shipping not applied if there are no items
            shipping = items.length > 0 ? 5 : 0,
            tax = 0;
        items.each(function (index, item) {
            var $item = $(item),
                price = $item.data('subtotal');
            subtotal += price;
        });
        $('.subtotalTotal span').text(formatDollar(subtotal));
        tax = subtotal * .05;
        $('.taxes span').text(formatDollar(tax));
        $('.shipping span').text(formatDollar(shipping));
        $('.finalTotal span').text(formatDollar(subtotal + tax + shipping));
    }

    //  Update the total quantity of items in notification, hides if zero

    function updateCartQuantity() {
        var quantities = 0,
            $cartQuantity = $('span.cart-quantity'),
            items = $cart.children();
        items.each(function (index, item) {
            var $item = $(item),
                quantity = +$item.find('.quantity').val();
            quantities += quantity;
        });
        if (quantities > 0) {
            $cartQuantity.removeClass('empty');
        } else {
            $cartQuantity.addClass('empty');
        }
        $cartQuantity.text(quantities);
    }


    //  Formats number into dollar format

    function formatDollar(amount) {
        return '$' + parseFloat(Math.round(amount * 100) / 100).toFixed(2);
    }


    /////////////////////////////////////////////////////

    /* Set rates + misc */
    var taxRate = 0.05;
    var shippingRate = 0.00;
    var fadeTime = 300;


    /* Assign actions */
    $('.product-quantity input').change(function () {
        updateQuantity(this);
    });

    $('.product-removal button').click(function () {
        removeItem(this);
    });


    /* Recalculate cart */
    function recalculateCart() {
        var subtotal = 0;

        /* Sum up row totals */
        $('.product').each(function () {
            subtotal += parseFloat($(this).children('.product-line-price').text());
        });

        /* Calculate totals */
        var tax = subtotal * taxRate;
        var shipping = (subtotal > 0 ? shippingRate : 0);
        var total = subtotal + tax + shipping;

        /* Update totals display */
        $('.totals-value').fadeOut(fadeTime, function () {
            $('#cart-subtotal').html(subtotal.toFixed(2));
            $('#cart-tax').html(tax.toFixed(2));
            $('#cart-shipping').html(shipping.toFixed(2));
            $('#cart-total').html(total.toFixed(2));
            if (total == 0) {
                $('.checkout').fadeOut(fadeTime);
            } else {
                $('.checkout').fadeIn(fadeTime);
            }
            $('.totals-value').fadeIn(fadeTime);
        });
    }


    /* Update quantity */
    function updateQuantity(quantityInput) {
        /* Calculate line price */
        var productRow = $(quantityInput).parent().parent();
        var price = parseFloat(productRow.children('.product-price').text());
        var quantity = $(quantityInput).val();
        var linePrice = price * quantity;

        /* Update line price display and recalc cart totals */
        productRow.children('.product-line-price').each(function () {
            $(this).fadeOut(fadeTime, function () {
                $(this).text(linePrice.toFixed(2));
                recalculateCart();
                $(this).fadeIn(fadeTime);
            });
        });
    }


    /* Remove item from cart */
    function removeItem(removeButton) {
        /* Remove row from DOM and recalc cart total */
        var productRow = $(removeButton).parent().parent();
        productRow.slideUp(fadeTime, function () {
            productRow.remove();
            recalculateCart();
        });
    }

});