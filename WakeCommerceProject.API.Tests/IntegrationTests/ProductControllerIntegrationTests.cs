using FluentAssertions;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Newtonsoft.Json;
using System.Net;
using System.Net.Http.Headers;
using System.Net.Http.Json;
using System.Text;
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

            using (var scope = _factory.Services.CreateScope())
            {
                var scopedServices = scope.ServiceProvider;
                var db = scopedServices.GetRequiredService<ApplicationDbContext>();

                db.Database.EnsureCreated();

                try
                {
                    db.Database.Migrate();
                }
                catch (Microsoft.Data.Sqlite.SqliteException ex) when (ex.Message.Contains("table \"Products\" already exists"))
                {
                    Console.WriteLine("The 'Products' table already exists.");
                }

                if (!db.Products.Any())
                {
                    Seeding.InitializeTestDb(db);
                }


            }
        }

        [Fact(DisplayName = "Post Valid Params Return OK")]
        public async Task Post_SendingValidProduct_ReturnOK()
        {
            var newProduct = new Product { Id = 12, Name = "Name", Description = "Description", Price = 49.99m, Stock = 5, SKU = "AL1234" };

            var serializedProduct = JsonConvert.SerializeObject(newProduct);

            HttpContent content = new StringContent(serializedProduct, Encoding.UTF8, "application/json");

            content.Headers.ContentType = new MediaTypeHeaderValue("application/json");

            var response = await _httpClient.PostAsync("/api/Product", content);

            response.StatusCode.Should().Be(HttpStatusCode.Created);
        }

        [Fact(DisplayName = "Post Invalid Params Return BadRequest")]
        public async Task Post_SendingInvalidProduct_ReturnBadRequest()
        {

            var newProduct = new Product { Id = 12, Name = "Name", Description = "Description", Price = -49.99m, Stock = 5, SKU = "AH1234" };

            var serializedProduct = JsonConvert.SerializeObject(newProduct);

            HttpContent content = new StringContent(serializedProduct, Encoding.UTF8, "application/json");

            content.Headers.ContentType = new MediaTypeHeaderValue("application/json");

            var response = await _httpClient.PostAsync("/api/Product", content);

            response.StatusCode.Should().Be(HttpStatusCode.BadRequest);
        }

        [Fact(DisplayName = "Get Return OK and Products")]
        public async Task GetProduct_ProductExists_ReturnOKWithProduct()
        {

            var response = await _httpClient.GetAsync("api/Product");
            var result = await response.Content.ReadFromJsonAsync<List<Product>>();

            response.StatusCode.Should().Be(HttpStatusCode.OK);
            result.Should().HaveCount(6);
        }

    }
}
