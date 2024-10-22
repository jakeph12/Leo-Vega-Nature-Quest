using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SettingsBehaivour : Window_Interface
{
    [SerializeField]
    private Toggle m_tgMain;

    public void Start()
    {
        m_tgMain.isOn = FxsManager.GetVolume() == 1;
        m_tgMain.onValueChanged.AddListener(OnCh);
    }
    public void OnCh(bool b)
    {
        if (b)
            FxsManager.SetVolume(1);
        else 
            FxsManager.SetVolume(0);
    }
    public void OpenWW(Window_Interface wi) 
    {
        FxsManager.m_sinThis.Click();
        SetBlocK(true);
        var W = Window_Controll_Main.OpenWindow(wi,TypeWindow.NonClose).GetComponent<Window_Interface>();
        W.m_acOnEndDell += () =>
        {
            SetBlocK(false);
        };
    }
}
