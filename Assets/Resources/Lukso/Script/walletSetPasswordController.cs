using UnityEditor;
using UnityEngine;
using UnityEngine.UIElements;

public class walletSetPasswordController
{
    TemplateContainer templateContainer;
    VisualElement walletContainer;

    public walletSetPasswordController()
    {
        walletContainer = GameObject.Find("UIDocument").GetComponent<UIDocument>().rootVisualElement;
        templateContainer = Resources.Load<VisualTreeAsset>("Lukso/setPassword").Instantiate();
        templateContainer.Q<Button>("Back").RegisterCallback<ClickEvent>(BackEvent);
        templateContainer.Q<Button>("Confirm").RegisterCallback<ClickEvent>(InformationEvent);
    }
    public TemplateContainer Create() => templateContainer;
    public void BackEvent(ClickEvent evt)
    {
        walletContainer.Remove(templateContainer);
        walletContainer.Add(new WelcomeController().Create());
    }

    public void InformationEvent(ClickEvent evt)
    {
        var first = templateContainer.Q<TextField>("PasswordInput1");
        var second = templateContainer.Q<TextField>("PasswordInput2");
        if (first.value.Length > 4 && second.value.Length > 4 && first.value == second.value)
        {
            PlayerPrefs.SetString("Password", first.value);  
            walletContainer.Add(new InformationController().Create());
            walletContainer.Remove(templateContainer);
        }
        first.AddToClassList("Error_input_password");
        second.AddToClassList("Error_input_password");
    }
}
