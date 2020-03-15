using Newtonsoft.Json;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace DiscogsConnect
{
    internal static class HttpClientExtensions
    {        
        public static async Task<T> GetAsync<T>(this HttpClient client, string requestUri)
        {
            var result = await client
                .GetStringAsync(requestUri)
                .ConfigureAwait(false);

            return JsonConvert.DeserializeObject<T>(result, DiscogsSerializerSettings.Default);
        }
        public static async Task<PaginationResponse<T>> GetPagedAsync<T>(this HttpClient client, string requestUri, int page = 1, int perPage = 100)
            => await client.GetAsync<PaginationResponse<T>>(requestUri + $"?page={page}&per_page={perPage}");
        
        public static async Task<List<T>> GetAllPagesAsync<T>(this HttpClient client, string requestUri) 
        {
            var result = new List<T>();

            var page = 1;
            Pagination pagination;

            do
            {
                var response = await client.GetPagedAsync<T>(requestUri, page++);
                result.AddRange(response.Items);
                pagination = response.Pagination;
            }
            while (pagination.Page != pagination.Pages);

            return result;
        }

        public static async Task PostAsync<T>(this HttpClient client, string requestUri, T content = null) where T : class
        {
            var httpContent = content == null ? null : 
                new StringContent(
                    JsonConvert.SerializeObject(content, DiscogsSerializerSettings.Default), 
                    Encoding.UTF8, 
                    "application/json");
            
            using (var response = await client.PostAsync(requestUri, httpContent))
            {
                response.EnsureSuccessStatusCode();                                
            }
        }
        public static async Task<TResult> PostAsync<T, TResult>(this HttpClient client, string requestUri, T content = null) where T : class
        {
            var httpContent = content == null ? null :
                new StringContent(
                    JsonConvert.SerializeObject(content, DiscogsSerializerSettings.Default),
                    Encoding.UTF8,
                    "application/json");

            using (var response = await client.PostAsync(requestUri, httpContent))
            {
                response.EnsureSuccessStatusCode();
                string responseBody = await response.Content.ReadAsStringAsync();
                return JsonConvert.DeserializeObject<TResult>(responseBody, DiscogsSerializerSettings.Default);                    
            }
        }        
    }
}
