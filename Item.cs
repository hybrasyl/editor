using HybrasylEditor.UI;
using System.ComponentModel;
using System.Drawing.Design;

namespace HybrasylEditor
{
    public class Item
    {
        private string name = "New Item";
        private int sprite = 0;
        private int equipSprite = -1;
        private int displaySprite = 0;
        private Sex sex = Sex.Neutral;
        private Class @class = Class.Peasant;
        private ItemType type = ItemType.UsableItem;
        private WeaponType weaponType = WeaponType.None;
        private EquipSlot equipSlot = EquipSlot.None;
        private Element element = Element.None;
        private int bodyStyle = 0;

        private bool repairable = true;
        private bool enchantable = false;
        private bool depositable = true;
        private bool droppable = true;
        private bool vendorable = true;
        private bool tailorable = false;
        private bool smithable = false;
        private bool consecratable = false;
        private bool perishable = false;
        private bool exchangeable = true;

        private bool automaticConsecrateVariants = false;
        private bool automaticTailorVariants = false;
        private bool automaticSmithVariants = false;
        private bool automaticEnchantVariants = false;
        private bool automaticElementVariants = false;

        private int level = 1;
        private int ability = 0;
        private int weight = 0;
        private int color = 0;
        private int maximumStack = 1;
        private int maximumDurability = 0;
        private int value = 0;

        private int hp = 0;
        private int mp = 0;
        private int str = 0;
        private int @int = 0;
        private int wis = 0;
        private int con = 0;
        private int dex = 0;
        private int hit = 0;
        private int dmg = 0;
        private int ac = 0;
        private int mr = 0;
        private int regen = 0;
        private int minimumDamageS = 0;
        private int maximumDamageS = 0;
        private int minimumDamageL = 0;
        private int maximumDamageL = 0;

        // Tabs in category names are not displayed but are used for sorting
        private const string basicInfo = "\t\t\t\t\tBasic Info";
        private const string itemFlags = "\t\t\t\tItem Flags";
        private const string itemVariants = "\t\t\tItem Variants";
        private const string itemCharacteristics = "\t\tItem Characteristics";
        private const string attributes = "\tAttributes";

        [Category(basicInfo), Description("The item's name, what the fuck else."), DefaultValue("")]
        public string Name
        {
            get { return name; }
            set { name = value; }
        }
        [Category(basicInfo), DefaultValue(0), Editor(typeof(ItemSpriteEditor), typeof(UITypeEditor))]
        public int Sprite
        {
            get { return sprite; }
            set { sprite = value; }
        }
        [Category(basicInfo), DefaultValue(-1), Editor(typeof(ItemSpriteEditor), typeof(UITypeEditor))]
        public int EquipSprite
        {
            get { return equipSprite; }
            set { equipSprite = value; }
        }
        [Category(basicInfo), DefaultValue(0)]
        public int DisplaySprite
        {
            get { return displaySprite; }
            set { displaySprite = value; }
        }
        [Category(basicInfo), DefaultValue(Sex.Neutral)]
        public Sex Sex
        {
            get { return sex; }
            set { sex = value; }
        }
        [Category(basicInfo), DefaultValue(Class.Peasant)]
        public Class Class
        {
            get { return @class; }
            set { @class = value; }
        }
        [Category(basicInfo), DefaultValue(ItemType.UsableItem)]
        public ItemType Type
        {
            get { return type; }
            set { type = value; }
        }
        [Category(basicInfo), DefaultValue(WeaponType.None)]
        public WeaponType WeaponType
        {
            get { return weaponType; }
            set { weaponType = value; }
        }
        [Category(basicInfo), DefaultValue(EquipSlot.None)]
        public EquipSlot EquipSlot
        {
            get { return equipSlot; }
            set { equipSlot = value; }
        }
        [Category(basicInfo), DefaultValue(Element.None)]
        public Element Element
        {
            get { return element; }
            set { element = value; }
        }
        [Category(basicInfo), DefaultValue(0)]
        public int BodyStyle
        {
            get { return bodyStyle; }
            set { bodyStyle = value; }
        }

        [Category(itemFlags), DefaultValue(true)]
        public bool Repairable
        {
            get { return repairable; }
            set { repairable = value; }
        }
        [Category(itemFlags), DefaultValue(false)]
        public bool Enchantable
        {
            get { return enchantable; }
            set { enchantable = value; }
        }
        [Category(itemFlags), DefaultValue(true)]
        public bool Depositable
        {
            get { return depositable; }
            set { depositable = value; }
        }
        [Category(itemFlags), DefaultValue(true)]
        public bool Droppable
        {
            get { return droppable; }
            set { droppable = value; }
        }
        [Category(itemFlags), DefaultValue(true)]
        public bool Vendorable
        {
            get { return vendorable; }
            set { vendorable = value; }
        }
        [Category(itemFlags), DefaultValue(false)]
        public bool Tailorable
        {
            get { return tailorable; }
            set { tailorable = value; }
        }
        [Category(itemFlags), DefaultValue(false)]
        public bool Smithable
        {
            get { return smithable; }
            set { smithable = value; }
        }
        [Category(itemFlags), DefaultValue(false)]
        public bool Consecratable
        {
            get { return consecratable; }
            set { consecratable = value; }
        }
        [Category(itemFlags), DefaultValue(false)]
        public bool Perishable
        {
            get { return perishable; }
            set { perishable = value; }
        }
        [Category(itemFlags), DefaultValue(true)]
        public bool Exchangeable
        {
            get { return exchangeable; }
            set { exchangeable = value; }
        }

