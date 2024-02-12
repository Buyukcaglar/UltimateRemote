namespace UltimateRemote.Enums;
public enum BgColorStyle
{
    None = 0,
    [StringValue("bg-light")]
    Light = 1,
    [StringValue("bg-dark")]
    Dark = 2,
    [StringValue("bg-primary")]
    Primary = 3,
    [StringValue("bg-secondary")]
    Secondary = 4,
    [StringValue("bg-success")]
    Success = 5,
    [StringValue("bg-warning")]
    Warning = 6,
    [StringValue("bg-danger")]
    Danger = 7,
    [StringValue("bg-info")]
    Info = 8,
    [StringValue("bg-pink")]
    Pink = 9,
    [StringValue("bg-purple")]
    Purple = 10,
    [StringValue("bg-indigo")]
    Indigo = 11,
    [StringValue("bg-teal")]
    Teal = 12,
    [StringValue("bg-yellow text-black")] // .text-black eklenmeli
    Yellow = 13,
    [StringValue("text-body")]
    Transparent = 99,
}

public enum PhosphorIcon
{
    None = 0,
    [StringValue("ph-gift")]
    GiftBox = 1,
    [StringValue("ph-archive-box")]
    ArchiveBox = 2,
    [StringValue("ph-caret-down")]
    CaretDown = 3,
    [StringValue("ph-caret-up")]
    CaretUp = 4,
    [StringValue("ph-arrows-clockwise")]
    Refresh = 5,
    [StringValue("ph-x")]
    IconX = 6,
    [StringValue("ph-check-circle")]
    CheckCircle = 7,
    [StringValue("ph-info")]
    InfoCircle = 8,
    [StringValue("ph-warning")]
    WarningTriangle = 9,
    [StringValue("ph-x-square")]
    XSquare = 10,
    [StringValue("ph-warning-octagon")]
    ErrorOctagon = 11,
    [StringValue("ph-list-plus")]
    ListPLus = 12,
    [StringValue("ph-pen")]
    Pen = 13,
    [StringValue("ph-eyeglasses")]
    EyeGlasses = 14,
    [StringValue("ph-currency-dollar")]
    CurrencyDollar = 15,
    [StringValue("ph-currency-circle-dollar")]
    CurrencyCircleDollar = 16,
    [StringValue("ph-gear")]
    Gear = 17,
    [StringValue("ph-image")]
    Image = 18,
    [StringValue("ph-file-image")]
    ImageFile = 19,
    [StringValue("ph-calculator")]
    Calculator = 20,
    [StringValue("ph-floppy-disk")]
    Floppy = 21,
    [StringValue("ph-shopping-cart")]
    ShoppingCart = 22,
    [StringValue("ph-truck")]
    Truck = 23,
    [StringValue("ph-note")]
    Note = 24,
    [StringValue("ph-user")]
    User = 25,
    [StringValue("ph-credit-card")]
    CreditCard = 26,
    [StringValue("ph-bounding-box")]
    BoundingBox = 27,
}

public enum ModalSize
{
    [StringValue("")]
    Default = 0,
    [StringValue("-lg")]
    Large = 1,
    [StringValue("-xl")]
    XLarge = 2,
    [StringValue("-full")]
    Full = 3,
    [StringValue("-sm")]
    Small = 4,
    [StringValue("-xs")]
    Mini = 5
}
