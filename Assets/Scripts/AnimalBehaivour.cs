using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[CreateAssetMenu(fileName ="New Animal",menuName ="Game/New Animal")]
public class AnimalBehaivour : ScriptableObject
{
    public int inId;
    public Sprite spMain;
    public string strDanger, strStry;
}
