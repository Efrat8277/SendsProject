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
        public readonly IDeliveryPersonRepository _deliveryPersonRepository;
        public PackageService(IPackageRepository packageRepository, IDeliveryPersonRepository deliveryPersonRepository)
        {
            _packageRepository = packageRepository;
            _deliveryPersonRepository = deliveryPersonRepository;
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
            var dp=await _deliveryPersonRepository.GetDeliveryPersonByIdAsync(package.DeliveryPersonId);
            eWorkDays dayOfSend=package.SendDate.DayOfWeek switch
            {   DayOfWeek.Sunday => eWorkDays.Sunday,
                DayOfWeek.Monday => eWorkDays.Monday,
                DayOfWeek.Tuesday => eWorkDays.Tuesday,
                DayOfWeek.Wednesday => eWorkDays.Wednesday,
                DayOfWeek.Thursday => eWorkDays.Thursday,
                DayOfWeek.Friday => eWorkDays.Friday,
                DayOfWeek.Saturday => eWorkDays.Saturday,
                _ => throw new ArgumentOutOfRangeException()
            };
            if(!dp.WorkDays.HasFlag(dayOfSend))
            {
                throw new Exception("השליח לא עובד ביום זה!");
            }
            var p=  _packageRepository.PostPackage(package);
            await _packageRepository.SaveAsync();
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
