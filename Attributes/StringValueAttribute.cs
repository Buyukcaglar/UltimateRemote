namespace UltimateRemote.Attributes
{
    public class StringValueAttribute(string value) : Attribute
    {
        public string StringValue { get; protected set; } = value;
    }

    public class HelpTopicAttribute<TEnum>(string name, string? iconCss, bool nonSelectable = false, bool topLevel = true, TEnum[]? subTopics = null)
        : Attribute
        where TEnum : Enum
    {
        public string Name { get; protected set; } = name;

        public string? IconCss { get; protected set; } = iconCss;

        public bool NonSelectable { get; protected set; } = nonSelectable;

        public bool TopLevel { get; protected set; } = topLevel;

        public TEnum[]? SubTopics { get; protected set; } = subTopics;

        public bool HasSubTopics => SubTopics is { Length: > 1 };
    }

}
