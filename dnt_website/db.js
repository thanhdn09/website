// db.js
const mysql = require('mysql');

// Thiết lập kết nối MySQL
const db = mysql.createConnection({
    host: 'localhost',
    user: 'root', // Thay bằng tên người dùng MySQL của bạn
    password: 'vanpersi34', // Thay bằng mật khẩu MySQL của bạn
    database: 'myt_databse' // Thay bằng tên database
});

// Kết nối MySQL
db.connect((err) => {
    if (err) {
        throw err;
    }
    console.log('Kết nối MySQL thành công!');
});

module.exports = db; // Xuất kết nối để sử dụng ở file khác
