using Microsoft.EntityFrameworkCore;
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
        public PackageRepository(DataContext context)
        {
            _context = context;
        }
        public async Task<List<Package>> GetPackagesAsync()
        {
            return await _context.Packages.Include(p => p.Recipient).ToListAsync();
        }
        public async Task<Package> GetPackageByIdAsync(int id)
        {
            return await _context.Packages.FirstOrDefaultAsync(p => p.Id == id);
        }

        public Package PostPackage(Package package)
        {
            _context.Packages.Add(package);
            return package;
        }
        public async Task PutPackageAsync(Package package)
        {
            var pack=await GetPackageByIdAsync(package.Id);
            pack.SendDate = package.SendDate;
            pack.IsSentToRecipient = package.IsSentToRecipient;
            pack.SenderName = package.SenderName;
            pack.Weight = package.Weight;

            //var index = package.Id;
            //_context.Packages[index].SenderName=package.SenderName;
            //_context.Packages[index].SendDate=package.SendDate;
            //_context.Packages[index].IsSentToRecipient=package.IsSentToRecipient;
            //_context.Packages[index].Weight=package.Weight;
        }
        public async Task DeletePackageAsync(int id)
        {
            var package= await GetPackageByIdAsync(id);
            _context.Packages.Remove(package);
        }
         public async Task SaveAsync()
        {
           await _context.SaveChangesAsync();
        }
    }
}
