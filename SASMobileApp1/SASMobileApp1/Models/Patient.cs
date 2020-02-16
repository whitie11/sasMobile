using System;
using System.Collections.Generic;
using System.Text;

namespace SASMobileApp1.Models
{
    public class Patient
    {
        public Patient()
        {
        }

        public int PatientId { get; set; }

        public string FirstName
        {
            get;
            set;
        }
        public string MidName
        {
            get;
            set;
        }
        public string LastName
        {
            get;
            set;
        }
        public string NHSno
        {
            get;
            set;
        }
        public DateTime? Birthdate
        {
            get;
            set;
        }
        public DateTime? LastSeen
        {
            get;
            set;
        }
        public LeaveReg Leave
        {
            get;
            set;
        }
        public string Section
        {
            get;
            set;
        }
    }



    public class PatientListItem : Patient
    {
        public PatientListItem(Patient pt)
        {
            this.FirstName = pt.FirstName;
            this.LastName = pt.LastName;
            this.MidName = pt.MidName;
            this.NHSno = pt.NHSno;
            this.PatientId = pt.PatientId;
            this.Birthdate = pt.Birthdate;
            this.LastSeen = pt.LastSeen;
            this.Leave = pt.Leave;
            this.Section = pt.Section;
        }


        public string FullName
        {
            get
            {
                string mid = "";
                if (MidName != "")
                {
                    mid = MidName + " ";
                }
                return FirstName + " " + mid + LastName;
            }
        }

        public bool IsSelected { get; set; }


        public string ObsTimeColour
        {
            get;
            set;
        }

        public bool ShowObsButton
        {
            get
            {
                if (this.IsSelected)
                {

                    if (this.Leave.IsCurrent)
                    {
                        return false;
                    }
                    return true;
                }
                return false;
            }
        }


        public bool ShowLeaveButton
        {
            get
            {
                if (this.IsSelected)
                {

                    if (this.Leave.IsCurrent)
                    {
                        return false;
                    }
                    return true;
                }
                return false;
            }
        }

        public bool ShowLeaveEditButton
        {
            get
            {
                if (this.IsSelected)
                {

                    if (this.Leave.IsCurrent)
                    {
                        return true;
                    }
                    return false;
                }
                return false;
            }
        }

    }
}
