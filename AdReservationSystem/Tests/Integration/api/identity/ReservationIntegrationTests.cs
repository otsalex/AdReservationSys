using System.IdentityModel.Tokens.Jwt;
using System.Net.Http.Headers;
using System.Net.Http.Json;
using System.Security.Claims;
using System.Text.Json;
using BLL.DTO;
using Microsoft.AspNetCore.Mvc.Testing;
using Public.DTO.v1.Identity;

namespace Tests.Integration.api.identity;

public class ReservationIntegrationTests : IClassFixture<CustomWebAppFactory<Program>>
{
    private readonly HttpClient _client;


    private readonly JsonSerializerOptions camelCaseJsonSerializerOptions = new JsonSerializerOptions()
    {
        PropertyNamingPolicy = JsonNamingPolicy.CamelCase
    };

    public ReservationIntegrationTests(CustomWebAppFactory<Program> factory)
    {
        _client = factory.CreateClient(new WebApplicationFactoryClientOptions
        {
            AllowAutoRedirect = false
        });
    }

    [Fact(DisplayName = "POST - create new reservation")]
    public async Task CreateNewReservationTest()
    {
        const string email = "login@test.ee";
        const string firstname = "TestFirst";
        const string lastname = "TestLast";
        const string password = "Foo.bar1";
        const int expiresInSeconds = 1;

        // Arrange
        await RegisterNewUser(email, password, firstname, lastname, expiresInSeconds);

        var loginURL = "/api/v1/identity/account/login?expiresInSeconds=1";

        var loginData = new
        {
            Email = email,
            Password = password,
        };

        var data = JsonContent.Create(loginData);
        var loginResponse = await _client.PostAsync(loginURL, data);

        var responseContent = await loginResponse.Content.ReadAsStringAsync();
        
        var jwt = JsonSerializer.Deserialize<JWTResponse>(responseContent, camelCaseJsonSerializerOptions);
        _client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", jwt!.JWT);
        
        // Act
        var reservationURL = "/api/v1/reservation";
        var reservationData = new
        {
            campaignName = "test",
            state = "pending",
            city = "Pärnu",
            startDate = "2023-04-30",
            endDate = "2023-04-30",
            adSpaces = new List<AdSpace>()
        };
        
        var reservationDataJson = JsonContent.Create(reservationData);
        var response = await _client.PostAsync(reservationURL, reservationDataJson);

        // Assert
        Assert.True(response.IsSuccessStatusCode);
        
    }

    [Fact(DisplayName = "POST - update reservation")]
    public async Task UpdateReservationTest()
    {
        // TODO: Sometimes fails because user jwt expires
        
        // Arrange
    
        // create a reservation and get its ID
        var reservationId = await LoginAndCreateReservation();
        
        // get the reservation from DB
        var reservationURL = "/api/v1/reservation/" + reservationId!.Replace("\"", "");
        var request = new HttpRequestMessage(HttpMethod.Get, reservationURL);
        var response = await _client.SendAsync(request);
        var content = await response.Content.ReadAsStringAsync();
        var data = System.Text.Json.JsonDocument.Parse(content);

        //Act
        
        var newName = "someOtherNameThanBefore";
        var reservationData = new
        {
            id = data.RootElement.GetProperty("id").GetGuid(),
            campaignName = newName,
            state = data.RootElement.GetProperty("state").GetString(),
            city = data.RootElement.GetProperty("city").GetString(),
            startDate = data.RootElement.GetProperty("startDate").GetString(),
            endDate = data.RootElement.GetProperty("endDate").GetString(),
            adSpaces = new List<AdSpace>()
        };
        // make put request
        var putRequest = new HttpRequestMessage(HttpMethod.Put, reservationURL);
        putRequest.Content = JsonContent.Create(reservationData);
        var putResponse = await _client.SendAsync(putRequest);
        var putResponseId = await putResponse.Content.ReadAsStringAsync();

        // ask the Reservation by the ID from PUT request
        var newReservationURL = "/api/v1/reservation/" + putResponseId.Replace("\"", "");
        var checkRequest = new HttpRequestMessage(HttpMethod.Get, newReservationURL);
        var checkResponse = await _client.SendAsync(checkRequest);
        var checkResponseContent = await checkResponse.Content.ReadAsStringAsync();
        var modifiedReservation = System.Text.Json.JsonDocument.Parse(checkResponseContent);
        
        //Assert
        // Assert that ID before and after PUT is the same
        Assert.Equal(reservationId.Replace("\"", ""), modifiedReservation.RootElement.GetProperty("id").ToString());
        // Assert that the name was changed
        Assert.Equal(newName.Replace("\"", ""), modifiedReservation.RootElement.GetProperty("campaignName").ToString());
        
        
    }

