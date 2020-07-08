using Microsoft.Extensions.Logging;

namespace BaseCleanArchitecture.Infra.Logs
{
    public class DataEvents
    {
        public static EventId Get = new EventId(10001, "Get register event");
        public static EventId Create = new EventId(10002, "Create register event");
        public static EventId Edit = new EventId(10003, "Edit register event");
        public static EventId Delete = new EventId(10004, "Delete register event");
    }
}