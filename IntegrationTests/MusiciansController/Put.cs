using Lemon.Web;
using Lemon.Web.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using Xunit;

namespace IntegrationTests.MusiciansController
{
    public class Put : IClassFixture<CustomWebApplicationFactory<Startup>>
    {
        private const string BaseUrl = "/api/musicians";

        private readonly HttpClient _client;

        public Put(CustomWebApplicationFactory<Startup> factory)
        {
            _client = factory.CreateClient();
        }

        [Fact]
        public async Task ReturnsNotFoundForInvalidId()
        {
            var musician = new MusicianViewModel
            {
                Id = 0,
                FirstName = "A",
                LastName = "B",
                DateOfBirth = DateTime.Today.AddYears(-90),
                DateOfDeath = DateTime.Today
            };
            HttpResponseMessage response = await _client.PutAsJsonAsync($"{BaseUrl}/0", musician);
            string responseContent = await response.Content.ReadAsStringAsync();

            Assert.Equal(HttpStatusCode.NotFound, response.StatusCode);
            Assert.Equal("0", responseContent);
        }

        [Fact]
        public async Task ReturnsBadRequestGivenIdMismatch()
        {
            var musician = new MusicianViewModel
            {
                Id = 4,
                FirstName = "A",
                LastName = "B",
                DateOfBirth = DateTime.Today.AddYears(-90),
                DateOfDeath = DateTime.Today
            };
            HttpResponseMessage response = await _client.PutAsJsonAsync($"{BaseUrl}/5", musician);

            Assert.Equal(HttpStatusCode.BadRequest, response.StatusCode);
        }

        [Fact]
        public async Task ReturnsBadRequestGivenNoFirstName()
        {
            var musician = new MusicianViewModel
            {
                Id = 4,
                LastName = "B",
                DateOfBirth = DateTime.Today
            };
            HttpResponseMessage response = await _client.PutAsJsonAsync($"{BaseUrl}/4", musician);
            string responseContent = await response.Content.ReadAsStringAsync();

            Assert.Equal(HttpStatusCode.BadRequest, response.StatusCode);
            Assert.Contains("FirstName", responseContent);
            Assert.Contains("The FirstName field is required.", responseContent);
        }

        [Fact]
        public async Task ReturnsBadRequestGivenFirstNameOver50Chars()
        {
            var musician = new MusicianViewModel
            {
                Id = 4,
                FirstName = "A".PadLeft(51),
                LastName = "B",
                DateOfBirth = DateTime.Today
            };
            HttpResponseMessage response = await _client.PutAsJsonAsync($"{BaseUrl}/4", musician);
            string responseContent = await response.Content.ReadAsStringAsync();

            Assert.Equal(HttpStatusCode.BadRequest, response.StatusCode);
            Assert.Contains("FirstName", responseContent);
            Assert.Contains("The field FirstName must be a string with a maximum length of 50.", responseContent);
        }

        [Fact]
        public async Task ReturnsBadRequestGivenNoLastName()
        {
            var musician = new MusicianViewModel
            {
                Id = 4,
                FirstName = "A",
                DateOfBirth = DateTime.Today
            };
            HttpResponseMessage response = await _client.PutAsJsonAsync($"{BaseUrl}/4", musician);
            string responseContent = await response.Content.ReadAsStringAsync();

            Assert.Equal(HttpStatusCode.BadRequest, response.StatusCode);
            Assert.Contains("LastName", responseContent);
            Assert.Contains("The LastName field is required.", responseContent);
        }

        [Fact]
        public async Task ReturnsBadRequestGivenLastNameOver50Chars()
        {
            var musician = new MusicianViewModel
            {
                Id = 4,
                FirstName = "A",
                LastName = "B".PadLeft(51),
                DateOfBirth = DateTime.Today
            };
            HttpResponseMessage response = await _client.PutAsJsonAsync($"{BaseUrl}/4", musician);
            string responseContent = await response.Content.ReadAsStringAsync();

            Assert.Equal(HttpStatusCode.BadRequest, response.StatusCode);
            Assert.Contains("LastName", responseContent);
            Assert.Contains("The field LastName must be a string with a maximum length of 50.", responseContent);
        }

