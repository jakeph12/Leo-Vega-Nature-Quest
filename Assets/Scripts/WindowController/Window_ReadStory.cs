using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Window_ReadStory : Window_Interface
{
    [SerializeField]
    private Text m_txMain;
    [SerializeField]
    private Image m_imMain;

    public void Start()
    {
        if(Animal_Holder.m_inCurId != Animal_Holder.m_lsAllAnimal.Count - 1)
        {
            m_txMain.text = $"{Animal_Holder.m_lsAllAnimal[Animal_Holder.m_inCurId - 1].strStry}";
            m_imMain.sprite = Animal_Holder.m_lsAllAnimal[Animal_Holder.m_inCurId - 1].spMain;
        }
        else
        {
            m_txMain.text = $"{Animal_Holder.m_lsAllAnimal[Animal_Holder.m_inCurId].strStry}";
            m_imMain.sprite = Animal_Holder.m_lsAllAnimal[Animal_Holder.m_inCurId].spMain;
        }

    }
}
