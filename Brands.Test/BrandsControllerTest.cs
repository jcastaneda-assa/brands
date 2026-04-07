using Brands.API.Controllers;
using Brands.API.Models;
using Brands.Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;

namespace Brands.Tests
{
    /// <summary>
    /// Run tests to make sure behaviours are correct
    /// following AAA pattern (Arrange, Act, Assert)
    /// (Arrange is done in constructor for all tests)
    /// </summary>
    public class BrandsControllerTest: IDisposable
    {
        private readonly BrandsContext _context;
        private readonly BrandsController _controller;
        
        public BrandsControllerTest()
        {
            var options = new DbContextOptionsBuilder<BrandsContext>()
                /// Add random name to ensure every test
                /// has a fresh in-memory db
                /// to avoid conflicts
                .UseInMemoryDatabase(databaseName: Guid.NewGuid().ToString()) 
                .Options;

            _context = new BrandsContext(options);

            _context.Brands.AddRange(
                new Brand { Id = 1, Name = "Toyota", Country = "Japon", CreatedAtYear = 1937 },
                new Brand { Id = 2, Name = "Ford", Country = "Estados Unidos", CreatedAtYear = 1903 },
                new Brand { Id = 3, Name = "BMW", Country = "Alemania", CreatedAtYear = 1916 }
            );

            _context.SaveChanges();

            _controller = new BrandsController(_context);
        }

        [Fact]
        public async Task GetBrands_ReturnsOkResult()
        {   
            var result = await _controller.GetBrands();
            var okResult = Assert.IsType<OkObjectResult>(result.Result);
            Assert.Equal(200, okResult.StatusCode);
        }

        [Fact]
        public async Task GetBrands_ReturnsAllThreeItems()
        {
            var result = await _controller.GetBrands();
            var okResult = Assert.IsType<OkObjectResult>(result.Result);
            var items = Assert.IsAssignableFrom<IEnumerable<Brand>>(okResult.Value);
            Assert.Equal(3, items.Count());
        }

        [Fact]
        public async Task GetBrands_ReturnsCorrectType()
        {
            var result = await _controller.GetBrands();
            Assert.IsType<OkObjectResult>(result.Result);
        }

        [Fact]
        public async Task GetBrands_ContainsToyota()
        {
            var result = await _controller.GetBrands();
            var okResult = Assert.IsType<OkObjectResult>(result.Result);
            var items = Assert.IsAssignableFrom<IEnumerable<Brand>>(okResult.Value);
            Assert.Contains(items, m => m.Name == "Toyota");
        }

        public void Dispose()
        {
            _context.Dispose();
        }
    }
}
