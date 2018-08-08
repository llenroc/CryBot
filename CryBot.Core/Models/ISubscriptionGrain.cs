﻿using System.Collections.Generic;
using System.Threading.Tasks;
using Orleans;

namespace CryBot.Core.Models
{
    public interface ISubscriptionGrain: IGrainWithStringKey
    {
        Task AddSubscription(WebSubscription subscription);
        Task<List<WebSubscription>> GetAllAsync();
    }
}