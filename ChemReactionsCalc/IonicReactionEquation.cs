using System.Diagnostics.Metrics;

namespace ChemReactionsCalc
{
    internal static class IonicReactionEquation
    {
        private static Compound _compound1 = new();
        public static int Factor1 { get; private set; } = 1;

        private static Compound _compound2 = new();
        public static int Factor2 { get; private set; } = 1;

        private static readonly Compound _compound3 = new();
        public static int Factor3 { get; private set; } = 1;

        private static readonly Compound _compound4 = new();
        public static int Factor4 { get; private set; } = 1;



        public static bool IsEquationWorking(Compound compound1, Compound compound2, out string ionExchangeEquation, out string сomment)
        {
            if (compound1 == null || compound2 == null)
            {
                ionExchangeEquation = "";
                сomment = "Входное соединение null, возможно оба.";
                return false;
            }

            if (compound1.Cation == null || compound1.Anion == null || compound2.Cation == null || compound2.Anion == null)
            {
                ionExchangeEquation = "";
                сomment = "Какой-то из входных Катионов/Анионов null, возможно все.";
                return false;
            }

            _compound1 = compound1;
            _compound2 = compound2;

            _compound3.AddAllIons(compound1.Cation, compound2.Anion);
            _compound4.AddAllIons(compound2.Cation, compound1.Anion);

            SetCompoundsFactors();

            // запишем реакцию для демонстрации 
            ionExchangeEquation = GetEquation();
            
            //в одном из параметров сформируем комментарий, которые передадим выше.
            return IsResultingCompoundsCorrect(_compound3, _compound4, out сomment);
        }

        /// <summary>
        /// Метод проверяет получившиется после реакции соединения на корректность.
        /// В результате реакции должен появится осадок, вода или газ.
        /// false если оба компонента:
        /// Р - растворимы
        /// true если хотя бы одно:
        /// 1) Н - не растворимый
        /// 2) М - малорастворимый
        /// 3) В - вода, обычно в таблицах значение - Р -раствор
        /// 4) РВГ - растворим, при этом Раствор&Вода&Газ    YHG- yes&water&gas, соединения, которые, после ионного обмена, являются Растворимыми, 
        /// но сразу распадается на газ и воду. В комментарии к реакции данный факт отмечается.
        /// 5) ГТ - растворим, при этом становися Газом при нагревании. Данный статут только у H2S.
        /// в комментарии указывается, что реакция проходит при нагревании, только тогда H2S газообразен.
        ///
        // TODO: что делать в этими вечествами, нужно разобраться.
        /// Г – вещество подвергается сильному гидролизу,  
        /// – – при контакте с водой подвергается гидролизу (вещество не получено)

        /// </summary>
        /// <param name="compound3"></param>
        /// <param name="compound4"></param>
        /// <param name="comment"></param>
        /// <returns></returns>      
        private static bool IsResultingCompoundsCorrect(Compound compound3, Compound compound4, out string comment)
        {
            bool isResultingCompound3Correct = IsCompoundGasWaterSediment(compound3, out string commentCompound3Part);

            bool isResultingCompound4Correct = IsCompoundGasWaterSediment(compound4, out string commentCompound4Part);

            bool isResultCorrect = isResultingCompound3Correct || isResultingCompound4Correct;

            if (isResultCorrect)
            {
                if (isResultingCompound3Correct && isResultingCompound4Correct)
                {
                    comment = "Реакция прошла успешно:"
                            + Environment.NewLine
                            + commentCompound3Part
                            + Environment.NewLine
                            + commentCompound4Part;
                    return true;
                }
                else if (isResultingCompound3Correct && !isResultingCompound4Correct)
                {
                    comment = "Реакция прошла успешно:"
                            + Environment.NewLine
                            + commentCompound3Part;
                    return true;
                }
                else
                {
                    comment = "Реакция прошла успешно:"
                            + Environment.NewLine
                            + commentCompound4Part;
                    return true;
                }
            }
            else            
            {
                comment = string.Format("Реакция ионного обмена не состоялась."
                            + Environment.NewLine
                            + "Оба полученных соединения {0} и {1} растворимы.",compound3.Show(false), compound4.Show(false));
                return false;
            }
        }


