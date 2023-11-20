using System.Text;

namespace WriteFile
{
    internal class Program
    {
        static void Main()
        {
            Console.InputEncoding = Console.OutputEncoding = Encoding.Unicode;

            var thread = new Thread(DoSomething);
            thread.Start();
            thread.Join();

            Console.WriteLine("Bấm phím bất kì để thoát");
            Console.ReadLine();

        }

        private static void DoSomething()
        {
            var filePath = Path.Combine(Directory.GetCurrentDirectory(), "dulieu.txt");
            do
            {
                Console.Write("Ký tự cần nhập vào: ");
                var keyInfo = Console.ReadKey();
                var keyChar = keyInfo.KeyChar;
                Console.WriteLine($"\nKý tự vừa nhập : {keyChar}");
                try
                {
                    using var stream = new FileStream(filePath, FileMode.Append);
                    using var writer = new StreamWriter(stream, Encoding.ASCII);
                    writer.Write(keyChar);
                    Console.WriteLine("Ghi ký tự vào tệp thành công!");
                    if (keyChar == '!')
                    {
                        Console.WriteLine("Đã nhận được lệnh dừng chương  trình");
                        break;
                    }
                }
                catch
                {
                    Console.WriteLine("Có luồng khác đang xử dụng file, không thể ghi ký tự này, hãy thử lại!");
                }
                Thread.Sleep(1000);
            } while (true);
        }
    }
}
