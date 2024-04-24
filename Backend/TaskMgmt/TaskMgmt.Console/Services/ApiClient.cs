using System;
using System.Net.Http;
using System.Text;
using System.Text.Json;

public class ApiClient
{
    private readonly HttpClient httpClient;

    public ApiClient(string baseAddress)
    {
        httpClient = new HttpClient
        {
            BaseAddress = new Uri(baseAddress)
        };
    }

    public HttpResponseMessage Get(string endpoint)
    {
        HttpResponseMessage response = httpClient.GetAsync(endpoint).Result;
        return response;
    }

    public HttpResponseMessage GetToken(string endpoint, string token)
    {
        httpClient.DefaultRequestHeaders.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", token);
        HttpResponseMessage response = httpClient.GetAsync(endpoint).Result;
        return response;
    }

    public HttpResponseMessage Post(string endpoint, object data)
    {
        string jsonData = JsonSerializer.Serialize(data);
        HttpContent content = new StringContent(jsonData, Encoding.UTF8, "application/json");

        HttpResponseMessage response = httpClient.PostAsync(endpoint, content).Result;
        return response;
    }

    public HttpResponseMessage PostToken(string endpoint, object data, string token)
    {
        string jsonData = JsonSerializer.Serialize(data);
        HttpContent content = new StringContent(jsonData, Encoding.UTF8, "application/json");
        httpClient.DefaultRequestHeaders.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", token);
        HttpResponseMessage response = httpClient.PostAsync(endpoint, content).Result;
        return response;
    }
}
    //    return await HandleResponse<T>(response);
    //}

    //private async Task<ApiResponse<T>> HandleResponse<T>(HttpResponseMessage response)
    //{
    //    ApiResponse<T> apiResponse = new ApiResponse<T>();
    //    apiResponse.StatusCode = (int)response.StatusCode;
    //    if (response.IsSuccessStatusCode)
    //    {
    //        string content = await response.Content.ReadAsStringAsync();
    //        apiResponse.Result = JsonSerializer.Deserialize<T>(content);
    //    }
    //    else
    //    {
    //        throw new HttpRequestException($"HTTP request failed with status code {response.StatusCode} - {response.ReasonPhrase}");
    //    }
    //    return apiResponse;
    //}
