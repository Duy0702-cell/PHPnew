-- Access SQL: tao bang Products
CREATE TABLE Products (
    Id AUTOINCREMENT PRIMARY KEY,
    ProductName TEXT(255),
    Brand TEXT(100),
    Price CURRENCY,
    ImageUrl TEXT(255),
    [Description] MEMO,
    NoiBat MEMO
);

INSERT INTO Products (ProductName, Brand, Price, ImageUrl, [Description], NoiBat) VALUES
('Laptop ASUS TUF A15', 'ASUS', 21990000, 'https://images.unsplash.com/photo-1496181133206-80ce9b88a853?auto=format&fit=crop&w=700&q=60', 'Laptop gaming tam trung manh me', 'CPU hieu nang cao|RAM 16GB da nhiem tot|Man hinh 144Hz muot'),
('Man hinh LG UltraGear 27"', 'LG', 5490000, 'https://images.unsplash.com/photo-1527443224154-c4a3942d3acf?auto=format&fit=crop&w=700&q=60', 'Man hinh 144Hz cho game va thiet ke', 'Tam nen IPS mau dep|Tan so quet 144Hz|Thoi gian dap ung thap'),
('PC Gaming RTX 4060', 'DuyTech Build', 25990000, 'https://images.unsplash.com/photo-1587202372775-989194f20f5d?auto=format&fit=crop&w=700&q=60', 'Case gaming hieu nang cao, toi uu FPS', 'Card do hoa RTX 4060|SSD NVMe toc do cao|Case thoang khi, de nang cap'),
('Ban phim co Keychron K2', 'Keychron', 1990000, 'https://images.unsplash.com/photo-1511467687858-23d96c32e4ae?auto=format&fit=crop&w=700&q=60', 'Ban phim co gon nhe, switch em', 'Layout gon 75%|Ket noi da thiet bi|Pin dung lau');
