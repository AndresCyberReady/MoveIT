# MoveIT

A self-hosted moving inventory tracker built with Blazor Server on .NET 10. Track every item in your move — where it's coming from, where it's going, how heavy and bulky it is, and how long it will take to unpack and set up at the new place.

## Features

**Dashboard**
- At-a-glance stats: total items, volume (ft³), estimated weight, and packing progress
- Visual progress bar for packing completion
- High-priority unpacked items flagged in red so nothing critical gets missed
- Items grouped by category

**Inventory**
- Add, edit, and delete items with a modal form
- Track origin room → destination room for each item
- Record dimensions (W × H × D in inches) with automatic cubic feet calculation
- Weight tracking in lbs
- Estimated unpack and setup/deploy time per item (formatted as `1h 30m`)
- Time Overview panel showing total unpack time, setup time, and combined move time across all items
- Priority levels: High, Medium, Low
- Status workflow: Not Packed → Packed → Loaded → Delivered
- Categories: Furniture, Electronics, Appliances, Clothing, Boxes, Fragile, Kitchen, Books, Other
- Free-text notes per item

**Filtering & Search**
- Search by item name or notes
- Filter by category, room, priority, or status

**Persistence**
- Inventory is saved to a local `inventory.json` file — no database required

## Tech Stack

| Layer | Technology |
|---|---|
| Framework | ASP.NET Core / Blazor Server |
| Runtime | .NET 10 |
| UI | Bootstrap 5 (via Blazor template) |
| Storage | JSON file (via `System.Text.Json`) |

## Getting Started

**Prerequisites:** [.NET 10 SDK](https://dotnet.microsoft.com/download)

```bash
git clone https://github.com/your-username/MoveIT.git
cd MoveIT/MoveIT
dotnet run
```

Then open `https://localhost:5001` (or the URL shown in the terminal) in your browser.

The inventory is stored in `MoveIT/inventory.json` and is created automatically on first run.

## Project Structure

```
MoveIT/
├── Components/
│   ├── Pages/
│   │   ├── Home.razor        # Dashboard
│   │   └── Inventory.razor   # Item list, filters, add/edit/delete
│   └── Layout/
│       └── NavMenu.razor     # Sidebar navigation
├── Models/
│   └── MovingItem.cs         # Item model + enums
├── Services/
│   └── InventoryService.cs   # In-memory list + JSON persistence
└── inventory.json            # Auto-generated data file (git-ignored)
```

## Data Model

Each item stores:

| Field | Type | Description |
|---|---|---|
| Name | string | Item label |
| Category | enum | Furniture, Electronics, etc. |
| OriginRoom | string | Where it currently lives |
| DestinationRoom | string | Where it's going |
| Priority | enum | High / Medium / Low |
| Status | enum | NotPacked / Packed / Loaded / Delivered |
| WidthIn / HeightIn / DepthIn | double? | Dimensions in inches |
| WeightLbs | double? | Weight in pounds |
| UnpackMinutes | int? | Estimated minutes to unpack |
| SetupMinutes | int? | Estimated minutes to set up / deploy |
| Notes | string | Free-text notes |

Cubic feet and total time are computed on the fly and not stored.

## License

MIT
