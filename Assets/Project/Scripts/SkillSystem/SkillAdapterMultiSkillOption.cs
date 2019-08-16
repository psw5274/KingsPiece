using UnityEngine;

[CreateAssetMenu(fileName = "New Skill With Multi-Skill Option", menuName = "Skill Adapter/Multi-Skill Option", order = 0)]
public class SkillAdapterMultiSkillOption : Skill
{
    [SerializeField]
    private Skill[] additionalSkills;
    public override void Operate(BoardCoord selectedBoardCoord)
    {
        base.Operate(selectedBoardCoord);
        foreach (Skill skill in additionalSkills)
        {
            skill.Operate(selectedBoardCoord);
        }
    }
}