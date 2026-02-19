using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.OleDb;
using System.IO;
using System.Web;

namespace DemoAsp
{
    public class DataProvider
    {
        private static readonly string strConn = ConfigurationManager.ConnectionStrings["AccessDb"] != null
            ? ConfigurationManager.ConnectionStrings["AccessDb"].ConnectionString
            : string.Empty;

        private static OleDbConnection MoKetNoi()
        {
            Exception loiCuoi = null;
            List<string> danhSach = LayDanhSachChuoiKetNoi();

            foreach (string cs in danhSach)
            {
                try
                {
                    OleDbConnection conn = new OleDbConnection(cs);
                    conn.Open();
                    return conn;
                }
                catch (Exception ex)
                {
                    loiCuoi = ex;
                }
            }

            throw new Exception("Khong the ket noi Access. " + (loiCuoi != null ? loiCuoi.Message : ""));
        }

        private static List<string> LayDanhSachChuoiKetNoi()
        {
            List<string> ds = new List<string>();
            string cs = ChuanHoaDataDirectory(strConn ?? string.Empty);
            ThemNeuChuaCo(ds, cs);

            string lower = cs.ToLowerInvariant();

            if (lower.Contains("provider=microsoft.ace.oledb.12.0"))
            {
                ThemNeuChuaCo(ds, ThayProvider(cs, "Microsoft.ACE.OLEDB.12.0", "Microsoft.Jet.OLEDB.4.0"));
            }
            else if (lower.Contains("provider=microsoft.jet.oledb.4.0"))
            {
                ThemNeuChuaCo(ds, ThayProvider(cs, "Microsoft.Jet.OLEDB.4.0", "Microsoft.ACE.OLEDB.12.0"));
            }

            return ds;
        }

        private static string ChuanHoaDataDirectory(string chuoiKetNoi)
        {
            if (string.IsNullOrWhiteSpace(chuoiKetNoi))
            {
                return chuoiKetNoi;
            }

            if (chuoiKetNoi.IndexOf("|DataDirectory|", StringComparison.OrdinalIgnoreCase) < 0)
            {
                return chuoiKetNoi;
            }

            string duongDanData = Convert.ToString(AppDomain.CurrentDomain.GetData("DataDirectory"));
            if (string.IsNullOrWhiteSpace(duongDanData))
            {
                string appPath = HttpRuntime.AppDomainAppPath;
                if (!string.IsNullOrWhiteSpace(appPath))
                {
                    duongDanData = Path.Combine(appPath, "App_Data");
                }
            }

            if (string.IsNullOrWhiteSpace(duongDanData))
            {
                return chuoiKetNoi;
            }

            return ThayProvider(chuoiKetNoi, "|DataDirectory|", duongDanData);
        }

        private static void ThemNeuChuaCo(List<string> ds, string cs)
        {
            if (string.IsNullOrWhiteSpace(cs))
            {
                return;
            }

            for (int i = 0; i < ds.Count; i++)
            {
                if (string.Equals(ds[i], cs, StringComparison.OrdinalIgnoreCase))
                {
                    return;
                }
            }

            ds.Add(cs);
        }

        private static string ThayProvider(string chuoiKetNoi, string providerCu, string providerMoi)
        {
            int viTri = chuoiKetNoi.IndexOf(providerCu, StringComparison.OrdinalIgnoreCase);
            if (viTri < 0)
            {
                return chuoiKetNoi;
            }

            return chuoiKetNoi.Substring(0, viTri)
                + providerMoi
                + chuoiKetNoi.Substring(viTri + providerCu.Length);
        }

        public static DataTable LayBang(string sql, params OleDbParameter[] prms)
        {
            DataTable tb = new DataTable();

            using (OleDbConnection conn = MoKetNoi())
            using (OleDbCommand cmd = new OleDbCommand(sql, conn))
            using (OleDbDataAdapter da = new OleDbDataAdapter(cmd))
            {
                if (prms != null && prms.Length > 0)
                {
                    cmd.Parameters.AddRange(prms);
                }

                da.Fill(tb);
            }

            return tb;
        }

        public static int ThucThi(string sql, params OleDbParameter[] prms)
        {
            using (OleDbConnection conn = MoKetNoi())
            using (OleDbCommand cmd = new OleDbCommand(sql, conn))
            {
                if (prms != null && prms.Length > 0)
                {
                    cmd.Parameters.AddRange(prms);
                }

                return cmd.ExecuteNonQuery();
            }
        }

        public static object LayGiaTri(string sql, params OleDbParameter[] prms)
        {
            using (OleDbConnection conn = MoKetNoi())
            using (OleDbCommand cmd = new OleDbCommand(sql, conn))
            {
                if (prms != null && prms.Length > 0)
                {
                    cmd.Parameters.AddRange(prms);
                }

                return cmd.ExecuteScalar();
            }
        }
    }
}
