namespace XYZLaundry.ViewModels
{
    public class CrudViewModel<T> where T : class
    {
        public string Action { get; set; }
        public int key { get; set; }
        public string AntiForgery { get; set; }
        public T Value { get; set; }
    }
}
