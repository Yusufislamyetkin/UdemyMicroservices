﻿using StackExchange.Redis;

namespace FreeCourse.Services.Basket.Service
{
    // Appsettings dosyasındaki redis host ve port verilerini burada kullanarak bağlantıyı kuruyoruz ve temel crud operasyonlarını yönetilebilir kılıyoruz.
    public class RedisService
    {
        private readonly string _host;
        private readonly int _port;

        private ConnectionMultiplexer _connectionMultiplexer;

        public RedisService(string host, int port)
        {
            _host = host;
            _port = port;
        }

        public void Connect()=> _connectionMultiplexer = ConnectionMultiplexer.Connect($"{_host}:{_port}");

        public IDatabase GetDb(int db = 1) => _connectionMultiplexer.GetDatabase(db);

    }
}
