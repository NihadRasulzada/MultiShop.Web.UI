namespace MultiShop.Web.UI.Extensions
{
    public static class MiddlewareExtensions
    {
        // Bu metot, WebApplication'ı uzatıyor ve middleware'leri buraya ekliyor
        public static void UseMiddlewares(this WebApplication app)
        {
            if (!app.Environment.IsDevelopment())  // Geliştirme ortamında değilse özel işlemler
            {
                app.UseExceptionHandler("/Home/Error");  // Hata sayfası
                app.UseHsts();  // HTTP Strict Transport Security
            }

            app.UseHttpsRedirection();  // HTTPS yönlendirmesi
            app.UseStaticFiles();  // Statik dosyalar
            app.UseRouting();  // Routing middleware'i
            app.UseAuthentication();  // Kimlik doğrulama
            app.UseAuthorization();  // Yetkilendirme

            // Controller route ayarları
            app.MapControllerRoute(
                name: "areas",
                pattern: "{area:exists}/{controller=Home}/{action=Index}/{id?}");
            app.MapControllerRoute(
                name: "default",
                pattern: "{controller=Default}/{action=Index}/{id?}");
        }
    }
}
