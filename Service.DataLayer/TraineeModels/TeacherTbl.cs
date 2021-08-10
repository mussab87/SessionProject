using System;
using System.Collections.Generic;

#nullable disable

namespace Service.DataLayer.TraineeModels
{
    public partial class TeacherTbl
    {
        public TeacherTbl()
        {
            TeacherCourseTbls = new HashSet<TeacherCourseTbl>();
        }

        public int TeacherId { get; set; }
        public string TeacherName { get; set; }
        public string TeacherNationalId { get; set; }
        public string TeacherPhoneNumber { get; set; }
        public string CreatedBy { get; set; }
        public DateTime? CreateStamp { get; set; }
        public DateTime? UpdateStamp { get; set; }

        public virtual ICollection<TeacherCourseTbl> TeacherCourseTbls { get; set; }
    }
}
