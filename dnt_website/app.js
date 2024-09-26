const express = require('express');
const mysql = require('mysql');
const session = require('express-session');
const bodyParser = require('body-parser');
const app = express();

// Thiết lập view engine là EJS
app.set('view engine', 'ejs');

// Sử dụng body-parser để phân tích các yêu cầu
app.use(bodyParser.urlencoded({ extended: false }));

// Thiết lập session
app.use(session({
    secret: 'secret_key', // Thay đổi khóa bí mật này
    resave: false,
    saveUninitialized: true,
}));

// Thiết lập kết nối MySQL
const db = mysql.createConnection({
    host: 'localhost',
    user: 'root',
    password: 'vanpersi34',
    database: 'myt_databse'
});

// Kết nối MySQL
db.connect((err) => {
    if (err) {
        throw err;
    }
    console.log('Kết nối MySQL thành công!');
});

// Middleware kiểm tra đăng nhập
const checkAuth = (req, res, next) => {
    if (req.session.user) {
        next();
    } else {
        res.redirect('/login');
    }
};

// Route đăng ký
app.get('/signup', (req, res) => {
    res.render('signup');
});

// Xử lý đăng ký
app.post('/signup', (req, res) => {
    const { username, password } = req.body;
    const sql = 'INSERT INTO users (username, password) VALUES (?, ?)';
    db.query(sql, [username, password], (err, result) => {
        if (err) throw err;
        res.send('Đăng ký thành công!');
    });
});

// Route đăng nhập
app.get('/login', (req, res) => {
    res.render('login');
});

// Xử lý đăng nhập
app.post('/login', (req, res) => {
    const { username, password } = req.body;
    const sql = 'SELECT * FROM users WHERE username = ? AND password = ?';
    db.query(sql, [username, password], (err, results) => {
        if (err) throw err;
        if (results.length > 0) {
            req.session.user = results[0]; // Lưu thông tin người dùng vào session
            res.redirect('/'); // Chuyển hướng đến trang chủ
        } else {
            res.send('Tên đăng nhập hoặc mật khẩu không đúng!');
        }
    });
});
// Đăng xuất
app.get('/logout', (req, res) => {
    req.session.destroy((err) => {
        if (err) throw err;
        res.redirect('/login'); // Chuyển hướng về trang đăng nhập
    });
});

// Route trang chủ (được bảo vệ)
app.get('/', checkAuth, (req, res) => {
    res.render('index', { user: req.session.user });
});

// Thêm sản phẩm
app.get('/products/add', checkAuth, (req, res) => {
    res.render('addProduct');
});

app.post('/products/add', checkAuth, (req, res) => {
    const { name, price } = req.body;
    const sql = 'INSERT INTO products (name, price) VALUES (?, ?)';
    db.query(sql, [name, price], (err, result) => {
        if (err) throw err;
        res.redirect('/products'); // Chuyển hướng đến trang danh sách sản phẩm
    });
});

// Xem danh sách sản phẩm
app.get('/products', checkAuth, (req, res) => {
    const sql = 'SELECT * FROM products';
    db.query(sql, (err, results) => {
        if (err) throw err;
        res.render('products', { products: results });
    });
});

// Khởi chạy server
const port = 3000;
app.listen(port, () => {
    console.log(`Server đang chạy tại http://localhost:${port}`);
});
