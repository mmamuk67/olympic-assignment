using Microsoft.AspNetCore.Mvc;
using olympic_assignment.Models;

namespace olympic_assignment.Controllers
{
    [Route("todo")]
    public class ToDoController : Controller
    {
        private readonly ITicketRepository _repository;

        public ToDoController(ITicketRepository repository)
        {
            _repository = repository;
        }

        [HttpGet("")]
        [HttpGet("index")]
        public IActionResult Index(string statusFilter = "all")
        {
            var tickets = _repository.GetAllTickets();

            if (!string.IsNullOrEmpty(statusFilter) && statusFilter.ToLower() != "all")
            {
                tickets = tickets.Where(t => t.Status.ToLower() == statusFilter.ToLower());
            }

            ViewBag.CurrentFilter = statusFilter;
            return View(tickets.ToList());
        }

        [HttpGet("add")]
        public IActionResult Add()
        {
            return View(new Ticket());
        }

        [HttpPost("add")]
        public IActionResult Add(Ticket ticket)
        {
            if (ModelState.IsValid)
            {
                _repository.AddTicket(ticket);
                _repository.Save();
                return RedirectToAction("Index");
            }
            return View(ticket);
        }

        [HttpPost("changestatus/{id}")]
        public IActionResult ChangeStatus(int id, string newStatus)
        {
            var ticket = _repository.GetTicketById(id);
            if (ticket != null && !string.IsNullOrWhiteSpace(newStatus))
            {
                ticket.Status = newStatus;
                _repository.Save();
            }
            return RedirectToAction("Index");
        }

        [HttpPost("delete/{id}")]
        public IActionResult Delete(int id)
        {
            _repository.DeleteTicket(id);
            _repository.Save();
            return RedirectToAction("Index");
        }
    }
}
