using SendsProject.Core.Models.Classes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SendsProject.Data.Repositories
{
    public class PackageRepository
    {
        private readonly DataContext _context;
        public List<Package> GetPackages()
        {
            return _context.Packages;
        }
        public Package GetPackageById(int id)
        {
            return _context.Packages.Find(p => p.Id == id);
        }
    }
}
