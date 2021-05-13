using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace SharkRush
{
    [CreateAssetMenu(fileName = "New Creature UI", menuName = "SharkRush/CreatureUI", order = 1)]
    public class CreatureUI : ScriptableObject
    {
        public CreatureType type;
        public Sprite creatureSprite;
        public string creatureName;
    } 
}
