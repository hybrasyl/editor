/*
 * This file is part of Project Hybrasyl.
 *
 * This program is free software; you can redistribute it and/or modify
 * it under the terms of the Affero General Public License as published by
 * the Free Software Foundation, version 3.
 *
 * This program is distributed in the hope that it will be useful, but
 * without ANY WARRANTY; without even the implied warranty of MERCHANTABILITY
 * or FITNESS FOR A PARTICULAR PURPOSE. See the Affero General Public License
 * for more details.
 *
 * You should have received a copy of the Affero General Public License along
 * with this program. If not, see <http://www.gnu.org/licenses/>.
 *
 * (C) 2015 Kyle Speck (kojasou@hybrasyl.com)
 *
 * Authors:   Kyle Speck    <kojasou@hybrasyl.com>
 *
 */

using System.Collections.Generic;

namespace HybrasylEditor
{
    public class Skill
    {
        public string Name { get; set; }
        public string Type { get; set; }
        public int Icon { get; set; }
        public string BookType { get; set; }
        public SkillAnimation Animation { get; set; }
        public int Sound { get; set; }
        public int Cooldown { get; set; }
        public SkillStatus Status { get; set; }
        public SkillCastCost CastCost { get; set; }
        public string Element { get; set; }
        public SkillIntent Intent { get; set; }
        public string WeaponType { get; set; }
        public List<string> PrimaryClasses { get; set; }
        public SkillMaxSkillLevel MaxSkillLevel { get; set; }
        public int Lines { get; set; }
        public SkillRequirements Requirements { get; set; }

        public Skill()
        {
            Name = string.Empty;
            Type = "skill";
            Icon = 0;
            BookType = "primary_skills";
            Animation = new SkillAnimation();
            Status = new SkillStatus();
            CastCost = new SkillCastCost();
            Element = "none";
            Intent = new SkillIntent();
            WeaponType = "none";
            PrimaryClasses = new List<string>();
            MaxSkillLevel = new SkillMaxSkillLevel();
            Requirements = new SkillRequirements();
        }
    }

    public class SkillAnimation
    {
        public SkillAnimationEffect Effect { get; set; }
        public List<SkillAnimationMotion> Motions { get; set; }

        public SkillAnimation()
        {
            Effect = new SkillAnimationEffect();
            Motions = new List<SkillAnimationMotion>();
        }
    }

    public class SkillAnimationEffect
    {
        public int SourceEffect { get; set; }
        public int SourceSpeed { get; set; }
        public int Speed { get; set; }
    }

    public class SkillAnimationMotion
    {
        public string Class { get; set; }
        public int Motion { get; set; }
        public int Speed { get; set; }
    }

    public class SkillStatus
    {
        public string Add { get; set; }
        public string Remove { get; set; }

        public SkillStatus()
        {
            Add = string.Empty;
            Remove = string.Empty;
        }
    }

    public class SkillCastCost
    {
        public int Hp { get; set; }
        public int HpPercentage { get; set; }
        public int Mp { get; set; }
        public int MpPercentage { get; set; }
        public int Gold { get; set; }
        public List<SkillCastCostItem> Items { get; set; }

        public SkillCastCost()
        {
            Items = new List<SkillCastCostItem>();
        }
    }

    public class SkillCastCostItem
    {
        public string Name { get; set; }
        public int Quantity { get; set; }
    }

    public class SkillIntent
    {
        public string Target { get; set; }
        public string Type { get; set; }
    }

    public class SkillMaxSkillLevel
    {
        public int Primary { get; set; }
        public int Secondary { get; set; }
        public int Warrior { get; set; }
        public int Rogue { get; set; }
        public int Wizard { get; set; }
        public int Priest { get; set; }
        public int Monk { get; set; }
    }

    public class SkillRequirements
    {
        public int MinLevel { get; set; }
        public int MinAbility { get; set; }
        public int MinLevelDisplay { get; set; }
        public int Gold { get; set; }
        public List<SkillRequirementsItem> Items { get; set; }
        public SkillRequirementsStats Stats { get; set; }

        public SkillRequirements()
        {
            Items = new List<SkillRequirementsItem>();
            Stats = new SkillRequirementsStats();
        }
    }

    public class SkillRequirementsItem
    {
        public string Name { get; set; }
        public int Quantity { get; set; }
    }

    public class SkillRequirementsStats
    {
        public int Str { get; set; }
        public int Int { get; set; }
        public int Wis { get; set; }
        public int Con { get; set; }
        public int Dex { get; set; }
    }
}
