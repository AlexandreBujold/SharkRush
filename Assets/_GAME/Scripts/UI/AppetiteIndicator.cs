using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

namespace SharkRush
{
    public class AppetiteIndicator : MonoBehaviour
    {
        public Image creatureImage;
        public TextMeshProUGUI creatureName;

        public CreatureUI seaLion;
        public CreatureUI dolphin;
        public CreatureUI seaTurtle;

        public void UpdateIndicator(CreatureType type)
        {
            CreatureUI target = seaLion;
            switch (type)
            {
                case CreatureType.SeaLion:
                    target = seaLion;
                    break;

                case CreatureType.Dolphin:
                    target = dolphin;
                    break;

                case CreatureType.SeaTurtle:
                    target = seaTurtle;
                    break;
            }
            ApplyCreatureInfo(target);
        }

        private void ApplyCreatureInfo(CreatureUI UIInfo)
        {
            if (UIInfo == null)
                return;

            if (creatureImage)
                creatureImage.sprite = UIInfo.creatureSprite;

            if (creatureName)
                creatureName.SetText(UIInfo.creatureName);
        }
    }
}
