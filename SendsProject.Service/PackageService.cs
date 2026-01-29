using SendsProject.Core.Models.Classes;
using SendsProject.Core.Repositories;
using SendsProject.Core.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SendsProject.Service
{
    public class PackageService : IPackageService
    {
        public readonly IPackageRepository _packageRepository;
        public PackageService(IPackageRepository packageRepository)
        {
            _packageRepository = packageRepository;
        }
        public async Task<List<Package>> GetPackagesAsync()
        {
            return await _packageRepository.GetPackagesAsync();
        }
        public async Task<Package> GetPackageByIdAsync(int id)
        {
            return await _packageRepository.GetPackageByIdAsync(id);
        }

        public async Task<Package> PostPackageAsync(Package package)
        {
           await _packageRepository.SaveAsync();

            var p= _packageRepository.PostPackage(package);
            return p;
        }

        public async Task PutPackageAsync(Package package)
        {

           await _packageRepository.PutPackageAsync(package);
           await _packageRepository.SaveAsync();

        }

        public async Task DeletePackageAsync(int id)
        {
           await _packageRepository.DeletePackageAsync(id);
           await _packageRepository.SaveAsync();

        }

    }
}
