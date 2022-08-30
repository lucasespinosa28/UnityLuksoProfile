using SimpleFileBrowser;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEditor;
using UnityEngine;
using UnityEngine.UIElements;

partial class LuksoWallet : MonoBehaviour
{
    [SerializeField]
    public static VisualElement WalletContainer;
    private void OnEnable()
    {
        WalletContainer = GameObject.Find("UIDocument").GetComponent<UIDocument>().rootVisualElement;
    }
}
