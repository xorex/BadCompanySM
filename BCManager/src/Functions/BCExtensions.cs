﻿using UnityEngine;

namespace BCM
{
  public static class BCExtensions
  {
    public static string ToStringRgb(this Color c)
    {
      return $"{c.r:F3}, {c.g:F3}, {c.b:F3}, {c.a:F3}";
    }
    public static string ToStringRgbHex(this Color c)
    {
      return $"#{(int) (c.r * 255):X2}{(int) (c.g * 255):X2}{(int) (c.b * 255):X2}";
    }
  }
}
