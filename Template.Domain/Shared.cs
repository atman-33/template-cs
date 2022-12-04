using System.Configuration;

namespace Template.Domain
{
    /// <summary>
    /// Shared
    /// </summary>
    public static class Shared
    {
        /// <summary>
        /// Fakeの時Trure（1:Fake）
        /// </summary>
        public static bool IsFake { get; } = (ConfigurationManager.AppSettings["IsFake"] == "1");

        /// <summary>
        /// SQLite 接続子
        /// </summary>
        public static string SQLiteConnectionString { get; } = ConfigurationManager.AppSettings["SQLiteConnectionString"];

        /// <summary>
        /// ログインID
        /// </summary>
        public static string LoginId { get; set; } = string.Empty;

        public static string OracleUser { get; } = ConfigurationManager.AppSettings["OracleUser"];
        public static string OraclePassword { get; } = ConfigurationManager.AppSettings["OraclePassword"];
        public static string OracleDataSource { get; } = ConfigurationManager.AppSettings["OracleDataSource"];

        public static string SampleListCsvPath { get; } = ConfigurationManager.AppSettings["SampleListCsvPath"];

        public static int TimerPeriod { get; } = Convert.ToInt32(ConfigurationManager.AppSettings["TimerPeriod"]);

        public static DateTime CurrentTime { get; set; } = DateTime.Now;
    }
}