using System;
using System.Collections.Generic;
using System.Text;

namespace SASMobileApp1.Models
{
  public  class ObservationDTO
    {
        public int ObsId { get; set; }
        public int PatientId { get; set; }
        public DateTime ObsTime { get; set; }
        public string ObsLocation { get; set; }
        public string Status { get; set; }
        public string SeenBy { get; set; }
        public string Notes { get; set; }
    }
}
