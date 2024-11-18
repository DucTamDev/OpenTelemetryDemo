
////using ConsoleAppDemo;
////using System.Diagnostics.Tracing;

////EventSourceProgram eventSourceProgram = new EventSourceProgram();
//////DiagnosticSourceProgram diagnosticSourceProgram = new DiagnosticSourceProgram();
//////EventListenerProgram eventListenerProgram =  new EventListenerProgram();


//using Newtonsoft.Json;

//DateTime a = DateTime.Now;
//DateTime b = DateTime.UtcNow;

//var ob = new { a, b };
//Console.Write(JsonConvert.SerializeObject(ob));

//string abc = CreateKey("HELLO {0}", "sdfsdf");

//Console.WriteLine("abc:" +abc);

//static string? CreateKey(string key, params object[] args)
//{
//    return args.Length > 0 ? string.Format(key, args).ToLower() : key?.ToLower();
//}


// Get all time zone information objects
using System.Collections.ObjectModel;

ReadOnlyCollection<TimeZoneInfo> timeZones = TimeZoneInfo.GetSystemTimeZones();

Console.WriteLine("Available Time Zones:");

// Loop through each time zone and print details
foreach (TimeZoneInfo timeZone in timeZones)
{
    Console.WriteLine($"\t- {timeZone.Id} ({timeZone.DisplayName})");
}