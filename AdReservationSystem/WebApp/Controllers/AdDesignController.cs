using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using DAL;


using System.IO;
using Domain;
using Domain.App;

namespace WebApp.Controllers
{
    public class AdDesignController : Controller
    {
        private readonly ApplicationDbContext _context;

        public AdDesignController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: AdDesign
        public async Task<IActionResult> Index()
        {
            return View(await _context.AdDesigns.ToListAsync());
        }

        // GET: AdDesign/Details/5
        public async Task<IActionResult> Details(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var adDesign = await _context.AdDesigns
                .FirstOrDefaultAsync(m => m.AdDesignId == id);
            if (adDesign == null)
            {
                return NotFound();
            }
            
            return View(adDesign);
        }

        [HttpGet]
        public ActionResult ViewPdf(string target, string name)
        {
            //this is a custom method
            
            var filePath = "." + Path.DirectorySeparatorChar + "designs" + Path.DirectorySeparatorChar + name + "." + "pdf";
            var fileAsBytes = System.IO.File.ReadAllBytes(filePath);
            
            Response.Headers.Add("Content-Disposition", $"inline; {name}");
            
            // contains two ways for viewing PDF files:
            
            //for downloading!
            //return File(designAsBytes, "application/pdf", refToImage + ".pdf");
            
            //for opening the PDF in browser!
            return File(fileAsBytes, "application/pdf");
        }
       
      

        // GET: AdDesign/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: AdDesign/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("AdDesignId,Name,RefToImage")] AdDesign adDesign, IFormFile postedFile)
        {
            //this is a custom method
            
            // design RefToImage is not used yet, keeping it in domain in case the filesystem grows more complicated.
            if (postedFile != null)
            {
                var path = "." + Path.DirectorySeparatorChar + "designs";
                var fileLoc =  path + Path.DirectorySeparatorChar + adDesign.Name + ".pdf";
                if (!Directory.Exists(path))
                {
                    Directory.CreateDirectory(path);
                }
                if (postedFile.Length > 0)
                {
                    using (var ms = new MemoryStream())
                    {
                        await postedFile.CopyToAsync(ms);
                        var fileBytes = ms.ToArray();
                        await System.IO.File.WriteAllBytesAsync(fileLoc, fileBytes);
                    }
                }
            }
            if (ModelState.IsValid)
            {
                adDesign.AdDesignId = Guid.NewGuid();
                _context.Add(adDesign);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(adDesign);
        }
        
        [HttpPost]
        public ActionResult GetFileFromUser(IFormFile postedFile)
        {
            if (postedFile != null)
            {
                var path = "." + Path.DirectorySeparatorChar + "designs";
                var fileLoc =  path + Path.DirectorySeparatorChar + "bbb" + ".pdf";
                if (!Directory.Exists(path))
                {
                    Directory.CreateDirectory(path);
                }
                if (postedFile.Length > 0)
                {
                    using (var ms = new MemoryStream())
                    {
                        postedFile.CopyTo(ms);
                        var fileBytes = ms.ToArray();
                        System.IO.File.WriteAllBytes(fileLoc, fileBytes);
                    }
                }
            }

            return Ok();
        }

        // GET: AdDesign/Edit/5
        public async Task<IActionResult> Edit(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var adDesign = await _context.AdDesigns.FindAsync(id);
            if (adDesign == null)
            {
                return NotFound();
            }
            return View(adDesign);
        }

        // POST: AdDesign/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(Guid id, [Bind("AdDesignId,Name,RefToImage")] AdDesign adDesign)
        {
            if (id != adDesign.AdDesignId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(adDesign);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!AdDesignExists(adDesign.AdDesignId))
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
            return View(adDesign);
        }

        // GET: AdDesign/Delete/5
        public async Task<IActionResult> Delete(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var adDesign = await _context.AdDesigns
                .FirstOrDefaultAsync(m => m.AdDesignId == id);
            if (adDesign == null)
            {
                return NotFound();
            }

            return View(adDesign);
        }

        // POST: AdDesign/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(Guid id)
        {
            var adDesign = await _context.AdDesigns.FindAsync(id);
            if (adDesign != null)
            {
                _context.AdDesigns.Remove(adDesign);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool AdDesignExists(Guid id)
        {
            return _context.AdDesigns.Any(e => e.AdDesignId == id);
        }
    }
}
