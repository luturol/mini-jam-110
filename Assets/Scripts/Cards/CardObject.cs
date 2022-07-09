using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Card", menuName = "ScriptableObjects/Card", order = 1)]
public class CardObject : ScriptableObject
{
    public CardType CardType;
    public Color Color;
}