        [Fact]
        public async Task ReturnsBadRequestGivenDateOfBirthLessThan1January1900()
        {
            var musician = new MusicianViewModel
            {
                Id = 4,
                FirstName = "A",
                LastName = "B",
                DateOfBirth = new DateTime(1899, 12, 31)
            };
            HttpResponseMessage response = await _client.PutAsJsonAsync($"{BaseUrl}/4", musician);
            string responseContent = await response.Content.ReadAsStringAsync();

            Assert.Equal(HttpStatusCode.BadRequest, response.StatusCode);
            Assert.Contains("DateOfBirth", responseContent);
            Assert.Contains($"The field DateOfBirth must be between {new DateTime(1900, 1, 1)} and {DateTime.Today}.", responseContent);
        }

        [Fact]
        public async Task ReturnsBadRequestGivenDateOfBirthGreaterThanToday()
        {
            var musician = new MusicianViewModel
            {
                Id = 4,
                FirstName = "A",
                LastName = "B",
                DateOfBirth = DateTime.Today.AddDays(1)
            };
            HttpResponseMessage response = await _client.PutAsJsonAsync($"{BaseUrl}/4", musician);
            string responseContent = await response.Content.ReadAsStringAsync();

            Assert.Equal(HttpStatusCode.BadRequest, response.StatusCode);
            Assert.Contains("DateOfBirth", responseContent);
            Assert.Contains($"The field DateOfBirth must be between {new DateTime(1900, 1, 1)} and {DateTime.Today}.", responseContent);
        }

        [Fact]
        public async Task ReturnsBadRequestGivenDateOfDeathLessThan1January1900()
        {
            var musician = new MusicianViewModel
            {
                Id = 4,
                FirstName = "A",
                LastName = "B",
                DateOfBirth = DateTime.Today,
                DateOfDeath = new DateTime(1899, 12, 31)
            };
            HttpResponseMessage response = await _client.PutAsJsonAsync($"{BaseUrl}/4", musician);
            string responseContent = await response.Content.ReadAsStringAsync();

            Assert.Equal(HttpStatusCode.BadRequest, response.StatusCode);
            Assert.Contains("DateOfDeath", responseContent);
            Assert.Contains($"The field DateOfDeath must be between {new DateTime(1900, 1, 1)} and {DateTime.Today}.", responseContent);
        }

        [Fact]
        public async Task ReturnsBadRequestGivenDateOfDeathGreaterThanToday()
        {
            var musician = new MusicianViewModel
            {
                Id = 4,
                FirstName = "A",
                LastName = "B",
                DateOfBirth = DateTime.Today,
                DateOfDeath = DateTime.Today.AddDays(1)
            };
            HttpResponseMessage response = await _client.PutAsJsonAsync($"{BaseUrl}/4", musician);
            string responseContent = await response.Content.ReadAsStringAsync();

            Assert.Equal(HttpStatusCode.BadRequest, response.StatusCode);
            Assert.Contains("DateOfDeath", responseContent);
            Assert.Contains($"The field DateOfDeath must be between {new DateTime(1900, 1, 1)} and {DateTime.Today}.", responseContent);
        }

        [Fact]
        public async Task ReturnsBadRequestGivenDateOfBirthGreaterThanDateOfDeath()
        {
            var musician = new MusicianViewModel
            {
                Id = 4,
                FirstName = "A",
                LastName = "B",
                DateOfBirth = DateTime.Today,
                DateOfDeath = DateTime.Today.AddDays(-1)
            };
            HttpResponseMessage response = await _client.PutAsJsonAsync($"{BaseUrl}/4", musician);
            string responseContent = await response.Content.ReadAsStringAsync();

            Assert.Equal(HttpStatusCode.BadRequest, response.StatusCode);
            Assert.Contains("Date of Birth must be prior to Date of Death.", responseContent);
        }

        [Fact]
        public async Task ReturnsNoContentGivenValidData()
        {
            var musician = new MusicianViewModel
            {
                Id = 4,
                FirstName = "A",
                LastName = "B",
                DateOfBirth = DateTime.Today.AddYears(-90),
                DateOfDeath = DateTime.Today
            };
            HttpResponseMessage response = await _client.PutAsJsonAsync($"{BaseUrl}/4", musician);

            Assert.Equal(HttpStatusCode.NoContent, response.StatusCode);
        }
    }
}
