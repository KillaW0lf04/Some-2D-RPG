﻿using System;
using System.Globalization;
using Microsoft.Xna.Framework;

//Code from http://thedeadpixelsociety.com/2012/01/hex-colors-in-xna/
namespace GameEngine.Extensions
{
    public static class ColorExtensions
    {
        /// <summary>
        /// Creates an ARGB hex string representation of the <see cref="Color"/> value.
        /// </summary>
        /// <param name="color">The <see cref="Color"/> value to parse.</param>
        /// <param name="includeHash">Determines whether to include the hash mark (#) character in the string. Defaults to true if not specified.</param>
        /// <returns>A hex string representation of the specified <see cref="Color"/> value.</returns>
        public static string ToHex(Color color, bool includeHash=true)
        {
            string[] argb = {
                color.A.ToString("X2"),
                color.R.ToString("X2"),
                color.G.ToString("X2"),
                color.B.ToString("X2"),
            };
            return (includeHash ? "#" : string.Empty) + string.Join(string.Empty, argb);
        }

        /// Creates a <see cref="Color"/> value from an ARGB or RGB hex string.  The string may
        /// begin with or without the hash mark (#) character.
        /// </summary>
        /// <param name="hexString">The ARGB hex string to parse.</param>
        /// <returns>
        /// A <see cref="Color"/> value as defined by the ARGB or RGB hex string.
        /// </returns>
        /// <exception cref="InvalidOperationException">Thrown if the string is not a valid ARGB or RGB hex value.</exception>
        public static Color ToColor(string hexString)
        {
            if (hexString.StartsWith("#"))
                hexString = hexString.Substring(1);
            uint hex = uint.Parse(hexString, System.Globalization.NumberStyles.HexNumber, CultureInfo.InvariantCulture);
            Color color = Color.White;
            if (hexString.Length == 8)
            {
                color.A = (byte)(hex >> 24);
                color.R = (byte)(hex >> 16);
                color.G = (byte)(hex >> 8);
                color.B = (byte)(hex);
            }
            else if (hexString.Length == 6)
            {
                color.R = (byte)(hex >> 16);
                color.G = (byte)(hex >> 8);
                color.B = (byte)(hex);
            }
            else
            {
                throw new InvalidOperationException("Invald hex representation of an ARGB or RGB color value.");
            }
            return color;
        }

        public static Color Transition(Color fromColor, Color toColor, float percentage, bool includeAlpha=true)
        {
            Color difference = ColorExtensions.Subtract(toColor, fromColor, includeAlpha);
            difference = ColorExtensions.Multiply(difference, percentage, includeAlpha);

            return ColorExtensions.Add(fromColor, difference, includeAlpha);
        }

        public static Color Inverse(Color color)
        {
            return new Color(255 - color.R, 255 - color.G, 255 - color.B, 255); 
        }

        public static Color Multiply(Color color, float value, bool includeAlpha=true)
        {
            byte alpha = (includeAlpha) ? (byte)Math.Min(color.A * value, 255) : color.A;

            return new Color(
                (byte)Math.Min(color.R * value, 255),
                (byte)Math.Min(color.G * value, 255),
                (byte)Math.Min(color.B * value, 255),
                alpha
                );
        }

        public static Color Divide(Color color, float value, bool includeAlpha=true)
        {
            byte alpha = (includeAlpha) ? (byte)(color.A / value) : color.A;

            return new Color(
                (byte)(color.R / value),
                (byte)(color.G / value),
                (byte)(color.B / value),
                alpha
                );
        }

        public static Color Add(Color color1, Color color2, bool includeAlpha=true)
        {
            byte alpha = (includeAlpha) ? (byte)Math.Min(color1.A + color2.A, 255) : color1.A;

            return new Color(
                (byte) Math.Min(color1.R + color2.R, 255),
                (byte) Math.Min(color1.G + color2.G, 255),
                (byte) Math.Min(color1.B + color2.B, 255),
                alpha
                );
        }

        public static Color Subtract(Color color1, Color color2, bool includeAlpha=true)
        {
            byte alpha = (includeAlpha)? (byte) Math.Max(color1.A - color2.A, 0) : color1.A;

            return new Color(
                (byte) Math.Max(color1.R - color2.R, 0),
                (byte) Math.Max(color1.G - color2.G, 0),
                (byte) Math.Max(color1.B - color2.B, 0),
                alpha
                );
        }
    }
}
