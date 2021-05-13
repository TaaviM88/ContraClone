using UnityEngine;
using System;
using System.Collections;
using System.Collections.Generic;

namespace Stats
{
    public class PlayerStats
    {
        public float health = 1.0f;
        public int strength = 6;
        public int wisdom = 6;
        public List<Skills.Skill> skills;
        public PlayerStats()
        {
            skills = new List<Skills.Skill>();
        }

    }
}

namespace Skills
{
    public enum Skill
    {
        Fireball,
        IceStorm,
        Heal,
        Curse,
        Bless
    }

    public static class Strings
    {
        public static Dictionary<Skill, string> SkillNames = new Dictionary<Skill, string>()
        {
            { Skill.Fireball, "Fireball" },
            { Skill.IceStorm, "Ice Storm" },
            { Skill.Heal,     "Heal" },
            { Skill.Curse,    "Curse" },
            { Skill.Bless,    "Bless" }
        };
    }

    public static class Actions
    {
        public static Dictionary<Skill, Action<Stats.PlayerStats, Player>> SkillActions = new Dictionary<Skill, Action<Stats.PlayerStats, Player>>()
        {
            { Skill.Fireball, (PlayerSkillsSet,Player) => { PlayerSkillsSet.health -= 0.1f; } },
            { Skill.IceStorm, (PlayerSkillsSet,Player) => { PlayerSkillsSet.health -= 0.15f; } },
            { Skill.Heal,     (PlayerSkillsSet,Player) => { PlayerSkillsSet.health += 0.20f; } },
            { Skill.Curse,    (PlayerSkillsSet,Player) => { PlayerSkillsSet.wisdom -= 2; } },
            { Skill.Bless,    (PlayerSkillsSet,Player) => { PlayerSkillsSet.strength += 2; } }
        };
    }
}


public class PlayerSkillsSet : MonoBehaviour
{
    public bool debugMessages = true;
    private Stats.PlayerStats _playerStats;
    Player _player;

public void Awake()
{
    _playerStats = new Stats.PlayerStats();
    _player = GetComponentInParent<Player>();
}
    public void AddSkillToPlayer(Skills.Skill skill)
    {
        _playerStats.skills.Add(skill);
    }

    public void UseSkillOnPlayer(Skills.Skill skill)
    {
        if (Skills.Actions.SkillActions.ContainsKey(skill))
        {
            Skills.Actions.SkillActions[skill](_playerStats,_player);

            if (debugMessages)
            {
                Debug.Log("Used " + Skills.Strings.SkillNames[skill]);
            }
        }
    }
}