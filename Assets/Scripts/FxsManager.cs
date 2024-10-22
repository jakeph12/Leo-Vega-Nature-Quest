using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FxsManager : MonoBehaviour
{
    public static FxsManager m_sinThis;
    private AudioSource m_audMain;
    [SerializeField]
    private AudioClip m_clMain;
    // Start is called before the first frame update
    public void Awake()
    {
        m_sinThis = this;
        m_audMain = GetComponent<AudioSource>();
        m_audMain.volume = PlayerPrefs.GetFloat("Volume", 1);
    }

    public static void PlayOneShot(AudioClip cli) => m_sinThis.m_audMain.PlayOneShot(cli);
    public void Click() => PlayOneShot(m_clMain);
    public static float GetVolume() => m_sinThis.m_audMain.volume;

    public static void SetVolume(float vo)
    {
        m_sinThis.m_audMain.volume = vo;
        PlayerPrefs.SetFloat("Volume",vo);

    }
}
