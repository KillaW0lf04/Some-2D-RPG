﻿
namespace Some2DRPG.Items
{
    public enum DamageType { Blunt, Slashing, Piercing }

    public class Weapon : Item
    {
        public int Damage { get; set; }
        public DamageType DamageType { get; set; }
    }
}
