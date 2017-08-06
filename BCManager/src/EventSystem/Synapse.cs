﻿using BCM.Neurons;
using System;
using System.Collections.Generic;

namespace BCM
{
  public class Synapse
  {
    public string name;
    public bool IsEnabled;
    public int beats;
    public int lastfired;
    public string options;
    public List<NeuronAbstract> neurons = new List<NeuronAbstract>();

    public Synapse()
    {
    }
    public void WireNeurons()
    {
      // todo: change to use the config files to define which neurons should be wired for a synapse
      //todo: add a check for init before world awake, if set to false, then delay wiring until world has loaded
      switch (name)
      {
        case "spawnmanager":
          neurons.Add(new SpawnManager());
          break;
        case "entityspawnmutator":
          neurons.Add(new EntitySpawnMutator(this));
          break;

        case "bagmonitor":
          neurons.Add(new BagMonitor());
          break;
        case "buffmonitor":
          neurons.Add(new BuffMonitor());
          break;
        case "deadisdead":
          neurons.Add(new DeadIsDead());
          break;
        case "deathwatch":
          neurons.Add(new DeathWatch());
          break;
        case "equipmentmonitor":
          neurons.Add(new EquipmentMonitor());
          break;
        case "positiontracker":
          neurons.Add(new PositionTracker());
          break;
        case "questmonitor":
          neurons.Add(new QuestMonitor());
          break;
        case "toolbeltmonitor":
          neurons.Add(new ToolbeltMonitor());
          break;

        case "pingkicker":
          neurons.Add(new PingKicker());
          break;

        case "mapexplorer":
          neurons.Add(new MapExplorer());
          break;
        case "saveworld":
          neurons.Add(new SaveWorld());
          break;
        case "trashcollector":
          neurons.Add(new TrashCollector());
          break;

        case "broadcastapi":
          neurons.Add(new BroadcastAPI());
          break;

        case "motd":
          neurons.Add(new Motd());
          break;

        default:
          Log.Out(Config.ModPrefix + " Unknown Synapse " + name);
          break;
      }
    }

    public bool FireNeurons(int b)
    {
      if ((b >= lastfired + beats) && IsEnabled)
      {
        lastfired = b;
        foreach (NeuronAbstract n in neurons)
        {
          try
          {
            n.Fire(b);
          } catch (Exception e)
          {
            Log.Out(Config.ModPrefix + " WARNING: Brain Damage detected trying to fire Neuron: " + n.GetType() + e);
          }
        }
      }
      return true;
    }
  }
}
