using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

partial class LuksoWallet : MonoBehaviour
{
    public static void Close(ClickEvent evt, VisualElement walletContainer, TemplateContainer templateContainer)
    {
        Debug.Log("Close button");
        OpenLuksoWallet.isOpen = false;
        walletContainer.Remove(templateContainer);
    }
}
