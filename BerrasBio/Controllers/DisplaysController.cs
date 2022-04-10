using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using BerrasBio.Data;
using BerrasBio.Models;

namespace BerrasBio.Controllers
{
    public class DisplaysController : Controller
    {
        private readonly BerrasBioContext _context;

        public DisplaysController(BerrasBioContext context)
        {
            _context = context;
        }

        /// <summary>
        /// GET: Displays/Home
        /// </summary>
        /// <returns>View(Home)</returns>
        public IActionResult Home()
        {
            return View();
        }

        /// <summary>
        /// GET: Displays/Index
        /// </summary>
        /// <returns>View(Index)</returns>
        public async Task<IActionResult> Index()
        {
            var berrasBioContext = _context.Display
                .Include(d => d.Movie)
                .Include(d => d.Salon);

            return View(await berrasBioContext.ToListAsync());
        }

        /// <summary>
        /// GET: Displays/Bookings
        /// </summary>
        /// <param name="id"></param>
        /// <returns>View(Bookings)</returns>
        // 
        [HttpGet]
        public async Task<IActionResult> Bookings(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var display = await _context.Display
                .Include(d => d.Movie)
                .Include(d => d.Salon)
                .FirstOrDefaultAsync(m => m.ID == id);

            DateTime today = DateTime.Now;
            ViewData["DateToday"] = today.ToString("dd-MM-yyyy");

            ViewBag.SeatsLeft = display.SeatsLeft < 12 ? display.SeatsLeft : 12;

            ViewBag.TicketPrice = display.Movie.MoviePrice;

            if (id == null)
            {
                return NotFound();
            }

            return View(display);
        }

        /// <summary>
        /// POST: Display/Bookings
        /// </summary>
        /// <param name="id"></param>
        /// <param name="nrOfTickets"></param>
        /// <returns>View(Booked)</returns>
        [HttpPost]
        public async Task<IActionResult> Bookings(int? id, int nrOfTickets)
        {
            if (id == null)
            {
                return NotFound();
            }

            var bookDisplay = await _context.Display
                .Include(d => d.Movie)
                .Include(d => d.Salon)
                .FirstOrDefaultAsync(m => m.ID == id);

            bookDisplay.SeatsLeft -= nrOfTickets;

            _context.Update(bookDisplay);
            _context.SaveChanges();

            if (bookDisplay == null)
            {
                return NotFound();
            }

            return RedirectToAction("Booked", new { id, nrOfTickets });
        }

        /// <summary>
        /// GET: Display/Booked
        /// </summary>
        /// <param name="id"></param>
        /// <param name="nrOfTickets"></param>
        /// <returns>View(bookedDisplay)</returns>
        public async Task<IActionResult> Booked(int? id, int? nrOfTickets)
        {
            if (id == null || nrOfTickets == null)
            {
                return NotFound();
            }

            var bookedDisplay = await _context.Display
                .Include(d => d.Movie)
                .Include(d => d.Salon)
                .FirstOrDefaultAsync(m => m.ID == id);

            DateTime today = DateTime.Now;
            ViewData["DateToday"] = today.ToString("dd-MM-yyyy");
            ViewData["NrOfTickets"] = nrOfTickets;
            var ticketPrice = bookedDisplay.Movie.MoviePrice;
            var salonPrice = bookedDisplay.Salon.Price;
            ViewData["TotalPrice"] = ticketPrice * nrOfTickets;

            if (bookedDisplay == null)
            {
                return NotFound();
            }

            return View(bookedDisplay);
        }

        // GET: Displays/Details/5
        [HttpGet]
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var display = await _context.Display
                .Include(d => d.Movie)
                .Include(d => d.Salon)
                .FirstOrDefaultAsync(m => m.ID == id);
            
            DateTime today = DateTime.Today;
            ViewData["DateToDay"] = today.ToString("dd-MM-yyyy");

            ViewBag.TicketPrice = display.Movie.MoviePrice;

            if (display == null)
            {
                return NotFound();
            }

            return View(display);
        }

        // GET: Displays/Create
        public IActionResult Create()
        {
            ViewData["MovieID"] = new SelectList(_context.Set<Movie>(), "ID", "ID");
            ViewData["SalonID"] = new SelectList(_context.Set<Salon>(), "ID", "ID");
            return View();
        }

        // POST: Displays/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("ID,StartingTime,MovieID,SalonID,SeatsLeft")] Display display)
        {
            if (ModelState.IsValid)
            {
                _context.Add(display);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["MovieID"] = new SelectList(_context.Set<Movie>(), "ID", "ID", display.MovieID);
            ViewData["SalonID"] = new SelectList(_context.Set<Salon>(), "ID", "ID", display.SalonID);
            return View(display);
        }

        // GET: Displays/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var display = await _context.Display.FindAsync(id);
            if (display == null)
            {
                return NotFound();
            }
            ViewData["MovieID"] = new SelectList(_context.Set<Movie>(), "ID", "ID", display.MovieID);
            ViewData["SalonID"] = new SelectList(_context.Set<Salon>(), "ID", "ID", display.SalonID);
            return View(display);
        }

        // POST: Displays/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("ID,StartingTime,MovieID,SalonID,SeatsLeft")] Display display)
        {
            if (id != display.ID)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(display);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!DisplayExists(display.ID))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            ViewData["MovieID"] = new SelectList(_context.Set<Movie>(), "ID", "ID", display.MovieID);
            ViewData["SalonID"] = new SelectList(_context.Set<Salon>(), "ID", "ID", display.SalonID);
            return View(display);
        }

        // GET: Displays/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var display = await _context.Display
                .Include(d => d.Movie)
                .Include(d => d.Salon)
                .FirstOrDefaultAsync(m => m.ID == id);
            if (display == null)
            {
                return NotFound();
            }

            return View(display);
        }

        // POST: Displays/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var display = await _context.Display.FindAsync(id);
            _context.Display.Remove(display);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool DisplayExists(int id)
        {
            return _context.Display.Any(e => e.ID == id);
        }
    }
}
