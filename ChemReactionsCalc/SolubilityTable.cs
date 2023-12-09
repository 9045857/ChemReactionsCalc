﻿using System.Security.Cryptography;

namespace ChemReactionsCalc
{
    /// <summary>
    /// Класс исходных данных по Анионам, Катионам и таблице растворимости.
    ///  
    /// Публичные методы:
    /// - GetCations() - получить массив катионов
    /// - GetAnions() - получить массив анионов
    /// - GetCompoundSolubility(Ion cation, Ion anion) - получить возможность растворимости для соединения катиона и аниона по таблице
    /// 
    /// Содержит методы для работы с исключениями:
    /// - 
    /// </summary>
    internal static class SolubilityTable
    {
        /// <summary>
        /// Массив катионов - положительно заряженных ионов.
        /// Структура массива - 4 элемента описывают катион:
        /// - написание
        /// - количество заряда
        /// - нужны ли скобки при коэффициенте, например, (NH4)2
        /// - индекс - соотвествует индексу в методе создающим массив Ion-ов, и индексу в таблице растворимости
        /// </summary>
        static string[] _cations = { "H", "1", "n", "0", "Li", "1", "n", "1", "K", "1", "n", "2", "Na", "1", "n", "3", "NH4", "1", "y", "4",
            "Mg", "2", "n", "5", "Ca", "2", "n", "6", "Ba", "2", "n", "7", "Sr", "2", "n", "8", "Al", "3", "n", "9", "Cr", "3", "n", "10",
            "Fe", "2", "n", "11", "Fe", "3", "n", "12", "Zn", "2", "n", "13", "Ag", "1", "n", "14", "Pb", "2", "n", "15", "Cu", "2", "n", "16",
            "Hg", "1", "n", "17", "Hg", "2", "n", "18", "Mn", "2", "n", "19", "Sn", "2", "n", "20", "Ni", "2", "n", "21", "Co", "2", "n", "22" };

        /// <summary>
        /// Массив анионов - отрицательно заряженных ионов.
        /// Структура массива - 4 элемента описывают анион:
        /// - написание
        /// - количество заряда
        /// - нужны ли скобки при коэффициенте, например, (CO3)2
        /// - индекс - соотвествует индексу в методе создающим массив Ion-ов, и индексу в таблице растворимости
        /// </summary>
        static string[] _anions = {"OH","1","y","0","F","1","n","1","Cl","1","n","2","Br","1","n","3","I","1","n","4","S","2","n","5",
            "SO3","2","y","6","SO4","2","y","7","PO4","3","y","8","CO3","2","y","9","SiO3","2","y","10","NO3","1","y","11", /*"AcO","1","y","12",*/
            "CrO4","2","y",/*"13"*/"12","ClO4","1","y",/*"14"*/"13" };

