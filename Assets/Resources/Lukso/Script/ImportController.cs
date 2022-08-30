using UnityEditor;
using UnityEngine;
using UnityEngine.UIElements;
using Nethereum.Hex.HexConvertors.Extensions;
using Nethereum.HdWallet;

public class ImportController
{
    TemplateContainer templateContainer;
    VisualElement walletContainer;
    public ImportController()
    {
        walletContainer = GameObject.Find("UIDocument").GetComponent<UIDocument>().rootVisualElement;
        templateContainer = Resources.Load<VisualTreeAsset>("Lukso/ImportSeed").Instantiate();
        templateContainer.Q<Button>("Back").RegisterCallback<ClickEvent>(BackEvent);
        templateContainer.Q<Button>("Confirm").RegisterCallback<ClickEvent>(setPassword);
        var containerImportSeed = templateContainer.Q("containerImportSeed");
        for (int i = 0; i < 24; i++)
        {
            string _text = $"word-{i + 1}";
            var input = new TextField() { name = _text, value = _text, label = null };
            input.AddToClassList("ImportSeedInput");
            containerImportSeed.Add(input);
        }
    }
    public TemplateContainer Create() => templateContainer;
    public void BackEvent(ClickEvent evt)
    {
        walletContainer.Remove(templateContainer);
        walletContainer.Add(new WelcomeController().Create());
    }

    public void setPassword(ClickEvent evt)
    {

        var words = new string[24];
        for (int i = 0; i < 24; i++)
        {
            string _text = $"word-{i + 1}";
            var input = templateContainer.Q<TextField>(_text);
            words[i] = input.value;
            //var input = new TextField() { name = _text, value = _text, label = null };
        }
        var wallet = new Wallet(string.Join(" ", words), "");
        PlayerPrefs.SetString("Address", wallet.GetAccount(0).Address);
        PlayerPrefs.SetString("key", $"0x{wallet.GetPrivateKey(0).ToHex()}");
        walletContainer.Add(new walletSetPasswordController().Create());
        walletContainer.Remove(templateContainer);
    }
}
