using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using HotChocolate.Subscriptions;
using HotChocolate.Types;
using HotChocolate;

namespace CarInfo.API.GraphQL
{
    public class SubscriptionAPI
    {
        [SubscribeAndResolve]
        public async IAsyncEnumerable<string> OnMessagesAsync()
        {
            yield return "1";
            await Task.Delay(2000);
            yield return "2";
            await Task.Delay(2000);
            yield return "3";
            await Task.Delay(2000);
            yield return "4";
        }

        [SubscribeAndResolve]
        public async ValueTask<IAsyncEnumerable<string>> OnCreateCarType([Service] ITopicEventReceiver topicEventReceiver)
        {
            return await topicEventReceiver.SubscribeAsync<string, string>("createCarType");
        }
    }
}
