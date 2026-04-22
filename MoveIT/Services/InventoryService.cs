using System.Text.Json;
using Microsoft.AspNetCore.Hosting;
using MoveIT.Models;

namespace MoveIT.Services;

public class InventoryService
{
    private readonly string _filePath;
    private List<MovingItem> _items;
    private static readonly JsonSerializerOptions _jsonOptions = new() { WriteIndented = true };

    public InventoryService(IWebHostEnvironment env)
    {
        _filePath = Path.Combine(env.ContentRootPath, "inventory.json");
        _items = Load();
    }

    public IReadOnlyList<MovingItem> Items => _items.AsReadOnly();

    public double TotalCubicFeet => _items.Sum(x => x.CubicFeet ?? 0);
    public double TotalWeight => _items.Sum(x => x.WeightLbs ?? 0);
    public int TotalUnpackMinutes => _items.Sum(x => x.UnpackMinutes ?? 0);
    public int TotalSetupMinutes => _items.Sum(x => x.SetupMinutes ?? 0);

    public void Add(MovingItem item)
    {
        _items.Add(item);
        Save();
    }

    public void Update(MovingItem updated)
    {
        var idx = _items.FindIndex(x => x.Id == updated.Id);
        if (idx >= 0)
        {
            _items[idx] = updated;
            Save();
        }
    }

    public void Remove(Guid id)
    {
        _items.RemoveAll(x => x.Id == id);
        Save();
    }

    private List<MovingItem> Load()
    {
        if (!File.Exists(_filePath)) return [];
        try
        {
            var json = File.ReadAllText(_filePath);
            return JsonSerializer.Deserialize<List<MovingItem>>(json, _jsonOptions) ?? [];
        }
        catch
        {
            return [];
        }
    }

    private void Save() =>
        File.WriteAllText(_filePath, JsonSerializer.Serialize(_items, _jsonOptions));
}
