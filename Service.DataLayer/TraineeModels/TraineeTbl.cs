using System;
using System.Collections.Generic;

#nullable disable

namespace Service.DataLayer.TraineeModels
{
    public partial class TraineeTbl
    {
        public TraineeTbl()
        {
            TraineeCourseTables = new HashSet<TraineeCourseTable>();
        }

        public int TraineeId { get; set; }
        public string TraineeName { get; set; }
        public string TraineeNationalId { get; set; }
        public string PhoneNumber { get; set; }
        public string CreatedBy { get; set; }
        public DateTime? CreateStamp { get; set; }
        public DateTime? UpdateStamp { get; set; }

        public virtual ICollection<TraineeCourseTable> TraineeCourseTables { get; set; }
    }
}
