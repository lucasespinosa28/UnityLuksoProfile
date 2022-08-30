using Nethereum.ABI;
using Nethereum.Hex.HexConvertors.Extensions;
using SimpleFileBrowser;
using System.Collections;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.UIElements;

public class OpenFileBrowser : MonoBehaviour
{
    [SerializeField]
    string token = "";
    void OnEnable()
    {
        FileBrowser.SetFilters(true, new FileBrowser.Filter("Images", ".jpg", ".png"), new FileBrowser.Filter("Text Files", ".txt", ".pdf"));
        FileBrowser.SetExcludedExtensions(".lnk", ".tmp", ".zip", ".rar", ".exe");
        FileBrowser.SetDefaultFilter(".jpg");
        FileBrowser.AddQuickLink("Users", "C:\\Users", null);
    }
    public void UploadFile(TextField hash, TextField url)
    {
        StartCoroutine(ShowLoadDialogCoroutine(hash, url));
    }
    public IEnumerator ShowLoadDialogCoroutine(TextField hash, TextField url)
    {
        yield return FileBrowser.WaitForLoadDialog(FileBrowser.PickMode.FilesAndFolders, true, null, null, "Load Files and Folders", "Load");
        Debug.Log(FileBrowser.Success);

        if (FileBrowser.Success)
        {
            var fileInfo = FileBrowser.Result[0].Split('\\');
            var filesName = fileInfo[fileInfo.Length - 1].Split('.');
            byte[] bytes = FileBrowserHelpers.ReadBytesFromFile(FileBrowser.Result[0]);
            var Encode = new ABIEncode();
            var hashFunction = Encode.GetSha3ABIEncodedPacked(bytes.ToHex())
                .ToHex();
            hash.value = $"0x{hashFunction}";
            WWWForm form = new WWWForm();
            form.AddBinaryData("file", bytes, filesName[0].ToString(), $"image/{filesName[1]}");
            using (UnityWebRequest request = UnityWebRequest.Post("https://api.pinata.cloud/pinning/pinFileToIPFS", form))
            {
                request.SetRequestHeader("Authorization", "Bearer " + token);
                //request.SetRequestHeader("Content-Type", "image/png");
                yield return request.SendWebRequest();
                if (request.result != UnityWebRequest.Result.Success)
                {
                    Debug.LogError($"Error: {request.error}");
                    Debug.LogError(request.downloadHandler.text);
                }
                else
                {
                    Debug.Log("Form upload complete!");
                    var TextResult = request.downloadHandler.text;
                    Debug.Log(TextResult);
                    url.value = TextResult.Split('"')[3];
                }
            }
        }

    }

}
