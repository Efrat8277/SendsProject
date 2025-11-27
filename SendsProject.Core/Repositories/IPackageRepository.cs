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
        public List<Package> GetPackages();
        public Package GetPackageById(int id);
        public Package PostPackage(Package package);
        public void PutPackage(Package package);
        public void DeletePackage(int id);

    }
}
