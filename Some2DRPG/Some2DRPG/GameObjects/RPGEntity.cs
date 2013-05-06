﻿using System.Collections.Generic;
using GameEngine.Drawing;
using GameEngine.GameObjects;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Some2DRPG.Items;

namespace Some2DRPG.GameObjects
{
    public enum Direction { Left, Up, Right, Down };

    public enum AttackStance { NotAttacking, Preparing, Attacking };

    public abstract class RPGEntity : CollidableEntity
    {
        public static string HUMAN_MALE = @"Animations/Characters/male_npc.anim";
        public static string HUMAN_FEMALE = @"Animations/Characters/female_npc.anim";
        public static string CREATURES_BAT = @"Animations/Monsters/bat.anim";
        public static string CREATURES_DUMMY = @"Animations/Monsters/combat_dummy.anim";

        public string Faction { get; set; }

        public List<Item> Backpack { get; set; }
        public Dictionary<ItemType, Item> Equiped { get; set; }

        public Direction Direction { get; set; }

        public AttackStance AttackStance { get; set; }

        public string BaseRace { get; set; }

        public int HP { get; set; }
        public int XP { get; set; }
        public int Coins { get; set; }

        public RPGEntity()
        {
            Construct(0, 0, RPGEntity.HUMAN_MALE);
        }

        public RPGEntity(string baseRace)
        {
            Construct(0, 0, baseRace);
        }

        public RPGEntity(float x, float y, string baseRace)
        {
            Construct(x, y, baseRace);
        }

        private void Construct(float x, float y, string baseRace)
        {
            this.HP = 0;
            this.XP = 0;
            this.Coins = 0;
            this.Pos = new Vector2(x, y);
            this.ScaleX = 1.0f;
            this.ScaleY = 1.0f;
            this.CollisionGroup = "Shadow";
            this.Backpack = new List<Item>();
            this.Equiped = new Dictionary<ItemType, Item>();
            this.BaseRace = baseRace;

            this.AttackStance = AttackStance.NotAttacking;
            this.Direction = Direction.Right;
        }

        public override void LoadContent(ContentManager content)
        {
            DrawableSet.LoadDrawableSetXml(Drawables, BaseRace, content);

            CurrentDrawableState = "Idle_Left";
        }

        #region Interaction Methods

        public virtual void Hit(Entity sender, GameTime gameTime)
        {
        }

        #endregion

        #region Equipment Methods

        public void QuickEquip(string itemName)
        {
            Equip(ItemRepository.GameItems[itemName]);
        }

        public void Equip(Item item)
        {
            Unequip(item.ItemType);

            Equiped[item.ItemType] = item;
            Drawables.Union(item.Drawables);
        }

        public void QuickUnequip(string itemName)
        {
            Unequip(ItemRepository.GameItems[itemName]);
        }

        public void Unequip(Item item)
        {
            Unequip(item.ItemType);
        }

        public void Unequip(ItemType itemType)
        {
            if( Equiped.ContainsKey(itemType) )
                Drawables.Remove(Equiped[itemType].Drawables);

            Equiped.Remove(itemType);
        }

        public bool QuickIsEquiped(string itemName)
        {
            return IsEquiped(ItemRepository.GameItems[itemName]);
        }

        public bool IsEquiped(Item item)
        {
            if (Equiped.ContainsKey(item.ItemType))
                return Equiped[item.ItemType] == item;
            else
                return false;
        }

        #endregion

        public override string ToString()
        {
            return string.Format("RPGEntity: Faction={0}, HP={1}, Name={2}", Faction, HP, Name);
        }
    }
}