﻿using EmployeeManagement.Business;
using System.Net;
using System.Text;
using System.Text.Json;

namespace EmployeeManagement.Test.HttpMessageHandlers
{
    public class TestablePromotionEligibilityHandler(bool isEligibleForPromotion) : HttpMessageHandler
    {
        private readonly bool _isEligibleForPromotion = isEligibleForPromotion;

        protected override Task<HttpResponseMessage> SendAsync(HttpRequestMessage request, CancellationToken cancellationToken)
        {
            var promotionEligibility = new PromotionEligibility()
            { 
                EligibleForPromotion = _isEligibleForPromotion

            };

            var response = new HttpResponseMessage(HttpStatusCode.OK)
            {
                Content = new StringContent(
                    JsonSerializer.Serialize(
                        promotionEligibility,
                        new JsonSerializerOptions { PropertyNamingPolicy = JsonNamingPolicy.CamelCase }
                     ),
                    Encoding.ASCII,
                    "application/json"
                    )
            };  
            
            return Task.FromResult(response);
        }
    }
}
