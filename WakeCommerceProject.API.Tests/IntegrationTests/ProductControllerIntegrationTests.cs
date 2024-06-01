using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http.Headers;
using System.Net.Http.Json;
using System.Text;
using System.Threading.Tasks;
using FluentAssertions;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Newtonsoft.Json;
using WakeCommerceProject.Domain;
using WakeCommerceProject.Infra.Data.Context;


namespace WakeCommerceProject.API.Tests.IntegrationTests
{
    public class ProductControllerIntegrationTests : IClassFixture<CustomWebApplicationFactory<Program>>
    {
        private readonly CustomWebApplicationFactory<Program> _factory;
        private HttpClient _httpClient;

        public ProductControllerIntegrationTests(CustomWebApplicationFactory<Program> factory)
        {
            _factory = factory;
            _httpClient = _factory.CreateClient(new WebApplicationFactoryClientOptions
            {
                AllowAutoRedirect = false
            });
        }
        
        [Fact(DisplayName = "Post Valid Params Return OK")]
        public async Task Post_SendingValidProduct_ReturnOK()
        {
            // Setting up sql database
            using (var scope = _factory.Services.CreateScope())
            {
                var scopedServices = scope.ServiceProvider;
                var db = scopedServices.GetRequiredService<ApplicationDbContext>();

                db.Database.EnsureCreated();
                // TODO: Resolver isso
                try { db.Database.Migrate(); } catch { }
                
            }

            var newProduct = new Product(12, "Name", "Description", 49.99m, 5);
            var serializedProduct = JsonConvert.SerializeObject(newProduct);

            HttpContent content = new StringContent(serializedProduct, Encoding.UTF8, "application/json");

            content.Headers.ContentType = new MediaTypeHeaderValue("application/json");

            var response = await _httpClient.PostAsync("/api/Product", content);

            response.StatusCode.Should().Be(HttpStatusCode.Created);
        }

        [Fact(DisplayName = "Get Return OK and Products")]
        public async Task GetProduct_ProductExists_ReturnOKWithProduct()
        {
            // Setting up sql database
            using (var scope = _factory.Services.CreateScope())
            {
                var scopedServices = scope.ServiceProvider;
                var db = scopedServices.GetRequiredService<ApplicationDbContext>();

                db.Database.EnsureCreated();
                // TODO: Resolver isso
                try
                {
                    db.Database.Migrate();
                    Seeding.InitializeTestDb(db);
                }
                catch { }


            }

            var response = await _httpClient.GetAsync("api/Product");
            var result = await response.Content.ReadFromJsonAsync<List<Product>>();

            response.StatusCode.Should().Be(HttpStatusCode.OK);
            result.Should().HaveCount(6);
        }

    }
}
