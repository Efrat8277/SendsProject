using SendsProject.Core.Models.Classes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SendsProject.Core.Services
{
    public interface IPackageService
    {
        public Task<List<Package>> GetPackagesAsync();
        public Task<Package> GetPackageByIdAsync(int id);
        public Task<Package> PostPackageAsync(Package package);
        public Task PutPackageAsync(Package package);
        public Task DeletePackageAsync(int id);
    }
}
