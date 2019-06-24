using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum SkillType { Attack, Buff, Debuff, Mixed, Etc }

public abstract class Skill : ScriptableObject
{
    public string skillName;
    public SkillType skillType;

    public int[] param;

    public abstract List<BoardCoord> GetAvailableTargetCoord();

    public abstract void SkillScript(params BoardCoord[] targetCoords); // 네이밍 체크

}