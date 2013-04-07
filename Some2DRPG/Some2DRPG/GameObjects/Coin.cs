﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using GameEngine.Drawing;
using GameEngine.GameObjects;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework;
using GameEngine;
using Microsoft.Xna.Framework.Audio;

namespace Some2DRPG.GameObjects
{
    public enum CoinType { Gold, Silver, Copper };

    public class Coin : Entity
    {
        public int CoinValue { get; set; }

        public SoundEffect CoinSound { get; set; }

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

            CoinSound = Content.Load<SoundEffect>("Sounds/Coins/coin1");
        }

        public override void Update(GameTime GameTime, TeeEngine Engine)
        {
            float COIN_MOVE_SPEED = 5000;

            Hero player = (Hero) Engine.GetEntity("Player");

            //find the distance between the player and this coin
            float distanceSquared = Vector2.DistanceSquared(Pos, player.Pos);

            float speed = COIN_MOVE_SPEED / distanceSquared;

            if (speed > 1)
            {
                this.Pos.X += (player.Pos.X > this.Pos.X) ? speed : -1 * speed;
                this.Pos.Y += (player.Pos.Y > this.Pos.Y) ? speed : -1 * speed;

                if (this.CurrentBoundingBox.Intersects(player.CurrentBoundingBox))
                {
                    CoinSound.Play();
                    player.Coins += this.CoinValue;
                    Engine.RemoveEntity(this);
                }
            }
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
