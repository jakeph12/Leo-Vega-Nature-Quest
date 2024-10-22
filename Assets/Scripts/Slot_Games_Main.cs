using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class Slot_Games_Main : MonoBehaviour,IDropHandler
{
    private Image m_inAll;
    private bool _m_bEmty = true;
    public bool m_bEmty 
    {
        get => _m_bEmty;
        set
        {
            _m_bEmty = value;
            m_inAll.color = Color.white;
            if(_m_bEmty) m_inAll.sprite = null;
        }
    }

    public void Awake()
    {
        m_inAll = GetComponent<Image>();
    }

    public void OnDrop(PointerEventData eventData)
    {
        if (!m_bEmty) return;
        if(Main_Slot_Controller.m_sinThis.m_gmSlot)
            if (Main_Slot_Controller.m_sinThis.m_gmSlot.m_slCurrent.m_inId == transform.GetSiblingIndex())
            {
                m_bEmty = false;
                m_inAll.sprite = eventData.pointerDrag.GetComponent<Slot_Game>().m_slCurrent.m_spMain;
                Destroy(eventData.pointerDrag.gameObject);
                Main_Slot_Controller.m_sinThis.SetA(false);
            }
    }

    public void OnEnter(bool act)
    {

        if (!m_bEmty || !Main_Slot_Controller.m_sinThis.m_gmSlot) return;
        if (act)
        {
            if (Main_Slot_Controller.m_sinThis.m_gmSlot.m_slCurrent.m_inId == transform.GetSiblingIndex())
                m_inAll.color = Color.gray;

        }
        else
        {
            if (Main_Slot_Controller.m_sinThis.m_gmSlot.m_slCurrent.m_inId == transform.GetSiblingIndex())
                m_inAll.color = Color.white;
        }

    }
}
