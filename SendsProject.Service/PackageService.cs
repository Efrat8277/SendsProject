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
    }
}
