﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using MusicCollaboration.Data;
using MusicCollaboration.Models;

namespace MusicCollaboration.Controllers
{
    public class CollaborationsController : Controller
    {
        private readonly MusicCollaborationContext _context;

        public CollaborationsController(MusicCollaborationContext context)
        {
            _context = context;
        }

     

      
        // GET: Collaborations
        public async Task<IActionResult> Index(CollaborationGenre searchGenre, string searchString)
        {
            //LINQ query to select the collaborations
            var collaborations = from m in _context.Collaboration
                                 select m;
            //If searchString parameter contains a string
            if (!String.IsNullOrEmpty(searchString))
            {
                //query modified to filter on the value of the search string
                    //Contains is run on the db, maps to SQL LIKE
                collaborations = collaborations.Where(s => s.Title.Contains(searchString));
            }

            if (searchGenre != 0)
            {
                collaborations = collaborations.Where(x => x.Genre == searchGenre);
            }
                

            return View(await collaborations.ToListAsync());
            //return View(await _context.Collaboration.ToListAsync());
        }
      


        // GET: Collaborations/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var collaboration = await _context.Collaboration
                .FirstOrDefaultAsync(m => m.ID == id);
            if (collaboration == null)
            {
                return NotFound();
            }

            return View(collaboration);
        }

        // GET: Collaborations/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Collaborations/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("ID,Title,Description,Bpm,SongKey,Genre,Type")] Collaboration collaboration)
        {
            if (ModelState.IsValid)
            {
                _context.Add(collaboration);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(collaboration);
        }

        // GET: Collaborations/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var collaboration = await _context.Collaboration.FindAsync(id);
            if (collaboration == null)
            {
                return NotFound();
            }
            return View(collaboration);
        }

        // POST: Collaborations/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("ID,Title,Description,Bpm,SongKey,Genre,Type")] Collaboration collaboration)
        {
            if (id != collaboration.ID)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(collaboration);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!CollaborationExists(collaboration.ID))
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
            return View(collaboration);
        }

        // GET: Collaborations/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var collaboration = await _context.Collaboration
                .FirstOrDefaultAsync(m => m.ID == id);
            if (collaboration == null)
            {
                return NotFound();
            }

            return View(collaboration);
        }

        // POST: Collaborations/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var collaboration = await _context.Collaboration.FindAsync(id);
            _context.Collaboration.Remove(collaboration);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool CollaborationExists(int id)
        {
            return _context.Collaboration.Any(e => e.ID == id);
        }
    }
}