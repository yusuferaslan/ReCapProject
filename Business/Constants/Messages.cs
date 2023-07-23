using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business.Constants
{
    public static class Messages
    {
        public static string CarAdded = "Araç Eklendi";
        public static string CarNotAdded = "Araç Eklenmedi; Model ismi minimum iki karakter ve günlük fiyat 0'dan büyük olmalıdır";
        public static string CarsListed = "Araçlar Listelendi";
        public static string MaintenanceTime = "Sistem Bakımda";
        public static string CarUpdated = "Araç Güncellendi";
        public static string CarDeleted = "Araç Silindi";

        public static string BrandAdded = "Marka Eklendi";
        public static string BrandsListed = "Markalar Listelendi";
        public static string BrandIdListed = "Girilen Marka Id Listelendi";
        public static string BrandUpdated = "Marka Güncellendi";
        public static string BrandDeleted = "Marka Silindi";

        public static string ColorAdded = "Renk Eklendi";
        public static string ColorNotAdded = "Renk Eklenmedi; Renk ismi minimum iki karakter olmalıdır";
        public static string ColorsListed = "Renkler Listelendi";
        public static string ColorIdListed = "Girilen Renk Id Listelendi";
        public static string ColorUpdated = "Renk Güncellendi";
        public static string ColorDeleted = "Renk Silindi";

        public static string UserAdded = "Kullanıcı Eklendi";        
        public static string UsersListed = "Kullanıcılar Listelendi";
        public static string UserIdListed = "Girilen Kullanıcı Id Listelendi";
        public static string UserUpdated = "Kullanıcı Güncellendi";
        public static string UserDeleted = "Kullanıcı Silindi";

        public static string CustomerAdded = "Müşteri Eklendi";
        public static string CustomersListed = "Müşteriler Listelendi";
        public static string CustomerIdListed = "Girilen Müşteri Id Listelendi";
        public static string CustomerUpdated = "Müşteri Güncellendi";
        public static string CustomerDeleted = "Müşteri Silindi";

        public static string RentalNotAdded = "Kiralama Yapılamadı; Arabanın kiralanabilmesi için arabanın teslim edilmesi gerekmektedir (Teslim tarihi girilmelidir).";
        public static string RentalAdded = "Kiralama Yapıldı";
    }
}
