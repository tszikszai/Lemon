using Lemon.Web;
using Lemon.Web.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using Xunit;

namespace IntegrationTests.BandsController
{
    public class Put : IClassFixture<CustomWebApplicationFactory<Startup>>
    {
        private const string BaseUrl = "/api/bands";

        private readonly HttpClient _client;

        public Put(CustomWebApplicationFactory<Startup> factory)
        {
            _client = factory.CreateClient();
        }

        [Fact]
        public async Task ReturnsNotFoundForInvalidId()
        {
            var band = new BandViewModel
            {
                Id = 0,
                Name = "A",
                ActiveFromYear = DateTime.Today.Year - 5,
                ActiveToYear = DateTime.Today.Year
            };
            HttpResponseMessage response = await _client.PutAsJsonAsync($"{BaseUrl}/0", band);
            string responseContent = await response.Content.ReadAsStringAsync();

            Assert.Equal(HttpStatusCode.NotFound, response.StatusCode);
            Assert.Equal("0", responseContent);
        }

        [Fact]
        public async Task ReturnsBadRequestGivenIdMismatch()
        {
            var band = new BandViewModel
            {
                Id = 1,
                Name = "A",
                ActiveFromYear = DateTime.Today.Year - 5,
                ActiveToYear = DateTime.Today.Year
            };
            HttpResponseMessage response = await _client.PutAsJsonAsync($"{BaseUrl}/2", band);

            Assert.Equal(HttpStatusCode.BadRequest, response.StatusCode);
        }

        [Fact]
        public async Task ReturnsBadRequestGivenNoName()
        {
            var band = new BandViewModel
            {
                Id = 1,
                ActiveFromYear = DateTime.Today.Year
            };
            HttpResponseMessage response = await _client.PutAsJsonAsync($"{BaseUrl}/1", band);
            string responseContent = await response.Content.ReadAsStringAsync();

            Assert.Equal(HttpStatusCode.BadRequest, response.StatusCode);
            Assert.Contains("Name", responseContent);
            Assert.Contains("The Name field is required.", responseContent);
        }

        [Fact]
        public async Task ReturnsBadRequestGivenNameOver100Chars()
        {
            var band = new BandViewModel
            {
                Id = 1,
                Name = "A".PadLeft(101),
                ActiveFromYear = DateTime.Today.Year
            };
            HttpResponseMessage response = await _client.PutAsJsonAsync($"{BaseUrl}/1", band);
            string responseContent = await response.Content.ReadAsStringAsync();

            Assert.Equal(HttpStatusCode.BadRequest, response.StatusCode);
            Assert.Contains("Name", responseContent);
            Assert.Contains("The field Name must be a string with a maximum length of 100.", responseContent);
        }

        [Fact]
        public async Task ReturnsBadRequestGivenActiveFromYearLessThan1900()
        {
            var band = new BandViewModel
            {
                Id = 1,
                Name = "A",
                ActiveFromYear = 1899
            };
            HttpResponseMessage response = await _client.PutAsJsonAsync($"{BaseUrl}/1", band);
            string responseContent = await response.Content.ReadAsStringAsync();

            Assert.Equal(HttpStatusCode.BadRequest, response.StatusCode);
            Assert.Contains("ActiveFromYear", responseContent);
            Assert.Contains($"The field ActiveFromYear must be between 1900 and {DateTime.Today.Year}.", responseContent);
        }

        [Fact]
        public async Task ReturnsBadRequestGivenActiveFromYearGreaterThanCurrentYear()
        {
            var band = new BandViewModel
            {
                Id = 1,
                Name = "A",
                ActiveFromYear = DateTime.Today.Year + 1
            };
            HttpResponseMessage response = await _client.PutAsJsonAsync($"{BaseUrl}/1", band);
            string responseContent = await response.Content.ReadAsStringAsync();

            Assert.Equal(HttpStatusCode.BadRequest, response.StatusCode);
            Assert.Contains("ActiveFromYear", responseContent);
            Assert.Contains($"The field ActiveFromYear must be between 1900 and {DateTime.Today.Year}.", responseContent);
        }

        [Fact]
        public async Task ReturnsBadRequestGivenActiveToYearLessThan1900()
        {
            var band = new BandViewModel
            {
                Id = 1,
                Name = "A",
                ActiveToYear = 1899
            };
            HttpResponseMessage response = await _client.PutAsJsonAsync($"{BaseUrl}/1", band);
            string responseContent = await response.Content.ReadAsStringAsync();

            Assert.Equal(HttpStatusCode.BadRequest, response.StatusCode);
            Assert.Contains("ActiveToYear", responseContent);
            Assert.Contains($"The field ActiveToYear must be between 1900 and {DateTime.Today.Year}.", responseContent);
        }

        [Fact]
        public async Task ReturnsBadRequestGivenActiveToYearGreaterThanCurrentYear()
        {
            var band = new BandViewModel
            {
                Id = 1,
                Name = "A",
                ActiveToYear = DateTime.Today.Year + 1
            };
            HttpResponseMessage response = await _client.PutAsJsonAsync($"{BaseUrl}/1", band);
            string responseContent = await response.Content.ReadAsStringAsync();

            Assert.Equal(HttpStatusCode.BadRequest, response.StatusCode);
            Assert.Contains("ActiveToYear", responseContent);
            Assert.Contains($"The field ActiveToYear must be between 1900 and {DateTime.Today.Year}.", responseContent);
        }

        [Fact]
        public async Task ReturnsBadRequestGivenActiveFromYearGreaterThanActiveToYear()
        {
            var band = new BandViewModel
            {
                Id = 1,
                Name = "A",
                ActiveFromYear = DateTime.Today.Year,
                ActiveToYear = DateTime.Today.Year - 1
            };
            HttpResponseMessage response = await _client.PutAsJsonAsync($"{BaseUrl}/1", band);
            string responseContent = await response.Content.ReadAsStringAsync();

            Assert.Equal(HttpStatusCode.BadRequest, response.StatusCode);
            Assert.Contains("Active from Year must be lower than or equal to Active to Year.", responseContent);
        }

        [Fact]
        public async Task ReturnsNoContentGivenValidData()
        {
            var band = new BandViewModel
            {
                Id = 1,
                Name = "A",
                ActiveFromYear = DateTime.Today.Year - 5,
                ActiveToYear = DateTime.Today.Year
            };
            HttpResponseMessage response = await _client.PutAsJsonAsync($"{BaseUrl}/1", band);

            Assert.Equal(HttpStatusCode.NoContent, response.StatusCode);
        }
    }
}
