using System;
using System.Text;
using System.Net;
using System.Net.Sockets;

//namespace SocketServer
//{
//    class Program
//    {
//        static void Main(string[] args)
//        {
//// Устанавливаем для сокета локальную конечную точку
//IPHostEntry ipHost = Dns.GetHostEntry("localhost");
//IPAddress ipAddr = ipHost.AddressList[0];
//IPEndPoint ipEndPoint = new IPEndPoint(ipAddr, 11000);

//// Создаем сокет Tcp/Ip
//Socket sListener = new Socket(ipAddr.AddressFamily, SocketType.Stream, ProtocolType.Tcp);

//// Назначаем сокет локальной конечной точке и слушаем входящие сокеты
//try
//{
//    sListener.Bind(ipEndPoint);
//    sListener.Listen(10);

//    // Начинаем слушать соединения
//    while (true)
//    {
//        Console.WriteLine("Ожидаем соединение через порт {0}", ipEndPoint);

//        // Программа приостанавливается, ожидая входящее соединение
//        Socket handler = sListener.Accept();
//        string data = null;

//        // Мы дождались клиента, пытающегося с нами соединиться

//        byte[] bytes = new byte[1024];
//        int bytesRec = handler.Receive(bytes);

//        data += Encoding.UTF8.GetString(bytes, 0, bytesRec);

//        // Показываем данные на консоли
//        Console.Write("Полученный текст: " + data + "\n\n");

//        // Отправляем ответ клиенту\
//        string reply = "Спасибо за запрос в " + data.Length.ToString()
//                + " символов";
//        byte[] msg = Encoding.UTF8.GetBytes(reply);
//        handler.Send(msg);

//        if (data.IndexOf("<TheEnd>") > -1)
//        {
//            Console.WriteLine("Сервер завершил соединение с клиентом.");
//            break;
//        }


//    }
//}
//catch (Exception ex)
//{
//    Console.WriteLine(ex.ToString());
//}
//finally
//{
//    Console.ReadLine();
//}
//        }
//    }
//}

var tcpListener = new TcpListener(IPAddress.Any, 8888);


    tcpListener.Start();    // запускаем сервер
    Console.WriteLine("Сервер запущен. Ожидание подключений... ");

    while (true)
    {
        // получаем подключение в виде TcpClient
        using var tcpClient = await tcpListener.AcceptTcpClientAsync();
        // получаем объект NetworkStream для взаимодействия с клиентом
        var stream = tcpClient.GetStream();
        // определяем данные для отправки - отправляем текущее время
        byte[] data = Encoding.UTF8.GetBytes(DateTime.Now.ToLongTimeString());
        // отправляем данные
        await stream.WriteAsync(data);
        Console.WriteLine($"Клиенту {tcpClient.Client.RemoteEndPoint} отправлены данные");
    }
