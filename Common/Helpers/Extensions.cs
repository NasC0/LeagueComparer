namespace Helpers
{
    public static class Extensions
    {
        public static bool IsInvalid(this string self)
        {
            return string.IsNullOrWhiteSpace(self);
        }
    }
}
