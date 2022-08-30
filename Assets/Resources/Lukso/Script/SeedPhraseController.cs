using NBitcoin;
using Nethereum.HdWallet;
using Newtonsoft.Json.Linq;
using UnityEditor;
using UnityEngine;
using UnityEngine.UIElements;
using Nethereum.Hex.HexConvertors.Extensions;
public class SeedPhraseController
{
    TemplateContainer templateContainer;
    VisualElement walletContainer;
    private int[] Lists = new int[3];
    private string[] Words = new string[3];
    public SeedPhraseController()
    {
        walletContainer = GameObject.Find("UIDocument").GetComponent<UIDocument>().rootVisualElement;
        templateContainer = Resources.Load<VisualTreeAsset>("Lukso/SeedPhrase").Instantiate();
        templateContainer.Q<Button>("Back").RegisterCallback<ClickEvent>(BackEvent);
        templateContainer.Q<Button>("ConfirmStep1").RegisterCallback<ClickEvent>(ShowConfirm);
        templateContainer.Q<Button>("ConfirmStep2").RegisterCallback<ClickEvent>(InformationEvent);
        ConfirmSeed();
        GenereteSeed();
    }
    public TemplateContainer Create() => templateContainer;
    public void ShowConfirm(ClickEvent evt)
    {
        Debug.Log("show confirm");
        var containerSeed = templateContainer.Q<VisualElement>("ContainerSeed");
        containerSeed.AddToClassList("hidden");
        var confirmSeed = templateContainer.Q<VisualElement>("ConfirmSeed");
        confirmSeed.AddToClassList("show");
    }
    public void BackEvent(ClickEvent evt)
    {
        walletContainer.Remove(templateContainer);
        walletContainer.Add(new WelcomeController().Create());
    }
    public void InformationEvent(ClickEvent evt)
    {
        Debug.Log($"#1-{templateContainer.Q<TextField>("FirstWord").value}-{Words[0]}");
        Debug.Log($"#2-{templateContainer.Q<TextField>("SecondWord").value}-{Words[1]}");
        Debug.Log($"#3-{templateContainer.Q<TextField>("ThirdWord").value}-{Words[2]}");
        if (
             templateContainer.Q<TextField>("FirstWord").value == Words[0] &&
            templateContainer.Q<TextField>("SecondWord").value == Words[1] &&
            templateContainer.Q<TextField>("ThirdWord").value == Words[2]
            )
        {
            walletContainer.Add(new walletSetPasswordController().Create());
            walletContainer.Remove(templateContainer);
        }
    }
    public void GenereteSeed()
    {
        var ContainerSeed = templateContainer.Q<VisualElement>("ContainerSeed");
        Mnemonic mnemo = new Mnemonic(Wordlist.English, WordCount.TwentyFour);
        for (int i = 0; i < mnemo.Words.Length; i++)
        {
            var seed = new Label($" {i + 1} - {mnemo.Words[i]}");
            Debug.Log(seed.text);
            seed.AddToClassList("ImportSeedInput");
            ContainerSeed.Add(seed);
        }
        Words[0] = mnemo.Words[Lists[0]];
        Words[1] = mnemo.Words[Lists[1]];
        Words[2] = mnemo.Words[Lists[2]];

        var wallet = new Wallet(mnemo.ToString(), "");
        PlayerPrefs.SetString("Address", wallet.GetAccount(0).Address);
        PlayerPrefs.SetString("key", $"0x{wallet.GetPrivateKey(0).ToHex()}");
    }
    public void ConfirmSeed()
    {
        Lists[0] = Random.Range(0, 23);
        Lists[1] = Random.Range(0, 23);
        Lists[2] = Random.Range(0, 23);
        while (Lists[0] == Lists[1])
        {
            Lists[1] = Random.Range(0, 23);
        }
        while (Lists[1] == Lists[2])
        {
            Lists[2] = Random.Range(0, 23);
        }
        while (Lists[2] == Lists[0])
        {
            Lists[2] = Random.Range(0, 23);
        }
        templateContainer.Q<TextField>("FirstWord").label = $"{Lists[0]+1} - Word";
        templateContainer.Q<TextField>("SecondWord").label = $"{Lists[1]+1} - Word";
        templateContainer.Q<TextField>("ThirdWord").label = $"{Lists[2]+1} - Word";
    }
}
