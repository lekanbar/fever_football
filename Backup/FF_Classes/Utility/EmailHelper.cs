using System;
using System.Net;
using System.Net.Mail;
using System.Configuration;

namespace FF_Classes
{
    /// <summary>
    /// Email Helper to facilitate Email sending.
    /// </summary>
    public class EmailHelper
    {
        //Message elements
        //private String smMailServer;
        private String smTo;
        private String smFrom;
        private String smMsg;
        private String smSubject;
        private String smCC;
        private String passWord;
        private MailMessage mMessage;
        //private String smPort;
        private Char[] ccSeparator = { ',', ';' };

        public EmailHelper()
        {
            //Initialize variables
            //smMailServer = "";
            mMessage = new MailMessage();
            smTo = "";
            smFrom = "";
            smMsg = "";
            smSubject = "";
            smCC = "";
            passWord = "";
            //smPort = "";
        }

        public String To
        {
            get
            {
                return smTo;
            }
            set
            {
                smTo = value;
            }
        }

        public String Password
        {
            get
            {
                return passWord;
            }
            set
            {
                passWord = value;
            }
        }

        public String From
        {
            get
            {
                return smFrom;
            }
            set
            {
                smFrom = value;
            }
        }

        public String MessageBody
        {
            get
            {
                return smMsg;
            }
            set
            {
                smMsg = value;
            }
        }

        public String Subject
        {
            get
            {
                return smSubject;
            }
            set
            {
                smSubject = value;
            }
        }

        public String CCopy
        {
            get
            {
                return smCC;
            }
            set
            {
                smCC = value;
            }
        }

        ///<summary>
        ///Method to facilitate Attaching of files to email
        ///</summary>
        ///<param name="attachment">The file path of the attachment</param>
        public void AddAttachments(string attachment)
        {
            // Create  the file attachment for this e-mail message.
            Attachment data = new Attachment(attachment);
            mMessage.Attachments.Add(data);
        }

        ///<summary>
        ///Method to send email based on set properties
        ///</summary>
        public String Send()
        {
            //send message
            try
            {
                mMessage.From = new MailAddress(smFrom);
                //In case of sending to more than one address.
                if (smTo != "" || smTo != String.Empty)
                {
                    if (smTo.IndexOfAny(ccSeparator) > 0)
                    {
                        //mMessage.To.Clear();
                        String[] strTo = smTo.Split(ccSeparator);
                        foreach (String aTo in strTo)
                        {
                            mMessage.To.Add(aTo.Trim());
                        }
                    }
                    else
                        mMessage.To.Add(smTo);

                }
                mMessage.Subject = smSubject;
                mMessage.Body = smMsg;
                mMessage.IsBodyHtml = true;


                //In case of CC, handle here.
                if (smCC != "" || smCC != String.Empty)
                {
                    String[] strCC = smCC.Split(ccSeparator);
                    foreach (String aCC in strCC)
                    {
                        mMessage.CC.Add(aCC.Trim());
                    }
                }

                //Now, send the message using Web.config settings
                NetworkCredential cred = new NetworkCredential(From, Password);
                SmtpClient mailClient = new SmtpClient("mail.theqoffice.com");
                //mailClient.EnableSsl = true;
                //mailClient.UseDefaultCredentials = false;
                mailClient.Credentials = cred;

                //Send the Message
                mailClient.Send(mMessage);
                //Display alert message
                return ("Sent");
                //data.Dispose();
            }
            catch (FormatException fex)
            {
                return ("Mail format exception: " + fex.Message);
            }
            catch (SmtpException Smtpex)
            {
                return ("SMTP exception: " + Smtpex.Message);
            }
            catch (Exception ex)
            {
                return ("General exception: " + ex.Message);
            }
        }
    }
}