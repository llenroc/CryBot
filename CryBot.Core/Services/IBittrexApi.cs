﻿using System;
using CryBot.Core.Models;

using System.Threading.Tasks;
using System.Collections.Generic;

namespace CryBot.Core.Services
{
    public interface ICryptoApi
    {
        event EventHandler<List<Ticker>> MarketsUpdated;

        void Initialize(string apiKey, string apiSecret);

        Task<CryptoResponse<Wallet>> GetWalletAsync();

        Task<CryptoResponse<List<CryptoOrder>>> GetOpenOrdersAsync();

        Task<CryptoResponse<List<CryptoOrder>>> GetCompletedOrdersAsync();

        Task<CryptoResponse<CryptoOrder>> BuyCoinAsync(CryptoOrder cryptoOrder);

        Task<CryptoResponse<Ticker>> GetTickerAsync(string market);
    }
}
