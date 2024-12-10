using System.Collections;
using UnityEngine;
using System;
using System.IO;
using System.Linq;
using SFB;

public class ScreenCapture : MonoBehaviour
{
    // only allow screenshot button on supported platforms
    RuntimePlatform[] allowedPlatforms =    {   RuntimePlatform.IPhonePlayer,
                                                RuntimePlatform.Android,
                                                RuntimePlatform.WindowsPlayer,
                                                RuntimePlatform.WindowsEditor,
                                                RuntimePlatform.WindowsServer,
                                                RuntimePlatform.OSXPlayer,
                                                RuntimePlatform.LinuxPlayer,
                                                RuntimePlatform.WebGLPlayer
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
        yield return new WaitForEndOfFrame();

        int width = Screen.width;
        int height = Screen.height;
        Texture2D texture = new Texture2D(width, height, TextureFormat.RGB24, false);

        // read screen
        texture.ReadPixels(new Rect(0, 0, width, height), 0, 0);
        texture.Apply();

        // encode texture into image
        byte[] data = texture.EncodeToPNG();
        string filename = "gooper-" + DateTime.Now.ToString("yyyy-MM-dd-HH-mm-ss");

        // check platform
        RuntimePlatform platform = Application.platform;
        if (platform == RuntimePlatform.Android || platform == RuntimePlatform.IPhonePlayer) {
            saveScreenshotOnMobile(data, filename);
        } else {
            saveScreenshotOnDesktopAndWebGL(data, filename);
        }

        Destroy(texture);
    }

    void saveScreenshotOnMobile(byte[] data, String filename) {
        NativeGallery.SaveImageToGallery(data, "My Grems", filename);
    }

    void saveScreenshotOnDesktopAndWebGL(byte[] data, String filename)
    {
        string panelTitle = "Select Folder";
        string defaultDirectory = Environment.GetFolderPath(Environment.SpecialFolder.UserProfile);
        string extension = "png";
        StandaloneFileBrowser.SaveFilePanelAsync(panelTitle, defaultDirectory, filename, extension, (string path) => { File.WriteAllBytes(path, data); });
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
