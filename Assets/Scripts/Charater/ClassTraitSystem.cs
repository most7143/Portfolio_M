﻿using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class ClassTraitSystem : MonoBehaviour
{
    public ClassNames Name;
    public ClassNames PrevClass = ClassNames.None;

    public Dictionary<ClassTraitNames, int> Traits = new();

    public int MaxLevel = 25;

    private void OnEnable()
    {
        EventManager<EventTypes>.Register<int>(EventTypes.LevelUp, OpenPopup);
    }

    private void OnDisable()
    {
        EventManager<EventTypes>.Unregister<int>(EventTypes.LevelUp, OpenPopup);
    }

    private void Start()
    {
        List<ClassTraitNames> traitNames = Enum.GetValues(typeof(ClassTraitNames)).Cast<ClassTraitNames>().ToList();

        for (int i = 0; i < 5; i++)
        {
            int rand = UnityEngine.Random.Range(1, traitNames.Count);

            Traits.Add(traitNames[rand], 0);

            EventManager<EventTypes>.Send(EventTypes.AddTrait, traitNames[rand]);
            traitNames.RemoveAt(rand);
        }
    }

    public List<ClassTraitNames> GetNames()
    {
        return Traits.Keys.ToList();
    }

    public void Add(ClassTraitNames name, int count)
    {
        Traits[name] += count;
    }

    public void OpenPopup(int level)
    {
        if ((int)Name >= 100)
            return;

        UIPopupManager.Instance.Spawn(UIPopupNames.ClassTrait);
    }

    public void ChangeClass(ClassNames name)
    {
        if (Name == name)
            return;

        Player player = InGameManager.Instance.Player;
        ClassData data = ResourcesManager.Instance.LoadScriptable<ClassData>(name.ToString());

        if (data != null)
        {
            PrevClass = Name;
            Name = name;
            if (data.Stats.Count > 0)
            {
                for (int i = 0; i < data.Stats.Count; i++)
                {
                    player.StatSystem.AddStat(StatTID.Class1, data.Stats[i], data.Values[i]);
                }
            }

            if (data.Projectiles.Count > 0)
            {
                for (int i = 0; i < data.Projectiles.Count; i++)
                {
                    player.ProjectileSystem.Register(data.Projectiles[i]);
                }
            }

            if (data.BuffNames.Count > 0)
            {
                for (int i = 0; i < data.BuffNames.Count; i++)
                {
                    if (data.Targets[i] == CharacterTypes.Player)
                    {
                        player.BuffSystem.Register(data.BuffNames[i], data.BuffAliveTimes[i], data.BuffValues[i]);
                    }
                    else if (data.Targets[i] == CharacterTypes.Monster)
                    {
                        if (player.Target != null)
                        {
                            player.Target.BuffSystem.Register(data.BuffNames[i], data.BuffAliveTimes[i], data.BuffValues[i]);
                        }
                    }
                }
            }

            OutGameManager.Instance.CheckNonClassChange = false;
            OutGameManager.Instance.ClassChanges.Add(name);

            EventManager<EventTypes>.Send(EventTypes.ChangeClass, name);

            Clear();
        }
    }

    private void Clear()
    {
        ClassTraitNames[] values = Traits.Keys.ToArray();

        for (int i = 0; i < values.Length; i++)
        {
            Traits[values[i]] = 0;
        }
    }
}