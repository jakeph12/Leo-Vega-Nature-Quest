using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Window_Game_Answer : Window_Interface
{
    private Action m_acOnFrirst,m_acOnSecond;
    [SerializeField] 
    private Button m_btFirst,m_btSecond;
    [SerializeField] 
    private Image m_imMain;
    public void Start()
    {
        if(m_imMain)
            m_imMain.sprite = Animal_Holder.m_spCurrentSprite;
    }
    public virtual void Inits(Action fr,Action sc)
    {
        m_acOnFrirst = fr;
        m_acOnSecond = sc;
        m_btFirst.onClick.AddListener(()=>m_acOnFrirst?.Invoke());
        m_btSecond.onClick.AddListener(()=> m_acOnSecond?.Invoke());

    }
}
