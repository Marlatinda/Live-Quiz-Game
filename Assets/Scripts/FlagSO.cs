using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[CreateAssetMenu(menuName ="Nation Flag",fileName ="New Nation")]
public class FlagSO : ScriptableObject
{
    [SerializeField] string nationName;
    [SerializeField] Sprite nationFlag;
    public string getNationName()
    {
        return nationName;
    }
    public Sprite getNationFlag()
    {
        return nationFlag;
    }
}
