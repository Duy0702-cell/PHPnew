using System;
using System.Collections.Generic;
using System.Data;
using System.Data.OleDb;

namespace DemoAsp
{
    public class ProductService
    {
        public DataTable GetProductsTable(int top)
        {
            string sql = top > 0
                ? "SELECT TOP " + top + " * FROM Products ORDER BY Id DESC"
                : "SELECT * FROM Products ORDER BY Id DESC";

            try
            {
                DataTable tb = DataProvider.LayBang(sql);
                if (tb.Rows.Count == 0)
                {
                    return GetFallbackTable();
                }

                return ChuanHoaBang(tb);
            }
            catch
            {
                return GetFallbackTable();
            }
        }

        public List<Product> GetProducts(int top)
        {
            List<Product> list = new List<Product>();
            DataTable tb = GetProductsTable(top);
            foreach (DataRow row in tb.Rows)
            {
                list.Add(Map(row));
            }
            return list;
        }

        public Product GetProductById(int id)
        {
            try
            {
                string sql = "SELECT TOP 1 * FROM Products WHERE Id = ?";
                DataTable tb = DataProvider.LayBang(sql, new OleDbParameter("@id", id));
                if (tb.Rows.Count > 0)
                {
                    return Map(tb.Rows[0]);
                }
            }
            catch
            {
            }

            foreach (Product sp in GetFallbackProducts())
            {
                if (sp.Id == id)
                {
                    return sp;
                }
            }

            return null;
        }

        public bool AddProduct(Product p, out string error)
        {
            error = string.Empty;

            try
            {
                string sql = "INSERT INTO Products (ProductName, Brand, Price, ImageUrl, [Description], NoiBat) VALUES (?, ?, ?, ?, ?, ?)";
                int row = DataProvider.ThucThi(
                    sql,
                    new OleDbParameter("@name", p.Name),
                    new OleDbParameter("@brand", p.Brand),
                    new OleDbParameter("@price", p.Price),
                    new OleDbParameter("@img", p.ImageUrl),
                    new OleDbParameter("@desc", p.Description),
                    new OleDbParameter("@noibat", p.NoiBat ?? string.Empty));

                return row > 0;
            }
            catch
            {
                // DB may not have column NoiBat yet -> fallback query without column
            }

            try
            {
                string sql = "INSERT INTO Products (ProductName, Brand, Price, ImageUrl, [Description]) VALUES (?, ?, ?, ?, ?)";
                int row = DataProvider.ThucThi(
                    sql,
                    new OleDbParameter("@name", p.Name),
                    new OleDbParameter("@brand", p.Brand),
                    new OleDbParameter("@price", p.Price),
                    new OleDbParameter("@img", p.ImageUrl),
                    new OleDbParameter("@desc", p.Description));

                return row > 0;
            }
            catch (Exception ex)
            {
                error = ex.Message;
                return false;
            }
        }

        public bool UpdateProduct(Product p, out string error)
        {
            error = string.Empty;

            try
            {
                string sql = "UPDATE Products SET ProductName = ?, Brand = ?, Price = ?, ImageUrl = ?, [Description] = ?, NoiBat = ? WHERE Id = ?";
                int row = DataProvider.ThucThi(
                    sql,
                    new OleDbParameter("@name", p.Name),
                    new OleDbParameter("@brand", p.Brand),
                    new OleDbParameter("@price", p.Price),
                    new OleDbParameter("@img", p.ImageUrl),
                    new OleDbParameter("@desc", p.Description),
                    new OleDbParameter("@noibat", p.NoiBat ?? string.Empty),
                    new OleDbParameter("@id", p.Id));

                return row > 0;
            }
            catch
            {
                // DB may not have column NoiBat yet -> fallback query without column
            }

            try
            {
                string sql = "UPDATE Products SET ProductName = ?, Brand = ?, Price = ?, ImageUrl = ?, [Description] = ? WHERE Id = ?";
                int row = DataProvider.ThucThi(
                    sql,
                    new OleDbParameter("@name", p.Name),
                    new OleDbParameter("@brand", p.Brand),
                    new OleDbParameter("@price", p.Price),
                    new OleDbParameter("@img", p.ImageUrl),
                    new OleDbParameter("@desc", p.Description),
                    new OleDbParameter("@id", p.Id));

                return row > 0;
            }
            catch (Exception ex)
            {
                error = ex.Message;
                return false;
            }
        }

