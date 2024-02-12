namespace UltimateRemote.Enums;
public enum ValidationState
{
    Neutral = 0,
    [StringValue("is-valid|valid-feedback")]
    Valid = 1,
    [StringValue("is-invalid|invalid-feedback")]
    Invalid = 2
}
