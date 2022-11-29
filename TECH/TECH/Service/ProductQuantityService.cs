
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TECH.Areas.Admin.Models;
using TECH.Areas.Admin.Models.Search;
using TECH.Data.DatabaseEntity;
using TECH.Reponsitory;
using TECH.Utilities;

namespace TECH.Service
{
    public interface IProductQuantityService
    {           
        int Add(QuantityProductModelView view);
        bool Update(QuantityProductModelView view);
        QuantityProductModelView GetById(int quantityId);
        bool Deleted(List<int> ids);
        List<QuantityProductModelView> GetProductQuantity(int productId);
        void Save();
        PagedResult<QuantityProductModelView> GetAllPaging(ProductQuantityViewModelSearch productQuantityViewModelSearch);
    }

    public class ProductQuantityService : IProductQuantityService
    {
        private readonly IProductQuantityRepository _productQuantityRepository;
        private IUnitOfWork _unitOfWork;
        public ProductQuantityService(IProductQuantityRepository productQuantityRepository,
            IUnitOfWork unitOfWork)
        {
            _productQuantityRepository = productQuantityRepository;
            _unitOfWork = unitOfWork;
        }
        public QuantityProductModelView GetById(int quantityId)
        {
            var quantity = new QuantityProductModelView();
            if (quantityId > 0)
            {
                var quantityServer = _productQuantityRepository.FindAll(q => q.id == quantityId).Select(q=>new QuantityProductModelView()
                {
                    id = q.id,
                    product_id = q.product_id,
                    color_id = q.color_id,
                    totalimport = q.totalimport,
                    priceimprot = q.priceimprot,
                    pricesell = q.pricesell,
                    totalsell = q.totalsell,
                    totalinventory = q.totalinventory,
                    capacity = q.capacity
                }).FirstOrDefault();
                if (quantityServer != null)
                    quantity = quantityServer;
            }
            return quantity;

        }
        public int Add(QuantityProductModelView view)
        {
            try
            {
                if (view != null)
                {
                    var image = new ProductQuantity
                    {
                        product_id = view.product_id,
                        color_id = view.color_id,
                        totalimport = view.totalimport,
                        priceimprot = view.priceimprot,
                        pricesell = view.pricesell,                       
                        capacity = view.capacity
                    };
                    _productQuantityRepository.Add(image);
                    Save();
                    return image.id;
                }
            }
            catch (Exception ex)
            {
            }
            return 0;

        }
        public List<QuantityProductModelView> GetProductQuantity(int productId)
        {
            if (productId > 0)
            {
                var productQuantity = _productQuantityRepository.FindAll(p => p.product_id == productId).Select(p => new QuantityProductModelView
                {
                    //Id = p.id,
                    ProductId = p.product_id,
                    ColorId = p.color_id,
                    AppSizeId = p.size_id,
                    TotalImported = p.totalimport,
                }).ToList();
                return productQuantity;
            }
            return null;
        }
        public void Save()
        {
            _unitOfWork.Commit();
        }
        public bool Update(QuantityProductModelView view)
        {
            try
            {
                if (view != null)
                {
                    var dataServer = _productQuantityRepository.FindById(view.id);
                    if (dataServer != null)
                    {
                        //dataServer.product_id = view.ProductId;
                        dataServer.color_id = view.color_id;
                        dataServer.totalimport = view.totalimport;
                        dataServer.capacity = view.capacity;
                        dataServer.priceimprot = view.priceimprot;
                        dataServer.pricesell = view.pricesell;                        
                        _productQuantityRepository.Update(dataServer);

                    }
                    return true;
                }
               
            }
            catch (Exception ex)
            {
                return false;
            }

            return false;
        }

        public bool Deleted(List<int> ids)
        {
            try
            {
                if (ids != null && ids.Count() > 0)
                {
                    foreach (var item in ids)
                    {
                        var dataServer = _productQuantityRepository.FindById(item);
                        if (dataServer != null)
                        {
                            _productQuantityRepository.Remove(dataServer);
                        }                        
                    }
                    return true;
                }
                return false;

            }
            catch (Exception ex)
            {

                throw;
            }

            return false;
        }

        public PagedResult<QuantityProductModelView> GetAllPaging(ProductQuantityViewModelSearch ProductModelViewSearch)
        {
            try
            {
                var query = _productQuantityRepository.FindAll();

                if (ProductModelViewSearch.productId > 0)
                {
                    query = query.Where(c => c.product_id == ProductModelViewSearch.productId);
                }

                int totalRow = query.Count();
                query = query.Skip((ProductModelViewSearch.PageIndex - 1) * ProductModelViewSearch.PageSize).Take(ProductModelViewSearch.PageSize);
                var data = query.Select(p => new QuantityProductModelView()
                {
                    id = p.id,
                    product_id = p.product_id,
                    color_id = p.color_id,
                    totalimport = p.totalimport,
                    status = p.status,
                    priceimprot = p.priceimprot,
                    pricesell = p.pricesell,
                    totalsell = p.totalsell,
                    totalinventory = p.totalinventory,     
                    capacity =p.capacity,
                }).ToList();

                var pagingData = new PagedResult<QuantityProductModelView>
                {
                    Results = data,
                    CurrentPage = ProductModelViewSearch.PageIndex,
                    PageSize = ProductModelViewSearch.PageSize,
                    RowCount = totalRow,
                };
                return pagingData;
            }
            catch (Exception ex)
            {
                throw;
            }
        }

    }
}
