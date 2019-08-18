using System.Collections.Generic;
using UnityEngine;
using Coordination.Query;
using System;
using System.Linq;

namespace SkillSystem
{
    public class Skill : ScriptableObject
    {
        [Serializable]
        public struct AffactationGroup
        {
            public CoordinationQuery query;
            public Affactation affactation;
        }

        public AffactationGroup[] groups;

        public void Operate(BoardCoord coordination)
        {
            var apq = new AffactationPriorityQueue();

            foreach (var group in groups)
            {
                if (!group.query.Exist(coordination))
                {
                    continue;
                }

                group.affactation.Operate(apq);
            }
        }


        public List<BoardCoord> GetAvailableTargetCoord()
        {
            List<BoardCoord> result = new List<BoardCoord>();

            foreach(var group in groups)
            {
                result = result.Concat(group.
                                       query.
                                       GetQueriedList().
                                       positions.
                                       Cast<BoardCoord>()).ToList();
            }

            return result;
        }
    }
}