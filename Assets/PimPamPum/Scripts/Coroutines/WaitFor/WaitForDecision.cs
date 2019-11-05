﻿namespace PimPamPum
{
    public class WaitForDecision : WaitFor
    {
        private const Decision startDecision = Decision.Pending;
        private Decision timeOutDecision;
        private bool decisionMade;

        public Decision Decision
        {
            get; private set;
        }

        public override bool MoveNext()
        {
            bool res = base.MoveNext() && !decisionMade;
            if (!res && !decisionMade)
            {
                MakeDecisionCard(timeOutDecision);
            }
            return res;
        }

        public WaitForDecision() : this(Decision.Pending) { }

        public WaitForDecision(Decision timeOutDecision) : base()
        {
            this.timeOutDecision = timeOutDecision;
            Decision = startDecision;
        }

        public override void MakeDecisionCard(Decision decision, Card card = null)
        {
            Decision = decision;
            decisionMade = decision != startDecision;
        }
    }
}