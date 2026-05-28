using System;
using System.Collections.Generic;

namespace lab_1
{
    public delegate void ConversionHandler(double result);

    public class UnitCalculator : IConverter
    {
        public event ConversionHandler OnCalculationFinished;

        private readonly Dictionary<string, double> _ratios = new Dictionary<string, double>
        {
            { "Метры", 1.0 }, { "Футы", 0.3048 }, { "Дюймы", 0.0254 },
            { "Килограммы", 1.0 }, { "Фунты", 0.453592 },
            { "Литры", 1.0 }, { "Галлоны", 3.78541 }
        };

        public double Convert(double value, string fromUnit, string toUnit)
        {
            double inBase = value * _ratios[fromUnit];
            double result = inBase / _ratios[toUnit];

            OnCalculationFinished?.Invoke(result);

            return result;
        }
    }
}