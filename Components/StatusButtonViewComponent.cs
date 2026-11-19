using Microsoft.AspNetCore.Mvc;
using olympic_assignment.Models;

namespace olympic_assignment.Components
{
    public class StatusButtonViewComponent : ViewComponent
    {
        public IViewComponentResult Invoke(Ticket ticket)
        {
            string normalized = (ticket.Status ?? "").Trim().ToLower();

            ViewBag.ButtonClass = normalized switch
            {
                "to do" or "todo" => "btn-secondary",
                "in progress" => "btn-warning text-dark",
                "qa" or "quality assurance" => "btn-info text-dark",
                "done" => "btn-success",
                _ => "btn-outline-secondary"
            };

            ViewBag.DisplayLabel = normalized switch
            {
                "to do" or "todo" => "To Do",
                "in progress" => "In Progress",
                "qa" or "quality assurance" => "QA",
                "done" => "Done",
                _ => string.IsNullOrWhiteSpace(ticket.Status) ? "To Do" : ticket.Status
            };

            ViewBag.NextStatus = normalized switch
            {
                "to do" or "todo" => "In Progress",
                "in progress" => "QA",
                "qa" or "quality assurance" => "Done",
                "done" => "To Do",
                _ => "To Do"
            };

            return View(ticket);
        }
    }
}
