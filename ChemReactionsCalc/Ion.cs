using System.Numerics;

namespace ChemReactionsCalc
{
    internal class Ion
    {
        public string Formula { get; }//"H", 
        public int IonizationDegree { get; }// "1", 
        public bool AreRequiredBrackets { get; }// "n", 
        public int Index { get; }//"0"
        public bool IsCation { get; }//true|false

        public Ion(string formula, int ionizationDegree, bool areRequiredBrackets, int index, bool isCation)
        {
            Formula = formula;
            IonizationDegree = ionizationDegree;
            AreRequiredBrackets = areRequiredBrackets;
            Index = index;
            IsCation = isCation;
        }

        public string GetToString()
        {
            string ionizationDegree = IonizationDegree == 1 ? "" : IonizationDegree.ToString();
            string sign = IsCation ? "+" : "-";

            return Formula + " " + ionizationDegree + sign;
        }
    }
}
