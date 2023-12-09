using Microsoft.VisualBasic;
using System.Drawing;
using System.Globalization;

namespace ChemReactionsCalc
{
    internal static class Message
    {
        public static string ReactionWithPrecipitation = "Реакция ионного обмена прошла успешно с выпадением в осадок {0}.";

        public static string ReactionWithGasRelease = "Реакция ионного обмена прошла успешно с выделением газа {0}.";

        public static string ReactionWithWaterFormation = "Реакция ионного обмена прошла успешно с образованием воды.";

        public static string NegativeResult = "Реакция ионного обмена прошла без выпадения осадка, образования воды или выделения газа.";

        public static string ImpossibleReaction_AnotherMechanism = "Реакция другого механизма, реакция ионного обмена невозможна.";


        // Сообщения для статутосов растворимости
        /// <summary>
        /// Р-растворим
        /// </summary>
        public static string Soluble = "Р – растворим";

        /// <summary>
        /// Свойство используется для указания соедниения {0} и что оно растворимо.
        /// </summary>
        public static string SolubleCompound = "{0} - растворим";

        /// <summary>
        /// Н - нерастворим
        /// </summary>
        public static string Insoluble = "Н - нерастворим";

        // public static string InsolubleCompound = "{0} - нерастворим";

        public static string Water = "Вода";

        public static string NotExistCompound = "не стабилен";

        public static string Hydrolysis = "подвержен гидролизу";

        /// <summary>
        /// Общая информация о Реакции ионного обмена, правило выбора исходных соедниений.
        /// </summary>
        public static string Manual = "Для запуска реакции ионного обмена (РИО), составьте начальные соединения,"
            + " выбрав катионы и анионы для каждого. "
            + Environment.NewLine
            + "Получившиеся соединения должны быть растворимы!"
            + Environment.NewLine
            + "Реакция пройдет успешно, если образуется осадок, газ или вода.";

        // сообщения программы при выборе соединений перед запуском реакции

        // в соединении выбран катион, нужно еще выбрать анион
        // в соединении выбрат анион нужно еще выбрать катион
        /// <summary>
        /// В соединении {0} отсуствует {1}, добавьте {1} из списка.
        /// {0} - либо номер слагаемого, либо написание соединения в настоящий момент
        /// {1} - катион или анион
        /// </summary>
        public static string NoOneIon = "В соединении {0} отсуствует {1}, добавьте {1} из списка.";

        // соединение не выбран ни катион ни анион.
        /// <summary>
        /// В соединении {0} отсуствует и катион и анион, добавьте из списков.
        /// {0} - номер слагаемого
        /// </summary>
        public static string NoAnyIon = "В соединении {0} отсуствует и катион и анион, добавьте из списков.";


        // соединение Вода. Воду нельзя выбирать
        /// <summary>
        /// В качестве cоединения выбрана Вода. Воду нельзя выбирать, и об этом сообщение.
        /// {0} - номер слагаемого, например: "1", "2" или "1 и 2"
        /// </summary>
        public static string SelectionErrorWater = "В качестве соединения {0} выбрана вода (H2O)."
            + Environment.NewLine
            + "Хотя реация ионного обмена проходит в воде,"
            + Environment.NewLine
            + "воду нельзя выбирать в качестве одного и соединений реакции.";


        // соединение Нерастворимно, Малорастворим, Г или -. Нужно данное выбрать другое, что бы было растворимо.
        /// <summary>
        /// Выбраны удовлетворяющие требованию "Растворимость" оба соединения.
        /// </summary>
        public static string Approval = "Отлично! Оба выбранных соединения растворимы."
            + Environment.NewLine
            + "Можно попробовать провести реакцию ионного обмена.";


        //Сообщения по результатам проведения реакции


        /// <summary>
        /// {0} соединение {1} растворимо и готово к реакции.
        /// </summary>
        public static string CompoundReady = "+ {0} соединение {1} растворимо и готово к реакции.";

        /// <summary>
        /// {0} соединение {1} нерастворимо. Замените его.
        /// </summary>
        public static string InsolubleCompound = "- {0} соединение {1} нерастворимо. Замените его.";

        /// <summary>
        /// {0} соединение {1} подвержено гидролизу. Замените его.
        /// </summary>
        public static string HydrolysisCompound = "- {0} соединение {1} подвержено гидролизу. Замените его.";

        /// <summary>
        /// {0} вещество {1} - вода. Замените воду на растворимое соединение.
        /// </summary>
        public static string WaterCompound = "- {0} вещество {1} - вода. Замените воду на растворимое соединение.";


        //Сообщения для блока проверки выбраны ли Катионы и Анионы для соединений.

        /// <summary>
        /// "Ошибка. Выберите катион и анион для {0} соединения."
        /// </summary>
        public static string ErrorCationAndAnionSelection = "- Выберите катион и анион для {0} соединения.";

        /// <summary>
        /// "Ошибка. Выберите катион для {0} соединения."
        /// </summary>
        public static string ErrorCationSelection = "- Выберите катион для {0} соединения.";

        /// <summary>
        /// "Ошибка. Выберите анион для {0} соединения."
        /// </summary>
        public static string ErrorAnionSelection = "- Выберите анион для {0} соединения.";


        /// <summary>
        /// "Первое"
        /// </summary>
        public static string First = "Первое";

        /// <summary>
        /// "Второе"
        /// </summary>
        public static string Second = "Второе";

        /// <summary>
        /// "первого"
        /// </summary>
        public static string FirstWho = "первого";

        /// <summary>
        /// "второго"
        /// </summary>
        public static string SecondWho = "второго";

    }
}
