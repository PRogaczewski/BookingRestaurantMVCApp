using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RestaurantLibrary.Services
{
    public interface ITimeService
    {
        DateTime FullDate(DateTime date, DateTime time);
    }
    public class TimeService : ITimeService
    {
        public DateTime FullDate(DateTime date, DateTime time)
        {
            var d = date.Date;
            var t = time.TimeOfDay;

            var fullDate = d.Add(t);

            return fullDate;
        }
    }
}
