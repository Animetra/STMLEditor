using System;

namespace STML.Model
{
    public class STMLScript : STMLSection
    {
        private int _head = 0;
        private int Head
        {
            get => _head;
            set { _head = Math.Clamp(value, 0, Children.Count); }
        }

        public STMLScript(STMLDocument parent) : base(parent)
        {
            Header = new STMLHeader("New Script");
        }

        public override STMLElement AddChild()
        {
            STMLExpression child = new STMLExpression(this);
            Children.Add(child);

            return child;
        }

        public void SetReadingHead(int index)
        {
            Head = index;
        }

        public STMLExpression Proceed()
        {
            STMLExpression currentExpression = GetExpression(Head);
            Head++;
            return currentExpression;
        }

        public STMLExpression Revert()
        {
            Head--;
            return Proceed();
        }

        public STMLExpression GetExpression(int index)
        {
            return (STMLExpression)Children[index];
        }
    }
}
