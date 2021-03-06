﻿using CryBot.Core.Trader;
using CryBot.Core.Storage;
using CryBot.Core.Strategies;
using CryBot.Core.Exchange.Models;
using CryBot.UnitTests.Infrastructure;

using FluentAssertions;

using Moq;

using System.Reactive.Subjects;
using System.Collections.Generic;

using Xunit;

namespace CryBot.UnitTests.Services.CryptoBrokerTests
{
    public class InitializeTests : CoinTraderTestBase
    {
        [Fact]
        public void CoinTrader_ShouldBe_InitializedWithAMarket()
        {
            CryptoBroker.Initialize(new TraderState{Market = Market});
            CryptoBroker.Market.Should().Be(Market);
        }

        [Fact]
        public void Initialize_ShouldSet_TraderData()
        {
            CryptoBroker.Initialize(new TraderState{Market = Market});
            CryptoBroker.Strategy.Should().NotBeNull();
            CryptoBroker.TraderState.Trades.Should().NotBeNull();
        }

        [Fact]
        public void TraderWithNoTrades_Should_CreateFirstTrade()
        {
            CryptoBroker.Initialize(new TraderState{Market = Market});
            CryptoBroker.TraderState.Trades.Count.Should().Be(1);
        }

        [Fact]
        public void UpdatePrice_ShouldNot_RunConcurrently()
        {
            var subject = new Subject<Ticker>();
            CryptoApiMock.MockBuyingTrade(new CryptoOrder());
            CryptoApiMock.SetupGet(c => c.TickerUpdated).Returns(subject);
            InitializeTrader(new TradeAction { TradeAdvice = TradeAdvice.Buy, OrderPricePerUnit = 1});

            for (int i = 0; i < 100; i++)
            {
                subject.OnNext(new Ticker { Market = Market });
            }

            Strategy.Verify(s => s.CalculateTradeAction(It.IsAny<Ticker>(), It.IsAny<Trade>()), Times.Exactly(100));
        }
    }
}
