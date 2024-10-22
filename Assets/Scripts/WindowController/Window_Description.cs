using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Window_Description : Window_Interface
{
    [SerializeField]
    private Text m_txMain;
    public Text anName;
    [SerializeField]
    private Image m_imMain;

    void Start()
    {
        anName.text = Animal_Holder.m_lsAllAnimal[Animal_Holder.m_inCurId].name;
        m_txMain.text = $"{Animal_Holder.m_lsAllAnimal[Animal_Holder.m_inCurId].strDanger}";
        m_imMain.sprite = Animal_Holder.m_spCurrentSprite;
    }
}
