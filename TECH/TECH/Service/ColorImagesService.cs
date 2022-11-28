
using Microsoft.CodeAnalysis;
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
    public interface IColorImagesService
    {           
        void Add(List<ColorImagesModelView> view);
        bool Update(List<ColorImagesModelView> view);
        List<int> GetColorImages(int productid);
        List<ColorImagesModelView> GetColorImageQuantity(int product_quantities_id);
        bool Deleted(int id);
        void Remove(List<int> ids);
        void Save();
    }

    public class ColorImagesService : IColorImagesService
    {
        private readonly IColorImagesRepository _colorImagesRepository;
        private IUnitOfWork _unitOfWork;
        public ColorImagesService(IColorImagesRepository colorImagesRepository,
            IUnitOfWork unitOfWork)
        {
            _colorImagesRepository = colorImagesRepository;
            _unitOfWork = unitOfWork;
        }
        public List<int> GetColorImages(int productid)
        {
            int imageid = 0;
            if (productid > 0)
            {
                var productimage = _colorImagesRepository.FindAll(p => p.product_quantities_id == productid).Select(p=>p.image_id.Value).ToList();
                if (productimage != null)
                    return productimage;
            }
            return null;
        }
        public List<ColorImagesModelView> GetColorImageQuantity(int product_quantities_id)
        {
            if (product_quantities_id > 0)
            {
                var data = _colorImagesRepository.FindAll(p => p.product_quantities_id == product_quantities_id).Select(p => new ColorImagesModelView()
                {
                    id = p.id,
                    image_id = p.image_id,
                    product_quantities_id = p.product_quantities_id
                }).ToList();
                if (data != null && data.Count > 0)
                    return data;
            }
            return null;
        }
        public void Add(List<ColorImagesModelView> view)
        {
            try
            {
                if (view != null && view.Count > 0)
                {
                    foreach (var item in view)
                    {
                        var image = new ColorImages
                        {
                            product_quantities_id = item.product_quantities_id,
                            image_id = item.image_id
                        };
                        _colorImagesRepository.Add(image);
                    }
                                
                }
            }
            catch (Exception ex)
            {
            }

        }
        public void Save()
        {
            _unitOfWork.Commit();
        }
        public bool Update(List<ColorImagesModelView> view)
        {
            try
            {
                if (view != null && view.Count > 0)
                {
                    foreach (var item in view)
                    {
                        var dataServer = _colorImagesRepository.FindById(item.id);
                        if (dataServer != null)
                        {
                            dataServer.product_quantities_id = item.product_quantities_id;
                            dataServer.image_id = item.image_id;
                            _colorImagesRepository.Update(dataServer);
                           
                        }
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

        public bool Deleted(int id)
        {
            try
            {
                var dataServer = _colorImagesRepository.FindById(id);
                if (dataServer != null)
                {
                    _colorImagesRepository.Remove(dataServer);
                    return true;
                }
            }
            catch (Exception ex)
            {

                throw;
            }

            return false;
        }
        public void Remove(List<int> ids)
        {
            try
            {
                var dataServer = _colorImagesRepository.FindAll(p=> ids.Contains(p.image_id.Value)).ToList();
                if (dataServer != null && dataServer.Count > 0)
                {
                    foreach (var item in dataServer)
                    {
                        _colorImagesRepository.Remove(item.id);
                    }                   
                }
            }
            catch (Exception ex)
            {

                throw;
            }
        }
    }
}
