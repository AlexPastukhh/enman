using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using FluentAssertions;
using Hospital.proj.Domain.Common;
using Hospital.proj.Domain.Users;
using Hospital.proj.Server.Contracts;
using Hospital.proj.Server.Utils;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Options;
using Microsoft.Playwright;
using Serilog;
using Xunit.Abstractions;
using Xunit.Extensions.Ordering;
using static Hospital.proj.Domain.Common.Errors;

namespace Hospital.proj.Tests.E2E
{
	[Collection("E2E")]
	public class AccountE2ETests
	{
		private readonly SharedContext _context;
		private readonly ITestOutputHelper _outPut;
		public AccountE2ETests(
			SharedContext context,
			ITestOutputHelper output)
		{
			_context = context;
			Log.Logger = new LoggerConfiguration()
				.WriteTo.TestOutput(output)
				.MinimumLevel.Debug()
				.CreateLogger();

			_outPut= output;


		}

		public  User GetTestUser(string firstPassword)
		{
			var currentTime = DateTimeOffset.UtcNow;
			var fullName = FullName.Create("First", "Middle", "Last").Value;
			var email = Email.Create(SharedContext.TestClientEmail).Value;

			var passwordHash = Password.HashPassword(firstPassword).Value;

			var user = User.CreateAccount(fullName, email, passwordHash, currentTime);

			return user;
		}

		

		private async Task OpenLastMessage(IPage page)
		{
			await page.GotoAsync(Routes.Ethereal);

			await page.Locator("input[type=email]")
				.FillAsync(SharedContext.TestClientEmail);

			await page.Locator("input[type=password]")
				.FillAsync(SharedContext.TestPassword);

			await page.Locator("button[type=submit]",
				new() { HasText = "Log in" })
					.ClickAsync();

			await page.Locator("a.nav-link",
				new() { HasText = "Messages" })
				.First
				.ClickAsync();
		}

	   
		[Fact,Order(1)]
		public async Task Cant_Submit_Register_Form_With_Invalid_Inputs()
		{
			//Arrange
			var page = await _context.Browser.NewPageAsync(new BrowserNewPageOptions()
			{
				BaseURL = SharedContext.AppUrl
			});

			await page.GotoAsync(Routes.RegisterPath);

			var user = GetTestUser(SharedContext.FirstPassword);

			var errors = new List<string>();

			_context.MonitorPageErrors(page, errors);

			_context.SetLogsForDebug(page);

			//Act
			await page.FillAsync("#emailInput","email");
			await page.FillAsync("#firstNameInput","");
			await page.FillAsync("#middleNameInput", "user.FullName.MiddleNameuser.FullName.MiddleNameuser.FullName.MiddleNameuser.FullName.MiddleNameuser.FullName.MiddleNameuser.FullName.MiddleNameuser.FullName.MiddleNameuser.FullName.MiddleNameuser.FullName.MiddleNameuser.FullName.MiddleNameuser.FullName.MiddleNameuser.FullName.MiddleNameuser.FullName.MiddleNameuser.FullName.MiddleNameuser.FullName.MiddleNameuser.FullName.MiddleNameuser.FullName.MiddleNameuser.FullName.MiddleNameuser.FullName.MiddleNameuser.FullName.MiddleNameuser.FullName.MiddleName"
				);
			await page.FillAsync("#lastNameInput", "");
			await page.FillAsync("#passwordInput", "dfss");
			await page.FillAsync("#passwordConfirmInput", SharedContext.FirstPassword);

			await page.ClickAsync("#registerBtn");

			await Task.Delay(500);

			//Assert
			errors.Should().BeEmpty();

			page.Url.Should().Be(Routes.RegisterPage);
			
			var errorMessages = await page
				.Locator("span.text-danger")
				.AllAsync();
			
			errorMessages.Count.Should().Be(6);
		}
		[Fact,Order(2)]
		public async Task Registers_Successfully()
		{
			//Arrange
			var page = await _context.Browser.NewPageAsync(new BrowserNewPageOptions()
			{
				BaseURL = SharedContext.AppUrl
			});

			await page.GotoAsync(Routes.RegisterPath);

			var user = GetTestUser(SharedContext.FirstPassword);

			var errors = new List<string>();

			_context.MonitorPageErrors(page, errors);

			_context.SetLogsForDebug(page);

			//Act
			await page.FillAsync("#emailInput",user.Email);
			await page.FillAsync("#firstNameInput",user.FullName.FirstName);
			await page.FillAsync("#middleNameInput", user.FullName.MiddleName);
			await page.FillAsync("#lastNameInput", user.FullName.LastName);
			await page.FillAsync("#passwordInput", SharedContext.FirstPassword);
			await page.FillAsync("#passwordConfirmInput", SharedContext.FirstPassword);

            await page.ClickAsync("#registerBtn");
            

            //Assert

            var errorMessages = await page
				.Locator("span.text-danger")
				.AllAsync();
			
			errorMessages.Should().BeEmpty();
			
			await Task.Delay(500);

			errors.Should().BeEmpty();

			await page.WaitForURLAsync(Routes.ActivateAccountPage);

			page.Url.Should().Be(Routes.ActivateAccountPage);
			
		}

