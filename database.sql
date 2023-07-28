create database PRN_Shoes_Store;
go
use PRN_Shoes_Store;
go

-- Create Brand Table
CREATE TABLE Brand (
    BrandID INT IDENTITY(1,1) PRIMARY KEY,
    BrandName VARCHAR(255),
    Country VARCHAR(255),
    Website VARCHAR(255)
);

-- Create Category Table
CREATE TABLE Category (
    CategoryID INT IDENTITY(1,1) PRIMARY KEY,
    CategoryName VARCHAR(255)
);

-- Create Shoe Table
CREATE TABLE Shoe (
    ShoeID INT IDENTITY(1,1) PRIMARY KEY,
    BrandID INT,
    CategoryID INT,
    SKU VARCHAR(255) UNIQUE,
    Model VARCHAR(255),
    Color VARCHAR(255),
    Size VARCHAR(255),
    Price DECIMAL(10, 2),
    Quantity INT,
    ImageURL VARCHAR(255),
    Description VARCHAR(MAX),
    FOREIGN KEY (BrandID) REFERENCES Brand(BrandID),
    FOREIGN KEY (CategoryID) REFERENCES Category(CategoryID)
);

-- Create Account Table
CREATE TABLE Account (
    AccountID INT IDENTITY(1, 1) PRIMARY KEY,
    Username VARCHAR(255) NOT NULL,
    Password VARCHAR(255) NOT NULL,
    Email VARCHAR(255) NOT NULL,
    FirstName VARCHAR(255),
    LastName VARCHAR(255),
    RegistrationDate DATETIME DEFAULT GETDATE(),
    IsActive BIT DEFAULT 1,
    Role VARCHAR(255) DEFAULT 'user',
    CONSTRAINT UQ_Username UNIQUE (Username),
    CONSTRAINT UQ_Email UNIQUE (Email)
);

-- Create Orders Table
CREATE TABLE Orders (
    OrderID INT IDENTITY(1,1) PRIMARY KEY,
    AccountID INT,
    OrderDate DATETIME DEFAULT GETDATE(),
    Total DECIMAL(10, 2),
    Status VARCHAR(50) DEFAULT 'Pending',
    FOREIGN KEY (AccountID) REFERENCES Account(AccountID)
);

-- Create OrderItem Table
CREATE TABLE OrderItem (
    OrderItemID INT IDENTITY(1,1) PRIMARY KEY,
    OrderID INT,
    ShoeID INT,
    Quantity INT,
    Price DECIMAL(10, 2),
    FOREIGN KEY (OrderID) REFERENCES Orders(OrderID),
    FOREIGN KEY (ShoeID) REFERENCES Shoe(ShoeID)
);
CREATE TABLE Ratings (
    RatingID INT IDENTITY(1,1) PRIMARY KEY,
    ShoeID INT,
    AccountID INT,
    Rating INT,
    Comment VARCHAR(MAX),
    Status VARCHAR(50) DEFAULT 'Pending',
    FOREIGN KEY (ShoeID) REFERENCES Shoe(ShoeID),
    FOREIGN KEY (AccountID) REFERENCES Account(AccountID)
);

-- Insert data into the Account table
INSERT INTO Account (Username, Password, Email, FirstName, LastName, Role)
VALUES ('admin', '123456', 'admin@example.com', 'Admin', 'User', 'admin');

INSERT INTO Account (Username, Password, Email, FirstName, LastName, Role)
VALUES ('user1', '123456', 'user1@example.com', 'John', 'Doe', 'user');

INSERT INTO Account (Username, Password, Email, FirstName, LastName, Role)
VALUES ('user2', '123456', 'user2@example.com', 'Jane', 'Smith', 'user');

-- Insert data into the Brand table
INSERT INTO Brand (BrandName, Country, Website)
VALUES ('Nike', 'United States', 'https://www.nike.com'),
       ('Adidas', 'Germany', 'https://www.adidas.com'),
       ('Puma', 'Germany', 'https://www.puma.com');

-- Insert data into the Category table
INSERT INTO Category (CategoryName)
VALUES ('Athletic'),
       ('Casual'),
       ('Formal');

-- Insert data into the Shoe table
-- Insert data into the Shoe table
INSERT INTO Shoe (BrandID, CategoryID, SKU, Model, Color, Size, Price, Quantity, ImageURL, Description)
VALUES (1, 1, 'SKU001', 'Nike Air Max 90', 'Black', '10', 129.99, 10, 'https://static.nike.com/a/images/t_PDP_1728_v1/f_auto,q_auto:eco/08f113fb-396f-4445-a89b-f82752a7cb82/air-max-90-g-golf-shoe-qlD3wL.png', 'Classic athletic shoe design'),
       (2, 1, 'SKU002', 'Adidas Stan Smith', 'White', '9', 89.99, 5, 'https://assets.adidas.com/images/h_840,f_auto,q_auto,fl_lossy,c_fill,g_auto/e01dea68cf93434bae5aac0900af99e8_9366/Giay_Stan_Smith_trang_FX5500_01_standard.jpg', 'Iconic tennis shoe design'),
       (1, 2, 'SKU003', 'Nike Air Force 1', 'White', '8', 109.99, 3, 'https://static.nike.com/a/images/t_PDP_1728_v1/f_auto,q_auto:eco/b7d9211c-26e7-431a-ac24-b0540fb3c00f/air-force-1-07-shoes-WrLlWX.png', 'Versatile casual shoe'),
       (3, 1, 'SKU004', 'Puma RS-X', 'Black', '7.5', 79.99, 8, 'https://i8.amplience.net/i/jpl/jd_602984_a?v=1', 'Modern sneaker design'),
       (1, 3, 'SKU005', 'Nike ZoomX Vaporfly NEXT%', 'Lime Blast', '11', 249.99, 2, 'https://product.hstatic.net/200000384421/product/606705_01.jpg_ef2cdc81292e4e7583_e6978625da564aff8d9f8b13a709416e.png', 'High-performance running shoe'),
       (2, 2, 'SKU006', 'Adidas NMD R1', 'Grey', '9.5', 129.99, 7, 'https://assets.adidas.com/images/w_600,f_auto,q_auto/03f33196c06e46248f20ab8200e60e8f_9366/NMD_R1_trang_FY2457_01_standard.jpg', 'Stylish and comfortable everyday shoe'),
       (3, 2, 'SKU007', 'Puma Suede Classic', 'Red', '8.5', 69.99, 4, 'https://images.puma.com/image/upload/f_auto,q_auto,b_rgb:fafafa,w_2000,h_2000/global/374915/02/sv01/fnd/PHL/fmt/png/Suede-Classic-XXI-Trainers', 'Timeless streetwear sneaker'),
       (1, 1, 'SKU008', 'Nike Air Jordan 1', 'Black', '10', 159.99, 6, 'https://bizweb.dktcdn.net/100/336/177/products/55-1afed988-470b-4651-86d8-c6afed8103e7.jpg?v=1606282879320', 'Iconic basketball shoe'),
       (2, 3, 'SKU009', 'Adidas Ultra Boost', 'Black', '11', 179.99, 9, 'https://assets.adidas.com/images/w_600,f_auto,q_auto/75cda55cf03c431c8982af3400fb6c30_9366/Giay_Ultraboost_1.0_Xam_GY7486_01_standard.jpg', 'Premium cushioning for running'),
       (3, 3, 'SKU010', 'Puma Clyde Court', 'White', '9.5', 119.99, 3, 'https://www.tradeinn.com/f/13735/137358614/puma-clyde-court-gw-basketball-shoes.jpg', 'Basketball shoe with sleek design');

