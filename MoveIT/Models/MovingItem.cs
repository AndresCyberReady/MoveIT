using System.Text.Json.Serialization;

namespace MoveIT.Models;

public enum Priority { High, Medium, Low }
public enum ItemStatus { NotPacked, Packed, Loaded, Delivered }
public enum ItemCategory { Furniture, Electronics, Appliances, Clothing, Boxes, Fragile, Kitchen, Books, Other }

public class MovingItem
{
    public Guid Id { get; set; } = Guid.NewGuid();
    public string Name { get; set; } = "";
    public string OriginRoom { get; set; } = "";
    public string DestinationRoom { get; set; } = "";
    public ItemCategory Category { get; set; } = ItemCategory.Other;
    public Priority Priority { get; set; } = Priority.Medium;
    public ItemStatus Status { get; set; } = ItemStatus.NotPacked;

    // Dimensions in inches
    public double? WidthIn { get; set; }
    public double? HeightIn { get; set; }
    public double? DepthIn { get; set; }

    // Weight in lbs
    public double? WeightLbs { get; set; }

    public string Notes { get; set; } = "";

    // Estimated time in minutes
    public int? UnpackMinutes { get; set; }
    public int? SetupMinutes { get; set; }

    [JsonIgnore]
    public int? TotalMinutes => (UnpackMinutes.HasValue || SetupMinutes.HasValue)
        ? (UnpackMinutes ?? 0) + (SetupMinutes ?? 0)
        : null;

    [JsonIgnore]
    public double? CubicFeet => (WidthIn.HasValue && HeightIn.HasValue && DepthIn.HasValue)
        ? Math.Round(WidthIn.Value * HeightIn.Value * DepthIn.Value / 1728.0, 2)
        : null;

    [JsonIgnore]
    public string DimensionsDisplay => (WidthIn.HasValue && HeightIn.HasValue && DepthIn.HasValue)
        ? $"{WidthIn}\" × {HeightIn}\" × {DepthIn}\""
        : "—";
}
