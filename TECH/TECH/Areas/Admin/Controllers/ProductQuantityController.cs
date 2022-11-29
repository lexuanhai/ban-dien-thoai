using Microsoft.AspNetCore.Mvc;
using System.Text.RegularExpressions;
using TECH.Areas.Admin.Models;
using TECH.Areas.Admin.Models.Search;
using TECH.Data.DatabaseEntity;
using TECH.Service;

namespace TECH.Areas.Admin.Controllers
{
    public class ProductQuantityController : BaseController
    {
        private readonly IProductQuantityService _productQuantityService;
        private readonly IProductsService _productsService;
        private readonly IImagesService _imagesService;
        private readonly IColorsService _colorsService;
        private readonly IColorImagesService _colorImagesService;
        private readonly Microsoft.AspNetCore.Hosting.IHostingEnvironment _hostingEnvironment;
        public ProductQuantityController(IProductQuantityService productQuantityService,
            IProductsService productsService,
            IImagesService imagesService,
            IColorImagesService colorImagesService,
             IColorsService colorsService,
        Microsoft.AspNetCore.Hosting.IHostingEnvironment hostingEnvironment)
        {
            _productQuantityService = productQuantityService;
            _productsService = productsService;
            _hostingEnvironment = hostingEnvironment;
            _imagesService = imagesService;
            _colorImagesService = colorImagesService;
            _colorsService = colorsService;
        }
        public IActionResult Index()
        {
            return View();
        }

        public IActionResult AddQuantity(int productId)
        {
            if (productId > 0)
            {
                var data = _productsService.GetByid(productId);
                if (data != null)
                {
                    return View(data);
                }
            }
            return View();
        }
        public JsonResult GetQuantityById(int quantityId)
        {
            if (quantityId > 0)
            {
                var data = _productQuantityService.GetById(quantityId);
                if (data != null && data.product_id.HasValue && data.product_id.Value > 0)
                {                   
                    // get product 
                    var productData = _productsService.GetByid(data.product_id.Value);
                    if (productData != null)
                    {
                        data.Product = productData;
                    }
                    // set color sản phẩm 
                    if (data.color_id.HasValue && data.color_id.Value > 0)
                    {
                        var colorData = _colorsService.GetByid(data.color_id.Value);
                        if (colorData != null)
                        {
                            data.Colors = colorData;
                        }
                    }
                    // hình ảnh sản phẩm
                    var colorImages = _colorImagesService.GetColorImageQuantity(quantityId);
                    if (colorImages != null && colorImages.Count > 0)
                    {
                        var lstImages = _imagesService.GetImageName(colorImages.Select(p=>p.image_id.Value).ToList());

                        if (lstImages != null && lstImages.Count > 0)
                        {
                            data.ImageModelView = lstImages;
                        }
                    }

                    return Json(new
                    {
                        Data = data,
                        Success = true
                    });
                }
            }
            return Json(new
            {
                Success = false
            });
        }
        //public IActionResult UpdateQuantityView(int quantityId)
        //{
        //    if (quantityId > 0)
        //    {
        //        var data = GetQuantityById(quantityId);
        //        if (data != null)
        //        {
        //            return View(data);
        //        }
        //    }
        //    return View();
        //}

        [HttpGet]
        public JsonResult GetProductQuantityForProductId(int productId)
        {
            if (productId > 0)
            {
                var data = _productQuantityService.GetProductQuantity(productId);
                return Json(new
                {
                    Data = data,
                    Success = true
                });
            }
            return Json(new
            {                
                Success = false
            });

        }
        [HttpPost]
        public JsonResult Add(QuantityProductModelView quantities)
        {
            try
            {
                if (quantities == null)
                {
                    return Json(new
                    {
                        success = false
                    });
                }
                int id = _productQuantityService.Add(quantities);
                //_productQuantityService.Save();
                return Json(new
                {
                    success = true,
                    id= id
                });
            }
            catch (Exception ex)
            {
                return Json(new
                {
                    success = false
                });
            }
           
        }
        [HttpPost]
        public JsonResult Update(QuantityProductModelView quantities)
        {
            try
            {
                if (quantities == null)
                {
                    return Json(new
                    {
                        success = false
                    });
                }
                _productQuantityService.Update(quantities);
                _productQuantityService.Save();
                return Json(new
                {
                    success = true
                });
            }
            catch (Exception ex)
            {
                return Json(new
                {
                    success = false
                });
            }
        }
        

        [HttpPost]
        public JsonResult Deleted(List<int> ids)
        {
            if (ids != null && ids.Count() > 0)
            {
                var result = _productQuantityService.Deleted(ids);
                _productQuantityService.Save();
                return Json(new
                {
                    success = result
                });
            }
            return Json(new
            {
                success = false
            });
        }


        [HttpPost]
        public IActionResult UploadImageProduct()
        {
            var files = Request.Form.Files;
            if (files != null && files.Count > 0)
            {
                var imageFolder = $@"\quantity-image\";

                string folder = _hostingEnvironment.WebRootPath + imageFolder;

                if (!Directory.Exists(folder))
                {
                    Directory.CreateDirectory(folder);
                }
                var _lstImageName = new List<string>();
                var quanity_id = Convert.ToInt32(files[0].Name);
                foreach (var itemFile in files)
                {
                    string fileNameFormat = Regex.Replace(itemFile.FileName.ToLower(), @"\s+", "");
                    string filePath = Path.Combine(folder, fileNameFormat);
                    if (!System.IO.File.Exists(filePath))
                    {
                        _lstImageName.Add(fileNameFormat);
                        using (FileStream fs = System.IO.File.Create(filePath))
                        {
                            itemFile.CopyTo(fs);
                            fs.Flush();
                        }
                    }
                }
                if (_lstImageName != null && _lstImageName.Count > 0)
                {
                    var lstImagesIds = _imagesService.Add(_lstImageName);
                    if (lstImagesIds != null && lstImagesIds.Count > 0)
                    {
                        var lstProductImages = lstImagesIds.Select(p => new ColorImagesModelView()
                        {
                            product_quantities_id = quanity_id,
                            image_id = p
                        }).ToList();
                        _colorImagesService.Add(lstProductImages);
                        _colorImagesService.Save();
                    }
                }
            }
            return Json(new
            {
                success = true
            });
        }

        [HttpGet]
        public JsonResult GetAllPaging(ProductQuantityViewModelSearch productQuantityViewModelSearch)
        {
            var data = _productQuantityService.GetAllPaging(productQuantityViewModelSearch);
            foreach (var item in data.Results)
            {
                if (item.product_id.HasValue && item.product_id.Value > 0)
                {
                    var product = _productsService.GetByid(item.product_id.Value);
                    if (product != null)
                    {
                        item.Product = product;
                    }
                }
                if (item.color_id.HasValue && item.color_id.Value > 0)
                {
                    var colors = _colorsService.GetByid(item.color_id.Value);
                    if (colors != null)
                    {
                        item.Colors = colors;
                    }
                }
                item.pricesellstr = item.pricesell.HasValue && item.pricesell.Value > 0 ? item.pricesell.Value.ToString("#,###") : "";
                item.priceimprotstr = item.priceimprot.HasValue && item.priceimprot.Value > 0 ? item.priceimprot.Value.ToString("#,###") : "";

            }
            return Json(new { data = data });
        }

    }
}
