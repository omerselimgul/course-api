using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entities.Exceptions.WeekSchedules
{
    public sealed class WeekScheduleNotFoundExcepiton : NotFoundException
    {
        public WeekScheduleNotFoundExcepiton(int id)
            : base($"The week schedule with id: {id} could not found.")
        {
        }
    }
}
