namespace ShoppingCart.EventFeed
{
    public interface IEventStore
    {
        IEnumerable<Event> GetEvents(
                long firstEventSequenceNumber, long lastEventSequenceNumber);
        void Raise(string eventName, object content);
    }


    public class EventStore : IEventStore, IService
    {
        private readonly List<Event> eventStore = [];
        private readonly long globalEventSequenceNumber = 0;

        public IEnumerable<Event> GetEvents(long firstEventSequenceNumber, long lastEventSequenceNumber)
        {
            var end = Math.Min(eventStore.Count, (int)lastEventSequenceNumber - (int)firstEventSequenceNumber);
            var events = this.eventStore
                //Careful casting long to int!!
                //This is only a temporary implementation
                .GetRange((int)firstEventSequenceNumber, end);
            return events;
        }

        // public IEnumerable<Event> GetEvents(
        //         long firstEventSequenceNumber,
        //         long lastEventSequenceNumber) =>
        //     database
        //         .Where(e =>
        //                 e.SequenceNumber >= firstEventSequenceNumber &&
        //                 e.SequenceNumber <= lastEventSequenceNumber)
        //         .OrderBy(e => e.SequenceNumber);

        public void Raise(string eventName, object content)
        {
            Event newEvent = new(this.globalEventSequenceNumber + 1, DateTimeOffset.UtcNow, eventName, content);
            this.eventStore.Add(newEvent);

            // var seqNumber = database.NextSequenceNumber();
            // database.Add(
            //         new Event (seqNumber,
            //             DateTimeOffset.UtcNow,
            //             eventName,
            //             content)
            //         );
        }
    }
}
