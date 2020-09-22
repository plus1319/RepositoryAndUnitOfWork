using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using RepositoryAndUnitOfWork.Contracts;
using RepositoryAndUnitOfWork.Data;
using RepositoryAndUnitOfWork.Models;

namespace RepositoryAndUnitOfWork.Controllers
{
    public class UsersController : Controller
    {
        private readonly IUnitOfWork _uow;

        public UsersController(IUnitOfWork uow)
        {
            _uow = uow;
        }

        // GET: Users
        public async Task<IActionResult> Index()
        {
            return View(await _uow.UserRepository.GetAllAsync());
        }

        // GET: Users/Details/5
        public async Task<IActionResult> Details(int? id,CancellationToken cancellationToken)
        {
            if (id == null)
            {
                return NotFound();
            }

            var user = await _uow.UserRepository
                .GetByIdAsync(cancellationToken,id);
            if (user == null)
            {
                return NotFound();
            }

            return View(user);
        }

        // GET: Users/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Users/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Name")] User user)
        {
            if (ModelState.IsValid)
            {
                _uow.UserRepository.Add(user);
                await _uow.CommitAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(user);
        }

        // GET: users/Edit/5
        public async Task<IActionResult> Edit(int? id,CancellationToken cancellationToken)
        {
            if (id == null)
            {
                return NotFound();
            }

            var user = await _uow.UserRepository.GetByIdAsync(cancellationToken, id);
            if (user == null)
            {
                return NotFound();
            }
            return View(user);
        }

        // POST: users/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Name")] User user)
        {
            if (id != user.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _uow.UserRepository.Update(user);
                    await _uow.CommitAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!UserExists(user.Id))
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
            return View(user);
        }

        // GET: users/Delete/5
        public async Task<IActionResult> Delete(int? id,CancellationToken cancellationToken)
        {
            if (id == null)
            {
                return NotFound();
            }

            var user = await _uow.UserRepository
                .GetByIdAsync(cancellationToken, id);
            if (user == null)
            {
                return NotFound();
            }

            return View(user);
        }

        // POST: users/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id,CancellationToken cancellationToken)
        {
            var user = await _uow.UserRepository.GetByIdAsync(cancellationToken, id);
            _uow.UserRepository.Delete(user);
            await _uow.CommitAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool UserExists(int id)
        {
            var user = _uow.UserRepository.GetById(id);
            return user != null;
        }
        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
