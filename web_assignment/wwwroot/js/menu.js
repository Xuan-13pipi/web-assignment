
//Photo perview
/*$('.upload input').on('change', e => {
    const file = e.target.files[0];
    const img = $(e.target).siblings('img');

    img.dataset.src ??= img.src;

    if (file && file.type.startsWith('image/')) {
        img.onload = e => URL.revokeObjectURL(img.src);
        img.src = URL.createObjectURL(file);
    }
    else {
        img.src = img.dataset.src;
        e.target.value = '';
    }
    // Trigger input validation
    $(e.target).valid();
});

*/

var selectedTable = null;
var orderItems = [];
var subtotal = 0;
var taxRate = 0.06;

function selectTable(element, tableId) {
    selectedTable = tableId;

    var tableBtns = document.querySelectorAll('.table-btn');
    for (var i = 0; i < tableBtns.length; i++) {
        tableBtns[i].classList.remove('selected');
    }

    element.classList.add('selected');
}

function addToOrder(itemName, price, imageSrc) {
    console.log('addToOrder called with:', itemName, price, imageSrc); // Debug log

    if (!selectedTable) {
        alert('Please select a table first!');
        return;
    }

    if (!itemName || isNaN(price)) {
        console.error('Invalid parameters:', itemName, price);
        return;
    }

    var existingItem = null;
    for (var i = 0; i < orderItems.length; i++) {
        if (orderItems[i].name === itemName) {
            existingItem = orderItems[i];
            break;
        }
    }

    if (existingItem) {
        existingItem.quantity += 1;
    } else {
        orderItems.push({
            name: itemName,
            price: price,
            quantity: 1,
            image: imageSrc || ''
        });
    }

    console.log('Current orderItems:', orderItems); // Debug log
    updateOrderDisplay();
}

function updateOrderDisplay() {
    var container = document.getElementById('cart-items-container');
    var cartCount = document.getElementById('cart-count');

    container.innerHTML = '';

    if (orderItems.length === 0) {
        var emptyMsg = document.createElement('div');
        emptyMsg.className = 'empty-cart';
        emptyMsg.textContent = 'No items added yet';
        container.appendChild(emptyMsg);

        subtotal = 0;
        cartCount.textContent = '(0)';
    } else {
        var clearAllBtn = document.createElement('button');
        clearAllBtn.className = 'clear-all-btn';
        clearAllBtn.textContent = 'Clear All';
        clearAllBtn.onclick = clearAllItems;
        container.appendChild(clearAllBtn);

        var itemCount = 0;
        for (var i = 0; i < orderItems.length; i++) {
            itemCount += orderItems[i].quantity;
        }
        cartCount.textContent = '(' + itemCount + ')';

        for (var i = 0; i < orderItems.length; i++) {
            var item = orderItems[i];
            var cartItem = document.createElement('div');
            cartItem.className = 'cart-item';

            var imageSrc = item.image || '';

            var cartItemHTML = '<img src="' + imageSrc + '" alt="' + item.name + '" class="cart-item-image" />' +
                '<div class="cart-item-info">' +
                '<div class="cart-item-name">' + item.name + '</div>' +
                '<div class="cart-item-price">RM ' + item.price.toFixed(2) + '</div>' +
                '</div>' +
                '<div class="quantity-controls">' +
                '<button class="quantity-btn" onclick="changeQuantity(' + i + ', -1)">-</button>' +
                '<input type="number" value="' + item.quantity + '" class="quantity-input" readonly />' +
                '<button class="quantity-btn" onclick="changeQuantity(' + i + ', 1)">+</button>' +
                '</div>' +
                '<div class="remove-btn" onclick="removeItem(' + i + ')"><img src="/images/trash.svg" alt="Trash" width="20" height="20" style="vertical-align: middle; margin-right: 5px;" /></div>';

            cartItem.innerHTML = cartItemHTML;
            container.appendChild(cartItem);
        }

        subtotal = 0;
        for (var i = 0; i < orderItems.length; i++) {
            subtotal += orderItems[i].price * orderItems[i].quantity;
        }
    }

    var tax = subtotal * taxRate;
    var total = subtotal + tax;

    document.getElementById('subtotal-amount').textContent = 'RM ' + subtotal.toFixed(2);
    document.getElementById('tax-amount').textContent = 'RM ' + tax.toFixed(2);
    document.getElementById('total-amount').textContent = 'RM ' + total.toFixed(2);
}

function changeQuantity(index, change) {
    orderItems[index].quantity += change;
    if (orderItems[index].quantity <= 0) {
        orderItems.splice(index, 1);
    }
    updateOrderDisplay();
}

function removeItem(index) {
    orderItems.splice(index, 1);
    updateOrderDisplay();
}

function clearAllItems() {
    orderItems = [];
    updateOrderDisplay();
}

function placeOrder() {
    if (!selectedTable) {
        alert('Please select a table first!');
        return;
    }

    if (orderItems.length === 0) {
        alert('Please add items to the order!');
        return;
    }

    var total = subtotal + (subtotal * taxRate);
    var orderData = {
        tableId: selectedTable,
        items: orderItems,
        subtotal: subtotal,
        tax: subtotal * taxRate,
        total: total,
        createdBy: 'Staff'
    };

    console.log('Submitting order:', orderData);
    alert('Order submitted for Table ' + selectedTable + '!\nTotal: RM ' + total.toFixed(2) + '\nItems: ' + orderItems.length);

    clearAllItems();

    var tableBtns = document.querySelectorAll('.table-btn');
    for (var i = 0; i < tableBtns.length; i++) {
        tableBtns[i].classList.remove('selected');
    }
    selectedTable = null;
}

document.addEventListener('DOMContentLoaded', function () {
    updateOrderDisplay();

    var addBtns = document.querySelectorAll('.add-btn');
    for (var i = 0; i < addBtns.length; i++) {
        addBtns[i].addEventListener('click', function (e) {
            e.preventDefault();
            var productCard = this.closest('.product-card');
            var itemName = productCard.getAttribute('data-item');
            var price = parseFloat(productCard.getAttribute('data-price'));
            var productImage = productCard.querySelector('.product-image');
            var imageSrc = productImage ? productImage.src : '';

            console.log('Adding item:', itemName, 'Price:', price, 'Image:', imageSrc); // Debug log

            if (itemName && !isNaN(price)) {
                addToOrder(itemName, price, imageSrc);
            } else {
                console.error('Invalid item data:', itemName, price);
            }
        });
    }

    document.getElementById('place-order-btn').addEventListener('click', placeOrder);
});