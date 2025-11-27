using SendsProject.Core.Models.Classes;
using SendsProject.Core.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SendsProject.Data.Repositories
{
    public class PackageRepository : IPackageRepository
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

        public Package PostPackage(Package package)
        {
            _context.Packages.Add(package);
            return package;
        }
        public void PutPackage(Package package)
        {
            var index = package.Id;
            _context.Packages[index].SenderName=package.SenderName;
            _context.Packages[index].SendDate=package.SendDate;
            _context.Packages[index].IsSentToRecipient=package.IsSentToRecipient;
            _context.Packages[index].Weight=package.Weight;
        }
        public void DeletePackage(int id)
        {
            var index = _context.Packages.FindIndex(d => d.Id == id);
            _context.Packages.RemoveAt(index);
        }
    }
}
