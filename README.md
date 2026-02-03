# 🎓 EduCorePro - Online Eğitim ve Kurs Yönetim Platformu (LMS)

![C#](https://img.shields.io/badge/C%23-239120?style=for-the-badge&logo=c-sharp&logoColor=white)
![ASP.NET](https://img.shields.io/badge/ASP.NET-512BD4?style=for-the-badge&logo=.net&logoColor=white)
![MSSQL](https://img.shields.io/badge/SQL_Server-CC2927?style=for-the-badge&logo=microsoft-sql-server&logoColor=white)
![Bootstrap](https://img.shields.io/badge/Bootstrap-563D7C?style=for-the-badge&logo=bootstrap&logoColor=white)

[cite_start]**EduCorePro**, öğrencilerin çeşitli kategorilerdeki eğitim içeriklerine erişebileceği, yöneticilerin ise bu içerikleri dinamik olarak yönetebileceği, ASP.NET Web Forms teknolojisi ve N-Katmanlı mimari kullanılarak geliştirilmiş kapsamlı bir Öğrenim Yönetim Sistemi (LMS) projesidir[cite: 4, 25].

---

## 🚀 Projenin Amacı

Bu projenin temel amacı, klasik eğitim materyallerinin dijital ortamda yönetilebilirliğini sağlamak ve kullanıcı etkileşimli bir web mimarisi oluşturmaktır. [cite_start]Proje; dinamik veri yönetimi, güvenli oturum (session) kontrolü ve ilişkisel veritabanı mimarisi üzerine kurgulanmıştır[cite: 6, 8, 10].

---

## 🛠️ Kullanılan Teknolojiler ve Mimari

* [cite_start]**Backend:** C# (.NET Framework 4.7.2), ASP.NET Web Forms [cite: 26]
* [cite_start]**Veritabanı:** Microsoft SQL Server (LocalDB), ADO.NET [cite: 27]
* [cite_start]**Frontend:** HTML5, CSS3, Bootstrap 5.3.0 [cite: 28]
* [cite_start]**Mimari:** N-Tier (Çok Katmanlı) Mimari, Dinamik Connection String Yapısı [cite: 25, 39]

---

## ✨ Özellikler

[cite_start]Proje **Öğrenci** ve **Yönetici (Admin)** olmak üzere iki temel modülden oluşmaktadır[cite: 13].

### 👤 Öğrenci (Kullanıcı) Modülü
* [cite_start]**Kurs Listeleme & Filtreleme:** Yazılım, dil, veritabanı gibi kategorilere göre kursları filtreleme[cite: 15].
* [cite_start]**Sepet İşlemleri:** Beğenilen kursları sepete ekleme ve sipariş simülasyonu[cite: 16].
* [cite_start]**Profil Yönetimi:** Şifre ve iletişim bilgilerini güvenli bir şekilde güncelleme[cite: 17].

### 🛡️ Yönetici (Admin) Modülü
* [cite_start]**CRUD İşlemleri:** Yeni kurs ekleme, güncelleme, silme ve kullanıcı yönetimi[cite: 19, 20].
* **Gelişmiş Raporlama:**
    * [cite_start]**Inner Join Analizi:** Hangi kursun hangi eğitmen tarafından verildiğini gösteren birleştirilmiş raporlar[cite: 69].
    * [cite_start]**Nested Select (İç İçe Sorgu):** Belirli kategorilerden ders alan öğrencilerin analizini yapan 3 katmanlı SQL sorguları[cite: 83].

---

## 🗄️ Veritabanı Şeması

[cite_start]Proje ilişkisel veritabanı yapısına sahip olup 4 ana tablodan oluşmaktadır[cite: 51]:

1.  **Users:** Kullanıcı ve yönetici giriş bilgileri.
2.  **Courses:** Kurs başlığı, fiyatı, görseli ve kategori bilgileri.
3.  **Instructors:** Eğitmen bilgileri.
4.  **Orders:** Kullanıcıların satın aldığı kurs sipariş kayıtları.

---

## ⚙️ Kurulum ve Çalıştırma (How to Run)

Proje **LocalDB** mimarisi kullandığı için ekstra bir SQL Server kurulumu gerektirmez. [cite_start]Aşağıdaki adımları izleyerek projeyi çalıştırabilirsiniz[cite: 37, 39]:

1.  **Projeyi İndirin:**
    ```bash
    git clone [https://github.com/KULLANICI_ADINIZ/EduCorePro.git](https://github.com/KULLANICI_ADINIZ/EduCorePro.git)
    ```
2.  **Projeyi Açın:**
    `EduCorePro.sln` dosyasına çift tıklayarak Visual Studio'da açın.
3.  **Veritabanı Bağlantısı:**
    Veritabanı dosyası (`EduCoreProDB.mdf`) `App_Data` klasörü içerisindedir. `Web.config` dosyasındaki dinamik bağlantı ayarı sayesinde herhangi bir işlem yapmanıza gerek yoktur.
4.  **Çalıştırın:**
    Visual Studio'da `F5` tuşuna basın veya **IIS Express** butonuna tıklayın.

### 🔑 Giriş Bilgileri (Örnek)
* **Admin Girişi:** `admin@educore.com` / `123456`
* **Öğrenci Girişi:** Kayıt ol ekranından yeni üyelik oluşturabilirsiniz.

---

## 👩‍💻 Geliştirici

[cite_start]**Rozerin OFLAS** - *Bilgisayar Mühendisliği Öğrencisi* [cite: 93]

---
