﻿using RestSharp;
using RestSharp.Deserializers;
using RestSharp.Serializers;
using SageOneApi.Interfaces;
using SageOneApi.Models;

namespace SageOneApi.Requests
{
    public class AdditionalItemPriceRequest : RequestBase, IAdditionalItemPriceRequest
    {
        public AdditionalItemPriceRequest(IRestClient client, string apiKey, int companyId) : base(client, apiKey, companyId) { }

        public AdditionalItemPrice Get(int id)
        {
            var response = _client.Execute<AdditionalItemPrice>(new RestRequest(string.Format("AdditionalItemPrice/Get/{0}?apikey={1}&companyid={2}", id, _apiKey, _companyId), Method.GET));
            return response.Data;
        }

        public PagingResponse<AdditionalItemPrice> Get(string filter = "", int skip = 0)
        {
            var url = string.Format("AdditionalItemPrice/Get?apikey={0}&companyid={1}", _apiKey, _companyId);

            if (!string.IsNullOrEmpty(filter))
                url = string.Format("AdditionalItemPrice/Get?apikey={0}&companyid={1}&$filter={2}", _apiKey, _companyId, filter);

            if (skip > 0)
                url += "&$skip=" + skip;

            var request = new RestRequest(url, Method.GET) { RequestFormat = DataFormat.Json };
            var response = _client.Execute(request);
            JsonDeserializer deserializer = new JsonDeserializer();

            return deserializer.Deserialize<PagingResponse<AdditionalItemPrice>>(response);
        }

        public AdditionalItemPrice Save(AdditionalItemPrice additionalItemPrice)
        {
            var url = string.Format("AdditionalItemPrice/Save?apikey={0}&companyid={1}", _apiKey, _companyId);
            var request = new RestRequest(url, Method.POST) { JsonSerializer = new JsonSerializer() };
            request.RequestFormat = DataFormat.Json;
            request.AddBody(additionalItemPrice);
            var response = _client.Execute<AdditionalItemPrice>(request);
            return response.Data;
        }

        public bool Delete(int id)
        {
            var url = string.Format("AdditionalItemPrice/Delete/{0}?apikey={1}&companyid={2}", id, _apiKey, _companyId);
            var response = _client.Execute<AdditionalItemPrice>(new RestRequest(url, Method.DELETE));
            return response.ResponseStatus == ResponseStatus.Completed;
        }
    }
}