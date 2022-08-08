﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using OnlineGameStore.BLL.Entities;
using OnlineGameStore.BLL.Repositories.GameStore;
using OnlineGameStore.DAL.Data;

namespace OnlineGameStore.DAL.Repositories.GameStore
{
    public class PlatformTypeRepository : GenericRepository<PlatformType>, IPlatformTypeRepository
    {
        public PlatformTypeRepository(StoreDbContext context) : base(context)
        {
        }

        public async Task<IEnumerable<string>> GetIdsByTypes(IEnumerable<string> types)
        {
            var platformTypes = await GetMany(s => types.Contains(s.Type));

            var platformTypesIds = platformTypes.Select(s => s.Id.ToString());

            return platformTypesIds;
        }

        public async Task<PlatformType> GetByType(string type, bool includeDeleted = false, params string[] includeProperties)
        {
            Expression<Func<PlatformType, bool>> predicate = pt => pt.Type == type;

            return await GetSingle(predicate, includeDeleted, includeProperties);
        }
    }
}