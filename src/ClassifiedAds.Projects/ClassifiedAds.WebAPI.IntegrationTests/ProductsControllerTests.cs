using ClassifiedAds.DomainServices.Entities;
using ClassifiedAds.CrossCuttingConcerns.ExtensionMethods;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;
using Xunit;
using IdentityModel.Client;

namespace ClassifiedAds.WebAPI.IntegrationTests
{
    public class ProductsControllerTests
    {
        private static HttpClient _httpClient = new HttpClient();

        public ProductsControllerTests()
        {
            _httpClient.BaseAddress = new Uri("https://localhost:44312");
            _httpClient.Timeout = new TimeSpan(0, 0, 30);
            _httpClient.DefaultRequestHeaders.Clear();
        }

        private async Task<List<Product>> GetProducts()
        {
            var response = await _httpClient.GetAsync("api/products", HttpCompletionOption.ResponseHeadersRead);
            response.EnsureSuccessStatusCode();
            var products = await response.Content.ReadAs<List<Product>>();
            return products;
        }

        private static async Task<Product> GetProductById(Guid id)
        {
            var response = await _httpClient.GetAsync($"api/products/{id}", HttpCompletionOption.ResponseHeadersRead);
            response.EnsureSuccessStatusCode();
            var product = await response.Content.ReadAs<Product>();
            return product;
        }

        private static async Task<Product> CreateProduct(Product product)
        {
            var request = new HttpRequestMessage(HttpMethod.Post, "api/products");
            request.Headers.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

            request.Content = product.AsJsonContent();

            var response = await _httpClient.SendAsync(request, HttpCompletionOption.ResponseHeadersRead);
            response.EnsureSuccessStatusCode();
            var createdProduct = await response.Content.ReadAs<Product>();
            return createdProduct;
        }

        private static async Task<Product> UpdateProduct(Guid id, Product product)
        {
            var request = new HttpRequestMessage(HttpMethod.Put, $"api/products/{id}");
            request.Headers.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

            request.Content = product.AsJsonContent();

            var response = await _httpClient.SendAsync(request, HttpCompletionOption.ResponseHeadersRead);
            response.EnsureSuccessStatusCode();
            var updatedProduct = await response.Content.ReadAs<Product>();
            return updatedProduct;
        }

        private static async Task DeleteProduct(Guid id)
        {
            var request = new HttpRequestMessage(HttpMethod.Delete, $"api/products/{id}");
            request.Headers.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

            var response = await _httpClient.SendAsync(request, HttpCompletionOption.ResponseHeadersRead);
            response.EnsureSuccessStatusCode();
        }

        [Fact]
        public async Task AllInOne()
        {
            var metaDataResponse = await _httpClient.GetDiscoveryDocumentAsync("https://localhost:44367/");
            var tokenResponse = await _httpClient.RequestPasswordTokenAsync(new PasswordTokenRequest
            {
                Address = metaDataResponse.TokenEndpoint,
                ClientId = "ClassifiedAds.WebMVC",
                ClientSecret = "secret",
                UserName = "phong@gmail.com",
                Password = "v*7Un8b4rcN@<-RN",
                Scope = "ClassifiedAds.WebAPI"
            });

            var token = tokenResponse.AccessToken;
            _httpClient.UseBearerToken(token);

            // POST
            var product = new Product
            {
                Name = "Test",
                Code = "TEST",
                Description = "Description"
            };
            Product createdProduct = await CreateProduct(product);
            Assert.True(product.Id != createdProduct.Id);
            Assert.Equal(product.Name, createdProduct.Name);
            Assert.Equal(product.Code, createdProduct.Code);
            Assert.Equal(product.Description, createdProduct.Description);

            // GET
            var products = await GetProducts();
            Assert.True(products.Count > 0);

            // GET ONE
            var refreshedProduct = await GetProductById(createdProduct.Id);
            Assert.Equal(refreshedProduct.Id, createdProduct.Id);
            Assert.Equal(refreshedProduct.Name, createdProduct.Name);
            Assert.Equal(refreshedProduct.Code, createdProduct.Code);
            Assert.Equal(refreshedProduct.Description, createdProduct.Description);

            // PUT
            refreshedProduct.Name = "Test 2";
            var updatedProduct = await UpdateProduct(refreshedProduct.Id, refreshedProduct);
            Assert.Equal(refreshedProduct.Id, updatedProduct.Id);
            Assert.Equal("Test 2", updatedProduct.Name);
            Assert.Equal(refreshedProduct.Code, updatedProduct.Code);
            Assert.Equal(refreshedProduct.Description, updatedProduct.Description);

            // DELETE
            await DeleteProduct(createdProduct.Id);
            await Assert.ThrowsAsync<HttpRequestException>(async () => await GetProductById(createdProduct.Id));
        }

    }
}
