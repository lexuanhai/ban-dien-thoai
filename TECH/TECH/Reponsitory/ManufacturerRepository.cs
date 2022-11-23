using Microsoft.EntityFrameworkCore;
using System;
using TECH.Data.DatabaseEntity;

namespace TECH.Reponsitory
{
    public interface IManufacturerRepository : IRepository<Manufacturer, int>
    {
       
    }

    public class ManufacturerRepository : EFRepository<Manufacturer, int>, IManufacturerRepository
    {
        public ManufacturerRepository(DataBaseEntityContext context) : base(context)
        {
        }
    }
}
