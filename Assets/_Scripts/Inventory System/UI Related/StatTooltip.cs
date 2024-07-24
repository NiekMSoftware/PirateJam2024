using System.Text;
using TMPro;
using UnityEngine;
using PirateJam.CharacterStats;

namespace PirateJam.Inventory_System.UI_Related
{
    public class StatTooltip : MonoBehaviour
    {
        [SerializeField] TMP_Text StatNameText;
        [SerializeField] TMP_Text StatModifersLabelText;
        [SerializeField] TMP_Text StatModifiersText;

        private StringBuilder sb = new StringBuilder();

        public void ShowToolTip(CharacterStat stat, string statName)
        {
            StatNameText.text = GetStatTopText(stat, statName);
            StatModifiersText.text = GetStatModifiersText(stat);

            gameObject.SetActive(true);
        }

        public void HideToolTip()
        {
            gameObject.SetActive(false);
        }

        private string GetStatTopText(CharacterStat stat, string statName)
        {
            sb.Length = 0;
            sb.Append(statName);
            sb.Append(" ");
            sb.Append(stat.Value);

            if (stat.Value != stat.BaseValue)
            {
                sb.Append(" (");
                sb.Append(stat.BaseValue);

                if (stat.Value > stat.BaseValue)
                    sb.Append("+");

                sb.Append(System.Math.Round(stat.Value - stat.BaseValue, 4));
                sb.Append(")");
            }

            return sb.ToString();
        }

        private string GetStatModifiersText(CharacterStat stat)
        {
            sb.Length = 0;

            foreach (StatModifier mod in stat.StatModifiers)
            {
                if (sb.Length > 0)
                    sb.AppendLine();

                if (mod.Value > 0)
                    sb.Append("+");

                if (mod.Type == StatModifierType.Flat)
                    sb.Append(mod.Value);
                else {
                    sb.Append(mod.Value);
                    sb.Append("%"); 
                }

                EquipableItem item = mod.Source as EquipableItem;

                if (item != null)
                {
                    sb.Append(" ");
                    sb.Append(item.ItemName);
                }
                else
                {
                    Debug.LogError("Modifier is not an EquipableItem!");
                }
            }

            return sb.ToString();
        }
    }
}