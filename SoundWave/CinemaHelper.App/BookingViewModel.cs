using SoundWave.App.Core;

namespace SoundWave.App
{
    public class BookingViewModel : ObservableObject
    {
        private TicketService ticketService;

        public BookingViewModel(TicketService ticketService) 
        {
            this.ticketService = ticketService;
        }
    }
}