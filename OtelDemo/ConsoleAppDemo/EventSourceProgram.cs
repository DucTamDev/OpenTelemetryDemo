using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Diagnostics.Tracing;

namespace ConsoleAppDemo
{
    public class EventSourceProgram
    {
        public EventSourceProgram()
        {
            //Initialize();
            ConsoleWriterEventListener listener = new ConsoleWriterEventListener();
            DemoEventSource demoEventSource = new DemoEventSource();
            demoEventSource.WorkStart("Alo...............");
        }

        private async void Initialize()
        {
            ConsoleWriterEventListener listener = new ConsoleWriterEventListener();

            Task a = ProcessWorkItem("A");
            Task b = ProcessWorkItem("B");
            await Task.WhenAll(a, b);

            
        }

        private async Task ProcessWorkItem(string requestName)
        {
            DemoEventSource.Log.WorkStart(requestName);
            await HelperA();
            await HelperB();
            DemoEventSource.Log.WorkStop();
        }

        private async Task HelperA()
        {
            DemoEventSource.Log.DebugMessage("HelperA");
            await Task.Delay(100); // pretend to do some work
        }

        private async Task HelperB()
        {
            DemoEventSource.Log.DebugMessage("HelperB");
            await Task.Delay(100); // pretend to do some work
        }
    }

    [EventSource(Name = "Demo")]
    public class DemoEventSource : EventSource
    {
        public static DemoEventSource Log = new DemoEventSource();

        [Event(1)]
        public void WorkStart(string requestName) => WriteEvent(1, requestName);
        [Event(2)]
        public void WorkStop() => WriteEvent(2);

        [Event(3)]
        public void DebugMessage(string message) => WriteEvent(3, message);
    }

    public class ConsoleWriterEventListener : EventListener
    {
        protected override void OnEventSourceCreated(EventSource eventSource)
        {
            if (eventSource.Name == "Demo")
            {
                Console.WriteLine("{0,-5} {1,-40} {2,-15} {3}", "TID", "Activity ID", "Event", "Arguments");
                EnableEvents(eventSource, EventLevel.Verbose);
            }
            else if (eventSource.Name == "System.Threading.Tasks.TplEventSource")
            {
                // Activity IDs aren't enabled by default.
                // Enabling Keyword 0x80 on the TplEventSource turns them on
                EnableEvents(eventSource, EventLevel.LogAlways, (EventKeywords)0x80);
            }
        }

        protected override void OnEventWritten(EventWrittenEventArgs eventData)
        {
            lock (this)
            {
                Console.Write("{0,-5} {1,-40} {2,-15} ", eventData.OSThreadId, eventData.ActivityId, eventData.EventName);
                if (eventData.Payload.Count == 1)
                {
                    Console.WriteLine(eventData.Payload[0]);
                }
                else
                {
                    Console.WriteLine("listening.....");
                }
            }
        }
    }
}
