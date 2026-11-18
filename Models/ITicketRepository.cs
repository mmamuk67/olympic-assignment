namespace olympic_assignment.Models
{
    public interface ITicketRepository
    {
        IEnumerable<Ticket> GetAllTickets();
        Ticket? GetTicketById(int id);
        void AddTicket(Ticket ticket);
        void DeleteTicket(int id);
        void Save();
    }
}