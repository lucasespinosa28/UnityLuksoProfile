using UnityEditor;
using UnityEngine;
using UnityEngine.UIElements;

public class WelcomeController: MonoBehaviour
{
    TemplateContainer templateContainer;
    VisualElement walletContainer;
    public WelcomeController()
    {
        walletContainer = GameObject.Find("UIDocument").GetComponent<UIDocument>().rootVisualElement;
        templateContainer = Resources.Load<VisualTreeAsset>("lukso/Welcome").Instantiate();
        if (PlayerPrefs.HasKey("Password"))
        {
            Debug.Log(PlayerPrefs.GetString("Password"));
            templateContainer.Q<VisualElement>("isPassword").AddToClassList("show");
        }
        templateContainer.Q<Button>("Open").RegisterCallback<ClickEvent>(InformationOpen);
        templateContainer.Q<Button>("Import").RegisterCallback<ClickEvent>(ImportOpen);
        templateContainer.Q<Button>("NewWallet").RegisterCallback<ClickEvent>(seedPhraseOpen);
        templateContainer.Q<Button>("CloseButton").RegisterCallback<ClickEvent>((evt) => LuksoWallet.Close(evt, walletContainer, templateContainer));
    }
    private void OnEnable()
    {
        Debug.Log("OnEnable");
    }
        public TemplateContainer Create() => templateContainer;
    private void InformationOpen(ClickEvent evt)
    {
        Debug.Log("Information");
        //PasswordInput
        var input = templateContainer.Q<TextField>("PasswordInput");
        if(input.value == PlayerPrefs.GetString("Password"))
        {
            LuksoWallet.WalletContainer.Add(new InformationController().Create());
            LuksoWallet.WalletContainer.Remove(templateContainer);
        }
    }
    private void ImportOpen(ClickEvent evt)
    {
        Debug.Log("ImportOpen");
        walletContainer.Add(new ImportController().Create());
        walletContainer.Remove(templateContainer);
    }

    private void seedPhraseOpen(ClickEvent evt)
    {
        Debug.Log("seedPhraseOpen");
        walletContainer.Add(new SeedPhraseController().Create());
        walletContainer.Remove(templateContainer);
    }
}
