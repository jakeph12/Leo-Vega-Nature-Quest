using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Main_Slot_Controller : MonoBehaviour
{
    public static Main_Slot_Controller m_sinThis;
    [SerializeField]
    private GameObject m_gmBlock;
    public Slot_Game m_gmSlot;

    public void Awake()
    {
        m_sinThis = this;
    }

    void Start()
    {
        m_gmBlock.SetActive(false);
        
    }


    public void SetA(bool act) => m_gmBlock.SetActive(act);
}
