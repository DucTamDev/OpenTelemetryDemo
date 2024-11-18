using System;
using System.Collections.Generic;
using System.Diagnostics.Tracing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleAppDemo
{
    public class EventListenerProgram
    {
        public EventListenerProgram()
        {
            using (var listener = new MyEventListener())
            {
                // Emit some events
                MyEventSource.Log.Information("This is an informational message");
                MyEventSource.Log.Warning("This is a warning message");
                MyEventSource.Log.Error("This is an error message");

                // Keep the console open
                Console.ReadLine();
            }
        }
    }


    // Define your custom event source
    [EventSource(Name = "MyCustomEventSource")]
    public class MyEventSource : EventSource
    {
        public static MyEventSource Log = new MyEventSource();

        // Define your events
        [Event(1)]
        public void Information(string message)
        {
            if (IsEnabled())
            {
                WriteEvent(1, message);
            }
        }

        [Event(2)]
        public void Warning(string message)
        {
            if (IsEnabled())
            {
                WriteEvent(2, message);
            }
        }

        [Event(3)]
        public void Error(string message)
        {
            if (IsEnabled())
            {
                WriteEvent(3, message);
            }
        }
    }

    // Listener class to subscribe to events
    public class MyEventListener : EventListener
    {
        protected override void OnEventSourceCreated(EventSource eventSource)
        {
            if (eventSource.Name == "MyCustomEventSource")
            {
                EnableEvents(eventSource, EventLevel.LogAlways);
            }
        }

        protected override void OnEventWritten(EventWrittenEventArgs eventData)
        {
            Console.WriteLine($"Event {eventData.EventId}: {eventData.Payload[0]}");
        }
    }

}
