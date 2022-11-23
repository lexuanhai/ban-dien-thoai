using Microsoft.EntityFrameworkCore;
using System;
using TECH.Data.DatabaseEntity;

namespace TECH.Reponsitory
{
    public interface IColorImagesRepository : IRepository<ColorImages, int>
    {
       
    }

    public class ColorImagesRepository : EFRepository<ColorImages, int>, IColorImagesRepository
    {
        public ColorImagesRepository(DataBaseEntityContext context) : base(context)
        {
        }
    }
}
