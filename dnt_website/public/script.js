// dnt_website/public/script.js
const productForm = document.getElementById('productForm');
const productTable = document.getElementById('productTable').getElementsByTagName('tbody')[0];

// Lấy danh sách sản phẩm
function fetchProducts() {
    fetch('/products')
        .then(response => response.json())
        .then(data => {
            productTable.innerHTML = ''; // Xóa nội dung trước đó
            data.forEach(product => {
                const row = productTable.insertRow();
                row.innerHTML = `
                    <td>${product.id}</td>
                    <td>${product.name}</td>
                    <td>${product.price}</td>
                    <td>
                        <button onclick="editProduct(${product.id}, '${product.name}', ${product.price})">Sửa</button>
                        <button onclick="deleteProduct(${product.id})">Xóa</button>
                    </td>
                `;
            });
        });
}

// Thêm hoặc cập nhật sản phẩm
productForm.addEventListener('submit', function(event) {
    event.preventDefault(); // Ngăn chặn hành động mặc định của form

    const productId = document.getElementById('productId').value;
    const productName = document.getElementById('productName').value;
    const productPrice = document.getElementById('productPrice').value;

    const url = productId ? `/products/${productId}` : '/products';
    const method = productId ? 'PUT' : 'POST';

    fetch(url, {
        method: method,
        headers: {
            'Content-Type': 'application/json'
        },
        body: JSON.stringify({ name: productName, price: productPrice })
    })
    .then(response => response.json())
    .then(data => {
        fetchProducts(); // Cập nhật danh sách sản phẩm
        productForm.reset(); // Đặt lại form
        document.getElementById('productId').value = ''; // Xóa ID sản phẩm
    });
});

// Chỉnh sửa sản phẩm
function editProduct(id, name, price) {
    document.getElementById('productId').value = id;
    document.getElementById('productName').value = name;
    document.getElementById('productPrice').value = price;
}

// Xóa sản phẩm
function deleteProduct(id) {
    fetch(`/products/${id}`, {
        method: 'DELETE'
    })
    .then(response => response.json())
    .then(data => {
        fetchProducts(); // Cập nhật danh sách sản phẩm
    });
}

// Lấy danh sách sản phẩm khi tải trang
window.onload = fetchProducts;