        [Category(itemVariants), DefaultValue(false)]
        public bool AutomaticConsecreateVariants
        {
            get { return automaticConsecrateVariants; }
            set { automaticConsecrateVariants = value; }
        }
        [Category(itemVariants), DefaultValue(false)]
        public bool AutomaticTailorVariants
        {
            get { return automaticTailorVariants; }
            set { automaticTailorVariants = value; }
        }
        [Category(itemVariants), DefaultValue(false)]
        public bool AutomaticSmithVariants
        {
            get { return automaticSmithVariants; }
            set { automaticSmithVariants = value; }
        }
        [Category(itemVariants), DefaultValue(false)]
        public bool AutomaticEnchantVariants
        {
            get { return automaticEnchantVariants; }
            set { automaticEnchantVariants = value; }
        }
        [Category(itemVariants), DefaultValue(false)]
        public bool AutomaticElementVariants
        {
            get { return automaticElementVariants; }
            set { automaticElementVariants = value; }
        }

        [Category(itemCharacteristics), DefaultValue(1)]
        public int Level
        {
            get { return level; }
            set { level = value; }
        }
        [Category(itemCharacteristics), DefaultValue(0)]
        public int Ability
        {
            get { return ability; }
            set { ability = value; }
        }
        [Category(itemCharacteristics), DefaultValue(0)]
        public int Weight
        {
            get { return weight; }
            set { weight = value; }
        }
        [Category(itemCharacteristics), DefaultValue(0)]
        public int Color
        {
            get { return color; }
            set { color = value; }
        }
        [Category(itemCharacteristics), DefaultValue(1)]
        public int MaximumStack
        {
            get { return maximumStack; }
            set { maximumStack = value; }
        }
        [Category(itemCharacteristics), DefaultValue(0)]
        public int MaximumDurability
        {
            get { return maximumDurability; }
            set { maximumDurability = value; }
        }
        [Category(itemCharacteristics), DefaultValue(0)]
        public int Value
        {
            get { return value; }
            set { this.value = value; }
        }

        [Category(attributes), DefaultValue(0)]
        public int HP
        {
            get { return hp; }
            set { hp = value; }
        }
        [Category(attributes), DefaultValue(0)]
        public int MP
        {
            get { return mp; }
            set { mp = value; }
        }
        [Category(attributes), DefaultValue(0)]
        public int Str
        {
            get { return str; }
            set { str = value; }
        }
        [Category(attributes), DefaultValue(0)]
        public int Int
        {
            get { return @int; }
            set { @int = value; }
        }
        [Category(attributes), DefaultValue(0)]
        public int Wis
        {
            get { return wis; }
            set { wis = value; }
        }
        [Category(attributes), DefaultValue(0)]
        public int Con
        {
            get { return con; }
            set { con = value; }
        }
        [Category(attributes), DefaultValue(0)]
        public int Dex
        {
            get { return dex; }
            set { dex = value; }
        }
        [Category(attributes), DefaultValue(0)]
        public int Hit
        {
            get { return hit; }
            set { hit = value; }
        }
        [Category(attributes), DefaultValue(0)]
        public int Dmg
        {
            get { return dmg; }
            set { dmg = value; }
        }
        [Category(attributes), DefaultValue(0)]
        public int AC
        {
            get { return ac; }
            set { ac = value; }
        }
        [Category(attributes), DefaultValue(0)]
        public int MR
        {
            get { return mr; }
            set { mr = value; }
        }
        [Category(attributes), DefaultValue(0)]
        public int Regen
        {
            get { return regen; }
            set { regen = value; }
        }
        [Category(attributes), DefaultValue(0)]
        public int MinimumDamageS
        {
            get { return minimumDamageS; }
            set { minimumDamageS = value; }
        }
        [Category(attributes), DefaultValue(0)]
        public int MaximumDamageS
        {
            get { return maximumDamageS; }
            set { maximumDamageS = value; }
        }
        [Category(attributes), DefaultValue(0)]
        public int MinimumDamageL
        {
            get { return minimumDamageL; }
            set { minimumDamageL = value; }
        }
        [Category(attributes), DefaultValue(0)]
        public int MaximumDamageL
        {
            get { return maximumDamageL; }
            set { maximumDamageL = value; }
        }

        public Item()
        {

        }
    }

    public enum ItemType
    {
        UsableItem,
        UnusableItem,
        Equipment
    }

    public enum WeaponType
    {
        None,
        OneHanded,
        TwoHanded,
        Dagger,
        Staff,
        Claw
    }

    public enum EquipSlot
    {
        None,
        Weapon,
        Armor,
        Shield,
        Helmet,
        Earring,
        Necklace,
        Rings,
        Gauntlet,
        Belt,
        Greaves,
        Boots,
        Accessory,
        Overcoat
    }
}
