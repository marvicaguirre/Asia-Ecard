using System.Collections.Generic;
using System.Linq;
using Wizardsgroup.Core.Interface;
using Wizardsgroup.Domain.Models;

namespace Wizardsgroup.Service
{
    public class GridSettingService
    {        
        private readonly IRepository<GridSetting> _gridSettingRepository;

        public GridSettingService(IUnitOfWork unitOfWork)
        {
            _gridSettingRepository = unitOfWork.Repository<GridSetting>();
        }

        public IQueryable<dynamic> GetAllGridNames()
        {
            var all = _gridSettingRepository.GetAll;
            var y = from c in all
                    group c by c.GridName
                        into x
                        select new { GridName = x.Key, GridValue = x.Key };
            return y;
        }

        public List<GridSetting> GetAllSettingsForGrid(string gridName)
        {
            return _gridSettingRepository.GetAll
                .Where(o=>o.GridName == gridName)
                .OrderBy(o=>o.SortOrder)
                .ToList();
        }
    }
}