        /// <summary>
        /// Двумерный массив растворимости ионных соединений. 
        /// Индексам соответствующих анионов и катионов соответствует значение растворимости их соединения.
        /// р - растворимый
        /// Н - не растворимый
        /// М - малорастворимый
        /// Г – вещество подвергается сильному гидролизу,  
        /// –  – вещество не получено
        /// "" - вода, обычно в таблицах значение - Р -раствор
        /// РВГ - Раствор&Вода&Газ    YHG- yes&water&gas, соединения, которые, после ионного обмена, являются Растворимыми, но сразу распадается на газ и воду
        /// ГТ - Газ&Температура    YGT-yes&gas&temperature, соединение, которе под воздействием температуры привращается в газ
        /// </summary>
        static string[,] _solubilityTable = {
            {"В","Р","Р","Р","РВГ","Н","М","Р","М","Н","Н","Н","Н","Н","–","М","Н","–","–","Н","Н","М","Н"},
            {"Р","М","Р","Р","Р","М","Н","М","М","М","Р","М","М","Р","Р","М","Р","Г","Г","Р","Р","Р","Р"},
            {"Р","Р","Р","Р","Р","Р","Р","Р","Р","Р","М","Р","Р","Р","Н","М","Р","Н","Р","Р","Г","Р","Р"},
            {"Р","Р","Р","Р","Р","Р","Р","Р","Р","Р","Р","Р","Р","Р","Н","М","Р","Н","М","Р","Г","Р","Р"},
            {"Р","Р","Р","Р","Р","Р","Р","Р","Р","Р","Р","Р","–","Р","Н","М","–","Н","М","Р","М","Р","Р"},
            {"ГТ","Р","Р","Р","Р","Г","Г","Р","Н","Г","Г","Н","Г","Н","Н","Н","Н","–","Н","М","Н","Н","М"},
            {"РВГ","Р","Р","Р","Р","Н","Н","Н","Н","–","–","Г","–","Н","Н","Н","–","–","Н","Н","–","Н","Н"},
            {"Р","Р","Р","Р","Р","Р","М","Н","М","Р","Р","Р","Р","Р","М","М","Р","М","Г","Р","Г","Р","Р"},
            {"Р","М","Р","Р","Г","Н","Н","Н","Н","Н","Н","Н","Н","Н","Н","Н","М","Н","Н","Н","Н","Н","Н"},
            {"РВГ","Р","Р","Р","Р","М","Н","Н","М","–","–","Н","–","Н","М","Н","Г","Н","–","Н","–","Н","Н"},
            {"Н","Н","Р","Р","–","Г","Н","Н","Н","Г","Г","Г","Г","Г","–","Г","Г","–","–","Г","Г","Г","Г"},
            {"Р","Р","Р","Р","Р","Р","Р","Р","Р","Р","Р","Р","Р","Р","Р","Р","Р","Г","Р","Р","Р","Р","Р"},
            /*{"Р","Р","Р","Р","Р","Р","Р","Р","Р","Г","Р","Р","Г","Р","Р","Р","Р","М","Р","Р","Р","Р","Р"},*/
            {"Р","Р","Р","Р","Р","Р","М","Н","М","Г","Г","Г","Г","Н","Н","Н","Н","Н","Н","Н","Г","Н","Н"},
            {"Р","Р","Р","Р","Р","Р","Р","Р","Р","Р","Р","Р","Р","Р","Р","Р","Р","Р","Р","Р","Р","Р","Р"}

        };

        /// <summary>
        /// Приватный метод получения массивов Катионов или Анионов в типа Ion 
        /// </summary>
        /// <param name="ions"></param>
        /// <param name="isCation"></param>
        /// <returns></returns>
        private static Ion[] GetIons(string[] ions, bool isCation)
        {
            int itemsInIon = 4;
            int ionsCaunt = ions.Length / itemsInIon;

            Ion[] currentIons = new Ion[ionsCaunt];

            for (int i = 0; i < ionsCaunt; i++)
            {
                string formula = ions[i * itemsInIon];
                int ionizationDegree = Convert.ToInt16(ions[i * itemsInIon + 1]);
                bool areRequiredBrackets = ions[i * itemsInIon + 2] == "y" ? true : false;
                int index = Convert.ToInt16(ions[i * itemsInIon + 3]);

                Ion ion = new Ion(formula, ionizationDegree, areRequiredBrackets, index, isCation);

                currentIons[i] = ion;
            }

            return currentIons;
        }

        /// <summary>
        /// Получить массив катионов типа Ion 
        /// </summary>
        /// <returns></returns>
        public static Ion[] GetCations()
        {
            return GetIons(_cations, true);
        }

        /// <summary>
        /// Получить массив анионов типа Ion
        /// </summary>
        /// <returns></returns>
        public static Ion[] GetAnions()
        {
            return GetIons(_anions, false);
        }

        /// <summary>
        /// Получить значение растворимости ионного соединения катиона и аниона.
        /// р - растворимый
        /// Н - не растворимый
        /// М - малорастворимый
        /// Г – вещество подвергается сильному гидролизу,  
        /// –  – вещество не получено
        /// "" - вода, обычно в таблицах значение - Р -раствор
        /// РВГ - Раствор&Вода&Газ    YHG- yes&water&gas, соединения, которые, после ионного обмена, являются Растворимыми, но сразу распадается на газ и воду
        /// ГТ - Газ&Температура    YGT-yes&gas&temperature, соединение, которе под воздействием температуры привращается в газ
        /// </summary>
        /// <param name="cation"></param>
        /// <param name="anion"></param>
        /// <returns></returns>
        public static string GetCompoundSolubility(Ion cation, Ion anion)
        {
            string compoundSolubility = _solubilityTable[anion.Index, cation.Index];
            return compoundSolubility;
        }
    }
}