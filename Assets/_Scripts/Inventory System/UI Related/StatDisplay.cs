using PirateJam.CharacterStats;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;

namespace PirateJam.Inventory_System.UI_Related
{
    public class StatDisplay : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
    {
        private CharacterStat _stat;
        public CharacterStat Stat { 
            get { return _stat; }
            set
            {
                _stat = value;
                UpdateStatValue();
            }
        }

        private string _name;
        public string Name {
            get { return _name; }
            set { 
                _name = value;
                nameText.text = _name.ToLower();
            }
        }

        [SerializeField] private TMP_Text nameText;
        [SerializeField] private TMP_Text valueText;
        [SerializeField] private StatTooltip tooltip;

        private void OnValidate()
        {
            TMP_Text[] texts = GetComponentsInChildren<TMP_Text>();
            nameText = texts[0];
            valueText = texts[1];

            if (tooltip == null)
                tooltip = FindObjectOfType<StatTooltip>();
        }

        public void UpdateStatValue()
        {
            valueText.text = _stat.Value.ToString();
        }

        public void OnPointerEnter(PointerEventData eventData)
        {
            tooltip.ShowToolTip(Stat, Name);
        }

        public void OnPointerExit(PointerEventData eventData)
        {
            tooltip.HideToolTip();
        }
    }
}