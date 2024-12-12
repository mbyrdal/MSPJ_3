using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;
using Newtonsoft.Json;
using ClientDesktop.DTOs;

public class ApiClient
{
    private readonly HttpClient _httpClient;

    public ApiClient(string baseUrl)
    {
        _httpClient = new HttpClient { BaseAddress = new Uri(baseUrl) };
    }

    public async Task<List<ProductInventoryViewModel>> GetAllProductsAsync()
    {
        var response = await _httpClient.GetAsync("Products");
        response.EnsureSuccessStatusCode();
        var json = await response.Content.ReadAsStringAsync();
        return JsonConvert.DeserializeObject<List<ProductInventoryViewModel>>(json);
    }

    public async Task<ProductInventoryViewModel> GetProductByOEMAsync(string oem)
    {
        var response = await _httpClient.GetAsync($"Products/{oem}");
        response.EnsureSuccessStatusCode();
        var json = await response.Content.ReadAsStringAsync();
        return JsonConvert.DeserializeObject<ProductInventoryViewModel>(json);
    }
}
