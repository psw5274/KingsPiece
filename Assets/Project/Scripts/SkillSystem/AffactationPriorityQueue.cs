using System.Collections.Generic;

namespace SkillSystem
{
    public class AffactationPriorityQueue
    {
        public Piece target;
        public System.Action postprocess;
        private List<AffactationModule> queue;

        public AffactationPriorityQueue(Piece target)
        {
            this.target = target;
            queue = new List<AffactationModule>();
        }

        public void Push(AffactationModule module)
        {
            queue.Add(module);
        }

        private void Preprocess()
        {
            queue.Sort((lhs, rhs) => lhs.priority.CompareTo(rhs.priority));
        }

        public void Process()
        {
            Preprocess();
            foreach (var element in queue)
            {
                element.Apply(target, this);
            }
            postprocess();
        }
    }
}