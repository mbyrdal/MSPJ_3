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

    public async Task<List<ProductInventoryDTO>> GetAllProductsAsync()
    {
        var response = await _httpClient.GetAsync("Products");
        response.EnsureSuccessStatusCode();
        var json = await response.Content.ReadAsStringAsync();
        return JsonConvert.DeserializeObject<List<ProductInventoryDTO>>(json);
    }

    public async Task<ProductInventoryDTO> GetProductByOEMAsync(string oem)
    {
        var response = await _httpClient.GetAsync($"Products/{oem}");
        response.EnsureSuccessStatusCode();
        var json = await response.Content.ReadAsStringAsync();
        return JsonConvert.DeserializeObject<ProductInventoryDTO>(json);
    }

    public async Task<ProductInventoryDTO> CreateProductAsync(ProductInventoryDTO product)
    {
        var json = JsonConvert.SerializeObject(product);
        var response = await _httpClient.PostAsync("Products", new StringContent(json, System.Text.Encoding.UTF8, "application/json"));
        response.EnsureSuccessStatusCode();
        var jsonResponse = await response.Content.ReadAsStringAsync();
        return JsonConvert.DeserializeObject<ProductInventoryDTO>(jsonResponse);
    }
    
    public void DeleteProductAsync(string oem)
    {
        var response = _httpClient.DeleteAsync($"Products/{oem}");
        response.Wait();
        response.Result.EnsureSuccessStatusCode();
    }
}
