# Hastane Randevu Sistemi
Hastane Randevu Sistemi Web Programlama Ödevi

Admin girişi, hasta girişi ve girişsiz olarak 3 parça olan sitemizde; sunucu tarafında doktorların branşları, isimleri, çalışma gün ve saatleri, ekleme-güncelleme-silme işlemleri(CRUD), tüm bu bilgilerin çekildiği SQLServer veri tabanı bulunmakla beraber, hem hastaların hem de adminin randevu kaydı oluşturabileceği ve bunların veri tabanında aynı tabloya kaydedileceği bir algoritma düzenlenmiştir.
Proje kodlarımızın, S.O.L.I.D. prensiplerine uygun olarak yazılmasına gayret edilmiştir.(esnek, clean code'a uygun, arabirimli, gereksiz koddan ve sınıftan kaçınma)
Login yapılmadığı takdirde sadece "Anasayfa" ve "Bize Ulaşın" butonları aktif olurken, hasta olarak login yapıldığında bu ikisine ek olarak "Doktorlar" butonu da gelmektedir. Bu 3. butonun içeriğinde hasta, doktorun tüm verilerini görmekle beraber sadece randevu alabilmektedir.
Admin ise yukarıda belirtildiği gibi tüm verileri erişebilir ve değiştirebilir pozisyondadır.
