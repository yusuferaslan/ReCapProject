using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Utilities.Helpers.FileHelper
{
    public class FileHelperManager : IFileHelper
    {
        public string Upload(IFormFile formFile, string root)
        {
            //IFormFile ASP.NET Core MVC ve Web API projelerinde, HTTP isteği gövdesinden gelen dosya verilerini temsil eden bir arabirimdir. 
            //Bu arabirim, HTTP isteğinin dosya yükleme veya form verilerini içeren bölümlerini işlemek için kullanılır. IFormFile nesnesi, dosyanın içeriğini, adını, uzantısını vb. taşıyan özelliklere sahiptir.
            //Kaynak: https://learn.microsoft.com/tr-tr/dotnet/api/microsoft.aspnetcore.http.iformfile?view=aspnetcore-7.0                  

            if (formFile.Length > 0)  //formFile.Length >> Gönderilen dosyanın uzunluğunu kontrol eder. Dosya uzunluğu 0'dan büyükse, yani gerçekten bir dosya gönderilmişse, işlemler devam eder.
            {
                if (!Directory.Exists(root))      //Directory >> System.IO'nın bir class'ı. Dizinleri ve alt dizinleri oluşturmak, taşımak ve numaralandırmak için statik yöntemleri kullanıma sunar. Exists >> Verilen yolun diskte var olan bir dizine başvurup başvurmayacağını belirler.
                {                                 //CarImageManager içerisine giderek parametrenin(root) karşılığı olarak "PathConstants.ImagesPath" verilmiştir. PathConstants clası takip edilirse (root) parametresine karşılık "wwwroot\\Uploads\\Images\\" dosya yolu demektir.
                                                  //Yani (root) = "wwwroot\\Uploads\\Images\\" dosyanın kaydedileceği adres dizini var mı kontrolunu yapar varsa adrese yazar yoksa dosyaların kayıt edilecek dizinini oluşturur
                    Directory.CreateDirectory(root);
                }
                string extension = Path.GetExtension(formFile.FileName); //Gönderilen dosyanın uzantısı elde edilir.
                string guid = Guid.NewGuid().ToString(); //Guid.NewGuid() >>> Eşsiz bir değer oluşturur.. ToString() >>> String hale getirir.
                string filePath = guid + extension; //Oluşturulan eşsiz değer (GUID) ve dosya uzantısı birleştirilerek yeni dosya adı elde edilir.

                using (FileStream fileStream = File.Create(root + filePath)) //Dosyanın kaydedileceği yeni yol ve adı kullanılarak bir FileStream oluşturur. Bu nesne, dosyanın içeriğini kopyalamak ve hedef dizine kaydetmek için kullanılır. Eğer aynı isimde bir dosya bulunuyorsa üzerine yazılır.
                {
                    formFile.CopyTo(fileStream); //IFormFile nesnesinin içeriğini oluşturulan FileStream nesnesine kopyalar. Yani, dosyanın içeriği yeni yere kopyalanır.
                    fileStream.Flush(); //Dosyanın içeriğini tampon bellekten gerçek diske yazmaya zorlar.Bellekten temizler.
                    return filePath; //Sql servere dosyayı adı ile eklemek için eşsiz değer ve uzantısı ile oluşan yeni adını geri gönderir.
                }
            }
            return null; //Dosya yükleme işlemi tamamlanmışsa filePath döndürülür, aksi halde null döndürülür.
        }

        public void Delete(string filePath) //String filePath >> 'CarImageManager'dan gelen dosyanın kaydedildiği adresini ve adını temsil eder.
        {
            if (File.Exists(filePath)) //Belirtilen filePath'deki dosyanın var olup olmadığını kontrol eder.
            {
                File.Delete(filePath); //Eğer dosya var ise bulunduğu yerden silinir.
            }
        }

        public string Update(IFormFile file, string filePath, string root) //Dosya güncellemek için gelen parametreler >> Güncellenecek yeni dosya, eski dosyanın kaydedildiği adres ve ad, ve yeni bir kayıt dizini
        {
            if (File.Exists(filePath)) //Belirtilen filePath'deki dosyanın var olup olmadığını kontrol eder.
            {
                File.Delete(filePath); //Eğer dosya var ise dosya bulunduğu yerden siliniyor.
            }
            return Upload(file, root); //Eski dosya silindikten sonra yerine geçecek yeni dosyanın *Upload* metoduna gönderilmesini sağlar. yeni dosya ve kayıt edileceği adres parametre olarak döndürülür.
        }       

    }
}

