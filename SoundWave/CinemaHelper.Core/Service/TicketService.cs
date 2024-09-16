namespace SoundWave.App
{
    public class TicketService
    {
        private AlbumRemoteDataSource ticketDataSource;

        public TicketService(AlbumRemoteDataSource ticketDataSource)
        {
            this.ticketDataSource = ticketDataSource;
        }
    }
}