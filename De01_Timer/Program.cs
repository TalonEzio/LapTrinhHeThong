namespace De01_Timer
{
    internal class Program
    {
        private static object LockObject = new object();
        private static bool isStopTimer1 = false, isStopTimer2 = false;
        private static Timer timer1,timer2;
        private static int c;
        private static readonly CountdownEvent CountdownEvent = new CountdownEvent(2);

        static void Main(string[] args)
        {
            
            timer1 = new Timer(NhapSoTuBanPhim, null, 0, 5);
            timer2 = new Timer(HienThi, null, 0, 10);

            CountdownEvent.Wait();// cho countdown ve 0
            Console.WriteLine("Bam phim bat ki de ket thuc");
            Console.ReadLine();
        }

        static void NhapSoTuBanPhim(object? state)
        {
            lock (LockObject)
            {
                if (isStopTimer1)
                {
                    return;
                }

                bool isNumber = false;

                Console.ForegroundColor = ConsoleColor.Cyan;
                do
                {
                    Console.Write("Nhap so: ");
                    string input = Console.ReadLine();

                    isNumber = int.TryParse(input, out c);
                    if (!isNumber)
                    {
                        Console.WriteLine("Du lieu nhap vao khong hop le, vui long thu lai");
                    }
                } while (!isNumber);

                Console.ResetColor();
                if (c == 0)
                {
                    isStopTimer1 = true;
                    timer1.Dispose();
                    CountdownEvent.Signal();
                }
            }
            
        }

        static void HienThi(object? state)
        {
            lock (LockObject)
            {
                if (isStopTimer2)
                {
                    return;
                }

                if (c == 0)
                {
                    isStopTimer2 = true;
                    timer2.Dispose();
                    CountdownEvent.Signal();
                    return;
                }

                Console.ForegroundColor = ConsoleColor.Green;

                Console.WriteLine("Gia tri cua bien c: " + c);
                if (LaSoNguyenTo(c))
                {
                    Console.WriteLine(c + " la so nguyen to");
                }
                else
                {
                    Console.WriteLine(c + " khong la so nguyen to");

                }
                Console.ResetColor();


            }

        }

        static bool LaSoNguyenTo(int x)
        {
            if (x < 2) return false;
            for (int i = 2; i * i <= x; ++i)
            {
                if (x % i == 0) return false;
            }

            return true;
        }
    }
}
