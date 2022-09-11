using System;

namespace Artemis.Plugins.Games.TruckSimulator.DataModels
{
    /// <summary>
    /// A data model that wraps a date time and simplifies what properties are available to the user.
    /// </summary>
    public class DateTimeModel
    {
        private readonly Func<DateTime> accessor;

        public DateTimeModel(Func<DateTime> accessor)
        {
            this.accessor = accessor;
        }

        public int Year => accessor().Year;
        public int Month => accessor().Month;
        public int Day => accessor().Day;
        public DayOfWeek DayOfWeek => accessor().DayOfWeek;

        public int Hour => accessor().Hour;
        public int Minute => accessor().Minute;
        // Do not need to output second since the data from the game only counts minutes so would always be zero.
    }
}
