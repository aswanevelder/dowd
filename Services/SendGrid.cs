using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using dowd.Models;
using SendGrid;
using SendGrid.Helpers.Mail;

namespace dowd.Services
{
    public class SendGridService : ISendGridService
    {
        private string _siteEmail;
        private string _logo;
        private string _message;
        private string _sendGridKey;
        private string _sendGridTemplateQuote;
        private string _sendGridTemplateMessage;
        private string _title;
        private string _siteName;

        public SendGridService()
        {
            _siteEmail = Environment.GetEnvironmentVariable("SITE_EMAIL");
            _siteName = Environment.GetEnvironmentVariable("SITE_NAME");
            _logo = Environment.GetEnvironmentVariable("SITE_LOGO");
            _sendGridKey = Environment.GetEnvironmentVariable("SENDGRID_KEY");
            _sendGridTemplateQuote = Environment.GetEnvironmentVariable("SENDGRID_TMP_QUOTE");
            _sendGridTemplateMessage = Environment.GetEnvironmentVariable("SENGRID_TMP_MESSAGE");
        }

        public async Task<Response> SendQuote(Quote model, List<string> files)
        {
            try
            {
                _message = Environment.GetEnvironmentVariable("QUOTE_MESSAGE");
                _title = Environment.GetEnvironmentVariable("QUOTE_TITLE");

                List<DataModel> data = new List<DataModel>();
                if (model != null)
                {
                    data.Add(new DataModel { data = $"Company: {model.Company}" });
                    data.Add(new DataModel { data = $"Contact Person: {model.ContactPerson}" });
                    data.Add(new DataModel { data = $"Email: {model.Email}" });
                    data.Add(new DataModel { data = $"Phone: {model.Phone}" });
                    data.Add(new DataModel { data = $"Product: {model.Product}" });
                    foreach (var file in files)
                        data.Add(new DataModel { data = $"File: {file}" });
                    data.Add(new DataModel { data = $"Width: {model.Width} mm" });
                    data.Add(new DataModel { data = $"Height: {model.Height} mm" });
                    data.Add(new DataModel { data = $"Installation Space Width: {model.InstallationSpaceWidth} mm" });
                    data.Add(new DataModel { data = $"Installation Space Height: {model.InstallationSpaceHeight} mm" });
                    data.Add(new DataModel { data = $"Quantity: {model.Quantity}" });
                    data.Add(new DataModel { data = $"Material: {model.Material}" });
                    if (model.AttachAdhesive)
                        data.Add(new DataModel { data = $"Attach Adhesive: Yes" });
                    if (model.AttachHoles)
                        data.Add(new DataModel { data = $"Attach Holes: Yes" });
                    if (model.AttachLabelPins)
                        data.Add(new DataModel { data = $"Attach Label Pins: Yes" });
                    if (model.AttachMagneticStrip)
                        data.Add(new DataModel { data = $"Attach Magnetic Strip: Yes" });
                    if (model.AttachMount)
                        data.Add(new DataModel { data = $"Attach Mount: Yes" });
                    if (model.AttachPegs)
                        data.Add(new DataModel { data = $"Attach Pegs: Yes" });
                    if (model.AttachSpacers)
                        data.Add(new DataModel { data = $"Attach Spacers: Yes" });
                    data.Add(new DataModel { data = $"Attach Other: {model.AttachOther}" });

                    var install = (model.InstallationRequired) ? "Yes" : "No";
                    data.Add(new DataModel { data = $"Installation Required: {install}" });
                    data.Add(new DataModel { data = $"Where did you hear of DOWD: {model.HowDidYouFindUs}" });
                    data.Add(new DataModel { data = $"Message: {model.Message}" });

                    var sendGridClient = new SendGridClient(_sendGridKey);
                    var sendGridMessage = new SendGridMessage();
                    sendGridMessage.SetFrom(model.Email, model.ContactPerson);
                    sendGridMessage.AddTo(_siteEmail, _siteName);
                    sendGridMessage.SetTemplateId(_sendGridTemplateQuote);
                    sendGridMessage.SetTemplateData(new MessageModel
                    {
                        rows = data,
                        image = _logo,
                        message = _message,
                        title = _title,
                        subtitle = string.Empty
                    });

                    var response = await sendGridClient.SendEmailAsync(sendGridMessage);

                    sendGridMessage.AddTo(model.Email, model.ContactPerson);
                    sendGridMessage.SetFrom(_siteEmail, _siteName);
                    response = await sendGridClient.SendEmailAsync(sendGridMessage);
                    return response;
                }
                else
                    return null;
            }
            catch (Exception ex)
            {
                string s = ex.Message;
                return null;
            }
        }

        public async Task<Response> SendContact(Contact model)
        {
            try
            {
                _message = Environment.GetEnvironmentVariable("CONTACT_MESSAGE");
                _title = Environment.GetEnvironmentVariable("CONTACT_TITLE");

                List<DataModel> data = new List<DataModel>();
                if (model != null)
                {
                    data.Add(new DataModel { data = $"Company: {model.Company}" });
                    data.Add(new DataModel { data = $"Contact Person: {model.ContactPerson}" });
                    data.Add(new DataModel { data = $"Email: {model.Email}" });
                    data.Add(new DataModel { data = $"Phone: {model.Phone}" });
                    data.Add(new DataModel { data = $"Message: {model.Message}" });

                    var sendGridClient = new SendGridClient(_sendGridKey);
                    var sendGridMessage = new SendGridMessage();
                    sendGridMessage.SetFrom(model.Email, model.ContactPerson);
                    sendGridMessage.AddTo(_siteEmail, _siteName);
                    sendGridMessage.SetTemplateId(_sendGridTemplateMessage);
                    sendGridMessage.SetTemplateData(new MessageModel
                    {
                        rows = data,
                        image = _logo,
                        message = _message,
                        title = _title,
                        subtitle = string.Empty
                    });

                    var response = await sendGridClient.SendEmailAsync(sendGridMessage);

                    sendGridMessage.AddTo(model.Email, model.ContactPerson);
                    sendGridMessage.SetFrom(_siteEmail, _siteName);
                    response = await sendGridClient.SendEmailAsync(sendGridMessage);
                    return response;
                }
                else
                    return null;
            }
            catch (Exception ex)
            {
                string s = ex.Message;
                return null;
            }
        }
    }
}