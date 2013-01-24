﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;
using GameEngine.Drawing;
using GameEngine.GameObjects;

namespace GameEngine.Interfaces
{
    /// <summary>
    /// Ground Pallette interface that specifies the required properties and methods that would allow
    /// for the Game to proporly be able to render the pallette.
    /// </summary>
    public interface IGroundPallette : ILoadable
    {
        void DrawGroundTexture(SpriteBatch SpriteBatch, Map GameMap, int X, int Y, Rectangle DesRectangle);

        Color GetTileColor(byte TileType);

        int TileCount { get; }
    }
}
