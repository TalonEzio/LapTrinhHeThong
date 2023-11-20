using System.Text;

namespace ReadFile
{
    internal class Program
    {
        static void Main(string[] args)
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
            int seek = 3;
            string filePath = @"D:\Learn\C#\Console\LapTrinhHeThong\WriteFile\bin\Debug\net6.0\dulieu.txt";
            do
            {
                try
                {
                    using var stream = new FileStream(filePath, FileMode.Open);
                    using var reader = new StreamReader(stream,Encoding.ASCII);
                    if (seek >= stream.Length)
                    {
                        Console.WriteLine("Chưa có dữ liệu gì mới, ngủ luồng này 5s để chờ ghi dữ liệu!");
                        stream.Close();
                        reader.Close();
                        Thread.Sleep(5000);
                        continue;
                    }
                    stream.Seek(seek, SeekOrigin.Begin);
                    var keyChar = (char)reader.Read();
                    Console.WriteLine($"Dữ liệu đọc từ file: {keyChar}");
                    if (keyChar == '!')
                    {
                        Console.WriteLine("Đã nhận được lệnh dừng chương  trình");
                        break;
                    }
                    seek++;
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
