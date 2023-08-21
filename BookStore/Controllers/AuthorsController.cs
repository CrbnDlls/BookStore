using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using BookStore;
using BookStore.Models;
using System.Collections;
using Newtonsoft.Json;
using System.Text.Json;
using System.Text.Json.Serialization;
using Microsoft.CodeAnalysis.RulesetToEditorconfig;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using BookStore.Enums;
using DAL.Interfaces;
using AutoMapper;
using DAL.Entity;

namespace BookStore.Controllers
{
    public class AuthorsController : Controller
    {
        private readonly IAuthorRepository _authorRepository;
        private readonly IBookRepository _bookRepository;
        private IMapper _mapper;

        public AuthorsController(IAuthorRepository authorRepository, IBookRepository bookRepository, IMapper mapper)
        {
            _authorRepository = authorRepository;
            _mapper = mapper;
            _bookRepository = bookRepository;
        }

        // GET: Authors
        public async Task<IActionResult> Index()
        {
            var authors = await _authorRepository.SelectWithBooks();
            var authorsView = authors.Select(_mapper.Map<AuthorViewModel>);
            return View(authorsView);
        }

        // GET: Authors/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var author = await _authorRepository.GetAuthorWithBooksById(id.Value);
            if (author == null)
            {
                return NotFound();
            }

            return View(_mapper.Map<AuthorViewModel>(author));
        }

        // GET: Authors/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Authors/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,FamilyName,Name,FathersName,BirthDate")] AuthorViewModel author)
        {
            if (ModelState.IsValid)
            {
                _authorRepository.Create(_mapper.Map<Author>(author));
                return RedirectToAction(nameof(Index));
            }
            return View(author);
        }

        // GET: Authors/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var author = await _authorRepository.GetById(id.Value);
            if (author == null)
            {
                return NotFound();
            }
            return View(_mapper.Map<AuthorViewModel>(author));
        }

        // POST: Authors/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,FamilyName,Name,FathersName,BirthDate")] AuthorViewModel author)
        {
            if (id != author.Id)
            {
                return NotFound();
            }
            
            if (ModelState.IsValid)
            {
                try
                {
                    await _authorRepository.Update(_mapper.Map<Author>(author));
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!AuthorExists(author.Id))
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
            return View(author);
        }

        // GET: Authors/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var author = await _authorRepository.GetById(id.Value);
                
            if (author == null)
            {
                return NotFound();
            }

            return View(_mapper.Map<AuthorViewModel>(author));
        }

        // POST: Authors/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var author = await _authorRepository.GetById(id);
            if (author != null)
            {
                await _authorRepository.Delete(id);
            }
            
            return RedirectToAction(nameof(Index));
        }

        private bool AuthorExists(int id)
        {
            return _authorRepository.GetById(id) != null;
        }

        public async Task<IActionResult> GetBooks(int id)
        {
            if (id == null)
            {
                return Json("");
            }

            Author author = await _authorRepository.GetAuthorWithBooksById(id);
            List<BookViewModel> books = author.Books.Select(_mapper.Map<BookViewModel>).ToList();    
            if (books == null)
            {
                return Json(new List<BookViewModel>());
            }
            var options = new JsonSerializerOptions() { ReferenceHandler = ReferenceHandler.IgnoreCycles };

            //options.Converters.Add(new JsonStringEnumConverter()); Не бере DisplayAttribute

            return Json(books, options);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<string> SaveBooks(int id, IEnumerable<BookViewModel> books)
        {
            
            foreach (var book in books)
            {
                switch (book.RecordStatus) 
                {
                    case RecordStatus.ToDelete:
                        if (book.Id == 0)
                            continue;
                        await _bookRepository.Delete(book.Id);
                        break;
                    case RecordStatus.ToInsert:
                        if (book.Id != 0)
                            continue;
                        book.AuthorId = id;
                        await _bookRepository.Create(_mapper.Map<Book>(book));
                        break;
                    case RecordStatus.ToUpdate:
                        if (book.Id == 0)
                            continue;
                        await _bookRepository.Update(_mapper.Map<Book>(book));
                        break;
                    default:
                        continue;
                }
            }
            return "Книги збережено";
        }
    }
}