        /// <summary>
        /// Метод проверяет получившиется после реакции одно соединение на корректность.
        /// В результате реакции должен появится осадок, вода или газ.
        /// false если он:
        /// Р - растворим
        /// true если:
        /// 1) Н - не растворимый
        /// 2) М - малорастворимый
        /// 3) В - вода, обычно в таблицах значение - Р -раствор
        /// 4) РВГ - растворим, при этом Раствор&Вода&Газ    YHG- yes&water&gas, соединения, которые, после ионного обмена, являются Растворимыми, 
        /// но сразу распадается на газ и воду. В комментарии к реакции данный факт отмечается.
        /// 5) ГТ - растворим, при этом становися Газом при нагревании. Данный статут только у H2S.
        /// в комментарии указывается, что реакция проходит при нагревании, только тогда H2S газообразен.
        ///
        // TODO: что делать в этими вечествами, нужно разобраться.
        /// Г – вещество подвергается сильному гидролизу,  
        /// – – при контакте с водой подвергается гидролизу (вещество не получено)
        /// </summary>
        /// <param name="compound"></param>
        /// <param name="comment"></param>
        /// <returns></returns>
        private static bool IsCompoundGasWaterSediment(Compound compound, out string comment)
        {
            if (compound == null)
            {
                comment = "Соединение null";
                return false;
            }

            if (compound.Solubility == null)
            {
                comment = "Растворимость соединения null";
                return false;
            }

            string compoundSolubility = compound.Solubility;

            switch (compoundSolubility)
            {
                case "Р":
                    comment = "";
                    return false;

                case "Н":
                    comment = string.Format("{0} осадок.", compound.Show(false));
                    return true;

                case "М":
                    comment = string.Format("{0} осадок.", compound.Show(false));
                    return true;

                case "В":
                    comment = string.Format("{0} вода.", compound.Show(false));
                    return true;

                case "РВГ":
                    comment = string.Format("{0} разложился на воду и газ.", compound.Show(false));
                    return true;

                case "ГТ":
                    comment = string.Format("{0} превратиться в газ при нагревании", compound.Show(false));
                    return true;

                case "Г":
                    comment = "?????????????";
                    return false;

                case "-":
                    comment = "?????????????-";
                    return false;

                default:
                    comment = string.Empty;
                    return false;
            }
        }

        /// <summary>
        /// Метод формирует запись уравнение ионного обмена.
        /// </summary>
        /// <returns></returns>
        private static string GetEquation()
        {
            string fact1 = Factor1 == 1 ? "" : Factor1.ToString();
            string fact2 = Factor2 == 1 ? "" : Factor2.ToString();
            string fact3 = Factor3 == 1 ? "" : Factor3.ToString();
            string fact4 = Factor4 == 1 ? "" : Factor4.ToString();

            return fact1 + _compound1.Show(false) + " + "
                + fact2 + _compound2.Show(false) + " = "
                + fact3 + _compound3.Show(true) + " + "
                + fact4 + _compound4.Show(true);
        }

        /// <summary>
        /// Метод проверяет подобранные соединения для начала реакции ионного обмена.
        /// Оба выбранных соединения должны быть растворимы.
        /// Варианты ответов:
        /// 
        /// </summary>
        /// <returns></returns>
        public static bool AreCompoundsReadyToReact(Compound compound1, Compound compound2, out string message)
        {
            if (compound1 == null || compound2 == null)
            {
                message = "Программная ошибка, как мининим одно из соединений null, а может и оба.";
                return false;
            }

            //Проверяем первое соединение
            //Проврка на выбор Катиона и Аниона

            bool isCompound1ReactionReady;
            string compound1Message;

            if (compound1.IsCationAndAnionExist)
            {
                isCompound1ReactionReady = CheckCompoundSolubility(1, compound1, out compound1Message);
            }
            //Проверка первого соединения на удовлетворение требованию на растворимость
            else
            {
                isCompound1ReactionReady = AreCationAndAnionSelected(1, compound1, out compound1Message);
            }

            //Проверяем второе соединение
            //Проврка на выбор Катиона и Аниона

            bool isCompound2ReactionReady;
            string compound2Message;

            if (compound2.IsCationAndAnionExist)
            {
                isCompound2ReactionReady = CheckCompoundSolubility(2, compound2, out compound2Message);
            }
            //Проверка второго соединения на удовлетворение требованию на растворимость
            else
            {
                isCompound2ReactionReady = AreCationAndAnionSelected(2, compound2, out compound2Message);
            }

            message = compound1Message + Environment.NewLine + compound2Message;

            return isCompound1ReactionReady && isCompound2ReactionReady;
        }

        private static bool AreCationAndAnionSelected(int compoundNumber, Compound compound, out string errorMessage)
        {
            string compoundNumberString;
            if (compoundNumber == 1)
            {
                compoundNumberString = Message.FirstWho;
            }
            else if (compoundNumber == 2)
            {
                compoundNumberString = Message.SecondWho;
            }
            else
            {
                compoundNumberString = "";
            }

            if (compound.Cation == null && compound.Anion == null)
            {
                errorMessage = string.Format(Message.ErrorCationAndAnionSelection, compoundNumberString);
                return false;
            }
            else if (compound.Cation == null && compound.Anion != null)
            {
                errorMessage = string.Format(Message.ErrorCationSelection, compoundNumberString);
                return false;
            }
            else if (compound.Cation != null && compound.Anion == null)
            {
                errorMessage = string.Format(Message.ErrorAnionSelection, compoundNumberString);
                return false;
            }

            errorMessage = "";
            return true;
        }


