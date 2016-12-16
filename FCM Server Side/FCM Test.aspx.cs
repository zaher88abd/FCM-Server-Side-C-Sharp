using System;
using System.IO;
using System.Net;
using System.Text;
using System.Web.Script.Serialization;

namespace FCM_Server_Side
{
    public partial class FcmTest : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }

        protected void Button1_Click(object sender, EventArgs e)
        {
            AndroidFCMPushNotificationStatus s = SendNotification(tbDeviceId.Text, int.Parse(tbData.Text));
            Result.Text = "Error" + s.Error + "<br> Resones" + s.Response + "<br> sucsse" + s.Successful;
        }
        



        public AndroidFCMPushNotificationStatus SendNotification(string deviceId, int RequestID)//send notification
        {
            AndroidFCMPushNotificationStatus result = new AndroidFCMPushNotificationStatus();
            string serverApiKey = Constanse.AppKey;  //Key from Firebase Cloud Message

            try
            {
                result.Successful = false;
                result.Error = null;

                WebRequest tRequest = WebRequest.Create("https://fcm.googleapis.com/fcm/send");
                tRequest.Method = "POST";
                tRequest.ContentType = "application/json";
                tRequest.Headers.Add(string.Format("Authorization: key={0}", serverApiKey));

                var data = new //Json of notification
                {
                    to = deviceId,
                    data = new { request_id = RequestID }
                };


                var serilazer = new JavaScriptSerializer();
                var json = serilazer.Serialize(data);
                Byte[] byteArray = Encoding.UTF8.GetBytes(json);
                tRequest.ContentLength = byteArray.Length;


                using (Stream dataStream = tRequest.GetRequestStream())
                {
                    dataStream.Write(byteArray, 0, byteArray.Length);

                    using (WebResponse tResponse = tRequest.GetResponse())
                    {
                        using (Stream dataStreamResponse = tResponse.GetResponseStream())
                        {
                            using (StreamReader tReader = new StreamReader(dataStreamResponse))
                            {
                                String sResponseFromServer = tReader.ReadToEnd();
                                result.Response = sResponseFromServer;
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                result.Successful = false;
                result.Response = null;
                result.Error = ex;
            }

            return result;
        }


        public class AndroidFCMPushNotificationStatus
        {
            public bool Successful
            {
                get;
                set;
            }

            public string Response
            {
                get;
                set;
            }
            public Exception Error
            {
                get;
                set;
            }
        }
    }
}