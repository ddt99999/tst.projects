﻿using System;
using System.Net.Http;
using System.Threading.Tasks;

namespace PhoenixSea.Trading.Core.Services
{
    public class DataDownloadService : IDataDownloadService
    {
        private static readonly HttpClient _httpClient = new HttpClient();
        public async Task<string> DownloadAsync(string url)
        {
            var response = await _httpClient.GetAsync(new Uri(url));
            var result = await response.Content.ReadAsStringAsync();
            return result;
        }
    }
}