        public bool DeleteProduct(int id, out string error)
        {
            error = string.Empty;
            try
            {
                string sql = "DELETE FROM Products WHERE Id = ?";
                int row = DataProvider.ThucThi(sql, new OleDbParameter("@id", id));
                return row > 0;
            }
            catch (Exception ex)
            {
                error = ex.Message;
                return false;
            }
        }

        private DataTable ChuanHoaBang(DataTable tb)
        {
            if (!tb.Columns.Contains("NoiBat"))
            {
                tb.Columns.Add("NoiBat", typeof(string));
            }
            return tb;
        }

        private Product Map(DataRow row)
        {
            Product p = new Product();
            p.Id = Convert.ToInt32(row["Id"]);
            p.Name = GetString(row, "ProductName");
            p.Brand = GetString(row, "Brand");
            p.Price = row["Price"] == DBNull.Value ? 0 : Convert.ToDecimal(row["Price"]);
            p.ImageUrl = GetString(row, "ImageUrl");
            p.Description = GetString(row, "Description");
            p.NoiBat = GetString(row, "NoiBat");
            return p;
        }

        private string GetString(DataRow row, string col)
        {
            if (!row.Table.Columns.Contains(col))
            {
                return string.Empty;
            }

            if (row[col] == DBNull.Value)
            {
                return string.Empty;
            }

            return Convert.ToString(row[col]);
        }

        private DataTable GetFallbackTable()
        {
            DataTable tb = new DataTable();
            tb.Columns.Add("Id", typeof(int));
            tb.Columns.Add("ProductName", typeof(string));
            tb.Columns.Add("Brand", typeof(string));
            tb.Columns.Add("Price", typeof(decimal));
            tb.Columns.Add("ImageUrl", typeof(string));
            tb.Columns.Add("Description", typeof(string));
            tb.Columns.Add("NoiBat", typeof(string));

            foreach (Product sp in GetFallbackProducts())
            {
                tb.Rows.Add(sp.Id, sp.Name, sp.Brand, sp.Price, sp.ImageUrl, sp.Description, sp.NoiBat);
            }
            return tb;
        }

        private List<Product> GetFallbackProducts()
        {
            return new List<Product>
            {
                new Product
                {
                    Id = 1,
                    Name = "Laptop ASUS TUF A15",
                    Brand = "ASUS",
                    Price = 21990000,
                    ImageUrl = "https://images.unsplash.com/photo-1496181133206-80ce9b88a853?auto=format&fit=crop&w=700&q=60",
                    Description = "Laptop gaming tam trung manh me.",
                    NoiBat = "CPU hieu nang cao|RAM 16GB da nhiem tot|Man hinh 144Hz muot"
                },
                new Product
                {
                    Id = 2,
                    Name = "Man hinh LG UltraGear 27\"",
                    Brand = "LG",
                    Price = 5490000,
                    ImageUrl = "https://images.unsplash.com/photo-1527443224154-c4a3942d3acf?auto=format&fit=crop&w=700&q=60",
                    Description = "Man hinh 144Hz cho game va thiet ke.",
                    NoiBat = "Tam nen IPS mau dep|Tan so quet 144Hz|Thoi gian dap ung thap"
                },
                new Product
                {
                    Id = 3,
                    Name = "PC Gaming RTX 4060",
                    Brand = "DuyTech Build",
                    Price = 25990000,
                    ImageUrl = "https://images.unsplash.com/photo-1587202372775-989194f20f5d?auto=format&fit=crop&w=700&q=60",
                    Description = "Case gaming hieu nang cao, toi uu FPS.",
                    NoiBat = "Card do hoa RTX 4060|SSD NVMe toc do cao|Case thoang khi, de nang cap"
                },
                new Product
                {
                    Id = 4,
                    Name = "Ban phim co Keychron K2",
                    Brand = "Keychron",
                    Price = 1990000,
                    ImageUrl = "https://images.unsplash.com/photo-1511467687858-23d96c32e4ae?auto=format&fit=crop&w=700&q=60",
                    Description = "Ban phim co gon nhe, switch em.",
                    NoiBat = "Layout gon 75%|Ket noi da thiet bi|Pin dung lau"
                }
            };
        }
    }
}
