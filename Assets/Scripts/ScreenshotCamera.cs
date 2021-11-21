using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScreenshotCamera : MonoBehaviour
{

    public int ScreenshotWidth = 1920;
    public int ScreenshotHeight = 1080;

    private Camera camera;
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
        string filename = ScreenShotName(ScreenshotWidth, ScreenshotHeight);
        System.IO.File.WriteAllBytes(filename, bytes);
        Debug.Log(string.Format("Took screenshot to: {0}", filename));
    }

    public static string ScreenShotName(int width, int height) {
         return string.Format("{0}/screenshots/screen_{1}x{2}_{3}.png", 
                              Application.dataPath, 
                              width, height, 
                              System.DateTime.Now.ToString("yyyy-MM-dd_HH-mm-ss"));
    }
}
