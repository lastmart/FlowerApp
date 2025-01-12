using System.Linq.Expressions;
using AutoMapper;
using FlowerApp.Data;
using FlowerApp.Data.DbModels.Flowers;
using FlowerApp.Domain.Models;
using FlowerApp.Domain.Models.FlowerModels;
using FlowerApp.DTOs.Common;
using Microsoft.EntityFrameworkCore;
using AppFlower = FlowerApp.Domain.Models.FlowerModels.Flower;
using DbFlower = FlowerApp.Data.DbModels.Flowers.Flower;

namespace FlowerApp.Service.Storages;

public class FlowersStorage : IFlowersStorage
{
    private readonly FlowerAppContext flowerAppContext;
    private readonly IMapper mapper;

    public FlowersStorage(FlowerAppContext flowerAppContext, IMapper mapper)
    {
        this.flowerAppContext = flowerAppContext;
        this.mapper = mapper;
    }

    public async Task<AppFlower?> Get(int id)
    {
        var dbFlower = await flowerAppContext.Flowers.FirstOrDefaultAsync(f => f.Id == id);
        return mapper.Map<AppFlower>(dbFlower);
    }

    public async Task<IList<AppFlower>> Get(int[] ids)
    {
        return await flowerAppContext.Flowers
            .Where(f => ids.Contains(f.Id))
            .Select(dbFlower => mapper.Map<AppFlower>(dbFlower))
            .ToListAsync();
    }

    public async Task<bool> Create(AppFlower model)
    {
        await using var transaction = await flowerAppContext.Database.BeginTransactionAsync();

        try
        {
            await flowerAppContext.Flowers.AddAsync(mapper.Map<DbFlower>(model));
            var result = await flowerAppContext.SaveChangesAsync() > 0;
            await transaction.CommitAsync();
            return result;
        }
        catch
        {
            await transaction.RollbackAsync();
            return false;
        }
    }

    public async Task<bool> Update(AppFlower model)
    {
        await using var transaction = await flowerAppContext.Database.BeginTransactionAsync();
        try
        {
            flowerAppContext.Flowers.Update(mapper.Map<DbFlower>(model));
            var result = await flowerAppContext.SaveChangesAsync() > 0;
            await transaction.CommitAsync();
            return result;
        }
        catch
        {
            await transaction.RollbackAsync();
            return false;
        }
    }

    public async Task<bool> Delete(int id)
    {
        await using var transaction = await flowerAppContext.Database.BeginTransactionAsync();
        try
        {
            var flower = await flowerAppContext.Flowers.FindAsync(id);
            if (flower == null)
                return true;
            flowerAppContext.Flowers.Remove(flower);
            var result = await flowerAppContext.SaveChangesAsync() > 0;
            await transaction.CommitAsync();
            return result;
        }
        catch
        {
            await transaction.RollbackAsync();
            return false;
        }
    }

    public async Task<List<AppFlower>> Get(string name)
    {
        return await flowerAppContext.Flowers
            .Where(f => f.Name.StartsWith(name) || f.ScientificName.StartsWith(name))
            .Select(f => mapper.Map<AppFlower>(f))
            .ToListAsync();
    }

    public async Task<GetFlowerResponse> Get(
        string? searchString,
        Pagination pagination,
        FlowerFilterParams? filterParams = null,
        FlowerSortParams? sortParams = null
    )
    {
        var flowers = flowerAppContext.Flowers.AsQueryable();
        if (searchString is not null)
        {
            searchString = searchString.ToLower();
            flowers = flowers.Where(f => f.Name.StartsWith(searchString) || f.ScientificName.StartsWith(searchString));
        }

        if (sortParams != null)
            flowers = SortFlowers(flowers, sortParams);

        if (filterParams != null)
            flowers = FilterFlowers(flowers, filterParams);

        var result = await flowers
            .Skip(pagination.Skip)
            .Take(pagination.Take)
            .Select(dbFlower => mapper.Map<AppFlower>(dbFlower))
            .ToListAsync();

        return new GetFlowerResponse(result.Count, result);
    }

    private static IQueryable<DbFlower> FilterFlowers(IQueryable<DbFlower> flowers, FlowerFilterParams filterParams)
    {
        if (filterParams.WateringFrequency != WateringFrequency.Any)
            flowers = flowers.Where(flower => (filterParams.WateringFrequency & flower.WateringFrequency) == filterParams.WateringFrequency);

        if (filterParams.Illumination != Illumination.Any)
            flowers = flowers.Where(flower => (filterParams.Illumination & flower.Illumination) == filterParams.Illumination);

        if (filterParams.ToxicCategories != ToxicCategory.Any)
            flowers = flowers.Where(flower => (filterParams.ToxicCategories & flower.ToxicCategory) == filterParams.ToxicCategories);

        if (filterParams.Soil != Soil.Any)
            flowers = flowers.Where(flower => (filterParams.Soil & flower.Soil) == filterParams.Soil);

        return flowers;
    }

    private static IQueryable<DbFlower> SortFlowers(IQueryable<DbFlower> flowers, FlowerSortParams sortParams)
    {
        var propertySelector = GetPropertySelector(sortParams.SortField);

        return sortParams.SortDirection switch
        {
            SortDirection.Descending => flowers.OrderByDescending(propertySelector),
            SortDirection.Ascending => flowers.OrderBy(propertySelector),
            _ => throw new ArgumentOutOfRangeException($"Invalid sort direction: {sortParams.SortDirection}")
        };
    }

    private static Expression<Func<DbFlower, object>> GetPropertySelector(FlowerSortField field)
    {
        return field switch
        {
            FlowerSortField.Name => flower => flower.Name,
            FlowerSortField.ScientificName => flower => flower.ScientificName,
            FlowerSortField.Size => flower => flower.Size,
            _ => throw new ArgumentOutOfRangeException($"Invalid field name to sort: {field}")
        };
    }
}