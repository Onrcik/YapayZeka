using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace YapayZekaÖdevi
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("                                        PROGRAMIN KURALLARI\n");
            Console.WriteLine("1-) Her hangi iki düğüm arasındaki en kısa mesafesi bulunmak istenen Graf matrise dönüştürülür.\n");
            Console.WriteLine("2-) Düğümlerin kendilerine uzaklıkları olmadığı için '0' olarak matriste gösterilmeli.\n");
            Console.WriteLine("3-) Matristki iki düğüm arasında doğrudan bağlantı yoksa sonsuz kabul edilir ve bu değeri kullanıcı kendi belirler.\n");
            Console.WriteLine("4-) İşlemlerde kolaylık olsun diye düğüm isimleri pozitif tam sayı olarak kullanıldı, graftaki düğümlerinizi buna göre isimlendiriniz.\n");
            Console.WriteLine("NOT: Grafın matri gösterimindeki değerleri doğru şekilde ve eksiksiz girilmelidir aksi taktirde progran yanlış değer üretir.\n");
            Console.WriteLine("________________________________________________________________________________\n\n");

            int N;
            int sonsuz;
            int K = 0;

            Console.Write("Grafınızda bulunan düğüm sayısını doğru bir şekilde giriniz= ");
            N = Convert.ToInt32(Console.ReadLine());

            Console.Write("Graf üzerinde sonsuz değer tanımı değerini gir= ");
            sonsuz = Convert.ToInt32(Console.ReadLine());

            int[,] D = new int[N, N]; // İki boyutlu 'D' matrisini tutan dizi.
            int[,] S = new int[N, N]; // İki boyutlu 'S' matrisini tutan dizi.

            //Kullanıcıdan matris değerleri alınıyor.
            for (int i = 0; i < N; i++)
            {
                for (int j = 0; j < N; j++)
                {
                    Console.Write("Matrisin " + (i + 1) + ". satir " + (j + 1) + ". sutun elemanını gir= ");
                    D[i, j] = Convert.ToInt32(Console.ReadLine());
                }
            }

            // S matrisinin değerlerini atama işlemi yapılıyor(Floyd algoritmasına göre).
            for (int i = 0; i < N; i++)
            {
                for (int j = 0; j < N; j++)
                {
                    if (i == j)
                    {
                        S[i, j] = 0;
                    }
                    else
                    {
                        S[i, j] = j + 1;
                    }
                }
            }

            // Bu for bloğu içerisinde kullanıcının verdiği matrise göre floyd ile hedef matris oluşturulur.iç içe 3 döngü ile gerçeklendi.
            while (K <= (N - 1))
            {
                for (int i = 0; i < N; i++)
                {
                    if (K == i)
                    {
                        continue;
                    }

                    for (int j = 0; j < N; j++)
                    {
                        if (K == j)
                        {
                            continue;
                        }

                        int hucre = D[i, j]; // O iterasyonda matrisin işlem yapılan hücresi. 
                        int karsilastirilanDeger = D[i, K] + D[K, j]; // En iyi yol için hücrenin kıyaslandığı yolu belirtir.

                        if (karsilastirilanDeger < hucre)
                        {
                            D[i, j] = karsilastirilanDeger;
                            S[i, j] = K + 1; // S matrisinde yapılan güncelleme.
                        }
                    }
                }
                K++;// Matris her iterasyonda bir defa baştan sona taranınca K değeri 1 arttırılır.
            }


            Console.WriteLine("Verilen matrise Floyd uygulandıktan sonra elde edilen matris; ");
            Console.WriteLine("Hesaplanan matrisin boyutu= " + N + "x" + N + " ve Graftaki sonsuz uzaklık değeri= " + sonsuz + " seçilmiştir!");
            Console.WriteLine("******************************************************************");
            // Floyd uygulanmış sonuç matrisi ekrana bastırılıyor. 
            for (int i = 0; i < N; i++)
            {
                for (int j = 0; j < N; j++)
                {
                    Console.Write("[" + (i + 1) + "," + (j + 1) + "]: " + D[i, j] + " | ");
                }
                Console.WriteLine();
            }
            Console.WriteLine("******************************************************************");



            // Bu kısımda kullanıcının hangi düğümler arasındaki en kısa yolu bulmak istiyorsa gereken işlemler yapılır.

            char tercih = 'E';

            do
            {
                int baslangicDogumu;
                int varisDugumu;
                bool rotaKontrol = true;

                List<int> hedefYol = new List<int>(); // Bu liste, 'S' matrisinden grafın başlangıç düğümünden itibaren yolu almak amacıyla oluşturuldu. 

                Console.Write("Aralarındaki en kısa yolu bulmak istediğiniz Başlangıç düğümünü girin= ");
                baslangicDogumu = Convert.ToInt32(Console.ReadLine());

                Console.Write("Aralarındaki en kısa yolu bulmak istediğiniz Varış düğümünü girin= ");
                varisDugumu = Convert.ToInt32(Console.ReadLine());

                hedefYol.Add(baslangicDogumu);
                hedefYol.Add(varisDugumu);

                // İzlenen rotayı tespit etmek için yapılan işlemler.
                while (rotaKontrol)
                {
                    int listeBoyu = hedefYol.Count();

                    for (int i = 0; i < listeBoyu - 1; i++)
                    {
                        int x = (hedefYol[i] - 1);
                        int y = (hedefYol[i + 1] - 1);
                        int deger = S[x, y];

                        if (deger != hedefYol[i + 1])
                        {
                            hedefYol.Insert(i + 1, deger);
                            break;
                        }
                        else if (i == (listeBoyu - 2) && deger == hedefYol[i + 1])
                        {
                            rotaKontrol = false;
                            break;
                        }
                    }

                }

                Console.WriteLine("*_____________________________________________________________________________*");
                Console.Write(baslangicDogumu + ". düğümden " + varisDugumu + ". düğüme en kısa yol= ");
                Console.WriteLine(D[baslangicDogumu - 1, varisDugumu - 1]);
                Console.Write("\nİzlenen rota = ");
                Console.WriteLine(string.Join("->", hedefYol.ToArray())); // Bu kod rotanın tutulduğu listeyi formatlı yazdırmaya yarar.
                Console.WriteLine("*_____________________________________________________________________________*");

                Console.Write("Başka iki düğüm arası uzaklık bulmak istermisiniz?(Birdaha için 'E' yada 'e'):");
                tercih = Convert.ToChar(Console.ReadLine());
                Console.WriteLine("_____________________________________________________________________________\n\n");
            } while (tercih == 'E' || tercih == 'e');

            Console.ReadKey();
        }
    }
}
       