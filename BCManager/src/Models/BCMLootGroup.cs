﻿using System;
using System.Collections.Generic;

namespace BCM.Models
{
  [Serializable]
  public class BCMLootGroup : BCMAbstract
  {
    #region Filters
    public static class StrFilters
    {
      public const string Name = "name";
      public const string Template = "template";
      public const string MinCount = "mincount";
      public const string MaxCount = "maxcount";
      public const string MinQual = "minqual";
      public const string MaxQual = "maxqual";
      public const string MinLevel = "minlevel";
      public const string MaxLevel = "maxlevel";
      public const string Items = "items";
    }

    private static Dictionary<int, string> _filterMap = new Dictionary<int, string>
    {
      { 0,  StrFilters.Name },
      { 1,  StrFilters.Template },
      { 2,  StrFilters.MinCount },
      { 3,  StrFilters.MaxCount },
      { 4,  StrFilters.MinQual },
      { 5,  StrFilters.MaxQual },
      { 6,  StrFilters.MinLevel },
      { 7,  StrFilters.MaxLevel },
      { 8,  StrFilters.Items }
    };
    public static Dictionary<int, string> FilterMap => _filterMap;

    #endregion

    #region Properties
    public string Name;
    public string Template;
    public int MinCount;
    public int MaxCount;
    public int MinQual;
    public int MaxQual;
    public double MinLevel;
    public double MaxLevel;
    public List<BCMLootEntry> Items = new List<BCMLootEntry>();

    public class BCMLootEntry
    {
      public int Item;
      public string Group;
      public double Prob;
      public string Template;
      public int Min;
      public int Max;
      public int MinQual;
      public int MaxQual;
      public double MinLevel;
      public double MaxLevel;
      //public string parentGroup;

      public BCMLootEntry(LootContainer.LootEntry lootEntry)
      {
        if (lootEntry.item != null) Item = lootEntry.item.itemValue.type;
        if (lootEntry.group != null) Group = lootEntry.group.name;
        Prob = Math.Round(lootEntry.prob, 6);
        Template = lootEntry.lootProbTemplate;
        Min = lootEntry.minCount;
        Max = lootEntry.maxCount;
        MinQual = lootEntry.minQuality;
        MaxQual = lootEntry.maxQuality;
        MinLevel = Math.Round(lootEntry.minLevel, 6);
        MaxLevel = Math.Round(lootEntry.maxLevel, 6);
      }
    }
    #endregion;

    public BCMLootGroup(object obj, string typeStr, Dictionary<string, string> options, List<string> filters) : base(obj, typeStr, options, filters)
    {
    }

    public override void GetData(object obj)
    {
      var loot = obj as LootContainer.LootGroup;
      if (loot == null) return;

      if (IsOption("filter"))
      {
        foreach (var f in StrFilter)
        {
          switch (f)
          {
            case StrFilters.Name:
              GetName(loot);
              break;
            case StrFilters.Template:
              GetTemplate(loot);
              break;
            case StrFilters.MinCount:
              GetMinCount(loot);
              break;
            case StrFilters.MaxCount:
              GetMaxCount(loot);
              break;
            case StrFilters.MinQual:
              GetMinQual(loot);
              break;
            case StrFilters.MaxQual:
              GetMaxQual(loot);
              break;
            case StrFilters.MinLevel:
              GetMinLevel(loot);
              break;
            case StrFilters.MaxLevel:
              GetMaxLevel(loot);
              break;
            case StrFilters.Items:
              GetItems(loot);
              break;
            default:
              Log.Out($"{Config.ModPrefix} Unknown filter {f}");
              break;
          }
        }
      }
      else
      {
        GetName(loot);
        GetTemplate(loot);
        GetMinCount(loot);
        GetMaxCount(loot);
        GetMinQual(loot);
        GetMaxQual(loot);
        GetMinLevel(loot);
        GetMaxLevel(loot);
        GetItems(loot);
      }

    }

    private void GetItems(LootContainer.LootGroup loot)
    {
      foreach (var lootEntry in loot.items)
      {
        Items.Add(new BCMLootEntry(lootEntry));
      }
      Bin.Add("Items", Items);
    }

    private void GetMaxLevel(LootContainer.LootGroup loot)
    {
      Bin.Add("MaxLevel", MaxLevel = Math.Round(loot.maxLevel, 6));
    }

    private void GetMinLevel(LootContainer.LootGroup loot)
    {
      Bin.Add("MinLevel", MinLevel = Math.Round(loot.minLevel, 6));
    }

    private void GetMaxCount(LootContainer.LootGroup loot)
    {
      Bin.Add("MaxCount", MaxCount = loot.maxCount);
    }

    private void GetMinCount(LootContainer.LootGroup loot)
    {
      Bin.Add("MinCount", MinCount = loot.minCount);
    }

    private void GetTemplate(LootContainer.LootGroup loot)
    {
      Bin.Add("Template", Template = loot.lootQualityTemplate);
    }

    private void GetMaxQual(LootContainer.LootGroup loot)
    {
      Bin.Add("MaxQual", MaxQual = loot.maxQuality);
    }

    private void GetMinQual(LootContainer.LootGroup loot)
    {
      Bin.Add("MinQual", MinQual = loot.minQuality);
    }

    private void GetName(LootContainer.LootGroup loot)
    {
      Bin.Add("Name", Name = loot.name);
    }
  }
}
