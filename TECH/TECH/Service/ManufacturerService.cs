
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using System.Xml.Linq;
using TECH.Areas.Admin.Models;
using TECH.Areas.Admin.Models.Search;
using TECH.Data.DatabaseEntity;
using TECH.Reponsitory;
using TECH.Utilities;

namespace TECH.Service
{
    public interface IManufacturerService
    {
        PagedResult<ManufacturerModelView> GetAllPaging(CategoryViewModelSearch ManufacturerModelViewSearch);
        ManufacturerModelView GetByid(int id);
        void Add(ManufacturerModelView view);
        bool Update(ManufacturerModelView view);
        bool Deleted(int id);
        void Save();
        bool UpdateStatus(int id, int status);
        bool IsCategoryNameExist(string name);
        List<ManufacturerModelView> GetAll();
        int GetCount();
        List<ManufacturerModelView> GetAllMenu();
    }

    public class ManufacturerService : IManufacturerService
    {
        private readonly IManufacturerRepository _manufacturerRepository;
        private IUnitOfWork _unitOfWork;
        public ManufacturerService(IManufacturerRepository manufacturerRepository,
            IUnitOfWork unitOfWork)
        {
            _manufacturerRepository = manufacturerRepository;
            _unitOfWork = unitOfWork;
        }
        public List<ManufacturerModelView> GetAll()
        {
            var data = _manufacturerRepository.FindAll().Select(c=>new ManufacturerModelView()
            {
                id = c.id,
                name = c.name
            }).ToList();

            return data;
        }

        public List<ManufacturerModelView> GetAllMenu()
        {
            var data = _manufacturerRepository.FindAll(c=>c.status == 0).Select(c => new ManufacturerModelView()
            {
                id = c.id,
                name = c.name
            }).ToList();

            return data;
        }

        public bool IsCategoryNameExist(string name)
        {
            var data = _manufacturerRepository.FindAll().Any(p => p.name == name);
            return data;
        }

        public ManufacturerModelView GetByid(int id)
        {
            var data = _manufacturerRepository.FindAll(p => p.id == id).FirstOrDefault();
            if (data != null)
            {                
                var model = new ManufacturerModelView()
                {
                    id = data.id,
                    name = data.name,
                    status = data.status,
                    created_at = data.created_at,
                    updated_at = data.updated_at
                };
                return model;
            }
            return null;
        }
        public int GetCount()
        {
            int count = 0;
            count = _manufacturerRepository.FindAll().Count();
            return count;
        }
        public void Add(ManufacturerModelView view)
        {
            try
            {
                if (view != null)
                {
                    var category = new Manufacturer
                    {                      
                        name = view.name,
                        address = view.address,
                        email = view.email,
                        phone = view.phone,
                        status = 0,
                        created_at = DateTime.Now,
                    };
                    _manufacturerRepository.Add(category);                  
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
        public bool Update(ManufacturerModelView view)
        {
            try
            {
                var dataServer = _manufacturerRepository.FindById(view.id);
                if (dataServer != null)
                {
                    dataServer.name = view.name;
                    dataServer.address = view.address;
                    dataServer.email = view.email;
                    dataServer.phone = view.phone;
                    dataServer.updated_at = DateTime.Now;                    
                    _manufacturerRepository.Update(dataServer);                                        
                    return true;
                }
            }
            catch (Exception ex)
            {
                return false;
            }

            return false;
        }

      public  bool UpdateStatus(int id, int status)
        {
            try
            {
                var dataServer = _manufacturerRepository.FindById(id);
                if (dataServer != null)
                {
                    dataServer.status = status;
                    _manufacturerRepository.Update(dataServer);
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
                var dataServer = _manufacturerRepository.FindById(id);
                if (dataServer != null)
                {
                    _manufacturerRepository.Remove(dataServer);
                    return true;
                }
            }
            catch (Exception ex)
            {

                throw;
            }

            return false;
        }
        public PagedResult<ManufacturerModelView> GetAllPaging(CategoryViewModelSearch ManufacturerModelViewSearch)
        {
            try
            {
                var query = _manufacturerRepository.FindAll();               
                
                if (!string.IsNullOrEmpty(ManufacturerModelViewSearch.name))
                {
                    query = query.Where(c => c.name.ToLower().Trim().Contains(ManufacturerModelViewSearch.name.ToLower().Trim()));
                }

                int totalRow = query.Count();
                query = query.Skip((ManufacturerModelViewSearch.PageIndex - 1) * ManufacturerModelViewSearch.PageSize).Take(ManufacturerModelViewSearch.PageSize);
                var data = query.Select(c => new ManufacturerModelView()
                {
                    name = (!string.IsNullOrEmpty(c.name) ? c.name : ""),
                    id = c.id,              
                    address = c.address,
                    email = c.email,
                    phone = c.phone,
                    status = c.status,
                    created_at = c.created_at,
                    updated_at = c.updated_at,
                }).ToList();
              
                var pagingData = new PagedResult<ManufacturerModelView>
                {
                    Results = data,
                    CurrentPage = ManufacturerModelViewSearch.PageIndex,
                    PageSize = ManufacturerModelViewSearch.PageSize,
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
