using BACKEND_MNG;
using SendGrid.Helpers.Mail.Model;

namespace ViewModels
{
    public class EmailViewModels
    {
        public string? emailReceiver { get; set; } = null!;

        public PurchaseViewModel? purchase { get; set; } = null!;

        public async Task sendEmail()
        {
            EmailSender sender = new EmailSender();
            string employeeeName = "Joao";

            string html = new string("<p>Hi "+ emailReceiver + "</p><br><p>" + purchase.supplier+"     "+ employeeeName+"     "+ purchase.net + "     " + purchase.status + "</p><br><p>" + "Please click at the link below to action It.</p>");


            string body = "Hi " + emailReceiver + " \n " + purchase.supplier + "\t" + employeeeName + " \t"
                + "\t" + purchase.net + "\t" + purchase.status + "\n\n" + "Please click at the link below to action It.";

            await sender.sendEmail(emailReceiver, "Joao Marques","Alex",html,"Purchase");
        }

    }
}