        /// <summary>
        /// Метод проверяет по таблице растворимость соединения, так же формирует сообщение об ошибке.
        /// Р - растворимый
        /// Н - не растворимый
        /// М - малорастворимый
        /// Г – вещество подвергается сильному гидролизу,  
        /// – – при контакте с водой подвергается гидролизу (вещество не получено)
        /// В - вода, обычно в таблицах значение - Р -раствор
        /// РВГ - растворим, при этом Раствор&Вода&Газ    YHG- yes&water&gas, соединения, которые, после ионного обмена, являются Растворимыми, но сразу распадается на газ и воду
        /// ГТ - растворим, при этом становися Газом при нагревании. Данный статут только у H2S.
        /// </summary>
        /// <param name="compoundNumber"></param>
        /// <param name="compound"></param>
        /// <param name="errorMessage"></param>
        /// <returns></returns>
        private static bool CheckCompoundSolubility(int compoundNumber, Compound compound, out string errorMessage)
        {
            string compoundNumberString;
            if (compoundNumber == 1)
            {
                compoundNumberString = Message.First;
            }
            else if (compoundNumber == 2)
            {
                compoundNumberString = Message.Second;
            }
            else
            {
                compoundNumberString = "";
            }

            //проверка на compound на null

            if (compound == null)
            {
                errorMessage = "Первое соединение null";
                return false;
            }

            if (compound.Solubility == null)
            {
                errorMessage = "Первое соединение не определяется по таблице растворимости - null";
                return false;
            }

            //Сопоставляем растворимость из таблицы с текстами сообщений, выдаем результат true|false

            string compoundSolubility = compound.Solubility;

            switch (compoundSolubility)
            {
                case "Р":
                    errorMessage = string.Format(Message.CompoundReady, compoundNumberString, compound.Show(false));
                    return true;

                case "РВГ":
                    errorMessage = string.Format(Message.CompoundReady, compoundNumberString, compound.Show(false));
                    return true;

                case "ГТ":
                    errorMessage = string.Format(Message.CompoundReady, compoundNumberString, compound.Show(false));
                    return true;

                case "Н":
                    errorMessage = string.Format(Message.InsolubleCompound, compoundNumberString, compound.Show(false));
                    return false;

                case "М":
                    errorMessage = string.Format(Message.InsolubleCompound, compoundNumberString, compound.Show(false));
                    return false;

                case "Г":
                    errorMessage = string.Format(Message.HydrolysisCompound, compoundNumberString, compound.Show(false));
                    return false;

                case "–":
                    errorMessage = string.Format(Message.HydrolysisCompound, compoundNumberString, compound.Show(false));
                    return false;

                case "В":
                    errorMessage = string.Format(Message.WaterCompound, compoundNumberString, compound.Show(false));
                    return false;

                default:
                    errorMessage = "Неизвестная ошибка. Бегите!";
                    return false;
            }
        }


        private static int SetFactor(int primaryСationCount, int primaryAnionCount, int finalCationCount, int finalAnionCount)
        {
            int factor = 1;

            if (primaryСationCount < finalCationCount)
            {
                factor = finalCationCount;
                return factor;
            }

            if (primaryAnionCount < finalAnionCount)
            {
                factor = finalAnionCount;
                return factor;
            }

            return factor;
        }

        private static void SetCompoundsFactors()
        {
            Factor1 = SetFactor(_compound1.CationCount, _compound1.AnionCount, _compound3.CationCount, _compound4.AnionCount);

            Factor2 = SetFactor(_compound2.CationCount, _compound2.AnionCount, _compound4.CationCount, _compound3.AnionCount);

            Factor3 = SetFactor(_compound3.CationCount, _compound3.AnionCount, _compound1.CationCount, _compound2.AnionCount);

            Factor4 = SetFactor(_compound4.CationCount, _compound4.AnionCount, _compound2.CationCount, _compound1.AnionCount);
        }

        /// <summary>
        ///Метод вызывается соответсвующее сообщение в Label 
        /// Р - растворимый
        /// Н - не растворимый
        /// М - малорастворимый
        /// Г – вещество подвергается сильному гидролизу,  
        /// – – при контакте с водой подвергается гидролизу (вещество не получено)
        /// В - вода, обычно в таблицах значение - Р -раствор
        /// РВГ - растворим, при этом Раствор&Вода&Газ    YHG- yes&water&gas, соединения, которые, после ионного обмена, являются Растворимыми, но сразу распадается на газ и воду
        /// ГТ - растворим, при этом становися Газом при нагревании. Данный статут только у H2S.
        /// </summary>
        /// <param name="label"></param>
        /// <param name="compound"></param>
        public static string? ShowCompaundSolubilityMessage(Compound compound)
        {
            string solubility = (compound != null &&
                compound.Cation != null &&
                compound.Anion != null &&
                compound.IsCationAndAnionExist) ?
                SolubilityTable.GetCompoundSolubility(compound.Cation, compound.Anion) :
                "";

            switch (solubility)
            {
                case "В":
                    return Message.Water;
                case "Р":
                    return Message.Soluble;
                case "Н":
                    return Message.Insoluble;
                case "М":
                    return Message.Insoluble;
                case "Г":
                    return Message.Hydrolysis;
                case "–":
                    return Message.Hydrolysis;
                case "РВГ":
                    return Message.Soluble;
                case "ГТ":
                    return Message.Soluble;
                default:
                    break;
            }

            return "";
        }
    }
}
