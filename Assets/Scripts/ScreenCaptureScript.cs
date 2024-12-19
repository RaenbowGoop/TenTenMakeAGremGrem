using System.Collections;
using UnityEngine;
using System;
using System.IO;
using System.Linq;
using SFB;
using UnityEngine.Playables;
using TMPro;

public class ScreenCapture : MonoBehaviour
{
    [SerializeField] private PlayableDirector screenshotNotificationAnimation;
    [SerializeField] private GameObject screenshotNotificationObject;

    // only allow screenshot button on supported platforms
    RuntimePlatform[] allowedPlatforms =    {   RuntimePlatform.IPhonePlayer,
                                                RuntimePlatform.Android,
                                                RuntimePlatform.WindowsPlayer,
                                                RuntimePlatform.WindowsEditor,
                                                RuntimePlatform.WindowsServer,
                                                RuntimePlatform.OSXPlayer,
                                                RuntimePlatform.LinuxPlayer
                                                // RuntimePlatform.WebGLPlayer
                                            };

    private void Start() {
        if (!allowedPlatforms.Contains(Application.platform)) {
            GameObject.FindGameObjectWithTag("ScreenCapture").SetActive(false);
        }
    }

    public void callCaptureAndSaveScreenshot() {
        StartCoroutine(captureAndSaveScreenshot());
    }
    private IEnumerator captureAndSaveScreenshot() {
        // Hide Screenshot Notification
        screenshotNotificationObject.SetActive(false);

        // Hide Mouse Click Animation
        TextMeshProUGUI mouseClickAnimation = GameObject.FindGameObjectWithTag("MouseClickAnimation").transform.GetChild(0).transform.GetChild(0).GetComponent<TextMeshProUGUI>();
        if (mouseClickAnimation != null) {
            mouseClickAnimation.color = new Color(mouseClickAnimation.color.r, mouseClickAnimation.color.g, mouseClickAnimation.color.b, 0);
        }

        yield return new WaitForEndOfFrame();

        // Screenshot parameters
        int width = Screen.width;
        int height = Screen.height;

        Rect screenshotBounds;
        Texture2D texture;

        // Platform Type
        RuntimePlatform platform = Application.platform;

        // Define Screenshot boudns based on platform
        if (platform == RuntimePlatform.Android || platform == RuntimePlatform.IPhonePlayer)
        {
            int screenshotWidth = (int)((4.0 / 3.0) * height);
            int widthSpace = (width - screenshotWidth)/2;
            screenshotBounds = new Rect(widthSpace, 0, screenshotWidth, height);
            texture = new Texture2D(screenshotWidth, height, TextureFormat.RGB24, false);
        } else
        {
            screenshotBounds = new Rect(0, 0, width, height);
            texture = new Texture2D(width, height, TextureFormat.RGB24, false);
        }

        // read screen
        texture.ReadPixels(screenshotBounds, 0, 0);
        texture.Apply();

        string filename = "gooper-" + DateTime.Now.ToString("yyyy-MM-dd-HH-mm-ss");

        // check platform
        if (platform == RuntimePlatform.Android || platform == RuntimePlatform.IPhonePlayer) {
            saveScreenshotOnMobile(texture, filename);
        } else {
            // encode texture into image
            byte[] data = texture.EncodeToPNG();
            saveScreenshotOnDesktopAndWebGL(data, filename);
        }

        Destroy(texture);

        // Unhide Screenshot Notification
        screenshotNotificationObject.SetActive(true);

        // Unhide Mouse Click Animation
        if (mouseClickAnimation != null)
        {
            mouseClickAnimation.color = new Color(mouseClickAnimation.color.r, mouseClickAnimation.color.g, mouseClickAnimation.color.b, 100);
        }
    }

    void saveScreenshotOnMobile(Texture2D data, String filename) {
        NativeGallery.SaveImageToGallery(data, "My Grems", filename, (callback, path) =>
        {
            if (callback == false)
            {
                screenshotNotificationObject.transform.GetChild(0).GetComponent<TextMeshProUGUI>().text = "Failed to Save Screenshot to Camera Roll.";
                screenshotNotificationAnimation.Stop();
                screenshotNotificationAnimation.Play();
            } else {
                screenshotNotificationObject.transform.GetChild(0).GetComponent<TextMeshProUGUI>().text = "Screenshot Saved Under \"My Grems\" Album.";
                screenshotNotificationAnimation.Stop();
                screenshotNotificationAnimation.Play();
            }
        }
        );
    }

    void saveScreenshotOnDesktopAndWebGL(byte[] data, String filename)
    {
        string panelTitle = "Select Folder";
        string defaultDirectory = Environment.GetFolderPath(Environment.SpecialFolder.UserProfile);
        string extension = "png";
        StandaloneFileBrowser.SaveFilePanelAsync(panelTitle, defaultDirectory, filename, extension, (string path) => {
            try
            {
                File.WriteAllBytes(path, data);
                screenshotNotificationObject.transform.GetChild(0).GetComponent<TextMeshProUGUI>().text = "Screenshot Saved!";
                screenshotNotificationAnimation.Stop();
                screenshotNotificationAnimation.Play();
            }
            catch (Exception e) {
                screenshotNotificationObject.transform.GetChild(0).GetComponent<TextMeshProUGUI>().text = "Failed to Save Screenshot.";
                screenshotNotificationAnimation.Stop();
                screenshotNotificationAnimation.Play();
            }
        });
    }

    /* void saveScreenshotOnDesktop(byte[] data, String filename) {
        string panelTitle = "Select Folder";
        string defaultDirectory = Environment.GetFolderPath(Environment.SpecialFolder.UserProfile);
        string extension = ".png";
        FileBrowser.ShowLoadDialog( (paths) => { File.WriteAllBytes(paths[0] + "\\" + filename + extension, data); }, 
                                    null, FileBrowser.PickMode.Folders, false, null, null, panelTitle, "Select" );
    }
    */
}
