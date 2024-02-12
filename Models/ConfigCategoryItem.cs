using System.Text.Json;

namespace UltimateRemote.Models;

public sealed record ConfigCategoryItem(string Name, JsonValueKind ValueKind);