		[Fact,Order(3)]
		public async Task Cant_Register_Twice_With_Same_Email()
		{
			//Arrange
			var page = await _context.Browser.NewPageAsync(new BrowserNewPageOptions()
			{
				BaseURL = SharedContext.AppUrl
			});

			await page.GotoAsync(Routes.RegisterPath);

			var user = GetTestUser(SharedContext.FirstPassword);

			var errors = new List<string>();

			_context.MonitorPageErrors(page, errors);

			_context.SetLogsForDebug(page);

			//Act
			await page.FillAsync("#emailInput",user.Email);
			await page.FillAsync("#firstNameInput",user.FullName.FirstName);
			await page.FillAsync("#middleNameInput", user.FullName.MiddleName);
			await page.FillAsync("#lastNameInput", user.FullName.LastName);
			await page.FillAsync("#passwordInput", SharedContext.FirstPassword);
			await page.FillAsync("#passwordConfirmInput", SharedContext.FirstPassword);

			var waitForResponseTask =page
				.WaitForResponseAsync("**/api/Account/register");

			await page.ClickAsync("#registerBtn");

			await waitForResponseTask;

			//Assert

			
			await Task.Delay(4000);

			//errors.Should().NotBeEmpty();

			page.Url.Should().Be(Routes.RegisterPage);
		}

		[Fact, Order(4)]
		public async Task Activates_Account()
		{
			//Arrange
			var page = await _context.Browser.NewPageAsync(new BrowserNewPageOptions()
			{
				BaseURL = SharedContext.AppUrl
			});

			_context.SetLogsForDebug(page);


			//Act        
			await OpenLastMessage(page);



			var subjectLink = page.Locator("a",
				new() { HasText = EmailHelpers.ActivateAccountSubject })
					.First;

			var subjectText = await subjectLink.InnerTextAsync();

			await subjectLink.ClickAsync();

			var frame = page
				.Locator("div#message>iframe")
					.ContentFrame;
					
			var messageText = await frame.Locator("span.message").First.InnerTextAsync();

			var link = frame
					.Locator("a.activation")
						.First;

			var linkText = await link.InnerTextAsync();


			await link.ClickAsync();

			//Assert

			await page.WaitForURLAsync(Routes.AccountActivatedPage);

			page.Url.Should().Be(Routes.AccountActivatedPage);

			messageText.Should()
				.Be($"{EmailHelpers.ActivateAccountMessageText} " +
					$"{EmailHelpers.ActivateAccountLinkText}");

			subjectText.Should().Be(
				EmailHelpers.ActivateAccountSubject);

			linkText.Should().Be(
				EmailHelpers.ActivateAccountLinkText);

		}

		//[Fact,Order(3)]
		//public async Task Logins_In_Account()
		//{
		//    //Arrange
		//    var page = await _context.Browser.NewPageAsync(
		//        new BrowserNewPageOptions()
		//        {
		//            BaseURL=Routes.BaseSpaUrl
		//        }
		//    );

		//    var errors = new List<string>();

		//    _context.MonitorPageErrors(page, errors);

		//    //Act
		//    await page.GotoAsync(Routes.LoginPath);

		//    await page.FillAsync("#emailInput", SharedContext.TestClientEmail);

		//    await page.FillAsync("#passwordInput", SharedContext.FirstPassword);

		//    await page.Locator("button[type=submit]")
		//        .ClickAsync();

		//    //Assert

		//    await page.WaitForURLAsync(Routes.);
			
		//    errors.Should().BeEmpty();




		//}
	}
}
