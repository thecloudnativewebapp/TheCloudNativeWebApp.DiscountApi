using FluentAssertions.Execution;
using Newtonsoft.Json.Linq;
using System.Text;
using TechTalk.SpecFlow;
using TheCloudNativeWebApp.Discounts.Api.Tests.IntegrationTests.Builders;
using TheCloudNativeWebApp.Discounts.Infrastructure.Sql.DataModel;

namespace TheCloudNativeWebApp.Discounts.Api.Tests.IntegrationTests;

[Binding]
[Scope(Feature = "Creating Vouchers")]
public class CreateVouchersSteps
{
    private const string VoucherCodePropertyName = "vouchercode";
    private const string ValuePropertyName = "value";
    
    private Faker _faker = new ();

    private JObject _request = new();

    private ApplicationBuilder _applicationBuilder = new();

    private HttpResponseMessage? _response;

    [BeforeScenario]
    public void Initialize()
    {
        _applicationBuilder = new ApplicationBuilder();
    }

    [Given("a unique vouchercode")]
    public void WithUniqueVoucherCode()
    {
        _request[VoucherCodePropertyName] = _faker.Random.Replace("########");
    }

    [Given(@"a voucher has been created in the past with vouchercode (.*)")]
    public void WithExistingVoucherCode(string voucherCode)
    {
        _applicationBuilder.WithPrecondition(dbContext =>
        {
            dbContext.Vouchers.Add(new Voucher
            {
                VoucherCode = voucherCode
            });

            dbContext.SaveChanges();
        });
    }

    [Given(@"a (.*) and a (.*)")]
    public void WithIncorrectVoucherCode(string voucherCode, string value)
    {
        _request[VoucherCodePropertyName] = voucherCode;

        if (decimal.TryParse(value, out decimal decimalValue))
        {
            _request[ValuePropertyName] = decimalValue;
        }
        else
        {
            _request[ValuePropertyName] = value;
        }
    }
    
    [When("the request is posted to the /api/voucher endpoint")]
    public async Task InvokeEndpoint()
    {
        var requestBody = _request.ToString();
        
        var httpClient = _applicationBuilder.CreateClient(); // Spin up the api
        _response = await httpClient.PostAsync("/api/voucher", new StringContent(requestBody, Encoding.UTF8, "application/json"));
    }
    
    [Then("the voucher has been saved")]
    public void AssertSaved()
    {
        using var dbContext = _applicationBuilder.CreateDiscountDbContext();

        dbContext.Vouchers
            .Any(x => x.VoucherCode == _request[VoucherCodePropertyName].Value<string>()).Should().BeTrue();
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
    
    [Then("the voucher contains the vouchercode and the value")]
    public void AssertVoucherContainsVouchercode()
    {
        using var dbContext = _applicationBuilder.CreateDiscountDbContext();

        dbContext.Vouchers
            .Any(x => x.VoucherCode == _request[VoucherCodePropertyName].Value<string>()
                    && x.Value == _request[ValuePropertyName].Value<decimal>()).Should().BeTrue();
    }

    [Then("the application produces an error which implies the vouchercode is incorrect")]
    public async Task AssertVoucherCodeErrorThrown() => await AssertIncorrectProperty("$.vouchercode");
    
    [Then("the application produces an error which implies the value is incorrect")]
    public async Task AssertValueErrorThrown() => await AssertIncorrectProperty("$.value");

    private async Task AssertIncorrectProperty(string propertyName)
    {
        if (_response == null)
        {
            throw new AssertionFailedException("It seems the request to the endpoint has not been executed.");
        }
        
        var response = await _response.Content.ReadAsStringAsync();
        response.Should().Contain(propertyName);
    }
}