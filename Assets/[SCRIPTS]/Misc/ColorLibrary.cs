using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace slotJupiter
{
    public static class ColorLibrary
    {
        public static Color Green = new Color(0.01960784f, 0.57647059f, 0.27450980f, 1f);
        public static Color Red = new Color(0.91372549f, 0.09019608f, 0.33333333f, 1f);
        public static Color Blue = new Color(0.09019608f, 0.30588235f, 0.91372549f, 1f);

        public static string SetColorText(Color color, string text)
        {
            string hexColor = ColorUtility.ToHtmlStringRGBA(color);
            return $"<color=#{hexColor}>{text}</color>";
        }
    }
}
