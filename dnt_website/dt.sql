CREATE DATABASE IF NOT EXISTS myt_databse;

USE myt_databse;

CREATE TABLE IF NOT EXISTS products (
    id INT AUTO_INCREMENT PRIMARY KEY,
    name VARCHAR(255) NOT NULL,
    price DECIMAL(10, 2) NOT NULL,
    description TEXT
);

INSERT INTO products (name, price, description) VALUES
('sp 1', 19.99, 'Mô tả cho sản phẩm 1'),
('Sản phẩm mẫu 2', 29.99, 'Mô tả cho sản phẩm 2');
