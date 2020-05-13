namespace MPT.SymbolicMath
{
    public struct UnitOperatorPair
    {
        public IBase Unit;
        public string Operator;

        public UnitOperatorPair(IBase unit, string unitOperator)
        {
            Unit = (IBase)unit.Clone();
            Operator = unitOperator;
        }


        public UnitOperatorPair(IBase unit, char unitOperator)
        {
            Unit = unit;
            Operator = operatorValidatedForEmpty(unitOperator);
        }

        public UnitOperatorPair UpdateUnit(IBase unit)
        {
            Unit = unit;
            return this;
        }

        public UnitOperatorPair UpdateOperator(char unitOperator)
        {
            Operator = unitOperator.ToString();
            return this;
        }

        public UnitOperatorPair UpdateOperator(string unitOperator)
        {
            Operator = unitOperator;
            return this;
        }

        public bool OperatorEquals(char unitOperator)
        {
            return (Operator == operatorValidatedForEmpty(unitOperator));
        }

        public bool OperatorEquals(string unitOperator)
        {
            return (Operator == unitOperator);
        }

        private static string operatorValidatedForEmpty(char unitOperator)
        {
            return (unitOperator == Query.EMPTY) ? string.Empty : unitOperator.ToString();
        }
    }
}
