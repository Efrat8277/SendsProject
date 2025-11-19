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
        public List<Package> GetPackages();
        public Package GetPackageById(int id);
    }
}
