using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Window_Game_Lose : Window_Game_Answer
{
    public GameObject m_gmMainText;
    public Text m_txFirst,m_txSecond;
    public void Inits(Action fr, Action sc,bool b = true)
    {
        base.Inits(fr, sc);
        if (b)
        {
            m_txFirst.text = "Try Again";
            m_txSecond.text = "Home";
            m_gmMainText.SetActive(true);
        }
        else
        {
            m_txFirst.text = "Read the Stroy";
            m_txSecond.text = "Go Home";
            m_gmMainText.SetActive(false);
        }
    }
}
