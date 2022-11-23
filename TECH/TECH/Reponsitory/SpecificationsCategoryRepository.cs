using Microsoft.EntityFrameworkCore;
using System;
using TECH.Data.DatabaseEntity;

namespace TECH.Reponsitory
{
    public interface ISpecificationsCategoryRepository : IRepository<SpecificationsCategory, int>
    {
       
    }

    public class SpecificationsCategoryRepository : EFRepository<SpecificationsCategory, int>, ISpecificationsCategoryRepository
    {
        public SpecificationsCategoryRepository(DataBaseEntityContext context) : base(context)
        {
        }
    }
}
