using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using System.Net;
using System.Net.Mail;
using System.Net.Security;
using System.Security.Cryptography.X509Certificates;
using System.ComponentModel;
public class ScreenshotCamera : MonoBehaviour
{

    public int ScreenshotWidth = 1920;
    public int ScreenshotHeight = 1080;

    private Camera camera;
    private string filename;
    // Start is called before the first frame update
    void Start()
    {
        camera = GetComponent<Camera>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void TakeScreenshot()
    {
        RenderTexture renderTexture = new RenderTexture(ScreenshotWidth, 1080, 24);
        camera.targetTexture = renderTexture;
        Texture2D screenShot = new Texture2D(ScreenshotWidth, ScreenshotHeight);
        camera.Render();
        RenderTexture.active = renderTexture;
        screenShot.ReadPixels(new Rect(0, 0, ScreenshotWidth, ScreenshotHeight), 0, 0);
        camera.targetTexture = null;
        RenderTexture.active = null; 
        Destroy(renderTexture);
        byte[] bytes = screenShot.EncodeToPNG();
        filename = ScreenShotName(ScreenshotWidth, ScreenshotHeight);
        System.IO.File.WriteAllBytes(filename, bytes);
        Debug.Log(string.Format("Took screenshot to: {0}", filename));
        SendEmail();
    }

    public static string ScreenShotName(int width, int height) {
         return string.Format("{0}/screenshots/screen_{1}x{2}_{3}.png", 
                              Application.dataPath, 
                              width, height, 
                              System.DateTime.Now.ToString("yyyy-MM-dd_HH-mm-ss"));
    }

    private void SendEmail()
    {
        SmtpClient client = new SmtpClient("smtp.gmail.com", 587);
        client.Credentials = new System.Net.NetworkCredential(
            "cmsachanggeng@gmail.com",
            "wbk2394394");
        client.EnableSsl = true;
        // Specify the email sender.
        // Create a mailing address that includes a UTF8 character
        // in the display name.
        MailAddress from = new MailAddress(
            "cmsachanggeng@gmail.com",
            "PekingOperaFace",
            System.Text.Encoding.UTF8);
        // Set destinations for the email message.
        MailAddress to = new MailAddress("cmsachanggeng@gmail.com");
        // Specify the message content.
        MailMessage message = new MailMessage(from, to);
        message.Body = "This is the Peking Opera Face Mask you draw today. ";
        message.BodyEncoding = System.Text.Encoding.UTF8;
        message.Subject = "Peking Opera Face Mask";
        message.SubjectEncoding = System.Text.Encoding.UTF8;

        Attachment data = new Attachment(filename);
        message.Attachments.Add(data);
        // Set the method that is called back when the send operation ends.
        client.SendCompleted += new SendCompletedEventHandler(SendCompletedCallback);
        // The userState can be any object that allows your callback
        // method to identify this send operation.
        // For this example, the userToken is a string constant.
        string userState = "test message1";
        client.SendAsync(message, userState);

    }

    private static void SendCompletedCallback(object sender, AsyncCompletedEventArgs e)
    {
        // Get the unique identifier for this asynchronous operation.
        string token = (string)e.UserState;

        if (e.Cancelled)
        {
            Debug.Log("Send canceled " + token);
        }
        if (e.Error != null)
        {
            Debug.Log("[ " + token + " ] " + " " + e.Error.ToString());
        }
        else
        {
            Debug.Log("Message sent.");
        }
    }
}
