using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.VisualStudio.TestPlatform.TestHost;

namespace TestProject1
{
    [TestClass]
    public class UnitTest1
    {
        [TestMethod]
        public async Task TestMethod1()
        {
            var webAppFactory = new WebApplicationFactory<Program>();
            var httpClient = webAppFactory.CreateDefaultClient();

            var response = await httpClient.GetAsync("");
            var stringResult = await response.Content.ReadAsStringAsync();

            Assert.AreEqual("Hello Worlds!", stringResult);
        }
        [TestMethod]
        public async Task TestIndexActionGetAllPlaces()
        {
            // Arrange
            var factory = new WebApplicationFactory<Program>();
            var client = factory.CreateClient();

            // Act
            var response = await client.GetAsync("/Place");

            // Assert
            response.EnsureSuccessStatusCode(); // Check for successful status code (200-299)
            var content = await response.Content.ReadAsStringAsync();
            Xunit.Assert.Contains("Кремль", content); // Check for expected content in the view
        }
    }
}