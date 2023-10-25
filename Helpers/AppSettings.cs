namespace TexCode.Helpers
{
    public class AppSettings
    {
        public string? Secret { get; set; }
        public string ShelfJsonFileData { get; set; }

        // refresh token time to live (in days), inactive tokens are
        // automatically deleted from the database after this time
        public string? RefreshTokenTTL { get; set; }
        public string? EmailFrom { get; set; }
        public string? SmtpHost { get; set; }
        public int SmtpPort { get; set; }
        public string? SmtpUser { get; set; }
        public string? SmtpPass { get; set; }
    }
}
