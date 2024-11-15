// Controllers/HomeController.cs
public class HomeController : Controller
{
	private readonly EmailService _emailService;

	public HomeController(EmailService emailService)
	{
		_emailService = emailService;
	}

	public async Task<IActionResult> SendEmail()
	{
		await _emailService.SendEmailAsync("recipient@example.com", "Test Email", "<h1>Hello, this is a test email!</h1>");
		return Ok("Email sent successfully!");
	}
}
