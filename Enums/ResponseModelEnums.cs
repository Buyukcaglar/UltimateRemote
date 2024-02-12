using System.Text.Json.Serialization;

namespace UltimateRemote.Enums;

[JsonConverter(typeof(JsonStringEnumConverter<Status>))]
public enum Status { [StringValue("Disabled")] Disabled = 0, [StringValue("Enabled")] Enabled = 1 }

[JsonConverter(typeof(JsonStringEnumConverter<YesNoBool>))]
public enum YesNoBool { [StringValue("No")] No = 0, [StringValue("Yes")]Yes = 1 }

