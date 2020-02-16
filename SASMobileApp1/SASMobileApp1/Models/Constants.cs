using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;
using System.Threading.Tasks;
using SASMobileApp1.Interfaces;
using SASMobileApp1.Models;
using SASMobileApp1.Services;

namespace SASMobileApp1.Models
{
    static class Constants
    {
        //  public const string API_Address = "http://sasdatamanager1-env.bm8hz53wy9.eu-west-2.elasticbeanstalk.com/";
        public const string API_Address = "https://sasdatamanager.azurewebsites.net/";

        public static readonly IList<String> Locations = new ReadOnlyCollection<string>
    (new List<String> {
           "Bedroom",
           "Bathroom",
           "Communinal Area",
           "Activity Room",
           "Garden",
           "Off Ward",
           "On Leave"
         });

        public static readonly IList<String> Status = new ReadOnlyCollection<string>
       (new List<String> {
           "Sleeping",
           "Euthymic",
           "Anxious",
           "Agitated",
           "Aggressive",
           "Secluded",
           "On Leave"
       });


        //public static readonly IList<LeaveType> LeaveTypes = new List<LeaveType>
        //        {
        //            new LeaveType { Id = 1, Code="EB", Text = "Escorted Within Building" },
        //            new LeaveType { Id = 2, Code="EG", Text = "Escorted Within Grounds" },
        //            new LeaveType {Id =3, Code="UEB", Text="Unescorted Within Building"},
        //            new LeaveType {Id =4,  Code="UEG", Text="Unescorted Within Grounds"},
        //            new LeaveType {Id =5, Code="EC", Text="Escorted Community"},
        //            new LeaveType {Id =6, Code="UC", Text="Unescorted Community"},
        //            new LeaveType {Id =7, Code = "O/N", Text="Overnight Leave"}
        //         };


        static LeaveService leaveService = new LeaveService();

        public static IList<LeaveType> LeaveTypes;

        static Constants()
        {
            //   var apiTask = new Task(() => LeaveTypes = leaveService.GetLeaveTypes().Result); // creates the task with the call on another thread
            //   apiTask.Start(); // starts the task - important, or you'll spin forever
            //   Task.WaitAll(apiTask);
        }

    }
}