    [Fact(DisplayName = "POST - delete reservation")]
    public async Task DeleteReservationTest()
    {
        // Arrange
        
        // create a reservation and get its ID
        var reservationId = await LoginAndCreateReservation();
        
        //Act
        // make delete request
        var reservationURL = "/api/v1/reservation/" + reservationId!.Replace("\"", "");
        var deleteRequest = new HttpRequestMessage(HttpMethod.Delete, reservationURL);
        var deleteResponse = await _client.SendAsync(deleteRequest);

        Assert.Equal(System.Net.HttpStatusCode.NoContent, deleteResponse.StatusCode);
    }
    private async Task<string> RegisterNewUser(string email, string password, string firstname, string lastname,
        int expiresInSeconds = 1)
    {
        var URL = $"/api/v1/identity/account/register?expiresInSeconds={expiresInSeconds}";

        var registerData = new
        {
            Email = email,
            Password = password,
            Firstname = firstname,
            Lastname = lastname,
        };

        var data = JsonContent.Create(registerData);
        // Act
        var response = await _client.PostAsync(URL, data);

        var responseContent = await response.Content.ReadAsStringAsync();
        // Assert
        Assert.True(response.IsSuccessStatusCode);

        VerifyJwtContent(responseContent, email, firstname, lastname,
            DateTime.Now.AddSeconds(expiresInSeconds + 1).ToUniversalTime());

        return responseContent;
    }
    private void VerifyJwtContent(string jwt, string email, string firstname, string lastname,
        DateTime validToIsSmallerThan)
    {
        var jwtResponse = JsonSerializer.Deserialize<JWTResponse>(jwt, camelCaseJsonSerializerOptions);

        Assert.NotNull(jwtResponse);
        Assert.NotNull(jwtResponse.RefreshToken);
        Assert.NotNull(jwtResponse.JWT);

        // verify the actual JWT
        var jwtToken = new JwtSecurityTokenHandler().ReadJwtToken(jwtResponse.JWT);
        Assert.Equal(email, jwtToken.Claims.FirstOrDefault(c => c.Type == ClaimTypes.Email)?.Value);
        Assert.Equal(firstname, jwtToken.Claims.FirstOrDefault(c => c.Type == ClaimTypes.GivenName)?.Value);
        Assert.Equal(lastname, jwtToken.Claims.FirstOrDefault(c => c.Type == ClaimTypes.Surname)?.Value);
        Assert.True(jwtToken.ValidTo < validToIsSmallerThan);
    }

    private async Task<string?> LoginAndCreateReservation()
    {
        // register
        Random rnd = new Random();
        int num = rnd.Next();
        
        // random to avoid registering same account
        var email = num + "@test.ee";
        const string firstname = "TestFirst";
        const string lastname = "TestLast";
        const string password = "Foo.bar1";
        const int expiresInSeconds = 1;
        
        await RegisterNewUser(email, password, firstname, lastname, expiresInSeconds);

        // login
        var loginURL = "/api/v1/identity/account/login?expiresInSeconds=1";

        var loginData = new
        {
            Email = email,
            Password = password,
        };

        var data = JsonContent.Create(loginData);
        var loginResponse = await _client.PostAsync(loginURL, data);

        var responseContent = await loginResponse.Content.ReadAsStringAsync();
        
        var jwt = JsonSerializer.Deserialize<JWTResponse>(responseContent, camelCaseJsonSerializerOptions);
        _client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", jwt!.JWT);
        
        // Create reservation
        var reservationURL = "/api/v1/reservation";
        var reservationData = new
        {
            campaignName = "test",
            state = "pending",
            city = "Pärnu",
            startDate = "2023-04-30",
            endDate = "2023-04-30",
            adSpaces = new List<AdSpace>()
        };
        
        var reservationDataJson = JsonContent.Create(reservationData);
        var response = await _client.PostAsync(reservationURL, reservationDataJson);
        var content = await response.Content.ReadAsStringAsync();
        return content;
    }
}