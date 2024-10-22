using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;


[RequireComponent(typeof(CanvasGroup))]
public class Slot_Game : MonoBehaviour , IDragHandler ,IBeginDragHandler,IEndDragHandler
{
    public Slot m_slCurrent;
    private int m_inId;
    private Transform m_trCur;
    private Main_Slot_Controller m_scrAll;
    private CanvasGroup m_cnGroop;
    public Action m_acOnDestroy;

    private Transform m_cuP;

    public void Awake()
    {
        m_cuP = transform.parent.transform;
        m_trCur = transform.parent.transform.parent.transform;
        m_scrAll = m_trCur.GetComponent<Main_Slot_Controller>();
        m_cnGroop = GetComponent<CanvasGroup>();
    }


    public void OnBeginDrag(PointerEventData eventData)
    {
        m_inId = transform.GetSiblingIndex();
        m_scrAll.SetA(true);
        m_cnGroop.blocksRaycasts = false;
        transform.SetParent(m_trCur.parent.transform);
        transform.localScale = Vector2.one / 2;
        m_scrAll.m_gmSlot = this;
        //throw new System.NotImplementedException();
    }

    public void OnDrag(PointerEventData eventData)
    {
        transform.position = Input.mousePosition;
    }

    public void OnEndDrag(PointerEventData eventData)
    {
        m_scrAll.SetA(false);
        m_scrAll.m_gmSlot = null;
        m_cnGroop.blocksRaycasts = true;
        transform.SetParent(m_cuP);
        transform.SetSiblingIndex(m_inId);
        transform.localScale = Vector2.one;
    }

    public void OnDestroy()
    {
        m_acOnDestroy?.Invoke();
    }
}
