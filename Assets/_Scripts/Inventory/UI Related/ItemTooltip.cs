using System.Text;
using TMPro;
using UnityEngine;

namespace PirateJam.Inventory.UI_Related
{
    public class ItemTooltip : MonoBehaviour
    {
        [SerializeField] TMP_Text ItemNameText;
        [SerializeField] TMP_Text ItemSlotText;
        [SerializeField] TMP_Text ItemStatsText;

        private StringBuilder sb = new StringBuilder();

        public void ShowToolTip(EquipableItem item)
        {
            // initialize text elements to the item
            ItemNameText.text = item.ItemName;
            ItemSlotText.text = item.EquipmentType.ToString();

            // initialize string builder
            sb.Length = 0;
            AddStat(item.StrengthBonus, "Strength");
            AddStat(item.AgilityBonus, "Agility");
            AddStat(item.VitalityBonus, "Vitality");

            AddStat(item.StrengthPercentBonus, "Strength", isPercent: true);
            AddStat(item.AgilityPercentBonus, "Agility", isPercent: true);
            AddStat(item.VitalityPercentBonus, "Vitality", isPercent: true);

            ItemStatsText.text = sb.ToString();

            gameObject.SetActive(true);
        }
        
        public void HideToolTip()
        { 
            gameObject.SetActive(false);
        }

        private void AddStat(float value, string statName, bool isPercent = false)
        {
            if (value != 0)
            {
                if (sb.Length > 0)
                    sb.AppendLine();

                if (value > 0)
                    sb.Append("+");

                if (isPercent)
                {
                    sb.Append(value);
                    sb.Append("% ");
                } else {
                    sb.Append(value);
                    sb.Append(" ");
                }
                sb.Append(statName);
            }
        }
    }
}