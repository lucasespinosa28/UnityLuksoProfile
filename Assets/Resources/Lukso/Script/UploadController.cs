using Nethereum.ABI;
using SimpleFileBrowser;
using System.Collections;
using Nethereum.Hex.HexConvertors.Extensions;
using System.IO;
using UnityEditor;
using UnityEngine;
using UnityEngine.UIElements;

public class uploadController : MonoBehaviour
{
    TemplateContainer templateContainer;
    VisualElement walletContainer;
    OpenFileBrowser UploadFile;
    LuksoContract contract;
    PinataUpload profile;
    public uploadController()
    {
        var gameObject = GameObject.Find("UIDocument");
        walletContainer = gameObject.GetComponent<UIDocument>().rootVisualElement;
        templateContainer = Resources.Load<VisualTreeAsset>("Lukso/UploadProfile").Instantiate();
      
        templateContainer.Q<Button>("Back").RegisterCallback<ClickEvent>(BackEvent);
        templateContainer.Q<Button>("UploadAvatar").RegisterCallback<ClickEvent>(UploadAvatarEvent);
        templateContainer.Q<Button>("UploadBrackground").RegisterCallback<ClickEvent>(UploadBrackgroundEvent);
        templateContainer.Q<Button>("DeployButton").RegisterCallback<ClickEvent>(DeployLuskoProfile);
        templateContainer.Q<Button>("UploadProfile").RegisterCallback<ClickEvent>(UploadIPFSProfile);
        templateContainer.Q<Button>("Update").RegisterCallback<ClickEvent>(UpdateProfile);
        UploadFile = gameObject.GetComponent<OpenFileBrowser>();
        if (PlayerPrefs.HasKey("ProfileAddress"))
        {
            templateContainer.Q<TextField>("ContractAddress").value = PlayerPrefs.GetString("ProfileAddress");

        }
        contract = gameObject.GetComponent<LuksoContract>();
        profile = gameObject.GetComponent<PinataUpload>();
    }
    public TemplateContainer Create() => templateContainer;
    void DeployLuskoProfile(ClickEvent evt)
    {
        var input = templateContainer.Q<TextField>("ContractAddress");
        contract.Deploy(input);
    }
    void UploadAvatarEvent(ClickEvent evt)
    {
        var hash = templateContainer.Q<TextField>("HashAvatar");
        var url = templateContainer.Q<TextField>("UrlAvatar");
        Debug.Log("Upload avatar");
        UploadFile.UploadFile(hash, url);
    }
    void UploadBrackgroundEvent(ClickEvent evt)
    {
        TextField hash = templateContainer.Q<TextField>("BrackgroundHash");
        var url = templateContainer.Q<TextField>("BackgroundUrl");
        Debug.Log("Upload Background");
        UploadFile.UploadFile(hash, url);
        

    }
    void UploadIPFSProfile(ClickEvent evt)
    {
        var name = templateContainer.Q<TextField>("NameInput");
        var description = templateContainer.Q<TextField>("DescriptionInput");
        var hashProfile = templateContainer.Q<TextField>("HashAvatar");
        var urlProfile = templateContainer.Q<TextField>("UrlAvatar");
        var hashBackground = templateContainer.Q<TextField>("BrackgroundHash");
        var urlBackground = templateContainer.Q<TextField>("BackgroundUrl");
        var ProfileUrl = templateContainer.Q<TextField>("ProfileUrl");
        profile.Profile(
            ProfileUrl,
            name.value,
            description.value,
            urlProfile.value,
            hashProfile.value,
            urlBackground.value,
            hashBackground.value);
    }
    void UpdateProfile(ClickEvent evt)
    {
        var name = templateContainer.Q<TextField>("NameInput");
        var description = templateContainer.Q<TextField>("DescriptionInput");
        var hashProfile = templateContainer.Q<TextField>("HashAvatar");
        var urlProfile = templateContainer.Q<TextField>("UrlAvatar");
        var hashBackground = templateContainer.Q<TextField>("BrackgroundHash");
        var urlBackground = templateContainer.Q<TextField>("BackgroundUrl");
        contract.UpdateProfile(
            name.value, 
            description.value,
            urlProfile.value,
            hashProfile.value,
            urlBackground.value,
            hashBackground.value);

    }
    void BackEvent(ClickEvent evt)
    {
        walletContainer.Add(new InformationController().Create());
        walletContainer.Remove(templateContainer);
    }
}
