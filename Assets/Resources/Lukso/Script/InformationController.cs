using UnityEditor;
using UnityEngine;
using UnityEngine.UIElements;

public class InformationController
{
    TemplateContainer templateContainer;
    VisualElement walletContainer;
    LuksoContract contract;
    public InformationController()
    {
        var gameObject = GameObject.Find("UIDocument");
        walletContainer = gameObject.GetComponent<UIDocument>().rootVisualElement;
        templateContainer = Resources.Load<VisualTreeAsset>("Lukso/Information").Instantiate();
        templateContainer.Q<Button>("CloseButton").RegisterCallback<ClickEvent>((evt) => LuksoWallet.Close(evt, walletContainer, templateContainer));
        templateContainer.Q<Button>("NewProfile").RegisterCallback<ClickEvent>(NewProfileEvent);
        templateContainer.Q<Button>("ImportProfile").RegisterCallback<ClickEvent>(ImportProfileEvent);
        var address = templateContainer.Q<TextField>("Address");
        address.value = $"{PlayerPrefs.GetString("Address")}";
        address.RegisterCallback<ClickEvent>((evt) =>
        {
            CopyText(evt, $"{PlayerPrefs.GetString("Address")}");
        });
        
        var profile = templateContainer.Q<TextField>("ProfileAddress");
        profile.value = $"{PlayerPrefs.GetString("ProfileAddress")}";
        profile.RegisterCallback<ClickEvent>((evt) =>
        {
            CopyText(evt, $"{PlayerPrefs.GetString("ProfileAddress")}");
        });

        contract = gameObject.GetComponent<LuksoContract>();
    }
    public void CopyText(ClickEvent evt,string text)
    {
        CopyToClipboard(text);
    }
    void CopyToClipboard(string str)
    {
        TextEditor textEditor = new TextEditor();
        textEditor.text = str;
        textEditor.SelectAll();
        textEditor.Copy();
    }
    public void ImportProfileEvent(ClickEvent evt)
    {
        var _label = templateContainer.Q<Label>("Welcome");
        contract.ProfileUrl(_label);
    }
    public TemplateContainer Create() => templateContainer;
    public void NewProfileEvent(ClickEvent evt)
    {
        walletContainer.Add(new uploadController().Create());
        walletContainer.Remove(templateContainer);
    }
}
