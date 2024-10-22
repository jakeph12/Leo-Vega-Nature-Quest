using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;

public class Animal_Holder
{
    public static bool m_bInit = false;

    private static void Init()
    {
        if (m_bInit) return;
        m_bInit = true;


        _m_inCurId = PlayerPrefs.GetInt("CurrentIdPlayer", 0);
        _m_lsAllAnimal = Resources.LoadAll<AnimalBehaivour>("Animals").OrderBy(value => value.inId).ToList();
        if (_m_inCurId < _m_lsAllAnimal.Count)
            _m_spCurrentSprite = _m_lsAllAnimal[_m_inCurId].spMain;
        else
            _m_spCurrentSprite = _m_lsAllAnimal[_m_lsAllAnimal.Count - 1].spMain;
    }


    private static Sprite _m_spCurrentSprite;
    private static int _m_inCurId;
    private static List<AnimalBehaivour> _m_lsAllAnimal = new List<AnimalBehaivour>();


    public static Sprite m_spCurrentSprite
    {
        get
        {
            Init();
            return _m_spCurrentSprite;
        }
    }
    public static int m_inCurId
    {
        get
        {
            Init();
            return _m_inCurId;
        }
        set
        {
            Init();

            _m_inCurId = value;

            if (_m_inCurId > m_lsAllAnimal.Count)
                _m_inCurId = m_lsAllAnimal.Count - 1;



            if(_m_inCurId < m_lsAllAnimal.Count)
                _m_spCurrentSprite = m_lsAllAnimal[_m_inCurId].spMain;
            else
                _m_spCurrentSprite = _m_lsAllAnimal[_m_lsAllAnimal.Count - 1].spMain;
            m_evOnChValue?.Invoke(_m_inCurId);
            PlayerPrefs.SetInt("CurrentIdPlayer", _m_inCurId);
        }
    }

    public delegate void ChValueInt(int value);
    public static event ChValueInt m_evOnChValue;

    public static List<AnimalBehaivour> m_lsAllAnimal
    {
        get
        {
            Init();
            return _m_lsAllAnimal;
        }
    }

}
