using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Window_MyAnimal : Window_Interface
{
    [SerializeField]
    private GameObject m_gmFirstSlot,m_gmSecondSlot,m_gmPrefab,m_gmFirstPanel,m_gmSecondPanel;
    [SerializeField]
    private Text m_txMain;
    [SerializeField]
    private Button m_btFirst, m_btSecond;
    [SerializeField]
    private GameObject m_gmRunner;
    // Start is called before the first frame update
    void Start()
    {
        m_txMain.text = $"{Animal_Holder.m_inCurId}/{Animal_Holder.m_lsAllAnimal.Count}";
        for (int i = 0;i < Animal_Holder.m_inCurId;i++)
        {
            int id = i;
            int cur = PlayerPrefs.GetInt("Animal" +id,0);
            GameObject m_gm = null;
            if(cur == 1)
                m_gm = Instantiate(m_gmPrefab, m_gmFirstSlot.transform);
            else if(cur == 2)
                m_gm = Instantiate(m_gmPrefab, m_gmSecondSlot.transform);

            if(m_gm)
                m_gm.transform.GetChild(0).GetComponent<Image>().sprite = Animal_Holder.m_lsAllAnimal[id].spMain;
        }

        
    }
    public void SetPF()
    {
        m_btFirst.interactable = false;
        m_btFirst.GetComponentInChildren<Text>().color = Color.white;
        m_gmRunner.transform.position = m_btFirst.transform.position;
        m_btSecond.interactable = true;
        m_btSecond.GetComponentInChildren<Text>().color = Color.black;
        m_gmFirstPanel.SetActive(true);
        m_gmSecondPanel.SetActive(false);
    }
    public void SetPS()
    {
        m_btSecond.interactable = false;
        m_btSecond.GetComponentInChildren<Text>().color = Color.white;
        m_gmRunner.transform.position = m_btSecond.transform.position;
        m_btFirst.interactable = true;
        m_btFirst.GetComponentInChildren<Text>().color = Color.black;
        m_gmSecondPanel.SetActive(true);
        m_gmFirstPanel.SetActive(false);
    }

}
