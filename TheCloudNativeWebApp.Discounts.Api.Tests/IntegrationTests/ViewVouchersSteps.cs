using FluentAssertions.Execution;
using Newtonsoft.Json.Linq;
using TechTalk.SpecFlow;
using TheCloudNativeWebApp.Discounts.Api.Tests.IntegrationTests.Builders;
using TheCloudNativeWebApp.Discounts.Infrastructure.Sql.DataModel;

namespace TheCloudNativeWebApp.Discounts.Api.Tests.IntegrationTests;

[Binding]
[Scope(Feature = "Viewing Vouchers")]
public class ViewVouchersSteps
{
    private Faker _faker = new ();

    private ApplicationBuilder _applicationBuilder = new();

    private HttpResponseMessage? _response;
    
    [BeforeScenario]
    public void Initialize()
    {
        _applicationBuilder = new ApplicationBuilder();
    }

    [Given("the system does contain vouchers")]
    public void WithoutVouchers()
    {
    }

    [Given("the system contains vouchers")]
    public void WithVouchers()
    {
        _applicationBuilder.WithPrecondition(dbContext =>
        {
            dbContext.Vouchers.Add(new Voucher { VoucherCode = "00000001", Value = 3.14m });
            dbContext.Vouchers.Add(new Voucher { VoucherCode = "00000002", Value = 6.28m });
            dbContext.SaveChanges();
        });
    }

    [When("I GET the /api/voucher endpoint")]
    public async Task InvokeGetEndpoint()
    {
        var httpClient = _applicationBuilder.CreateClient(); // Spin up the api
        _response = await httpClient.GetAsync("/api/voucher");
    }
    
    [Then("the API responds with an status code (([0-9]{3})\\s(.*))")]
    public async Task AssertStatusCode(string regexMatch, int expectedStatusCode, string errorMessage)
    {
        if (_response == null)
        {
            throw new AssertionFailedException("It seems the request to the endpoint has not been executed.");
        }

        var responseBody = await _response.Content.ReadAsStringAsync();
        Console.WriteLine(responseBody);
    
        var actualStatusCode = (int)_response.StatusCode;
        
        actualStatusCode.Should().Be(expectedStatusCode);
    }
    
    [Then("I get a list of vouchers")]
    public async Task AssertItems()
    {
        if (_response == null)
        {
            throw new ArgumentNullException(nameof(_response));
        }

        var stringContents = await _response.Content.ReadAsStringAsync();
        var jsonObject = JObject.Parse(stringContents);

        jsonObject["items"].Should().NotBeEmpty();
    }
    
    [Then(@"I get an empty list")]
    public async Task AssertEmptyList()
    {
        if (_response == null)
        {
            throw new ArgumentNullException(nameof(_response));
        }
        
        var stringContents = await _response.Content.ReadAsStringAsync();
        var jsonObject = JObject.Parse(stringContents);

        jsonObject["items"].Should().BeEmpty();
    }
}