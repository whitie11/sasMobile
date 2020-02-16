using System;
namespace SASMobileApp1.Models
{
    public class LeaveReg
    {
        public int LeaveId { get; set; }
        public int PatientId { get; set; }
        public int LeaveTypeId { get; set; }
    public string Description { get; set; }
        public bool IsCurrent { get; set; }
        public DateTime TimeOut { get; set; }
        public DateTime TimeRetDue { get; set; }
        public DateTime? TimeRetActual { get; set; }
        public LeaveType LeaveType { get; set; }
    }

    public class LeaveType
    {
       public int LeaveTypeId { get; set; }
       public string Code { get; set; }
       public string Text { get; set; }
    }

    public class LeaveRegDTO
    {
        public int LeaveId { get; set; }
        public int PatientId { get; set; }
        public int LeaveTypeId { get; set; }
        public string Description { get; set; }
        public bool IsCurrent { get; set; }
        public DateTime TimeOut { get; set; }
        public DateTime TimeRetDue { get; set; }
        public DateTime? TimeRetActual { get; set; }
    }
}
