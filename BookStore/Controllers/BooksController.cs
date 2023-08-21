using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using BookStore;
using BookStore.Models;
using DAL.Interfaces;
using AutoMapper;
using DAL.Entity;
using DAL.Repositories;

namespace BookStore.Controllers
{
    public class BooksController : Controller
    {
        private readonly IBookRepository bookRepository;
        private readonly IAuthorRepository authorRepository;
        private readonly IMapper mapper;

        public BooksController(IBookRepository bookRepository,  IMapper mapper, IAuthorRepository authorRepository)
        {
            this.bookRepository = bookRepository;
            this.mapper = mapper;
            this.authorRepository = authorRepository;
        }

        // GET: Books
        public async Task<IActionResult> Index()
        {
            var books = await bookRepository.SelectWithAuthors();
            return View(books.Select(mapper.Map<BookViewModel>));
        }

        // GET: Books/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var book = await bookRepository.GetBookWithAuthor(id.Value);
            if (book == null)
            {
                return NotFound();
            }

            return View(mapper.Map<BookViewModel>(book));
        }

        // GET: Books/Create
        public async Task<IActionResult> Create()
        {
            ViewData["AuthorId"] = new SelectList((await authorRepository.Select()).Select(mapper.Map<AuthorViewModel>), "Id", "FullName");
            return View();
        }

        // POST: Books/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Title,PageQuantity,Genre,AuthorId")] BookViewModel book)
        {
            if (ModelState.IsValid)
            {
                bookRepository.Create(mapper.Map<Book>(book));
                return RedirectToAction(nameof(Index));
            }
            ViewData["AuthorId"] = new SelectList((await authorRepository.Select()).Select(mapper.Map<AuthorViewModel>), "Id", "FullName");
            return View(book);
        }

        // GET: Books/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var book = await bookRepository.GetBookWithAuthor(id.Value);
            if (book == null)
            {
                return NotFound();
            }
            ViewData["AuthorId"] = new SelectList((await authorRepository.Select()).Select(mapper.Map<AuthorViewModel>), "Id", "FullName");
            return View(mapper.Map<BookViewModel>(book));
        }

        // POST: Books/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Title,PageQuantity,Genre,AuthorId")] BookViewModel book)
        {
            if (id != book.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    await bookRepository.Update(mapper.Map<Book>(book));
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!BookExists(book.Id))
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
            ViewData["AuthorId"] = new SelectList((await authorRepository.Select()).Select(mapper.Map<AuthorViewModel>), "Id", "FullName");
            return View(book);
        }

        // GET: Books/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var book = await bookRepository.GetBookWithAuthor(id.Value);
            if (book == null)
            {
                return NotFound();
            }

            return View(mapper.Map<BookViewModel>(book));
        }

        // POST: Books/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var book = await bookRepository.GetById(id);
            if (book != null)
            {
                await bookRepository.Delete(id);
            }
            
           return RedirectToAction(nameof(Index));
        }

        private bool BookExists(int id)
        {
          return bookRepository.GetById(id) != null;
        }
    }
}
