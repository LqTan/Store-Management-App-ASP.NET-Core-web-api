using EcommerceApp.Helpers;
using EcommerceApp.Models;
using EcommerceApp.Repositories.InterfaceRepositories;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace EcommerceApp.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductController : ControllerBase
    {
        private readonly IProductRepository repo;
        private readonly IWebHostEnvironment webHostEnvironment;

        public ProductController(IProductRepository repo, IWebHostEnvironment webHostEnvironment)
        {
            this.repo = repo;
            this.webHostEnvironment = webHostEnvironment;
        }
        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            try
            {
                var products = await repo.GetAllAsync();

                var baseUrl = $"{Request.Scheme}://{Request.Host}{Request.PathBase}";
                foreach (var product in products)
                {
                    if (!string.IsNullOrEmpty(product.Image))
                    {
                        var imageName = Path.GetFileName(product.Image);
                        product.Image = $"{baseUrl}/assets/{imageName}";
                    }
                }

                return Ok(products);
            }
            catch
            {
                return BadRequest();
            }
        }
        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            try
            {
                var product = await repo.GetByIdAsync(id);
                var baseUrl = $"{Request.Scheme}://{Request.Host}{Request.PathBase}";
                if (!string.IsNullOrEmpty(product.Image))
                {
                    var imageName = Path.GetFileName(product.Image);
                    product.Image = $"{baseUrl}/assets/{imageName}";
                }
                
                return Ok(product);
            } 
            catch
            {
                return BadRequest();
            }
            //var brand = await repo.GetByIdAsync(id);
            //return brand == null ? NotFound() : Ok(brand);
        }
        [HttpPost]
        [Authorize(Roles = AppRole.Admin)]
        public async Task<IActionResult> Add([FromForm] ProductModel model, [FromForm] ProductImageModel imageModel)
        {
            try
            {         
                if (imageModel.ProductImage != null)
                {
                    var assetsPath = Path.Combine(webHostEnvironment.ContentRootPath, "Assets");

                    if (!Directory.Exists(assetsPath))
                    {
                        Directory.CreateDirectory(assetsPath);
                    }

                    var filepath = Path.Combine(assetsPath, imageModel.ProductImage.FileName);
                    using (var stream = new FileStream(filepath, FileMode.Create))
                    {
                        await imageModel.ProductImage.CopyToAsync(stream);
                    }
                    model.Image = filepath;
                }

                var newProductId = await repo.AddAsync(model);
                var product = await repo.GetByIdAsync(newProductId);
                return product == null ? NotFound(newProductId) : Ok(product);
            }
            catch
            {
                return BadRequest();
            }
        }
        [HttpPut("{id}")]
        [Authorize(Roles = AppRole.Admin)]
        public async Task<IActionResult> Update(int id, [FromForm] ProductModel model, [FromForm] ProductImageModel imageModel)
        {
            try
            {
                var existingProduct = await repo.GetByIdAsync(id);
                if (existingProduct == null)
                {
                    return NotFound();
                }
                if (imageModel.ProductImage != null)
                {
                    if (!string.IsNullOrEmpty(existingProduct.Image))
                    {
                        var imagePath = Path.Combine(webHostEnvironment.ContentRootPath, "Assets", Path.GetFileName(existingProduct.Image));

                        // Kiểm tra nếu hình ảnh tồn tại và xóa nó
                        if (System.IO.File.Exists(imagePath))
                        {
                            System.IO.File.Delete(imagePath);
                        }
                    }
                    var assetsPath = Path.Combine(webHostEnvironment.ContentRootPath, "Assets");
                    if (!Directory.Exists(assetsPath))
                    {
                        Directory.CreateDirectory(assetsPath);
                    }
                    var filepath = Path.Combine(assetsPath, imageModel.ProductImage.FileName);
                    using (var stream = new FileStream(filepath, FileMode.Create))
                    {
                        await imageModel.ProductImage.CopyToAsync(stream);
                    }
                    model.Image = filepath;
                }
                else
                {
                    model.Image = existingProduct.Image;
                }
                await repo.UpdateAsync(id, model);
                return Ok();
            }
            catch
            {
                return BadRequest();
            }            
        }
        [HttpDelete("{id}")]
        [Authorize(Roles = AppRole.Admin)]
        public async Task<IActionResult> Delete(int id)
        {
            try
            {
                // Truy xuất sản phẩm để lấy đường dẫn hình ảnh
                var product = await repo.GetByIdAsync(id);
                if (product == null)
                {
                    return NotFound();
                }
                // Lấy đường dẫn hình ảnh
                if (!string.IsNullOrEmpty(product.Image))
                {
                    var imagePath = Path.Combine(webHostEnvironment.ContentRootPath, "Assets", Path.GetFileName(product.Image));

                    // Kiểm tra nếu hình ảnh tồn tại và xóa nó
                    if (System.IO.File.Exists(imagePath))
                    {
                        System.IO.File.Delete(imagePath);
                    }
                }
                // Xóa sản phẩm từ cơ sở dữ liệu
                await repo.DeleteAsync(id);
                return Ok();
            }
            catch
            {
                return BadRequest();
            }            
        }
    }
}
