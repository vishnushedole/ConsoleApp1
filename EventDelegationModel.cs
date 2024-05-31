using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApp1
{
    //1: Event Data Class
    public class CustomEventArgs : System.EventArgs
    {
        //1.1 public data members
        public int Number { get; set; }
        public string Message { get; set; } = "";
    }
    //2: Declare the Delegate
    public delegate void CustomEventHandler(object sender, CustomEventArgs e);
    // 3: Declare the publisher class
    class Publisher 
    {
        //3.1 Declare a variable for the Delegate [Instantion part]
        public event CustomEventHandler MyEvent;
        //3.2 Write a funciton which performs some activity and trigger notification
        public void TrigerNotifications()
        {
            //3.3 Iterate to find the threshold levels
            for (int i=0;i<10;i++)
            {
                //3.4 check for limits
                if(i==5 || i==6)
                {
                    //3.5 Build the Event Data Class with values
                    CustomEventArgs e = new CustomEventArgs
                    {
                        Number = i,
                    Message = "Threshold breached"
                    };
                    //3.6 Check whether the Event object is null, if not then invoke it
                    MyEvent?.Invoke(null, e);
                    //?.is called the null conditional operator and is equevalent to 
                    //if(MyEvent is not null) MyEvent.Invoke(null,0);

                }
            }
        }
    }
    //4 : Declare the Subscriber class
    class Subscriber 
    {
        public void HandleNotifications(object sender,CustomEventArgs e) 
        {
            Console.WriteLine($"SUBSCRIBER SAYS: [Number:{e.Number},Message:{e.Message}]");

        }
    }

    internal class EventDelegationModel
    {
        internal static void Test()
        {
            //5.1 Declare the publisher object
            Publisher p = new Publisher();
            //5.2 Declare the Subscriber object
            Subscriber s = new Subscriber();
            //5.3 Bind the publisher delegate with the subscriber function
            //the second part of the delegate instantiation
            p.MyEvent += new CustomEventHandler(s.HandleNotifications);
            //5.4 call the publisher method to triger notifications
            p.MyEvent += delegate (object sender, CustomEventArgs args)
            {
                Console.WriteLine("Anonymous methods");
            };
            p.MyEvent += (send, args) => { Console.WriteLine("This is Lambda method"); };
            p.TrigerNotifications();

            //5.5 add more subscriber using anonymous method and lambdas
            // and triger notifications again
            Console.WriteLine("\n Adding more handlers/subscribers....");
        }
    }
}
