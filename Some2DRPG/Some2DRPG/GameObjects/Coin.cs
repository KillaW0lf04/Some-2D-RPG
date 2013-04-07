﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using GameEngine.Drawing;
using GameEngine.GameObjects;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework;
using GameEngine;

namespace Some2DRPG.GameObjects
{
    public enum CoinType { Gold, Silver, Copper };

    public class Coin : Entity
    {
        public int CoinValue { get; set; }

        public CoinType CoinType {
            get { return _coinType; }
            set
            {
                CurrentDrawableState = value.ToString();
                _coinType = value;
            }
        }

        private CoinType _coinType;

        public Coin(float X, float Y, int CoinValue, CoinType CoinType)
            : base(X, Y)
        {
            this.CoinType = CoinType;
            this.ScaleX = 0.7f;
            this.ScaleY = 0.7f;
            this.CoinValue = CoinValue;
        }

        public override void LoadContent(ContentManager Content)
        {
            //Load the coin animation
            Animation.LoadAnimationXML(this.Drawables, "Animations/Misc/coin.anim", Content);
        }

        public override void Update(GameTime GameTime, TeeEngine Engine)
        {
            Entity player = Engine.GetEntity("Player");

            float distance = Vector2.Distance(Pos, player.Pos);

            if (distance < 100)
                this.Drawables.SetGroupProperty("", "Color", Color.Red);
            else
                this.Drawables.SetGroupProperty("", "Color", Color.White);
        }

        public override string ToString()
        {
            return string.Format(
                "Coin: CoinValue={0}, CoinType={1}, Pos={2}",
                CoinValue,
                CoinType,
                Pos );

        }
    }
}
