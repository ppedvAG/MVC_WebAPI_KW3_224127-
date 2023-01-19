using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using System.Drawing;
using System.Runtime.ConstrainedExecution;
using System;
using WebAPI_ControllerSamples.Models;
using System.Text;
using WebAPI_ControllerSamples.Data;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using Microsoft.AspNetCore.Components.RenderTree;
using Microsoft.DotNet.Scaffolding.Shared.CodeModifier.CodeChange;

namespace WebAPI_ControllerSamples.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ReturnValuesController : ControllerBase
    {
        private readonly ILogger _logger;
        private readonly MovieDbContext _context;
        
        public ReturnValuesController(ILogger<ReturnValuesController> logger, MovieDbContext context)
        {
            _logger = logger;
            _context = context;
        }

        [HttpGet("StringSample_HelloWorld")]
        public string HelloWorld() => "Hello World";

        [HttpGet("ContentResultSample_HelloWorld")]
        public ContentResult HelloWorld2()
        => Content("Hello World");

        [HttpGet("ContentResultVsString")]
        public ContentResult GetData()
        {

            //In der WebAPI gibt es den Rückgabetyp ContentResult. Was ist der Unterschied bei der Rückgabe, zwischen einem string und einem ContentResult?

            //Der Unterschied zwischen der Rückgabe eines string und eines ContentResult in einer ASP.NET Core Web API besteht darin, dass ContentResult ein erweitertes Ergebnis darstellt,
            //das zusätzlich zum Rückgabewert (einem string) auch den HTTP-Statuscode und den Content-Typ enthalten kann.
            //
            //Wenn Sie einen einfachen string zurückgeben, wird standardmäßig der Statuscode 200 (OK) und der Content-Typ "text/plain" verwendet.
            //Mit ContentResult können Sie jedoch beides explizit festlegen.

            return Content("Hello World!", "text/plain", Encoding.UTF8);
        }




        [HttpGet("GetComplexObject")]
        public Movie GetAnyMovie()
        {
            //Das Object Movie wird als JSON zurück gegeben. 
            //Contra: 
            //  -   Wenn du einen Fehler hast, kannst du keinen Fehlercode mitsenden. 

            Movie movie = new Movie() { Id = 123, Title = "Coda", Description = "talentierte Sängerin in einer Familie mit Taubstummen", Price = 10 };
            return movie;
        }


        #region IActionResult / ActionResult 
        [HttpGet("GetMovie_with_IActionResult")]
        public async Task<IActionResult> GetMovie_with_IActionResult()
        {
            Movie movie = new Movie() { Id = 123, Title = "Coda", Description = "talentierte Sängerin in einer Familie mit Taubstummen", Price = 10 };

            if (movie.Id == 0)
                return NotFound(); //404 Fehler wird zurück gegeben 

            if (movie.Title == "The Crow")
                return BadRequest("Der Film steht auf dem Index"); //400 Fehler

            return Ok(movie); //200 
        }

        [HttpGet("GetMovie_with_ActionResult")]
        public async Task<ActionResult> GetMovie_with_ActionResult()
        {
            Movie movie = new Movie() { Id = 123, Title = "Coda", Description = "talentierte Sängerin in einer Familie mit Taubstummen", Price = 10 };

            if (movie.Id == 0)
                return NotFound(); //404 Fehler wird zurück gegeben 

            if (movie.Title == "The Crow")
                return BadRequest("Der Film steht auf dem Index"); //400 Fehler

            return Ok(movie); //200 
        }

        [HttpGet("GetMovie_with_GenericActionResult")]
        public async Task<ActionResult<Movie>> GetMovie_with_GenericActionResult()
        {
            Movie movie = new Movie() { Id = 123, Title = "Coda", Description = "talentierte Sängerin in einer Familie mit Taubstummen", Price = 10 };

            if (movie.Id == 0)
                return NotFound(); //404 Fehler wird zurück gegeben 

            if (movie.Title == "The Crow")
                return BadRequest("Der Film steht auf dem Index"); //400 Fehler

            return Ok(movie); //200 
        }


        #endregion


        #region Eine Liste von Movies 
        [HttpGet("GetMovies_return_List")]
        public async Task<ActionResult<IEnumerable<Movie>>> GetMovie()
        {
            if (_context.Movie == null)
            {
                return NotFound();
            }

            //ASP.NET Core puffert das Ergebnis von Aktionen, die zurückgegeben werden
            return await _context.Movie.ToListAsync(); //Wir verlasen die Methode mit einer Liste von Movies
        }
        #endregion



        #region IEnumerable<T> / IAsyncEnumerable<T>
        [HttpGet("GetMovies_with_Enumerable_yield_return_movie")]
        public IEnumerable<Movie> GetMovies2()
        {
            var movies = _context.Movie.ToList();

            foreach(Movie movie in movies)
                yield return movie;
        }


        [HttpGet("GetMovies_with_IAsyncEnumerable_and_yield_return")]
        public async IAsyncEnumerable<Movie> GetMovies3()
        {
            IAsyncEnumerable<Movie> movies = _context.Movie.AsAsyncEnumerable();

            await foreach (Movie currentMovie in movies)
            {
                yield return currentMovie;
            }
        }
        #endregion




        #region IResult
        //Für was benötige ich die IResult als Rückgabetyp?
        //IResult kann verwendet werden, um einen allgemeinen Rückgabetyp für eine API-Methode zu definieren.
        // Dies ist nützlich, wenn Sie verschiedene Arten von Ergebnissen zurückgeben müssen, z.B.einen Erfolgscode, ein Fehlerobjekt oder
        // ein anderes benutzerdefiniertes Ergebnis.Ein anderer Vorteil ist, dass es Ihnen ermöglicht, den Rückgabetyp einfach zu ändern, ohne den Code zu ändern.

        [HttpGet("{id}")]
        [ProducesResponseType(typeof(Movie), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public IResult GetById(int id)
        {
            var product = _context.Movie.Find(id);
            

            return product == null ? 
                Results.NotFound() : 
                Results.Ok(product);
        }

        #endregion

    }
}
