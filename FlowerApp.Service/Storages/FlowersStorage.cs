using System.Linq.Expressions;
using AutoMapper;
using FlowerApp.Data;
using FlowerApp.Data.DbModels.Flowers;
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
        FlowerSortOptions? sortBy = null
    )
    {
        var flowers = flowerAppContext.Flowers.AsQueryable();
        if (searchString is not null)
        {
            searchString = searchString.ToLower();
            flowers = flowers.Where(f => f.Name.StartsWith(searchString) || f.ScientificName.StartsWith(searchString));
        }

        if (sortBy != null && sortBy.SortOptions.Any())
            flowers = SortFlowers(flowers, sortBy);

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
        if (filterParams.WateringFrequency is not null)
            flowers = flowers.Where(f => filterParams.WateringFrequency.Contains(f.WateringFrequency));

        if (filterParams.Illumination is not null)
            flowers = flowers.Where(f => filterParams.Illumination.Contains(f.Illumination));

        if (filterParams.ToxicCategories != null && filterParams.ToxicCategories.Any())
        {
            var toxicCategory =
                filterParams.ToxicCategories.Aggregate(ToxicCategory.None, (current, category) => current | category);

            flowers = toxicCategory == ToxicCategory.None
                ? flowers.Where(f => f.ToxicCategory == ToxicCategory.None)
                : flowers.Where(f => (f.ToxicCategory & toxicCategory) != 0);
        }

        return flowers;
    }

    private static IQueryable<DbFlower> SortFlowers(IQueryable<DbFlower> flowers, FlowerSortOptions sortOptions)
    {
        IOrderedQueryable<DbFlower>? orderedQuery = null;

        foreach (var sortOption in sortOptions.SortOptions)
        {
            var propertySelector = GetPropertySelector(sortOption.FlowerSortField);

            if (orderedQuery == null)
                orderedQuery = sortOption.IsDescending
                    ? flowers.OrderByDescending(propertySelector)
                    : flowers.OrderBy(propertySelector);
            else
                orderedQuery = sortOption.IsDescending
                    ? orderedQuery.ThenByDescending(propertySelector)
                    : orderedQuery.ThenBy(propertySelector);
        }

        return orderedQuery ?? flowers;
    }

    private static Expression<Func<DbFlower, object>> GetPropertySelector(FlowerSortField flowerSort)
    {
        return flowerSort switch
        {
            FlowerSortField.Name => f => f.Name,
            FlowerSortField.ScientificName => f => f.ScientificName,
            FlowerSortField.WateringFrequency => f => f.WateringFrequency,
            FlowerSortField.IlluminationInSuites => f => f.Illumination,
            FlowerSortField.IsToxic => f => f.ToxicCategory != ToxicCategory.None,
            _ => throw new ArgumentException("Invalid sort option.")
        };
    }
}