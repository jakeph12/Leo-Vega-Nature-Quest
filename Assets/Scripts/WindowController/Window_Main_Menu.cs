using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting.Dependencies.NCalc;
using UnityEngine;
using UnityEngine.UI;

public class Window_Main_Menu : Window_Interface
{
    public Text m_txCount;
    public Window_Interface winint;
    public void Start()
    {

        if (PlayerPrefs.GetInt("first", 0) == 0)
        {
            m_gmBlock.SetActive(true); 
            var c = Window_Controll_Main.OpenWindow(winint).GetComponent<Window_Interface>();
            c.m_acOnEndDell = () =>
                {
                    m_gmBlock.SetActive(false);
                };
        }

        if (m_txCount)
        {
            OnCurStateCh(Animal_Holder.m_inCurId);
            Animal_Holder.m_evOnChValue += OnCurStateCh;
            m_acOnStartDell += () =>
            {
                Animal_Holder.m_evOnChValue -= OnCurStateCh;
            };
        }
    }
    public void OnCurStateCh(int number)
    {
        m_txCount.text = $"{number}/{Animal_Holder.m_lsAllAnimal.Count}";
    }
}
