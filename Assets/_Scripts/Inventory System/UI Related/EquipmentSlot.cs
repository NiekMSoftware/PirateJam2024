namespace PirateJam.Inventory_System.UI_Related
{
    public class EquipmentSlot : ItemSlot
    {
        public EquipmentType EquipmentType;

        protected override void OnValidate()
        {
            base.OnValidate();
            gameObject.name = EquipmentType.ToString() + " Slot";
        }

        public override bool CanReceiveItem(Item item)
        {
            if (item == null)
                return true;

            EquipableItem equipableItem = item as EquipableItem;
            return equipableItem != null && equipableItem.EquipmentType == EquipmentType;
        }
    }
}