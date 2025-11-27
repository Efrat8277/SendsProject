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
        public List<Package> GetPackages()
        {
            return _packageRepository.GetPackages();
        }
        public Package GetPackageById(int id)
        {
            return _packageRepository.GetPackageById(id);
        }

        public Package PostPackage(Package package)
        {
            return _packageRepository.PostPackage(package);
        }

        public void PutPackage(Package package)
        {

            _packageRepository.PutPackage(package);
        }

        public void DeletePackage(int id)
        {
            _packageRepository.DeletePackage(id);
        }
    }
}
