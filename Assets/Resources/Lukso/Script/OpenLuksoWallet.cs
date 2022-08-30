using UnityEngine;
using UnityEngine.UI;
using UnityEngine.UIElements;

public class OpenLuksoWallet : MonoBehaviour
{
    public static bool isOpen = false;
    void Start()
    {
        gameObject.GetComponent<UnityEngine.UI.Button>().onClick.AddListener(TaskOnClick);
    }

    void TaskOnClick()
    {
        if (!isOpen)
        {
            var walletContainer = LuksoWallet.WalletContainer;
            walletContainer.Add(new WelcomeController().Create());
            isOpen = true;
        }
        Debug.Log("Open wallet");
    }
}
