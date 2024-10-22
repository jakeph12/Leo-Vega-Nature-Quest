using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public enum TypeWindow 
{
    Main,
    Second,
    NonClose

}

public class Window_Controll_Main : MonoBehaviour
{
    public static Window_Controll_Main m_sinThis;
    [SerializeField]
    private GameObject m_gmMainWC,m_gmSecondWC,m_gmNonCloseWC;
    public GameObject m_gmMainWi,m_gmSecondWi;

    [SerializeField]
    private Window_Interface m_wiMain;


    public void Awake()
    {
        m_sinThis = this;
    }
    public void Start()
    {
       OpenWindow(m_wiMain, TypeWindow.Main,0);
    }
    public static GameObject OpenWindow(Window_Interface wi,TypeWindow ty = TypeWindow.Second,float time = 1f)
    {
        var tr = m_sinThis.m_gmMainWC.transform;
        switch (ty)
        {
            case TypeWindow.Main:

                if (m_sinThis.m_gmMainWi)
                    m_sinThis.m_gmMainWi.GetComponent<Window_Interface>().Close(time);

                break;

            case TypeWindow.Second:

                if(m_sinThis.m_gmSecondWi)
                    m_sinThis.m_gmSecondWi.GetComponent<Window_Interface>().Close(time);

                tr = m_sinThis.m_gmSecondWC.transform;
                break;

            case TypeWindow.NonClose:
                tr = m_sinThis.m_gmNonCloseWC.transform;
                break;
        }

        var w = Instantiate(wi.gameObject, tr);
        var wW = w.GetComponent<Window_Interface>();
        wW.Open(time);
        switch (ty)
        {
            case TypeWindow.Main:

                m_sinThis.m_gmMainWi = w;

                break;

            case TypeWindow.Second:

                m_sinThis.m_gmSecondWi = w;

                break;
        }

        return w;
    }
}
