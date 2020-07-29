namespace ToDo.UI.Converters
{
    public interface IConverter<in TSource, out TTarget>
    {
        TTarget Convert(TSource source);
    }
}