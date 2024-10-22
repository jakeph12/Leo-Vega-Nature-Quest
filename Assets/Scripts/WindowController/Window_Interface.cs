using DG.Tweening;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Window_Interface : MonoBehaviour
{

    private Vector2 m_vcStartPos;
    public Action m_acOnInit;
    public Action m_acOnStartInit;
    public Action m_acOnStartDell;
    public Action m_acOnEndDell;
    [SerializeField]
    protected GameObject m_gmBlock;


    public void Awake()
    {
        m_vcStartPos = transform.localPosition;
        m_acOnInit += () =>
        {
            m_gmBlock.SetActive(false);
        };
    }
    public virtual void Close(float time = 1f)
    {
        if(time > 0)
            FxsManager.m_sinThis.Click();

        m_gmBlock.SetActive(true);
        m_acOnStartDell?.Invoke();
        transform.DOScale(Vector2.zero, time).OnComplete(() =>
        {
            m_acOnEndDell?.Invoke();
            Destroy(gameObject);
        });
    }
    public virtual void Open(float time = 1f)
    {
        m_acOnStartInit?.Invoke();
        transform.localScale = Vector2.zero;
        transform.DOScale(Vector2.one, time).OnComplete(()=>m_acOnInit?.Invoke());
    }
    public virtual void OpenOther(Window_Interface wi) 
    {
        FxsManager.m_sinThis.Click();

        if (m_gmBlock)
            m_gmBlock.SetActive(true);
        var t = Window_Controll_Main.OpenWindow(wi, TypeWindow.Second).GetComponent<Window_Interface>();
        t.m_acOnEndDell += () =>
        {
            if(m_gmBlock)
                m_gmBlock.SetActive(false);
        };
    }
    public virtual GameObject OpenAtherRet(Window_Interface wi)
    {
        
        if (m_gmBlock)
            m_gmBlock.SetActive(true);
        var t = Window_Controll_Main.OpenWindow(wi, TypeWindow.Second).GetComponent<Window_Interface>();
        t.m_acOnEndDell += () =>
        {
            if (m_gmBlock)
                m_gmBlock.SetActive(false);
        };
        return t.gameObject;
    }
    public virtual void SetBlocK(bool b)
    {
        m_gmBlock.SetActive(b);
    }
}
