using System.Text;

namespace De05_Timer
{
    internal class Program
    {
        private static object LockObject = new object();
        private static bool isStopTimer1 = false, isStopTimer2 = false;
        private static Timer timer1, timer2;
        private static readonly CountdownEvent CountdownEvent = new CountdownEvent(2);
        private static string filePath = "D:\\dulieu.txt";
        static void Main(string[] args)
        {
            timer1 = new Timer(NhapChuoiVaoFile, null, 0, 5);
            timer2 = new Timer(DocChuoiTuFile, null, 0, 10);


            CountdownEvent.Wait();

            Console.ResetColor();
            Console.WriteLine("Bam phim bat ki de ket thuc");
            Console.ReadLine();
        }

        static void NhapChuoiVaoFile(object? state)
        {
            lock (LockObject)
            {
                if (isStopTimer1)
                {
                    //Console.WriteLine("Timer1 da dung");
                    return;
                }


                Console.ForegroundColor = ConsoleColor.Green;
                string s = String.Empty;

                Console.Write("Nhap chuoi: ");
                s = Console.ReadLine() ?? "";

                if (s.ToLower().Equals("thoat"))
                {
                    isStopTimer1 = true;
                    timer1.Dispose();
                    CountdownEvent.Signal();
                    return;
                }

                using var stream = new FileStream(filePath, FileMode.Open);
                using var writer = new StreamWriter(stream, Encoding.ASCII);
                writer.WriteLine(s);
                Console.WriteLine("Ghi du lieu vao file thanh cong");
                Console.ResetColor();
            }
        }

        static void DocChuoiTuFile(object? state)
        {
            lock (LockObject)
            {
                if (isStopTimer1)
                {
                    //Console.WriteLine("Timer2 da dung");
                    timer2.Dispose();


                    if (CountdownEvent.CurrentCount == 1)
                    {
                        CountdownEvent.Signal();
                    }
                    return;
                }


                Console.ForegroundColor = ConsoleColor.Cyan;
                using var fileStream = new FileStream(filePath, FileMode.Open);
                using var reader = new StreamReader(fileStream, Encoding.ASCII);
                string s = reader.ReadLine() ?? "";

                Console.WriteLine("Chuoi doc duoc tu file: " + s);

                string reverseS = String.Concat(s.Reverse());

                Console.WriteLine("Chuoi dao nguoc : " + reverseS);
                Console.ResetColor();

            }
        }
    }
}
