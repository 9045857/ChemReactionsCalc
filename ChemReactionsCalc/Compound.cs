using System;

namespace ChemReactionsCalc
{
    internal class Compound
    {
        public Ion? Cation { get; private set; }
        public int CationCount { get; private set; }

        public Ion? Anion { get; private set; }
        public int AnionCount { get; private set; }

        public bool IsCationAndAnionExist { get; private set; } = false;

        public string? Solubility { get; private set; }

        public void AddIon(Ion ion, bool isCation)
        {
            if (isCation)
            {
                Cation = ion;

                if (Anion != null)
                {
                    SetSolubilityAndCoefficients(Cation, Anion);
                }
            }
            else
            {
                Anion = ion;

                if (Cation != null)
                {
                    SetSolubilityAndCoefficients(Cation, Anion);
                }
            }
        }

        private void SetSolubilityAndCoefficients(Ion cation, Ion anion)
        {
            IsCationAndAnionExist = true;
            CountAnionCationCoefficients(cation, anion);
            Solubility = SolubilityTable.GetCompoundSolubility(cation, anion);
        }

        public void AddAllIons(Ion cation, Ion anion)
        {
            Cation = cation;
            Anion = anion;

            SetSolubilityAndCoefficients(cation, anion);
        }

        public string Show(bool withDecomposition)
        {
           //Проверяем на null Катион и Анион
            if (Cation == null && Anion == null)
            {
                return "";
            }

            if (Cation == null && Anion != null)
            {
                return Anion.Formula;
            }

            if (Anion == null && Cation != null)
            {
                return Cation.Formula;
            }


            // VS не понимает, что Катион и Анион проверены на null, поэтому вставляем основной код метода в отдельный блок, где они точно не null
            if (Cation != null && Anion != null)
            {
                CountAnionCationCoefficients(Cation, Anion);


                string cationPart;
                if (CationCount == 1)
                {
                    cationPart = Cation.Formula;
                }
                else
                {
                    cationPart = Cation.AreRequiredBrackets ? "(" + Cation.Formula + ")" + CationCount.ToString() :
                        Cation.Formula + CationCount.ToString();
                }

                string anionPart;
                if (AnionCount == 1)
                {
                    anionPart = Anion.Formula;
                }
                else
                {
                    anionPart = Anion.AreRequiredBrackets ? "(" + Anion.Formula + ")" + AnionCount.ToString() :
                        Anion.Formula + AnionCount.ToString();
                }

                string currentFormula = cationPart + anionPart;

                return GetRightCompoundFormula(currentFormula, withDecomposition);
            }
            return "";
        }

        /// <summary>
        /// Метод корректирует некоторые формулы:
        /// HOH = H2O
        /// газы:
        /// H2CO3 = H2O + CO2
        /// H2SO3 = H2O + SO2
        /// NH4OH = NH3 + H2O
        /// H2S - ?кислота, становится газом при нагревании. 
        /// </summary>
        /// <param name="compound"></param>
        /// <returns></returns>
        private static string GetRightCompoundFormula(string formula, bool withDecomposition)
        {
            if (formula == "HOH")
            {
                return "H2O";
            }

            if (!withDecomposition)
            {
                return formula;
            }

            switch (formula)
            {
                case "H2CO3":
                    return "(H2O+CO2)";
                case "H2SO3":
                    return "(H2O+SO2)";
                case "NH4OH":
                    return "(NH3+H2O)";
                default:
                    break;
            }

            return formula;
        }


        private void CountAnionCationCoefficients(Ion cation, Ion anion)
        {
            if (!IsCationAndAnionExist) { return; }

            int anionNegativeChargeValue = Math.Abs(anion.IonizationDegree);
            int cationPositiveChargeValue = Math.Abs(cation.IonizationDegree);


            if (anionNegativeChargeValue == cationPositiveChargeValue)
            {
                CationCount = 1;
                AnionCount = 1;
            }
            else if (anionNegativeChargeValue % cationPositiveChargeValue == 0)
            {
                CationCount = (anionNegativeChargeValue / cationPositiveChargeValue);
                AnionCount = 1;

            }
            else if (cationPositiveChargeValue % anionNegativeChargeValue == 0)
            {
                CationCount = 1;
                AnionCount = (cationPositiveChargeValue / anionNegativeChargeValue);

            }
            else
            {
                CationCount = anionNegativeChargeValue;
                AnionCount = cationPositiveChargeValue;
            }
        }
    }
}
