﻿using Orleans;

using System.Threading.Tasks;
using System.Collections.Generic;
using CryBot.Core.Models.Grains;

namespace CryBot.Core.Models
{
    public interface ITraderGrain: IGrainWithStringKey
    {
        Task UpdatePriceAsync(Ticker ticker);

        Task AddTradeAsync(Trade trade);

        Task<List<Trade>> GetActiveTrades();

        Task<TraderSettings> GetSettings();

        Task<TraderState> GetTraderData();

        Task UpdateTrades(List<Trade> trades);
        Task SetMarketAsync(string market);
        Task<bool> IsInitialized();
    }
}