using Microsoft.EntityFrameworkCore;
using System;
using TECH.Data.DatabaseEntity;

namespace TECH.Reponsitory
{
    public interface ISpecificationsRepository : IRepository<Specifications, int>
    {
       
    }

    public class SpecificationsRepository : EFRepository<Specifications, int>, ISpecificationsRepository
    {
        public SpecificationsRepository(DataBaseEntityContext context) : base(context)
        {
        }
    }
}
