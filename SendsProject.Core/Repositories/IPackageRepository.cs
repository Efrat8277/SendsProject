using SendsProject.Core.Models.Classes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SendsProject.Core.Repositories
{
    public interface IPackageRepository
    {
        public Task<List<Package>> GetPackagesAsync();
        public Task<Package> GetPackageByIdAsync(int id);
        public Package PostPackage(Package package);
        public Task PutPackageAsync(Package package);
        public Task DeletePackageAsync(int id);
        public Task SaveAsync();


    }
}
