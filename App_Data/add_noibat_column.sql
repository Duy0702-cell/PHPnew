-- Them 1 cot NoiBat de luu 3 y noi bat trong 1 truong
ALTER TABLE Products ADD COLUMN NoiBat MEMO;

-- Du lieu mau neu cot dang rong
UPDATE Products
SET NoiBat = 'Hieu nang on dinh|Linh kien chinh hang|De dang nang cap'
WHERE NoiBat IS NULL OR Trim(NoiBat) = '';
