// routes/product.js
const express = require('express');
const router = express.Router();
const db = require('../db'); // Nhập kết nối từ db.js

// Lấy tất cả sản phẩm
router.get('/', (req, res) => {
    const sql = 'SELECT * FROM products';
    db.query(sql, (err, results) => {
        if (err) {
            return res.status(500).send(err);
        }
        res.json(results);
    });
});

// Thêm sản phẩm
router.post('/', (req, res) => {
    const newProduct = req.body; // Dữ liệu sản phẩm mới từ body
    const sql = 'INSERT INTO products SET ?';
    
    db.query(sql, newProduct, (err, result) => {
        if (err) {
            return res.status(500).send(err);
        }
        res.status(201).json({ id: result.insertId, ...newProduct });
    });
});

// Cập nhật sản phẩm
router.put('/:id', (req, res) => {
    const productId = req.params.id;
    const updatedProduct = req.body; // Dữ liệu sản phẩm cập nhật từ body
    const sql = 'UPDATE products SET ? WHERE id = ?';
    
    db.query(sql, [updatedProduct, productId], (err, result) => {
        if (err) {
            return res.status(500).send(err);
        }
        if (result.affectedRows === 0) {
            return res.status(404).json({ message: 'Sản phẩm không tìm thấy.' });
        }
        res.json({ id: productId, ...updatedProduct });
    });
});

// Xóa sản phẩm
router.delete('/:id', (req, res) => {
    const productId = req.params.id;
    const sql = 'DELETE FROM products WHERE id = ?';
    
    db.query(sql, productId, (err, result) => {
        if (err) {
            return res.status(500).send(err);
        }
        if (result.affectedRows === 0) {
            return res.status(404).json({ message: 'Sản phẩm không tìm thấy.' });
        }
        res.json({ message: 'Sản phẩm đã được xóa.' });
    });
});

module.exports = router; // Xuất router để sử dụng ở file khác
