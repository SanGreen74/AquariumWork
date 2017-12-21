using System;

namespace AquariumLibrary.BaseClasses
{
    public static class Randomizer
    {
        private static readonly Random _random = new Random();

        /// <summary>
        /// Возвращает true или false, результат зависит от процента удачи
        /// </summary>
        /// <param name="percent">Процент удачи от 0 до 100</param>
        /// <returns></returns>
        public static bool Success(double percent)
        {
            if (percent < 0 || percent > 100)
                throw new ArgumentException(nameof(percent));
            return Next(0, 100) <= percent;
        }

        public static int Next(int min, int max)
        {
            var next = _random.Next(min, max);
            return next;
        }
    }
}
