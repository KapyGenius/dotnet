using System;
using System.Collections.Generic;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using NetAngular.Models;
using NetAngular.Repositories;

namespace NetAngular.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class CategoryController : ControllerBase
    {
        private readonly ICategorieRepository _repository;
        private readonly string _path;

        public CategoryController(ICategorieRepository repository, IWebHostEnvironment env)
        {
            _repository = repository;
            _path = System.IO.Path.Combine(env.ContentRootPath, "data", "images", "categories");
        }

        [HttpGet]
        public IEnumerable<Category> Get()
        {
            return _repository.GetCategories();

        }

        [HttpGet("{Id}")]
        public IActionResult Get(int Id)
        {
            var result = _repository.GetCategoryById(Id);

            if (result == null)
            {
                return NotFound();
            }

            return Ok(result);
        }

        [HttpPost]
        public IActionResult Post([FromBody] Category category)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var result = _repository.CreateCategory(category);

            return CreatedAtAction("Get", new { id = result.Id } , result);
        }

        [HttpPut("{id}")]
        public IActionResult Update(int id, [FromBody] Category category)
        {
            if(id != category.Id)
            {
                return BadRequest();
            }

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (_repository.GetCategoryById(id) == null)
            {
                return NotFound();
            }

            var result = _repository.UpdateCategory(category);

            return Ok(result);
        }

        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            if(_repository.GetCategoryById(id) == null)
            {
                return BadRequest();
            }

            _repository.DeleteCategorie(id);

            return NoContent();
        }

        [HttpGet("{id}/image")]
        public IActionResult Image(int id)
        {
            var file = System.IO.Path.Combine(_path, $"{id}.jpg");
            if (System.IO.File.Exists(file))
            {
                var bytes = System.IO.File.ReadAllBytes(file);
                return File(bytes, "image/jpeg");
            }

            return NotFound();
        }
    }
}
